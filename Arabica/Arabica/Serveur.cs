using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Arabica
{
    /* Classe Serveur
     * Cette classe sert pour tout ce qui concerne la communication avec le serveur
     */
    class Serveur
    {
        private Socket S;
        
        public Serveur(string addressIp, int portNumber)
        {
            try
            {
                S = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                S.Connect(IPAddress.Parse(addressIp), portNumber);
            }
            catch
            {
                Console.WriteLine("La connexion au serveur est impossible");
                Console.ReadKey();
                Environment.Exit(0);
            }

        }

        /* RecevoirDuServeur
         * Permet de recevoir les informations envoyée par le serveur et les convertis en chaine de caractères
         * Input  : taille de la chaine voulue
         * Output : void
         */
        public string RecevoirDuServeur(int size)
        {
            byte[] messageRecu = new byte[size];
            S.Receive(messageRecu);
            string messageRecuString = Encoding.ASCII.GetString(messageRecu);
            return messageRecuString;
        }

        /* Jouer
         * Permet d'envoyer au serveur le jeu du client sous le bon format
         * Input  : coordonées de la case jouée par le client
         * Output : void
         */
        public void Jouer(int x, int y)
        {
            string message = "A:" + x + y;
            S.Send(Encoding.ASCII.GetBytes(message));
        }

        /* GetValide
         * Permet de recevoir la validité de la case jouée par le client
         * Input  : void
         * Output : booléen de validité
         */
        public bool GetValide()
        {
            string messageRecu = RecevoirDuServeur(4);
            if (messageRecu == "VALI") return true;
            else return false;
        }

        /* GetJeu
         * Permet de recevoir le jeu du serveur
         * Input  : void
         * Output : tableau de 2 entiers contenant le jeu du serveur
         *          si le jeu est fini, retourne null
         */
        public int[] GetJeu()
        {
            string messageRecu = RecevoirDuServeur(4);
            if (messageRecu == "FINI") return null;
            int[] jeuServeur = new int[2];
            jeuServeur[0] = messageRecu[2] - 48; // décalement du à l'ASCII
            jeuServeur[1] = messageRecu[3] - 48;
            return jeuServeur;
        }

        /* GetRejouer
         * Permet de savoir si le client doit rejouer
         * Input  : void
         * Output : booléen de validité où false signifie que la partie est finie
         */
        public bool GetRejouer()
        {
            string messageRecu = RecevoirDuServeur(4);
            if (messageRecu == "ENCO") return true;
            else return false;
        }

        /* GetScores
         * Permet de recevoir les scores en fin de partie
         * Input  : void
         * Output : tableau de 2 entiers contenant le score final
         */
        public int[] GetScores()
        {
            string messageRecu = RecevoirDuServeur(7);
            string[] decoupage = messageRecu.Split(':');
            int[] scores = new int[2];
            scores[0] = int.Parse(decoupage[1]);
            scores[1] = int.Parse(decoupage[2]);
            return scores;
        }

        /* Fermer
         * Permet de fermer la connexion avec le serveut
         * Input  : void
         * Output : void
         */
        public void Fermer()
        {
            S.Shutdown(SocketShutdown.Both);
            S.Close();
        }
    }
}
