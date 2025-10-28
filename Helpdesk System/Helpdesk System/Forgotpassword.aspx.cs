using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;


namespace Helpdesk_System
{
    public partial class Forgotpassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private string PopulateBody(string Password)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/assets/Email/ChangePassword.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Password}", Password);
            return body;
        }


        private void SendHtmlFormattedEmail(string body)
        {

            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress("balighfib@gmail.com");
                mailMessage.To.Add(Email.Value);
                mailMessage.Subject = "Helpdesk System | Forgot Password";
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new
                System.Net.NetworkCredential("balighfib@gmail.com", "Ba1234567");
                smtpClient.Send(mailMessage);

            }
        }


        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }


        protected void Submit(object sender, EventArgs e)
        {


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
            try
            {
                con.Open();
                String query = "SELECT COUNT(1) FROM [dbo].[Users] WHERE Email=@Email";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", Email.Value);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 1)
                {
                    SqlCommand typecomand = new SqlCommand("SELECT * from [dbo].[Users] WHERE Email=@Email", con);
                    typecomand.Parameters.AddWithValue("@Email", Email.Value);
                    SqlDataReader dr;
                    dr = typecomand.ExecuteReader();
                    dr.Read();
                    string HashPassword = dr[3].ToString();
                    string Password = Decrypt(HashPassword);


                    string body = PopulateBody(Password);
                    SendHtmlFormattedEmail(body);
                    Response.Redirect("/LoginPage.aspx");


                }
                else
                {
                    Response.Redirect("/LoginPage.aspx");
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex);

            }


        }
    }
}