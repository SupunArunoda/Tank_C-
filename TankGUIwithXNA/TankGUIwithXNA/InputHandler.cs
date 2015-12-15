using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankGUIwithXNA
{
    class InputHandler
    {
        String str1 = "PLAYERS_FULL#", str2 = "ALREADY_ADDED#", str3 = "GAME_ALREADY_STARTED#",
            str4 = "OBSTACLE#", str5 = "CELL_OCCUPIED#", str6 = "DEAD#", str7 = "TOO_QUICK#", str8 = "INVALID_CELL#",
            str9 = "GAME_HAS_FINISHED#", str10 = "GAME_NOT_STARTED_YET#", str11 = "NOT_A_VALID_CONTESTANT#";
        public void handleCommand(String str)
        {
            if ((str == str1) || (str == str2) || (str == str3))
            {
                Console.WriteLine(str + "can't connect to the game. Try again later...");
                Game1 game = new Game1();
                game.Exit();
            }


        }
    }
}
