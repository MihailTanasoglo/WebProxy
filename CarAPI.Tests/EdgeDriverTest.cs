using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace CarAPI.Tests
{
    [TestClass]
    public class EdgeDriverTest
    {
        
        [TestMethod]
        public void VerifyPageTitle()
        {
            
            Assert.AreEqual(2+2, 4);
        }

        
    }
}
