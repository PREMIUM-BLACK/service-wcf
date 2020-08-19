using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PremiumBlack.Service.WCF
{
    public partial class EncryptedClass
    {
        public virtual bool CheckHash(String password)
        {
            //Sort alphabetical
            var members = this.GetType().GetProperties().OrderBy(a => a.Name);

            try
            {
                String h = "";
                foreach (var m in members)
                {
                    if (m.Name == "Hash")
                        continue;

                    var v = m.GetValue(this) as String;

                    if (v == null)
                        continue;

                    h += v;
                }

                return (SHA256String(h + password).ToLower() == this.Hash);
            }
            catch
            {

            }

            return false;
        }

        public virtual String GetHash(String password)
        {
            //Sort alphabetical
            var members = this.GetType().GetProperties().OrderBy(a => a.Name);

            try
            {
                String h = "";
                foreach (var m in members)
                {
                    if (m.Name == "Hash")
                        continue;

                    var v = m.GetValue(this) as String;

                    if (v == null)
                        continue;

                    h += v;
                }

                return SHA256String(h + password).ToLower();
            }
            catch
            {

            }

            return "";
        }

        public virtual void HashData(String password)
        {
            this.Hash = GetHash(password);
        }

        public static string SHA256String(string str)
        {
            byte[] b = System.Text.Encoding.ASCII.GetBytes(str);
            var sha = new SHA256Managed();
            byte[] checksum = sha.ComputeHash(b);
            return BitConverter.ToString(checksum).Replace("-", String.Empty);
        }

    }
}
