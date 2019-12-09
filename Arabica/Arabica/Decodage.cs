using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arabica
{
    /* Classe PartieDecodage
     * Elle regroupe toutes les fonctions permetant de lire, de décoder et d'afficher la carte
     */
    public class PartieDecodage
    {

        /* FP1  Lecture
         * Permet de stocker les valeurs de la trame dans un tableau d'entier de 10 par 10
         * Input  : Trame de la carte obtenu dans la partie serveur
         * Output : Tableau des valeurs pour chaque case de la trame 
         */
        public static int[,] Lecture(string trame)
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
        public static void Spread(ref char[,] carteDecode, int index1, int index2, int[,] tabValeursCarte)
        {
            List<int[]> toDo = new List<int[]> { new int[] { index1, index2 } }; // Liste contenant les cases à étendre
            while (toDo.Count > 0)
            {
                List<int[]> toDoTemp = toDo.ToList(); // Liste temporaire contenant les futures cases à étendre 
                foreach (int[] parcelle in toDo)
                {
                    bool bordureNord = false, bordureOuest = false, bordureEst = false, bordureSud = false;
                    int valeurCase = tabValeursCarte[parcelle[0], parcelle[1]];
                    
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
                        if (carteDecode[parcelle[0] - 1, parcelle[1]] == '\0')
                        {
                            carteDecode[parcelle[0] - 1, parcelle[1]] = carteDecode[parcelle[0], parcelle[1]];
                            toDoTemp.Add(new int[] { parcelle[0] - 1, parcelle[1] });
                        }
                    }
                    if (!bordureOuest)
                    {
                        if (carteDecode[parcelle[0], parcelle[1] - 1] == '\0')
                        {
                            carteDecode[parcelle[0], parcelle[1] - 1] = carteDecode[parcelle[0], parcelle[1]];
                            toDoTemp.Add(new int[] { parcelle[0], parcelle[1] - 1 });
                        }
                    }
                    if (!bordureEst)
                    {
                        if (carteDecode[parcelle[0], parcelle[1] + 1] == '\0')
                        {
                            carteDecode[parcelle[0], parcelle[1] + 1] = carteDecode[parcelle[0], parcelle[1]];
                            toDoTemp.Add(new int[] { parcelle[0], parcelle[1] + 1 });
                        }
                    }
                    if (!bordureSud)
                    {
                        if (carteDecode[parcelle[0] + 1, parcelle[1]] == '\0')
                        {
                            carteDecode[parcelle[0] + 1, parcelle[1]] = carteDecode[parcelle[0], parcelle[1]];
                            toDoTemp.Add(new int[] { parcelle[0] + 1, parcelle[1] });
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
        public static char[,] Decodage(int[,] tabValeursCarte)
        {
            char[,] carteDecode = new char[10, 10];
            char derniereParcelle = 'a'; // Premier terrain trouvé est 'a' 
            bool isPremiereParcelle = true;

            for (int index1 = 0; index1 < 10; index1++)
            {
                for (int index2 = 0; index2 < 10; index2++)
                {
                    int valeurCase = tabValeursCarte[index1, index2];

                    if (valeurCase / 64 == 1) // Ajout des Mers
                    {
                        carteDecode[index1, index2] = 'M';
                    }
                    else if (valeurCase / 32 == 1) // Ajout des Forêts
                    {
                        carteDecode[index1, index2] = 'F';
                    }
                    else if (isPremiereParcelle) // Traitement de la première parcelle
                    {
                        carteDecode[index1, index2] = 'a';
                        Spread(ref carteDecode, index1, index2, tabValeursCarte);
                        isPremiereParcelle = false;
                    }
                    else if (carteDecode[index1, index2] == '\0') // Traitement des autres commentaires
                    {
                        carteDecode[index1, index2] = derniereParcelle = (char)((int)derniereParcelle + 1); // Pour l'incrémentation des noms des parcelles utilisation du code ASCII
                        Spread(ref carteDecode, index1, index2, tabValeursCarte);
                    }
                }
            }
            return carteDecode;
        }

        /* FS3 Affichache carte
         * Permet d'afficher la carte à l'aide des caractères contenus dans le tableau de la carte décodée
         * Input  : Tableau avec les parcelles nommées
         * Output : void
         */
        public static void AffichageCarte(char[,] carteDecode)
        {
            for (int index1 = 0; index1 < 10; index1++)
            {
                for (int index2 = 0; index2 < 10; index2++)
                {
                    Console.Write(carteDecode[index1, index2] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}