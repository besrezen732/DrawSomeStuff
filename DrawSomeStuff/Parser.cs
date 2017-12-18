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
        public Parser()
        {
            Aperture ap = new Aperture();
            ap.radEx = 10;
            ap.color = Color.Red;
            curAper = ap;
        }

        Dictionary<string, Aperture> aperDict = new Dictionary<string, Aperture>();// словарь апертур
        Aperture curAper;
        struct Aperture
        {
            public int type;
            public int numberSide;
            public float radEx;
            public float radIn;
            public Color color;
        }
        // переменные для  изменения  в void парсерах 
       
        private bool _isMm = false; // милиметры или дюймы

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
                ap.type = 1;
                string radius = command.Substring(11, 3);
                ap.radEx = Convert.ToSingle(radius);
            }
            if (type == "R")//квадрат
            {
                ap.type = 2;
                string line = command.Substring(11, 3);
                ap.radEx = Convert.ToSingle(line);
            }
            if (command.Substring(6, 2) == "OC")//многогранник после ОС число граней
            {
                ap.type = 3;
                ap.numberSide = Convert.ToInt32(command.Substring(8, 1));
                string line = command.Substring(13, 3);
                ap.radEx = Convert.ToSingle(line);
            }
            ap.color = Color.Black;

            aperDict.Add(name, ap);
        }
        void AM(string command) { }
        void AB(string command) { }
        void Dnn(string command) { } //(nn≥10)

        void D01(string command)
        {
            
        }

        void D02(string command)
        {
            
        }

        void D03(string command)
        {
            
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
                {@"D01", D01},
                {@"D02", D02},
                {@"D03", D03},
                {@"D", D}//по хорошему нужно проверять что строка начинается с D, а пока такой хардкод

            };
            var keyCommandString = dictionaryCommands.FirstOrDefault(it => command.Contains(it.Key)).Value;
            if (keyCommandString != null)
            {
                var parse = new ParseGcommand(keyCommandString);
                parse(command);
            }

        }

        public void ParseOneStringDraw(string onestring, int paramX, int paramY, ref int x, ref int y, ref int d)
        {
            int posXinStr = onestring.IndexOf("X", StringComparison.Ordinal);
            int posYinStr = onestring.IndexOf("Y", StringComparison.Ordinal);
            int posDinStr = onestring.IndexOf("D", StringComparison.Ordinal);
            int Length = onestring.Length;

            // новые позиции для координат
            x = Convert.ToInt32(onestring.Substring(posXinStr, posYinStr - posXinStr));
            y = Convert.ToInt32(onestring.Substring(posYinStr, posDinStr - posYinStr));
            d = Convert.ToInt32(onestring.Substring(posDinStr, Length - posDinStr));
        }

        public void ClearDictionaryAperture()
        {
            aperDict.Clear();
        }
    }
}
