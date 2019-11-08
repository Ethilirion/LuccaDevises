using System;
using LuccaDevises.Converter;
using Xunit;

namespace LuccaDevises.Test
{
    public class DeviseToConvert_TestClass : IDisposable
    {
        [Fact]
        public void DeviseSourceFormat()
        {
            try
            {
                new DeviseToConvert("", "", "");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseFormat);
            }
            try
            {
                new DeviseToConvert("azdaazdazd", "", "");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseFormat);
            }
            try
            {
                new DeviseToConvert("))", "", "");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseFormat);
            }
        }

        [Fact]
        public void DeviseCibleFormat()
        {
            try
            {
                new DeviseToConvert("DEF", "", "");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseFormat);
            }
            try
            {
                new DeviseToConvert("DEF", "azdaazdazd", "");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseFormat);
            }
            try
            {
                new DeviseToConvert("DEF", "))", "");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseFormat);
            }
            try
            {
                new DeviseToConvert("DEF", null, "");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseFormat);
            }
        }
        [Fact]
        public void ChangeRateFormat()
        {
            try
            {
                new DeviseToConvert("DEF", "", "DAF");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseChangeRateFormat);
            }
            try
            {
                new DeviseToConvert("DEF", "aaaa", "DAF");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseChangeRateFormat);
            }
            try
            {
                new DeviseToConvert("DEF", null, "DAF");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseChangeRateFormat);
            }
            try
            {
                new DeviseToConvert("DEF", "665678.22", "DAF");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseChangeRateFormat);
            }
            try
            {
                new DeviseToConvert("DEF", "665678.22765789", "DAF");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseChangeRateFormat);
            }
            try
            {
                new DeviseToConvert("DEF", "665678.8555", "DAF");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseChangeRateFormat);
            }
        }

        [Fact]
        public void ValidEntity()
        {
            var dcrt1 = new DeviseToConvert("EUR", "550", "JPY");
            Assert.Equal(dcrt1.ValueToConvert, 550);
            Assert.Equal(dcrt1.DeviseSource, "EUR");
            Assert.Equal(dcrt1.DeviseCible, "JPY");
        }

        public void Dispose()
        {

        }
    }
}
