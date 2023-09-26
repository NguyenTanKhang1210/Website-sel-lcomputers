using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace BanPC.Library
{
    public static class XString
    {
        public static string Str_Slug(string s)
        {
            String[][] symbols =
            {
                new string[] {"[å|ä|ā|à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|ä|ą]", "a" },
                new string[]{"[đ]","d" },
                new string[]{"[é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ]","e" },
                 new string[]{"[i|í|ì|ỉ|ĩ|ị]","i" },
                 new string[]{"[ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ]","o" },
                 new string[]{"[ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự]","u" },
                   new string[]{"[ý|ỳ|ỷ|ỹ|ỵ]","y" },
                   new string[]{"[\\s'\";,]","-" }
                    };
            s = s.ToLower();
            foreach (var ss in symbols)
            {
                s = Regex.Replace(s, ss[0], ss[1]);
            }
            return s;
        }
        public static string Str_Limit(this string str,int? length)
        {
            int lengt = (length ?? 20);
            if (str.Length > lengt)
            {
                str = str.Substring(0, lengt) + "...";
            } 
            return str;
        }
        public static string ToMD5(this string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sbHash = new StringBuilder();
            foreach (byte b in bHash)
            {
                sbHash.Append(String.Format("0:x2)", b));
            }
            return sbHash.ToString();
        }
        public static string ToShortString(this string str, int? length)
        {
            int lengt = (length ?? 20);
            if (str.Length > lengt)
            {
                str = str.Substring(0, lengt) + "...";
            }
            return str;
        }
    }
}