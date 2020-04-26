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
        
        public string RecevoirDuServeur(int size)
        {
            byte[] messageRecu = new byte[size];
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
            string messageRecu = RecevoirDuServeur(4);
            if (messageRecu.Contains("VALI")) return true;
            else return false;
        }

        public int[] GetJeu()
        {
            string messageRecu = RecevoirDuServeur(4);
            if (messageRecu.Contains("FINI")) return null;
            int[] jeuServeur = new int[2];
            jeuServeur[0] = messageRecu[2] - 48;
            jeuServeur[1] = messageRecu[3] - 48;
            return jeuServeur;
        }

        public bool GetRejouer()
        {
            string messageRecu = RecevoirDuServeur(4);
            if (messageRecu.Contains("ENCO")) return true;
            else return false;
        }

        public int[] GetScores()
        {
            string messageRecu = RecevoirDuServeur(7);
            string[] decoupage = messageRecu.Split(':');

            int[] scores = new int[2];
            scores[0] = int.Parse(decoupage[1]);
            scores[1] = int.Parse(decoupage[2]);
            return scores;
        }
        
        public void Fermer()
        {
            S.Shutdown(SocketShutdown.Both);
            S.Close();
        }
    }
}
