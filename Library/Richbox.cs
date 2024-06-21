using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Library
{
    public class Richbox
    {
        private RichTextBox richTextBox;
        public Richbox(RichTextBox richText)
        {
            richTextBox = richText;
        }

        public void Msg(string format, params object[] args)
        {
            richTextBox.AppendText(string.Format(format, args)+ Environment.NewLine);
            //设置光标的位置到文本尾
            richTextBox.Select(richTextBox.TextLength, 0);
            //滚动到控件光标处
            richTextBox.ScrollToCaret();
        }

        public void Msg(Color color, string format, params object[] args)
        {
            richTextBox.SelectionStart = richTextBox.TextLength;
            richTextBox.SelectionLength = 0;
            richTextBox.SelectionColor = color;
            richTextBox.AppendText(string.Format(format, args) + Environment.NewLine);
            //设置光标的位置到文本尾
            richTextBox.Select(richTextBox.TextLength, 0);
            //滚动到控件光标处
            richTextBox.ScrollToCaret();
        }

        public void Clear()
        {
            richTextBox.Clear();
        }
    }
}
