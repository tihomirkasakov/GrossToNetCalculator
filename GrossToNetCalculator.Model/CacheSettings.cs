using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace GrossToNetCalculator.Model
{
    public abstract class CacheSettings
    {
        public virtual string GetSha256Hash<T>()
        {
            Type type = this.GetType();
            PropertyInfo[] props = type.GetProperties();
            StringBuilder sb = new StringBuilder();

            foreach (var prop in props)
            {
                sb.Append(prop.GetValue(this));
            }

            byte[] bytes = Encoding.Unicode.GetBytes(sb.ToString());
            SHA256 sha = new SHA256Managed();
            byte[] hashBytes = sha.ComputeHash(bytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

    }
}
