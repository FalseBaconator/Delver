using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Slime : Enemy
    {

        public Slime(int x, int y, Map map, Player player, EnemyManager enemyManager) : base(x, y, 1, 1, 'O', map, ConsoleColor.Cyan, player, enemyManager)
        {

        }

    }
}
