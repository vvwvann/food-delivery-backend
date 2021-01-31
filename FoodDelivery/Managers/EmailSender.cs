using MimeKit;
using MailKit.Net.Smtp;
using System;

namespace FoodDelivery.Managers
{
  public static class EmailSender
  {
    private static string _email = "";
    private static string _password = "";

    public static bool SendMessage(string email, string subject, string text)
    {
      try {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(new MailboxAddress("Администрация сайта", _email));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) {
          Text = text
        };

        using var client = new SmtpClient();
        client.Connect("smtp.gmail.com", 587, false);
        client.Authenticate(_email, _password);
        client.Send(emailMessage);
        client.Disconnect(true);
      }
      catch (Exception ex) {
        Console.WriteLine(ex);
        return false;
      }

      return true;
    } 
  }
}
