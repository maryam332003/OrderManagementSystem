using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace OrdersManagement.Service
{
    public class EmailService 
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //public async Task SendEmailAsync(string toEmail, string subject, string message)
        //{
        //    ////var emailMessage = new MimeMessage();
        //    ////emailMessage.From.Add(new MailboxAddress("YourAppName", _configuration["EmailSettings:FromEmail"]));
        //    ////emailMessage.To.Add(new MailboxAddress("", toEmail));
        //    ////emailMessage.Subject = subject;
        //    ////emailMessage.Body = new TextPart("html") { Text = message };

        //    ////using var client = new SmtpClient();
        //    ////await client.ConnectAsync(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:Port"]), MailKit.Security.SecureSocketOptions.StartTls);
        //    ////await client.AuthenticateAsync(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
        //    ////await client.SendAsync(emailMessage);
        //    ////await client.DisconnectAsync(true);
        //    //try
        //    //{
        //    //    var emailMessage = new MimeMessage();
        //    //    emailMessage.From.Add(new MailboxAddress("YourAppName", _configuration["EmailSettings:FromEmail"]));
        //    //    emailMessage.To.Add(new MailboxAddress("", toEmail));
        //    //    emailMessage.Subject = subject;
        //    //    emailMessage.Body = new TextPart("html") { Text = message };

        //    //    using var client = new SmtpClient();

        //    //    client.Timeout = 10000;
        //    //    client.LocalDomain = "localhost";

        //    //    await client.ConnectAsync(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:Port"]), SecureSocketOptions.StartTls);
        //    //    await client.AuthenticateAsync(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
        //    //    await client.SendAsync(emailMessage);
        //    //    await client.DisconnectAsync(true);
        //    //}
        //    //catch (SmtpProtocolException ex)
        //    //{
        //    //    Console.WriteLine($"SMTP Protocol Exception: {ex.Message}");
        //    //}
        //    //catch (SocketException ex)
        //    //{
        //    //    Console.WriteLine($"Socket Exception: {ex.Message}");
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Console.WriteLine($"General Exception: {ex.Message}");
        //    //}


        //}


        public static void SendEmail(Email email)
        {
            //try
            //{
            //    var client = new SmtpClient("smtp.gmail.com", 587)
            //    {
            //        EnableSsl = true,
            //        Credentials = new NetworkCredential("mariemgamal200333@gmail.com", "dsadfdpednrjkwkt")
            //    };

            //    var mailMessage = new MailMessage
            //    {
            //        From = new MailAddress("mariemgamal200333@gmail.com"),
            //        Subject = email.Subject,
            //        Body = email.Body,
            //        IsBodyHtml = true
            //    };

            //    mailMessage.To.Add(email.Recipients);

            //    client.Send(mailMessage);
            //}
            //catch (Exception ex)
            //{
            //    // Log the exception (ex)
            //    throw new Exception("An error occurred while sending the email.", ex);
            //}

            try
            {
                using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("mariemgamal200333@gmail.com", "dsadfdpednrjkwkt");
                    smtpClient.EnableSsl = true;

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress("mariemgamal200333@gmail.com");
                    mailMessage.To.Add(new MailAddress(email.Recipients));
                    mailMessage.Subject = email.Subject;
                    mailMessage.Body = email.Body;
                    mailMessage.IsBodyHtml = true;

                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                // Handle or log the exception appropriately
                throw new ApplicationException("Failure sending mail.", ex);
            }
        }

    }
}
