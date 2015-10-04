using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FormatProviderLibrary;

namespace FormatProviderLibraryTests
{
    [TestClass]
    public class HexFormatProviderTest
    {
        [TestMethod]
        public void HexFormatter_WithArgumentEquals423_MustReturn1A7()
        {
            DecimalToHexTest(423, "1A7");
        }

        [TestMethod]
        public void HexFormatter_WithArgumentEquals163522_MustReturn27EC2()
        {
            DecimalToHexTest(163522, "27EC2");
        }

        [TestMethod]
        public void HexFormatter_WithArgumentEquals1027_MustReturn403()
        {
            DecimalToHexTest(1027, "403");
        }

        [TestMethod]
        public void HexFormatter_WithWrongFormatString_DoesntReturnHex()
        {
            int dec = 123;
            IFormatProvider provider = new DecimalToHexProvider();
            string expected = "00123";
            string actual = String.Format(provider, "{0:D5}", dec);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void HexFormatter_WithStringArgument_ThrownException()
        {
            IFormatProvider provider = new DecimalToHexProvider();
            string actual = String.Format(provider, "{0:Hx}", "00123");
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void HexFormatter_WithDoubleArgument_ThrownException()
        {
            IFormatProvider provider = new DecimalToHexProvider();
            string actual = String.Format(provider, "{0:Hx}", 123.552);
            Assert.Fail();
        }

        private void DecimalToHexTest(int dec, string expected)
        {
            IFormatProvider provider = new DecimalToHexProvider();
            string actual = String.Format(provider, "{0:Hx}", dec);
            Assert.AreEqual(expected, actual);
        }
    }
}
