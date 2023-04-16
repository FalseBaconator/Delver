using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Exit
    {

        Position pos;
        Tile sprite = Constants.exitSprite;
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
            Random rand = Constants.rand;
            bool placed = false;
            toShow = true;

            while (placed == false)
            {
                //Console.WriteLine("Attempt(Exit)");
                pos = new Position(rand.Next(Constants.mapWidth * Constants.roomWidth), rand.Next(Constants.mapHeight * Constants.roomHeight));
                if (Math.Abs(player.GetPos().x - pos.x) > (Constants.mapWidth * Constants.roomWidth) / 4 || Math.Abs(player.GetPos().y - pos.y) > (Constants.mapHeight * Constants.roomHeight) / 4)
                {
                    if(map.isFloorAt(pos))
                        placed = true;
                }
            }
        }

        public void hide()
        {
            pos = new Position(Constants.mapHeight * Constants.roomHeight - 1, Constants.mapWidth * Constants.roomWidth - 1);
        }

        public void Draw()
        {
            if (toShow)
            {
                rend.WholeMap[pos.y, pos.x] = sprite;
            }
        }

        public bool isExitAt(Position pos, bool win)
        {
            if(pos == this.pos)
            {
                if(win)
                    manager.loadManager.NextFloor();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
