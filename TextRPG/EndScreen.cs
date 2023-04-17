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

        private SoundManager soundManager;

        public EndScreen(SoundManager soundManager)
        {
            this.soundManager = soundManager;
        }

        public void Display(EndCon endCon)
        {
            switch (endCon)
            {
                case EndCon.Win:
                    soundManager.Play(SoundManager.Noise.win);
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
                    soundManager.Play(SoundManager.Noise.lose);
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
