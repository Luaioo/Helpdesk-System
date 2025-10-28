using System;
using System.Configuration;
using System.Data.SqlClient;   // To Connect to SQL Server
using System.Security.Cryptography;  // To encrypt the Admin password
using System.Text;
using System.IO;
using System.Data;


namespace Helpdesk_System
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }
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

        protected void Login(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);

            string strpass = Encrypt(Password.Value);// to encrypt the password
            SqlDataAdapter sda = new SqlDataAdapter("SELECT Typeofuser FROM Users WHERE Email='" + Email.Value + "' and Password='" + strpass + "' ", con);
            DataTable dt = new System.Data.DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                switch (dt.Rows[0]["Typeofuser"] as string)
                {
                    case "Admin":
                        {
                            con.Open();
                            SqlCommand typecomand = new SqlCommand("SELECT * from [dbo].[Users] WHERE Email=@Email AND Password=@Password", con);
                            typecomand.Parameters.AddWithValue("@Email", Email.Value.ToString());
                            typecomand.Parameters.AddWithValue("@Password", strpass);
                            SqlDataReader dr;
                            dr = typecomand.ExecuteReader();
                            dr.Read();
                            Session["UserID"] = dr[0].ToString();
                            Session["Name"] = dr[1].ToString();
                            Session["Email"] = dr[2].ToString();
                            Session["Typeofuser"] = dr[4].ToString();

                            Response.Redirect("/AdminDashboard.aspx");

                            break;
                        }

                    case "User":
                        {

                            con.Open();
                            SqlCommand typecomand = new SqlCommand("SELECT * from [dbo].[Users] WHERE Email=@Email AND Password=@Password", con);
                            typecomand.Parameters.AddWithValue("@Email", Email.Value.ToString());
                            typecomand.Parameters.AddWithValue("@Password", strpass);
                            SqlDataReader dr;
                            dr = typecomand.ExecuteReader();
                            dr.Read();
                            Session["UserID"] = dr[0].ToString();
                            Session["Name"] = dr[1].ToString();
                            Session["Email"] = dr[2].ToString();
                            Session["Typeofuser"] = dr[4].ToString();

                            Response.Redirect("/UserDashboard.aspx");

                            break;
                        }
                }
            }
            else
            {
                StatusLabel.Text = "Wrong Username/Password Credential";

            }





        }
    }
}