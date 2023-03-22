using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Exit
    {

        int x;
        int y;
        char sprite = Constants.exitSprite;
        ConsoleColor color = Constants.exitColor;
        GameManager manager;
        Render rend;
        Map map;
        bool toShow = false;

        public Exit(GameManager manager, Render rend, Map map)
        {
            this.manager = manager;
            this.rend = rend;
            this.map = map;            
        }

        public void PlaceExit(Player player)
        {
            Random rand = new Random();
            bool placed = false;
            toShow = true;

            while (placed == false)
            {
                //Console.WriteLine("Attempt(Exit)");
                x = rand.Next(Constants.mapWidth * Constants.roomWidth);
                y = rand.Next(Constants.mapHeight * Constants.roomHeight);
                if (Math.Abs(player.GetX() - x) > (Constants.mapWidth * Constants.roomWidth) / 4 || Math.Abs(player.GetY() - y) > (Constants.mapHeight * Constants.roomHeight) / 4)
                {
                    if(map.isFloorAt(x, y))
                        placed = true;
                }
            }
        }

        public void Draw()
        {
            if (toShow)
            {
                rend.ScreenChars[y, x] = sprite;
                rend.ScreenColors[y, x] = color;
            }
        }

        public bool isExitAt(int x, int y, bool win)
        {
            if(x == this.x && y == this.y)
            {
                if(win)
                    manager.NextFloor();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
