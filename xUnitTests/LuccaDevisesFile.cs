using System;
using System.Collections.Generic;
using LuccaDevises.Converter;
using Xunit;

namespace LuccaDevises.Test
{
    public class LuccaDevisesFile_Test : IDisposable
    {
        [Fact]
        public void LuccaDevisesFile_Constructor()
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
            var ldf = new LuccaDevisesFile(workingLines);

            Assert.True(ldf.DevisesChangeRateTuples.Count == 6);
            Assert.True(ldf.NumberOfDevises == 6);
            Assert.True(ldf.DeviseToConvert.DeviseCible == "JPY");
            Assert.True(ldf.DeviseToConvert.DeviseSource == "EUR");
            Assert.True(ldf.DeviseToConvert.ValueToConvert == 550);

            var ct = new ConverterTool(ldf);
            int res = ct.ProcessChangeRate();
            Assert.Equal(res, 59033);
        }

        [Fact]
        public void LuccaDevisesFile_MissingDevises()
        {
            try
            {
                List<string> workingLines = new List<string>();
                workingLines.Add("EUR;550;JOY");
                workingLines.Add("6");
                workingLines.Add("AUD;CHF;0.9661");
                workingLines.Add("JPY;KRW;13.1151");
                workingLines.Add("EUR;CHF;1.2053");
                workingLines.Add("AUD;JPY;86.0305");
                workingLines.Add("EUR;USD;1.2989");
                workingLines.Add("JPY;INR;0.6571");
                var ldf = new LuccaDevisesFile(workingLines);
            }
            catch (Exception e)
            {
                Assert.True(e is MissingDevise);
            }
            try
            {
                List<string> workingLines = new List<string>();
                workingLines.Add("EUF;550;JPY");
                workingLines.Add("6");
                workingLines.Add("AUD;CHF;0.9661");
                workingLines.Add("JPY;KRW;13.1151");
                workingLines.Add("EUR;CHF;1.2053");
                workingLines.Add("AUD;JPY;86.0305");
                workingLines.Add("EUR;USD;1.2989");
                workingLines.Add("JPY;INR;0.6571");
                var ldf = new LuccaDevisesFile(workingLines);
            }
            catch (Exception e)
            {
                Assert.True(e is MissingDevise);
            }
        }

        [Fact]
        public void LuccaDevisesFile_DoubleDevises()
        {
            try
            {
                List<string> workingLines = new List<string>();
                workingLines.Add("EUF;550;JPY");
                workingLines.Add("6");
                workingLines.Add("AUD;CHF;0.9661");
                workingLines.Add("JPY;KRW;13.1151");
                workingLines.Add("JPY;KRW;13.1151");
                workingLines.Add("EUR;CHF;1.2053");
                workingLines.Add("AUD;JPY;86.0305");
                workingLines.Add("EUR;USD;1.2989");
                workingLines.Add("JPY;INR;0.6571");
                var ldf = new LuccaDevisesFile(workingLines);
            }
            catch (Exception e)
            {
                Assert.True(e is DoubleDeviseChangeRate);
            }
            try
            {
                List<string> workingLines = new List<string>();
                workingLines.Add("EUF;550;JPY");
                workingLines.Add("6");
                workingLines.Add("AUD;CHF;0.9661");
                workingLines.Add("JPY;KRW;13.1151");
                workingLines.Add("KRW;JPY;13.1151");
                workingLines.Add("EUR;CHF;1.2053");
                workingLines.Add("AUD;JPY;86.0305");
                workingLines.Add("EUR;USD;1.2989");
                workingLines.Add("JPY;INR;0.6571");
                var ldf = new LuccaDevisesFile(workingLines);
            }
            catch (Exception e)
            {
                Assert.True(e is DoubleDeviseChangeRate);
            }
        }

        [Fact]
        public void LuccaDevisesFile_FailConstructor()
        {
            try
            {
                List<string> workingLines = null;
                var ldf = new LuccaDevisesFile(workingLines);
            }
            catch (Exception e)
            {
                Assert.True(e is IncorrectFile);
            }
            try
            {
                List<string> workingLines = new List<string>();
                workingLines.Add("EUR;550;JPY");
                var ldf = new LuccaDevisesFile(workingLines);
            }
            catch (Exception e)
            {
                Assert.True(e is IncorrectFile);
            }
            try
            {
                List<string> workingLines = new List<string>();
                workingLines.Add("EUR");
                workingLines.Add("EUR");
                workingLines.Add("EUR");
                workingLines.Add("EUR");
                var ldf = new LuccaDevisesFile(workingLines);
            }
            catch (Exception e)
            {
                Assert.True(e is MisformatLine);
            }
        }

        public void Dispose()
        {
        }
    }
}
