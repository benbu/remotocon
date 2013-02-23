using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Remotocon.Server
{
    public class Encryption
    {
        private static readonly string DefaultKey = "!N5g(g3f37k.;''$RNB<>spe";
        private static readonly String DefaultIV = "12345678";
        private byte[] key;
        private byte[] iv;

        public Encryption()
        {
            key = UTF8Encoding.UTF8.GetBytes(Encryption.DefaultKey);
            iv = UTF8Encoding.UTF8.GetBytes(Encryption.DefaultIV);
        }

        public Encryption(string username, string password)
        {
            string tempKey = username + password + Encryption.DefaultKey;
            if(tempKey.Length > Encryption.DefaultKey.Length)
                tempKey = tempKey.Remove(Encryption.DefaultKey.Length);
            key = UTF8Encoding.UTF8.GetBytes(tempKey);
            iv = UTF8Encoding.UTF8.GetBytes(Encryption.DefaultIV);
        }

        public byte[] Encrypt(byte[] toEncryptArray)
        {
            if (toEncryptArray.Length < 1)
                return toEncryptArray;

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = this.key;
            tdes.IV = this.iv;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.CBC;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return resultArray;
        }

        public byte[] Decrypt(byte[] encryptedArray)
        {
            if (encryptedArray.Length < 1)
                return encryptedArray;

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = this.key;
            tdes.IV = this.iv;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.CBC;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(encryptedArray, 0, encryptedArray.Length);

            //Release resources held by TripleDes Encryptor                
            tdes.Clear();

            return resultArray;
        }

        public string EncryptToString(string p)
        {
            return Convert.ToBase64String(Encrypt(UTF8Encoding.UTF8.GetBytes(p)));
        }

        public string DecryptToString(string p)
        {
            return UTF8Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(p)));
        }
    }
}
