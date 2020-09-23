using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace ChatJuanPabloPriotti.Test
{
    [TestClass]
    public class ChatTest
    {
        [TestMethod]
        public void TestCsvReader()
        {
            // Arrange
            Consumer.Consumer consumer = new Consumer.Consumer();
            string user = "Test User";
            string stock_code = "aapl.us";

            // Act
            var result = consumer.ReadCSV($"{user},{stock_code}");

            // Assert
            Assert.IsTrue(result);
        }
    }
}
