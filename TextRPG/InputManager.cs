using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class InputManager
    {
        ConsoleKey key;

        //Program main;

        public GameManager manager;

        Player player;

        public InputManager(Player player)
        {
            this.player = player;
            player.inputManager = this;
        }


        public void Update()
        {
            key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Escape)
            {
                manager.play = false;
            }
        }

        public ConsoleKey GetKey()
        {
            return key;
        }
    }
}
