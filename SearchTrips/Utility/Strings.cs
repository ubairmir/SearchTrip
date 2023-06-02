using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;



namespace API
{
    namespace Utility
    {
        /// <summary>
        /// Summary description for Design
        /// </summary>
        /// 

        public class Strings
        {


            public static string GetMimeType(string fileName)
            {
                string mimeType = "application/unknown";
                string ext = System.IO.Path.GetExtension(fileName).ToLower();

                Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
                if (regKey != null && regKey.GetValue("Content Type") != null)
                    mimeType = regKey.GetValue("Content Type").ToString();
                return mimeType;

            }

            public static string Resolve(string str)
            {
                str = str.Replace("#s", "'").Replace("#d", "\"").Replace("#fslas", "\\");
                str = str.Replace("&lt;", "").Replace("&gt;", "").Trim();
                str = RemoveTags(str);
                return str.Replace("<", "&lt;").Replace(">", "&gt;").Trim();
            }



            public static string Resolve2(string str)
            {
                str = str.Replace("@@@comma", ",");
                return str;                
            }


            /// <summary>
            /// 
            /// </summary>
            /// <param name="startdate">january-2015</param>
            /// <param name="enddate">december-2017</param>
            /// <param name="months">1</param>
            /// <param name="years">2</param>
            public static void GetMonthYear(string startdate, string enddate,ref int months,ref int years)
            {
                try
                {
                    DateTime dt_start = Convert.ToDateTime(startdate);
                    DateTime dt_end = Convert.ToDateTime(enddate);

                    double totaldays = (dt_end - dt_start).TotalDays;

                    months = (int)(totaldays / 30) + 1;
                    years = months / 12;
                }
                catch
                {
                }
            }



            public static string Telephone(string phone)
            {

                phone = RemoveSpace(phone);
                string countrycode = "";
                string formatedphone = phone;
                if (phone.Contains("+") && phone.Length > 3)
                {
                    countrycode = phone.Substring(0, 3);
                    phone = phone.Substring(3, phone.Length - 3);
                }

                if (phone.Length > 5)
                {
                    formatedphone = phone.Substring(0, 5);
                }

                if (phone.Length > 8)
                {
                    formatedphone += " " + phone.Substring(5, 3);
                }

                if (phone.Length > 8)
                {
                    formatedphone += " " + phone.Substring(8, phone.Length - 8);
                }


                if (formatedphone.Contains("+"))
                {
                    return formatedphone.Trim();
                }
                else
                {
                    return (countrycode + " " + formatedphone).Trim();
                }
            }

            public static string RemoveSpace(string phone)
            {
                if (phone != null)
                {
                    return phone.Replace(" ", "");
                }
                else
                {
                    return phone;
                }
            }


            public static string RemoveSpecialChars(string str)
            {
                return System.Text.RegularExpressions.Regex.Replace(str, "[^A-Za-z0-9]", "");
            }


            public static string RemoveSpecialChars(string str, bool exceptDot)
            {
                if (exceptDot)
                {
                    return System.Text.RegularExpressions.Regex.Replace(str, "[^A-Za-z0-9.]", "");
                }
                else
                {
                    return System.Text.RegularExpressions.Regex.Replace(str, "[^A-Za-z0-9]", "");
                }
            }

            public static string GetSpecialChars(string str)
            {
                return System.Text.RegularExpressions.Regex.Replace(str, "[A-Za-z0-9]", "");
            }

          

            public static string GetAlphabet(string str)
            {
                return System.Text.RegularExpressions.Regex.Replace(str, "[^A-Za-z ]", "");
            }

            public static string GetNumeric(string str)
            {
                if (str != null)
                {
                    return System.Text.RegularExpressions.Regex.Replace(str, "[^0-9]", "");
                }
                else
                {
                    return str;
                }
            }


            public static string GetCustom(string strvalue, string regex, string Replacewith)
            {
                return System.Text.RegularExpressions.Regex.Replace(strvalue, "regex", Replacewith);
            }



            public static string NumberFormat(object numbers, bool flotingpoint = false)
            {
                string returnvalue = "";

                try
                {

                    string str = numbers.ToString();

                    string sign = "";
                    //if (str.Trim().StartsWith("-"))
                    //{
                    //    sign = "-";
                    //    str = str.Replace("-", "");
                    //}

                    double d = double.Parse(str);
                    decimal parsed = (decimal)d;

                   // decimal parsed = decimal.Parse(str);
                    System.Globalization.CultureInfo hindi = new System.Globalization.CultureInfo("hi-IN");
                   

                    

                    if (flotingpoint)
                    {
                        string text = string.Format(hindi, "{0:C2}", parsed);
                        returnvalue= text.Remove(0, 1);
                    }
                    else
                    {
                        string text = string.Format(hindi, "{0:C0}", parsed);
                        returnvalue = text.Remove(0, 1);
                    }


                  //  returnvalue = returnvalue.Trim().TrimStart('$');

                    returnvalue = sign + returnvalue.Trim();
                }
                catch
                {
                    returnvalue = numbers.ToString();
                }

                return returnvalue;
            }


            public static string RemoveTags(string str)
            {
                try
                {
                    return System.Text.RegularExpressions.Regex.Replace(str, @"<[^>]*>", String.Empty);
                }
                catch
                {
                    return str;
                }

            }


            public string Replace_butnotinP(string text, string replacetext, string replacewith)
            {

                string resp = "";
                try
                {
                    MatchCollection m = Regex.Matches(text, @"<p\s*(.+?)\s*</p>");

                    for (int i = 0; i < m.Count; i++)
                    {
                        text = text.Replace(m[i].Value, "@#" + i + "@");
                    }

                    text = text.Replace(replacetext, replacewith);

                    for (int i = 0; i < m.Count; i++)
                    {
                        text = text.Replace("@#" + i + "@", m[i].Value);
                    }

                    resp = text;

                }
                catch
                {
                }

                return resp;

            }

            //public static string NewLine(string text, bool convertLink = false)
            //{

            //    text = text.Replace("\r\n", "<br/>").Replace(Environment.NewLine, "<br/>").Replace("\n", "<br/>").Replace("&#xA;", "<br/>");

            //    if (convertLink)
            //    {

            //        string regexEmail = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
            //        var arr = Regex.Matches(text, regexEmail, RegexOptions.IgnoreCase);

            //        foreach (var m in arr)
            //        {
            //            var email = m.ToString();
            //            text = text.Replace(email, "<a href='mailto:" + email + "'>" + email + "</a>");
            //        }


            //        var arrWord = text.Split(new string[] { " ", Environment.NewLine, "<br/>" }, StringSplitOptions.RemoveEmptyEntries).Where(a => a.StartsWith("http://") || a.StartsWith("https://")).Distinct().ToList();


            //        foreach (var m in arrWord)
            //        {
            //            if (m.Length > 10 && (m.Contains("https://") || m.Contains("http://")))
            //            {
            //                text = text.Replace(m, "<a  target='_blank' href='" + m.ToString() + "'>" + m.ToString() + "</a>");
            //            }
            //        }


            //    }





            //    return text;


            //}


            public static string ConvertBBCodeToHTML(string str)
            {
                Regex exp;
                // format the bold tags: [b][/b]
                // becomes: <strong></strong>
                exp = new Regex(@"[b](.+?)[/b]");
                str = exp.Replace(str, "<strong>$1</strong>");

                // format the italic tags: [i][/i]
                // becomes: <em></em>
                exp = new Regex(@"[i](.+?)[/i]");
                str = exp.Replace(str, "<em>$1</em>");

                // format the underline tags: [u][/u]
                // becomes: <u></u>
                exp = new Regex(@"[u](.+?)[/u]");
                str = exp.Replace(str, "<u>$1</u>");

                // format the strike tags: [s][/s]
                // becomes: <strike></strike>
                exp = new Regex(@"[s](.+?)[/s]");
                str = exp.Replace(str, "<strike>$1</strike>");

                // format the url tags: [url=www.website.com]my site[/url]
                // becomes: <a href="www.website.com">my site</a>
                exp = new Regex(@"[url=([^]]+)]([^]]+)[/url]");
                str = exp.Replace(str, "<a href=\"$1\">$2</a>");

                // format the img tags: 
                // becomes: <img src="www.website.com/img/image.jpeg">
                exp = new Regex(@"[img]([^]]+)[/img]");
                str = exp.Replace(str, "<img src=\"$1\">");

                // format img tags with alt: [img=www.website.com/img/image.jpeg]this is the alt text[/img]
                // becomes: <img src="www.website.com/img/image.jpeg" alt="this is the alt text">
                exp = new Regex(@"[img=([^]]+)]([^]]+)[/img]");
                str = exp.Replace(str, "<img src=\"$1\" alt=\"$2\">");

                //format the colour tags: [color=red][/color]
                // becomes: <font color="red"></font>
                // supports UK English and US English spelling of colour/color
                exp = new Regex(@"[color=([^]]+)]([^]]+)[/color]");
                str = exp.Replace(str, "<font color=\"$1\">$2</font>");
                exp = new Regex(@"[colour=([^]]+)]([^]]+)[/colour]");
                str = exp.Replace(str, "<font color=\"$1\">$2</font>");

                // format the size tags: [size=3][/size]
                // becomes: <font size="+3"></font>
                exp = new Regex(@"[size=([^]]+)]([^]]+)[/size]");
                str = exp.Replace(str, "<font size=\" +$1\">$2</font>");

                // lastly, replace any new line characters with 

                str = str.Replace("rn", "rn");


                return str;

            }


            public static string GetDomain(string url)
            {
                string resp = "";
                try
                {
                    if (!url.StartsWith("http") && !url.Contains("://"))
                    {
                        url = "http://" + url;
                    }


                    resp = new Uri(url)
                        .GetLeftPart(UriPartial.Authority)
                        .Replace("/www.", "/")
                        .Replace("http://", "")
                        .Replace("https://", "");


                }
                catch
                {
                }
                return resp;
            }


            /// <summary>
            /// return alphabet, Ex 1=A, 26=Z, 27=AA
            /// </summary>
            /// <param name="number"></param>
            /// <returns></returns>
            public static string GetAlphabetCode(int number)
            {
                int start = (int)'A' - 1;
                if (number <= 26) return ((char)(number + start)).ToString();

                StringBuilder str = new StringBuilder();
                int nxt = number;

                List<char> chars = new List<char>();

                while (nxt != 0)
                {
                    int rem = nxt % 26;
                    if (rem == 0) rem = 26;

                    chars.Add((char)(rem + start));
                    nxt = nxt / 26;

                    if (rem == 26) nxt = nxt - 1;
                }


                for (int i = chars.Count - 1; i >= 0; i--)
                {
                    str.Append((char)(chars[i]));
                }

                return str.ToString();
            }



            public static string GetDaysMode(DateTime date)
            {
                
                decimal day = Math.Ceiling((decimal)(date.Day / float.Parse("7")));

                string dayname = date.DayOfWeek.ToString();
                string mode = "";

                switch ((int)day)
                {
                    case 1:
                        {
                            mode = "first"; break;
                        }
                    case 2:
                        {
                            mode = "second"; break;
                        }
                    case 3:
                        {
                            mode = "third"; break;
                        }
                    case 4:
                        {
                            mode = "fourth"; break;
                        }
                    case 5:
                        {
                            mode = "fifth"; break;
                        }
                }


                return (mode + " " + dayname.Substring(0, 3)).ToUpper();


            }



            public static string GetEcoText(DateTime dt)
            {
                string resp = "-";
                try
                {
                    int s = (int)(DateTime.Now - dt).TotalSeconds;
                    if (s < 60)
                    {
                        resp = "few second ago";
                    }
                    else if (s >= 60 && s < 180)
                    {
                        resp = "few minute ago";
                    }
                    else if (s >= 180 && s < 900)
                    {
                        resp = (int)(s / 60) + " minutes ago";
                    }
                    else if(dt.Day == DateTime.Now.Day)
                    {
                        resp = "Today"; ;
                    }
                    else if (dt.Day == DateTime.Now.AddDays(-1).Day)
                    {
                        resp = "Yesterday"; ;
                    }
                    else 
                    {
                        resp = dt.ToString("dd-MMM-yyyy");
                    }


                }
                catch
                {
                }

                return resp;
            }




        }

        public class XML
        {

            public static string CDDATA(string str)
            {
                return string.Format("<![CDATA[{0}]]>", str);
                
            }


            public static string Element(string name, string value, bool valuewithcddata = true)
            {
                if (valuewithcddata)
                {
                    return string.Format("<{0}>{1}</{0}>", name, CDDATA(value));
                }
                else
                {
                    return string.Format("<{0}>{1}</{0}>", name, value);
                }
            }



          




        }

    }
}
