using LuccaDevises.Converter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuccaDevises.Test
{
    [TestClass]
    public class ConverterTool_TestClass
    {
        [TestMethod]
        public void TestEasyConversion()
        {
            try
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
                int res = ct.FindChange();
                Assert.IsTrue(res == 1100);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }

            try
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
                int res = ct.FindChange();
                Assert.IsTrue(res == 275);
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
        }
    }
}
