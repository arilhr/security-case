using PacketData;
using System;
using System.Security.Cryptography;
using Xunit;

namespace unit_test
{
    public class AESUnitTest
    {
        [Fact]
        public void TestAESGenerateNewKey()
        {
            AesEncryptor aes = new AesEncryptor();
            string oldKey = Convert.ToBase64String(aes.aes.Key);
            aes.GenerateNewKey();
            string newKey = Convert.ToBase64String(aes.aes.Key);

            Assert.NotEqual(oldKey, newKey);
        }

        [Fact]
        public void TestAESEncryptDecrypt()
        {
            AesEncryptor aes = new AesEncryptor();

            string testText = "Test";
            string encryptedKey = aes.Encrypt(testText);
            string decryptedKey = aes.Decrypt(encryptedKey);

            Assert.Equal(decryptedKey, testText);
        }

        [Fact]
        public void TestAESSetKey()
        {
            AesEncryptor aes = new AesEncryptor();
            string oldKey = Convert.ToBase64String(aes.aes.Key);
            aes.GenerateNewKey();
            aes.SetKey(Convert.FromBase64String(oldKey));

            Assert.Equal(Convert.ToBase64String(aes.aes.Key), oldKey);
        }
    }
}
