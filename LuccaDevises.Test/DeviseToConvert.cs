using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LuccaDevises.Converter;

namespace LuccaDevises.Test
{
    [TestClass]
    public class DeviseToConvert_TestClass
    {
        [TestMethod]
        public void DeviseSourceFormat()
        {
            try
            {
                new DeviseToConvert("", "", "");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseFormat);
            }
            try
            {
                new DeviseToConvert("azdaazdazd", "", "");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseFormat);
            }
            try
            {
                new DeviseToConvert("))", "", "");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseFormat);
            }
        }

        [TestMethod]
        public void DeviseCibleFormat()
        {
            try
            {
                new DeviseToConvert("DEF", "", "");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseFormat);
            }
            try
            {
                new DeviseToConvert("DEF", "azdaazdazd", "");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseFormat);
            }
            try
            {
                new DeviseToConvert("DEF", "))", "");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseFormat);
            }
            try
            {
                new DeviseToConvert("DEF", null, "");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseFormat);
            }
        }
        [TestMethod]
        public void ChangeRateFormat()
        {
            try
            {
                new DeviseToConvert("DEF", "", "DAF");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseChangeRateFormat);
            }
            try
            {
                new DeviseToConvert("DEF", "aaaa", "DAF");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseChangeRateFormat);
            }
            try
            {
                new DeviseToConvert("DEF", null, "DAF");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseChangeRateFormat);
            }
            try
            {
                new DeviseToConvert("DEF", "665678.22", "DAF");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseChangeRateFormat);
            }
            try
            {
                new DeviseToConvert("DEF", "665678.22765789", "DAF");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseChangeRateFormat);
            }
            try
            {
                new DeviseToConvert("DEF", "665678.8555", "DAF");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseChangeRateFormat);
            }
        }

        [TestMethod]
        public void ValidEntity()
        {
            var dcrt1 = new DeviseToConvert("EUR", "550", "JPY");
            Assert.AreEqual(dcrt1.ValueToConvert, 550);
            Assert.AreEqual(dcrt1.DeviseSource, "EUR");
            Assert.AreEqual(dcrt1.DeviseCible, "JPY");
        }
    }
}
