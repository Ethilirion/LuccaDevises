using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LuccaDevises.Converter;

namespace LuccaDevises.Test
{
    [TestClass]
    public class LuccaDevisesFile_Test
    {
        [TestMethod]
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

            Assert.IsTrue(ldf.DevisesChangeRateTuples.Count == 6);
            Assert.IsTrue(ldf.NumberOfDevises == 6);
            Assert.IsTrue(ldf.DeviseToConvert.DeviseCible == "JPY");
            Assert.IsTrue(ldf.DeviseToConvert.DeviseSource == "JPY");
            Assert.IsTrue(ldf.DeviseToConvert.ValueToConvert == 550);
        }

        [TestMethod]
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
                Assert.IsTrue(e is MissingDevise);
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
                Assert.IsTrue(e is MissingDevise);
            }
        }

        [TestMethod]
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
                Assert.IsTrue(e is DoubleDeviseChangeRate);
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
                Assert.IsTrue(e is DoubleDeviseChangeRate);
            }
        }

        [TestMethod]
        public void LuccaDevisesFile_FailConstructor()
        {
            try
            {
                List<string> workingLines = null;
                var ldf = new LuccaDevisesFile(workingLines);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is IncorrectFile);
            }
            try
            {
                List<string> workingLines = new List<string>();
                workingLines.Add("EUR;550;JPY");
                var ldf = new LuccaDevisesFile(workingLines);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is IncorrectFile);
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
                Assert.IsTrue(e is MisformatLine);
            }
        }
    }
}
