using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.webui.EmailServices
{
    public interface IEmailSender
    {
        Task EmailSenderAsyc(string email,string subject, string htmlMessage);
    }
}