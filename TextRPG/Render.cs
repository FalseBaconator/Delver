using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRPG
{
    internal class Render
    {

        //public char[,] ScreenChars = new char[GameManager.constants.mapHeight * GameManager.constants.roomHeight, GameManager.constants.mapWidth * GameManager.constants.roomWidth];

        //public ConsoleColor[,] ScreenColors = new ConsoleColor[GameManager.constants.mapHeight * GameManager.constants.roomHeight, GameManager.constants.mapWidth * GameManager.constants.roomWidth];

        //public ConsoleColor[,] BackgroundColors = new ConsoleColor[GameManager.constants.mapHeight * GameManager.constants.roomHeight, GameManager.constants.mapWidth * GameManager.constants.roomWidth];

        public Tile[,] WholeMap = new Tile[GameManager.constants.mapHeight * GameManager.constants.roomHeight, GameManager.constants.mapWidth * GameManager.constants.roomWidth];

        private Hud hud;

        private Camera cam;

        //private char[,] borderChars = new char[GameManager.constants.camSize + 1, GameManager.constants.camSize + 1];
        /*
        {
            {'╔','═','═','═','═','═','═','═','╗'},
            {'║',' ',' ',' ',' ',' ',' ',' ','║'},
            {'║',' ',' ',' ',' ',' ',' ',' ','║'},
            {'║',' ',' ',' ',' ',' ',' ',' ','║'},
            {'║',' ',' ',' ',' ',' ',' ',' ','║'},
            {'║',' ',' ',' ',' ',' ',' ',' ','║'},
            {'║',' ',' ',' ',' ',' ',' ',' ','║'},
            {'║',' ',' ',' ',' ',' ',' ',' ','║'},
            {'╚','═','═','═','═','═','═','═','╝'}
        };*/

        private GameManager gManager;

        //private char[,] printToScreenCharsCur = new char[GameManager.constants.rendHeight, GameManager.constants.rendWidth];
        //private ConsoleColor[,] printToScreenColorsCur = new ConsoleColor[GameManager.constants.rendHeight, GameManager.constants.rendWidth];
        //private ConsoleColor[,] printToScreenBackgroundColorsCur = new ConsoleColor[GameManager.constants.rendHeight, GameManager.constants.rendWidth];

        private Tile[,] toRend = new Tile[GameManager.constants.rendHeight, GameManager.constants.rendWidth];

        //private char[,] printToScreenCharsPrev = new char[GameManager.constants.rendHeight, GameManager.constants.rendWidth];
        //private ConsoleColor[,] printToScreenColorsPrev = new ConsoleColor[GameManager.constants.rendHeight, GameManager.constants.rendWidth];
        //private ConsoleColor[,] printToScreenBackgroundColorsPrev = new ConsoleColor[GameManager.constants.rendHeight, GameManager.constants.rendWidth];

        private Tile[,] prevRend = new Tile[GameManager.constants.rendHeight, GameManager.constants.rendWidth];

        //[GameManager.constants.camSize + 2, GameManager.constants.mapWidth + GameManager.constants.camSize + 2]
        private MiniMap mini;


        public void setGameManager(GameManager gManager)
        {
            this.gManager = gManager;
        }

        public void setHud(Hud hud)
        {
            this.hud = hud;
        }

        public void setCam(Camera camera)
        {
            cam = camera;
        }

        public void setMiniMap(MiniMap miniMap)
        {
            mini = miniMap;
        }

        public void DrawToScreen()  //Draws the map according to the arrays
        {
            Console.CursorVisible = false;
            int x = cam.pos.x - (GameManager.constants.camSize / 2);
            int y = cam.pos.y - (GameManager.constants.camSize / 2);

            //fill with space
            for (int i = 0; i < toRend.GetLength(0); i++)
            {
                for (int j = 0; j < toRend.GetLength(1); j++)
                {
                    toRend[i, j] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                }
            }

            //Add Border
            for (int i = 0; i < GameManager.constants.camSize + 2; i++)
            {
                for (int j = 0; j < GameManager.constants.hudWidth + 1; j++)
                {
                    switch (i)
                    {
                        case 0:
                            switch (j)
                            {
                                case 0:
                                    toRend[i, j] = new Tile('╔', GameManager.constants.borderColor, GameManager.constants.BGColor);
                                    break;
                                case var _ when j == GameManager.constants.hudWidth / 2:
                                    toRend[i, j] = new Tile('╦', GameManager.constants.borderColor, GameManager.constants.BGColor);
                                    break;
                                case var _ when j == GameManager.constants.hudWidth:
                                    toRend[i, j] = new Tile('╗', GameManager.constants.borderColor, GameManager.constants.BGColor);
                                    break;
                                default:
                                    toRend[i, j] = new Tile('═', GameManager.constants.borderColor, GameManager.constants.BGColor);
                                    break;
                            }
                            break;
                        case var _ when i == GameManager.constants.camSize + 1:
                            switch (j)
                            {
                                case 0:
                                    toRend[i, j] = new Tile('╚', GameManager.constants.borderColor, GameManager.constants.BGColor);
                                    break;
                                case var _ when j == GameManager.constants.hudWidth / 2:
                                    toRend[i, j] = new Tile('╩', GameManager.constants.borderColor, GameManager.constants.BGColor);
                                    break;
                                case var _ when j == GameManager.constants.hudWidth:
                                    toRend[i, j] = new Tile('╝', GameManager.constants.borderColor, GameManager.constants.BGColor);
                                    break;
                                default:
                                    toRend[i, j] = new Tile('═', GameManager.constants.borderColor, GameManager.constants.BGColor);
                                    break;
                            }
                            break;
                        default:
                            switch (j)
                            {
                                case 0:
                                case var _ when j == GameManager.constants.hudWidth / 2:
                                case var _ when j == GameManager.constants.hudWidth:
                                    toRend[i, j] = new Tile('║', GameManager.constants.borderColor, GameManager.constants.BGColor);
                                    break;
                                default:
                                    toRend[i, j] = new Tile(' ', GameManager.constants.borderColor, GameManager.constants.BGColor);
                                    break;
                            }
                            break;
                    }
                }
            }

            //Add Camera
            for (int i = 0; i < GameManager.constants.camSize; i++)
            {
                for (int j = 0; j < GameManager.constants.camSize; j++)
                {
                    toRend[i + 1, j + 1] = WholeMap[i+y,j+x];
                }
            }

            //Add MiniMap
            if (Globals.currentFloor < GameManager.constants.BossFloor)
            {
                for (int i = 0; i < mini.revealedMap.GetLength(0); i++)
                {
                    for (int j = 0; j < mini.revealedMap.GetLength(1); j++)
                    {
                        toRend[i+1, j + GameManager.constants.camSize + 2] = mini.revealedMap[i, j];
                    }
                }
            }

            //Add Hud
            int hudOffset = 0;
            if (GameManager.constants.camSize + 2 >= GameManager.constants.mapHeight)
            {
                hudOffset = GameManager.constants.camSize + 2;
            }else
            {
                hudOffset = GameManager.constants.mapHeight;
            }

            for (int i = 0; i < hud.hudArray.GetLength(0); i++)
            {
                for (int j = 0; j < hud.hudArray.GetLength(1); j++)
                {
                    toRend[i + hudOffset, j] = hud.hudArray[i, j];
                }
            }

            //Print to Screen
            for (int i = 0; i < toRend.GetLength(0); i++)
            {
                for (int j = 0; j < toRend.GetLength(1); j++)
                {
                    if (j < Console.WindowWidth && i < Console.WindowHeight)
                    {
                        Console.SetCursorPosition(j, i);
                        if (toRend[i,j] != prevRend[i,j])
                        {
                            //Console.BackgroundColor = toRend[i, j].backgroundColor;
                            //Console.ForegroundColor = toRend[i, j].foregroundColor;
                            Console.Write(toRend[i, j]);
                        }
                    }

                }
            }

            MatchBuffers();

        }

        public void MatchBuffers()
        {
            for (int i = 0; i < GameManager.constants.rendHeight; i++)
            {
                for (int j = 0; j < GameManager.constants.rendWidth; j++)
                {
                    prevRend[i, j] = toRend[i, j];
                }
            }

            //printToScreenColorsPrev = printToScreenColorsCur;
            //printToScreenCharsPrev = printToScreenCharsCur;
            //printToScreenBackgroundColorsPrev = printToScreenBackgroundColorsCur;
        }

        /*public void ResetBackgrounds()                                      //
        {                                                                   //
            for (int i = 0; i < BackgroundColors.GetLength(1); i++)         //
            {                                                               //
                for (int j = 0; j < BackgroundColors.GetLength(0); j++)     //
                {                                                           //  Resets the background color of every char to black
                    BackgroundColors[j, i] = ConsoleColor.Black;            //
                }                                                           //
            }                                                               //
        }                                                                   //*/

    }
}
