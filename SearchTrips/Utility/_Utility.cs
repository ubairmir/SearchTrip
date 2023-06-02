using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Security.Cryptography;

/// <summary>
/// Summary description for Media
/// </summary>
public class _Utility
{
    public static float SGST = 9;
    public static float CGST = 9;
    public static float IGST = 18;

    public static string GetSHA256Hash(string str)
    {
        using (HashAlgorithm hashAlgorithm = SHA256.Create())
        {
            var hash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(str));
            string DocumentHashHex = "";
            foreach (byte b in hash) DocumentHashHex += b.ToString("X2");
            return DocumentHashHex;
        }
    }
    public static string FormatResponse(string resp)
    {
        return resp.Replace("success:", "true:").Replace("failed:", "false:");
    }


    /// <summary>
    /// Get array of bytes from stream
    /// </summary>
    /// <param name="stream">System.IO.Stream</param>
    /// <returns></returns>
    public static byte[] GetBytes(System.IO.Stream stream)
    {
        long originalPosition = 0;

        if (stream.CanSeek)
        {
            originalPosition = stream.Position;
            stream.Position = 0;
        }

        try
        {
            byte[] readBuffer = new byte[4096];

            int totalBytesRead = 0;
            int bytesRead;

            while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
            {
                totalBytesRead += bytesRead;

                if (totalBytesRead == readBuffer.Length)
                {
                    int nextByte = stream.ReadByte();
                    if (nextByte != -1)
                    {
                        byte[] temp = new byte[readBuffer.Length * 2];
                        Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                        Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                        readBuffer = temp;
                        totalBytesRead++;
                    }
                }
            }

            byte[] buffer = readBuffer;
            if (readBuffer.Length != totalBytesRead)
            {
                buffer = new byte[totalBytesRead];
                Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
            }
            return buffer;
        }
        finally
        {
            if (stream.CanSeek)
            {
                stream.Position = originalPosition;
            }
        }
    }

    /// <summary>
    /// Get array of bytes from string
    /// </summary>
    /// <param name="str">string </param>
    /// <returns></returns>
    public static byte[] GetBytes(string str)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(str);

        //    new byte[str.Length * sizeof(char)];
        //System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
        return bytes;
    }

    /// <summary>
    /// Get array of bytes from image
    /// </summary>
    /// <param name="img"></param>
    /// <returns></returns>

    /// <summary>
    /// Get string from bytes
    /// </summary>
    /// <param name="bytes">array of bytes</param>
    /// <returns></returns>
    public static string GetString(byte[] bytes)
    {
        char[] chars = new char[bytes.Length / sizeof(char)];
        System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
        return new string(chars);
    }


    public static double GetAmountwithoutTax(double sellingprice, double TaxPercent)
    {
        try
        {
            return ((sellingprice / (TaxPercent + 100)) * 100F);
        }
        catch
        {
        }

        return 0;
    }



    public static double Round2Digit(object number)
    {
        string str = number.ToString();

        var d = double.Parse(str).ToString("0.00");
        return double.Parse(d);

    }



    //Get financial year
    public static string GetCurrentFinancialYear()
    {
        int CurrentYear = DateTime.Today.Year;
        int PreviousYear = DateTime.Today.Year - 1;
        int NextYear = DateTime.Today.Year + 1;
        string PreYear = PreviousYear.ToString();
        string NexYear = NextYear.ToString().Substring(2, 2);
        string CurYear = CurrentYear.ToString();
        string FinYear = null;

        if (DateTime.Today.Month > 3)
            FinYear = CurYear + "-" + NexYear;
        else
            FinYear = PreYear + "-" + CurYear;
        return FinYear.Trim();
    }

    public static  List<string> GetFinancialYears()
    { 
        List<string> financialyearlist = new List<string>();

        int CurrentYear = DateTime.Today.Year;
        int PreviousYear = DateTime.Today.Year - 1;
        int NextYear = DateTime.Today.Year + 1;
        string PreYear = PreviousYear.ToString();
        string NexYear = NextYear.ToString().Substring(2, 2);
        string CurYear = CurrentYear.ToString();
        string FinYear = null;

        if (DateTime.Today.Month > 3)
        { 
            FinYear = CurYear + "-" + NexYear;
             financialyearlist.Add(FinYear);
        }

        else
        { 
            FinYear = PreYear + "-" + CurYear;
        }
        CurrentYear = CurrentYear - 1;
        while (CurrentYear  >= 2021)
        {
            FinYear=CurrentYear + "-" + (CurrentYear+1).ToString().Substring(2,2);
            financialyearlist.Add(FinYear);
            CurrentYear = CurrentYear - 1;
        }

        return financialyearlist;

    }
	
	public static string GetPreviousFinancialYear(string year)
    {
        DateTime dt = DateTime.Parse("01/04/" + year).AddYears(-1);
        int CurrentYear = 0;
        int PreviousYear = 0;
        int NextYear = 0;
        if (DateTime.Now.ToString("MMM").ToLower() == "apr")
        {
            CurrentYear = dt.Year - 1;
            PreviousYear = dt.Year - 2;
            NextYear = dt.Year;
        }
        else
        {
            CurrentYear = dt.Year;
            PreviousYear = dt.Year - 1;
            NextYear = dt.Year + 1;
        }

        string PreYear = PreviousYear.ToString();
        string NexYear = NextYear.ToString().Substring(2, 2);
        string CurYear = CurrentYear.ToString();
        string FinYear = null;

        if (DateTime.Today.Month > 3)
            FinYear = CurYear + "-" + NexYear;
        else
            FinYear = PreYear + "-" + CurYear;
        return FinYear.Trim();
    }

    public static string GetNextFinancialYear(string year)
    {
        DateTime dt = DateTime.Parse("01/04/" + year);
        int CurrentYear = 0;
        int PreviousYear = 0;
        int NextYear = 0;

        CurrentYear = dt.Year + 1;
        PreviousYear = dt.Year;
        NextYear = dt.Year + 2;

        string PreYear = PreviousYear.ToString();
        string NexYear = NextYear.ToString().Substring(2, 2);
        string CurYear = CurrentYear.ToString();
        string FinYear = null;

        if (DateTime.Today.Month > 3)
            FinYear = CurYear + "-" + NexYear;
        else
            FinYear = PreYear + "-" + CurYear;
        return FinYear.Trim();
    }



    /// <summary>
    /// Get Years for prev and next for current year
    /// </summary>
    /// <param name="prev"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public static List<string> GetYears_Prev_Next(int prev=2019,int next=0)
    {
        List<string> yearlist = new List<string>();
        int CurrentYear = DateTime.Today.Year;
        CurrentYear += next;

        while (CurrentYear >= prev)
        {
            yearlist.Add(CurrentYear.ToString());
            CurrentYear = CurrentYear - 1;
        }

        return yearlist;

    }


    public static List<string> GetFinancialYears_Prev_Next()
    {
        List<string> financialyearlist = new List<string>();

        int CurrentYear = DateTime.Today.Year;
        int PreviousYear = DateTime.Today.Year - 1;
        int NextYear = DateTime.Today.Year + 1;
        string PreYear = PreviousYear.ToString();
        string NexYear = NextYear.ToString().Substring(2, 2);
        string CurYear = CurrentYear.ToString();
        string FinYear = null;

        if (DateTime.Today.Month > 3)
        {
            FinYear = CurYear + "-" + NexYear;
            financialyearlist.Add(FinYear);
        }
        else
        {
            FinYear = PreYear + "-" + CurYear;
        }
        CurrentYear = CurrentYear - 1;
        while (CurrentYear >= PreviousYear)
        {
            FinYear = CurrentYear + "-" + (CurrentYear + 1).ToString().Substring(2, 2);
            financialyearlist.Add(FinYear);
            CurrentYear = CurrentYear - 1;
        }

        return financialyearlist;

    }


    public static string GetFileExtension(string base64String)
    {
        var data = base64String.Substring(0, 5);

        switch (data.ToUpper())
        {
            case "IVBOR":
                return "png";
            case "/9J/4":
                return "jpg";
            case "R0lGO":
                return "gif";
            case "JVBER":
                return "pdf";
            case "AAABA":
                return "ico";
            case "UMFYI":
                return "rar";
            case "E1XYD":
                return "rtf";
            case "U1PKC":
                return "txt";
            case "MQOWM":
            case "77U/M":
                return "srt";
            case "AAAAI":
                return "mp4";
            case "GkXfo":
                return "webm";
            case "AAAAH":
                return "mp4";
            case "UklGR":
                return "avi";
            case "RkxWA":
                return "flv";
            case "AAAAF":
                return "mov";
            default:
                return string.Empty;
        }
    }
    public static String GetMobileVersion(string userAgent, string device)
    {
        var temp = userAgent.Substring(userAgent.IndexOf(device) + device.Length).TrimStart();
        var version = string.Empty;

        foreach (var character in temp)
        {
            var validCharacter = false;
            int test = 0;

            if (Int32.TryParse(character.ToString(), out test))
            {
                version += character;
                validCharacter = true;
            }

            if (character == '.' || character == '_')
            {
                version += '.';
                validCharacter = true;
            }

            if (validCharacter == false)
                break;
        }

        return version;
    }
	
    public static int GetAge(string date)
    {
        try
        {
            if (!string.IsNullOrEmpty(date) && API.Utility.IsValid.DateTime(date))
            {
              return  DateTime.Now.Year-Convert.ToDateTime(date).Year;
            }
        }
        catch
        {

        }

        return 0;
    }

    static string CalculateAge(DateTime Dob)
    {
        try
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }

            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            int Hours = Now.Subtract(PastYearDate).Hours;

            return Years + " years" + Months + " months " + Days +" days";
        }
        catch
        {

        }
        return "";
    }
}
