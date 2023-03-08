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

        public void DrawToScreen()  //Draws the map according to the arrays
        {
            Console.CursorVisible = false;
            for (int i = 0; i < ScreenChars.GetLength(0); i++)
            {
                for (int j = 0; j < ScreenChars.GetLength(1); j++)
                {
                    Console.SetCursorPosition(j, i);                    //
                    Console.BackgroundColor = BackgroundColors[i, j];   //  Writes the char with the right background, color, and sprite
                    Console.ForegroundColor = ScreenColors[i, j];       //
                    Console.Write(ScreenChars[i, j]);                   //
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
