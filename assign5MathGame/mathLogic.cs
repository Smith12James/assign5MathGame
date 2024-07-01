using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assign5MathGame
{
    class mathLogic
    {
        public int[] add()
        {
            Random rand = new Random();

            int iFirst = rand.Next(0, 11);
            int iSecond = rand.Next(0, 11);

            int iAnswer = iFirst + iSecond;

            int[] addArray = { iFirst, iSecond, iAnswer };

            return addArray;

        }

    }
}
