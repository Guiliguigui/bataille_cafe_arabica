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
    class Serveur
    {
        /*Cette procédure permet de se connecter au serveur, elle admet en entrée une référence
         * de type Socket et ainsi que l'address ip et le port du serveur.Cette procédure ne retourne rien.
         * Pour cela elle crée une nouvelle socket dans la référence de la socket passé en paramètre,
         * cette socket admet une adresse ip de type IPV4 et qui utilise le Protocole TCP.
         * Pour finir la procédure se connecte Serveur grâce à la fonction connect de la classe Socket.
         * (Pour se connecter il est nécessaire d'utiliser la fonction parse de la classe IPAddress 
         * sur le string passer en paramètre, puis d'entrer le numéro du port du serveur)
         */
        public static void ConnectionServeur(ref Socket S, string address_ip, int port_number)
        {
            S = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            S.Connect(IPAddress.Parse(address_ip), port_number);

        }

        /*Cette fonction permet de recevoir la trame de la carte de la part du serveur, 
         * elle prend en entrée une référence de type Socket. Pour recevoir un message
         * de la part du serveur on utilise la méthode Receive de la classe Socket.
         * On communique avec le serveur par un type tableau d'octet, pour convertir ce type 
         * en string on utilise la méthode GetString de la classe Encoding.ASCII.
         * Une fois cela terminer on désactive la socket en utilisant les méthodes
         * Shutdown et Close de la classe Socket. Pour finir on retourne la trame de type string.
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
