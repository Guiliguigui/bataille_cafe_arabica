using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicaLibrary
{
    public class IA
    {
        private readonly string nom;
        private Case derniereCaseJouee;

        public IA(string nom)
        {
            this.nom = nom;
        }

        public string Nom { get => nom;}
        public Case DerniereCaseJouee { get => derniereCaseJouee; set => derniereCaseJouee = value; }

        private List<Case> GetCasesJouables(Case derniereCase, Carte carte)
        {
            List<Case> casesJouables = new List<Case>();

            int x = derniereCase.X;
            int y = derniereCase.Y;

            for (int indexX = 0; indexX < 10; indexX++) 
            {
                Case caseVisee = carte.CarteObjet[indexX, y];
                if (indexX != x && caseVisee != null && caseVisee.Parcelle != derniereCase.Parcelle && caseVisee.Proprietaire == null) casesJouables.Add(caseVisee);
            }
            for (int indexY = 0; indexY < 10; indexY++)
            {
                Case caseVisee = carte.CarteObjet[x, indexY];
                if (indexY != y && caseVisee != null && caseVisee.Parcelle != derniereCase.Parcelle && caseVisee.Proprietaire == null) casesJouables.Add(caseVisee);
            }

            return casesJouables;
        }

        public int[] ChoisirOuJouer(Case derniereCase, Carte carte)
        {
            Random rand = new Random();
            int x, y;
            if (derniereCase == null)
            {
                Case premiereCase;
                do
                {
                    x = rand.Next(1, 9);
                    y = rand.Next(1, 9);
                    premiereCase = carte.CarteObjet[x, y];

                } while (premiereCase == null);
            }
            else
            {
                List<Case> casesJouables = new List<Case>();
                casesJouables = GetCasesJouables(derniereCase, carte);
                int var = rand.Next(0, casesJouables.Count);
                x = casesJouables[var].X;
                y = casesJouables[var].Y;
            }
            return new int[2] { x, y };
        }
    }
}
