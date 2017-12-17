using System;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Windows.Forms;

namespace DrawSomeStuff
{
    class Helper
    {
        public string OpenFile(PictureBox pictureBox = null, RichTextBox richTextBox1 = null, ToolStripButton readOneString = null,  ToolStripButton tsbDraw = null)
        {
            string res = String.Empty;
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
                    sourceStream.Read(result, 0, (int)sourceStream.Length);
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
            string[] oneString = strFile.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int maxNumber = oneString.Length;
            return (numberOfString > maxNumber-1)? "Конец файла": oneString[numberOfString];
        }
    }
}
