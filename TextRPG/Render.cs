using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Render
    {

        public char[,] ScreenChars = new char[Constants.mapHeight * Constants.roomHeight, Constants.mapWidth * Constants.roomWidth];

        public ConsoleColor[,] ScreenColors = new ConsoleColor[Constants.mapHeight * Constants.roomHeight, Constants.mapWidth * Constants.roomWidth];

        public ConsoleColor[,] BackgroundColors = new ConsoleColor[Constants.mapHeight * Constants.roomHeight, Constants.mapWidth * Constants.roomWidth];

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

        private char[,] printToScreenCharsCur = new char[Constants.rendHeight, Constants.rendWidth];
        private ConsoleColor[,] printToScreenColorsCur = new ConsoleColor[Constants.rendHeight, Constants.rendWidth];
        private ConsoleColor[,] printToScreenBackgroundColorsCur = new ConsoleColor[Constants.rendHeight, Constants.rendWidth];

        private char[,] printToScreenCharsPrev = new char[Constants.rendHeight, Constants.rendWidth];
        private ConsoleColor[,] printToScreenColorsPrev = new ConsoleColor[Constants.rendHeight, Constants.rendWidth];
        private ConsoleColor[,] printToScreenBackgroundColorsPrev = new ConsoleColor[Constants.rendHeight, Constants.rendWidth];

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
            int x = cam.x - (Constants.camSize / 2);
            int y = cam.y - (Constants.camSize / 2);

            //fill with space
            for (int i = 0; i < printToScreenCharsCur.GetLength(0); i++)
            {
                for (int j = 0; j < printToScreenCharsCur.GetLength(1); j++)
                {
                    printToScreenCharsCur[i, j] = ' ';
                    printToScreenColorsCur[i, j] = ConsoleColor.White;
                    printToScreenBackgroundColorsCur[i, j] = ConsoleColor.Black;
                }
            }

            //Add Border
            for (int i = 0; i < Constants.camSize + 2; i++)
            {
                for (int j = 0; j < Constants.camSize + 2; j++)
                {
                    if (i == 0)
                    {
                        if (j == 0)
                            printToScreenCharsCur[i, j] = '╔';
                        else if (j == Constants.camSize + 1)
                            printToScreenCharsCur[i, j] = '╗';
                        else
                            printToScreenCharsCur[i, j] = '═';
                    }else if (i == Constants.camSize + 1)
                    {
                        if (j == 0)
                            printToScreenCharsCur[i, j] = '╚';
                        else if (j == Constants.camSize + 1)
                            printToScreenCharsCur[i, j] = '╝';
                        else
                            printToScreenCharsCur[i, j] = '═';
                    }
                    else
                    {
                        if (j == 0 || j == Constants.camSize + 1)
                            printToScreenCharsCur[i, j] = '║';
                        else
                            printToScreenCharsCur[i, j] = ' ';
                    }
                    //printToScreenChars[i,j] = borderChars[i,j];
                    printToScreenColorsCur[i, j] = ConsoleColor.White;
                    printToScreenBackgroundColorsCur[i, j] = ConsoleColor.Black;
                }
            }

            //Add Camera
            for (int i = 0; i < Constants.camSize; i++)
            {
                for (int j = 0; j < Constants.camSize; j++)
                {

                    printToScreenCharsCur[i + 1, j + 1] = ScreenChars[i+y,j+x];
                    printToScreenColorsCur[i + 1, j + 1] = ScreenColors[i + y, j + x];
                    printToScreenBackgroundColorsCur[i + 1, j + 1] = BackgroundColors[i + y, j + x];

                }
            }

            //Add MiniMap
            if (gManager.getFloor() < Constants.BossFloor)
            {
                for (int i = 0; i < Constants.mapHeight; i++)
                {
                    for (int j = 0; j < Constants.mapWidth; j++)
                    {
                        printToScreenCharsCur[i, j + Constants.camSize + 2] = mini.revealedMap[i, j];
                        printToScreenColorsCur[i, j + Constants.camSize + 2] = mini.foregroundColors[i, j];
                        printToScreenBackgroundColorsCur[i, j + Constants.camSize + 2] = mini.backgroundColors[i, j];
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
                    printToScreenCharsCur[i + hudOffset, j] = hud.hudArray[i, j];
                    printToScreenColorsCur[i + hudOffset, j] = ConsoleColor.White;
                    printToScreenBackgroundColorsCur[i + hudOffset, j] = ConsoleColor.Black;
                }
            }

            //Print to Screen
            for (int i = 0; i < printToScreenCharsCur.GetLength(0); i++)
            {
                for (int j = 0; j < printToScreenCharsCur.GetLength(1); j++)
                {
                    if (j < Console.WindowWidth && i < Console.WindowHeight)
                    {
                        Console.SetCursorPosition(j, i);
                        if (printToScreenBackgroundColorsCur[i, j] != printToScreenBackgroundColorsPrev[i, j] || printToScreenCharsCur[i, j] != printToScreenCharsPrev[i, j] || printToScreenColorsCur[i,j] != printToScreenColorsPrev[i, j])
                        {
                            Console.BackgroundColor = printToScreenBackgroundColorsCur[i, j];
                            Console.ForegroundColor = printToScreenColorsCur[i, j];
                            Console.Write(printToScreenCharsCur[i, j]);
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
                    printToScreenBackgroundColorsPrev[i, j] = printToScreenBackgroundColorsCur[i, j];
                    printToScreenColorsPrev[i, j] = printToScreenColorsCur[i, j];
                    printToScreenCharsPrev[i, j] = printToScreenCharsCur[i, j];
                }
            }

            //printToScreenColorsPrev = printToScreenColorsCur;
            //printToScreenCharsPrev = printToScreenCharsCur;
            //printToScreenBackgroundColorsPrev = printToScreenBackgroundColorsCur;
        }

        public void ResetBackgrounds()                                      //
        {                                                                   //
            for (int i = 0; i < BackgroundColors.GetLength(1); i++)         //
            {                                                               //
                for (int j = 0; j < BackgroundColors.GetLength(0); j++)     //
                {                                                           //  Resets the background color of every char to black
                    BackgroundColors[j, i] = ConsoleColor.Black;            //
                }                                                           //
            }                                                               //
        }                                                                   //

    }
}
