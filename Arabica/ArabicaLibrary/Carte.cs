using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabicaLibrary
{
    public class Carte
    {
        private char[,] carte = new char[10, 10];
        private Case[,] carteObjet = new Case[10, 10];
        private List<Parcelle> parcelles= new List<Parcelle>();

        public Carte(string trame)
        {
            int[,] tabValeursCarte = new int[10, 10];
            tabValeursCarte = Lecture(trame);
            Decodage(tabValeursCarte);
            //initCarteObjet();
            
        }

        
        //private void initCarteObjet()
        //{
        //    char lastParcelle = 'a';
        //    for (byte index1 = 0; index1 < 10; index1++)
        //    {
        //        for (byte index2 = 0; index2 < 10; index2++)
        //        {
        //            this.carteObjet[index1, index2] = new Case(index1, index2, carte[index1, index2]);
        //            if (carte[index1, index2] > lastParcelle)
        //                lastParcelle = carte[index1, index2];
        //        }
        //    }

        //    int nbParcelles = lastParcelle - 'a';
        //    for(byte index=0; index<nbParcelles; index++)
        //    {
        //        parcelles.Add(new Parcelle((char)((int)'a' + index)));
        //    }

        //    for (byte index1 = 0; index1 < 10; index1++)
        //    {
        //        for (byte index2 = 0; index2 < 10; index2++)
        //        {
        //            parcelles[]
        //        }
        //    }
        //}



        //Les méthodes initialement contenues dans Decodage.cs ont été déplacées et réadaptées pour simplifier le code

        /* FP1  Lecture
         * Permet de stocker les valeurs de la trame dans un tableau d'entier de 10 par 10
         * Input  : Trame de la carte obtenu dans la partie serveur
         * Output : Tableau des valeurs pour chaque case de la trame 
         */
        private int[,] Lecture(string trame)
        {
            int[,] tabValeursCarte = new int[10, 10];
            string[] tabLignes = trame.Split('|'); // '|' sépare les lignes dans la trame 
            for (int index1 = 0; index1 < 10; index1++)
            {
                string[] ligneSplit = tabLignes[index1].Split(':'); // ':' sépare les valeurs dans chaque ligne 
                for (int index2 = 0; index2 < 10; index2++)
                    int.TryParse(ligneSplit[index2], out tabValeursCarte[index1, index2]);
            }
            return tabValeursCarte;
        }

        /* FS2.1 Spread
         * Permet d'étendre une parcelle jusqu'à ses bordures
         * Input  : Par référence : La carte décodée
         *          Par valeur : -les deux index de la position de la case (qui n'est pas mer ou foret) 
         *                       -le tableau des valeurs pour chaque case de la trame 
         * Output : Void
         */
        private void Spread(int index1, int index2, int[,] tabValeursCarte, Parcelle parcelleObjet)
        {
            List<int[]> toDo = new List<int[]> { new int[] { index1, index2 } }; // Liste contenant les cases à étendre
            while (toDo.Count > 0)
            {
                List<int[]> toDoTemp = toDo.ToList(); // Liste temporaire contenant les futures cases à étendre 
                foreach (int[] tmpCase in toDo)
                {
                    bool bordureNord = false, bordureOuest = false, bordureEst = false, bordureSud = false;
                    int valeurCase = tabValeursCarte[tmpCase[0], tmpCase[1]];
                    char nomCase = carte[tmpCase[0], tmpCase[1]];
                    Case caseObjet = new Case(tmpCase[0], tmpCase[1], nomCase);
                    this.carteObjet[tmpCase[0], tmpCase[1]] = caseObjet;
                    parcelleObjet.ajouterCase(caseObjet);

                    // Vérification de l'emplacement des bordures de la carte 
                    if (valeurCase / 8 == 1)
                    {
                        valeurCase -= 8; bordureEst = true;
                    }
                    if (valeurCase / 4 == 1)
                    {
                        valeurCase -= 4; bordureSud = true;
                    }
                    if (valeurCase / 2 == 1)
                    {
                        valeurCase -= 2; bordureOuest = true;
                    }
                    if (valeurCase / 1 == 1)
                    {
                        bordureNord = true;
                    }

                    // Nommage des cases où bordure inexistante et ajout dans la liste toDo 
                    if (!bordureNord)
                    {
                        if (carte[tmpCase[0] - 1, tmpCase[1]] == '\0')
                        {
                            carte[tmpCase[0] - 1, tmpCase[1]] = nomCase;
                            toDoTemp.Add(new int[] { tmpCase[0] - 1, tmpCase[1] });
                        }
                    }
                    if (!bordureOuest)
                    {
                        if (carte[tmpCase[0], tmpCase[1] - 1] == '\0')
                        {
                            carte[tmpCase[0], tmpCase[1] - 1] = nomCase;
                            toDoTemp.Add(new int[] { tmpCase[0], tmpCase[1] - 1 });
                        }
                    }
                    if (!bordureEst)
                    {
                        if (carte[tmpCase[0], tmpCase[1] + 1] == '\0')
                        {
                            carte[tmpCase[0], tmpCase[1] + 1] = nomCase;
                            toDoTemp.Add(new int[] { tmpCase[0], tmpCase[1] + 1 });
                        }
                    }
                    if (!bordureSud)
                    {
                        if (carte[tmpCase[0] + 1, tmpCase[1]] == '\0')
                        {
                            carte[tmpCase[0] + 1, tmpCase[1]] = nomCase;
                            toDoTemp.Add(new int[] { tmpCase[0] + 1, tmpCase[1] });
                        }
                    }
                    toDoTemp.RemoveAt(0);
                    // Enlevement de la case qui a été étendue
                }
                toDo = toDoTemp.ToList();
                // Update de la liste de cases à étendre
            }
        }

        /* FP2 Decodage 
         * Permet de décoder les valeurs contenues dans le tableau pour créer le tableau final avec les parcelles nommées
         * Input  : Tableau des appartenances parcelle pour chaque cases de la trame
         * Output : Tableau prêt à être afficher avec les parcelles nommées
         */
        private void Decodage(int[,] tabValeursCarte)
        {
            carte = new char[10, 10];
            char derniereParcelle = 'a'; // Premier terrain trouvé est 'a' 
            bool isPremiereParcelle = true;

            for (int index1 = 0; index1 < 10; index1++)
            {
                for (int index2 = 0; index2 < 10; index2++)
                {
                    int valeurCase = tabValeursCarte[index1, index2];

                    if (valeurCase / 64 == 1) // Ajout des Mers
                    {
                        carte[index1, index2] = 'M';
                    }
                    else if (valeurCase / 32 == 1) // Ajout des Forêts
                    {
                        carte[index1, index2] = 'F';
                    }
                    else if (isPremiereParcelle) // Traitement de la première parcelle
                    {
                        carte[index1, index2] = 'a';
                        Parcelle parcelleObjet = new Parcelle('a');
                        parcelles.Add(parcelleObjet);
                        Case caseObjet = new Case(index1, index2, carte[index1, index2]);
                        this.carteObjet[index1, index2] = caseObjet;
                        parcelleObjet.ajouterCase(caseObjet);
                        Spread(index1, index2, tabValeursCarte, parcelleObjet);
                        isPremiereParcelle = false;
                    }
                    else if (carte[index1, index2] == '\0') // Traitement des autres parcelles
                    {
                        carte[index1, index2] = derniereParcelle = (char)((int)derniereParcelle + 1); // Pour l'incrémentation des noms des parcelles utilisation du code ASCII
                        Parcelle parcelleObjet = new Parcelle(derniereParcelle);
                        parcelles.Add(parcelleObjet);
                        Case caseObjet = new Case(index1, index2, carte[index1, index2]);
                        this.carteObjet[index1, index2] = caseObjet;
                        parcelleObjet.ajouterCase(caseObjet);
                        Spread(index1, index2, tabValeursCarte,parcelleObjet);
                    }
                }
            }
        }

        /* FS3 Afficher
         * Permet d'afficher la carte à l'aide des caractères contenus dans le tableau de la carte décodée
         * Input  : Tableau avec les parcelles nommées
         * Output : void
         */
        public void Afficher()
        {
            for (byte index1 = 0; index1 < 10; index1++)
            {
                for (byte index2 = 0; index2 < 10; index2++)
                {
                    Console.Write(this.carte[index1, index2] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
