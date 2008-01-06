using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Common
{
    public static class MathHelper
    {
        /// <summary>
        /// Does a number have more then 'places' number of decimal places?
        /// </summary>
        /// <param name="input"></param>
        /// <param name="places"></param>
        /// <returns></returns>
        public static bool CheckDecimalPlaces(decimal input, int places)
        {
            decimal round = decimal.Round(input, places, MidpointRounding.AwayFromZero);
            return input != round;
        }

    }
}
