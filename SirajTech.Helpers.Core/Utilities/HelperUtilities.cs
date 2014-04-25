using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace SirajTech.Helpers.Core
{
    public static class HelperUtilities
    {
        #region Encryption And Decryption

        private static readonly byte[] staticKey = { 135, 133, 131, 129, 127, 125, 123, 121, 119, 117, 115, 113, 111, 109, 107, 105 };
        private static readonly byte[] staticIv = { 105, 107, 109, 111, 113, 115, 117, 119, 121, 123, 125, 127, 129, 131, 133, 135 };



        public static byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (var algorithm = Aes.Create())
            using (var encryptor = algorithm.CreateEncryptor(key, iv))
                return Crypt(data, key, iv, encryptor);
        }



        public static byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
        {
            using (var algorithm = Aes.Create())
            using (var decryptor = algorithm.CreateDecryptor(key, iv))
                return Crypt(data, key, iv, decryptor);
        }



        public static string Encrypt(string data, byte[] key, byte[] iv)
        {
            return string.IsNullOrEmpty(data)
                    ? null
                    : Convert.ToBase64String(
                                             Encrypt(Encoding.UTF8.GetBytes(data), key, iv));
        }



        public static string Decrypt(string data, byte[] key, byte[] iv)
        {
            return string.IsNullOrEmpty(data)
                    ? null
                    : Encoding.UTF8.GetString(
                                              Decrypt(Convert.FromBase64String(data), key, iv));
        }



        /// <summary>
        ///     Encrypts a particular string with a specific Key
        /// </summary>
        /// <param name="stringToEncrypt"> </param>
        /// <returns> </returns>
        public static string Encrypt(string stringToEncrypt)
        {
            byte[] key = { };
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
            byte[] inputByteArray; //Convert.ToByte(stringToEncrypt.Length) 

            try
            {
                key = Encoding.UTF8.GetBytes("QtCfWF1FymaHjExXsu30BQ==".Substring(0, 8));
                var des = new DESCryptoServiceProvider();
                inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch
            {
                return (string.Empty);
            }
        }



        /// <summary>
        ///     Decrypts a particular string with a specific Key
        /// </summary>
        public static string Decrypt(string stringToDecrypt)
        {
            byte[] key = { };
            byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };
            var inputByteArray = new byte[stringToDecrypt.Length];
            try
            {
                key = Encoding.UTF8.GetBytes("QtCfWF1FymaHjExXsu30BQ==".Substring(0, 8));
                var des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                var encoding = Encoding.UTF8;
                return encoding.GetString(ms.ToArray(), 0, ms.ToArray().Length);
            }
            catch
            {
                return (string.Empty);
            }
        }



        private static byte[] Crypt(byte[] data, byte[] key, byte[] iv, ICryptoTransform cryptor)
        {
            var m = new MemoryStream();
            using (Stream c = new CryptoStream(m, cryptor, CryptoStreamMode.Write))
                c.Write(data, 0, data.Length);
            return m.ToArray();
        }

        #endregion



        /// <summary>
        ///     Get the property path from given lambda as string 
        /// </summary>
        /// <typeparam name="T">The type to get one of its properties path </typeparam>
        /// <typeparam name="TProperty">The wanted property to get its path</typeparam>
        /// <param name="selector">Lambda expression to retrieve the property path from</param>
        /// <returns>String that represent the property path</returns>
        public static string GetPropertyPath<T, TProperty>(Expression<Func<T, TProperty>> selector)
        {
            var sb = new StringBuilder();
            var memberExpr = selector.Body as MemberExpression;
            if (memberExpr == null)
            {
                var unaryExpression = selector.Body as UnaryExpression;
                if (unaryExpression != null)
                    memberExpr = unaryExpression.Operand as MemberExpression;
            }
            while (memberExpr != null)
            {
                var name = memberExpr.Member.Name;
                if (sb.Length > 0)
                    name = name + ".";
                sb.Insert(0, name);
                if (memberExpr.Expression is ParameterExpression)
                    return sb.ToString();
                memberExpr = memberExpr.Expression as MemberExpression;
            }
            var constantExpression = selector.Body as ConstantExpression;
            if (constantExpression != null) return constantExpression.Value as string;
            throw new ArgumentException("The expression must be a MemberExpression, UnaryExpression or ConstantExpression", "selector");
        }



        public static Type GetPropertyType<T, TProperty>(Expression<Func<T, TProperty>> selector)
        {
            return GetPropertyType(typeof(T), GetPropertyPath(selector));
        }



        public static Type GetPropertyType<T>(string propertyPath)
        {
            return GetPropertyType(typeof(T), propertyPath);
        }



        /// <summary>
        ///     Get the type of the sub property no matter what level of the property is or the value is null or not null
        /// </summary>
        /// <param name="baseType">the base type to start from it</param>
        /// <param name="propertyPath">the path to the </param>
        /// <returns></returns>
        public static Type GetPropertyType(Type baseType, string propertyPath)
        {
            if (baseType == null)
                throw new ArgumentNullException("baseType");

            var neededHierarchy = propertyPath.Split('.');
            for (var i = 0; i < neededHierarchy.Count(); i++)
            {
                if (baseType.GetProperties().All(p => p.Name != neededHierarchy[i]))
                    throw new Exception("The Type \"" + baseType + "\" does NOT contain property named : \"" + neededHierarchy[i] + "\"");

                baseType = baseType.GetProperty(neededHierarchy[i]).PropertyType;
            }
            return baseType;
        }



        /// <summary>
        ///     Get the value of passed value's property by passing the hierarchy of it
        /// </summary>
        /// <param name = "value">The object to get property from</param>
        /// <param name = "property">Property hierarchy as string separated with '.'</param>
        /// <returns>The property value as object</returns>
        public static object GetPropertyValue(object value, string property)
        {
            var neededHierarchy = property.Split('.');
            var neededValue = value;
            for (int i = 0; i < neededHierarchy.Count(); i++)
            {
                if (neededValue == null)
                    continue;
                var props = TypeDescriptor.GetProperties(neededValue, true);
                if (props.Count > 0 && props.Find(neededHierarchy[i], true) != null)
                {
                    neededValue = props.Count > 0
                            ? props.Find(neededHierarchy[i], true).GetValue(neededValue)
                            : null;
                }
                else
                {
                    neededValue = property;
                }
            }
            return neededValue;
        }



        /// <summary>
        ///     Get Enum values as dictionary
        /// </summary>
        /// <typeparam name="K"> </typeparam>
        /// <returns> Dictionary with string key and int value </returns>
        public static IEnumerable<KeyValuePair<string, int>> EnumToIntDictionary<K>()
        {
            if (typeof(K).BaseType != typeof(Enum))
            {
                throw new InvalidCastException();
            }
            return Enum.GetValues(typeof(K)).Cast<Int32>().ToDictionary(currentItem => Enum.GetName(typeof(K), currentItem));
        }



        /// <summary>
        ///     Get Enum values as Dictionary with string key and string value
        /// </summary>
        /// <typeparam name="K"> </typeparam>
        /// <returns> Dictionary with string key and string value </returns>
        public static IEnumerable<KeyValuePair<string, string>> EnumToStringDictionary<K>()
        {
            if (typeof(K).BaseType != typeof(Enum))
            {
                throw new InvalidCastException();
            }
            return Enum.GetValues(typeof(K)).Cast<Int32>().ToDictionary(item => Enum.GetName(typeof(K), item), item => ((int)item).ToString());
        }



        /// <summary>
        /// Get the current thread lang.
        /// </summary>
        /// <returns>current thread language as TwoLetterISOLanguageName</returns>
        public static string CurrentLang()
        {
            return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        }



        /// <summary>
        ///     Try to get value from resource file
        /// </summary>
        /// <param name="resourceType"> The type of resource file</param>
        /// <param name="resourceKey"> Resource Key</param>
        /// <returns></returns>
        public static string GetResourceValue(Type resourceType, string resourceKey)
        {
            var property = resourceType.GetProperty(resourceKey, BindingFlags.Static | BindingFlags.Public);
            if (property != null)
            {
                return (string)property.GetValue(property.DeclaringType, null);
            }
            return null;
        }
    }
}