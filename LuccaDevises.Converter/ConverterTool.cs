using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public static bool DeviseInPathList(string deviseCible, List<DeviseChangeRateTuple> currentPath)
        {
            return currentPath.Any(dcrt => dcrt.DeviseSource == deviseCible || dcrt.DeviseCible == deviseCible);
        }

        private List<DeviseChangeRateTuple> PopulatePaths(string deviseCible, string deviseSource, ReadOnlyCollection<DeviseChangeRateTuple> fullTupleList, List<DeviseChangeRateTuple> currentPath)
        {
            if (currentPath.Count() > fullTupleList.Count()) return null;
            if (DeviseInPathList(deviseCible, currentPath) && DeviseInPathList(deviseSource, currentPath)) return currentPath;

            var lastSource = currentPath.Last().DeviseSource;
            var lastCible = currentPath.Last().DeviseCible;
            var nextSourceDeviseInChain = fullTupleList.Where(tuple => currentPath.Any(dcrt => dcrt.Equals(tuple)) == false)
                .Where(tuple => tuple.DeviseSource == lastSource || tuple.DeviseCible == lastSource)
                .FirstOrDefault();
            var nextCibleDeviseInChain = fullTupleList.Where(tuple => currentPath.Any(dcrt => dcrt.Equals(tuple)) == false)
                .Where(tuple => tuple.DeviseSource == lastCible || tuple.DeviseCible == lastCible)
                .FirstOrDefault();
            List<DeviseChangeRateTuple> nextPathFromSource = null;
            List<DeviseChangeRateTuple> nextPathFromCible = null;
            if (nextSourceDeviseInChain.Equals(default(DeviseChangeRateTuple)) == false)
            {
                var sourcePath = new List<DeviseChangeRateTuple>(currentPath);
                sourcePath.Add(nextSourceDeviseInChain);
                nextPathFromSource = PopulatePaths(deviseCible, deviseSource, fullTupleList, sourcePath);
            }
            if (nextCibleDeviseInChain.Equals(default(DeviseChangeRateTuple)) == false)
            {
                var ciblePath = new List<DeviseChangeRateTuple>(currentPath);
                ciblePath.Add(nextCibleDeviseInChain);
                nextPathFromCible = PopulatePaths(deviseCible, deviseSource, fullTupleList, ciblePath);
            }
            if (nextPathFromSource == null && nextPathFromCible == null) return null;
            if (nextPathFromSource == null && nextPathFromCible != null) return nextPathFromCible;
            if (nextPathFromSource != null && nextPathFromCible == null) return nextPathFromSource;
            if (nextPathFromSource.Count < nextPathFromCible.Count) return nextPathFromSource;
            if (nextPathFromCible.Count <= nextPathFromSource.Count) return nextPathFromCible;
            
            return null;
        }

        public int ProcessChangeRate()
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
                if (toConvert.EqualsInvertedDevises(tuple))
                {
                    res = ConverterTool.ApplyInvertRate(tuple, toConvert.ValueToConvert);
                    return Convert.ToInt32(res);
                }
            }

            ReadOnlyCollection<DeviseChangeRateTuple> fullTupleList = _file.DevisesChangeRateTuples.AsReadOnly();
            List<List<DeviseChangeRateTuple>> resultPaths = new List<List<DeviseChangeRateTuple>>();
            foreach (var tuple in _file.DevisesChangeRateTuples)
            {
                var pathFound = PopulatePaths(toConvert.DeviseCible, toConvert.DeviseSource, fullTupleList, new List<DeviseChangeRateTuple> { tuple });
                if (pathFound != null)
                    resultPaths.Add(pathFound);
            }


#if DEBUG
            foreach (var path in resultPaths)
            {
                Console.WriteLine(string.Join("=>", path.Select(a => $"{a.DeviseSource}-{a.DeviseCible}-{a.DeviseChangeRate}")));
            }
#endif

            if (resultPaths.Count == 1) return ProcessPath(resultPaths[0], toConvert.ValueToConvert);
            else if (resultPaths.Count > 1) return ProcessPath(resultPaths.OrderBy(rp => rp.Count).First(), toConvert.ValueToConvert);
            return Convert.ToInt32(0);
        }

        private string FindLink(DeviseChangeRateTuple source, DeviseChangeRateTuple cible)
        {
            if (source.DeviseSource == cible.DeviseCible || source.DeviseSource == cible.DeviseSource)
                return source.DeviseSource;
            if (source.DeviseCible == cible.DeviseCible || source.DeviseCible == cible.DeviseSource)
                return source.DeviseCible;
            return null;
        }

        private int ProcessPath(List<DeviseChangeRateTuple> listChangeRates, double valueToConvert)
        {
#if DEBUG
            Console.WriteLine($"Chosen path : ");
            Console.WriteLine($"{string.Join("=>", listChangeRates.Select(a => $"{a.DeviseSource}-{a.DeviseCible}-{a.DeviseChangeRate}"))}");
#endif
            string chainDevise = string.Empty;

            var currentValue = valueToConvert; 
            for (int i = 0; i < listChangeRates.Count; i++)
            {
                var currentItem = listChangeRates[i];
                var nextItem = (i + 1 >= listChangeRates.Count ? default(DeviseChangeRateTuple) : listChangeRates[i + 1]);
                // Last item
                if (nextItem.Equals(default(DeviseChangeRateTuple)))
                {
                    if (chainDevise == currentItem.DeviseCible)
                    {
                        currentValue = Math.Round(currentValue * Math.Round(1.0 / currentItem.DeviseChangeRate, 4), 4);
                    }
                    else if (chainDevise == currentItem.DeviseSource)
                    {
                        currentValue = Math.Round(currentValue * currentItem.DeviseChangeRate, 4);
                    }
                }
                // All befores
                else
                {
                    chainDevise = FindLink(currentItem, nextItem);
                    if (chainDevise == currentItem.DeviseSource)
                    {
                        currentValue = Math.Round(currentValue * Math.Round(1.0 / currentItem.DeviseChangeRate, 4), 4);
                    }
                    else if (chainDevise == currentItem.DeviseCible)
                    {
                        currentValue = Math.Round(currentValue * currentItem.DeviseChangeRate, 4);
                    }
                }
            }
            return Convert.ToInt32(Math.Round(currentValue));
        }
    }
}
