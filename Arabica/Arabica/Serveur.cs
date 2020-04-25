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
    /*Class Serveur
     * Cette classe sert pour tout ce qui concerne le serveur
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
        
        public string RecevoirDuServeur()
        {
            byte[] messageRecu = new byte[1024];
            S.Receive(messageRecu);
            string messageRecuString = Encoding.ASCII.GetString(messageRecu);
            return messageRecuString;
        }

        public void Jouer(int x, int y)
        {
            string message = "A:" + x + y;
            S.Send(Encoding.ASCII.GetBytes(message));
        }

        public bool GetValide()
        {
            string messageRecu = RecevoirDuServeur();
            if (messageRecu == "VALI") return true;
            else return false;
        }

        public int[] GetJeu()
        {
            string messageRecu = RecevoirDuServeur();
            if (messageRecu == "FINI") return null;
            int[] JeuServeur = new int[2];
            JeuServeur[0] = messageRecu[2];
            JeuServeur[1] = messageRecu[3];
            return JeuServeur;
        }

        public bool GetRejouer()
        {
            string messageRecu = RecevoirDuServeur();
            if (messageRecu == "ENCO") return true;
            else return false;
        }

        public int[] GetScores()
        {
            string messageRecu = RecevoirDuServeur();
            string[] decoupage = messageRecu.Split(':');

            int[] Scores = new int[2];
            Scores[0] = int.Parse(decoupage[1]);
            Scores[1] = int.Parse(decoupage[2]);
            return Scores;
        }
        
        public void Fermer()
        {
            S.Shutdown(SocketShutdown.Both);
            S.Close();
        }
    }
}
