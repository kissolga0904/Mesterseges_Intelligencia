using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToe.AllapotTer;
using TicTacToe.Keresok;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        Mezo[,] palya = new Mezo[Allapot.oszlop, Allapot.sor];
        Allapot allapot;
        public Form1()
        {
            allapot = new Allapot();
            InitializeComponent();
            for (int i = 0; i < Allapot.sor; i++)
            {
                for (int j = 0; j < Allapot.oszlop; j++)
                {
                    Mezo mezo = new Mezo(i, j);
                    mezo.Location = new Point(i * 100, j * 100);
                    mezo.Click += new EventHandler(mezoClick);
                    palya[i, j] = mezo;
                    //hozzáadjuk a formhoz a gombokat
                    Controls.Add(mezo);
                }
            }
        }
        private void mezoClick(object sender, EventArgs e)
        {
            Mezo mezo = (Mezo)sender;
            Point pont = mezo.Pont;

            Operator op = new Operator(pont, allapot.Jatekos);
            if (op.Elofeltetel(allapot))
            {
                Kirajzol(mezo);
                allapot = op.Lepes(allapot);
                CelfeltetelVizsgalat();

                //ProbaHibaKereses probaHibaKereses = new ProbaHibaKereses();
                //Operator opGep = probaHibaKereses.Ajanlas(allapot);

                NegaMax negaMax = new NegaMax();
                Operator opGep = negaMax.Ajanl(allapot);

                Mezo mezoGep = palya[opGep.Hova.X, opGep.Hova.Y];
                Kirajzol(mezoGep);
                allapot = opGep.Lepes(allapot);
                CelfeltetelVizsgalat();
            }
        }

        private void Kirajzol(Mezo mezo)
        {
            mezo.Text = allapot.Jatekos;
            if (allapot.Jatekos == "X")
            {
                mezo.ForeColor = Color.Red;
            }
            else
            {
                mezo.ForeColor = Color.Blue;
            }
        }

        private void CelfeltetelVizsgalat()
        {
            if (allapot.Celfeltetel() != null)
            {
                if (allapot.Celfeltetel() == "Döntetlen")
                {
                    MessageBox.Show(allapot.Celfeltetel());
                }
                else
                {
                    MessageBox.Show("Gratulálok! Nyert: " + allapot.Celfeltetel());
                }
                this.Close();
                Application.Exit();
            }
        }
    }
}
