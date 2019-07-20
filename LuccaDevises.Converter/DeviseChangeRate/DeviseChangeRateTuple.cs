using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace LuccaDevises.Converter
{
    public struct DeviseChangeRateTuple
    {
        #region business rules for format
        /// <summary>
        /// Check the format for devise
        /// </summary>
        /// <param name="devise"></param>
        /// <returns>TRUE If all is OK</returns>
        private bool CheckDeviseFormat(string devise)
        {
            return !(string.IsNullOrEmpty(devise) || devise.Length != 3 || devise.Any(a => char.IsLetter(a) == false));
        }

        /// <summary>
        /// Check the format for change rate (\d+.\d{4})
        /// </summary>
        /// <param name="changeRate"></param>
        /// <returns>TRUE If all is OK</returns>
        private bool CheckChangeRateFormat(string changeRate)
        {
            Regex reg = new Regex(@"^\d+\.\d{4}$");
            
            return !(string.IsNullOrEmpty(changeRate) || !reg.IsMatch(changeRate));
        }
        #endregion

        private string _deviseSource;
        public string DeviseSource
        {
            get
            {
                return _deviseSource;
            }
        }

        private string _deviseCible;
        public string DeviseCible
        {
            get
            {
                return _deviseCible;
            }
        }

        private string _deviseChangeRate;
        public double DeviseChangeRate
        {
            get
            {
                return double.Parse(_deviseChangeRate, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// An object containing a devise change rate
        /// </summary>
        /// <param name="deviseSource"></param>
        /// <param name="deviseCible"></param>
        /// <param name="changeRate"></param>
        /// <exception cref="InvalidDeviseChangeRateFormat">Change rate was on the wrong format</exception>
        /// <exception cref="InvalidDeviseFormat">A devise was on the wrong format</exception>
        /// <exception cref="DuplicateDevise">Cible & source (target and origin) cannotbe the same</exception>
        public DeviseChangeRateTuple(string deviseSource, string deviseCible, string changeRate)
        {
            _deviseCible = string.Empty;
            _deviseSource = string.Empty;
            _deviseChangeRate = string.Empty;

            if (!CheckDeviseFormat(deviseSource))
                throw new InvalidDeviseFormat(deviseSource);
            _deviseSource = deviseSource;

            if (!CheckDeviseFormat(deviseCible))
                throw new InvalidDeviseFormat(deviseCible);
            _deviseCible = deviseCible;

            if (_deviseSource == _deviseCible)
                throw new DuplicateDevise(_deviseSource);

            if (!CheckChangeRateFormat(changeRate))
                throw new InvalidDeviseChangeRateFormat(changeRate);
            _deviseChangeRate = changeRate;
        }
    }
}