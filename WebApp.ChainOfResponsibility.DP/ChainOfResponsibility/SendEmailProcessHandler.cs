
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using WebApp.ChainOfResponsibility.ChainOfResponsibility;

namespace WebApp.ChainOfResponsibility.DP.ChainOfResponsibility;

public class SendEmailProcessHandler(string fileName, string toEmail) : Processhandler
{
    private readonly string _fileName = fileName;
    private readonly string _toEmail = toEmail;

    public override object handle(object o)
    {
        var zipMemoryStream = o as MemoryStream;
        zipMemoryStream.Position = 0;
        var mailMessage = new MailMessage();

        var smptClient = new SmtpClient("srvm11.trwww.com");

        mailMessage.From = new MailAddress("deneme@kariyersistem.com");

        mailMessage.To.Add(new MailAddress(_toEmail));

        mailMessage.Subject = "Zip dosyası";

        mailMessage.Body = "<p>Zip dosyası ektedir.</p>";

        Attachment attachment = new Attachment(zipMemoryStream, _fileName, MediaTypeNames.Application.Zip);

        mailMessage.Attachments.Add(attachment);

        mailMessage.IsBodyHtml = true;
        smptClient.Port = 587;
        smptClient.Credentials = new NetworkCredential("deneme@kariyersistem.com", "Password12*");

        smptClient.Send(mailMessage);

        return base.handle(null);
    }
}
