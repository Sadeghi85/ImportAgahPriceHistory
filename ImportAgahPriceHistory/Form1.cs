using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportAgahPriceHistory
{
    public partial class Form1 : Form
    {
        CookieContainer Cookies = new CookieContainer();
        string PostData = "";

        string txtConsoleOldText = "";
        public Form1()
        {
            InitializeComponent();

            //Console.SetOut(new MultiTextWriter(new ControlWriter(txtConsole), Console.Out));
            //Console.OutputEncoding = System.Text.Encoding.Unicode;

            dtpStartDate.Value = DateTime.Now - new TimeSpan(7, 0, 0, 0);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
        }



        private async void btnRequestCaptcha_Click(object sender, EventArgs e)
        {
            string CaptchaUrl = "https://online.agah.com/Auth/Captcha";

            Cookies = new CookieContainer();

            var request = (HttpWebRequest)WebRequest.Create(CaptchaUrl);
            request.Method = "GET";
            request.CookieContainer = new CookieContainer();
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.1) Gecko/2008070208 Firefox/3.0.1";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.Timeout = 60000;


            using (var response = (HttpWebResponse)await request.GetResponseAsync())
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
            try
            {
                DB_BourseEntities ctx = new DB_BourseEntities();

                //List<int> tmp = new List<int>() { 2236, 2237, 2553, 2554, 2555, 2556, 2557, 2559, 2560, 2561, 2562, 2563, 2564, 2570, 2571, 2572, 2573, 2574, 2575, 2576, 2577, 2578, 2579 };
                //List<vwSecurity> Securities = ctx.vwSecurity.Where(x => tmp.Contains(x.SecurityID)).OrderBy(x => x.SecurityName).ToList();

                List<vwSecurity> Securities = ctx.vwSecurity.OrderBy(x => x.SecurityName).ToList();

                foreach (vwSecurity Security in Securities)
                {
                    await Task.Run(() => ImportSingleAgah(Security, 16));
                    await Task.Run(() => ImportSingleAgah(Security, 18));
                    await Task.Run(() => ImportSingleAgah(Security, 19));

                }

                Debug.WriteLine(string.Format("Done.\n"));

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }

        private async void btnImportRahavard365_Click(object sender, EventArgs e)
        {
            try
            {
                DB_BourseEntities ctx = new DB_BourseEntities();

                //List<int> tmp = new List<int>() { 2236, 2148, 2271, 2420, 2520 };
                //List<vwSecurity> Securities = ctx.vwSecurity.Where(x => tmp.Contains(x.SecurityID)).OrderBy(x => x.SecurityName).ToList();

                List<vwSecurity> Securities = ctx.vwSecurity.OrderBy(x => x.SecurityName).ToList();

                //Parallel.ForEach(Securities, (Security) =>
                //{
                //    ImportSingleRahavard365(Security, 16);
                //    //ImportSingleRahavard365(Security, 18);
                //    //ImportSingleRahavard365(Security, 19);
                //    ImportSingleRahavard365(Security, 1020);
                //});

                foreach (vwSecurity Security in Securities)
                {

                    await Task.Run(() => ImportSingleRahavard365(Security, 16));
                    await Task.Run(() => ImportSingleRahavard365(Security, 1021));

                }

                Debug.WriteLine(string.Format("Done.\n"));

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void btnImportTse_Click(object sender, EventArgs e)
        {
            try
            {
                DB_BourseEntities ctx = new DB_BourseEntities();

                //List<int> tmp = new List<int>() { 2236, 2148, 2271, 2420, 2520 };
                //List<vwSecurity> Securities = ctx.vwSecurity.Where(x => tmp.Contains(x.SecurityID)).OrderBy(x => x.SecurityName).ToList();

                List<vwSecurity> Securities = ctx.vwSecurity.OrderBy(x => x.SecurityName).ToList();

                Parallel.ForEach(Securities, (Security) =>
                {
                    ImportSingleTSE(Security);

                });

                //foreach (vwSecurity Security in Securities)
                //{
                //    //Thread.Sleep(100);
                //    Task.Run(() => ImportSingleTSE(Security));
                //    //await Task.Run(() => ImportSingleTSE(Security));

                //}

                Debug.WriteLine(string.Format("Done.\n"));

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void ImportSingleRahavard365(vwSecurity Security, int AdjustmentTypeID)
        {
            try
            {
                HttpWebRequest request = null;
                string HistoryUrl = "";
                string responseData = "";
                HttpWebRequest request2 = null;
                string HistoryUrl2 = "";
                string responseData2 = "";

                string adjustment = "";
                string adjustmentLabel = "";

                switch (AdjustmentTypeID)
                {
                    case 16:
                        adjustment = "";
                        adjustmentLabel = "بدون تعدیل";
                        break;
                    case 18:
                        adjustment = ":type1";
                        adjustmentLabel = "افزایش سرمایه";
                        break;
                    case 19:
                        adjustment = ":type3";
                        adjustmentLabel = "افزایش سرمایه و سود نقدی";
                        break;
                    case 1020:
                        adjustment = ":type4";
                        adjustmentLabel = "افزایش سرمایه و سود نقدی با احتساب آورده";
                        break;
                    case 1021:
                        adjustment = ":type5";
                        adjustmentLabel = "عملکردی";
                        break;
                    default:
                        adjustment = "";
                        adjustmentLabel = "بدون تعدیل";
                        break;
                }


                DateTime StartDate = dtpStartDate.Value.Date;

                HistoryUrl = string.Format("https://rahavard365.com/api/chart/bars?ticker=exchange.asset:{0}:real_close{1}&resolution=D&startDateTime={2}&endDateTime={3}&firstDataRequest=false", Security.Rahavard365ID, adjustment, DateTimeToUnixTimeStamp(StartDate), DateTimeToUnixTimeStamp(DateTime.Now));

                request = (HttpWebRequest)WebRequest.Create(HistoryUrl);
                request.Method = "GET";
                request.CookieContainer = Cookies;
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.1) Gecko/2008070208 Firefox/3.0.1";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                request.AllowAutoRedirect = true;
                request.Timeout = 30000;

                responseData = "";

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        StreamReader responseReader = new StreamReader(stream);
                        responseData = responseReader.ReadToEnd();
                    }
                }


                ////////////////////////////////////////////////////
                HistoryUrl2 = string.Format("https://rahavard365.com/api/chart/bars?ticker=exchange.asset:{0}:close{1}&resolution=D&startDateTime={2}&endDateTime={3}&firstDataRequest=false", Security.Rahavard365ID, adjustment, DateTimeToUnixTimeStamp(StartDate), DateTimeToUnixTimeStamp(DateTime.Now));

                request2 = (HttpWebRequest)WebRequest.Create(HistoryUrl2);
                request2.Method = "GET";
                request2.CookieContainer = Cookies;
                request2.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.1) Gecko/2008070208 Firefox/3.0.1";
                request2.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request2.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                request2.AllowAutoRedirect = true;
                request2.Timeout = 30000;

                responseData2 = "";

                using (var response2 = (HttpWebResponse)request2.GetResponse())
                {
                    using (var stream2 = response2.GetResponseStream())
                    {
                        StreamReader responseReader2 = new StreamReader(stream2);
                        responseData2 = responseReader2.ReadToEnd();
                    }
                }


                //////////////////////////////////////////////////////


                if (responseData.Contains("[{") && responseData2.Contains("[{"))
                {
                    //PriceHistoryRahavard365Data PriceHistoryData = JsonConvert.DeserializeObject<PriceHistoryRahavard365Data>(responseData);
                    //List<PriceHistoryRahavard365> PriceHistoryList = PriceHistoryData.data;
                    List<PriceHistoryRahavard365> PriceHistoryList = JsonConvert.DeserializeObject<List<PriceHistoryRahavard365>>(responseData);
                    List<PriceHistoryRahavard365> PriceHistoryList2 = JsonConvert.DeserializeObject<List<PriceHistoryRahavard365>>(responseData2);

                    Debug.WriteLine(string.Format("Updating price history for \"{0}\" with adjusment of \"{1}\".\n", Security.SecurityName, adjustmentLabel));
                    //Console.WriteLine(string.Format("Updating price history for \"{0}\" with adjusment of \"{1}\".\n", Security.SecurityName, adjustmentLabel));


                    using (DB_BourseEntities ctx = new DB_BourseEntities())
                    {
                        using (var transaction = ctx.Database.BeginTransaction())
                        {
                            try
                            {
                                ctx.tblSecurityHistory.RemoveRange(ctx.tblSecurityHistory.Where(x => x.SecurityID == Security.SecurityID && x.AdjustmentTypeID == AdjustmentTypeID && x.Date >= StartDate));
                                ctx.SaveChanges();

                                List<tblSecurityHistory> tblSecurityHistories = new List<tblSecurityHistory>();

                                for (var i = 0; i < PriceHistoryList.Count; i++)
                                //foreach (PriceHistoryRahavard365 PriceHistory in PriceHistoryList)
                                {
                                    DateTime Date = UnixTimeStampToDateTime(Convert.ToInt32(PriceHistoryList[i].time / 1000 + 4.5 * 3600)).Date;
                                    string DatePersian = new PersianDateTime(Date).ToString(PersianDateTimeFormat.Date);
                                    int SecurityID = Security.SecurityID;
                                    double ClosingPrice = Convert.ToDouble(PriceHistoryList[i].close);
                                    //////////////////
                                    double VolumeWeightedPrice = Convert.ToDouble(PriceHistoryList2[i].close);
                                    /////////////////
                                    double OpeningPrice = Convert.ToDouble(PriceHistoryList[i].open);
                                    double HighestPrice = Convert.ToDouble(PriceHistoryList[i].high);
                                    double LowestPrice = Convert.ToDouble(PriceHistoryList[i].low);
                                    long Volume = PriceHistoryList[i].volume;


                                    tblSecurityHistory tblSecurityHistory = new tblSecurityHistory();

                                    tblSecurityHistory.AdjustmentTypeID = AdjustmentTypeID;
                                    tblSecurityHistory.Date = Date;
                                    tblSecurityHistory.DatePersian = DatePersian;
                                    tblSecurityHistory.ClosingPrice = ClosingPrice;
                                    tblSecurityHistory.VolumeWeightedPrice = VolumeWeightedPrice;
                                    tblSecurityHistory.HighestPrice = HighestPrice;
                                    tblSecurityHistory.LowestPrice = LowestPrice;
                                    tblSecurityHistory.OpeningPrice = OpeningPrice;
                                    tblSecurityHistory.SecurityID = SecurityID;
                                    tblSecurityHistory.Volume = Volume;

                                    tblSecurityHistories.Add(tblSecurityHistory);



                                }

                                ctx.BulkInsert(tblSecurityHistories);


                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                            }
                        }
                    }



                    //Console.WriteLine(string.Format("Done updating price history for \"{0}\" with adjusment of \"{1}\".\n", Security.SecurityName, adjustmentLabel));
                    Debug.WriteLine(string.Format("Done updating price history for \"{0}\" with adjusment of \"{1}\".\n", Security.SecurityName, adjustmentLabel));

                }
                else
                {
                    if (!responseData.Contains("nextTime"))
                    {
                        //Console.WriteLine(string.Format("Error updating price history for \"{0}\" with adjusment of \"{1}\".\n", Security.SecurityName, adjustmentLabel));
                        Debug.WriteLine(string.Format("Error updating price history for \"{0}\" with adjusment of \"{1}\".\n", Security.SecurityName, adjustmentLabel));
                    }
                }
                


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }



        }

        private void ImportSingleAgah(vwSecurity Security, int AdjustmentTypeID)
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


            DateTime StartDate = dtpStartDate.Value.Date;

            //string HistoryUrl = string.Format("https://online.agah.com/TradingView/history?symbol={0}-{1}&resolution=D&from={2}&to={3}", Security.SecuritySymbol, adjustment, DateTimeToUnixTimeStamp(DateTime.Now.Subtract(new TimeSpan(Convert.ToInt32(nudImportDays.Value), 0, 0, 0))), DateTimeToUnixTimeStamp(DateTime.Now));
            string HistoryUrl = string.Format("https://online.agah.com/TradingView/history?symbol={0}-{1}&resolution=D&from={2}&to={3}", Security.SecuritySymbol, adjustment, DateTimeToUnixTimeStamp(StartDate), DateTimeToUnixTimeStamp(DateTime.Now));

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

                var PriceHistory = JsonConvert.DeserializeObject<PriceHistoryAgah>(responseData);

                if (PriceHistory.s == "ok")
                {
                    Debug.WriteLine(string.Format("Updating price history for \"{0}\" with adjusment of \"{1}\".\n", Security.SecurityName, adjustmentLabel));

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


                    Debug.WriteLine(string.Format("Done updating price history for \"{0}\" with adjusment of \"{1}\".\n", Security.SecurityName, adjustmentLabel));


                }
                else
                {
                    Debug.WriteLine(string.Format("Error updating price history for \"{0}\" with adjusment of \"{1}\".\n", Security.SecurityName, adjustmentLabel));
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
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

        class PriceHistoryRahavard365Data
        {
            public List<PriceHistoryRahavard365> data { get; set; }
        }
        class PriceHistoryRahavard365
        {
            public double time { get; set; }
            public double open { get; set; }
            public double high { get; set; }
            public double low { get; set; }
            public double close { get; set; }
            public long volume { get; set; }
        }
        class PriceHistoryAgah
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


            using (var response = (HttpWebResponse)await request.GetResponseAsync())
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

                if (response.Cookies.Count >= 2)
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
            //string tmpText = txtConsole.Text;

            //string tmpDifference = "";

            //if (txtConsoleOldText.Length > 0)
            //    tmpDifference = tmpText.Replace(txtConsoleOldText, "");

            //if (tmpDifference.Contains("\r") || tmpDifference.Contains("\n"))
            //{
            //    txtConsole.SelectionStart = txtConsole.Text.Length;
            //    txtConsole.ScrollToCaret();
            //}

            //txtConsoleOldText = tmpText;
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

                    Debug.WriteLine(string.Format("Updating natural/legal history for \"{0}\".\n", Security.SecurityName));


                    using (DB_BourseEntities ctx = new DB_BourseEntities())
                    {
                        using (var transaction = ctx.Database.BeginTransaction())
                        {
                            try
                            {
                                DateTime StartDate = dtpStartDate.Value.Date;

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

                                    //if (Date >= DateTime.Now.Subtract(new TimeSpan(Convert.ToInt32(nudImportDays.Value), 0, 0, 0)))
                                    //if (Date >= new DateTime(2011, 3, 21, 0, 0, 0))



                                    if (Date >= StartDate)
                                    {
                                        //DB_BourseEntities ctx = new DB_BourseEntities();
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

                                            // Force Update
                                            ctx.Entry(SecurityHistory).State = System.Data.Entity.EntityState.Modified;
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

                                            // Force Update
                                            ctx.Entry(SecurityHistory).State = System.Data.Entity.EntityState.Modified;
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

                                            // Force Update
                                            ctx.Entry(SecurityHistory).State = System.Data.Entity.EntityState.Modified;
                                            ctx.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }


                                }


                                // Fake update to engage the trigger on null legal/natural values to update VolumeStrength
                                List<tblSecurityHistory> SecurityHistoryList = ctx.tblSecurityHistory.Where(x => x.SecurityID == Security.SecurityID && x.NaturalBuyCount == null && x.Date >= StartDate).ToList();
                                foreach (tblSecurityHistory SecurityHistory in SecurityHistoryList)
                                {
                                    SecurityHistory.DatePersian = SecurityHistory.DatePersian;
                                    // Force Update
                                    ctx.Entry(SecurityHistory).State = System.Data.Entity.EntityState.Modified;
                                    ctx.SaveChanges();
                                }

                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                            }
                        }
                    }




                    Debug.WriteLine(string.Format("Done updating natural/legal history for \"{0}\".\n", Security.SecurityName));
                }
                else
                {
                    Debug.WriteLine(string.Format("Error updating natural/legal history for \"{0}\".\n", Security.SecurityName));
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }



        }

        private void ImportSingleTSEInfo(vwSecurity Security)
        {
            string StatusUrl = string.Format("http://tsetmc.com/Loader.aspx?ParTree=151311&i={0}", Security.TseID);

            var request = (HttpWebRequest)WebRequest.Create(StatusUrl);
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
                Debug.WriteLine(string.Format("Updating info for \"{0}\".\n", Security.SecurityName));

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        StreamReader responseReader = new StreamReader(stream);
                        responseData = responseReader.ReadToEnd();
                    }
                }

                if (responseData.Count() > 0 && responseData.Contains("ZTitad") && responseData.Contains("EstimatedEPS") && responseData.Contains("BaseVol"))
                {
                    long BaseVolume = -1;
                    long SharesCount = -1;
                    int EPS = -1;

                    Match match;

                    match = Regex.Match(responseData, @"BaseVol *= *'?(\d+)'?");
                    if (!string.IsNullOrEmpty(match.Groups[1].Value))
                    {
                        BaseVolume = Convert.ToInt64(match.Groups[1].Value);
                    }

                    match = Regex.Match(responseData, @"ZTitad *= *'?(\d+)'?");
                    if (!string.IsNullOrEmpty(match.Groups[1].Value))
                    {
                        SharesCount = Convert.ToInt64(match.Groups[1].Value);
                    }


                    match = Regex.Match(responseData, @"EstimatedEPS *= *'?(-?\d+)'?");
                    if (!string.IsNullOrEmpty(match.Groups[1].Value))
                    {
                        EPS = Convert.ToInt32(match.Groups[1].Value);
                    }



                    DB_BourseEntities ctx = new DB_BourseEntities();
                    tblSecurity TSecurity = new tblSecurity();

                    TSecurity = ctx.tblSecurity.FirstOrDefault(x => x.SecurityID == Security.SecurityID);
                    if (TSecurity != null)
                    {
                        TSecurity.EPS = EPS;
                        TSecurity.SharesCount = SharesCount;
                        TSecurity.BaseVolume = BaseVolume;
                        ctx.SaveChanges();
                    }
                }
                else
                {
                    //DB_BourseEntities ctx = new DB_BourseEntities();
                    //tblSecurity TSecurity = new tblSecurity();

                    //TSecurity = ctx.tblSecurity.FirstOrDefault(x => x.SecurityID == Security.SecurityID);
                    //if (TSecurity != null)
                    //{
                    //    TSecurity.StatusID = 1016;
                    //    ctx.SaveChanges();
                    //}
                }

                Debug.WriteLine(string.Format("Done updating info for \"{0}\".\n", Security.SecurityName));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        private void ImportSingleTSEStatus(vwSecurity Security)
        {
            string StatusUrl = string.Format("http://www.tsetmc.com/tsev2/data/Supervision.aspx?i={0}", Security.TseID);

            var request = (HttpWebRequest)WebRequest.Create(StatusUrl);
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
                Debug.WriteLine(string.Format("Updating status for \"{0}\".\n", Security.SecurityName));

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        StreamReader responseReader = new StreamReader(stream);
                        responseData = responseReader.ReadToEnd();
                    }
                }

                if (responseData.Count() > 0 && responseData.Contains('#'))
                {
                    List<string> Data = responseData.Split('#').ToList();
                    string Status = Data[0];
                    Data.RemoveAt(0);
                    string StatusDescription = string.Join(" - ", Data);
                    int StatusID = 1016;

                    switch (Status)
                    {
                        case "1":
                            StatusID = 1017;
                            break;
                        case "2":
                            StatusID = 1018;
                            break;
                        case "3":
                            StatusID = 1019;
                            break;
                        default:
                            StatusID = 1016;
                            break;
                    }


                    DB_BourseEntities ctx = new DB_BourseEntities();
                    tblSecurity TSecurity = new tblSecurity();


                    TSecurity = ctx.tblSecurity.FirstOrDefault(x => x.SecurityID == Security.SecurityID);
                    if (TSecurity != null)
                    {
                        TSecurity.StatusID = StatusID;
                        TSecurity.StatusDescription = StatusDescription;
                        ctx.SaveChanges();
                    }



                }
                else
                {
                    DB_BourseEntities ctx = new DB_BourseEntities();
                    tblSecurity TSecurity = new tblSecurity();

                    TSecurity = ctx.tblSecurity.FirstOrDefault(x => x.SecurityID == Security.SecurityID);
                    if (TSecurity != null)
                    {
                        TSecurity.StatusID = 1016;
                        ctx.SaveChanges();
                    }
                }

                Debug.WriteLine(string.Format("Done updating status for \"{0}\".\n", Security.SecurityName));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void btnImportTseStatus_Click(object sender, EventArgs e)
        {
            try
            {
                DB_BourseEntities ctx = new DB_BourseEntities();

                //List<int> tmp = new List<int>() { 2236, 2148, 2271, 2420, 2520 };
                //List<vwSecurity> Securities = ctx.vwSecurity.Where(x => tmp.Contains(x.SecurityID)).OrderBy(x => x.SecurityName).ToList();

                List<vwSecurity> Securities = ctx.vwSecurity.OrderBy(x => x.SecurityName).ToList();

                Parallel.ForEach(Securities, (Security) =>
                {
                    ImportSingleTSEStatus(Security);
                    ImportSingleTSEInfo(Security);

                });

                //foreach (vwSecurity Security in Securities)
                //{
                //    //await Task.Run(() => ImportSingleTSE(Security));
                //    Task.Run(() => ImportSingleTSEStatus(Security));
                //    Task.Run(() => ImportSingleTSEInfo(Security));
                //}

                Debug.WriteLine(string.Format("Done.\n"));

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void btnRequestCaptchaRahavard_Click(object sender, EventArgs e)
        {
            try
            {


                string LoginUrl = "https://rahavard365.com/login";

                Cookies = new CookieContainer();
                PostData = "";

                var request = (HttpWebRequest)WebRequest.Create(LoginUrl);
                request.Method = "GET";
                request.CookieContainer = new CookieContainer();
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.1) Gecko/2008070208 Firefox/3.0.1";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.Timeout = 60000;

                string responseData = "";

                using (var response = (HttpWebResponse)await request.GetResponseAsync())
                {
                    foreach (Cookie tempCookie in response.Cookies)
                    {
                        Cookies.Add(tempCookie);
                    }

                    using (var stream = response.GetResponseStream())
                    {
                        StreamReader responseReader = new StreamReader(stream);
                        responseData = responseReader.ReadToEnd();
                    }
                }


                string CaptchaGuid = "";
                string RequestVerificationToken = "";

                Match m;

                m = Regex.Match(responseData, @"<input[^<>]*name=""__RequestVerificationToken""[^<>]*value=""([^""]+)""");

                RequestVerificationToken = m.Groups[1].Value;

                m = Regex.Match(responseData, @"<input[^<>]*name=""captcha-guid""[^<>]*value=""([^""]+)""");

                CaptchaGuid = m.Groups[1].Value;

                PostData = string.Format("__RequestVerificationToken={0}&captcha-guid={1}&LoginModel.RememberMe=false", RequestVerificationToken, CaptchaGuid);


                string CaptchaUrl = string.Format("https://rahavard365.com/captcha.ashx?guid={0}", CaptchaGuid);

                request = (HttpWebRequest)WebRequest.Create(CaptchaUrl);
                request.Method = "GET";
                request.CookieContainer = Cookies;
                request.Referer = LoginUrl;
                request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.1) Gecko/2008070208 Firefox/3.0.1";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.Timeout = 60000;


                using (var response = (HttpWebResponse)await request.GetResponseAsync())
                {
                    //foreach (Cookie tempCookie in response.Cookies)
                    //{
                    //    Cookies.Add(tempCookie);
                    //}

                    using (var stream = response.GetResponseStream())
                    {
                        pbCaptchaRahavard.Image = Bitmap.FromStream(stream);
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Can't get Captcha.");
            }
        }

        private async void btnLoginRahavard_Click(object sender, EventArgs e)
        {
            string LoginUrl = "https://rahavard365.com/login";

            string RequestVerificationToken = "";

            var request = (HttpWebRequest)WebRequest.Create(LoginUrl);
            request.Method = "GET";
            request.CookieContainer = Cookies;
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.1) Gecko/2008070208 Firefox/3.0.1";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.Referer = LoginUrl;
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.AllowAutoRedirect = false;
            request.Timeout = 60000;

            using (var response = (HttpWebResponse)await request.GetResponseAsync())
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

                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(responseData);

                    RequestVerificationToken =
                      (from form in doc.DocumentNode.Descendants("form").Where(node => node.Id == "loginForm")
                      from input in form.Descendants("input").Where(node => node.Attributes["name"].Value == "__RequestVerificationToken")
                      select  input.GetAttributeValue("value", "")).FirstOrDefault();

                }
            }



            LoginUrl = "https://rahavard365.com/login";

            string Captcha = txtCaptchaRahavard.Text.Trim();
            string Username = txtUsernameRahavard.Text.Trim();
            string Password = txtPasswordRahavard.Text.Trim();

            //string postString = string.Format("{0}&LoginModel.Username={1}&LoginModel.Password={2}&captcha={3}&__RequestVerificationToken={4}", PostData, Username, Password, Captcha, RequestVerificationToken);
            string postString = string.Format("{0}&LoginModel.Username={1}&LoginModel.Password={2}&captcha={3}", PostData, Username, Password, Captcha);

            request = (HttpWebRequest)WebRequest.Create(LoginUrl);
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


            using (var response = (HttpWebResponse)await request.GetResponseAsync())
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

                //if (response.Cookies.Count >= 2)
                if (response.Cookies[".rahavard365auth"] != null)
                {
                    lblLoginRahavard.Text = "Logged In";
                }
                else
                {
                    lblLoginRahavard.Text = "Logged Out";
                }
            }
        }





        //private void button1_Click(object sender, EventArgs e)
        //{
        //    string rah = @"C:\Users\Admin\Desktop\rah.txt";

        //    try
        //    {
        //        var lines = File.ReadLines(rah);
        //        foreach (var line in lines)
        //        {
        //            string[] l = line.Split('-');

        //            string SecurityName = l[0];
        //            int RahID = Convert.ToInt32(l[1]);

        //            DB_BourseEntities ctx = new DB_BourseEntities();

        //            tblSecurity Security = ctx.tblSecurity.FirstOrDefault(x => x.SecurityName == SecurityName);
        //            if (Security != null)
        //            {
        //                Security.Rahavard365ID = RahID;
        //                ctx.SaveChanges();
        //                Debug.WriteLine(string.Format("Security \"{0}\" updated.", SecurityName));
        //            }
        //            else
        //            {
        //                Debug.WriteLine(string.Format("Security \"{0}\" not found.", SecurityName));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }

        //}
        //private async void button1_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DB_BourseEntities ctx = new DB_BourseEntities();

        //        //List<int> tmp = new List<int>() { 2409, 2410 };
        //        //List<vwSecurity> Securities = ctx.vwSecurity.Where(x => tmp.Contains(x.SecurityID)).OrderBy(x => x.SecurityName).ToList();

        //        List<vwSecurity> Securities = ctx.vwSecurity.OrderBy(x => x.SecurityName).ToList();

        //        foreach (vwSecurity Security in Securities)
        //        {

        //            await Task.Run(() => ImportSingleTSEStatus(Security));
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //}

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
        //                Debug.WriteLine(string.Format("Security \"{0}\" updated.", SecurityName));
        //            }
        //            else
        //            {
        //                Debug.WriteLine(string.Format("Security \"{0}\" not found.", SecurityName));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }

        //}
    }
}
