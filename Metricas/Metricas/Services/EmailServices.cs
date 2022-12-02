using Metricas.Models;
using Metricas.Utils;
using System.Net;
using System.Net.Mail;

namespace Metricas.Services
{
    public class EmailServices
    {
        private readonly EmailConfig _email;
        public EmailServices(EmailConfig email)
        {
            _email = email;
        }
        public Task<HttpStatusCode> Send(Email email)
        {
            MailMessage emailMessage = new MailMessage();
            try
            {
                PrometheusConfig.emailSend.Inc(1);
                //configrando cliente smtp
                var smtpClient = new SmtpClient(_email.SmptClient, _email.Port);
                smtpClient.EnableSsl = true;
                smtpClient.Timeout = 60 * 60;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_email.EmailFrom, _email.Password);

                //configurando corpo do email / Informações do email
                //=> quem envia
                emailMessage.From = new MailAddress(_email.EmailFrom, "TesteMetricas");
                //=>corpor do email
                emailMessage.Body = email.EmailBody;
                //=>Titulo do email
                emailMessage.Subject = email.Subject;
                emailMessage.IsBodyHtml = true;
                emailMessage.Priority = MailPriority.Normal;
                emailMessage.To.Add(email.EmailTo);
                smtpClient.Send(emailMessage);
                PrometheusConfig.emailSuccess.Inc(1);
                return Task.FromResult(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                PrometheusConfig.emailFail.Inc(1);
                return Task.FromResult(HttpStatusCode.BadRequest);

            }

        }
    }
}