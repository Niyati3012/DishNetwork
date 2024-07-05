namespace DishNetwork.Helper
{
	public class EmailManager
	{
		/// <summary>
		/// Gets the content of a mail template file.
		/// </summary>
		/// <param name="templateFileName">The file name of the template.</param>
		/// <returns>The content of the mail template.</returns>
		private static string GetMailTemplate(string templateFileName)
		{
			// Initialize an empty string to store the mail template content
			string strMailTemplet = string.Empty;

			// Read the contents of the mail template file
			using (StreamReader sr = new StreamReader("" + templateFileName))
			{
				// Read each line of the file until the end
				string sLine;
				while ((sLine = sr.ReadLine()) != null)
				{
					// Append the line to the mail template content
					strMailTemplet += sLine;
				}
			}

			// Return the mail template content
			return strMailTemplet;
		}

		/// <summary>
		/// Sends an email containing a one-time password (OTP) for account verification.
		/// </summary>
		/// <param name="strToName">The name of the recipient.</param>
		/// <param name="strMailInfor">The OTP value.</param>
		/// <param name="strToMail">The email address of the recipient.</param>
		public static void HtmlViewer_SendMailForOTP(string strToName, string strMailInfor, string strToMail)
		{
			// Get the content of the mail template file
			string strMailTemplet = GetMailTemplate("UserLoginOTP.html");

			// Replace placeholders in the mail template with actual values
			strMailTemplet = strMailTemplet.Replace("[[ToName]]", strToName);
			strMailTemplet = strMailTemplet.Replace("[[OtpValue]]", strMailInfor);

			// Set the subject of the email
			string strEmailSubject = "Account Verification";

			// Send the email containing the OTP
			//EmailHelper.SendEmailForOTp(strToMail, strEmailSubject, strMailTemplet);
		}
	}
}

