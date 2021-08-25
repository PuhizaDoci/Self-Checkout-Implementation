using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ToshibaPosSinkronizimi
{
    public class DergoEmail
    {
        public static void Dergo(string ErrorMesazhi, string PikaShitjes, string subjekt="Gabim gjate sinkronizimit te shenimeve! Pika e shitjes : ")
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("kubitsolution@gmail.com", "kubitsolution");
                mail.To.Add("qaushbytyqi@gmail.com");
                mail.To.Add("shpendkastrati1@gmail.com");
                mail.Subject = subjekt + PikaShitjes;
                mail.Body = ErrorMesazhi;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("kubitsolution@gmail.com", "@Kubit1234ASDF");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                System.Threading.Thread.Sleep(2000);                

            }
            catch (Exception ex)
            {
            }
        }
    }
}
