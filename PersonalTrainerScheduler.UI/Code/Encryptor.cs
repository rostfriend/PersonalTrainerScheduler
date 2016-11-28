using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PersonalTrainerScheduler.UI.Code
{
   // Review TK: Don't forget to use access modifiers before classes.
   // I would prefer to have such interface as IServiceEncryptor and class MD5ServiceEncryptor.
    class Encryptor
    {
        public static string GetHash(string text)
        {
            using (MD5 cryptoProvider = new MD5CryptoServiceProvider())
            {

                byte[] result = cryptoProvider.ComputeHash(Encoding.Default.GetBytes(text));

                StringBuilder stringResult = new StringBuilder();

                for (int i = 0; i < result.Length; i++)
                {
                    stringResult.Append(result[i].ToString("x2"));
                }

                return stringResult.ToString();
            }
        }
    }
}
