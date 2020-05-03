using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicaLibrary
{
    /* Classe Parcelle
     * Cette classe sert à créer une parcelle dynamique
     */
    public class Parcelle
    {
        private char nom;
        private List<Case> cases = new List<Case>();
        private byte nbCase;
        private byte nbCaseJouees;
        private Dictionary<IA, int> casesPlanteesIA = new Dictionary<IA, int>();//nombre de cases plantées par IA

        public Parcelle(char nom)
        {
            this.nom = nom;
            this.nbCase = 0;
            this.nbCaseJouees = 0;
        }

        /* AjouterCase
         * Permet d'ajouter une case à la parcelle
         * Input  : la case à ajouter
         * Output : void
         */
        public void AjouterCase(Case @case)
        {
            cases.Add(@case);
            nbCase++;
        }

        /* NouvelleCasePlantee
         * Permet d'actualiser l'instance lorsque l'une des cases a été plantée
         * Input  : l'IA qui a planté la case
         * Output : void
         */
        public void NouvelleCasePlantee(IA planteur)
        {
            nbCaseJouees += 1;
            if (casesPlanteesIA.ContainsKey(planteur))
                casesPlanteesIA[planteur] += 1;
            else
                casesPlanteesIA.Add(planteur, 1);
        }

        public char Nom { get => nom; }
        public List<Case> Cases { get => cases;}
        public byte NbCase { get => nbCase; }
        public byte NbCaseJouees { get => nbCaseJouees; }
        public Dictionary<IA, int> CasesPlanteesIA { get => casesPlanteesIA; }
    }
}
