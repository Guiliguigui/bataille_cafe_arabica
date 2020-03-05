using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicaLibrary
{
    public class Parcelle
    {
        private char nom;
        private List<Case> cases;
        private byte nbCase;
        private byte nbCaseJouees;

        public Parcelle(char nom, List<Case> cases, byte nbCase)
        {
            this.nom = nom;
            this.cases = cases;
            this.nbCase = nbCase;
            nbCaseJouees = 0;
        }

        public char Nom { get => nom; set => nom = value; }
        public List<Case> Cases { get => cases; set => cases = value; }
        public byte NbCase { get => nbCase; set => nbCase = value; }
        public byte NbCaseJouees { get => nbCaseJouees; set => nbCaseJouees = value; }

    }
}
