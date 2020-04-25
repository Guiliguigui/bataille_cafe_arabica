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
    class PartieServeur
    {
        /*FP1 :Procédure ConnectionServeur
         * Elle permet de se connecter au serveur et quitte le programme  en cas d'echec
         * Input : Référence sur un objet de type Socket, un string address_ip, un int port_number
         * Output: (void)
         */
        public static void ConnectionServeur(ref Socket S, string addressIp, int portNumber)
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

        /*FP2 : Fonction RecevoirDuServeur de type string
         * Elle permet de recevoir les informations envoyés pas le serveur ainsi que de clore la connexion avec le serveur
         * Input : Référence sur une Socket
         * Output : un string Trame
         */
        public static string RecevoirDuServeur(Socket S)
        {
            byte[] messageRecu = new byte[1024];
            int byteRecu = S.Receive(messageRecu);
            string messageRecuString = Encoding.ASCII.GetString(messageRecu);
            S.Shutdown(SocketShutdown.Both);
            S.Close();
            return messageRecuString;
        }

        public static bool GetValide(Socket S)
        {
            string messageRecu = RecevoirDuServeur(S);
            if (messageRecu == "VALI") return true;
            else return false;
        }

        public static int[] GetJeuServeur(Socket S)
        {
            string messageRecu = RecevoirDuServeur(S);
            if (messageRecu == "FINI") return null;
            int[] JeuServeur = new int[2];
            JeuServeur[0] = messageRecu[2];
            JeuServeur[1] = messageRecu[3];
            return JeuServeur;
        }

        public static bool GetRejouer(Socket S)
        {
            string messageRecu = RecevoirDuServeur(S);
            if (messageRecu == "ENCO") return true;
            else return false;
        }

        public static int[] GetScores(Socket S)
        {
            string messageRecu = RecevoirDuServeur(S);
            string[] decoupage = messageRecu.Split(':');

            int[] Scores = new int[2];
            Scores[0] = int.Parse(decoupage[1]);
            Scores[1] = int.Parse(decoupage[2]);
            return Scores;
        }

    }
}
