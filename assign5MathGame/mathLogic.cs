using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assign5MathGame
{
    /// <summary>
    /// this class holds all game play math equation logic. Below is how they line up:
    /// add() --> addition
    /// sub() --> subtraction
    /// multi() --> multiplication
    /// divis() --> division
    /// </summary>
    class mathLogic
    {

        /// <summary>
        /// This method creates two random numbers, adds them together, and returns an array with the two numbers, and the answer.
        /// </summary>
        /// <returns></returns>
        public int[] add()
        {
            Random rand = new Random();

            int iFirst = rand.Next(0, 11);
            int iSecond = rand.Next(0, 11);

            int iAnswer = iFirst + iSecond;

            int[] addArray = { iFirst, iSecond, iAnswer };

            return addArray;

        }

        /// <summary>
        ///  This method creates two random numbers, checks which is the bigger of the two and assigns that as index 0, subtracts the two numbers for the answer, and returns an array with the two numbers, and the answer.
        /// </summary>
        /// <returns></returns>
        public int[] sub()
        {
            Random rand = new Random();

            int iFirst = rand.Next(0, 11);
            int iSecond = rand.Next(0, 11);

            if (iFirst > iSecond)
            {
                int iAnswer = iFirst - iSecond;

                int[] subArray = { iFirst, iSecond, iAnswer };

                return subArray;

            } else if (iFirst < iSecond)
            {
                int iAnswer = iSecond - iFirst;

                int[] subArray = { iSecond, iFirst, iAnswer };

                return subArray;

            } else
            {
                int iAnswer = iFirst - iSecond;

                int[] subArray = { iFirst, iSecond, iAnswer };

                return subArray;

            }

        }

        /// <summary>
        /// multi is short for multiplication. This method creates two random numbers, multiplies them, and returns an array with the two numbers, and the answer
        /// </summary>
        /// <returns></returns>
        public int[] multi()
        {
            Random rand = new Random();

            int iFirst = rand.Next(0, 11);
            int iSecond = rand.Next(0, 11);

            int iAnswer = iFirst * iSecond;

            int[] multiArray = { iFirst, iSecond, iAnswer };

            return multiArray;

        }

        /// <summary>
        /// divis is short for division. This method creates two random numbers between 1 and 10 for the first number as the divisor cannot be 0, and 0 and 10 for the second number.
        /// These numbers are multiplied as iTotal and then the final answer is divided as iTotal / iFirst = iAnswer. The returned array is the total, the first number, and the answer.
        /// </summary>
        /// <returns></returns>
        public int[] divis()
        {
            Random rand = new Random();

            int iFirst = rand.Next(1, 11);
            int iSecond = rand.Next(0, 11);

            int iTotal = iFirst * iSecond;
            int iAnswer = iTotal / iFirst;

            int[] divisArray = { iTotal, iFirst, iAnswer };

            return divisArray;

        }



    }
}
