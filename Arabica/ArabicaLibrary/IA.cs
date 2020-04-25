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
        public Case DerniereCaseJouee { get => derniereCaseJouee;}

        public List<Case> GetCasesJouables(Case derniereCase, Carte carteObjet)
        {

            return null;
        }

        public string Jouer(List<Case> casesJouables, ref Carte carteObjet)
        {

            return "A:00";
        }
    }
}
