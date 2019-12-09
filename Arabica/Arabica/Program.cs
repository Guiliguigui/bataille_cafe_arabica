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
    class Program
    {
        static void Main(string[] args)
        {
            Socket S = null;
            string address_ip = "51.91.120.237";
            //string address_ip = "172.16.0.88";
            int port_number = 1212;
            Serveur.ConnectionServeur(ref S, address_ip, port_number);
            string Trame = Serveur.RecevoirDuServeur(ref S);
            int[,] tabValeursCarte = new int[10, 10];
            tabValeursCarte = PartieDecodage.Lecture(Trame);
            //tabValeursCarte = PartieDecodage.Lecture("67:69:69:69:69:69:69:69:69:73|74:3:9:7:5:13:3:1:9:74|74:2:8:7:5:13:6:4:12:74|74:6:12:7:9:7:13:3:9:74|74:3:9:11:6:13:7:4:8:74|74:6:12:6:13:11:3:13:14:74|74:7:13:7:13:10:10:3:9:74|74:3:9:11:7:12:14:2:8:74|74:6:12:6:13:7:13:6:12:74|70:69:69:69:69:69:69:69:69:76|");
            //tabValeursCarte = PartieDecodage.Lecture("7:1:9:79:79:79:79:79:79:79|79:6:0:13:79:79:79:79:79:79|79:79:10:79:79:79:79:79:79:79|79:79:10:79:79:79:11:79:79:79|79:3:0:9:79:79:10:79:79:79|79:6:4:4:5:5:0:13:79:79|79:79:79:79:79:79:10:79:11:79|79:79:79:79:7:5:4:5:12:79|79:79:79:79:79:79:79:79:79:79|79:79:79:79:79:79:79:79:79:79|");
            //tabValeursCarte = PartieDecodage.Lecture("3:9:71:69:65:65:65:65:65:73|2:8:3:9:70:68:64:64:64:72|6:12:2:8:3:9:70:68:64:72|11:11:6:12:6:12:3:9:70:76|10:10:11:11:67:73:6:12:3:9|14:14:10:10:70:76:7:13:6:12|3:9:14:14:11:7:13:3:9:75|2:8:7:13:14:3:9:6:12:78|6:12:3:1:9:6:12:35:33:41|71:77:6:4:12:39:37:36:36:44|");
            //tabValeursCarte = PartieDecodage.Lecture("3:1:1:1:1:1:1:1:1:9|2:0:0:0:0:0:0:0:0:8|2:0:0:0:0:0:0:0:0:8|2:0:0:0:0:0:0:0:0:8|2:0:0:0:0:0:0:0:0:8|2:0:0:0:0:0:0:0:0:8|2:0:0:0:0:0:0:0:0:8|2:0:0:0:0:0:0:0:0:8|2:0:0:0:0:0:0:0:0:8|6:4:4:4:4:4:4:4:4:12|");
            char[,] carteDecode = new char[10, 10];
            carteDecode = PartieDecodage.Decodage(tabValeursCarte);
            PartieDecodage.AffichageCarte(carteDecode);

            Console.ReadKey();
        }
    }
}
