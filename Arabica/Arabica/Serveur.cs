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
        public static void ConnectionServeur(ref Socket S, string address_ip, int port_number)
        {
            S = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                S.Connect(IPAddress.Parse(address_ip), port_number);
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
        public static string RecevoirDuServeur(ref Socket S)
        {
            byte[] messageRecu = new byte[1024];
            int byteRecu = S.Receive(messageRecu);
            string Trame = Encoding.ASCII.GetString(messageRecu);
            S.Shutdown(SocketShutdown.Both);
            S.Close();
            return Trame;
        }
    }
}
