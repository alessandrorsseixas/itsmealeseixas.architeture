using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using itsmealeseixas.architeture.utilities.SeedWorks;

namespace itsmealeseixas.architeture.utilities.Helpers
{
    public static class EmailHelper
    {

        public static void SendEmail(Email entity, SmtpClient smtp, Dictionary<string, string> sender, string logopath, bool showLogo = false)
        {
            try
            {


                MailMessage mensagem = new MailMessage();


                // Configurações do remetente
                mensagem.From = new MailAddress(sender.GetValueOrDefault("Email"), sender.GetValueOrDefault("Name"));

                // Configurações do destinatário
                mensagem.To.Add(new MailAddress(entity.TO));

                if (!string.IsNullOrEmpty(entity.CC)) mensagem.CC.Add(new MailAddress(entity.CC));
                if (!string.IsNullOrEmpty(entity.CCO)) mensagem.Bcc.Add(new MailAddress(entity.CCO));

                // Configurações do assunto e corpo do e-mail
                mensagem.Subject = entity.Title;
                mensagem.Body = entity.Body;
                mensagem.IsBodyHtml = true;

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                if (showLogo)
                {
                    Attachment logoAttachment = new Attachment(logopath);
                    logoAttachment.ContentId = "logo";
                    mensagem.Attachments.Add(logoAttachment);

                }
                // Envio do e-mail
                smtp.Send(mensagem);


            }
            catch (Exception ex)
            {
                throw;
            }
        } 
    }
}
