using System;
using Xunit;

namespace CarAPITest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(4, 2+2);

        }
        [Fact]
        public void Test2()
        {
            Assert.Equal(5, 3 + 2);

        }
    }
}
