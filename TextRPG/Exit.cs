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
        Tile sprite = GameManager.constants.exitSprite;
        GameManager manager;
        Render rend;
        Map map;
        Hud hud;
        bool toShow = false;

        public Exit(GameManager manager, Render rend, Map map)
        {
            this.manager = manager;
            this.rend = rend;
            this.map = map;            
        }

        public void PlaceExit(Player player)
        {
            Random rand = GameManager.constants.rand;
            bool placed = false;
            toShow = true;

            while (placed == false)
            {
                //Console.WriteLine("Attempt(Exit)");
                pos = new Position(rand.Next(GameManager.constants.mapWidth * GameManager.constants.roomWidth), rand.Next(GameManager.constants.mapHeight * GameManager.constants.roomHeight));
                if (Math.Abs(player.GetPos().x - pos.x) > (GameManager.constants.mapWidth * GameManager.constants.roomWidth) / 4 || Math.Abs(player.GetPos().y - pos.y) > (GameManager.constants.mapHeight * GameManager.constants.roomHeight) / 4)
                {
                    if(map.isFloorAt(pos))
                        placed = true;
                }
            }
        }

        public void hide()
        {
            pos = new Position(GameManager.constants.mapHeight * GameManager.constants.roomHeight - 1, GameManager.constants.mapWidth * GameManager.constants.roomWidth - 1);
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
                {
                    if (!Globals.questCompleted)
                    {
                        hud.SetMessage("Complete quest: " + Globals.questString);
                        return true;
                    }
                    manager.loadManager.NextFloor();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetHud(Hud hud)
        {
            this.hud = hud;
        }
    }
}
