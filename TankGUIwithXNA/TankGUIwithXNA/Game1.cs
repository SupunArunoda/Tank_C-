using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TankGUIwithXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 
    public struct PlayerData
    {
        public int playerNumber;
        public Vector2 Position;
        public bool IsAlive, whetherShot;
        public Color Color;
        public int direction;
        public double health, coins, points;

    }

    public struct BrickData
    {
        public Vector2 Position;
        public int damage;
    }

    public struct TressureData
    {
        public int timeLeft;
        public Vector2 Position;
        public int value;
    }
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;

        ///<Texture variables for game images.....>
        Texture2D backgroundTexture1, backgroundTexture2, backgroundTexture3, backgroundTexture4, backgroundTexture5;
        Texture2D stoneTexture1, stoneTexture2, stoneTexture3, stoneTexture4;
        Texture2D waterTexture1, waterTexture2, waterTexture3, waterTexture4;
        Texture2D brickTexture1, brickTexture2, brickTexture3, brickTexture4;
        Texture2D coinTexture1, coinTexture2, coinTexture3, coinTexture4;
        Texture2D lifeTexture1, lifeTexture2, lifeTexture3, lifeTexture4;
        Texture2D tankTexture1, tankTexture2, tankTexture3, tankTexture0;
        Rectangle stoneBlock, waterBlock, brickBlock, coinBlock, gameScreen, lifeBlock, tankBlock0, tankBlock1, tankBlock2, tankBlock3;

        ///<screen parameters....>
        int screenWidth;
        int screenHeight;
        int blockWidth, blockHeight;

        public static PlayerData[] players;
        public static BrickData[] bricks;
        public static TressureData[] coins, life = new TressureData[100];
        public static int numberOfPlayers = 5;


        ///<g='ground , 'w='water' , s='stone' , b='brick' , c='coin' , l='life'>

        public static String[,] gameMatrix = new String[MessageHandler.mapSize, MessageHandler.mapSize];
        /*{

        { "s", "g", "b0", "g", "c", "w", "s", "g", "b0", "g" }, 
        { "s", "g", "l", "g", "w", "w", "s", "g", "g", "w"   }, 
        { "g", "g", "b0", "g", "g", "g", "s", "c", "g", "g"  },
        { "g", "g", "b0", "w", "l", "w", "s", "g", "b2", "g" }, 
        { "s", "g", "g", "g", "g", "g", "s", "g", "b0", "g"  }, 
        { "s", "g", "b1", "g", "g", "g", "g", "c", "b0", "s" },
        { "s", "w", "b1", "g", "g", "g", "g", "g", "g", "s"  }, 
        { "s", "w", "b0", "b0", "b1", "w", "s", "g", "g", "g"}, 
        { "s", "w", "b0", "g", "c", "w", "s", "g", "b3", "g" }, 
        { "s", "w", "b0", "g", "g", "w", "s", "c", "b3", "g" } 

        };*/

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            graphics.PreferredBackBufferWidth = 951;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = "Battle Force";
            base.Initialize();
        }

        protected override void LoadContent()
        {
            ///< Create a new SpriteBatch, which can be used to draw textures....>
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ///< load your game content here....>
            device = graphics.GraphicsDevice;
            backgroundTexture1 = Content.Load<Texture2D>("background11");
            backgroundTexture2 = Content.Load<Texture2D>("background22");
            backgroundTexture3 = Content.Load<Texture2D>("background33");
            backgroundTexture4 = Content.Load<Texture2D>("background44");
            backgroundTexture5 = Content.Load<Texture2D>("background55");
            stoneTexture1 = Content.Load<Texture2D>("stone11");
            stoneTexture2 = Content.Load<Texture2D>("stone22");
            stoneTexture3 = Content.Load<Texture2D>("stone33");
            stoneTexture4 = Content.Load<Texture2D>("stone44");
            waterTexture1 = Content.Load<Texture2D>("water11");
            waterTexture2 = Content.Load<Texture2D>("water22");
            waterTexture3 = Content.Load<Texture2D>("water33");
            waterTexture4 = Content.Load<Texture2D>("water44");
            brickTexture1 = Content.Load<Texture2D>("brick11");
            brickTexture2 = Content.Load<Texture2D>("brick22");
            brickTexture3 = Content.Load<Texture2D>("brick33");
            brickTexture4 = Content.Load<Texture2D>("brick44");
            coinTexture1 = Content.Load<Texture2D>("coin11");
            coinTexture2 = Content.Load<Texture2D>("coin22");
            coinTexture3 = Content.Load<Texture2D>("coin33");
            coinTexture4 = Content.Load<Texture2D>("coin44");
            lifeTexture1 = Content.Load<Texture2D>("life11");
            lifeTexture2 = Content.Load<Texture2D>("life22");
            lifeTexture3 = Content.Load<Texture2D>("life33");
            lifeTexture4 = Content.Load<Texture2D>("life44");
            tankTexture0 = Content.Load<Texture2D>("tank00");
            tankTexture1 = Content.Load<Texture2D>("tank11");
            tankTexture2 = Content.Load<Texture2D>("tank22");
            tankTexture3 = Content.Load<Texture2D>("tank33");

            ///<initializing sreen parameters>
            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;
            blockWidth = (screenWidth *4/3) / MessageHandler.mapSize;
            blockHeight = (2*screenHeight ) / MessageHandler.mapSize;
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin();
           
            DrawMap();
            SetUpPlayers();
            updateBrick();
            updateTressure();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void SetUpPlayers()
        {
            Color[] playerColors = new Color[5];
            playerColors[0] = Color.Red;
            playerColors[1] = Color.Green;
            playerColors[2] = Color.Blue;
            playerColors[3] = Color.Purple;
            playerColors[4] = Color.Orange;


            players = new PlayerData[numberOfPlayers];
            for (int i = 0; i < numberOfPlayers; i++)
            {
                players[i].playerNumber = i + 1;
                players[i].IsAlive = true;
                players[i].Color = playerColors[i];

            }
        }

        private void DrawMap()
        {


            gameScreen = new Rectangle(0,0,screenWidth , screenHeight );
            spriteBatch.Draw(backgroundTexture5, gameScreen, Color.White);//change background texture number here..



            for (int i = 0; i < MessageHandler.mapSize; i++)
                for (int j = 0; j < MessageHandler.mapSize; j++)
                {
                    Console.WriteLine(gameMatrix[i,j]);
                    
                    
                        if(gameMatrix[i, j]=="s")
                            {
                                stoneBlock = new Rectangle(blockWidth * j + 10, blockHeight * i + 10, blockWidth, blockHeight);
                                spriteBatch.Draw(stoneTexture2, stoneBlock, Color.White);//change stone texture number here..
                                continue;
                            }
                        else if(gameMatrix[i, j]=="w")
                            {
                                waterBlock = new Rectangle(blockWidth * j + 10, blockHeight * i + 10, blockWidth, blockHeight);
                                spriteBatch.Draw(waterTexture4, waterBlock, Color.White);//change water texture number here..
                                continue;
                            }

                        else if (gameMatrix[i, j] == "p0")
                    
                            {
                                tankBlock0 = new Rectangle(blockWidth * j + 10, blockHeight * i + 10, blockWidth, blockHeight);
                                spriteBatch.Draw(tankTexture0, tankBlock0, Color.White);
                                continue;
                            }

                        else if (gameMatrix[i, j] == "p1")
                    
                            {
                                tankBlock1 = new Rectangle(blockWidth * j + 10, blockHeight * i + 10, blockWidth, blockHeight);
                                spriteBatch.Draw(tankTexture1, tankBlock1, Color.White);
                                continue;
                            }

                        else if (gameMatrix[i, j] == "p2")
                 
                            {
                                tankBlock2 = new Rectangle(blockWidth * j + 10, blockHeight * i + 10, blockWidth, blockHeight);
                                spriteBatch.Draw(tankTexture2, tankBlock2, Color.White);
                                continue;
                            }

                        else if (gameMatrix[i, j] == "p3")
                 
                            {
                                tankBlock3 = new Rectangle(blockWidth * j + 10, blockHeight * i + 10, blockWidth, blockHeight);
                                spriteBatch.Draw(tankTexture3, tankBlock3, Color.White);
                                continue;
                            
                             }
                }
        }

        public static void setGameMatrix(String[,] matrix)
        {
            gameMatrix = matrix;

        }




        public void updateBrick()
        {
            for (int i = 0; i < MessageHandler.mapSize; i++)
                for (int j = 0; j < MessageHandler.mapSize; j++)
                {
                    switch (gameMatrix[i, j])
                    {
                        case "b0":
                            {
                                brickBlock = new Rectangle(blockWidth * j + 10, blockHeight * i + 10, blockWidth, blockHeight);
                                spriteBatch.Draw(brickTexture2, brickBlock, Color.White);//change water texture number here..
                                continue;
                            }
                        case "b1":
                            {
                                brickBlock = new Rectangle(blockWidth * j + 10, blockHeight * i + 10, blockWidth, blockHeight);
                                spriteBatch.Draw(brickTexture2, brickBlock, Color.White * 0.8f);//change water texture number here..
                                continue;
                            }
                        case "b2":
                            {
                                brickBlock = new Rectangle(blockWidth * j + 10, blockHeight * i + 10, blockWidth, blockHeight);
                                spriteBatch.Draw(brickTexture2, brickBlock, Color.White * 0.6f);//change water texture number here..
                                continue;
                            }
                        case "b3":
                            {
                                brickBlock = new Rectangle(blockWidth * j + 10, blockHeight * i + 10, blockWidth, blockHeight);
                                spriteBatch.Draw(brickTexture2, brickBlock, Color.White * 0.4f);//change water texture number here..
                                continue;
                            }

                    }

                }
        }

        public void updateTressure()
        {
            for (int i = 0; i < MessageHandler.mapSize; i++)
                for (int j = 0; j < MessageHandler.mapSize; j++)
                {
                    switch (gameMatrix[i, j])
                    {
                        case "L":
                            {
                                lifeBlock = new Rectangle(blockWidth * j + 10, blockHeight * i + 10, blockWidth, blockHeight);
                                spriteBatch.Draw(lifeTexture3, lifeBlock, Color.White);//change water texture number here..
                                continue;
                            }

                        case "C":
                            {
                                coinBlock = new Rectangle(blockWidth * j + 10, blockHeight * i + 10, blockWidth, blockHeight);
                                spriteBatch.Draw(coinTexture3, coinBlock, Color.White);//change coin texture number here..
                                continue;
                            }
                    }
                }
        }
    }
}
