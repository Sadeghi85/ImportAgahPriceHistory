using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportAgahPriceHistory
{
    public class ControlWriter : TextWriter
    {
        delegate void SetTextCallbackString(string text);
        delegate void SetTextCallbackChar(char text);
        private Control textbox;
        public ControlWriter(Control textbox)
        {
            this.textbox = textbox;
        }

        public override void Write(char value)
        {
            //textbox.Text += value;
            if (textbox.InvokeRequired)
            {
                SetTextCallbackChar d = new SetTextCallbackChar(Write);
                textbox.Invoke(d, new object[] { value });
            }
            else
            {
                if (value == '\n')
                {
                    textbox.Text += value;

                    textbox.Text = Regex.Replace(textbox.Text, @"^(.*?)((?<=\n)[^\n]+\n)$", "$2$1", RegexOptions.Singleline);
                }
                else
                {
                    textbox.Text += value;
                }
                    
            }
        }

        public override void Write(string value)
        {
            //textbox.Text += value;
            if (textbox.InvokeRequired)
            {
                SetTextCallbackString d = new SetTextCallbackString(Write);
                textbox.Invoke(d, new object[] { value });
            }
            else
            {
                textbox.Text += value;
            }
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }
    public class MultiTextWriter : TextWriter
    {
        private IEnumerable<TextWriter> writers;
        public MultiTextWriter(IEnumerable<TextWriter> writers)
        {
            this.writers = writers.ToList();
        }
        public MultiTextWriter(params TextWriter[] writers)
        {
            this.writers = writers;
        }

        public override void Write(char value)
        {
            foreach (var writer in writers)
                writer.Write(value);
        }

        public override void Write(string value)
        {
            foreach (var writer in writers)
                writer.Write(value);
        }

        public override void Flush()
        {
            foreach (var writer in writers)
                writer.Flush();
        }

        public override void Close()
        {
            foreach (var writer in writers)
                writer.Close();
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }
    }
}
