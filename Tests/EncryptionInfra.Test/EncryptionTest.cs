using EncryptionInfra.Business.Services;

namespace EncryptionInfra.Test
{
    [TestClass]
    public class EncryptionTest
    {
        [TestMethod]
        [DataRow("testTEXT")]
        public void EncryptionTestMethod(string inputText)
        {
            var outputText = EncryptionService.Encrypt(inputText);
            var decryptedText = EncryptionService.Decrypt(outputText);

            Assert.AreEqual(inputText, decryptedText);
        }
    }
}