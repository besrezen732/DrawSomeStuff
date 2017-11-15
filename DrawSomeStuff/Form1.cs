using System;
using System.Drawing;
using System.Windows.Forms;

namespace DrawSomeStuff
{
    public partial class laptop : Form
    {
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
        }

        void initGraphics()
        {
            gr = pictureBox.CreateGraphics();
            pen = new Pen(Color.Black, 3);
            brush = new SolidBrush(Color.Black);
        }

        //void drawGraphics()
        //{
        //    pen.Color = Color.Brown;
        //    gr.DrawLine(pen, new Point(450,400), new Point(450,200));
        //    pen.Color = Color.Red;
        //    gr.DrawRectangle(pen, 50, 100, 120, 200);

        //    pen.Color = Color.Blue;
        //    gr.DrawRectangle(pen, 65, 110, 30, 50);

        //    pen.Color = Color.Blue;
        //    gr.DrawRectangle(pen, 65, 180, 30, 50);

        //    pen.Color = Color.Blue;
        //    gr.DrawRectangle(pen, 125, 180, 30, 50);

        //    pen.Color = Color.Blue;
        //    gr.DrawRectangle(pen, 125, 110, 30, 50);

        //    pen.Color = Color.Brown;
        //    gr.DrawRectangle(pen, 100, 250, 30, 50);

        //    pen.Width = 7;
        //    pen.Color = Color.Green;
        //    gr.DrawEllipse(pen, 430, 125, 40, 80);
        //    brush.Color = Color.Orange;
        //    gr.FillPie(brush, 10, 40, 40, 40, 0, 120);

        //}

        private void tsbDraw_Click(object sender, EventArgs e)
        {
            //drawGraphics();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var service = new Helper();
            stringFile = service.OpenFile(pictureBox, richTextBox1);
            //richTextBox1.Text = stringFile;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var service = new Helper();
            stringFile = service.OpenFile(pictureBox, richTextBox1);
            //richTextBox1.Text = stringFile;
        }

        private int _numberOfstring = 0;

        private void readOneString_Click(object sender, EventArgs e)
        {
            var service = new Helper();
            string oneString = service.GetOneString(stringFile, _numberOfstring);
            richTextBox1.Text += oneString + '\n';
            _numberOfstring++;
            if (oneString == "Конец файла")
                readOneString.Enabled = false;
        }
    }
}
