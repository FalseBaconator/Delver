using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class StartScreen
    {

        public void Display()
        {
            Console.Clear();
            Console.WriteLine(" ▓▓▓   ▓▓▓▓▓ ▓     ▓   ▓  ▓▓▓▓▓ ▓▓▓    ▓ ");
            Console.WriteLine(" ▓  ▓  ▓     ▓     ▓   ▓  ▓     ▓  ▓   ▓ ");
            Console.WriteLine(" ▓   ▓ ▓▓▓   ▓      ▓ ▓   ▓▓▓   ▓▓▓    ▓ ");
            Console.WriteLine(" ▓  ▓  ▓     ▓      ▓ ▓   ▓     ▓  ▓     ");
            Console.WriteLine(" ▓▓▓   ▓▓▓▓▓ ▓▓▓▓▓   ▓    ▓▓▓▓▓ ▓   ▓  ▓ ");
            Console.WriteLine("─────────────────────────────────────────");
            Console.WriteLine("        Press Any Key To Continue        ");
            Console.ReadKey(true);
            Console.Clear();

        }


    }
}
