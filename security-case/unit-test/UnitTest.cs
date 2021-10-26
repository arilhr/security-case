using PacketData;
using System;
using System.Security.Cryptography;
using Xunit;

namespace unit_test
{
    public class UnitTest
    {
        [Fact]
        public void Test_RSA_Generate_New_Key()
        {
            RsaEncryption enc = new RsaEncryption();
            enc.GenerateKey();

            Assert.NotNull(enc.privateKey.Modulus);
            Assert.NotNull(enc.publicKey.Modulus);
        }

        [Fact]
        public void Test_RSA_Convert_Key()
        {
            RsaEncryption enc = new RsaEncryption();
            enc.GenerateKey();

            string publicKeyString = enc.ConvertKeyToString(enc.publicKey);
            Assert.Equal(enc.ConvertStringToKey(publicKeyString).Modulus, enc.publicKey.Modulus);

            string privateKeyString = enc.ConvertKeyToString(enc.privateKey);
            Assert.Equal(enc.ConvertStringToKey(privateKeyString).Modulus, enc.privateKey.Modulus);
        }

        [Fact]
        public void Test_RSA_Encrypt_Decrypt()
        {
            RsaEncryption enc = new RsaEncryption();
            enc.GenerateKey();

            string testText = "Test";

            // encrypt
            string encryptedText = enc.Encrypt(testText);

            // decrypt
            string decryptedText = enc.Decrypt(encryptedText);

            Assert.Equal(decryptedText, testText);
        }

        [Fact]
        public void Test_AES_Generate_New_Key()
        {
            AesEncryptor aes = new AesEncryptor();
            string oldKey = Convert.ToBase64String(aes.aes.Key);
            aes.GenerateNewKey();
            string newKey = Convert.ToBase64String(aes.aes.Key);

            Assert.NotEqual(oldKey, newKey);
        }

        [Fact]
        public void Test_AES_Encrypt_Decrypt()
        {
            AesEncryptor aes = new AesEncryptor();

            string testText = "Test";
            string encryptedKey = aes.Encrypt(testText);
            string decryptedKey = aes.Decrypt(encryptedKey);

            Assert.Equal(decryptedKey, testText);
        }

        [Fact]
        public void Test_AES_Set_Key()
        {
            AesEncryptor aes = new AesEncryptor();
            string oldKey = Convert.ToBase64String(aes.aes.Key);
            aes.GenerateNewKey();
            aes.SetKey(Convert.FromBase64String(oldKey));

            Assert.Equal(Convert.ToBase64String(aes.aes.Key), oldKey);
        }
    }
}
