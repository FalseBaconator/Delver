using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class MapChunk
    {
        public char[,] tile;

        public bool BOpen = false;  //doors. true if open
        public bool LOpen = false;
        public bool ROpen = false;
        public bool TOpen = false;

        public MapChunk(char[,] tile)
        {
            this.tile = tile;
        }


    }
}
