using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Render
    {

        //public char[,] ScreenChars = new char[Constants.mapHeight * Constants.roomHeight, Constants.mapWidth * Constants.roomWidth];

        //public ConsoleColor[,] ScreenColors = new ConsoleColor[Constants.mapHeight * Constants.roomHeight, Constants.mapWidth * Constants.roomWidth];

        //public ConsoleColor[,] BackgroundColors = new ConsoleColor[Constants.mapHeight * Constants.roomHeight, Constants.mapWidth * Constants.roomWidth];

        public Tile[,] WholeMap = new Tile[Constants.mapHeight * Constants.roomHeight, Constants.mapWidth * Constants.roomWidth];

        private Hud hud;

        private Camera cam;

        //private char[,] borderChars = new char[Constants.camSize + 1, Constants.camSize + 1];
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

        //private char[,] printToScreenCharsCur = new char[Constants.rendHeight, Constants.rendWidth];
        //private ConsoleColor[,] printToScreenColorsCur = new ConsoleColor[Constants.rendHeight, Constants.rendWidth];
        //private ConsoleColor[,] printToScreenBackgroundColorsCur = new ConsoleColor[Constants.rendHeight, Constants.rendWidth];

        private Tile[,] toRend = new Tile[Constants.rendHeight, Constants.rendWidth];

        //private char[,] printToScreenCharsPrev = new char[Constants.rendHeight, Constants.rendWidth];
        //private ConsoleColor[,] printToScreenColorsPrev = new ConsoleColor[Constants.rendHeight, Constants.rendWidth];
        //private ConsoleColor[,] printToScreenBackgroundColorsPrev = new ConsoleColor[Constants.rendHeight, Constants.rendWidth];

        private Tile[,] prevRend = new Tile[Constants.rendHeight, Constants.rendWidth];

        //[Constants.camSize + 2, Constants.mapWidth + Constants.camSize + 2]
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
            int x = cam.pos.x - (Constants.camSize / 2);
            int y = cam.pos.y - (Constants.camSize / 2);

            //fill with space
            for (int i = 0; i < toRend.GetLength(0); i++)
            {
                for (int j = 0; j < toRend.GetLength(1); j++)
                {
                    toRend[i, j] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                }
            }

            //Add Border
            for (int i = 0; i < Constants.camSize + 2; i++)
            {
                for (int j = 0; j < Constants.hudWidth + 1; j++)
                {
                    switch (i)
                    {
                        case 0:
                            switch (j)
                            {
                                case 0:
                                    toRend[i, j] = new Tile('╔', Constants.borderColor, Constants.BGColor);
                                    break;
                                case Constants.hudWidth / 2:
                                    toRend[i, j] = new Tile('╦', Constants.borderColor, Constants.BGColor);
                                    break;
                                case Constants.hudWidth:
                                    toRend[i, j] = new Tile('╗', Constants.borderColor, Constants.BGColor);
                                    break;
                                default:
                                    toRend[i, j] = new Tile('═', Constants.borderColor, Constants.BGColor);
                                    break;
                            }
                            break;
                        case Constants.camSize + 1:
                            switch (j)
                            {
                                case 0:
                                    toRend[i, j] = new Tile('╚', Constants.borderColor, Constants.BGColor);
                                    break;
                                case Constants.hudWidth / 2:
                                    toRend[i, j] = new Tile('╩', Constants.borderColor, Constants.BGColor);
                                    break;
                                case Constants.hudWidth:
                                    toRend[i, j] = new Tile('╝', Constants.borderColor, Constants.BGColor);
                                    break;
                                default:
                                    toRend[i, j] = new Tile('═', Constants.borderColor, Constants.BGColor);
                                    break;
                            }
                            break;
                        default:
                            switch (j)
                            {
                                case 0:
                                case Constants.hudWidth / 2:
                                case Constants.hudWidth:
                                    toRend[i, j] = new Tile('║', Constants.borderColor, Constants.BGColor);
                                    break;
                                default:
                                    toRend[i, j] = new Tile(' ', Constants.borderColor, Constants.BGColor);
                                    break;
                            }
                            break;
                    }
                }
            }

            //Add Camera
            for (int i = 0; i < Constants.camSize; i++)
            {
                for (int j = 0; j < Constants.camSize; j++)
                {
                    toRend[i + 1, j + 1] = WholeMap[i+y,j+x];
                }
            }

            //Add MiniMap
            if (Globals.currentFloor < Constants.BossFloor)
            {
                for (int i = 0; i < mini.revealedMap.GetLength(0); i++)
                {
                    for (int j = 0; j < mini.revealedMap.GetLength(1); j++)
                    {
                        toRend[i+1, j + Constants.camSize + 2] = mini.revealedMap[i, j];
                    }
                }
            }

            //Add Hud
            int hudOffset = 0;
            if (Constants.camSize + 2 >= Constants.mapHeight)
            {
                hudOffset = Constants.camSize + 2;
            }else
            {
                hudOffset = Constants.mapHeight;
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
            for (int i = 0; i < Constants.rendHeight; i++)
            {
                for (int j = 0; j < Constants.rendWidth; j++)
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
