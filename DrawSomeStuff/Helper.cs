using System;
using System.Drawing;
using System.IO;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace DrawSomeStuff
{
    class Helper
    {
        public string OpenFile(PictureBox pictureBox, RichTextBox richTextBox1,
            ToolStripButton readOneString = null, ToolStripButton tsbDraw = null)
        {
            Graphics g = pictureBox.CreateGraphics();
            g.Clear(Color.AliceBlue);
            pictureBox.BackColor = Color.AliceBlue;

            string res = String.Empty;
            richTextBox1.Clear();
            OpenFileDialog openGerber = new OpenFileDialog();
            openGerber.InitialDirectory = @"C:\";
            openGerber.Filter = @"gbr files (*.grb)|*.grb|All files (*.*)|*.*";
            openGerber.FilterIndex = 2;
            openGerber.RestoreDirectory = true;
            if (openGerber.ShowDialog() == DialogResult.OK)
            {
                byte[] result;
                using (FileStream sourceStream = File.Open(openGerber.FileName, FileMode.Open))
                {
                    result = new byte[sourceStream.Length];
                    sourceStream.Read(result, 0, (int) sourceStream.Length);
                }
                res = Encoding.ASCII.GetString(result);
                richTextBox1.Visible = true;
                pictureBox.Visible = true;
                if (res != String.Empty)
                {
                    readOneString.Enabled = true;
                    tsbDraw.Enabled = true;
                }
            }
            return res;
        }

        public string GetOneString(string strFile, int numberOfString)
        {
            string[] oneString = strFile.Split(new char[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
            int maxNumber = oneString.Length;
            return (numberOfString > maxNumber - 1) ? "Конец файла" : oneString[numberOfString];
        }

        public void GetProportion(string gerberFile, PictureBox pBox, ref double proportionX, ref double proportionY)
        {
            int numberOfString = 0;
            string[] oneString = gerberFile.Split(new char[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
            int maxNumber = oneString.Length;
            int x = 0;
            int y = 0;
            while (numberOfString < maxNumber)
            {
                var res = oneString[numberOfString];
                if (res.Contains(@"D01") || res.Contains(@"D02") || res.Contains(@"D03"))
                {
                    int posXinStr = res.IndexOf("X", StringComparison.Ordinal) + 1;
                    int posYinStr = res.IndexOf("Y", StringComparison.Ordinal) + 1;
                    int posDinStr = res.IndexOf("D", StringComparison.Ordinal);
                    var serv = new Parser();
                    int newX = Convert.ToInt32(serv.StrimTrim(res.Substring(posXinStr,
                        posYinStr - posXinStr - 1))); //новыя позиция х
                    int newY = Convert.ToInt32(serv.StrimTrim(res.Substring(posYinStr,
                        posDinStr - posYinStr))); // новая позиция y
                    x = Math.Max(x, newX);
                    y = Math.Max(y, newY);
                }
                numberOfString++;
            }
            if (x != 0 && y != 0)
            {
                proportionX = (double)(pBox.Size.Width-200) / x;
                proportionY = (double)(pBox.Size.Height-100) / y;
            }
        }
    }
}

