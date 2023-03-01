using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Program
    {

        static GameManager manager = new GameManager();
        
        static void Main(string[] args)
        {
            manager.Play();
        }
    }
}
