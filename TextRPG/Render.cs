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

        private char[,] printToScreenChars = new char[Constants.rendHeight, Constants.rendWidth];
        private ConsoleColor[,] printToScreenColors = new ConsoleColor[Constants.rendHeight, Constants.rendWidth];
        private ConsoleColor[,] printToScreenBackgroundColors = new ConsoleColor[Constants.rendHeight, Constants.rendWidth];
        //[Constants.camSize + 2, Constants.mapWidth + Constants.camSize + 2]
        private MiniMap mini;


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
            for (int i = 0; i < printToScreenChars.GetLength(0); i++)
            {
                for (int j = 0; j < printToScreenChars.GetLength(1); j++)
                {
                    printToScreenChars[i, j] = ' ';
                    printToScreenColors[i, j] = ConsoleColor.White;
                    printToScreenBackgroundColors[i, j] = ConsoleColor.Black;
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
                            printToScreenChars[i, j] = '╔';
                        else if (j == Constants.camSize + 1)
                            printToScreenChars[i, j] = '╗';
                        else
                            printToScreenChars[i, j] = '═';
                    }else if (i == Constants.camSize + 1)
                    {
                        if (j == 0)
                            printToScreenChars[i, j] = '╚';
                        else if (j == Constants.camSize + 1)
                            printToScreenChars[i, j] = '╝';
                        else
                            printToScreenChars[i, j] = '═';
                    }
                    else
                    {
                        if (j == 0 || j == Constants.camSize + 1)
                            printToScreenChars[i, j] = '║';
                        else
                            printToScreenChars[i, j] = ' ';
                    }
                    //printToScreenChars[i,j] = borderChars[i,j];
                    printToScreenColors[i, j] = ConsoleColor.White;
                    printToScreenBackgroundColors[i, j] = ConsoleColor.Black;
                }
            }

            //Add Camera
            for (int i = 0; i < Constants.camSize; i++)
            {
                for (int j = 0; j < Constants.camSize; j++)
                {

                    printToScreenChars[i + 1, j + 1] = ScreenChars[i+y,j+x];
                    printToScreenColors[i + 1, j + 1] = ScreenColors[i + y, j + x];
                    printToScreenBackgroundColors[i + 1, j + 1] = BackgroundColors[i + y, j + x];

                }
            }

            //Add MiniMap
            for (int i = 0; i < Constants.mapHeight; i++)
            {
                for (int j = 0; j < Constants.mapWidth; j++)
                {
                    printToScreenChars[i, j + Constants.camSize + 2] = mini.revealedMap[i, j];
                    printToScreenColors[i, j + Constants.camSize + 2] = mini.foregroundColors[i, j];
                    printToScreenBackgroundColors[i, j + Constants.camSize + 2] = mini.backgroundColors[i, j];
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
                    printToScreenChars[i + hudOffset, j] = hud.hudArray[i, j];
                    printToScreenColors[i + hudOffset, j] = ConsoleColor.White;
                    printToScreenBackgroundColors[i + hudOffset, j] = ConsoleColor.Black;
                }
            }

            //Print to Screen
            for (int i = 0; i < printToScreenChars.GetLength(0); i++)
            {
                for (int j = 0; j < printToScreenChars.GetLength(1); j++)
                {
                    if (j < Console.WindowWidth && i < Console.WindowHeight)
                    {
                        Console.SetCursorPosition(j, i);
                        Console.BackgroundColor = printToScreenBackgroundColors[i, j];
                        Console.ForegroundColor = printToScreenColors[i, j];
                        Console.Write(printToScreenChars[i, j]);
                    }

                }
            }

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
