using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web;

using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Mail;

/// <summary>
/// Summary description for Alert
/// </summary>
public class Alert
{
    public Alert()
    {
    }

    private string mailfrom;
    private string mailto;
    private string mailobject;
    private string mailbody;

    public string MailFrom
    {
        get { return mailfrom; }
        set { mailfrom = value; }
    }

    public string MailTo
    {
        get { return mailto; }
        set { mailto = value; }
    }

    public string MailObject
    {
        get { return mailobject; }
        set { mailobject = value; }
    }

    public string MailBody
    {
        get { return mailbody; }
        set { mailbody = value; }
    }

    public bool SendAlert(Alert alert, DataSet Emails)
    {
        bool Success = false;
        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(alert.MailFrom);
            for (int i = 0; i <= Emails.Tables[0].Rows.Count  - 1; i++)
            {
                mail.To.Add(Emails.Tables[0].Rows[i][0].ToString());
            }
            mail.Subject = alert.MailObject;
            mail.Body = alert.MailBody;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("e.ettazi", "TMC90minutes");
            smtp.Send(mail);
            Success = true;
        }
        catch
        {
            Success = false;
        }

        return Success;
    }

    public string SendAlert(Alert alert)
    {
        string Success = "";
        try
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(alert.MailFrom);
            mail.To.Add(alert.MailTo);

            mail.Subject = alert.MailObject;
            mail.Body = alert.MailBody;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Credentials = new NetworkCredential(@"e.ettazi","TMC90minutes");
            
           // SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["SMTP"].ToString());
            //smtp.Credentials = new NetworkCredential(@"" + ConfigurationManager.AppSettings["UserSMTP"].ToString(), ConfigurationManager.AppSettings["PasswordSMTP"].ToString());
            smtp.Send(mail);
            Success = "Message Envoyé";
        }
        catch (Exception Ex)
        {
            Success = Ex.ToString();
        }

        return Success;
    }
}
