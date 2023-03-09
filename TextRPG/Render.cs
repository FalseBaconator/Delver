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

        private Camera cam;

        private char[,] borderChars = new char[,]
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
        };

        private char[,] printToScreenChars;
        //private char[,] printToScreenColors;


        public void setCam(Camera camera)
        {
            cam = camera;
        }

        public void DrawToScreen()  //Draws the map according to the arrays
        {
            Console.CursorVisible = false;
            int x = cam.x - (Constants.camSize / 2);
            int y = cam.y - (Constants.camSize / 2);
            printToScreenChars = borderChars;
            for (int i = 0; i < Constants.camSize; i++)
            {
                for (int j = 0; j < Constants.camSize; j++)
                {
                    //Console.SetCursorPosition(j+1, i+1);                    //
                    //Console.BackgroundColor = BackgroundColors[i+y, j+x];       //  Writes the char with the right background, color, and sprite
                    //Console.ForegroundColor = ScreenColors[i+y, j+x];           //
                    //Console.Write(ScreenChars[i+y, j+x]);                       //

                    printToScreenChars[i + 1, j + 1] = ScreenChars[i+y,j+x];

                }
            }

            for (int i = 0; i <= Constants.camSize + 1; i++)
            {
                for (int j = 0; j <= Constants.camSize + 1; j++)
                {
                    Console.SetCursorPosition(j, i);
                    if(i > 0 && i < Constants.camSize + 1 && j > 0 && j < Constants.camSize + 1)
                    {
                        Console.BackgroundColor = BackgroundColors[i + y - 1, j + x - 1];
                        Console.ForegroundColor = ScreenColors[i + y - 1, j + x - 1];
                    }
                    else
                    {
                        Console.ResetColor();
                    }
                    Console.Write(printToScreenChars[i,j]);

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
