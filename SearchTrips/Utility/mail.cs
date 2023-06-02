using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;


namespace SearchTrips.Utility
{
    public class mail
    {
        public string Resolve(string str)
        {
            str = API.Utility.Strings.Resolve(str);
            return str;
        }
        public string SendQuotes(string name, string mobile, string email, string address)
        {
            string mailresponse = "";
            string resp = "";
            try
            {
                name = Resolve(name);
                mobile = Resolve(mobile);
                email = Resolve(email);
                address = Resolve(address);


                if (string.IsNullOrEmpty(name))
                {
                    return "false:Missing Name";
                }
                if (string.IsNullOrEmpty(mobile))
                {
                    return "false:Missing Mobile";
                }
                else if (!API.Utility.IsValid.IndianMobileNo(mobile))
                {
                    return "false:Invalid Mobile Number";
                }
                if (string.IsNullOrEmpty(email))
                {
                    return "false:Missing Email";
                }
                else if (!API.Utility.IsValid.Email(email))
                {
                    return "false:Invalid Email";
                }

                if (string.IsNullOrEmpty(address))
                {
                    return "false:Missing Address.";
                }
                
               
                // sending mail now 
                string body = "";
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("enquiry@searchtrip.co.in");
                msg.To.Add("info@searchtrip.co.in");
                msg.Bcc.Add("searchtrip88@gmail.com");
                msg.Subject = "Booking lead from " + email;

                using (StreamReader reader = new StreamReader(Path.Combine("Booking_Template.html")))
                {
                    body = reader.ReadToEnd();
                }
                //body=body.Replace("{HotelName}", hotelname);
                body = body.Replace("{name}", name);
                body = body.Replace("{mobile}", mobile);
                body = body.Replace("{email}", email);
                body = body.Replace("{address}", address);
                
                msg.Body = body;
                msg.IsBodyHtml = true;
                // MailMessage instance to a specified SMTP server
                SmtpClient smtp = new SmtpClient("searchtrip.co.in");
                smtp.Credentials = new System.Net.NetworkCredential("enquiry@searchtrip.co.in", "Developoper#1996");

                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                // Sending the email
                try
                {
                    smtp.Send(msg);
                }
                catch (Exception mex)
                {
                    resp = mex.ToString();

                }

                if (!string.IsNullOrEmpty(resp))
                {
                    mailresponse = resp;
                }
                else
                {
                    mailresponse = "Your querry has being submitted , we will get back to you soon!";
                }
                msg.Dispose();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return mailresponse;
        }
        public string SendFeedback(string name, string email,string message)
        {
            string mailresponse = "";
            string resp = "";
            try
            {

                name = Resolve(name);
                email = Resolve(email);
                message = Resolve(message);


                if (string.IsNullOrEmpty(name))
                {
                    return "false:Missing Name";
                }
                if (string.IsNullOrEmpty(email))
                {
                    return "false:Missing Email";
                }
                else if (!API.Utility.IsValid.Email(email))
                {
                    return "false:Invalid Email";
                }
                if (string.IsNullOrEmpty(message))
                {
                    return "false:Missing Message";
                }


                string body = "";
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("feedback@searchtrip.co.in");
                msg.To.Add("info@searchtrip.co.in");
                msg.Bcc.Add("searchtrip88@gmail.com");
                msg.Subject = "Contact from " + email;

                using (StreamReader reader = new StreamReader(Path.Combine("Forwarding_template.html")))
                {
                    body = reader.ReadToEnd();
                }
                //body=body.Replace("{HotelName}", hotelname);
                body = body.Replace("{name}", name);
                body = body.Replace("{email}", email);
                body = body.Replace("{message}", message);
                msg.Body = body;
                msg.IsBodyHtml = true;
                // MailMessage instance to a specified SMTP server
                SmtpClient smtp = new SmtpClient("searchtrip.co.in");
                smtp.Credentials = new System.Net.NetworkCredential("feedback@searchtrip.co.in", "Developoper#1996");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                // Sending the email
                try
                {
                    smtp.Send(msg);
                }
                catch (Exception mex)
                {
                    resp = mex.ToString();
                }

                if (!string.IsNullOrEmpty(resp))
                {
                    mailresponse = resp; //"false:Reservation faild to submit, please try again  after some time or call us +91 9103000019";
                }
                else
                {
                    mailresponse = "Your Feedback has being submitted , we will get back to you soon!";
                }
                msg.Dispose();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return mailresponse;
        }
    }
}
