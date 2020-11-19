using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mail;

namespace SPBuildingWebHotline
{
    /// <summary>
    /// Summary description for Utils.
    /// </summary>
    public class Utils
    {
        public Utils()
        {
        }

        public static void SendMail(string host, string sender, string recipient, string cc, string subject, string message)
        {
            //MailMessage msg = new MailMessage();
            //msg.From = sender;
            //msg.To = recipient;
            //msg.Cc = cc;
            //msg.Subject = subject;
            //msg.Body = message;

            //SmtpMail.SmtpServer = host;
            //SmtpMail.Send(msg);        
            
            SmtpClient smtpClient = new SmtpClient();
            NetworkCredential basicCredential = new NetworkCredential("m1@thespi.com", "1234");

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            MailAddress fromAddress = new MailAddress(sender);

            MailAddress toAddress = new MailAddress(recipient);

            smtpClient.Host = host;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = basicCredential;

            msg.From = fromAddress;
            msg.Subject = subject;
            
            //Set IsBodyHtml to true means you can send HTML email.            
            msg.IsBodyHtml = true;
            msg.Body = message;
            msg.To.Add(toAddress);

            if (cc == "")
            {

            }
            else
            {
                string ccMail = cc.Trim();

                int ccMailLenght = ccMail.Length;
                if (ccMail != null)
                {
                    if (ccMail.EndsWith(";"))
                        cc = ccMail.Substring(0, ccMailLenght - 1);

                    msg.CC.Add(cc.Trim());
                }
            }
            try
            {
                smtpClient.Send(msg);
            }
            catch(Exception ex)
            {
                //Error, could not send the message
               // Response.Write(ex.Message);
            }


        }

        public static string HotlineEmailSend(string brId)
        {
            string hotlineEmail = "";
            switch (brId)
            {
                case ("1"):
                    hotlineEmail = Global.HOTLINE_EMAIL1;
                    break;
                case ("2"):
                    hotlineEmail = Global.HOTLINE_EMAIL2;
                    break;
                case ("3"):
                    hotlineEmail = Global.HOTLINE_EMAIL3;
                    break;
            }
            return hotlineEmail;
        }


        public static string ExtractFileExt(string filename)
        {
            int first = filename.LastIndexOf(".");
            if (first != -1)
            {
                return filename.Substring(first + 1);
            }
            else return "";
        }

        public static string DecodeStatus(string StatusCode)
        {
            return Global.StatusTable[StatusCode].ToString();
        }

        public static string DecodePriorityAsText(string priority)
        {
            if (priority == "1")
            {
                return "ต่ำ";
            }
            else if (priority == "2")
            {
                return "ปานกลาง";
            }
            else if (priority == "3")
            {
                return "สูง";
            }
            else
                return "ไม่ทราบ";
        }

        public static DateTime GetEopDt()
        {
            int diffDay = 0;
            DayOfWeek day_of_week = DateTime.Now.DayOfWeek;
            switch (day_of_week)
            {
                case DayOfWeek.Monday: diffDay = 4; break;
                case DayOfWeek.Tuesday: diffDay = 3; break;
                case DayOfWeek.Wednesday: diffDay = 2; break;
                case DayOfWeek.Thursday: diffDay = 1; break;
                case DayOfWeek.Friday: diffDay = 0; break;
                case DayOfWeek.Saturday:
                case DayOfWeek.Sunday: throw new Exception("คุณไม่สามารถคัดลอกปัญหาไปยัง SP อาคาร  Status Reportในวันเสาร์หรืออาทิตย์ได้");
            }
            TimeSpan ts = new TimeSpan(diffDay, 0, 0, 0);
            return DateTime.Now.Add(ts);
        }


        public static DateTime ConvertDateToDateTime(string dt)
        {
            System.IFormatProvider format = new System.Globalization.CultureInfo("en-US");
          
            DateTime dateTimeReturn = DateTime.ParseExact(dt, "dd/MM/yyyy", format);
          
            return dateTimeReturn;
        }

        public static string ConvertDateToString(DateTime dt)
        {
            string dateTimeReturn;
            System.IFormatProvider format = new System.Globalization.CultureInfo("en-US");
            dateTimeReturn = dt.ToString("dd/MM/yyyy", format);

            return dateTimeReturn;
        }



    }
}
