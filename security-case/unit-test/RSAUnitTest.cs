using PacketData;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace unit_test
{
    public class RSAUnitTest
    {
        [Fact]
        public void TestRSAGenerateNewKey()
        {
            RsaEncryption enc = new RsaEncryption();
            enc.GenerateKey();

            Assert.NotNull(enc.privateKey.Modulus);
            Assert.NotNull(enc.publicKey.Modulus);
        }

        [Fact]
        public void TestRSAConvertKeyToString()
        {
            RsaEncryption enc = new RsaEncryption();
            enc.GenerateKey();

            string publicKeyString = enc.ConvertKeyToString(enc.publicKey);
            Assert.Equal(enc.ConvertStringToKey(publicKeyString).Modulus, enc.publicKey.Modulus);

            string privateKeyString = enc.ConvertKeyToString(enc.privateKey);
            Assert.Equal(enc.ConvertStringToKey(privateKeyString).Modulus, enc.privateKey.Modulus);
        }

        [Fact]
        public void TestRSAEncryptData()
        {
            RsaEncryption enc = new RsaEncryption();
            enc.GenerateKey();
            enc.Encrypt("Test");
        }

        [Fact]
        public void TestRSADecryptData()
        {
            RsaEncryption enc = new RsaEncryption();
            enc.GenerateKey();

            string testText = "This is decrypt testing";
            string encrypted = enc.Encrypt(testText);
            string decrypted = enc.Decrypt(encrypted);

            Assert.Equal(decrypted, testText);
        }
    }
}
