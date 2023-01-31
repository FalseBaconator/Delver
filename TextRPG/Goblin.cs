using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Goblin : Enemy
    {

        public Goblin(int x, int y, Map map, Player player, EnemyManager enemyManager) : base(x, y, 5, 2, 'X', map, ConsoleColor.DarkGreen, player, enemyManager)
        {

        }

    }
}
