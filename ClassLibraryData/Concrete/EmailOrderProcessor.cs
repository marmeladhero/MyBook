using ClassLibraryData.Abstract;
using ClassLibraryData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;


namespace ClassLibraryData.Concrete
{
    public class EmailSettings
    {
        public string strFrom = "bookstoreaspnet@gmail.com";
        public string strPassword = "ghbvf12345";
        public bool UseSsl = true;
        public int iServerPort = 587;
        public string strSmtpName = "smtp.gmail.com";

    }

    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings settings = new EmailSettings();

        public void ProcessorOrder(Cart cart, User user)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = settings.UseSsl;
                smtpClient.Host = settings.strSmtpName;
                smtpClient.Port = settings.iServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(settings.strFrom, settings.strPassword);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                StringBuilder body = new StringBuilder()
                    .AppendLine("Your order from BookStore\r\n")
                    .AppendLine("Goods:\r\n");

                foreach(var item in cart.Lines)
                {
                    var subtotal = item.book.Price * item.Quantity;
                    body.AppendFormat("Count:{0}  Name:{1}; Total:{2} USD\r\n", item.Quantity, item.book.Name, subtotal);
                }

                body.AppendFormat("Total sum: {0} USD\r\n", cart.ComputeTotalValue())
                    .AppendLine("Wish you best regrats! BookStore <3");

                MailMessage mailMessage = new MailMessage(settings.strFrom, user.Email, "BookStore YOUR Order", body.ToString());
                smtpClient.Send(mailMessage);
            }
        }
    }
}
