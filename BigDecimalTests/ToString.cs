using NUnit.Framework;
using System.Collections;

namespace BigDecimalTests
{
    public class Tests
    {
        [TestCaseSource("ToStringCases")]
        public void ToString(decimal number)
        {
            BigDecimal bigDecimal = number;
            Assert.AreEqual(number.ToString(), bigDecimal.ToString());
        }

        public static IEnumerable ToStringCases()
        {
            yield return new TestCaseData(12300m);
            yield return new TestCaseData(1230m);
            yield return new TestCaseData(123m);
            yield return new TestCaseData(12.3m);
            yield return new TestCaseData(1.23m);
            yield return new TestCaseData(0.123m);
            yield return new TestCaseData(0.0123m);
            yield return new TestCaseData(0.00123m);
        }
    }
}