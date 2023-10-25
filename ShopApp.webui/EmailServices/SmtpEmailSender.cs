using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ShopApp.webui.EmailServices
{
    public class SmtpEmailSender : IEmailSender
    {
        private string _host;
        private int _port;
        private string _username;
        private bool _enableSSL;
        private string _password;
        public SmtpEmailSender(string host,int port,string username,bool enableSSL,string password)
        {
            this._password = password;
            this._enableSSL = enableSSL;
            this._host = host;
            this._port = port;
            this._username = username;

        }
        public Task EmailSenderAsyc(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient(this._host,this._port)
            {
                EnableSsl = this._enableSSL,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_username, _password),

            };
            return client.SendMailAsync(
                new MailMessage(this._username,email,subject,htmlMessage){
                    IsBodyHtml = true
                }
            );

        }
    }
}