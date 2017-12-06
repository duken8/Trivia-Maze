using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;

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
        public int SoftLocksRemaining { get; private set; }

        //Database results
        //Boolean section
        List<String> BooleanPrompts = new List<String>();
        List<Boolean> BooleanAnswers = new List<Boolean>();

        //MultipleChoice
        List<String> ChoicePrompts = new List<String>();
        List<String> ChoiceCorrectAnswers = new List<String>();
        List<String> ChoiceFalse1Answers = new List<String>();
        List<String> ChoiceFalse2Answers = new List<String>();
        List<String> ChoiceFalse3Answers = new List<String>();

        //Extended response
        List<String> ExtendedPrompts = new List<String>();
        List<String> ExtendedAnswers = new List<String>();

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
            //Log($"XPos => {XPos}\nYPos => {YPos}");
            SoftLocksRemaining = 0;
            BuildNextTile(XPos, YPos);
            MyLoc = GameMap[XPos, YPos];
            MAX_SIZE = 5;
            XPos = YPos = 0;

            //grab all sqlite data
            ConfigureDatabase();
        }

        private void ConfigureDatabase()
        {
            Log("Openning connection");
            SQLiteConnection conn = new SQLiteConnection("Data Source=TriviaMazeDB.db;Version=3;"); 
            conn.Open();
            Log("Connection open");
            SQLiteCommand cmd = conn.CreateCommand();

            //Boolean data
            cmd.CommandText = "SELECT Prompt, Answer FROM BoolQuestions";
            SQLiteDataReader r = cmd.ExecuteReader();
            Log("Boolean Stuff: \n");
            while (r.Read())
            {
                BooleanPrompts.Add(Convert.ToString(r["Prompt"]));
                int BooleanAnswer = int.Parse(Convert.ToString(r["Answer"]));
                if(BooleanAnswer == 1)
                {
                    BooleanAnswers.Add(true);
                }
                else
                {
                    BooleanAnswers.Add(false);
                }
                Log(r.ToString());
            }
            r.Close();

            //Multiple Choice
            cmd.CommandText = "SELECT Prompt, Answer, FalseAnswer1, FalseAnswer2, FalseAnswer3 FROM ChoiceQuestions";
            r = cmd.ExecuteReader();
            Log("Multiple Choice Stuff: \n");
            while (r.Read())
            {
                ChoicePrompts.Add(Convert.ToString(r["Prompt"]));
                ChoiceCorrectAnswers.Add(Convert.ToString(r["Answer"]));
                ChoiceFalse1Answers.Add(Convert.ToString(r["FalseAnswer1"]));
                ChoiceFalse2Answers.Add(Convert.ToString(r["FalseAnswer2"]));
                ChoiceFalse3Answers.Add(Convert.ToString(r["FalseAnswer3"]));
            }
            r.Close();

            //Extended
            cmd.CommandText = "SELECT Prompt, Keyword FROM ExtendedQuestions";
            r = cmd.ExecuteReader();
            Log("Extended Response Stuff: \n");
            while (r.Read())
            {
                ExtendedPrompts.Add(Convert.ToString(r["Prompt"]));
                ExtendedAnswers.Add(Convert.ToString(r["Keyword"]));
            }
            r.Close();
        }

        //call to build a tile at the location passed in
        //TODO: fix softlock count. when we build a new room, we need to unlock the door we came from
        private void BuildNextTile(int x, int y)
        {
            if (GameMap[x, y] == null)
            {
                Log($"Creating tile <{x},{y}> and assigning to MyLoc");
                GameMap[x, y] = new TriviaTile(x, y);
                MyLoc = GameMap[x, y];
                //Log($"Adding {MyLoc.LocksCount} to lock total");
                SoftLocksRemaining += MyLoc.LocksCount;
                //Log($"Active SoftLocks - {SoftLocksRemaining}");
            }
            else
            {
                Log("Tile already exists, Updating MyLoc");
                MyLoc = GameMap[x, y];
            }
        }
        //overload to handle unlocking the door we came from
        private void BuildNextTile(int x, int y, Direction CameFrom)
        {
            if (GameMap[x, y] == null)
            {
                Log($"Creating tile <{x},{y}> and assigning to MyLoc");
                GameMap[x, y] = new TriviaTile(x, y);
                MyLoc = GameMap[x, y];
                //Log($"Adding {MyLoc.LocksCount} to lock total");
                switch (CameFrom)
                {
                    case Direction.North:
                        MyLoc.NorthLock = Lock.Unlocked;
                        break;
                    case Direction.South:
                        MyLoc.SouthLock = Lock.Unlocked;
                        break;
                    case Direction.East:
                        MyLoc.EastLock = Lock.Unlocked;
                        break;
                    case Direction.West:
                        MyLoc.WestLock = Lock.Unlocked;
                        break;
                }
                SoftLocksRemaining += MyLoc.LocksCount;
                //Log($"Active SoftLocks - {SoftLocksRemaining}");
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
            //bool rv = true;
            //YPos--;
            //check if invalid. if so, revert work done
            if (MyLoc.NorthLock == Lock.HardLock)
            {
                Log("Can't move up out of bounds or through a hard lock");
                //YPos++;
                return false;
            }
            else if (MyLoc.NorthLock == Lock.Unlocked)
            {
                Log("Moving through unlocked door");
                YPos--;
                BuildNextTile(XPos, YPos);
                return true;
            }
            else
            {
                // Move unobstructed, present question and adjust lock accordingly

                //BasicQuestion questionForm

                if (true) //assume question is answered correct for now
                {
                    MyLoc.NorthLock = Lock.Unlocked;
                    YPos--;
                    BuildNextTile(XPos, YPos, Direction.South);
                    SoftLocksRemaining -= 2; //2 locks, 1 is the door i opened, other is door i came through
                    Log($"Active Soft Locks - {SoftLocksRemaining}");
                    return true;
                }
                else //question answered wrong. can't move, revert position back and return false
                {
                    MyLoc.NorthLock = Lock.HardLock;
                    //YPos++;
                    SoftLocksRemaining--; //only the door i was trying to open
                    BuildNextTile(XPos, YPos);
                    return false;

                }
            }
            //before i build the next tile, i need to check locks and ask question
            //BuildNextTile(XPos, YPos);
            //return rv;
        }

        //call to move down one spot
        public bool MoveDown()
        {
            Log($"Attempting to move through {MyLoc.SouthLock}");
            //bool rv = true;
            //YPos++;
            //hard locks denote boundaries and failed unlock attempts. they are always impassable
            if (MyLoc.SouthLock == Lock.HardLock)
            {
                Log("Can't move down out of bounds or through a hard lock");
                //YPos--;
                return false;
            }
            else if (MyLoc.SouthLock == Lock.Unlocked)
            {
                Log("Moving through unlocked door");
                YPos++;
                BuildNextTile(XPos, YPos);
                return true;
            }
            else
            {
                //valid move, do work
                //SoftLocksRemaining--; //either way, we are converting a soft lock into something new
                if (true) //assume question is answered correct for now
                {
                    MyLoc.SouthLock = Lock.Unlocked;
                    YPos++;
                    BuildNextTile(XPos, YPos, Direction.North);
                    SoftLocksRemaining -= 2;
                    Log($"Active Soft Locks - {SoftLocksRemaining}");
                    return true;
                }
                else //question answered wrong. can't move, revert position back and return false
                {
                    MyLoc.SouthLock = Lock.HardLock;
                    //YPos--;
                    BuildNextTile(XPos, YPos);
                    SoftLocksRemaining--;
                    return false;

                }
            }
            //before i build the next tile, i need to check locks and ask question
            //BuildNextTile(XPos, YPos);
            //return rv;
        }
        public bool MoveLeft()
        {
            //bool rv = true;
            //XPos--;
            if (MyLoc.WestLock == Lock.HardLock)
            {
                Log("Can't move left out of bounds or through a hard lock");
                //XPos++;
                return false;
            }
            else if (MyLoc.WestLock == Lock.Unlocked)
            {
                Log("Moving through unlocked door");
                XPos--;
                BuildNextTile(XPos, YPos);
                return true;
            }
            else
            {
                //valid move, do work
                //SoftLocksRemaining--; //either way, we are converting a soft lock into something new
                if (true) //assume question is answered correct for now
                {
                    MyLoc.WestLock = Lock.Unlocked;
                    XPos--;
                    BuildNextTile(XPos, YPos, Direction.East);
                    SoftLocksRemaining -= 2;
                    Log($"Active Soft Locks - {SoftLocksRemaining}");
                    return true;
                }
                else //question answered wrong. can't move, revert position back and return false
                {
                    MyLoc.WestLock = Lock.HardLock;
                    //XPos++;
                    BuildNextTile(XPos, YPos);
                    SoftLocksRemaining--;
                    return false;

                }
            }
            //before i build the next tile, i need to check locks and ask 
            //BuildNextTile(XPos, YPos);
            //return rv;
        }
        public bool MoveRight()
        {
            //bool rv = true;
            //XPos++;
            if (MyLoc.EastLock == Lock.HardLock)
            {
                Log("Can't move down out of bounds or through a hard lock");
                //XPos--;
                return false;
            }
            else if (MyLoc.EastLock == Lock.Unlocked)
            {
                Log("Moving through unlocked door");
                XPos++;
                BuildNextTile(XPos, YPos);
                return true;
            }
            else
            {
                //valid move, do work
                //Randomize question/answer selection
                String[] array = {ChoicePrompts[0], ChoiceCorrectAnswers[0], ChoiceFalse1Answers[0], ChoiceFalse2Answers[0], ChoiceFalse3Answers[0]};
                Window newQuest = new BasicQuestion(array);
                newQuest.ShowDialog();
                //SoftLocksRemaining--; //either way, we are converting a soft lock into something new
                if (true) //assume question is answered correct for now
                {
                    MyLoc.EastLock = Lock.Unlocked;
                    XPos++;
                    BuildNextTile(XPos, YPos, Direction.West);
                    SoftLocksRemaining -= 2;
                    Log($"Active Soft Locks - {SoftLocksRemaining}");
                    return true;
                }
                else //question answered wrong. can't move, revert position back and return false
                {
                    MyLoc.EastLock = Lock.HardLock;
                    //XPos--;
                    BuildNextTile(XPos, YPos);
                    SoftLocksRemaining--;
                    return false;

                }
            }
            //before i build the next tile, i need to check locks and ask 
            //BuildNextTile(XPos, YPos);
            //return rv;
        }
        
        private void Log(String msg)
        {
            Debug.WriteLine(msg);
        }
        
        private enum Direction
        {
            North, South, East, West
        }
    }
}
