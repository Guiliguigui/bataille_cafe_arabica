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

    public static void AffichageCarte(char[,] carteDecode)
    {
        for(int index1=0;index1<10;index1++)
        {
            for (int index2 = 0; index2 < 10; index2++)
            {
                Console.Write(carteDecode[index1, index2]);
            }
            Console.WriteLine();
        }
    }
}
