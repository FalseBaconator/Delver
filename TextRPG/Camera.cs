using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Camera
    {
        public int x;
        public int y;

        private Player player;
        private GameManager gManager;

        public Camera(Player player, GameManager gManager)
        {
            this.player = player;
            this.gManager = gManager;
        }

        public void Update()
        {
            if (gManager.getFloor() < Constants.BossFloor)
            {
                if (player.GetX() >= Constants.camSize / 2 && player.GetX() < Constants.mapWidth * Constants.roomWidth - (Constants.camSize / 2))
                    x = player.GetX();
                if (player.GetY() >= Constants.camSize / 2 && player.GetY() < Constants.mapHeight * Constants.roomHeight - (Constants.camSize / 2))
                    y = player.GetY();
            }else if (gManager.getFloor() == Constants.BossFloor)
            {
                if (player.GetX() >= Constants.camSize / 2 && player.GetX() < Constants.BossRoomWidth - (Constants.camSize / 2))
                    x = player.GetX();
                if (player.GetY() >= Constants.camSize / 2 && player.GetY() < Constants.BossRoomHeight - (Constants.camSize / 2))
                    y = player.GetY();
            }
        }

    }
}
