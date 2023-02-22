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

        private Random rand;

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
        private MapChunk Empty = new MapChunk(new char[7, 7]
        {
            {' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' '},
            {' ',' ',' ',' ',' ',' ',' '}
        });

        private MapChunk[,] TempMap = new MapChunk[5, 5];


        private void GetFiles()
        {
            string[] tempB = File.ReadAllLines("MapChunkCategories/B.txt"); //Grab appropriate text file    //
            for (int i = 0; i < 3; i++)                                                                     //
            {                                                                                               //
                B[i] = new MapChunk(new char[7,7]); //makes new chunk                                       //
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
                if(x == 0) // Top of Map                                                                    //
                {                                                                                           //
                    if (TempMap[x, y+1].LOpen && TempMap[x+1, y].TOpen)                                     //
                    {                                                                                       //
                        return RandomizeTile(BR);                                                           //
                    }else if (TempMap[x, y+1].LOpen)                                                        //  Gets a random chunk for the left side of the map
                    {                                                                                       //
                        return RandomizeTile(R);                                                            //  Checks all possible entrance arrangements for the current arrangement
                    }else if (TempMap[x+1, y].TOpen)                                                        //
                    {                                                                                       //  Gives a random chunk that has the correct arrangement
                        return RandomizeTile(B);                                                            //
                    }                                                                                       //
                }else if (x == TempMap.GetLength(0)-1) //Bottom of map                                      //  Repeat for right side and center
                {                                                                                           //
                    if (TempMap[x, y+1].LOpen && TempMap[x-1, y].BOpen)                                     //
                    {                                                                                       //
                        return RandomizeTile(RT);                                                           //
                    }                                                                                       //
                    else if (TempMap[x, y+1].LOpen)                                                         //
                    {                                                                                       //
                        return RandomizeTile(R);                                                            //
                    }                                                                                       //
                    else if (TempMap[x-1, y].BOpen)                                                         //
                    {                                                                                       //
                        return RandomizeTile(T);                                                            //
                    }                                                                                       //
                }                                                                                           //
                else //left side of map not top or bottom                                                   //
                {                                                                                           //
                    if (TempMap[x, y+1].LOpen && TempMap[x+1, y].TOpen && TempMap[x-1, y].BOpen)            //
                    {                                                                                       //
                        return RandomizeTile(BRT);                                                          //
                    } else if (TempMap[x, y+1].LOpen && TempMap[x+1, y].TOpen)                              //
                    {                                                                                       //
                        return RandomizeTile(BR);                                                           //
                    } else if (TempMap[x-1, y].BOpen && TempMap[x, y+1].LOpen)                              //
                    {                                                                                       //
                        return RandomizeTile(RT);                                                           //
                    } else if (TempMap[x-1, y].BOpen && TempMap[x+1, y].TOpen)                              //
                    {                                                                                       //
                        return RandomizeTile(BT);                                                           //
                    } else if (TempMap[x, y+1].LOpen)                                                       //
                    {                                                                                       //
                        return RandomizeTile(R);                                                            //
                    } else if (TempMap[x-1, y].BOpen)                                                       //
                    {                                                                                       //
                        return RandomizeTile(T);                                                            //
                    } else if (TempMap[x+1, y].TOpen)                                                       //
                    {                                                                                       //
                        return RandomizeTile(B);                                                            //
                    }                                                                                       //
                }                                                                                           //
            } else if(y == TempMap.GetLength(1) - 1)    //right side of map
            {
                if (x == 0) //top of map
                {
                    if (TempMap[x, y-1].ROpen && TempMap[x+1, y].TOpen)
                    {
                        return RandomizeTile(BL);
                    }
                    else if (TempMap[x, y-1].ROpen)
                    {
                        return RandomizeTile(L);
                    }
                    else if (TempMap[x+1, y].TOpen)
                    {
                        return RandomizeTile(B);
                    }
                }
                else if (x == TempMap.GetLength(0) - 1) // bottom of map
                {
                    if (TempMap[x, y-1].ROpen && TempMap[x-1, y].BOpen)
                    {
                        return RandomizeTile(LT);
                    }
                    else if (TempMap[x, y-1].ROpen)
                    {
                        return RandomizeTile(L);
                    }
                    else if (TempMap[x-1, y].BOpen)
                    {
                        return RandomizeTile(T);
                    }
                }
                else    // right side of map not top or bottom
                {
                    if (TempMap[x, y-1].ROpen && TempMap[x+1, y].TOpen && TempMap[x-1, y].BOpen)
                    {
                        return RandomizeTile(BLT);
                    }
                    else if (TempMap[x, y-1].ROpen && TempMap[x+1, y].TOpen)
                    {
                        return RandomizeTile(BL);
                    }
                    else if (TempMap[x-1, y].BOpen && TempMap[x, y-1].ROpen)
                    {
                        return RandomizeTile(LT);
                    }
                    else if (TempMap[x-1, y].BOpen && TempMap[x+1, y].TOpen)
                    {
                        return RandomizeTile(BT);
                    }
                    else if (TempMap[x, y-1].ROpen)
                    {
                        return RandomizeTile(L);
                    }
                    else if (TempMap[x-1, y].BOpen)
                    {
                        return RandomizeTile(T);
                    }
                    else if (TempMap[x+1, y].TOpen)
                    {
                        return RandomizeTile(B);
                    }
                }
            }else if (x == 0)   //Top of Map not left or right
            {
                if (TempMap[x, y+1].LOpen && TempMap[x, y-1].ROpen && TempMap[x+1, y].TOpen)
                {
                    return RandomizeTile(BLR);
                }else if (TempMap[x, y+1].LOpen && TempMap[x, y-1].ROpen)
                {
                    return RandomizeTile(LR);
                }else if (TempMap[x, y+1].LOpen && TempMap[x+1, y].TOpen)
                {
                    return RandomizeTile(BR);
                }else if (TempMap[x, y-1].ROpen && TempMap[x+1, y].TOpen)
                {
                    return RandomizeTile(BL);
                }else if (TempMap[x, y+1].LOpen)
                {
                    return RandomizeTile(R);
                }else if (TempMap[x, y-1].ROpen)
                {
                    return RandomizeTile(L);
                }else if (TempMap[x+1, y].TOpen)
                {
                    return RandomizeTile(B);
                }
            }else if(x == TempMap.GetLength(0) - 1) //bottom of map not left or right
            {
                if (TempMap[x, y+1].LOpen && TempMap[x, y-1].ROpen && TempMap[x-1, y].BOpen)
                {
                    return RandomizeTile(LRT);
                }
                else if (TempMap[x, y+1].LOpen && TempMap[x, y-1].ROpen)
                {
                    return RandomizeTile(LR);
                }
                else if (TempMap[x, y+1].LOpen && TempMap[x-1, y].BOpen)
                {
                    return RandomizeTile(RT);
                }
                else if (TempMap[x, y-1].ROpen && TempMap[x-1, y].BOpen)
                {
                    return RandomizeTile(LT);
                }
                else if (TempMap[x, y+1].LOpen)
                {
                    return RandomizeTile(R);
                }
                else if (TempMap[x, y-1].ROpen)
                {
                    return RandomizeTile(L);
                }
                else if (TempMap[x-1, y].BOpen)
                {
                    return RandomizeTile(T);
                }
            }
            else    //not on the edge of the map
            {
                if (TempMap[x-1, y].BOpen && TempMap[x, y-1].ROpen && TempMap[x, y+1].LOpen && TempMap[x+1, y].TOpen)
                {
                    return RandomizeTile(BLRT);
                }
                else if (TempMap[x, y+1].LOpen && TempMap[x, y-1].ROpen && TempMap[x+1, y].TOpen)
                {
                    return RandomizeTile(BLR);
                }
                else if (TempMap[x, y+1].LOpen && TempMap[x, y-1].ROpen && TempMap[x-1, y].BOpen)
                {
                    return RandomizeTile(LRT);
                }else if (TempMap[x, y-1].ROpen && TempMap[x+1, y].TOpen && TempMap[x-1, y].BOpen)
                {
                    return RandomizeTile(BLT);
                }else if (TempMap[x, y+1].LOpen && TempMap[x+1, y].TOpen && TempMap[x-1, y].BOpen)
                {
                    return RandomizeTile(BRT);
                } else if (TempMap[x, y+1].LOpen && TempMap[x-1, y].BOpen)
                {
                    return RandomizeTile(RT);
                } else if (TempMap[x, y-1].ROpen && TempMap[x-1, y].BOpen)
                {
                    return RandomizeTile(LT);
                }
                else if (TempMap[x-1, y].BOpen && TempMap[x+1, y].TOpen)
                {
                    return RandomizeTile(BT);
                }
                else if (TempMap[x, y+1].LOpen && TempMap[x, y-1].ROpen)
                {
                    return RandomizeTile(LR);
                }
                else if (TempMap[x, y+1].LOpen && TempMap[x+1, y].TOpen)
                {
                    return RandomizeTile(BR);
                }
                else if (TempMap[x, y-1].ROpen && TempMap[x+1, y].TOpen)
                {
                    return RandomizeTile(BL);
                }
                else if (TempMap[x-1, y].BOpen)
                {
                    return RandomizeTile(T);
                }
                else if (TempMap[x, y+1].LOpen)
                {
                    return RandomizeTile(R);
                }
                else if (TempMap[x, y-1].ROpen)
                {
                    return RandomizeTile(L);
                }
                else if (TempMap[x+1, y].TOpen)
                {
                    return RandomizeTile(B);
                }
            }
            return Empty;
        }

        public char[,] RandomizeMap()
        {
            rand = new Random();

            GetFiles();


            char[,] grid = new char[35, 35];
            TempMap[2,2] = RandomizeTile(BLRT); //Center tile is always open in all 4 directions
            switch (rand.Next(0, 5))    //1,1 is always open in at least 3 directions
            {
                case 0:
                    TempMap[1, 1] = RandomizeTile(BLR);
                    break;
                case 1:
                    TempMap[1, 1] = RandomizeTile(BLT);
                    break;
                case 2:
                    TempMap[1, 1] = RandomizeTile(BRT);
                    break;
                case 3:
                    TempMap[1, 1] = RandomizeTile(LRT);
                    break;
                case 4:
                    TempMap[1, 1] = RandomizeTile(BLRT);
                    break;
            }
            switch (rand.Next(0, 5))    //1,3 is always open in at least 3 directions
            {
                case 0:
                    TempMap[1, 3] = RandomizeTile(BLR);
                    break;
                case 1:
                    TempMap[1, 3] = RandomizeTile(BLT);
                    break;
                case 2:
                    TempMap[1, 3] = RandomizeTile(BRT);
                    break;
                case 3:
                    TempMap[1, 3] = RandomizeTile(LRT);
                    break;
                case 4:
                    TempMap[1, 3] = RandomizeTile(BLRT);
                    break;
            }
            switch (rand.Next(0, 5))    //3,1 is always open in at least 3 directions
            {
                case 0:
                    TempMap[3, 1] = RandomizeTile(BLR);
                    break;
                case 1:
                    TempMap[3, 1] = RandomizeTile(BLT);
                    break;
                case 2:
                    TempMap[3, 1] = RandomizeTile(BRT);
                    break;
                case 3:
                    TempMap[3, 1] = RandomizeTile(LRT);
                    break;
                case 4:
                    TempMap[3, 1] = RandomizeTile(BLRT);
                    break;
            }
            switch (rand.Next(0, 5))    //3,3 is always open in at least 3 directions
            {
                case 0:
                    TempMap[3, 3] = RandomizeTile(BLR);
                    break;
                case 1:
                    TempMap[3, 3] = RandomizeTile(BLT);
                    break;
                case 2:
                    TempMap[3, 3] = RandomizeTile(BRT);
                    break;
                case 3:
                    TempMap[3, 3] = RandomizeTile(LRT);
                    break;
                case 4:
                    TempMap[3, 3] = RandomizeTile(BLRT);
                    break;
            }
            switch (rand.Next(0, 4))    //0,2 is always open in at least 2 directions
            {
                case 0:
                    TempMap[0, 2] = RandomizeTile(LR);
                    break;
                case 1:
                    TempMap[0, 2] = RandomizeTile(BL);
                    break;
                case 2:
                    TempMap[0, 2] = RandomizeTile(BR);
                    break;
                case 3:
                    TempMap[0, 2] = RandomizeTile(BLR);
                    break;
            }
            switch (rand.Next(0, 4))    //2,0 is always open in at least 2 directions
            {
                case 0:
                    TempMap[2, 0] = RandomizeTile(BR);
                    break;
                case 1:
                    TempMap[2, 0] = RandomizeTile(BT);
                    break;
                case 2:
                    TempMap[2, 0] = RandomizeTile(RT);
                    break;
                case 3:
                    TempMap[2, 0] = RandomizeTile(BRT);
                    break;
            }
            switch (rand.Next(0, 4))    //2,4 is always open in at least 2 directions
            {
                case 0:
                    TempMap[2, 4] = RandomizeTile(BL);
                    break;
                case 1:
                    TempMap[2, 4] = RandomizeTile(BT);
                    break;
                case 2:
                    TempMap[2, 4] = RandomizeTile(LT);
                    break;
                case 3:
                    TempMap[2, 4] = RandomizeTile(BLT);
                    break;
            }
            switch (rand.Next(0, 4))    //4,2 is always open in at least 2 directions
            {
                case 0:
                    TempMap[4, 2] = RandomizeTile(LR);
                    break;
                case 1:
                    TempMap[4, 2] = RandomizeTile(LT);
                    break;
                case 2:
                    TempMap[4, 2] = RandomizeTile(RT);
                    break;
                case 3:
                    TempMap[4, 2] = RandomizeTile(LRT);
                    break;
            }
            switch (rand.Next(0, 3))    //top left corner to allow fill to work
            {
                case 0:
                    TempMap[0, 0] = RandomizeTile(BR);
                    break;
                case 1:
                    TempMap[0, 0] = RandomizeTile(B);
                    break;
                case 2:
                    TempMap[0, 0] = RandomizeTile(R);
                    break;
            }
            switch (rand.Next(0, 3))    //top right corner to allow fill to work
            {
                case 0:
                    TempMap[0, 4] = RandomizeTile(BL);
                    break;
                case 1:
                    TempMap[0, 4] = RandomizeTile(B);
                    break;
                case 2:
                    TempMap[0, 4] = RandomizeTile(L);
                    break;
            }
            switch (rand.Next(0, 3))    //bottom left corner to allow fill to work
            {
                case 0:
                    TempMap[4, 0] = RandomizeTile(RT);
                    break;
                case 1:
                    TempMap[4, 0] = RandomizeTile(R);
                    break;
                case 2:
                    TempMap[4, 0] = RandomizeTile(T);
                    break;
            }
            switch (rand.Next(0, 3))    //bottom right corner to allow fill to work
            {
                case 0:
                    TempMap[4, 4] = RandomizeTile(LT);
                    break;
                case 1:
                    TempMap[4, 4] = RandomizeTile(L);
                    break;
                case 2:
                    TempMap[4, 4] = RandomizeTile(T);
                    break;
            }
            TempMap[0, 1] = FillTile(0, 1); //
            TempMap[0, 3] = FillTile(0, 3); //
            TempMap[1, 0] = FillTile(1, 0); //
            TempMap[1, 2] = FillTile(1, 2); //
            TempMap[1, 4] = FillTile(1, 4); //
            TempMap[2, 1] = FillTile(2, 1); //  Fills in the empty squares based off of the openings provided
            TempMap[2, 3] = FillTile(2, 3); //
            TempMap[3, 0] = FillTile(3, 0); //
            TempMap[3, 2] = FillTile(3, 2); //
            TempMap[3, 4] = FillTile(3, 4); //
            TempMap[4, 1] = FillTile(4, 1); //
            TempMap[4, 3] = FillTile(4, 3); //

            for (int i = 0; i < 5; i++)                                             //
            {                                                                       //
                for (int j = 0; j < 5; j++)                                         //
                {                                                                   //
                    for (int k = 0; k < 7; k++)                                     //
                    {                                                               //
                        for (int l = 0; l < 7; l++)                                 //
                        {                                                           //
                            grid[i * 7 + k, j * 7 + l] = TempMap[i, j].tile[k, l];  //  puts the chunks together in grid
                        }                                                           //
                    }                                                               //
                }                                                                   //
            }                                                                       //

            return grid;    //return grid to map
        }
    }
}
