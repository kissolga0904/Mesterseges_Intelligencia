using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe.AllapotTer
{
    //Az állapottér leírja a játék aktuális állapotát
    class Allapot
    {
        public static int sor = 3;
        public static int oszlop = 3;

        private string[,] palya;

        public string[,] Palya
        {
            get { return palya; }
            set { palya = value; }
        }


        private string jatekos;

        public string Jatekos
        {
            get { return jatekos; }
            set { jatekos = value; }
        }

        public Allapot()
        {
            this.Palya = new string[sor, oszlop];
            this.Jatekos = "X";
        }

        public string Celfeltetel()
        {
            //4 célállapot: X (X nyert), O(O nyert), Döntetlen, null: tart még a játék

            //három egymás mellett
            //forral leellenőrizzük hogy bármelyik sorban van-e 3 egymás melleti egyforma jel
            for (int i = 0; i < sor; i++)
            {
                if (palya[i, 0] != null && palya[i, 0] == palya[i, 1] && palya[i, 1] == palya[i, 2])
                {
                    return palya[i, 0];
                }
            }

            //három egymás alatt
            for (int i = 0; i < oszlop; i++)
            {
                if (palya[0, i] != null && palya[0, i] == palya[1, i] && palya[1, i] == palya[2, i])
                {
                    return palya[0, i];
                }
            }

            //főátlót
            if (palya[0, 0] != null && palya[0, 0] == palya[1, 1] && palya[1, 1] == palya[2, 2])
            {
                return palya[0, 0];
            }

            //mellékátló
            if (palya[0, 2] != null && palya[0, 2] == palya[1, 1] && palya[1, 1] == palya[2, 0])
            {
                return palya[0, 2];
            }

            //még nincs vége
            for (int i = 0; i < sor; i++)
            {
                for (int j = 0; j < oszlop; j++)
                {
                    if (palya[i, j] == null)
                    {
                        return null;
                    }
                }
            }

            //döntetlen
            return "Döntetlen";
        }

        public int Heurisztika()
        {
            int suly = 0;
            if (Celfeltetel() == "O")
            {
                return 100;
            }

            if (Celfeltetel() == "X")
            {
                return 80;
            }

            for (int i = 0; i < Allapot.oszlop; i++)
            {
                for (int j = 0; j < Allapot.sor; j++)
                {
                    //ha bármelyik mező a játékosé és a mellette fölötte
                    if (palya[i, j] == Jatekos)
                    {
                        //Elsőnek ellenőrizzük hogy kimentünk-e a pályáról lent ÉS hogyha az alatta lévő mező is a játékosé,
                        //akkor a súlyt növelem 5-el
                        if (i + 1 < 3 && palya[i + 1, j] == Jatekos)
                        {
                            suly += 5;
                        }
                        //Elsőnek ellenőrizzük hogy kimentünk-e a pályáról jobb oldalon ÉS hogyha a jobboldali mező is a 
                        //játékosé(én), akkor a súlyt növeli 5-el
                        if (j + 1 < 3 && palya[i, j + 1] == Jatekos)
                        {
                            suly += 5;
                        }
                        //Elsőnek ellenőrizzük hogy kimentünk-e a pályáról lent majd ellenőrzöm hogy kimentünk-e a pályáról
                        //jobb oldalon ÉS hogyha alatta és a jobb oldalán lévő mező a játékosé, akkor a súlyt növelem 5-el
                        //főátló ellenőrzés jobbról lefele
                        if (i + 1 < 3 && j + 1 < 3 && palya[i + 1, j + 1] == Jatekos)
                        {
                            suly += 5;
                        }
                        //Elsőnek ellenőrizzük hogy kimentünk-e a pályáról fent majd ellenőrzöm hogy kimentünk-e a pályáról
                        //bal oldalon ÉS hogyha felette és a bal oldalán lévő mező a játékosé, akkor a súlyt növelem 5-el
                        //főátló ellenőrzés balról felfele
                        if (i - 1 >= 0 && j - 1 >= 0 && palya[i - 1, j - 1] == Jatekos)
                        {
                            suly += 5;
                        }
                        //mellékátló ellenőrzés balról lefele
                        if (j - 1 >= 0 && i + 1 < 3 && palya[i + 1, j - 1] == Jatekos)
                        {
                            suly += 5;
                        }
                        //mellékátló ellenőrzés jobbról felfele
                        if (j + 1 < 3 && i - 1 >= 0 && palya[i - 1, j + 1] == Jatekos)
                        {
                            suly += 5;
                        }
                    }
                }
            }
            return suly;   
        }
    }
}
