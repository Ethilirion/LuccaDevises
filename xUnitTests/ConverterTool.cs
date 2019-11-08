using LuccaDevises.Converter;
using System;
using System.Collections.Generic;
using Xunit;

namespace LuccaDevises.Test
{
    public class ConverterTool_TestClass : IDisposable
    {
        public ConverterTool_TestClass()
        {

        }

        [Fact]
        public void TestSlightlyHarderConversion()
        {
            List<string> workingLines = new List<string>();
            workingLines.Add("EUR;1000;JPY");
            workingLines.Add("2");
            workingLines.Add("EUR;CHF;0.5000");
            workingLines.Add("CHF;JPY;2.0000");
            var ldf = new LuccaDevisesFile(workingLines);
            var ct = new ConverterTool(ldf);
            int res = ct.ProcessChangeRate();
        }

        [Fact]
        public void TestEasyConversion()
        {
            List<string> workingLines = new List<string>();
            workingLines.Add("EUR;550;JPY");
            workingLines.Add("6");
            workingLines.Add("AUD;CHF;0.9661");
            workingLines.Add("JPY;KRW;13.1151");
            workingLines.Add("EUR;CHF;1.2053");
            workingLines.Add("AUD;JPY;86.0305");
            workingLines.Add("EUR;USD;1.2989");
            workingLines.Add("JPY;INR;0.6571");
            workingLines.Add("EUR;JPY;2.0000");
            var ldf = new LuccaDevisesFile(workingLines);
            var ct = new ConverterTool(ldf);
            int res = ct.ProcessChangeRate();
            Assert.True(res == 1100);
        }

        [Fact]
        public void TestSecondEasyConversion()
        {
            List<string> workingLines = new List<string>();
            workingLines.Add("EUR;550;JPY");
            workingLines.Add("6");
            workingLines.Add("AUD;CHF;0.9661");
            workingLines.Add("JPY;KRW;13.1151");
            workingLines.Add("EUR;CHF;1.2053");
            workingLines.Add("AUD;JPY;86.0305");
            workingLines.Add("EUR;USD;1.2989");
            workingLines.Add("JPY;INR;0.6571");
            workingLines.Add("JPY;EUR;2.0000");
            var ldf = new LuccaDevisesFile(workingLines);
            var ct = new ConverterTool(ldf);
            int res = ct.ProcessChangeRate();
            Assert.True(res == 275);
        }

        public void Dispose()
        {
        }
    }
}
