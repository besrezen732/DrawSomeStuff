using System;
using System.Drawing;
using System.Windows.Forms;

namespace DrawSomeStuff
{
    public partial class laptop : Form
    {
        Parser _Parser;
        Graphics gr;
        Pen pen;
        SolidBrush brush;
        private string stringFile;

        public laptop()
        {
            InitializeComponent();
            initGraphics();
            richTextBox1.Visible = false;
            pictureBox.Visible = false;
            readOneString.Enabled = false;
            tsbDraw.Enabled = false;
        }

        void initGraphics()
        {
            gr = pictureBox.CreateGraphics();
            pen = new Pen(Color.Black, 3);
            brush = new SolidBrush(Color.Black);
            _Parser = new Parser();
        }

        
        private void tsbDraw_Click(object sender, EventArgs e)
        {
            //drawGraphics();
        }
        

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var service = new Helper();
            stringFile = service.OpenFile(pictureBox, richTextBox1, readOneString,tsbDraw);
            //richTextBox1.Text = stringFile;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var service = new Helper();
            stringFile = service.OpenFile(pictureBox, richTextBox1, readOneString, tsbDraw);
            //richTextBox1.Text = stringFile;
        }

        private int _numberOfstring = 0;

        private void readOneString_Click(object sender, EventArgs e)
        {
            var service = new Helper();

            string oneString = service.GetOneString(stringFile, _numberOfstring);
            _Parser.Parse(oneString);
            richTextBox1.Text += oneString + '\n';
            _numberOfstring++;
            if (oneString == "Конец файла")
                readOneString.Enabled = false;
        }
    }
}
