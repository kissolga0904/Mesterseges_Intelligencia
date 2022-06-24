using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.AllapotTer
{
    class Operator
    {
        private Point hova;

        public Point Hova
        {
            get { return hova; }
            set { hova = value; }
        }

        private string mit;

        public string Mit
        {
            get { return mit; }
            set { mit = value; }
        }

        private int suly;

        public int Suly
        {
            get { return suly; }
            set { suly = value; }
        }
        

        public Operator(Point hova, string mit)
        {
            this.Hova = hova;
            this.Mit = mit;
        }

        public bool Elofeltetel(Allapot aktualisAllapot)
        {
            return aktualisAllapot.Palya[Hova.X, Hova.Y] == null;
        }

        public Allapot Lepes(Allapot aktualisAllapot)
        {
            Allapot ujAllapot = new Allapot(); //létrehoz egy állapotot
            ujAllapot.Palya = (string[,])aktualisAllapot.Palya.Clone(); //az állapotot leklónozzuk az előző alapján

            //és módosítjuk az aktuális lépés szerint
            ujAllapot.Palya[Hova.X, Hova.Y] = Mit;
            if (Mit == "X")
            {
                ujAllapot.Jatekos = "O";
            }
            else
            {
                ujAllapot.Jatekos = "X";
            }
            return ujAllapot;
        }
    }
}
