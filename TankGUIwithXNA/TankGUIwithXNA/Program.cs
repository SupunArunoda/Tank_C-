using System;
using System.Threading;
namespace TankGUIwithXNA
{
#if WINDOWS || XBOX
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {

            using (Game1 game = new Game1())
            {
                 Connection cnt = new Connection();
                  cnt.connect();
           
            
               // Console.WriteLine("I'm in the initialize method");
                //thread created for receiving messages
                Thread t = new Thread(MessageReceiver.getMessage);
                
                
                t.Start();
                game.Run();
            }


        }
    }
#endif
}

