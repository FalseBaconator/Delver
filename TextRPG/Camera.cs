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
        //private GameManager gManager;

        public Camera(Player player, GameManager gManager)
        {
            this.player = player;
            //this.gManager = gManager;
        }

        public void Update()
        {
            if (Globals.currentFloor < GameManager.constants.BossFloor)
            {
                if (player.GetPos().x >= GameManager.constants.camSize / 2 && player.GetPos().x < GameManager.constants.mapWidth * GameManager.constants.roomWidth - (GameManager.constants.camSize / 2))
                    pos.x = player.GetPos().x;
                if (player.GetPos().y >= GameManager.constants.camSize / 2 && player.GetPos().y < GameManager.constants.mapHeight * GameManager.constants.roomHeight - (GameManager.constants.camSize / 2))
                    pos.y = player.GetPos().y;
            }else if (Globals.currentFloor == GameManager.constants.BossFloor)
            {
                if (player.GetPos().x >= GameManager.constants.camSize / 2 && player.GetPos().x < GameManager.constants.BossRoomWidth - (GameManager.constants.camSize / 2))
                    pos.x = player.GetPos().x;
                if (player.GetPos().y >= GameManager.constants.camSize / 2 && player.GetPos().y < GameManager.constants.BossRoomHeight - (GameManager.constants.camSize / 2))
                    pos.y = player.GetPos().y;
            }
        }

    }
}
