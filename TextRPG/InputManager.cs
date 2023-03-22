using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class InputManager
    {
        private ConsoleKey key;

        private GameManager manager;


        public InputManager(GameManager manager)
        {
            this.manager = manager;
        }


        public void Update()    //Gets a key
        {
            ClearInputBuffer();

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.Escape)
            {
                manager.EndGame(false);
            }
        }

        public ConsoleKey GetKey()  //Gets the saved key
        {
            return key;
        }

        public void ClearInputBuffer()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }
    }
}
