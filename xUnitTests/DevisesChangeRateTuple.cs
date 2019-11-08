using LuccaDevises.Converter;
using System;
using Xunit;

namespace LuccaDevises.Test
{
    public class DeviseChangeRateTuple_TestClass : IDisposable
    {
        [Fact]
        public void DeviseSourceFormat()
        {
            try
            {
                new DeviseChangeRateTuple("", "", "");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseFormat);
            }
            try
            {
                new DeviseChangeRateTuple("azdaazdazd", "", "");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseFormat);
            }
            try
            {
                new DeviseChangeRateTuple("))", "", "");
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
                new DeviseChangeRateTuple("DEF", "", "");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseFormat);
            }
            try
            {
                new DeviseChangeRateTuple("DEF", "azdaazdazd", "");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseFormat);
            }
            try
            {
                new DeviseChangeRateTuple("DEF", "))", "");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseFormat);
            }
            try
            {
                new DeviseChangeRateTuple("DEF", null, "");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseFormat);
            }
        }

        [Fact]
        public void DuplicateDevise()
        {
            try
            {
                new DeviseChangeRateTuple("DEF", "DEF", "");
            }
            catch (Exception e)
            {
                Assert.True(e is DuplicateDevise);
            }
        }

        [Fact]
        public void ChangeRateFormat()
        {
            try
            {
                new DeviseChangeRateTuple("DEF", "DAF", "");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseChangeRateFormat);
            }
            try
            {
                new DeviseChangeRateTuple("DEF", "DAF", "aaaa");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseChangeRateFormat);
            }
            try
            {
                new DeviseChangeRateTuple("DEF", "DAF", null);
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseChangeRateFormat);
            }
            try
            {
                new DeviseChangeRateTuple("DEF", "DAF", "665678");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseChangeRateFormat);
            }
            try
            {
                new DeviseChangeRateTuple("DEF", "DAF", "665678.22");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseChangeRateFormat);
            }
            try
            {
                new DeviseChangeRateTuple("DEF", "DAF", "665678.22765789");
            }
            catch (Exception e)
            {
                Assert.True(e is InvalidDeviseChangeRateFormat);
            }
        }

        [Fact]
        public void ValidEntity()
        {
            var dcrt1 = new DeviseChangeRateTuple("DEF", "DAF", "665678.2273");
            Assert.Equal(dcrt1.DeviseChangeRate, 665678.2273);
            Assert.Equal(dcrt1.DeviseSource, "DEF");
            Assert.Equal(dcrt1.DeviseCible, "DAF");
        }

        public void Dispose()
        {
        }
    }
}
