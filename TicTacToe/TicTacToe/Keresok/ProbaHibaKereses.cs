using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.AllapotTer;

namespace TicTacToe.Keresok
{
    class ProbaHibaKereses
    {
        //Addig próbál lépni egy mezőre, amíg az a lépés nem érvényes, ha nem érvényes, újrapróbálja
        public Operator Ajanlas(Allapot allapot)
        {
            while (true)
            {
                Random random = new Random();
                Operator op = new Operator((new Point(random.Next(Allapot.sor), random.Next(Allapot.oszlop))), "O");
                if (op.Elofeltetel(allapot))
                {
                    return op;
                }
            }
        }
    }
}
