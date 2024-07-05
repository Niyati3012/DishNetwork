using DishNetwork.Repository.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DishNetwork.Entity.DataContext;
using DishNetwork.Entity.ViewModels;

namespace DishNetwork.Repository.Repository
{
	public class SendEmailRepository : ISendEmailRepository
	{
		private readonly SendEmailModel _emailConfig;
		public SendEmailRepository(SendEmailModel emailConfig)
		{
			_emailConfig = emailConfig;
		}
		public bool SendEmail(String To, string Subject, string Body)
		{
			try
			{
				MailMessage message = new MailMessage();
				message.From = new MailAddress(_emailConfig.From);
				message.Subject = Subject;
				message.To.Add(new MailAddress(To));
				message.Body = Body;
				message.IsBodyHtml = true;
				using (var smtpClient = new SmtpClient(_emailConfig.SmtpServer))
				{
					smtpClient.Port = 587;
					smtpClient.Credentials = new NetworkCredential(_emailConfig.Username, _emailConfig.Password);
					smtpClient.EnableSsl = true;
					smtpClient.Send(message);
				}
			}
			catch
			{
				throw;
			}
			return true;
		}
	}
}
