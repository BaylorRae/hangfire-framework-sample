using NUnit.Framework;

namespace testbase
{
    public class TestBase
    {
        [TearDown]
        public void FlushLetHelpers()
        {
            LetTestHelper.LetHelper.Flush();
        }
    }
}