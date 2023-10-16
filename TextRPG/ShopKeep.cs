using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class ShopKeep : GameCharacter
    {
        protected Player player;

        protected string name;

        protected ItemManager itemManager;

        protected Hud hud;

        protected Exit exit;

        protected ShopKeepManager shopKeepManager;

        public ShopKeep(Position pos, Map map, Player player, EnemyManager enemyManager, ItemManager itemManager, Render rend, GameManager manager, Hud hud, Exit exit,  SoundManager soundManager, ShopKeepManager shopKeepManager) : base(pos, GameManager.constants.shopKeepBaseHP, GameManager.constants.shopKeepBaseAttack, GameManager.constants.shopKeepSprite, map, enemyManager, rend, manager, soundManager)
        {
            this.name = name;
            this.player = player;
            this.itemManager = itemManager;
            this.hud = hud;
            this.exit = exit;
            this.shopKeepManager = shopKeepManager;
        }

        protected void RandomMove()
        {
            targetPos = pos;
            bool moved = false;
            int checks = 0;

            int dir = GameManager.constants.rand.Next(4);

            while (moved == false)
            {
                checks++;
                if (checks >= 4)
                    moved = true;
                switch (dir)
                {
                    case 0:
                        if (IsSpaceAvailable(new Position(targetPos.x, targetPos.y - 1)))
                        {
                            targetPos.y--;
                            moved = true;
                        }
                        else
                            dir++;
                        break;
                    case 1:
                        if (IsSpaceAvailable(new Position(targetPos.x, targetPos.y + 1)))
                        {
                            targetPos.y++;
                            moved = true;
                        }
                        else
                            dir++;
                        break;
                    case 2:
                        if (IsSpaceAvailable(new Position(targetPos.x - 1, targetPos.y)))
                        {
                            targetPos.x--;
                            moved = true;
                        }
                        else
                            dir++;
                        break;
                    case 3:
                        if (IsSpaceAvailable(new Position(targetPos.x + 1, targetPos.y)))
                        {
                            targetPos.x++;
                            moved = true;
                        }
                        else
                            dir = 0;
                        break;
                }
            }
            if (IsSpaceAvailable(targetPos))
            {
                pos = targetPos;
            }
        }

        public bool IsSpaceAvailable(Position pos)
        {
            bool available = true;
            if (map.isFloorAt(pos) == false)
                available = false;
            if (enemyManager.EnemyAt(pos, false) != null)
                available = false;
            if (itemManager.ItemAt(pos) != null)
                available = false;
            if (player.isPlayerAt(pos))
                available = false;
            if (exit.isExitAt(pos, false))
                available = false;

            return available;
        }

        public void Update()
        {
            if (alive)
            {

                if (player.isPlayerAt(new Position(pos.x, pos.y - 1)) || player.isPlayerAt(new Position(pos.x, pos.y + 1)) || player.isPlayerAt(new Position(pos.x - 1, pos.y)) || player.isPlayerAt(new Position(pos.x + 1, pos.y)))       //
                {                                                                                                                                   //
                    //AttackPlayer(player);                                                                                                           //  Enemy uses turn to attack player if they're adjacent
                }                                                                                                                                   //
                else                //
                {                   //  Move in a random direction if hasn't attacked
                    RandomMove();   //
                }                   //
            }
        }
    }
}