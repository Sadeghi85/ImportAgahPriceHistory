using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportAgahPriceHistory
{
    public partial class Form1 : Form
    {
        CookieContainer Cookies = new CookieContainer();
        string txtConsoleOldText = "";
        public Form1()
        {
            InitializeComponent();

            Console.SetOut(new MultiTextWriter(new ControlWriter(txtConsole), Console.Out));

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
        }



        private async void btnRequestCaptcha_Click(object sender, EventArgs e)
        {
            string CaptchaUrl = "https://online.agah.com/Auth/Captcha";

            var request = (HttpWebRequest)WebRequest.Create(CaptchaUrl);
            request.Method = "GET";
            request.CookieContainer = new CookieContainer();
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.1) Gecko/2008070208 Firefox/3.0.1";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.Timeout = 60000;


            using (var response = (HttpWebResponse) await request.GetResponseAsync())
            {
                foreach (Cookie tempCookie in response.Cookies)
                {
                    Cookies.Add(tempCookie);
                }

                using (var stream = response.GetResponseStream())
                {
                    pbCaptcha.Image = Bitmap.FromStream(stream);
                }
            }
            
        }

        private async void btnImport_Click(object sender, EventArgs e)
        {
            DB_BourseEntities ctx = new DB_BourseEntities();

            //List<int> tmp = new List<int>() { 2409, 2410 };
            //List<vwSecurity> Securities = ctx.vwSecurity.Where(x => tmp.Contains(x.SecurityID)).OrderBy(x => x.SecurityName).ToList();

            List<vwSecurity> Securities = ctx.vwSecurity.OrderBy(x => x.SecurityName).ToList();

            foreach (vwSecurity Security in Securities)
            {
                await Task.Run(() => ImportSingle(Security, 16));
                await Task.Run(() => ImportSingle(Security, 18));
                await Task.Run(() => ImportSingle(Security, 19));

                await Task.Run(() => ImportSingleTSE(Security));
            }
            

            


        }

        private void ImportSingle(vwSecurity Security, int AdjustmentTypeID)
        {
            int adjustment = 0;
            string adjustmentLabel = "";

            switch (AdjustmentTypeID)
            {
                case 16:
                    adjustment = 0;
                    adjustmentLabel = "بدون تعدیل";
                    break;
                case 18:
                    adjustment = 1;
                    adjustmentLabel = "افزایش سرمایه";
                    break;
                case 19:
                    adjustment = 2;
                    adjustmentLabel = "افزایش سرمایه و سود نقدی";
                    break;
                default:
                    adjustment = 0;
                    adjustmentLabel = "بدون تعدیل";
                    break;
            }

            string HistoryUrl = string.Format("https://online.agah.com/TradingView/history?symbol={0}-{1}&resolution=D&from={2}&to={3}", Security.SecuritySymbol, adjustment, DateTimeToUnixTimeStamp(DateTime.Now.Subtract(new TimeSpan(Convert.ToInt32(nudImportDays.Value),0,0,0))), DateTimeToUnixTimeStamp(DateTime.Now));

            var request = (HttpWebRequest)WebRequest.Create(HistoryUrl);
            request.Method = "GET";
            request.CookieContainer = Cookies;
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.1) Gecko/2008070208 Firefox/3.0.1";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.AllowAutoRedirect = true;
            request.Timeout = 60000;



            string responseData = "";
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        StreamReader responseReader = new StreamReader(stream);
                        responseData = responseReader.ReadToEnd();
                    }
                }

                var PriceHistory = JsonConvert.DeserializeObject<PriceHistory>(responseData);

                if (PriceHistory.s == "ok")
                {
                    Console.WriteLine(string.Format("Updating price history for \"{0}\" with adjusment of \"{1}\".\n", Security.SecurityName, adjustmentLabel));

                    int count = PriceHistory.t.Count;

                    DB_BourseEntities ctx = new DB_BourseEntities();

                    for (int i = 0; i < count; i++)
                    {
                        DateTime Date = UnixTimeStampToDateTime(PriceHistory.t[i]);
                        string DatePersian = new PersianDateTime(Date).ToString(PersianDateTimeFormat.Date);
                        //int AdjustmentType = 19;
                        int SecurityID = Security.SecurityID;
                        int ClosingPrice = PriceHistory.c[i];
                        int OpeningPrice = PriceHistory.o[i];
                        int HighestPrice = PriceHistory.h[i];
                        int LowestPrice = PriceHistory.l[i];
                        long Volume = PriceHistory.v[i];

                        tblSecurityHistory SecurityHistory = ctx.tblSecurityHistory.FirstOrDefault(x => x.Date == Date && x.SecurityID == SecurityID && x.AdjustmentTypeID == AdjustmentTypeID);

                        if (SecurityHistory != null)
                        {
                            SecurityHistory.ClosingPrice = ClosingPrice;
                            SecurityHistory.DatePersian = DatePersian;
                            SecurityHistory.HighestPrice = HighestPrice;
                            SecurityHistory.LowestPrice = LowestPrice;
                            SecurityHistory.OpeningPrice = OpeningPrice;
                            SecurityHistory.Volume = Volume;

                            ctx.SaveChanges();
                        }
                        else
                        {
                            SecurityHistory = new tblSecurityHistory();
                            SecurityHistory.AdjustmentTypeID = AdjustmentTypeID;
                            SecurityHistory.ClosingPrice = ClosingPrice;
                            SecurityHistory.Date = Date;
                            SecurityHistory.DatePersian = DatePersian;
                            SecurityHistory.HighestPrice = HighestPrice;
                            SecurityHistory.LowestPrice = LowestPrice;
                            SecurityHistory.OpeningPrice = OpeningPrice;
                            SecurityHistory.SecurityID = SecurityID;
                            SecurityHistory.Volume = Volume;

                            ctx.tblSecurityHistory.Add(SecurityHistory);
                            ctx.SaveChanges();
                        }

                    }


                    Console.WriteLine(string.Format("Done updating price history for \"{0}\" with adjusment of \"{1}\".\n", Security.SecurityName, adjustmentLabel));


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


            
        }
        public static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime();
            return dtDateTime;
        }
        public static int DateTimeToUnixTimeStamp(DateTime Date)
        {
            // Unix timestamp is seconds past epoch
            TimeSpan s = Date - new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            
            return Convert.ToInt32(s.TotalSeconds);
        }

        class PriceHistory
        {
            public string s { get; set; }
            public IList<int> t { get; set; }
            public IList<int> c { get; set; }
            public IList<int> o { get; set; }
            public IList<int> h { get; set; }
            public IList<int> l { get; set; }
            public IList<long> v { get; set; }
        }
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string LoginUrl = "https://online.agah.com/Auth/login";

            string Captcha = txtCaptcha.Text.Trim();
            string Username = txtUsername.Text.Trim();
            string Password = txtPassword.Text.Trim();

            string postString = string.Format("username={0}&password={1}&captcha={2}", Username, Password, Captcha);

            var request = (HttpWebRequest)WebRequest.Create(LoginUrl);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = Cookies;
            request.ContentLength = postString.Length;
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.1) Gecko/2008070208 Firefox/3.0.1";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.Referer = LoginUrl;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.AllowAutoRedirect = false;
            request.Timeout = 60000;




            StreamWriter requestWriter = new StreamWriter(await request.GetRequestStreamAsync());
            requestWriter.Write(postString);
            requestWriter.Close();


            using (var response = (HttpWebResponse) await request.GetResponseAsync())
            {

                //Cookies = new CookieContainer();
                foreach (Cookie tempCookie in response.Cookies)
                {
                    Cookies.Add(tempCookie);
                }

                using (var stream = response.GetResponseStream())
                {
                    StreamReader responseReader = new StreamReader(stream);
                    string responseData = responseReader.ReadToEnd();
                }

                if (response.Cookies.Count > 0)
                {
                    lblLogin.Text = "Logged In";
                }
                else
                {
                    lblLogin.Text = "Logged Out";
                }
            }

            





        }

        private void txtConsole_TextChanged(object sender, EventArgs e)
        {
            string tmpText = txtConsole.Text;

            string tmpDifference = "";

            if (txtConsoleOldText.Length > 0)
                tmpDifference = tmpText.Replace(txtConsoleOldText, "");

            if (tmpDifference.Contains("\r") || tmpDifference.Contains("\n"))
            {
                txtConsole.SelectionStart = txtConsole.Text.Length;
                txtConsole.ScrollToCaret();
            }

            txtConsoleOldText = tmpText;
        }

        private void ImportSingleTSE(vwSecurity Security)
        {
            

            string HistoryUrl = string.Format("http://www.tsetmc.com/tsev2/data/clienttype.aspx?i={0}", Security.TseID);

            var request = (HttpWebRequest)WebRequest.Create(HistoryUrl);
            request.Method = "GET";
            //request.CookieContainer = Cookies;
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.1) Gecko/2008070208 Firefox/3.0.1";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.AllowAutoRedirect = true;
            request.Timeout = 60000;

            string responseData = "";
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        StreamReader responseReader = new StreamReader(stream);
                        responseData = responseReader.ReadToEnd();
                    }
                }

                if (responseData.Count() > 0 && responseData.Contains(';'))
                {
                    string[] DateData = responseData.Split(';');

                    Console.WriteLine(string.Format("Updating natural/legal history for \"{0}\".\n", Security.SecurityName));

                    foreach (string Data in DateData)
                    {
                        string[] nlData = Data.Split(',');

                        DateTime Date = DateTime.ParseExact(nlData[0], "yyyyMMdd", CultureInfo.InvariantCulture);
                        int NaturalBuyCount = Convert.ToInt32(nlData[1]);
                        int LegalBuyCount = Convert.ToInt32(nlData[2]);
                        int NaturalSellCount = Convert.ToInt32(nlData[3]);
                        int LegalSellCount = Convert.ToInt32(nlData[4]);
                        long NaturalBuyVolume = Convert.ToInt64(nlData[5]);
                        long LegalBuyVolume = Convert.ToInt64(nlData[6]);
                        long NaturalSellVolume = Convert.ToInt64(nlData[7]);
                        long LegalSellVolume = Convert.ToInt64(nlData[8]);

                        if (Date >= DateTime.Now.Subtract(new TimeSpan(Convert.ToInt32(nudImportDays.Value), 0, 0, 0)))
                        {
                            DB_BourseEntities ctx = new DB_BourseEntities();
                            tblSecurityHistory SecurityHistory = new tblSecurityHistory();

                            // بدون تعدیل
                            SecurityHistory = ctx.tblSecurityHistory.FirstOrDefault(x => x.SecurityID == Security.SecurityID && x.Date == Date & x.AdjustmentTypeID == 16);
                            if (SecurityHistory != null)
                            {
                                SecurityHistory.LegalBuyCount = LegalBuyCount;
                                SecurityHistory.LegalBuyVolume = LegalBuyVolume;
                                SecurityHistory.LegalSellCount = LegalSellCount;
                                SecurityHistory.LegalSellVolume = LegalSellVolume;
                                SecurityHistory.NaturalBuyCount = NaturalBuyCount;
                                SecurityHistory.NaturalBuyVolume = NaturalBuyVolume;
                                SecurityHistory.NaturalSellCount = NaturalSellCount;
                                SecurityHistory.NaturalSellVolume = NaturalSellVolume;
                                ctx.SaveChanges();
                            }


                            // افزایش سرمایه
                            SecurityHistory = ctx.tblSecurityHistory.FirstOrDefault(x => x.SecurityID == Security.SecurityID && x.Date == Date & x.AdjustmentTypeID == 18);
                            if (SecurityHistory != null)
                            {
                                SecurityHistory.LegalBuyCount = LegalBuyCount;
                                SecurityHistory.LegalBuyVolume = LegalBuyVolume;
                                SecurityHistory.LegalSellCount = LegalSellCount;
                                SecurityHistory.LegalSellVolume = LegalSellVolume;
                                SecurityHistory.NaturalBuyCount = NaturalBuyCount;
                                SecurityHistory.NaturalBuyVolume = NaturalBuyVolume;
                                SecurityHistory.NaturalSellCount = NaturalSellCount;
                                SecurityHistory.NaturalSellVolume = NaturalSellVolume;
                                ctx.SaveChanges();
                            }

                            // افزایش سرمایه و سود نقدی
                            SecurityHistory = ctx.tblSecurityHistory.FirstOrDefault(x => x.SecurityID == Security.SecurityID && x.Date == Date & x.AdjustmentTypeID == 19);
                            if (SecurityHistory != null)
                            {
                                SecurityHistory.LegalBuyCount = LegalBuyCount;
                                SecurityHistory.LegalBuyVolume = LegalBuyVolume;
                                SecurityHistory.LegalSellCount = LegalSellCount;
                                SecurityHistory.LegalSellVolume = LegalSellVolume;
                                SecurityHistory.NaturalBuyCount = NaturalBuyCount;
                                SecurityHistory.NaturalBuyVolume = NaturalBuyVolume;
                                SecurityHistory.NaturalSellCount = NaturalSellCount;
                                SecurityHistory.NaturalSellVolume = NaturalSellVolume;
                                ctx.SaveChanges();
                            }
                        }
                        else
                        {
                            break;
                        }

                        
                    }

                    Console.WriteLine(string.Format("Done updating natural/legal history for \"{0}\".\n", Security.SecurityName));
                }

                               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }



        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    string tse = @"C:\Users\Admin\Desktop\tse.txt";

        //    try
        //    {
        //        var lines = File.ReadLines(tse);
        //        foreach (var line in lines)
        //        {
        //            string[] l = line.Split('-');

        //            string SecurityName = l[0];
        //            long TseID = Convert.ToInt64(l[1]);

        //            DB_BourseEntities ctx = new DB_BourseEntities();

        //            tblSecurity Security = ctx.tblSecurity.FirstOrDefault(x => x.SecurityName == SecurityName);
        //            if (Security != null)
        //            {
        //                Security.TseID = TseID;
        //                ctx.SaveChanges();
        //                Console.WriteLine(string.Format("Security \"{0}\" updated.", SecurityName));
        //            }
        //            else
        //            {
        //                Console.WriteLine(string.Format("Security \"{0}\" not found.", SecurityName));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }

        //}
    }
}
