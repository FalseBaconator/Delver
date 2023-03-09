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

        public Camera(Player player)
        {
            this.player = player;
        }

        public void Update()
        {
            if (player.GetX() >= Constants.roomWidth / 2 && player.GetX() < Constants.mapWidth * Constants.roomWidth - (Constants.roomWidth / 2))
                x = player.GetX();
            if (player.GetY() >= Constants.roomHeight / 2 && player.GetY() < Constants.mapHeight * Constants.roomHeight - (Constants.roomHeight / 2))
                y = player.GetY();
        }

    }
}
