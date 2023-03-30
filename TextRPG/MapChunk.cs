using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class MapChunk
    {
        public char[,] roomMap;

        public bool BOpen = false;  //doors. true if open
        public bool LOpen = false;
        public bool ROpen = false;
        public bool TOpen = false;

        public MapChunk(char[,] map)
        {
            this.roomMap = map;
        }


    }
}
