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
        TriviaTile[,] GameMap;
        TriviaTile MyLoc;
        public int XPos { get; private set; }
        public int YPos{ get; private set; } 
        private readonly int MAX_SIZE;

        public TriviaBoard()
        {
            GameMap = new TriviaTile[5, 5];
            for(int i = 0; i < GameMap.GetLength(0); i++)
            {
                for(int j = 0; j < GameMap.GetLength(0); j++)
                {
                    GameMap[i, j] = new TriviaTile();

                }
                
            }
            MyLoc = new TriviaTile();
            MAX_SIZE = 5;
            XPos = YPos = 0;
        }

        public bool MoveUp()
        {
            bool rv = true;
            YPos--;
            if(YPos < 0)
            {
                Log("Can't move up out of bounds");
                YPos++;
                return false;
            }
            //valid move, do work
            return rv;
        }
        public bool MoveDown()
        {
            bool rv = true;
            YPos++;
            if (YPos >= MAX_SIZE)
            {
                Log("Can't move down out of bounds");
                YPos--;
                return false;
            }
            //valid move, do work
            return rv;
        }
        public bool MoveLeft()
        {
            bool rv = true;
            XPos--;
            if (XPos < 0)
            {
                Log("Can't move left out of bounds");
                XPos++;
                return false;
            }
            return rv;
        }
        public bool MoveRight()
        {
            bool rv = true;
            XPos++;
            if (XPos >= MAX_SIZE)
            {
                Log("Can't move down out of bounds");
                XPos--;
                return false;
            }
            return rv;
        }
        
        private void Log(String msg)
        {
            Debug.WriteLine(msg);
        }
    }
}
