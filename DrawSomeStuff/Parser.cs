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
        Graphics gr;
        private PictureBox pBox;
        private double prop;
       
        
        public Parser(Graphics graphics = null,PictureBox pictureBox = null, double proportion = 0.0)
        {
            ClearDictionaryAperture();
            gr = graphics;
            pBox = pictureBox;
            Aperture ap = new Aperture();
            ap.radEx = 10;
            ap.color = Color.Red;
            curAper = ap;
            prop = proportion;
        }

        Dictionary<string, Aperture> aperDict = new Dictionary<string, Aperture>();// словарь апертур
        Aperture curAper;
        struct Aperture
        {
            public int typeAp;
            public int numberSide;
            public float radEx;
            public float radIn;
            public Color color;
        }
        // переменные для  изменения  в void парсерах 
       
        private bool _isMm = false; // милиметры или дюймы
        private int x=0,y=0;
        

        #region DelegateList //список делегатов

        delegate void ParseGcommand(string command);
        void OF(string command) { } // команды типа OF /Offset / смещения /абсолютные 
        void FSLA(string command) { }
        void IP(string command) { } // Тип поляризации

        //формат команд официальной спецификации 
        void FS(string command) { }

        void MO(string command)
        {
            if (command.Contains(@"MOIN"))
                _isMm = false;
            if (command.Contains("MOMM"))
                _isMm = true;
        }

        void AD(string command)// задаем параметры апертур
        {
            string name = command.Substring(3, 3);
            Aperture ap = new Aperture();
            string type = command.Substring(6, 1);
            if (type == "C")//круг
            {
                ap.typeAp = 1;
                string radius = command.Substring(11, 3);
                ap.radEx = Convert.ToSingle(radius);
                ap.color = Color.Green;
            }
            if (type == "R")//квадрат
            {
                ap.typeAp = 2;
                string line = command.Substring(11, 3);
                ap.radEx = Convert.ToSingle(line);
                ap.color= Color.Red;
            }
            if (command.Substring(6, 2) == "OC")//многогранник после ОС число граней
            {
                ap.typeAp = 3;
                ap.numberSide = Convert.ToInt32(command.Substring(8, 1));
                string line = command.Substring(13, 3);
                ap.radEx = Convert.ToSingle(line);
                ap.color = Color.Yellow;
            }
            

            aperDict.Add(name, ap);
        }
        void AM(string command) { }
        void AB(string command) { }
        void Dnn(string command) { } //(nn≥10)


        void D0X(string command)
        {
            int posXinStr = command.IndexOf("X", StringComparison.Ordinal) + 1;
            int posYinStr = command.IndexOf("Y", StringComparison.Ordinal) + 1;
            int posDinStr = command.IndexOf("D", StringComparison.Ordinal);
            int Length = command.Length;

            // новые позиции для координат StrimTrim
            int newX = Convert.ToInt32(StrimTrim(command.Substring(posXinStr,
                posYinStr - posXinStr - 1))); //новыя позиция х
            int newY = Convert.ToInt32(StrimTrim(command.Substring(posYinStr,
                posDinStr - posYinStr ))); // новая позиция y
            string D = command.Substring(posDinStr, Length - posDinStr - 2); //тип положения пера

            //отрисовка
            switch (D)
            {
                case "D01": //перемещение с открытым затвором (линия)
                    gr.DrawLine(
                        new Pen(Color.Black, 5),
                        new Point(x+10, (pBox.Height-100)+10 - y),
                        new Point((int)(newX * prop) +10, (pBox.Height-100)+10 - (int)(newY * prop)));
                    break;
                case "D02": // с закрытым (телепортация)

                    break;
                case "D03": //отрисовка апертуры 
                    switch (curAper.typeAp)
                    {
                        case 1:
                            gr.FillEllipse(
                                new SolidBrush(curAper.color),
                                (int)(newX * prop), (pBox.Height-100) - (int)(newY * prop),
                                20,20);
                            break;
                        case 2:
                            gr.FillRectangle(
                                new SolidBrush(curAper.color),
                                (int)(newX * prop), (pBox.Height-100) - (int)(newY * prop),
                                20, 20);
                            break;
                        //case 3:
                        //    gr.DrawPolygon()
                        //    break;
                    }
                    break;
            }
            x = (int)(newX * prop);
            y = (int)(newY * prop);

        }

        void G01(string command) { }
        void G02(string command) { }
        void G03(string command) { }
        void G74(string command) { }

        void G75(string command)
        {
            //выбор квадранта для отображения (рисуем в 1 координатной плоскости или во всех 4 х)
        }
        void LP(string command) { }
        void LM(string command) { }
        void LR(string command) { }
        void LS(string command) { }
        void G36(string command) { }
        void G37(string command) { }
        void SR(string command) { }
        void G04(string command) { }
        void TF(string command) { }
        void TA(string command) { }
        void TO(string command) { }
        void TD(string command) { }
        void M02(string command) { }

        void D(string command)
        {
            command = command.Substring(0, 3);
            curAper = aperDict[command];
        }



        #endregion

        public void Parse(string command)// для текущего тз остальные будут добавлены позже или вообще никогда
        {
            var dictionaryCommands = new Dictionary<string, ParseGcommand>
            {
                {@"G75", G75},
                {@"MOIN", MO},
                {@"OF", OF},
                {@"FSLA", FSLA},
                {@"IPPOS", IP},
                {@"IPNEG", IP},
                {@"LPD", LP},
                {@"LPC", LP},
                {@"AD", AD},
                {@"AM", AM},
                {@"D0", D0X},
                {@"D", D}//по хорошему нужно проверять что строка начинается с D, а пока такой хардкод

            };
            var keyCommandString = dictionaryCommands.FirstOrDefault(it => command.Contains(it.Key)).Value;
            if (keyCommandString != null)
            {
                var parse = new ParseGcommand(keyCommandString);
                parse(command);
            }
            

        }
        public void ClearDictionaryAperture()
        {
            aperDict.Clear();
        }

        public string StrimTrim(string command)
        {
            while (command.StartsWith("0"))
                command = command.TrimStart('0');
            return command;
        }

        
    }
}
