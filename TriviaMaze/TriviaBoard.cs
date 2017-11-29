using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
//Tyger test comment
namespace TriviaMaze
{
    /*
     * Represents the entire gameboard, composed of a grid of TriviaTiles
     * that the user must navigate through
     */
    public class TriviaBoard
    {
        /* only allocate questions in the rooms we've explored too.
         * this infers that if the user has attempted every question on the board and failed
         * they lose. first room only has 2 questions
         */
        TriviaTile[,] GameMap;
        TriviaTile MyLoc;
        public int XPos { get; private set; }
        public int YPos{ get; private set; } 
        private readonly int MAX_SIZE;
        public int GatesAvailable { get; private set; }

        public TriviaBoard()
        {
            GameMap = new TriviaTile[5, 5];

            /* instead of instantiating all of them, we are only going to 
             * instantiate rooms we've reached
            for (int i = 0; i < GameMap.GetLength(0); i++)
            {
                for(int j = 0; j < GameMap.GetLength(0); j++)
                {
                    GameMap[i, j] = new TriviaTile();

                }
                
            }*/
            BuildNextTile(XPos, YPos);
            MyLoc = GameMap[XPos, YPos];
            MAX_SIZE = 5;
            XPos = YPos = 0;
        }

        //call to build a tile at the location passed in
        private void BuildNextTile(int x, int y)
        {
            if (GameMap[x, y] == null)
            {
                Log($"Creating tile <{x},{y}> and assigning to MyLoc");
                GameMap[x, y] = new TriviaTile(x, y);
                MyLoc = GameMap[x, y];
            }
            else
            {
                Log("Tile already exists, Updating MyLoc");
                MyLoc = GameMap[x, y];
            }
                

        }

        //Call to move up one spot
        public bool MoveUp()
        {
            bool rv = true;
            YPos--;
            //check if invalid. if so, revert work done
            if(YPos < 0 || MyLoc.NorthLock == Lock.HardLock)
            {
                Log("Can't move up out of bounds or through a hard lock");
                YPos++;
                return false;
            }
            //valid move, do work

            //before i build the next tile, i need to check locks and ask question
            BuildNextTile(XPos, YPos);
            return rv;
        }

        //call to move down one spot
        public bool MoveDown()
        {
            bool rv = true;
            YPos++;
            if (YPos >= MAX_SIZE || MyLoc.SouthLock == Lock.HardLock)
            {
                Log("Can't move down out of bounds or through a hard lock");
                YPos--;
                return false;
            }
            //valid move, do work
            
            //before i build the next tile, i need to check locks and ask question
            BuildNextTile(XPos, YPos);
            return rv;
        }
        public bool MoveLeft()
        {
            bool rv = true;
            XPos--;
            if (XPos < 0 || MyLoc.WestLock == Lock.HardLock)
            {
                Log("Can't move left out of bounds or through a hard lock");
                XPos++;
                return false;
            }
            BuildNextTile(XPos, YPos);
            return rv;
        }
        public bool MoveRight()
        {
            bool rv = true;
            XPos++;
            if (XPos >= MAX_SIZE || MyLoc.EastLock == Lock.HardLock)
            {
                Log("Can't move down out of bounds or through a hard lock");
                XPos--;
                return false;
            }
            BuildNextTile(XPos, YPos);
            return rv;
        }
        
        private void Log(String msg)
        {
            Debug.WriteLine(msg);
        }
    }
}
