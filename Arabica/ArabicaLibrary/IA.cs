using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicaLibrary
{
    /* Classe IA
     * Cette classe permet d'instancier les deux IA mais aussi de faire jouer l'IA du client
     * Il est aussi possible de faire jouer deux IA client l'une contre l'autre
     */
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

        /* GetCasesJouables
         * Permet de savoir quelle case l'IA cliente à le droit de planter selon les règles du jeu
         * Input  : la dernière case jouée par l'ia adverse et l'instance de la carte
         * Output : la liste des cases jouables par le client
         */
        private List<Case> GetCasesJouables(Case derniereCase, Carte carte)
        {
            List<Case> casesJouables = new List<Case>();

            int x = derniereCase.X;
            int y = derniereCase.Y;

            /* Pour chaque case dans la même ligne ou colonne on vérifie que la case :
             * -n'est pas la dernière case jouée
             * -n'est pas mer ou foret (null)
             * -ne fait pas partie de la parcelle dont la dernière case est issue
             * -n'a pas de propriétaire (n'est pas plantée)
             */
            for (int indexX = 0; indexX < 10; indexX++) 
            {
                Case caseVisee = carte.CarteObjet[indexX, y];
                if (indexX != x && caseVisee != null && caseVisee.Parcelle != derniereCase.Parcelle && caseVisee.Proprietaire == null)
                    casesJouables.Add(caseVisee);
            }
            for (int indexY = 0; indexY < 10; indexY++)
            {
                Case caseVisee = carte.CarteObjet[x, indexY];
                if (indexY != y && caseVisee != null && caseVisee.Parcelle != derniereCase.Parcelle && caseVisee.Proprietaire == null)
                    casesJouables.Add(caseVisee);
            }

            return casesJouables;
        }

        /* ChoisirOuJouer
         * Permet à l'IA de choisir quelle case jouer
         * Input  : la dernière case jouée par l'ia adverse et l'instance de la carte
         * Output : tableau de 2 entiers contenant le jeu du client
         */
        public int[] ChoisirOuJouer(Case derniereCase, Carte carte)
        {
            Random rand = new Random();
            int x, y;
            if (derniereCase == null)//si il n'y a pas de dernière case le jeu viens de commencer, on joue aléatoirement
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
                Dictionary<Case, int> priorites = new Dictionary<Case, int>();

                foreach (Case @case in casesJouables)
                {
                    int priorite = 0;//valeur indiquant l'importantce de jouer une case plutot qu'un autre

                    //on cerche des cases voisines plantées par le client pour le bonus
                    List<Case> casesVoisines = new List<Case>();
                    casesVoisines.Add(carte.CarteObjet[@case.X + 1, @case.Y]);
                    casesVoisines.Add(carte.CarteObjet[@case.X - 1, @case.Y]);
                    casesVoisines.Add(carte.CarteObjet[@case.X, @case.Y + 1]);
                    casesVoisines.Add(carte.CarteObjet[@case.X, @case.Y - 1]);
                    foreach (Case voisine in casesVoisines)  if(voisine != null) if(voisine.Proprietaire == this) priorite += 1;

                    Parcelle parcelle = @case.Parcelle;
                    int casesPosedees = 0;
                    if (parcelle.CasesPlanteesIA.ContainsKey(this))
                        casesPosedees = parcelle.CasesPlanteesIA[this];
                    int casesServeur = parcelle.NbCaseJouees - casesPosedees;

                    if (casesPosedees <= parcelle.NbCase / 2)
                    {
                        if (casesServeur > parcelle.NbCase / 2)//points parcelle bloqués par le serveur
                            priorite -= 10;
                        else if (casesServeur == parcelle.NbCase / 2 && casesPosedees == parcelle.NbCase / 2 - 1)//bloquer la parcelle et empêcher le gain de points
                            priorite += 4;
                        else
                            priorite += 2;
                    }
                    else//points parcelle bloqués par le client
                        priorite -= 4;

                    priorites.Add(@case, priorite);
                }
                
                Case caseSelectionnee = priorites.OrderByDescending(@case => @case.Value).FirstOrDefault().Key;
                x = caseSelectionnee.X;
                y = caseSelectionnee.Y;

                //Jeu Aléatoire
                //int var = rand.Next(0, casesJouables.Count);
                //x = casesJouables[var].X;
                //y = casesJouables[var].Y;
            }
            return new int[2] { x, y };
        }
    }
}
