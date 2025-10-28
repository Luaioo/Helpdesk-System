using System;
using System.Configuration;
using System.Data.SqlClient;   // To Connect to SQL Server
using System.Security.Cryptography;  // To encrypt the user password
using System.Text;
using System.IO;
using System.Web.UI;
using System.Web;
using System.Net.Mail;


namespace Helpdesk_System
{
    public partial class AddUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoServerCaching();
            HttpContext.Current.Response.Cache.SetNoStore();

            if (Session["Email"] == null || Session["Typeofuser"].ToString() != "Admin")
            {

                Response.Redirect("LoginPage.aspx");

            }
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
                con.Open();
                int id = Convert.ToInt32(Session["UserID"]);
                SqlCommand cmd = new SqlCommand("SELECT * FROM [Users] WHERE [UID] = '" + id + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ProfileName.Text = reader[1].ToString();
                    ProfileName1.Text = reader[1].ToString();


                }
            }
        }


        private static string CreateRandomPassword(int length = 10)
        {
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }  // To Create 12 digits Random Password

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }


        private string PopulateBody(string Name, string Email, string Password)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/assets/Email/UserCredential.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Name}", Name);
            body = body.Replace("{Email}", Email);
            body = body.Replace("{Password}", Password);
            return body;
        }


        private void SendHtmlFormattedEmail(string body)
        {

            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress("balighfib@gmail.com");
                mailMessage.To.Add(Email.Value);
                mailMessage.Subject = "Helpdesk System | New User Registered";
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new
                System.Net.NetworkCredential("balighfib@gmail.com", "Ba1234567");
                smtpClient.Send(mailMessage);

            }
        } // Mail Function


        protected void Add_User(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);


            try
            {
                con.Open();
                String query = "SELECT COUNT(*) FROM [dbo].[Users] WHERE Email ='" + Email.Value + "'";
                SqlCommand cmd1 = new SqlCommand(query, con);
                int check = Convert.ToInt32(cmd1.ExecuteScalar().ToString());
                if (check > 0)
                {
                    string script = "alert(\"Acount alrady exist!\");";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                }
                else
                {

                    string Typeofuser = string.Empty;

                    if (Checkbox1.Checked)
                    {
                        Typeofuser = "User";
                    }
                    else if (Checkbox2.Checked)
                    {
                        Typeofuser = "Admin";
                    }

                    String Query;
                    string Password = CreateRandomPassword();
                    string strpass = Encrypt(Password);
                    Query = "INSERT INTO [dbo].[Users] (Name, Email, Password, Typeofuser ) VALUES (@Name, @Email, @Password, @Typeofuser)";
                    SqlCommand cmd = new SqlCommand(Query, con);
                    cmd.Parameters.AddWithValue("@Name", Name.Value);
                    cmd.Parameters.AddWithValue("@Email", Email.Value);
                    cmd.Parameters.AddWithValue("@Password", strpass);
                    cmd.Parameters.AddWithValue("@Typeofuser", Typeofuser);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        string body = PopulateBody(Name.Value, Email.Value, Password);
                        SendHtmlFormattedEmail(body);
                        Response.Redirect("/AdminUserList.aspx");

                    }
                    catch (Exception ees)
                    {
                        Response.Write(ees);
                    }
                }

                con.Close();

            }
            catch (Exception ex)
            {

                Response.Write(ex);

            } 
        }










protected void LogOut(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Response.Redirect("loginPage.aspx");
        }
    }
}