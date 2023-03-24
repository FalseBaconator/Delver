using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextRPG
{
    internal class MapGenerator
    {
        // ▓ = wall Grey
        // , = floor Grey
        // █ = door Brown

        private Random rand = Constants.rand;

        /*
        MapChunk[] TLCorners = new MapChunk[]
        {
            new MapChunk(new char[,]
            {
                {'▓','▓','▓','▓','▓','▓','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',',','},
                {'▓',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓','▓','▓',',','▓','▓','▓'},
            }),
            new MapChunk(new char[,]
            {
                {' ',' ',' ',' ',' ',' ',' '},
                {' ','▓','▓','▓','▓','▓',' '},
                {' ','▓',',',',',',','▓','▓'},
                {' ','▓',',',',',',',',',','},
                {' ','▓',',',',',',','▓','▓'},
                {' ','▓','▓',',','▓','▓',' '},
                {' ',' ','▓',',','▓',' ',' '},
            }),
            new MapChunk(new char[,]
            {
                {' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' '},
                {' ',' ','▓','▓','▓','▓','▓'},
                {' ',' ','▓',',',',',',',','},
                {' ',' ','▓',',','▓','▓','▓'},
                {' ',' ','▓',',','▓',' ',' '},
                {' ',' ','▓',',','▓',' ',' '},
            })

        };

        MapChunk[] TRCorners = new MapChunk[]
        {
            new MapChunk(new char[,]
            {
                {'▓','▓','▓','▓','▓','▓','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',','▓'},
                {',',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓','▓','▓',',','▓','▓','▓'},
            }),
            new MapChunk(new char[,]
            {
                {' ',' ',' ',' ',' ',' ',' '},
                {' ','▓','▓','▓','▓','▓',' '},
                {'▓','▓',',',',',',','▓',' '},
                {',',',',',',',',',','▓',' '},
                {'▓','▓',',',',',',','▓',' '},
                {' ','▓','▓',',','▓','▓',' '},
                {' ',' ','▓',',','▓',' ',' '},
            }),
            new MapChunk(new char[,]
            {
                {' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' '},
                {'▓','▓','▓','▓','▓',' ',' '},
                {',',',',',',',','▓',' ',' '},
                {'▓','▓','▓',',','▓',' ',' '},
                {' ',' ','▓',',','▓',' ',' '},
                {' ',' ','▓',',','▓',' ',' '},
            })

        };

        MapChunk[] BLCorners = new MapChunk[]
        {
            new MapChunk(new char[,]
            {
                {'▓','▓','▓',',','▓','▓','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',',','},
                {'▓',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓','▓','▓','▓','▓','▓','▓'},
            }),
            new MapChunk(new char[,]
            {
                {' ',' ','▓',',','▓',' ',' '},
                {' ','▓','▓',',','▓','▓',' '},
                {' ','▓',',',',',',','▓','▓'},
                {' ','▓',',',',',',',',',','},
                {' ','▓',',',',',',','▓','▓'},
                {' ','▓','▓','▓','▓','▓',' '},
                {' ',' ',' ',' ',' ',' ',' '},
            }),
            new MapChunk(new char[,]
            {
                {' ',' ','▓',',','▓',' ',' '},
                {' ',' ','▓',',','▓',' ',' '},
                {' ',' ','▓',',','▓','▓','▓'},
                {' ',' ','▓',',',',',',',','},
                {' ',' ','▓','▓','▓','▓','▓'},
                {' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' '},
            })

        };

        MapChunk[] BRCorners = new MapChunk[]
        {
            new MapChunk(new char[,]
            {
                {'▓','▓','▓',',','▓','▓','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',','▓'},
                {',',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓','▓','▓','▓','▓','▓','▓'},
            }),
            new MapChunk(new char[,]
            {
                {' ',' ','▓',',','▓',' ',' '},
                {' ','▓','▓',',','▓','▓',' '},
                {'▓','▓',',',',',',','▓',' '},
                {',',',',',',',',',','▓',' '},
                {'▓','▓',',',',',',','▓',' '},
                {' ','▓','▓','▓','▓','▓',' '},
                {' ',' ',' ',' ',' ',' ',' '},
            }),
            new MapChunk(new char[,]
            {
                {' ',' ','▓',',','▓',' ',' '},
                {' ',' ','▓',',','▓',' ',' '},
                {'▓','▓','▓',',','▓',' ',' '},
                {',',',',',',',','▓',' ',' '},
                {'▓','▓','▓','▓','▓',' ',' '},
                {' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' '},
            })

        };

        MapChunk[] Tops = new MapChunk[]
        {
            new MapChunk(new char[,]
            {
                {'▓','▓','▓','▓','▓','▓','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',','▓'},
                {',',',',',',',',',',',',','},
                {'▓',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓','▓','▓',',','▓','▓','▓'},
            }),
            new MapChunk(new char[,]
            {
                {' ',' ',' ',' ',' ',' ',' '},
                {' ','▓','▓','▓','▓','▓',' '},
                {'▓','▓',',',',',',','▓','▓'},
                {',',',',',',',',',',',',','},
                {'▓','▓',',',',',',','▓','▓'},
                {' ','▓','▓',',','▓','▓',' '},
                {' ',' ','▓',',','▓',' ',' '},
            }),
            new MapChunk(new char[,]
            {
                {' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' '},
                {'▓','▓','▓','▓','▓','▓','▓'},
                {',',',',',',',',',',',',','},
                {'▓','▓','▓',',','▓','▓','▓'},
                {' ',' ','▓',',','▓',' ',' '},
                {' ',' ','▓',',','▓',' ',' '},
            })

        };

        MapChunk[] Bottoms = new MapChunk[]
        {
            new MapChunk(new char[,]
            {
                {'▓','▓','▓',',','▓','▓','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',','▓'},
                {',',',',',',',',',',',',','},
                {'▓',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓','▓','▓','▓','▓','▓','▓'},
            }),
            new MapChunk(new char[,]
            {
                {' ',' ','▓',',','▓',' ',' '},
                {' ','▓','▓',',','▓','▓',' '},
                {'▓','▓',',',',',',','▓','▓'},
                {',',',',',',',',',',',',','},
                {'▓','▓',',',',',',','▓','▓'},
                {' ','▓','▓','▓','▓','▓',' '},
                {' ',' ',' ',' ',' ',' ',' '},
            }),
            new MapChunk(new char[,]
            {
                {' ',' ','▓',',','▓',' ',' '},
                {' ',' ','▓',',','▓',' ',' '},
                {'▓','▓','▓',',','▓','▓','▓'},
                {',',',',',',',',',',',',','},
                {'▓','▓','▓','▓','▓','▓','▓'},
                {' ',' ',' ',' ',' ',' ',' '},
                {' ',' ',' ',' ',' ',' ',' '},
            })

        };

        MapChunk[] Lefts = new MapChunk[]
        {
            new MapChunk(new char[,]
            {
                {'▓','▓','▓',',','▓','▓','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',',','},
                {'▓',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓','▓','▓',',','▓','▓','▓'},
            }),
            new MapChunk(new char[,]
            {
                {' ',' ','▓',',','▓',' ',' '},
                {' ','▓','▓',',','▓','▓',' '},
                {' ','▓',',',',',',','▓','▓'},
                {' ','▓',',',',',',',',',','},
                {' ','▓',',',',',',','▓','▓'},
                {' ','▓','▓',',','▓','▓',' '},
                {' ',' ','▓',',','▓',' ',' '},
            }),
            new MapChunk(new char[,]
            {
                {' ',' ','▓',',','▓',' ',' '},
                {' ',' ','▓',',','▓',' ',' '},
                {' ',' ','▓',',','▓','▓','▓'},
                {' ',' ','▓',',',',',',',','},
                {' ',' ','▓',',','▓','▓','▓'},
                {' ',' ','▓',',','▓',' ',' '},
                {' ',' ','▓',',','▓',' ',' '},
            })

        };

        MapChunk[] Rights = new MapChunk[]
        {
            new MapChunk(new char[,]
            {
                {'▓','▓','▓',',','▓','▓','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',','▓'},
                {',',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓','▓','▓',',','▓','▓','▓'},
            }),
            new MapChunk(new char[,]
            {
                {' ',' ','▓',',','▓',' ',' '},
                {' ','▓','▓',',','▓','▓',' '},
                {'▓','▓',',',',',',','▓',' '},
                {',',',',',',',',',','▓',' '},
                {'▓','▓',',',',',',','▓',' '},
                {' ','▓','▓',',','▓','▓',' '},
                {' ',' ','▓',',','▓',' ',' '},
            }),
            new MapChunk(new char[,]
            {
                {' ',' ','▓',',','▓',' ',' '},
                {' ',' ','▓',',','▓',' ',' '},
                {'▓','▓','▓',',','▓',' ',' '},
                {',',',',',',',','▓',' ',' '},
                {'▓','▓','▓',',','▓',' ',' '},
                {' ',' ','▓',',','▓',' ',' '},
                {' ',' ','▓',',','▓',' ',' '},
            })

        };

        MapChunk[] Centers = new MapChunk[]
        {
            new MapChunk(new char[,]
            {
                {'▓','▓','▓',',','▓','▓','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',','▓'},
                {',',',',',',',',',',',',','},
                {'▓',',',',',',',',',',','▓'},
                {'▓',',',',',',',',',',','▓'},
                {'▓','▓','▓',',','▓','▓','▓'},
            }),
            new MapChunk(new char[,]
            {
                {' ',' ','▓',',','▓',' ',' '},
                {' ','▓','▓',',','▓','▓',' '},
                {'▓','▓',',',',',',','▓','▓'},
                {',',',',',',',',',',',',','},
                {'▓','▓',',',',',',','▓','▓'},
                {' ','▓','▓',',','▓','▓',' '},
                {' ',' ','▓',',','▓',' ',' '},
            }),
            new MapChunk(new char[,]
            {
                {' ',' ','▓',',','▓',' ',' '},
                {' ',' ','▓',',','▓',' ',' '},
                {'▓','▓','▓',',','▓','▓','▓'},
                {',',',',',',',',',',',',','},
                {'▓','▓','▓',',','▓','▓','▓'},
                {' ',' ','▓',',','▓',' ',' '},
                {' ',' ','▓',',','▓',' ',' '},
            })

        };
        */

        private MapChunk[] B = new MapChunk[3];       //0
        private MapChunk[] L = new MapChunk[3];       //1
        private MapChunk[] R = new MapChunk[3];       //2
        private MapChunk[] T = new MapChunk[3];       //3
        private MapChunk[] BL = new MapChunk[3];      //4
        private MapChunk[] BR = new MapChunk[3];      //5
        private MapChunk[] BT = new MapChunk[3];      //6
        private MapChunk[] LR = new MapChunk[3];      //7
        private MapChunk[] LT = new MapChunk[3];      //8
        private MapChunk[] RT = new MapChunk[3];      //9
        private MapChunk[] BLR = new MapChunk[3];     //10
        private MapChunk[] BLT = new MapChunk[3];     //11
        private MapChunk[] BRT = new MapChunk[3];     //12
        private MapChunk[] LRT = new MapChunk[3];     //13
        private MapChunk[] BLRT = new MapChunk[3];    //14
        private MapChunk Empty = new MapChunk(new char[Constants.roomHeight, Constants.roomWidth]
        {
            {' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' '}
        });

        private MapChunk[,] TempMap = new MapChunk[Constants.mapHeight, Constants.mapWidth];
        private char[,] miniMap = new char[Constants.mapHeight,Constants.mapWidth];

        //private MapChunk[,] FinalRoomArrangement = new MapChunk[Constants.mapHeight, Constants.mapWidth];

        private void GetFiles()
        {
            string[] tempB = File.ReadAllLines("MapChunkCategories/B.txt"); //Grab appropriate text file    //
            for (int i = 0; i < 3; i++)                                                                     //
            {                                                                                               //
                B[i] = new MapChunk(new char[7, 7]); //makes new chunk                                       //
                B[i].BOpen = true;  //sets chunk's appropriate door bools to true                           //
                for (int j = 0; j < 7; j++)                         //                                      //  Repeat for every chunk category
                {                                                   //                                      //
                    for (int k = 0; k < 7; k++)                     //                                      //
                    {                                               //                                      //
                        B[i].tile[j, k] = tempB[j + (7 * i)][k];    //Gets chunk chars from file            //
                    }                                               //                                      //
                }                                                   //                                      //
            }                                                                                               //

            string[] tempL = File.ReadAllLines("MapChunkCategories/L.txt");
            for (int i = 0; i < 3; i++)
            {
                L[i] = new MapChunk(new char[7, 7]);
                L[i].LOpen = true;
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        L[i].tile[j, k] = tempL[j + (7 * i)][k];
                    }
                }
            }

            string[] tempR = File.ReadAllLines("MapChunkCategories/R.txt");
            for (int i = 0; i < 3; i++)
            {
                R[i] = new MapChunk(new char[7, 7]);
                R[i].ROpen = true;
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        R[i].tile[j, k] = tempR[j + (7 * i)][k];
                    }
                }
            }

            string[] tempT = File.ReadAllLines("MapChunkCategories/T.txt");
            for (int i = 0; i < 3; i++)
            {
                T[i] = new MapChunk(new char[7, 7]);
                T[i].TOpen = true;
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        T[i].tile[j, k] = tempT[j + (7 * i)][k];
                    }
                }
            }

            string[] tempBL = File.ReadAllLines("MapChunkCategories/BL.txt");
            for (int i = 0; i < 3; i++)
            {
                BL[i] = new MapChunk(new char[7, 7]);
                BL[i].BOpen = true;
                BL[i].LOpen = true;
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        BL[i].tile[j, k] = tempBL[j + (7 * i)][k];
                    }
                }
            }

            string[] tempBR = File.ReadAllLines("MapChunkCategories/BR.txt");
            for (int i = 0; i < 3; i++)
            {
                BR[i] = new MapChunk(new char[7, 7]);
                BR[i].BOpen = true;
                BR[i].ROpen = true;
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        BR[i].tile[j, k] = tempBR[j + (7 * i)][k];
                    }
                }
            }

            string[] tempBT = File.ReadAllLines("MapChunkCategories/BT.txt");
            for (int i = 0; i < 3; i++)
            {
                BT[i] = new MapChunk(new char[7, 7]);
                BT[i].BOpen = true;
                BT[i].TOpen = true;
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        BT[i].tile[j, k] = tempBT[j + (7 * i)][k];
                    }
                }
            }

            string[] tempLR = File.ReadAllLines("MapChunkCategories/LR.txt");
            for (int i = 0; i < 3; i++)
            {
                LR[i] = new MapChunk(new char[7, 7]);
                LR[i].LOpen = true;
                LR[i].ROpen = true;
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        LR[i].tile[j, k] = tempLR[j + (7 * i)][k];
                    }
                }
            }

            string[] tempLT = File.ReadAllLines("MapChunkCategories/LT.txt");
            for (int i = 0; i < 3; i++)
            {
                LT[i] = new MapChunk(new char[7, 7]);
                LT[i].LOpen = true;
                LT[i].TOpen = true;
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        LT[i].tile[j, k] = tempLT[j + (7 * i)][k];
                    }
                }
            }

            string[] tempRT = File.ReadAllLines("MapChunkCategories/RT.txt");
            for (int i = 0; i < 3; i++)
            {
                RT[i] = new MapChunk(new char[7, 7]);
                RT[i].ROpen = true;
                RT[i].TOpen = true;
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        RT[i].tile[j, k] = tempRT[j + (7 * i)][k];
                    }
                }
            }

            string[] tempBLR = File.ReadAllLines("MapChunkCategories/BLR.txt");
            for (int i = 0; i < 3; i++)
            {
                BLR[i] = new MapChunk(new char[7, 7]);
                BLR[i].BOpen = true;
                BLR[i].LOpen = true;
                BLR[i].ROpen = true;
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        BLR[i].tile[j, k] = tempBLR[j + (7 * i)][k];
                    }
                }
            }

            string[] tempBLT = File.ReadAllLines("MapChunkCategories/BLT.txt");
            for (int i = 0; i < 3; i++)
            {
                BLT[i] = new MapChunk(new char[7, 7]);
                BLT[i].BOpen = true;
                BLT[i].LOpen = true;
                BLT[i].TOpen = true;
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        BLT[i].tile[j, k] = tempBLT[j + (7 * i)][k];
                    }
                }
            }

            string[] tempBRT = File.ReadAllLines("MapChunkCategories/BRT.txt");
            for (int i = 0; i < 3; i++)
            {
                BRT[i] = new MapChunk(new char[7, 7]);
                BRT[i].BOpen = true;
                BRT[i].ROpen = true;
                BRT[i].TOpen = true;
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        BRT[i].tile[j, k] = tempBRT[j + (7 * i)][k];
                    }
                }
            }

            string[] tempLRT = File.ReadAllLines("MapChunkCategories/LRT.txt");
            for (int i = 0; i < 3; i++)
            {
                LRT[i] = new MapChunk(new char[7, 7]);
                LRT[i].LOpen = true;
                LRT[i].ROpen = true;
                LRT[i].TOpen = true;
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        LRT[i].tile[j, k] = tempLRT[j + (7 * i)][k];
                    }
                }
            }

            string[] tempBLRT = File.ReadAllLines("MapChunkCategories/BLRT.txt");
            for (int i = 0; i < 3; i++)
            {
                BLRT[i] = new MapChunk(new char[7, 7]);
                BLRT[i].BOpen = true;
                BLRT[i].LOpen = true;
                BLRT[i].ROpen = true;
                BLRT[i].TOpen = true;
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 7; k++)
                    {
                        BLRT[i].tile[j, k] = tempBLRT[j + (7 * i)][k];
                    }
                }
            }
        }

        private MapChunk RandomizeTile(MapChunk[] chunkOptions) //Gets a chunk from a predetermined category
        {
            MapChunk chunk = chunkOptions[rand.Next(0, chunkOptions.Length)];
            return chunk;
        }

        private MapChunk FillTile(int x, int y)
        {
            if (y == 0) // Left Side of map                                                                 //
            {                                                                                               //
                if (x == 0) // Top of Map                                                                    //
                {                                                                                           //
                    if (TempMap[x, y + 1].LOpen && TempMap[x + 1, y].TOpen)                                     //
                    {                                                                                       //
                        return RandomizeTile(BR);                                                           //
                    }
                    else if (TempMap[x, y + 1].LOpen)                                                        //  Gets a random chunk for the left side of the map
                    {                                                                                       //
                        return RandomizeTile(R);                                                            //  Checks all possible entrance arrangements for the current arrangement
                    }
                    else if (TempMap[x + 1, y].TOpen)                                                        //
                    {                                                                                       //  Gives a random chunk that has the correct arrangement
                        return RandomizeTile(B);                                                            //
                    }                                                                                       //
                }
                else if (x == TempMap.GetLength(0) - 1) //Bottom of map                                      //  Repeat for right side and center
                {                                                                                           //
                    if (TempMap[x, y + 1].LOpen && TempMap[x - 1, y].BOpen)                                     //
                    {                                                                                       //
                        return RandomizeTile(RT);                                                           //
                    }                                                                                       //
                    else if (TempMap[x, y + 1].LOpen)                                                         //
                    {                                                                                       //
                        return RandomizeTile(R);                                                            //
                    }                                                                                       //
                    else if (TempMap[x - 1, y].BOpen)                                                         //
                    {                                                                                       //
                        return RandomizeTile(T);                                                            //
                    }                                                                                       //
                }                                                                                           //
                else //left side of map not top or bottom                                                   //
                {                                                                                           //
                    if (TempMap[x, y + 1].LOpen && TempMap[x + 1, y].TOpen && TempMap[x - 1, y].BOpen)            //
                    {                                                                                       //
                        return RandomizeTile(BRT);                                                          //
                    }
                    else if (TempMap[x, y + 1].LOpen && TempMap[x + 1, y].TOpen)                              //
                    {                                                                                       //
                        return RandomizeTile(BR);                                                           //
                    }
                    else if (TempMap[x - 1, y].BOpen && TempMap[x, y + 1].LOpen)                              //
                    {                                                                                       //
                        return RandomizeTile(RT);                                                           //
                    }
                    else if (TempMap[x - 1, y].BOpen && TempMap[x + 1, y].TOpen)                              //
                    {                                                                                       //
                        return RandomizeTile(BT);                                                           //
                    }
                    else if (TempMap[x, y + 1].LOpen)                                                       //
                    {                                                                                       //
                        return RandomizeTile(R);                                                            //
                    }
                    else if (TempMap[x - 1, y].BOpen)                                                       //
                    {                                                                                       //
                        return RandomizeTile(T);                                                            //
                    }
                    else if (TempMap[x + 1, y].TOpen)                                                       //
                    {                                                                                       //
                        return RandomizeTile(B);                                                            //
                    }                                                                                       //
                }                                                                                           //
            }
            else if (y == TempMap.GetLength(1) - 1)    //right side of map
            {
                if (x == 0) //top of map
                {
                    if (TempMap[x, y - 1].ROpen && TempMap[x + 1, y].TOpen)
                    {
                        return RandomizeTile(BL);
                    }
                    else if (TempMap[x, y - 1].ROpen)
                    {
                        return RandomizeTile(L);
                    }
                    else if (TempMap[x + 1, y].TOpen)
                    {
                        return RandomizeTile(B);
                    }
                }
                else if (x == TempMap.GetLength(0) - 1) // bottom of map
                {
                    if (TempMap[x, y - 1].ROpen && TempMap[x - 1, y].BOpen)
                    {
                        return RandomizeTile(LT);
                    }
                    else if (TempMap[x, y - 1].ROpen)
                    {
                        return RandomizeTile(L);
                    }
                    else if (TempMap[x - 1, y].BOpen)
                    {
                        return RandomizeTile(T);
                    }
                }
                else    // right side of map not top or bottom
                {
                    if (TempMap[x, y - 1].ROpen && TempMap[x + 1, y].TOpen && TempMap[x - 1, y].BOpen)
                    {
                        return RandomizeTile(BLT);
                    }
                    else if (TempMap[x, y - 1].ROpen && TempMap[x + 1, y].TOpen)
                    {
                        return RandomizeTile(BL);
                    }
                    else if (TempMap[x - 1, y].BOpen && TempMap[x, y - 1].ROpen)
                    {
                        return RandomizeTile(LT);
                    }
                    else if (TempMap[x - 1, y].BOpen && TempMap[x + 1, y].TOpen)
                    {
                        return RandomizeTile(BT);
                    }
                    else if (TempMap[x, y - 1].ROpen)
                    {
                        return RandomizeTile(L);
                    }
                    else if (TempMap[x - 1, y].BOpen)
                    {
                        return RandomizeTile(T);
                    }
                    else if (TempMap[x + 1, y].TOpen)
                    {
                        return RandomizeTile(B);
                    }
                }
            }
            else if (x == 0)   //Top of Map not left or right
            {
                if (TempMap[x, y + 1].LOpen && TempMap[x, y - 1].ROpen && TempMap[x + 1, y].TOpen)
                {
                    return RandomizeTile(BLR);
                }
                else if (TempMap[x, y + 1].LOpen && TempMap[x, y - 1].ROpen)
                {
                    return RandomizeTile(LR);
                }
                else if (TempMap[x, y + 1].LOpen && TempMap[x + 1, y].TOpen)
                {
                    return RandomizeTile(BR);
                }
                else if (TempMap[x, y - 1].ROpen && TempMap[x + 1, y].TOpen)
                {
                    return RandomizeTile(BL);
                }
                else if (TempMap[x, y + 1].LOpen)
                {
                    return RandomizeTile(R);
                }
                else if (TempMap[x, y - 1].ROpen)
                {
                    return RandomizeTile(L);
                }
                else if (TempMap[x + 1, y].TOpen)
                {
                    return RandomizeTile(B);
                }
            }
            else if (x == TempMap.GetLength(0) - 1) //bottom of map not left or right
            {
                if (TempMap[x, y + 1].LOpen && TempMap[x, y - 1].ROpen && TempMap[x - 1, y].BOpen)
                {
                    return RandomizeTile(LRT);
                }
                else if (TempMap[x, y + 1].LOpen && TempMap[x, y - 1].ROpen)
                {
                    return RandomizeTile(LR);
                }
                else if (TempMap[x, y + 1].LOpen && TempMap[x - 1, y].BOpen)
                {
                    return RandomizeTile(RT);
                }
                else if (TempMap[x, y - 1].ROpen && TempMap[x - 1, y].BOpen)
                {
                    return RandomizeTile(LT);
                }
                else if (TempMap[x, y + 1].LOpen)
                {
                    return RandomizeTile(R);
                }
                else if (TempMap[x, y - 1].ROpen)
                {
                    return RandomizeTile(L);
                }
                else if (TempMap[x - 1, y].BOpen)
                {
                    return RandomizeTile(T);
                }
            }
            else    //not on the edge of the map
            {
                if (TempMap[x - 1, y].BOpen && TempMap[x, y - 1].ROpen && TempMap[x, y + 1].LOpen && TempMap[x + 1, y].TOpen)
                {
                    return RandomizeTile(BLRT);
                }
                else if (TempMap[x, y + 1].LOpen && TempMap[x, y - 1].ROpen && TempMap[x + 1, y].TOpen)
                {
                    return RandomizeTile(BLR);
                }
                else if (TempMap[x, y + 1].LOpen && TempMap[x, y - 1].ROpen && TempMap[x - 1, y].BOpen)
                {
                    return RandomizeTile(LRT);
                }
                else if (TempMap[x, y - 1].ROpen && TempMap[x + 1, y].TOpen && TempMap[x - 1, y].BOpen)
                {
                    return RandomizeTile(BLT);
                }
                else if (TempMap[x, y + 1].LOpen && TempMap[x + 1, y].TOpen && TempMap[x - 1, y].BOpen)
                {
                    return RandomizeTile(BRT);
                }
                else if (TempMap[x, y + 1].LOpen && TempMap[x - 1, y].BOpen)
                {
                    return RandomizeTile(RT);
                }
                else if (TempMap[x, y - 1].ROpen && TempMap[x - 1, y].BOpen)
                {
                    return RandomizeTile(LT);
                }
                else if (TempMap[x - 1, y].BOpen && TempMap[x + 1, y].TOpen)
                {
                    return RandomizeTile(BT);
                }
                else if (TempMap[x, y + 1].LOpen && TempMap[x, y - 1].ROpen)
                {
                    return RandomizeTile(LR);
                }
                else if (TempMap[x, y + 1].LOpen && TempMap[x + 1, y].TOpen)
                {
                    return RandomizeTile(BR);
                }
                else if (TempMap[x, y - 1].ROpen && TempMap[x + 1, y].TOpen)
                {
                    return RandomizeTile(BL);
                }
                else if (TempMap[x - 1, y].BOpen)
                {
                    return RandomizeTile(T);
                }
                else if (TempMap[x, y + 1].LOpen)
                {
                    return RandomizeTile(R);
                }
                else if (TempMap[x, y - 1].ROpen)
                {
                    return RandomizeTile(L);
                }
                else if (TempMap[x + 1, y].TOpen)
                {
                    return RandomizeTile(B);
                }
            }
            return Empty;
        }

        private void MapChecker()
        {
            List<Tuple<int, int>> checkedCoords = new List<Tuple<int, int>>();
            checkedCoords.Add(Tuple.Create(Constants.mapHeight/2, Constants.mapWidth/2));
            bool done = false;
            while (done == false)
            {
                bool addedRooms = false;
                List<Tuple<int, int>> toAdd = new List<Tuple<int, int>>();

                foreach (Tuple<int, int> coords in checkedCoords)
                {
                    if (coords.Item1 > 0 && TempMap[coords.Item1, coords.Item2].TOpen)
                    {
                        if (checkedCoords.Contains(Tuple.Create(coords.Item1 - 1, coords.Item2)) == false)
                        {
                            addedRooms = true;
                            toAdd.Add(Tuple.Create(coords.Item1 - 1, coords.Item2));
                        }
                    }
                    if (coords.Item1 < Constants.mapWidth -1 && TempMap[coords.Item1, coords.Item2].BOpen)
                    {
                        if (checkedCoords.Contains(Tuple.Create(coords.Item1 + 1, coords.Item2)) == false)
                        {
                            addedRooms = true;
                            toAdd.Add(Tuple.Create(coords.Item1 + 1, coords.Item2));
                        }
                    }
                    if (coords.Item2 > 0 && TempMap[coords.Item1, coords.Item2].LOpen)
                    {
                        if (checkedCoords.Contains(Tuple.Create(coords.Item1, coords.Item2 - 1)) == false)
                        {
                            addedRooms = true;
                            toAdd.Add(Tuple.Create(coords.Item1, coords.Item2 - 1));
                        }
                    }
                    if (coords.Item2 < Constants.mapHeight - 1 && TempMap[coords.Item1, coords.Item2].ROpen)
                    {
                        if (checkedCoords.Contains(Tuple.Create(coords.Item1, coords.Item2 + 1)) == false)
                        {
                            addedRooms = true;
                            toAdd.Add(Tuple.Create(coords.Item1, coords.Item2 + 1));
                        }
                    }
                }

                foreach (Tuple<int, int> coords in toAdd)
                {
                    if (checkedCoords.Contains(coords) == false)
                        checkedCoords.Add(coords);
                }

                if (addedRooms == false)
                {
                    done = true;
                }

            }

            for (int i = 0; i < Constants.mapWidth; i++)
            {
                for (int j = 0; j < Constants.mapHeight; j++)
                {

                    if (checkedCoords.Contains(Tuple.Create(i, j)) != true)
                    {
                        TempMap[i, j] = Empty;
                    }

                }
            }

        }

        public char[,] RandomizeMap()
        {
            GetFiles();

            char[,] grid = new char[Constants.mapHeight * Constants.roomHeight, Constants.mapWidth * Constants.roomWidth];

            for (int i = 0; i < Constants.mapHeight; i++)
            {
                for (int j = 0; j < Constants.mapWidth; j++)
                {
                    if((i+j)%2 == 0)
                    {
                        MapChunk[][] roomArrays;
                        if(i > 0 && i < Constants.mapHeight - 1 && j > 0 && j < Constants.mapWidth - 1)
                        {
                            roomArrays = new MapChunk[][]
                            {
                                BLR,BLT,BRT,LRT,BLRT,
                            };
                            TempMap[i, j] = RandomizeTile(roomArrays[rand.Next(roomArrays.Length)]);
                        }
                        else if (i == 0)
                        {
                            if(j > 0 && j < Constants.mapWidth - 1)
                            {
                                roomArrays = new MapChunk[][]
                                {
                                BL,BR,LR,BLR
                                };
                                TempMap[i, j] = RandomizeTile(roomArrays[rand.Next(roomArrays.Length)]);
                            }
                            else if(j == 0)
                            {
                                roomArrays = new MapChunk[][]
                                {
                                    B,R,BR
                                };
                                TempMap[i, j] = RandomizeTile(roomArrays[rand.Next(roomArrays.Length)]);
                            }
                            else if(j == Constants.mapWidth - 1)
                            {
                                roomArrays = new MapChunk[][]
                                {
                                    B,L,BL
                                };
                                TempMap[i, j] = RandomizeTile(roomArrays[rand.Next(roomArrays.Length)]);
                            }
                        }else if (i == Constants.mapHeight - 1)
                        {
                            if (j > 0 && j < Constants.mapWidth - 1)
                            {
                                roomArrays = new MapChunk[][]
                                {
                                    LT,LR,RT
                                };
                                TempMap[i, j] = RandomizeTile(roomArrays[rand.Next(roomArrays.Length)]);
                            }
                            else if (j == 0)
                            {
                                roomArrays = new MapChunk[][]
                                {
                                    T,R,RT
                                };
                                TempMap[i, j] = RandomizeTile(roomArrays[rand.Next(roomArrays.Length)]);
                            }
                            else if (j == Constants.mapWidth - 1)
                            {
                                roomArrays = new MapChunk[][]
                                {
                                    T,L,LT
                                };
                                TempMap[i, j] = RandomizeTile(roomArrays[rand.Next(roomArrays.Length)]);
                            }
                        }
                        else
                        {
                            if (j == 0)
                            {
                                roomArrays = new MapChunk[][]
                                {
                                    BT,BR,RT,BRT
                                };
                                TempMap[i, j] = RandomizeTile(roomArrays[rand.Next(roomArrays.Length)]);
                            }
                            else if (j == Constants.mapWidth - 1)
                            {
                                roomArrays = new MapChunk[][]
                                {
                                    BT,BL,LT,BLT
                                };
                                TempMap[i, j] = RandomizeTile(roomArrays[rand.Next(roomArrays.Length)]);
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < Constants.mapHeight; i++)
            {
                for (int j = 0; j < Constants.mapWidth; j++)
                {
                    if((i+j)%2 != 0)
                    {
                        TempMap[i, j] = FillTile(i,j);
                    }
                }
            }

            MapChecker();

            for (int i = 0; i < Constants.mapHeight; i++)                           //
            {                                                                       //
                for (int j = 0; j < Constants.mapWidth; j++)                        //
                {                                                                   //
                    for (int k = 0; k < Constants.roomHeight; k++)                  //
                    {                                                               //
                        for (int l = 0; l < Constants.roomWidth; l++)               //
                        {                                                           //
                            grid[i * 7 + k, j * 7 + l] = TempMap[i, j].tile[k, l];  //  puts the chunks together in grid
                        }                                                           //
                    }                                                               //
                }                                                                   //
            }                                                                       //

            return grid;
        }

        public char[,] BossRoom()
        {
            string[] tempBossRooms = File.ReadAllLines("MapChunkCategories/BossRooms.txt");
            MapChunk[] BossRooms = new MapChunk[Constants.RoomsPerCategory];
            for(int i = 0; i < Constants.RoomsPerCategory; i++)
            {
                BossRooms[i] = new MapChunk(new char[Constants.BossRoomHeight, Constants.BossRoomWidth]);
                for (int j = 0; j < Constants.BossRoomHeight; j++)
                {
                    for (int k = 0; k < Constants.BossRoomWidth; k++)
                    {
                        BossRooms[i].tile[j, k] = tempBossRooms[j + (Constants.BossRoomHeight * i)][k];
                    }
                }
            }
            return BossRooms[rand.Next(BossRooms.Length)].tile;
        }

        public char[,] makeMiniMap()
        {
            for (int i = 0; i < TempMap.GetLength(0); i++)
            {
                for (int j = 0; j < TempMap.GetLength(1); j++)
                {
                    if (B.Contains(TempMap[i, j]))
                        miniMap[i, j] = '^';
                    if (L.Contains(TempMap[i, j]))
                        miniMap[i, j] = '>';
                    if (R.Contains(TempMap[i, j]))
                        miniMap[i, j] = '<';
                    if (T.Contains(TempMap[i, j]))
                        miniMap[i, j] = 'V';

                    if (BL.Contains(TempMap[i, j]))
                        miniMap[i, j] = '┐';
                    if (BR.Contains(TempMap[i, j]))
                        miniMap[i, j] = '┌';
                    if (BT.Contains(TempMap[i, j]))
                        miniMap[i, j] = '│';
                    if (LR.Contains(TempMap[i, j]))
                        miniMap[i, j] = '─';
                    if (LT.Contains(TempMap[i, j]))
                        miniMap[i, j] = '┘';
                    if (RT.Contains(TempMap[i, j]))
                        miniMap[i, j] = '└';

                    if (BLR.Contains(TempMap[i, j]))
                        miniMap[i, j] = '┬';
                    if (BLT.Contains(TempMap[i, j]))
                        miniMap[i, j] = '┤';
                    if (BRT.Contains(TempMap[i, j]))
                        miniMap[i, j] = '├';
                    if (LRT.Contains(TempMap[i, j]))
                        miniMap[i, j] = '┴';

                    if (BLRT.Contains(TempMap[i, j]))
                        miniMap[i, j] = '┼';
                }
            }

            return miniMap;
        }

    }
}
