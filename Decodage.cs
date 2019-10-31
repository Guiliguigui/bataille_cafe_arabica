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

    public static char[,] Decodage(int[,] tabValeursCarte)
    {
        char[,] carteDecode = new char[10, 10];
        char derniereParcelle = '`'; //'`'est le caractère qui précède 'a' en table ASCII

        for (int index1 = 0; index1 < 10; index1++)
        {
            for (int index2 = 0; index2 < 10; index2++)
            {
                bool bordureOuest = false, bordureNord = false;
                int valeurCase = tabValeursCarte[index1, index2];

                if (valeurCase / 64 == 1)
                {
                    carteDecode[index1, index2] = 'M'; valeurCase -= 64;
                }
                else if (valeurCase / 32 == 1)
                {
                    carteDecode[index1, index2] = 'F'; valeurCase -= 32;
                }
                else
                {
                    if (valeurCase / 8 == 1)
                    {
                        valeurCase -= 8;
                    }
                    if (valeurCase / 4 == 1)
                    {
                        valeurCase -= 4;
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
                        carteDecode[index1, index2] = carteDecode[index1 - 1, index2];
                    }
                    else if (!bordureOuest)
                    {
                        carteDecode[index1, index2] = carteDecode[index1, index2 - 1];
                    }
                    else
                    {
                        carteDecode[index1, index2] = derniereParcelle = (char)((int)derniereParcelle + 1);
                    }
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
