using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LuccaDevises.Converter
{
    public struct DeviseToConvert
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
        /// <param name="valueToConvert"></param>
        /// <returns>TRUE If all is OK</returns>
        private bool CheckValueToConvertFormat(string valueToConvert)
        {
            Regex reg = new Regex(@"^\d+$");

            return !(string.IsNullOrEmpty(valueToConvert) || !reg.IsMatch(valueToConvert));
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

        private string _valueToConvert;
        public double ValueToConvert
        {
            get
            {
                return double.Parse(_valueToConvert, CultureInfo.InvariantCulture);
            }
        }

        public bool EqualsDevises(DeviseToConvert dtc)
        {
            return dtc.DeviseCible == this.DeviseCible && dtc.DeviseSource == this.DeviseSource;
        }

        public bool EqualsDevises(DeviseChangeRateTuple dtc)
        {
            return dtc.DeviseCible == this.DeviseCible && dtc.DeviseSource == this.DeviseSource;
        }

        public bool EqualsInvertedDevises(DeviseToConvert dtc)
        {
            return dtc.DeviseSource == this.DeviseCible && dtc.DeviseCible == this.DeviseSource;
        }

        public bool EqualsInvertedDevises(DeviseChangeRateTuple dtc)
        {
            return dtc.DeviseSource == this.DeviseCible && dtc.DeviseCible == this.DeviseSource;
        }

        /// <summary>
        /// An object containing the devise to convert
        /// </summary>
        /// <param name="deviseSource"></param>
        /// <param name="deviseCible"></param>
        /// <param name="changeRate"></param>
        /// <exception cref="InvalidDeviseChangeRateFormat">Change rate was on the wrong format</exception>
        /// <exception cref="InvalidDeviseFormat">A devise was on the wrong format</exception>
        public DeviseToConvert(string deviseSource, string changeRate, string deviseCible)
        {
            _deviseCible = string.Empty;
            _deviseSource = string.Empty;
            _valueToConvert = string.Empty;

            if (!CheckDeviseFormat(deviseSource))
                throw new InvalidDeviseFormat(deviseSource);
            _deviseSource = deviseSource;

            if (!CheckDeviseFormat(deviseCible))
                throw new InvalidDeviseFormat(deviseCible);
            _deviseCible = deviseCible;
            
            if (!CheckValueToConvertFormat(changeRate))
                throw new InvalidDeviseChangeRateFormat(changeRate);
            _valueToConvert = changeRate;
        }
    }
}
