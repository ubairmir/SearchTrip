using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing;

namespace API
{
    namespace Utility
    {
        /// <summary>
        /// Summary description for ValidationClass
        /// </summary>
        public class IsValid
        {

           

            public static bool GSTIN(string gstin)
            {
                bool resp = false;
                try
                {
                    if (gstin.Length != 15)
                    {
                        return false;
                    }

                    System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(@"^([0]{1}[1-9]{1}|[1-2]{1}[0-9]{1}|[3]{1}[0-7]{1})([a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}[1-9a-zA-Z]{1}[zZ]{1}[0-9a-zA-Z]{1})+$");
                    System.Text.RegularExpressions.Match m1 = re.Match(gstin);
                    return m1.Success;
                }
                catch
                {
                    resp = false;
                }

                return resp;
            }
            public static bool Email(string email)
            {
                System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
                System.Text.RegularExpressions.Match m1 = re.Match(email);
                return m1.Success;
            }

            public static bool Aadhar(string aadharno, bool isVID=false)
            {
                bool resp = false;
                try
                {
                    aadharno = Strings.RemoveSpace(aadharno);

                    string numeric = Strings.GetNumeric(aadharno);

                    if (isVID)
                    {
                        if (numeric.Length == aadharno.Length && ( aadharno.Length == 12 || aadharno.Length == 16))
                        {
                            resp = true;
                        }
                    }
                    else
                    {
                        if (numeric.Length == aadharno.Length && aadharno.Length == 12)
                        {
                            resp = true;
                        }
                    }
                }
                catch
                {
                    resp = false;
                }

                return resp;
            }

            public static bool FingerPos(string name)
            {
                bool resp = false;
                switch (name.Trim().ToUpper())
                {
                    // case "LEFT_IRIS":
                    // case "RIGHT_IRIS":
                    case "LEFT_INDEX":
                    case "LEFT_LITTLE":
                    case "LEFT_MIDDLE":
                    case "LEFT_RING":
                    case "LEFT_THUMB":
                    case "RIGHT_INDEX":
                    case "RIGHT_LITTLE":
                    case "RIGHT_MIDDLE":
                    case "RIGHT_RING":
                    case "RIGHT_THUMB":
                        resp = true;
                        break;

                }

                return resp;
            }

            public static bool PAN(string pan)
            {
                bool resp = false;
                try
                {
                    System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(@"^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$");
                    System.Text.RegularExpressions.Match m1 = re.Match(pan);
                    return m1.Success;
                }
                catch
                {
                    resp = false;
                }

                return resp;
            }

            public static bool IndianPIN(string PINCode)
            {
                bool resp = false;
                try
                {
                    PINCode = PINCode.Trim();

                    if (PINCode.Length == 6 && Int(PINCode))
                    {
                        resp = true;
                    }
                }
                catch
                {
                    resp = false;
                }

                return resp;
            }

            public static bool IndianMobileNo(string mobileno)
            {
                try
                {
                    mobileno = Strings.RemoveSpace(mobileno);
                    string regex = "^(?:(?:\\+|0{0,2})91(\\s*[\\-]\\s*)?|[0]?)?[6789]\\d{9}$";

                    return System.Text.RegularExpressions.Regex.IsMatch(mobileno, regex);
                }
                catch
                {
                    return false;
                }
            }

            public static bool Percentage(string per)
            {
                try
                {
                    per = Strings.RemoveSpace(per);
                    string regex = "(?!^0*$)(?!^0*\\.0*$)^\\d{1,2}(\\.\\d{1,2})?$";
                    return System.Text.RegularExpressions.Regex.IsMatch(per, regex);
                }
                catch
                {
                    return false;
                }
            }

            public static bool Telephone(string telephone)
            {
                try
                {
                    telephone = telephone.Trim().Replace(" ", "-");
                    string regex = "\\+?\\d[\\d -]{8,12}\\d";

                    return System.Text.RegularExpressions.Regex.IsMatch(telephone, regex);
                }
                catch
                {
                    return false;
                }
            }

            public static bool Numeric(string strnumber)
            {
                try
                {
                    string regex = @"^\d+$";
                    return System.Text.RegularExpressions.Regex.IsMatch(strnumber, regex);
                }
                catch
                {
                    return false;
                }
            }
            public static bool IsAlphaNumeric(string text)
            {
                try
                {
                    string regex = @"^[a-zA-Z][a-zA-Z0-9]*$";
                    return System.Text.RegularExpressions.Regex.IsMatch(text, regex);
                }
                catch
                {
                    return false;
                }
            }

            public static bool AllowedChar(string str)
            {
                try
                {
                    str = str.Trim();                       
                    string regex = "^[ A-Za-z0-9_@./#&+-]*$";
                    return System.Text.RegularExpressions.Regex.IsMatch(str, regex);
                }
                catch
                {
                    return false;
                }
            }

            public static bool Url(string url)
            {

                if (url.ToLower().StartsWith("http") && url.ToLower().Contains("://") && url.Contains("."))
                {
                    return true;
                }
                else
                {
                    return false;
                }
               
            }

            public static bool Image(string url)
            {

                if (url.ToLower().EndsWith(".png") || url.ToLower().EndsWith(".jpg") || url.ToLower().EndsWith(".jpeg") || url.ToLower().EndsWith(".gif"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public static bool DateTime(string strdatetime)
            {
                try
                {
                    System.DateTime.Parse(strdatetime);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="strdatetime"></param>
            /// <param name="formats">Ex: var formats = new[] { "dd/MMM/yyyy", "dd-MMM-yyyy" };</param>
            /// <returns></returns>
            public static bool DateTime(string strdatetime, string[] formats)
            {
                try
                {
                    DateTime fromDateValue;

                    //var formats = new[] { "dd/MMM/yyyy", "dd-MMM-yyyy" };

                    if (System.DateTime.TryParseExact(strdatetime, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out fromDateValue))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }

            public static bool Int(string strnumber)
            {
                try
                {
                    int.Parse(strnumber);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public static bool Float(string strnumber)
            {
                try
                {
                    float.Parse(strnumber);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public static bool Xml(string xml)
            {
                try
                {
                    System.Xml.Linq.XDocument.Parse(xml);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public static bool IsEmpty(string text)
            {
                if (string.IsNullOrEmpty(text) || text.Trim() == "-")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

          

            public static bool Time(string time)
            {
                try
                {
                    string regex = @"^(?:[01][0-9]|2[0-3]):[0-5][0-9]$";
                    return System.Text.RegularExpressions.Regex.IsMatch(time, regex);
                }
                catch
                {
                    return false;
                }
            }

            public static bool RTOCode(string rtocode)
            {
                bool resp = false; 
                try
                {
                    string regex = @"^[a-zA-Z]{2}[0-9]{2}$";
                    resp = System.Text.RegularExpressions.Regex.IsMatch(rtocode, regex);

                    if (!resp)
                    {
                        regex = @"^[a-zA-Z]{2}[0-9]{2}[a-zA-Z]{1}$";
                        resp = System.Text.RegularExpressions.Regex.IsMatch(rtocode, regex);
                    }

                }
                catch
                {
                    resp = false;
                }
                return resp;
            }
            //created by kamaldeep on 14-01-2021
            public static bool RegistrationNo(string regno)
            {
                bool resp = false;
                try
                {
                    string regex = @"^[a-zA-Z]{2}-[0-9]{2}-[a-zA-Z0-9]{1,3}-[0-9]{4}$";
                    resp = System.Text.RegularExpressions.Regex.IsMatch(regno, regex);

                }
                catch
                {
                    resp = false;
                }
                return resp;
            }
            //created by kamaldeep on 16-01-2021
            public static bool PassportNo(string text)
            {
                bool resp = false;
                try
                {
                    string regex = @"^[A-Z]{1}[0-9]{7}$";
                    resp = System.Text.RegularExpressions.Regex.IsMatch(text, regex);

                }
                catch
                {
                    resp = false;
                }
                return resp;
            }

            // created by ranjeet on 22-04-2021.
            public static bool IsAlpha(string text, bool allowspace = false)
            {
                try
                {
                    string regex = @"^[a-zA-Z]*$";
                    if (allowspace)
                    {
                        regex = @"^[a-zA-Z\s]*$";
                    }
                    return System.Text.RegularExpressions.Regex.IsMatch(text, regex);
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}