using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DrawSomeStuff
{
    class Helper
    {
        public string OpenFile(RichTextBox richTextBox1, PictureBox pictureBox)
        {
            string res = String.Empty;
            OpenFileDialog openGerber = new OpenFileDialog();
            openGerber.InitialDirectory = @"C:\";
            openGerber.Filter = @"gbr files (*.gbr)|*.gbr|All files (*.*)|*.*";
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
            }
            return res;
        }
    }
}
