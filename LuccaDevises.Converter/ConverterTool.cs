using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuccaDevises.Converter
{
    public class ConverterTool
    {
        private LuccaDevisesFile _file;

        public ConverterTool(LuccaDevisesFile ldf)
        {
            _file = ldf;
        }

        public static double ApplyRate(DeviseChangeRateTuple dcrt, double val)
        {
            // A B T : B(T) = A*T
            return val * dcrt.DeviseChangeRate;
        }

        /// <summary>
        /// Apply an inverted change rate on a value
        /// </summary>
        /// <param name="dcrt">Change rate</param>
        /// <param name="val">Value to conver</param>
        /// <returns>The computed value depending on change rate</returns>
        public static double ApplyInvertRate(DeviseChangeRateTuple dcrt, double val)
        {
            // A B T : A(T) = B*(1/T)
            return val * 1 / dcrt.DeviseChangeRate;
        }

        public int FindChange()
        {
            double res = 0;
            var toConvert = _file.DeviseToConvert;
            foreach (var tuple in _file.DevisesChangeRateTuples)
            {
                if (toConvert.EqualsDevises(tuple))
                {
                    res = ConverterTool.ApplyRate(tuple, toConvert.ValueToConvert);
                    return Convert.ToInt32(res);
                }
            }
            return Convert.ToInt32(0);
        }
    }
}
