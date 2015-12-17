using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
namespace TankGUIwithXNA
{
    class MessageReceiver
    {
        
            public static void getMessage()
            {

                TcpListener serverListner = null;
                try
                {
                    serverListner = new TcpListener(IPAddress.Any, 7000);
                    serverListner.Start();
                    Byte[] bytes = new Byte[1024];
                    String data;

                    MessageHandler msghandler = new MessageHandler();
                   // CommandHandling handler2 = new CommandHandling();

                    //listening loop;
                    while (true)
                    {
                        TcpClient gameServer = serverListner.AcceptTcpClient();
                        data = null;
                        NetworkStream stream = gameServer.GetStream();

                        int i;

                       
                        // Loop to receive all the data sent by the client.
                        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            // Translate data bytes to a ASCII string.
                            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);  //Encode Byte into a String
                            String[] spltted = data.Split(new Char[] { ':' });
                            
                            Console.WriteLine(data);
                            msghandler.handleData(spltted);
                            //handler2.handleCommand(data);
                        }
                        gameServer.Close();
                        stream.Close();
                    }
                }
                catch (SocketException e)
                {
                    Console.WriteLine("SocketException: {0}", e);
                }
                finally
                {
                    // Stop listening for new clients.
                    serverListner.Stop();
                }

            }
        }
    }

