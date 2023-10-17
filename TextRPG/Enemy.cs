using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Enemy : GameCharacter
    {
        protected Player player;

        //protected bool moved;

        protected string name;

        protected int sightRange = GameManager.constants.EnemySightRange;

        protected ItemManager itemManager;

        protected Hud hud;

        protected Exit exit;

        protected int XPReward;

        protected int GoldReward;

        public enum Behavior { Random, Flee, Chase }
        public Behavior behavior;

        public bool isFinalBoss;

        public Enemy(Position pos, int HP, int ATK, Tile sprite, string name, Behavior behavior, bool boss, Map map, Player player, EnemyManager enemyManager, ItemManager itemManager, Render rend, GameManager manager, Hud hud, Exit exit, int XPReward, int GoldReward, SoundManager soundManager) : base(pos, HP, ATK, sprite, map, enemyManager, rend, manager, soundManager)
        {
            this.name = name;
            this.player = player;
            this.itemManager = itemManager;
            this.hud = hud;
            this.exit = exit;
            this.XPReward = XPReward;
            this.GoldReward = GoldReward;
            this.behavior = behavior;
            isFinalBoss = boss;
        }

        public void Update()
        {
            if (!alive) return;

            //Attacks player if adjacent
            if (player.isPlayerAt(new Position(pos.x, pos.y - 1)) || player.isPlayerAt(new Position(pos.x, pos.y + 1)) || player.isPlayerAt(new Position(pos.x - 1, pos.y)) || player.isPlayerAt(new Position(pos.x + 1, pos.y)))
            {
                AttackPlayer(player);
            }
            else
            {  //Moves
                switch (behavior)
                {
                    case Behavior.Random:
                        RandomMove();
                        break;
                    case Behavior.Flee:
                        if (!CanSeePlayer())
                        {
                            RandomMove();
                        }
                        else
                        {
                            int deltaX = player.GetPos().x - pos.x;         //
                            int deltaY = player.GetPos().y - pos.y;         //  Prepare to Move
                            targetPos = pos;

                            //---------------------------Chose a direction (run)
                            if (deltaX > 0 && deltaY > 0)
                            {
                                if (deltaX >= deltaY)
                                {
                                    if (IsSpaceAvailable(new Position(pos.x - 1, pos.y)))
                                        targetPos.x--;
                                    else if (IsSpaceAvailable(new Position(pos.x, pos.y - 1)))
                                        targetPos.y--;
                                }
                                else
                                {
                                    if (IsSpaceAvailable(new Position(pos.x, pos.y - 1)))
                                        targetPos.y--;
                                    else if (IsSpaceAvailable(new Position(pos.x - 1, pos.y)))
                                        targetPos.x--;
                                }
                            }
                            else if (deltaX > 0 && deltaY < 0)
                            {
                                if (deltaX >= deltaY * -1)
                                {
                                    if (IsSpaceAvailable(new Position(pos.x - 1, pos.y)))
                                        targetPos.x--;
                                    else if (IsSpaceAvailable(new Position(pos.x, pos.y + 1)))
                                        targetPos.y++;
                                }
                                else
                                {
                                    if (IsSpaceAvailable(new Position(pos.x, pos.y + 1)))
                                        targetPos.y++;
                                    else if (IsSpaceAvailable(new Position(pos.x - 1, pos.y)))
                                        targetPos.x--;
                                }
                            }
                            else if (deltaX < 0 && deltaY > 0)
                            {
                                if (deltaX * -1 >= deltaY)
                                {
                                    if (IsSpaceAvailable(new Position(pos.x + 1, pos.y)))
                                        targetPos.x++;
                                    else if (IsSpaceAvailable(new Position(pos.x, pos.y - 1)))
                                        targetPos.y--;
                                }
                                else
                                {
                                    if (IsSpaceAvailable(new Position(pos.x, pos.y - 1)))
                                        targetPos.y--;
                                    else if (IsSpaceAvailable(new Position(pos.x + 1, pos.y)))
                                        targetPos.x++;
                                }
                            }
                            else if (deltaX < 0 && deltaY < 0)
                            {
                                if (deltaX * -1 >= deltaY * -1)
                                {
                                    if (IsSpaceAvailable(new Position(pos.x + 1, pos.y)))
                                        targetPos.x++;
                                    else if (IsSpaceAvailable(new Position(pos.x, pos.y + 1)))
                                        targetPos.y++;
                                }
                                else
                                {
                                    if (IsSpaceAvailable(new Position(pos.x, pos.y + 1)))
                                        targetPos.y++;
                                    else if (IsSpaceAvailable(new Position(pos.x + 1, pos.y)))
                                        targetPos.x++;
                                }
                            }
                            else if (deltaX == 0)
                            {
                                if (deltaY < 0)
                                {
                                    if (IsSpaceAvailable(new Position(pos.x, pos.y + 1)))
                                        targetPos.y++;
                                    else if (IsSpaceAvailable(new Position(pos.x - 1, pos.y)))
                                        targetPos.x--;
                                    else
                                        targetPos.x++;
                                }
                                else
                                {
                                    if (IsSpaceAvailable(new Position(pos.x, pos.y - 1)))
                                        targetPos.y--;
                                    else if (IsSpaceAvailable(new Position(pos.x - 1, pos.y)))
                                        targetPos.x--;
                                    else
                                        targetPos.x++;
                                }
                            }
                            else if (deltaY == 0)
                            {
                                if (deltaX < 0)
                                {
                                    if (IsSpaceAvailable(new Position(pos.x + 1, pos.y)))
                                        targetPos.x++;
                                    else if (IsSpaceAvailable(new Position(pos.x, pos.y - 1)))
                                        targetPos.y--;
                                    else
                                        targetPos.y++;
                                }
                                else
                                {
                                    if (IsSpaceAvailable(new Position(pos.x - 1, pos.y)))
                                        targetPos.x--;
                                    else if (IsSpaceAvailable(new Position(pos.x, pos.y - 1)))
                                        targetPos.y--;
                                    else
                                        targetPos.y++;
                                }
                            }
                            //---------------------------Direction chosen (run)


                            if (IsSpaceAvailable(targetPos))
                            {
                                pos = targetPos;
                            }
                        }
                        break;
                    case Behavior.Chase:
                        if (!CanSeePlayer())
                        {
                            RandomMove();
                        }
                        else
                        {
                            int deltaX = player.GetPos().x - pos.x;         //
                            int deltaY = player.GetPos().y - pos.y;         //  Prepare to move
                            targetPos = pos;

                            //---------------------------Chose a direction (chase)
                            if (deltaX > 0 && deltaY > 0)
                            {
                                if (deltaX >= deltaY)
                                {
                                    if (IsSpaceAvailable(new Position(pos.x + 1, pos.y)))
                                        targetPos.x++;
                                    else if (IsSpaceAvailable(new Position(pos.x, pos.y + 1)))
                                        targetPos.y++;
                                }
                                else
                                {
                                    if (IsSpaceAvailable(new Position(pos.x, pos.y + 1)))
                                        targetPos.y++;
                                    else if (IsSpaceAvailable(new Position(pos.x + 1, pos.y)))
                                        targetPos.x++;
                                }
                            }
                            else if (deltaX > 0 && deltaY < 0)
                            {
                                if (deltaX >= deltaY * -1)
                                {
                                    if (IsSpaceAvailable(new Position(pos.x + 1, pos.y)))
                                        targetPos.x++;
                                    else if (IsSpaceAvailable(new Position(pos.x, pos.y - 1)))
                                        targetPos.y--;
                                }
                                else
                                {
                                    if (IsSpaceAvailable(new Position(pos.x, pos.y - 1)))
                                        targetPos.y--;
                                    else if (IsSpaceAvailable(new Position(pos.x + 1, pos.y)))
                                        targetPos.x++;
                                }
                            }
                            else if (deltaX < 0 && deltaY > 0)
                            {
                                if (deltaX * -1 >= deltaY)
                                {
                                    if (IsSpaceAvailable(new Position(pos.x - 1, pos.y)))
                                        targetPos.x--;
                                    else if (IsSpaceAvailable(new Position(pos.x, pos.y + 1)))
                                        targetPos.y++;
                                }
                                else
                                {
                                    if (IsSpaceAvailable(new Position(pos.x, pos.y + 1)))
                                        targetPos.y++;
                                    else if (IsSpaceAvailable(new Position(pos.x - 1, pos.y)))
                                        targetPos.x--;
                                }
                            }
                            else if (deltaX < 0 && deltaY < 0)
                            {
                                if (deltaX * -1 >= deltaY * -1)
                                {
                                    if (IsSpaceAvailable(new Position(pos.x - 1, pos.y)))
                                        targetPos.x--;
                                    else if (IsSpaceAvailable(new Position(pos.x, pos.y - 1)))
                                        targetPos.y--;
                                }
                                else
                                {
                                    if (IsSpaceAvailable(new Position(pos.x, pos.y - 1)))
                                        targetPos.y--;
                                    else if (IsSpaceAvailable(new Position(pos.x - 1, pos.y)))
                                        targetPos.x--;
                                }
                            }
                            else if (deltaX == 0)
                            {
                                if (deltaY < 0)
                                {
                                    if (IsSpaceAvailable(new Position(pos.x, pos.y - 1)))
                                        targetPos.y--;
                                    else if (IsSpaceAvailable(new Position(pos.x + 1, pos.y)))
                                        targetPos.x++;
                                    else
                                        targetPos.x--;
                                }
                                else
                                {
                                    if (IsSpaceAvailable(new Position(pos.x, pos.y + 1)))
                                        targetPos.y++;
                                    else if (IsSpaceAvailable(new Position(pos.x + 1, pos.y)))
                                        targetPos.x++;
                                    else
                                        targetPos.x--;
                                }
                            }
                            else if (deltaY == 0)
                            {
                                if (deltaX < 0)
                                {
                                    if (IsSpaceAvailable(new Position(pos.x - 1, pos.y)))
                                        targetPos.x--;
                                    else if (IsSpaceAvailable(new Position(pos.x, pos.y + 1)))
                                        targetPos.y++;
                                    else
                                        targetPos.y--;
                                }
                                else
                                {
                                    if (IsSpaceAvailable(new Position(pos.x + 1, pos.y)))
                                        targetPos.x++;
                                    else if (IsSpaceAvailable(new Position(pos.x, pos.y + 1)))
                                        targetPos.y++;
                                    else
                                        targetPos.y--;
                                }
                            }
                            //---------------------------Direction Chosen (chase)

                            if (IsSpaceAvailable(targetPos))
                            {
                                pos = targetPos;
                            }
                        }
                        break;
                }
            }
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
            if(IsSpaceAvailable(targetPos))
            {
                pos = targetPos;
            }
        }

        public override void TakeDMG(int DMG)
        {
            base.TakeDMG(DMG);
            if (HP <= 0)
            {
                enemyManager.RemoveEnemy(this);
                player.giveXP(XPReward);
                player.giveGold(GoldReward);
                if(isFinalBoss)
                {
                    manager.EndGame(true);
                }
            }
        }

        public void AttackPlayer(Player target)
        {
            target.TakeDMG(ATK);
            if (target.GetHealth() <= 0) hud.SetMessage(name + " killed Player");
            else if (hud.GetMessage() == " ") hud.SetMessage(name + " attacked Player");
            else hud.SetMessage("Player and " + name + " both attacked");
        }

        public bool CanSeePlayer()
        {
            bool check = false;

            int a2 = (player.GetPos().x - pos.x);
            a2 = a2 * a2;

            int b2 = (player.GetPos().y - pos.y);
            b2 = b2 * b2;

            int c = (int)Math.Sqrt(a2+b2);

            if (c <= sightRange)
            {
                check = true;
            }

            return check;
        }

        public int GetXP()
        {
            return XPReward;
        }

        public int GetGold()
        {
            return GoldReward;
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
            if(exit.isExitAt(pos, false))
                available = false;

            return available;

        }

        public string GetName()
        {
            return name;
        }

        public static Behavior GetBehavior(string input)
        {
            string behaviorString = input.Split('^')[1];
            switch (behaviorString)
            {
                case "Random":
                    return Behavior.Random;
                case "Flee":
                    return Behavior.Flee;
                case "Chase":
                    return Behavior.Chase;
                default:
                    return Behavior.Random;
            }
        }

    }
}
