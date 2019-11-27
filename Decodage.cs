using System;

public class Decodage
{
    public static int[,] Lecture(string trame)
    {
        int[,] tabValeursCarte = new int[10, 10];
        string[] tabLignes = trame.Split('|');
        for (int index1 = 0; index1 < 10; index1++)
        {
            string[] ligneSplit = tabLignes[index1].Split(':');
            for (int index2 = 0; index2 < 10; index2++)
                int.TryParse(ligneSplit[index2], out tabValeursCarte[index1, index2]);
        }
        return tabValeursCarte;
    }

    public static void Spread(ref char[,] carteDecode, int index1, int index2, int[,] tabValeursCarte)
    {
        List<int[]> toDo = new List<int[]> { new int[] { index1, index2 } };
        while (toDo.Count > 0)
        {
            List<int[]> toDoTemp = toDo.ToList();
            foreach (int[] parcelle in toDo)
            {
                bool bordureNord = false, bordureOuest = false, bordureEst = false, bordureSud = false;
                int valeurCase = tabValeursCarte[parcelle[0], parcelle[1]];

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
            }
            toDo = toDoTemp.ToList();
        }
    }

    public static char[,] Decodage(int[,] tabValeursCarte)
    {
        char[,] carteDecode = new char[10, 10];
        char derniereParcelle = 'a';
        bool premiereParcelle = true;

        for (int index1 = 0; index1 < 10; index1++)
        {
            for (int index2 = 0; index2 < 10; index2++)
            {
                int valeurCase = tabValeursCarte[index1, index2];

                if (valeurCase / 64 == 1)
                {
                    carteDecode[index1, index2] = 'M';
                }
                else if (valeurCase / 32 == 1)
                {
                    carteDecode[index1, index2] = 'F';
                }
                else if (premiereParcelle)
                {
                    carteDecode[index1, index2] = 'a';
                    Spread(ref carteDecode, index1, index2, tabValeursCarte);
                    premiereParcelle = false;
                }
                else if (carteDecode[index1, index2] == '\0')
                {
                    carteDecode[index1, index2] = derniereParcelle = (char)((int)derniereParcelle + 1);
                    Spread(ref carteDecode, index1, index2, tabValeursCarte);
                }
            }
        }
        return carteDecode;
    }

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
