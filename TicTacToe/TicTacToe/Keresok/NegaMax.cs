using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.AllapotTer;

namespace TicTacToe.Keresok
{
    class NegaMax
    {
        private int maxMelyseg = 2;
        public Operator Ajanl(Allapot allapot)
        {
            List<Operator> ajanlottOperator = new List<Operator>();

            for (int i = 0; i < Allapot.oszlop; i++)
            {
                for (int j = 0; j < Allapot.sor; j++)
                {
                    Operator aktaulisOperator = new Operator(new Point(i, j), allapot.Jatekos);
                    if (aktaulisOperator.Elofeltetel(allapot))
                    {
                        Allapot ujAllapot = aktaulisOperator.Lepes(allapot);
                        Bejaras(ujAllapot, aktaulisOperator, 0, 1);
                        ajanlottOperator.Add(aktaulisOperator);
                    }
                }
            }

            ajanlottOperator = ajanlottOperator.OrderByDescending(o => o.Suly).ToList();

            return ajanlottOperator[0];
        }

        private void Bejaras(Allapot aktualisAllapot, Operator eredetiOperator, int melyseg, int elojel)
        {
            if (aktualisAllapot.Celfeltetel() == "X" || aktualisAllapot.Celfeltetel() == "O")
            {
                eredetiOperator.Suly = elojel * aktualisAllapot.Heurisztika();
            }
            else
            {
                if (melyseg < maxMelyseg && aktualisAllapot.Celfeltetel() != "Döntetlen")
                {
                    int max = Int32.MinValue;
                    for (int i = 0; i < Allapot.oszlop; i++)
                    {
                        for (int j = 0; j < Allapot.sor; j++)
                        {
                            Operator aktualisOperator = new Operator(new Point(i, j), aktualisAllapot.Jatekos);
                            if (aktualisOperator.Elofeltetel(aktualisAllapot))
                            {
                                Allapot ujAllapot = aktualisOperator.Lepes(aktualisAllapot);
                                int aktualisSuly = elojel * ujAllapot.Heurisztika();
                                if (aktualisSuly > max)
                                {
                                    max = aktualisSuly;
                                }
                                Bejaras(ujAllapot, eredetiOperator, melyseg + 1, elojel * -1);
                            }
                        }
                    }
                    eredetiOperator.Suly += max;
                }
            }
        }
    }
}
