using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Render
    {

        public char[,] ScreenChars = new char[35, 35];

        public ConsoleColor[,] ScreenColors = new ConsoleColor[35, 35];

        public void DrawToScreen()
        {
            Console.CursorVisible = false;
            for (int i = 0; i < ScreenChars.GetLength(0); i++)
            {
                for (int j = 0; j < ScreenChars.GetLength(1); j++)
                {
                    Console.SetCursorPosition(i, j);
                    Console.ForegroundColor = ScreenColors[i, j];
                    Console.Write(ScreenChars[i, j]);
                }
            }
        }

    }
}
