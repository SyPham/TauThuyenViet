using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TauThuyenViet.ClassHelpers
{
    public static class MyUtility
    {
        public static string UrlEncode(this string value)
        {
            string result = value;

            string symbols = @"/\?#$& ";
            foreach (var item in symbols)
            {
                result = result.Replace(item, '-');
            }

            string pattern = "-+";
            Regex regex = new Regex(pattern);
            result = regex.Replace(result, "-");

            return result;
        }

        public static string GetFirstImage(this string imageList)
        {
            //Bắt lỗi null
            if (imageList == null || imageList.Trim() == string.Empty)
                return string.Empty;

            string result = imageList.Split("\n")[0].Trim();
            return result;
        }

        public static string[] SplitImages(this string imageList)
        {
            if (imageList == null)
                return new string[0];

            return imageList.Trim().Split('\n');
        }
    }
}
