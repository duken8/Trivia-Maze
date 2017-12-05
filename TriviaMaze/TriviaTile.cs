using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze
{
    /*
    * Represents a room where the user chooses which direction to go,
    * and is presented a question before being allowed to pass
    * 
    */
    public class TriviaTile
    {
        public int XCoord { get; private set; }
        public int YCoord { get; private set; }
        public Lock NorthLock { get; set; }
        public Lock SouthLock { get; set; }
        public Lock EastLock { get; set; }
        public Lock WestLock { get; set; }
        public int LocksCount { get; private set; }

        public TriviaTile(int x, int y)
        {
            LocksCount = 0;
            XCoord = x;
            YCoord = y;
            switch(x)
            {
                /* If x = 0, we are near the west boundary and need to hardlock it.
                 * If x = max, we are near the east boundary and need to hardlock it.
                 * otherwise, we are in the middle and both need to be softlocked.
                 */
                case 0:
                    EastLock = Lock.SoftLock;
                    WestLock = Lock.HardLock;
                    LocksCount++;
                    break;

                case 4:
                    EastLock = Lock.HardLock;
                    WestLock = Lock.SoftLock;
                    LocksCount++;
                    break;

                default:
                    EastLock = Lock.SoftLock;
                    WestLock = Lock.SoftLock;
                    LocksCount += 2;
                    break;
            }
            //same as x but with North/south gates
            switch(y)
            {
                case 0:
                    NorthLock = Lock.HardLock;
                    SouthLock = Lock.SoftLock;
                    LocksCount++;
                    break;

                case 4:
                    NorthLock = Lock.SoftLock;
                    SouthLock = Lock.HardLock;
                    LocksCount++;
                    break;

                default:
                    NorthLock = Lock.SoftLock;
                    SouthLock = Lock.SoftLock;
                    LocksCount += 2;
                    break;
            }
        }
    }
    /* Custom lock enum to denote the 3 possible states of a lock
     * HardLock = Permanent
     * SoftLock = Potential to unlock
     * Unlocked = Can traverse through
     */
    public enum Lock
    {
        HardLock, SoftLock, Unlocked
    }
}
