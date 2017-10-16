using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PoCTestProject.Com.Configs
{
    class FormatUtils
    {

        public static String formatCamelCaseText(String camelCaseText)
        {
            //split name by camel case
            String formattedName = Regex.Replace(camelCaseText, "(\\B[A-Z]+?(?=[A-Z][^A-Z])|\\B[A-Z]+?(?=[^A-Z]))", " $1").Trim();

            System.Globalization.TextInfo ti = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
            formattedName = ti.ToTitleCase(formattedName);

            return formattedName;
        }

        public static String GetTimestamp(DateTime time)
        {
            return time.ToString("yyyMMddHHmmssffff");
        }
    }
}
