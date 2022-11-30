using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Media;

namespace LetsGetMoving
{


    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D bluePiece;
        Texture2D gameBoard;
        Texture2D[] darkTrianglesPointingDown = new Texture2D[6];
        Texture2D[] lightTrianglesPointingDown = new Texture2D[6];
        Texture2D[] darkTrianglesPointingUp = new Texture2D[6];
        Texture2D[] lightTrianglesPointingUp = new Texture2D[6];
        Texture2D[] blackPieces = new Texture2D[15];
        Texture2D[] whitePieces = new Texture2D[15];
        Vector2 bluePiecePosition;
        Vector2 savedPosition;
        Vector2 gameBoardPosition;
        Vector2[] blackPiecePosition = new Vector2[15];
        Vector2[] whitePiecePosition = new Vector2[15];
        int[] yPosition = new int[12];
        int[] xPosition = new int[12];
        bool rightKeyReleased;
        bool leftKeyReleased;
        bool upKeyReleased;
        bool downKeyReleased;
        bool mReleased;
        float Of;
        MouseState mState;
        Song song;

        Random random = new Random();
        Texture2D[] dieOne = new Texture2D[6];
        Texture2D[] dieTwo = new Texture2D[6];
        Texture2D[] dieThree = new Texture2D[6];
        Texture2D[] dieFour = new Texture2D[6];
        Texture2D rollDiceButton;
        int valueDieOne;
        int valueDieTwo;
        int valueDieThree;
        int valueDieFour;
        int RollDiceButtonRadius = 34;
        Vector2 RollDiceButtonPosition = new Vector2(670, 205);

        void RollDice()
        {
            valueDieOne = random.Next(0, 6);
            valueDieTwo = random.Next(0, 6);
            if (valueDieOne == valueDieTwo)
            {
                valueDieThree = valueDieOne;
                valueDieFour = valueDieOne;
            }

        }
        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume -= 0.1f;
            MediaPlayer.Play(song);
        }


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            for (int x = 0; x < 12; x++)
            {
                xPosition[x] = 59 + (x * 59);
                yPosition[x] = 2 + (x * 39);
            }

            bluePiecePosition = new Vector2(xPosition[0], yPosition[0]);
            rightKeyReleased = true;
            leftKeyReleased = true;
            upKeyReleased = true;
            downKeyReleased = true;
            gameBoardPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            blackPiecePosition = new Vector2[15] {  new Vector2(xPosition[0], yPosition[0]), new Vector2(xPosition[0], yPosition[1]),
                                                    new Vector2(xPosition[0], yPosition[2]), new Vector2(xPosition[0], yPosition[3]),
                                                    new Vector2(xPosition[0], yPosition[4]), new Vector2(xPosition[11], yPosition[0]),
                                                    new Vector2(xPosition[11], yPosition[1]), new Vector2(xPosition[4], yPosition[9]),
                                                    new Vector2(xPosition[4], yPosition[10]), new Vector2(xPosition[4], yPosition[11]),
                                                    new Vector2(xPosition[6], yPosition[7]), new Vector2(xPosition[6], yPosition[8]),
                                                    new Vector2(xPosition[6], yPosition[9]), new Vector2(xPosition[6], yPosition[10]),
                                                    new Vector2(xPosition[6], yPosition[11])};

            whitePiecePosition = new Vector2[15] {  new Vector2(xPosition[0], yPosition[7]), new Vector2(xPosition[0], yPosition[8]),
                                                    new Vector2(xPosition[0], yPosition[9]), new Vector2(xPosition[0], yPosition[10]),
                                                    new Vector2(xPosition[0], yPosition[11]), new Vector2(xPosition[4], yPosition[0]),
                                                    new Vector2(xPosition[4], yPosition[1]), new Vector2(xPosition[4], yPosition[2]),
                                                    new Vector2(xPosition[6], yPosition[0]), new Vector2(xPosition[6], yPosition[1]),
                                                    new Vector2(xPosition[6], yPosition[2]), new Vector2(xPosition[6], yPosition[3]),
                                                    new Vector2(xPosition[6], yPosition[4]), new Vector2(xPosition[11], yPosition[10]),
                                                    new Vector2(xPosition[11], yPosition[11])};
            base.Initialize();
        }

        protected override void LoadContent()
            
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            bluePiece = Content.Load<Texture2D>("BluePiece");
            gameBoard = Content.Load<Texture2D>("BlankGameBoard");
            this.song = Content.Load<Song>("testsoundeffect2");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            for (int x = 0; x < 6; x++)
            {
                darkTrianglesPointingDown[x] = Content.Load<Texture2D>("DarkGreenPointingDown");
                lightTrianglesPointingDown[x] = Content.Load<Texture2D>("LightGreenPointingDown");
                darkTrianglesPointingUp[x] = Content.Load<Texture2D>("DarkGreenPointingUp");
                lightTrianglesPointingUp[x] = Content.Load<Texture2D>("LightGreenPointingUp");
            }

            for (int x = 0; x < 15; x++)
            {
                blackPieces[x] = Content.Load<Texture2D>("BlackPiece");
                whitePieces[x] = Content.Load<Texture2D>("WhitePiece");
            }

            rollDiceButton = Content.Load<Texture2D>("RollDiceButton");

            dieOne[0] = Content.Load<Texture2D>("whitedie1");
            dieOne[1] = Content.Load<Texture2D>("whitedie2");
            dieOne[2] = Content.Load<Texture2D>("whitedie3");
            dieOne[3] = Content.Load<Texture2D>("whitedie4");
            dieOne[4] = Content.Load<Texture2D>("whitedie5");
            dieOne[5] = Content.Load<Texture2D>("whitedie6");

            dieTwo[0] = Content.Load<Texture2D>("whitedie1");
            dieTwo[1] = Content.Load<Texture2D>("whitedie2");
            dieTwo[2] = Content.Load<Texture2D>("whitedie3");
            dieTwo[3] = Content.Load<Texture2D>("whitedie4");
            dieTwo[4] = Content.Load<Texture2D>("whitedie5");
            dieTwo[5] = Content.Load<Texture2D>("whitedie6");

            dieThree[0] = Content.Load<Texture2D>("whitedie1");
            dieThree[1] = Content.Load<Texture2D>("whitedie2");
            dieThree[2] = Content.Load<Texture2D>("whitedie3");
            dieThree[3] = Content.Load<Texture2D>("whitedie4");
            dieThree[4] = Content.Load<Texture2D>("whitedie5");
            dieThree[5] = Content.Load<Texture2D>("whitedie6");

            dieFour[0] = Content.Load<Texture2D>("whitedie1");
            dieFour[1] = Content.Load<Texture2D>("whitedie2");
            dieFour[2] = Content.Load<Texture2D>("whitedie3");
            dieFour[3] = Content.Load<Texture2D>("whitedie4");
            dieFour[4] = Content.Load<Texture2D>("whitedie5");
            dieFour[5] = Content.Load<Texture2D>("whitedie6");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mState = Mouse.GetState();
            // Right Key Logic
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && rightKeyReleased == true)
            {
                savedPosition = bluePiecePosition;
                bluePiecePosition = new Vector2(savedPosition.X + 59, savedPosition.Y);
                rightKeyReleased = false;
            }

            for (int x = 0; x < 15; x++)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    if (bluePiecePosition.X == blackPiecePosition[x].X + 59 && bluePiecePosition.Y == blackPiecePosition[x].Y)
                    {
                    Vector2 newPosition  = new Vector2(blackPiecePosition[x].X + 59, blackPiecePosition[x].Y);
                    blackPiecePosition[x] = newPosition;
                    }

                    else if (bluePiecePosition.X == whitePiecePosition[x].X + 59 && bluePiecePosition.Y == whitePiecePosition[x].Y)
                    {
                        Vector2 newPosition = new Vector2(whitePiecePosition[x].X + 59, whitePiecePosition[x].Y);
                        whitePiecePosition[x] = newPosition;
                    }
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Right))
            {
                rightKeyReleased = true;
            }

            // Left Key Logic
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && leftKeyReleased == true)
            {
                savedPosition = bluePiecePosition;
                bluePiecePosition = new Vector2(savedPosition.X - 59, savedPosition.Y);
                leftKeyReleased = false;
            }

            for (int x = 0; x < 15; x++)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    if (bluePiecePosition.X == blackPiecePosition[x].X - 59 && bluePiecePosition.Y == blackPiecePosition[x].Y)
                    {
                        Vector2 newPosition = new Vector2(blackPiecePosition[x].X - 59, blackPiecePosition[x].Y);
                        blackPiecePosition[x] = newPosition;
                    }

                    else if (bluePiecePosition.X == whitePiecePosition[x].X - 59 && bluePiecePosition.Y == whitePiecePosition[x].Y)
                    {
                        Vector2 newPosition = new Vector2(whitePiecePosition[x].X -59, whitePiecePosition[x].Y);
                        whitePiecePosition[x] = newPosition;
                    }
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Left))
            {
                leftKeyReleased = true;
            }

            // Up Key Logic
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && upKeyReleased == true)
            {
                savedPosition = bluePiecePosition;
                bluePiecePosition = new Vector2(savedPosition.X, savedPosition.Y - 39);
                upKeyReleased = false;
            }

            for (int x = 0; x < 15; x++)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    if (bluePiecePosition.Y == blackPiecePosition[x].Y - 39 && bluePiecePosition.X == blackPiecePosition[x].X)
                    {
                        Vector2 newPosition = new Vector2(blackPiecePosition[x].X, blackPiecePosition[x].Y - 39);
                        blackPiecePosition[x] = newPosition;
                    }

                    else if (bluePiecePosition.Y == whitePiecePosition[x].Y - 39 && bluePiecePosition.X == whitePiecePosition[x].X)
                    {
                        Vector2 newPosition = new Vector2(whitePiecePosition[x].X, whitePiecePosition[x].Y - 39);
                        whitePiecePosition[x] = newPosition;
                    }
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Up))
            {
                upKeyReleased = true;
            }

            // Down Key Logic
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && downKeyReleased == true)
            {
                savedPosition = bluePiecePosition;
                bluePiecePosition = new Vector2(savedPosition.X, savedPosition.Y + 39);
                downKeyReleased = false;
            }

            for (int x = 0; x < 15; x++)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    if (bluePiecePosition.Y == blackPiecePosition[x].Y + 39 && bluePiecePosition.X == blackPiecePosition[x].X)
                    {
                        Vector2 newPosition = new Vector2(blackPiecePosition[x].X, blackPiecePosition[x].Y + 39);
                        blackPiecePosition[x] = newPosition;
                    }

                    else if (bluePiecePosition.Y == whitePiecePosition[x].Y + 39 && bluePiecePosition.X == whitePiecePosition[x].X)
                    {
                        Vector2 newPosition = new Vector2(whitePiecePosition[x].X, whitePiecePosition[x].Y + 39);
                        whitePiecePosition[x] = newPosition;
                    }
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Down))
            {
                downKeyReleased = true;
            }

            // Roll Dice Button Logic
            if (mState.LeftButton == ButtonState.Pressed && mReleased == true)
            {
                float mouseRollDiceButtonDistance = Vector2.Distance(new Vector2(RollDiceButtonPosition.X + RollDiceButtonRadius, RollDiceButtonPosition.Y + RollDiceButtonRadius), mState.Position.ToVector2());
                if (mouseRollDiceButtonDistance < RollDiceButtonRadius)
                {
                    RollDice();
                    mReleased = false;
                }
            }

            if (mState.LeftButton == ButtonState.Released)
            {
                mReleased = true;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();
            _spriteBatch.Draw(gameBoard, gameBoardPosition, null, Color.White, Of, new Vector2(gameBoard.Width / 2, gameBoard.Height / 2), Vector2.One, SpriteEffects.None, Of);
            for (int x = 0; x < 6; x++)
            {
                _spriteBatch.Draw(darkTrianglesPointingDown[x], new Vector2((118 * x) + 49, 2), Color.White);
                _spriteBatch.Draw(lightTrianglesPointingDown[x], new Vector2((118 * x) + 109, 2), Color.White);
                _spriteBatch.Draw(darkTrianglesPointingUp[x], new Vector2((118 * x) + 109, 280), Color.White);
                _spriteBatch.Draw(lightTrianglesPointingUp[x], new Vector2((118 * x) + 49, 280), Color.White);
            }

            for (int y = 0; y < 15; y++)
            {
                // Black Pieces Left Stack of 5
                _spriteBatch.Draw(blackPieces[y], blackPiecePosition[y], Color.Black);
                // White Pieces Left Stack of 5
                _spriteBatch.Draw(whitePieces[y], whitePiecePosition[y], Color.White);
            }

            _spriteBatch.Draw(bluePiece, bluePiecePosition, Color.White);

            _spriteBatch.Draw(dieOne[valueDieOne], new Vector2(190, 226), Color.White);
            _spriteBatch.Draw(dieTwo[valueDieTwo], new Vector2(250, 226), Color.White);

            if (valueDieOne == valueDieTwo)
            {
                _spriteBatch.Draw(dieThree[valueDieThree], new Vector2(130, 226), Color.White);
                _spriteBatch.Draw(dieFour[valueDieFour], new Vector2(70, 226), Color.White);
            }

            _spriteBatch.Draw(rollDiceButton, RollDiceButtonPosition, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}