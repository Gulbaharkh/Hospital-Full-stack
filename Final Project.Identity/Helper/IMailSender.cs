using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Identity.Helper
{
    public interface IMailSender
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
