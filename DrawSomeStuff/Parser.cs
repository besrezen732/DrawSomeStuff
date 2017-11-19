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
            //выборка взята ради примера из LK3, по хорошему можно на базу данных переписать
                { "Наименование", "name" },
                { "ТорговаяМарка", "mark" },
                { "Страна", "counry" },
                { "ДатаПодписания", "date" },
                { "ТорговаяМаркаЛат", "mark_lat" } };
       
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

        public void GetCommandForParse(string command)
        {
           //var resCommand  = dictionaryCommand.Where(itt=>(command.Contains(itt.Key))).FirstOrDefault().Value;
           // switch (resCommand)
           //     case :// тут выборка по командам и запускам парсеров для обработки 

            
        }
    }
}
