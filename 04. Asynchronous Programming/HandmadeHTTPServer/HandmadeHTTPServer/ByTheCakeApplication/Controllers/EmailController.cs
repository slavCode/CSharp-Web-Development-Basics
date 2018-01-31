using System.Net;
using System.Net.Mail;

namespace HandmadeHTTPServer.ByTheCakeApplication.Controllers
{
    using Infrastructure;
    using Server.Http.Contracts;

    public class EmailController: Controller
    {
       private const string FromPassword = @"Ii^);6thS#2s4Gc;EQ+@+'7$h$V5ZrZ_vYAE3Da{& E~p==xXT5@fp{W'1CL";



        public IHttpResponse Email() => this.FileViewResponse("email");

        public IHttpResponse Email(string email, string subject, string body)
        {
            var fromAddress = new MailAddress("tablecapitan@gmail.com", "The Captain");
            var toAddress = new MailAddress(email, "To Name");

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, FromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }

            return this.FileViewResponse("email");
        } 

    }
}
