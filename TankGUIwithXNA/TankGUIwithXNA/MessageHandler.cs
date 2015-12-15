using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TankGUIwithXNA
{
    class MessageHandler
    {
       
            public static int mapSize = 20;
            private String[,] map = new String[mapSize, mapSize];
            private String[,] temp = new String[mapSize, mapSize];
            private int playerNumber = 0;
            private int direction = 0;
            int num_of_plyer = 0; ///no of players
            private List<coin_pile> coins = new List<coin_pile>();
            private List<life_pack> lifes = new List<life_pack>();
            private List<player_data> players = new List<player_data>();
            int[] currentX;
            int[] currentY;

            public struct coin_pile
            {
                public int x;//coin pile x direction
                public int y;//coin pile y direction
                public int life;// coin pile life
                public int value;// coin value
            }

            public struct life_pack
            {
                public int x;
                public int y;
                public int life;
            }

            public struct player_data
            {
                public int x;
                public int y;
                public int direct;//player direction
                public int coins;//plyeer coins
                public int points;//player points
            }

            public void handleData(String[] msg)
            {

                
                if (msg[0] == "S")
                {
                    String[] plyrdt = msg[1].Split(new Char[] { ';' });

                    ///getting my number....
                    String[] pn = plyrdt[0].Split(new Char[] { 'P' });
                    playerNumber = Convert.ToInt32(pn[1]);

                    ///getting my position...
                    String[] coord = plyrdt[1].Split(new Char[] { ',' });
                    map[Convert.ToInt32(coord[1]), Convert.ToInt32(coord[0])] = "";

                    ///getting direction...
                    String[] dir = plyrdt[2].Split(new Char[] { '#' });
                    direction = Convert.ToInt32(dir[0]);
                }

                ///map initialization....
                if (msg[0] == "I")
                {

                    for (int i = 0; i < mapSize; i++)
                    {
                        for (int j = 0; j < mapSize; j++)
                        {
                            map[i, j] = "g";
                        }
                    }

                    //String[] pn = msg[1].Split(new Char[] { 'P' });
                    //playerNumber = Convert.ToInt32( pn[1]);

                    String[] bricks = msg[2].Split(new Char[] { ';' });
                    String[] stone = msg[3].Split(new Char[] { ';' });
                    String[] water = msg[4].Split(new Char[] { ';' });



                    for (int i = 0; i < bricks.Length; i++)
                    {
                        String[] coordinates = bricks[i].Split(new Char[] { ',' });
                        map[Convert.ToInt32(coordinates[1]), Convert.ToInt32(coordinates[0])] = "b0";
                    }

                    for (int i = 0; i < stone.Length; i++)
                    {
                        String[] coordinates = stone[i].Split(new Char[] { ',' });
                        map[Convert.ToInt32(coordinates[1]), Convert.ToInt32(coordinates[0])] = "s";
                    }

                    for (int i = 0; i < water.Length; i++)
                    {
                        String[] coordinates = water[i].Split(new Char[] { ',' });
                        if (i == water.Length - 1)
                        {
                            String[] lst = coordinates[1].Split(new Char[] { '#' });

                            map[Convert.ToInt32(lst[0]), Convert.ToInt32(coordinates[0])] = "w";
                        }
                        else
                        {
                            map[Convert.ToInt32(coordinates[1]), Convert.ToInt32(coordinates[0])] = "w";
                        }

                    }

                    for (int i = 0; i < mapSize; i++)
                    {
                        for (int j = 0; j < mapSize; j++)
                        {
                            temp[i, j] = map[i, j];
                        }
                        Console.WriteLine("");
                    }

                    

                }

                ///coin piles...
                if (msg[0] == "C")
                {
                    ///getting coin position...
                    String[] coordinates = msg[1].Split(new Char[] { ',' });

                    coin_pile temparyCoin = new coin_pile();
                    temparyCoin.x = Convert.ToInt32(coordinates[0]);
                    temparyCoin.y = Convert.ToInt32(coordinates[1]);

                    /// getting coin timeout...
                    temparyCoin.life = Convert.ToInt32(msg[2]);

                    ///getting coin values...
                    String[] dir = msg[3].Split(new Char[] { '#' });
                    temparyCoin.value = Convert.ToInt32(dir[0]);

                    coins.Add(temparyCoin);

                    //temp = map;


                }

                ///Life Packs...
                if (msg[0] == "L")
                {
                    ///getting LifePack position...
                    String[] coordinates = msg[1].Split(new Char[] { ',' });

                    life_pack temparyLife = new life_pack();
                    temparyLife.x = Convert.ToInt32(coordinates[0]);
                    temparyLife.y = Convert.ToInt32(coordinates[1]);

                    ///getting life timeout...
                    String[] to = msg[2].Split(new Char[] { '#' });
                    temparyLife.life = Convert.ToInt32(to[0]);

                    lifes.Add(temparyLife);
                }

                ///Global Updates...
                if (msg[0] == "G")
                {
                    int nop = 0;
                    for (int i = 0; i < mapSize; i++)
                    {
                        for (int j = 0; j < mapSize; j++)
                        {
                            map[i, j] = temp[i, j];
                        }
                        
                    }



                    for (int i = 1; i < msg.Length; i++)
                    {
                        if (msg[i].ElementAt(0) == 'P')
                        {
                            nop++;
                        }
                    }
                    num_of_plyer = nop;
                    currentX = new int[nop];
                    currentY = new int[nop];
                    Console.WriteLine("Number of Players......." + nop);


                    Console.WriteLine("call updateCoins...");
                    for (int i = 1; i <= nop; i++)
                    {
                        String[] playerdata = msg[i].Split(new Char[] { ';' });
                        String[] coordinates = playerdata[1].Split(new Char[] { ',' });
                        //int 
                        currentX[i - 1] = Convert.ToInt32(coordinates[0]);
                        currentY[i - 1] = Convert.ToInt32(coordinates[1]);

                        switch (playerdata[2])
                        {
                            case "0":
                                {
                                    map[Convert.ToInt32(coordinates[1]), Convert.ToInt32(coordinates[0])] = "p0";
                                    continue;
                                }

                            case "1":
                                {
                                    map[Convert.ToInt32(coordinates[1]), Convert.ToInt32(coordinates[0])] = "p1";
                                    continue;
                                }
                            case "2":
                                {
                                    map[Convert.ToInt32(coordinates[1]), Convert.ToInt32(coordinates[0])] = "p2";
                                    continue;
                                }
                            case "3":
                                {
                                    map[Convert.ToInt32(coordinates[1]), Convert.ToInt32(coordinates[0])] = "p3";
                                    continue;
                                }
                        }
                    }

                    updateCoins();

                    for (int i = 0; i < coins.Count; i++)
                    {
                        if (coins[i].life != 0)
                        {
                            temp[Convert.ToInt32(coins[i].y), Convert.ToInt32(coins[i].x)] = "C";
                        }
                        else
                        {
                            Console.WriteLine("udyuftbg7uaydiuyb");
                            temp[Convert.ToInt32(coins[i].y), Convert.ToInt32(coins[i].x)] = "g";
                        }


                    }
                    Console.WriteLine("call updateLifes...");
                    updateLifes();

                    for (int i = 0; i < lifes.Count; i++)
                    {
                        if (lifes[i].life != 0)
                        {
                            temp[Convert.ToInt32(lifes[i].y), Convert.ToInt32(lifes[i].x)] = "L";
                        }
                        else
                        {
                            temp[Convert.ToInt32(lifes[i].y), Convert.ToInt32(lifes[i].x)] = "g";
                        }


                    }


                }

              
            }

            public void updateLifes()
            {
                Console.WriteLine("Update lifes--" + lifes.Count);
                life_pack tempL = new life_pack();
                if (lifes.Count != 0)
                {
                    for (int i = 0; i < lifes.Count; i++)
                    {
                        if (lifes[i].life >= 1000)
                        {
                            int j = lifes[i].life;
                            tempL = lifes[i];
                            tempL.life = j - 1000;
                            lifes.RemoveAt(i);
                            lifes.Insert(i, tempL);
                        }
                        else
                        {
                            tempL = lifes[i];
                            tempL.life = 0;
                            lifes.RemoveAt(i);
                            lifes.Insert(i, tempL);
                        }



                        for (int j = 0; j < num_of_plyer; j++)
                        {
                            if (currentX[j] == lifes[i].x && currentY[j] == lifes[i].y)
                            {
                                tempL = lifes[i];
                                tempL.life = 0;
                                lifes.RemoveAt(i);
                                lifes.Insert(i, tempL);
                                Console.WriteLine("Taken by player...");
                            }
                        }

                      //  Console.WriteLine("Life---------" + lifes[i])life_pack;
                    }
                }
            }

            public void updateCoins()
            {

                Console.WriteLine("Coin pile update..");
                coin_pile tempC = new coin_pile();
                if (coins.Count != 0)
                {
                    for (int i = 0; i < coins.Count; i++)
                    {
                        if (coins[i].life >= 1000)
                        {
                            int j = coins[i].life;
                            tempC = coins[i];
                            tempC.life = j - 1000;
                            coins.RemoveAt(i);
                            coins.Insert(i, tempC);
                        }
                        else
                        {
                            tempC = coins[i];
                            tempC.life = 0;
                            coins.RemoveAt(i);
                            coins.Insert(i, tempC);
                        }

                        for (int j = 0; j < num_of_plyer; j++)
                        {
                            if (currentX[j] == coins[i].x && currentY[j] == coins[i].y)
                            {
                                tempC = coins[i];
                                tempC.life = 0;
                                coins.RemoveAt(i);
                                coins.Insert(i, tempC);
                            }
                        }

                        //Console.WriteLine("Life---------" + coins[i].lt);
                    }
                }


            }

    }
}
