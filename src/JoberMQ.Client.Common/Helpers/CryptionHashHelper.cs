using System;
using System.Security.Cryptography;
using System.Text;

namespace JoberMQ.Client.Common.Helpers
{
    /// <summary>
    /// Bu yöntemler ile şifrelediğimiz veriye tekrar ulaşamayız.
    /// Metni aynı yöntem ile şifreleyerek, şifreli metinleri birbirleri ile aynımı diye kontrol edebiliriz.
    /// </summary>
    public class CryptionHashHelper
    {
        public static string MD5EnCryption(string SifrelenecekMetin)
        {
            using (MD5 sifre = MD5.Create())
            {
                byte[] arySifre = StringToByte(SifrelenecekMetin);
                byte[] aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash);
            }
        }

        public static string SHA1EnCryption(string SifrelenecekMetin)
        {
            using (SHA1 sifre = SHA1.Create())
            {
                byte[] arySifre = StringToByte(SifrelenecekMetin);
                byte[] aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash);
            }
        }

        public static string SHA256EnCryption(string SifrelenecekMetin)
        {
            using (SHA256 sifre = SHA256.Create())
            {
                byte[] arySifre = StringToByte(SifrelenecekMetin);
                byte[] aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash);
            }
        }

        public static string SHA384EnCryption(string SifrelenecekMetin)
        {
            using (SHA384 sifre = SHA384.Create())
            {
                byte[] arySifre = StringToByte(SifrelenecekMetin);
                byte[] aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash);
            }
        }

        public static string SHA512EnCryption(string SifrelenecekMetin)
        {
            using (SHA512 sifre = SHA512.Create())
            {
                byte[] arySifre = StringToByte(SifrelenecekMetin);
                byte[] aryHash = sifre.ComputeHash(arySifre);
                return BitConverter.ToString(aryHash);
            }
        }

        private static byte[] StringToByte(string deger)
        {
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            return ByteConverter.GetBytes(deger);
        }

    }
}
