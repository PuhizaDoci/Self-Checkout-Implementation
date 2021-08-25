using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Data;

namespace ToshibaPosSinkronizimi
{
    public class Siguria
    {
        const string QelesiPerSiguri = "5M@R7P05";

        public enum Qasja
        {
            Lexim,
            Fshirje,
            Azhurim,
            Regjistrim,
            Asnje,
            TeGjitha
        }

        public static string EnkriptoStringun(string Mesazhi)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(QelesiPerSiguri));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(Mesazhi);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }

        public static string DekriptoStringun(string Mesazhi)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(QelesiPerSiguri));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            if (Mesazhi != null)
            {
                byte[] DataToDecrypt = Convert.FromBase64String(Mesazhi);
                try
                {
                    ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                    Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
                }
                finally
                {
                    TDESAlgorithm.Clear();
                    HashProvider.Clear();
                }
                return UTF8.GetString(Results);
            }
            else
                return null;
        }

        public static DataTable QasjetTabela { get; set; }

        public static bool KaQasjeNeModul(string emriIModulit, Qasja qasja)
        {
            bool kaQasje = false;
            foreach (DataRow Row in QasjetTabela.Rows)
            {
                if (Row[""].ToString() == emriIModulit)
                {
                    switch (qasja)
                    {
                        case Qasja.Lexim:
                            if (Convert.ToBoolean(Row["Sel"]))
                                kaQasje = true;
                            break;
                        case Qasja.Fshirje:
                            if (Convert.ToBoolean(Row[""]))
                                kaQasje = true;
                            break;
                        case Qasja.Azhurim:
                            if (Convert.ToBoolean(Row[""]))
                                kaQasje = true;
                            break;
                        case Qasja.Regjistrim:
                            if (Convert.ToBoolean(Row[""]))
                                kaQasje = true;
                            break;
                        case Qasja.Asnje:
                            if (Convert.ToBoolean(Row[""]))
                                kaQasje = true;
                            break;
                        case Qasja.TeGjitha:
                            if (Convert.ToBoolean(Row[""]))
                                kaQasje = true;
                            break;
                        default:
                            break;
                    }
                }
            }
            return kaQasje;
        }
    }
}
