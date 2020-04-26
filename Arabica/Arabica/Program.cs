using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using ArabicaLibrary;

namespace Arabica
{
    class Program
    {
        static void Main(string[] args)
        {
            //string address_ip = "51.91.120.237";
            //string address_ip = "172.16.0.88";
            string address_ip = "127.0.0.1";
            int port_number = 1213;
            Serveur Serveur = new Serveur(address_ip, port_number);
            string trame = Serveur.RecevoirDuServeur(1024);
            //string trame = ("67:69:69:69:69:69:69:69:69:73|74:3:9:7:5:13:3:1:9:74|74:2:8:7:5:13:6:4:12:74|74:6:12:7:9:7:13:3:9:74|74:3:9:11:6:13:7:4:8:74|74:6:12:6:13:11:3:13:14:74|74:7:13:7:13:10:10:3:9:74|74:3:9:11:7:12:14:2:8:74|74:6:12:6:13:7:13:6:12:74|70:69:69:69:69:69:69:69:69:76|");
            //string trame = ("7:1:9:79:79:79:79:79:79:79|79:6:0:13:79:79:79:79:79:79|79:79:10:79:79:79:79:79:79:79|79:79:10:79:79:79:11:79:79:79|79:3:0:9:79:79:10:79:79:79|79:6:4:4:5:5:0:13:79:79|79:79:79:79:79:79:10:79:11:79|79:79:79:79:7:5:4:5:12:79|79:79:79:79:79:79:79:79:79:79|79:79:79:79:79:79:79:79:79:79|");
            //string trame = ("3:9:71:69:65:65:65:65:65:73|2:8:3:9:70:68:64:64:64:72|6:12:2:8:3:9:70:68:64:72|11:11:6:12:6:12:3:9:70:76|10:10:11:11:67:73:6:12:3:9|14:14:10:10:70:76:7:13:6:12|3:9:14:14:11:7:13:3:9:75|2:8:7:13:14:3:9:6:12:78|6:12:3:1:9:6:12:35:33:41|71:77:6:4:12:39:37:36:36:44|");
            //string trame = ("3:1:1:1:1:1:1:1:1:9|2:0:0:0:0:0:0:0:0:8|2:0:0:0:0:0:0:0:0:8|2:0:0:0:0:0:0:0:0:8|2:0:0:0:0:0:0:0:0:8|2:0:0:0:0:0:0:0:0:8|2:0:0:0:0:0:0:0:0:8|2:0:0:0:0:0:0:0:0:8|2:0:0:0:0:0:0:0:0:8|6:4:4:4:4:4:4:4:4:12|");
            Carte Carte = new Carte(trame);
            Carte.Afficher();
            IA IAClient = new IA("ArabicaMaster");
            IA IAServeur = new IA("ClerentIA");
            bool fini = false;

            while (!fini)
            {
                Random rand = new Random();
                int var = rand.Next(1, 9);
                //jeu du client carte
                Serveur.Jouer(1, var);
                bool valide = Serveur.GetValide();
                Console.WriteLine("valide ="+ valide);
                if (valide)
                {
                    Carte.CarteObjet[1, var].Planter(IAClient);
                }

                int[] jeuServeur = Serveur.GetJeu();
                Console.WriteLine("jeu =" + jeuServeur[0]+ jeuServeur[1]);
                bool rejouer = Serveur.GetRejouer();
                Console.WriteLine("rejouer =" + rejouer);
                if (jeuServeur == null) fini = true;
                else
                {
                    Carte.CarteObjet[jeuServeur[0], jeuServeur[1]].Planter(IAServeur);
                    if (rejouer == false) fini = true;
                }
            }

            int[] scores = Serveur.GetScores();
            Console.WriteLine("Scores : \n -" + IAClient.Nom + " : " + scores[0] + "\n -" + IAServeur.Nom + " : " + scores[1]);
            Carte.Afficher();
            Serveur.Fermer();
            Console.ReadKey();
        }
    }
}
