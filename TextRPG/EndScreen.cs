using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class EndScreen
    {
        public enum EndCon
        {
            Win,
            Lose
        }

        public void Display(EndCon endCon)
        {
            switch (endCon)
            {
                case EndCon.Win:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Clear();
                    Console.WriteLine(" ▓   ▓  ▓▓▓  ▓   ▓  ▓   ▓  ▓▓▓▓▓ ▓   ▓  ▓ ");
                    Console.WriteLine("  ▓ ▓  ▓   ▓ ▓   ▓  ▓   ▓    ▓   ▓▓  ▓  ▓ ");
                    Console.WriteLine("   ▓   ▓   ▓ ▓   ▓  ▓ ▓ ▓    ▓   ▓ ▓ ▓  ▓ ");
                    Console.WriteLine("   ▓   ▓   ▓ ▓   ▓  ▓ ▓ ▓    ▓   ▓  ▓▓    ");
                    Console.WriteLine("   ▓    ▓▓▓   ▓▓▓    ▓ ▓   ▓▓▓▓▓ ▓   ▓  ▓ ");
                    Console.WriteLine("─────────────────────────────────────────");
                    Console.WriteLine("        Press Any Key To Continue        ");
                    Console.ReadKey(true);
                    Console.Clear();
                    break;
                case EndCon.Lose:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Clear();
                    Console.WriteLine(" ▓▓▓▓▓  ▓▓▓  ▓▓▓   ▓▓▓▓▓  ▓▓▓▓▓  ▓▓▓▓  ▓ ");
                    Console.WriteLine(" ▓ ▓ ▓ ▓   ▓ ▓  ▓    ▓      ▓   ▓      ▓ ");
                    Console.WriteLine(" ▓ ▓ ▓ ▓   ▓ ▓▓▓     ▓      ▓    ▓▓▓   ▓ ");
                    Console.WriteLine(" ▓ ▓ ▓ ▓   ▓ ▓  ▓    ▓      ▓       ▓    ");
                    Console.WriteLine(" ▓ ▓ ▓  ▓▓▓  ▓   ▓   ▓    ▓▓▓▓▓ ▓▓▓▓   ▓ ");
                    Console.WriteLine("─────────────────────────────────────────");
                    Console.WriteLine("        Press Any Key To Continue        ");
                    Console.ReadKey(true);
                    Console.Clear();
                    break;
            }

        }
    }
}
