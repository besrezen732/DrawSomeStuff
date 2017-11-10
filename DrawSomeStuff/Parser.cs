using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DrawSomeStuff
{
    class Parser
    {
        private Graphics _graphics;
        private readonly TextBox _tBox;

        public Parser(TextBox tBox, Graphics graphics)
        {
            _tBox = tBox;
            _graphics = graphics;
        }

        public void Parse(string stringFile)
        {
            Print(stringFile);
        }

        public void Print(string str)
        {
            _tBox.Text += Environment.NewLine + @">>" + str;
        }
    }
}
