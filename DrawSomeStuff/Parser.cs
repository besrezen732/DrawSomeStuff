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
        //список делегатов
        delegate void ParseGcommand(string command);

        void OF(string command){} // команды типа OF /Offset / смещения /абсолютные 
        void FSLA(string command){}
        void IP(string command){} // Тип поляризации

        //формат команд официальной спецификации 
        void FS(string command){}
        void MO(string command){}
        void AD(string command){}
        void AM(string command){}
        void AB(string command){}
        void Dnn(string command){} //(nn≥10)
        void D01(string command){}
        void D02(string command){}
        void D03(string command){}
        void G01(string command){}
        void G02(string command){}
        void G03(string command){}
        void G74(string command){}

        void G75(string command)
        {
            int i = 10;
        }
        void LP(string command){}
        void LM(string command){}
        void LR(string command){}
        void LS(string command){}
        void G36(string command){}
        void G37(string command){}
        void SR(string command){}
        void G04(string command){}
        void TF(string command){}
        void TA(string command){}
        void TO(string command){}
        void TD(string command){}
        void M02(string command){}

        public void Parse(string command)// для текущего тз остальные будут добавлены позже или вообще никогда
        {
            var dictionaryCommands = new Dictionary<string, ParseGcommand>
            {
                {"G75", G75},
                {"MOIN", MO},
                {"OF", OF},
                {"FSLA", FSLA},
                {"IPPOS", IP},
                {"IPNEG", IP},
                {"LPD", LP},
                {"LPC", LP},
                {"AD", AD},
                {"AM", AM}
            };
            ParseGcommand parseGcommand = dictionaryCommands.FirstOrDefault(it=> command.Contains(it.Key)).Value;
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
    }
}
