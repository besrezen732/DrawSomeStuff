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
        public Dictionary<string, string> dictionaryCommand = new Dictionary<string, string> {
                { "G", "" },//дуги
                { "MOIN", "" },//дюймы
                { "MOMM", "" },//милиметры
                //%OFA0B0*%
                { "FSLA", "" },//подавление ведущих нулей
                { "LPD", "" },//Слои
                { "LPC", "" },//Слои
                //%AMOC8*
                //5,1,8,0,0,1.08239X$1,22.5*
                //%
                { "ADD", "" },
                { "D", "" },
                {"M","" }
        };
       
            public void ParseOneStringDraw(string onestring,int paramX,int paramY, ref int x, ref int y, ref int d)
        {
            int posXinStr = onestring.IndexOf("X", StringComparison.Ordinal);
            int posYinStr = onestring.IndexOf("Y", StringComparison.Ordinal);
            int posDinStr = onestring.IndexOf("D", StringComparison.Ordinal);
            int Length = onestring.Length;

            // новые позиции для координат
            x = Convert.ToInt32(onestring.Substring(posXinStr, posYinStr- posXinStr));
            y = Convert.ToInt32(onestring.Substring(posYinStr, posDinStr - posYinStr));
            d = Convert.ToInt32(onestring.Substring(posDinStr, Length - posDinStr));
        }
    }
}
