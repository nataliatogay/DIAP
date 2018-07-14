using brownshouse.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace brownshouse.Domain.Models
{
    class MailSender : ISender
    {
        private string host;
        private int port;
        private NetworkCredential networkCredential;

        public MailSender(string host, int port, NetworkCredential networkCredential)
        {
            this.host = host;
            this.port = port;
            this.networkCredential = networkCredential;
        }

        public void Send(string to, string topic, string body, ICollection<string> files)
        {
            MailMessage mailMessage = new MailMessage(
                new MailAddress(networkCredential.UserName, "SmtpExampleApp"),
                new MailAddress(to)
                );
            mailMessage.Subject = topic;
            mailMessage.Body = body;

            if (files != null)
            {
                foreach(var file in files)
                {
                    if (!String.IsNullOrEmpty(file))
                    {
                        mailMessage.Attachments.Add(new Attachment(file));
                    }
                }
            }
            SmtpClient smtp = new SmtpClient(host, port);
            smtp.Credentials = networkCredential;
            smtp.EnableSsl = true;
            smtp.Send(mailMessage);
        }
    }
}
