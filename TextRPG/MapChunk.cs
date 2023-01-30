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

        public MapChunk(char[,] tile)
        {
            this.tile = tile;
        }
    }
}
