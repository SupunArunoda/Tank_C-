using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace TankGUIwithXNA
{
    class Connection
    {
        public void connect()
        {

            try
            {
                TcpClient tcpclnt = new TcpClient();
                Console.WriteLine("Connecting.....");

                tcpclnt.Connect("127.0.0.1", 6000);
                Console.WriteLine("Connected");
                String str = "JOIN#";
                NetworkStream netStm = tcpclnt.GetStream();

                ASCIIEncoding ascEn = new ASCIIEncoding();
                byte[] bArry = ascEn.GetBytes(str);
                netStm.Write(bArry, 0, bArry.Length);
                Console.WriteLine("JOIN SUCCESS");


                netStm.Close();

                tcpclnt.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
                Game1 game = new Game1();
                game.Exit();
            }
        }
    }
}
