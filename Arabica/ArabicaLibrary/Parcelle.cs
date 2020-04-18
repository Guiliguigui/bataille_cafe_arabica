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
        private List<Case> cases = new List<Case>();
        private byte nbCase;
        private byte nbCaseJouees;
        private Dictionary<IA, int> casesPlanteesIA = new Dictionary<IA, int>();

        public Parcelle(char nom)
        {
            this.nom = nom;
            this.nbCase = 0;
            this.nbCaseJouees = 0;
        }

        public void ajouterCase(Case p_case)
        {
            cases.Add(p_case);
            nbCase++;
        }

        public void nouvelleCasePlantee(IA planteur)
        {
            nbCaseJouees += 1;
            if (casesPlanteesIA.ContainsKey(planteur))
            {
                casesPlanteesIA[planteur] += 1;
            }
            else
            {
                casesPlanteesIA.Add(planteur, 1);
            }
        }

        public char Nom { get => nom; }
        public List<Case> Cases { get => cases;}
        public byte NbCase { get => nbCase; }
        public byte NbCaseJouees { get => nbCaseJouees; }

    }
}
