using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LuccaDevises.Converter;

namespace LuccaDevises.Test
{
    [TestClass]
    public class DeviseChangeRateTuple_TestClass
    {
        [TestMethod]
        public void DeviseSourceFormat()
        {
            try
            {
                new DeviseChangeRateTuple("", "", "");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseFormat);
            }
            try
            {
                new DeviseChangeRateTuple("azdaazdazd", "", "");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseFormat);
            }
            try
            {
                new DeviseChangeRateTuple("))", "", "");
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
                new DeviseChangeRateTuple("DEF", "", "");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseFormat);
            }
            try
            {
                new DeviseChangeRateTuple("DEF", "azdaazdazd", "");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseFormat);
            }
            try
            {
                new DeviseChangeRateTuple("DEF", "))", "");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseFormat);
            }
            try
            {
                new DeviseChangeRateTuple("DEF", null, "");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseFormat);
            }
        }

        [TestMethod]
        public void DuplicateDevise()
        {
            try
            {
                new DeviseChangeRateTuple("DEF", "DEF", "");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is DuplicateDevise);
            }
        }

        [TestMethod]
        public void ChangeRateFormat()
        {
            try
            {
                new DeviseChangeRateTuple("DEF", "DAF", "");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseChangeRateFormat);
            }
            try
            {
                new DeviseChangeRateTuple("DEF", "DAF", "aaaa");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseChangeRateFormat);
            }
            try
            {
                new DeviseChangeRateTuple("DEF", "DAF", null);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseChangeRateFormat);
            }
            try
            {
                new DeviseChangeRateTuple("DEF", "DAF", "665678");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseChangeRateFormat);
            }
            try
            {
                new DeviseChangeRateTuple("DEF", "DAF", "665678.22");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseChangeRateFormat);
            }
            try
            {
                new DeviseChangeRateTuple("DEF", "DAF", "665678.22765789");
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is InvalidDeviseChangeRateFormat);
            }
        }

        [TestMethod]
        public void ValidEntity()
        {
            var dcrt1 = new DeviseChangeRateTuple("DEF", "DAF", "665678.2273");
            Assert.AreEqual(dcrt1.DeviseChangeRate, 665678.2273);
            Assert.AreEqual(dcrt1.DeviseSource, "DEF");
            Assert.AreEqual(dcrt1.DeviseCible, "DAF");
        }
    }
}
