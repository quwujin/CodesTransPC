using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace YHFramework.Common
{
	public class EmailTool
	{
		public string Address_From;

		public string Address_FromName;

		public string Address_To;

		public string Subject;

		public string BodyText;

		public bool HtmlFormat;

		public string SMTPServer;

		public string Bcc;

		public string CC;

		public List<string> AttachFiles;

		public string ReplyTo;

		public string ReplyToName;

		private MailMessage TheMail;

		public EmailTool()
		{
			this.TheMail = new MailMessage();
			this.AttachFiles = new List<string>();
		}

		public static void sendEmail(string address_To, string subject, string bodyText, string file)
		{
			EmailTool email = new EmailTool();
			email.Address_From = "项目管理系统通知<report@iseedling.com>";
			email.Address_To = address_To;
			email.Subject = subject;
			email.BodyText = bodyText;
			if (!string.IsNullOrEmpty(file))
			{
				email.AttachFiles.Add(file);
			}
			email.SMTPServer = "smtp.exmail.qq.com";
			string result = email.Send();
		}

		public string Send()
		{
			this.TheMail.From = new MailAddress(this.Address_From, this.Address_FromName);
			string result;
			if (string.IsNullOrEmpty(this.Address_To))
			{
				result = "";
			}
			else
			{
				string[] _addresses = this.Address_To.Split(new char[]
				{
					';'
				});
				string[] array = _addresses;
				for (int i = 0; i < array.Length; i++)
				{
					string _item = array[i];
					if (!string.IsNullOrEmpty(_item))
					{
						if (!this.IsEmail(_item))
						{
							result = "the email address : " + _item + " is invalid";
							return result;
						}
						this.TheMail.To.Add(_item);
					}
				}
				if (!string.IsNullOrEmpty(this.CC))
				{
					_addresses = this.CC.Split(new char[]
					{
						';'
					});
					array = _addresses;
					for (int i = 0; i < array.Length; i++)
					{
						string _item = array[i];
						if (!string.IsNullOrEmpty(_item))
						{
							if (!this.IsEmail(_item))
							{
								result = "the email address : " + _item + " is invalid";
								return result;
							}
							this.TheMail.CC.Add(_item);
						}
					}
				}
				if (!string.IsNullOrEmpty(this.Bcc))
				{
					_addresses = this.Bcc.Split(new char[]
					{
						';'
					});
					array = _addresses;
					for (int i = 0; i < array.Length; i++)
					{
						string _item = array[i];
						if (!string.IsNullOrEmpty(_item))
						{
							if (!this.IsEmail(_item))
							{
								result = "the email address : " + _item + " is invalid";
								return result;
							}
							this.TheMail.Bcc.Add(_item);
						}
					}
				}
				if (!string.IsNullOrEmpty(this.ReplyTo))
				{
					this.TheMail.ReplyTo = new MailAddress(this.ReplyTo, this.ReplyToName);
				}
				foreach (string _filename in this.AttachFiles)
				{
					this.TheMail.Attachments.Add(new Attachment(_filename));
				}
				this.TheMail.Subject = this.Subject;
				this.TheMail.Body = this.BodyText;
				this.TheMail.IsBodyHtml = this.HtmlFormat;
				this.TheMail.BodyEncoding = Encoding.UTF8;
				this.TheMail.Priority = MailPriority.Normal;
				string _result = "";
				try
				{
					SmtpClient smtpClient = new SmtpClient();
					if (this.SMTPServer != "")
					{
						smtpClient.Host = this.SMTPServer;
					}
					string smtpUserName = "report@iseedling.com";
					string SmtpPassword = "yh9012309";
					if (!string.IsNullOrEmpty(smtpUserName) && !string.IsNullOrEmpty(SmtpPassword))
					{
						smtpClient.Credentials = new NetworkCredential(smtpUserName, SmtpPassword);
					}
					smtpClient.Send(this.TheMail);
				}
				catch (Exception ex)
				{
					_result = string.Concat(new object[]
					{
						ex.Message,
						"--",
						ex.InnerException,
						"--",
						ex.StackTrace
					});
				}
				result = _result;
			}
			return result;
		}

		private bool IsEmail(string source)
		{
			return Regex.IsMatch(source, "^[A-Za-z0-9](([_\\.\\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\\.\\-]?[a-zA-Z0-9]+)*)\\.([A-Za-z]{2,})$", RegexOptions.IgnoreCase);
		}
	}
}
