using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Camera
    {
        //public int x;
        //public int y;

        public Position pos;

        private Player player;
        private GameManager gManager;

        public Camera(Player player, GameManager gManager)
        {
            this.player = player;
            this.gManager = gManager;
        }

        public void Update()
        {
            if (Globals.currentFloor < Constants.BossFloor)
            {
                if (player.GetPos().x >= Constants.camSize / 2 && player.GetPos().x < Constants.mapWidth * Constants.roomWidth - (Constants.camSize / 2))
                    pos.x = player.GetPos().x;
                if (player.GetPos().y >= Constants.camSize / 2 && player.GetPos().y < Constants.mapHeight * Constants.roomHeight - (Constants.camSize / 2))
                    pos.y = player.GetPos().y;
            }else if (Globals.currentFloor == Constants.BossFloor)
            {
                if (player.GetPos().x >= Constants.camSize / 2 && player.GetPos().x < Constants.BossRoomWidth - (Constants.camSize / 2))
                    pos.x = player.GetPos().x;
                if (player.GetPos().y >= Constants.camSize / 2 && player.GetPos().y < Constants.BossRoomHeight - (Constants.camSize / 2))
                    pos.y = player.GetPos().y;
            }
        }

    }
}
