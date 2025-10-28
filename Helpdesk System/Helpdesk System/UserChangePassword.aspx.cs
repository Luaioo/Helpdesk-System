using System;
using System.Configuration;
using System.Data.SqlClient;   // to connect to SQL Server
using System.Security.Cryptography;  // To encrypt the Admin password
using System.Data;
using System.Text;
using System.IO;
using System.Web;

namespace Helpdesk_System
{
    public partial class UserChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoServerCaching();
            HttpContext.Current.Response.Cache.SetNoStore();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);

            if (Session["Email"] == null || Session["Typeofuser"].ToString() != "User")
            {
                Response.Redirect("LoginPage.aspx");
            }
            else
            {
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


        private string Encrypt(string clearText)  //to AES encrypt password
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


        protected void ChangePassword(object sender, EventArgs e)
        {

            int id = Convert.ToInt32(Session["UserID"]);
            string strpass = Encrypt(CurrentPassword.Value);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT password from Users WHERE Password= '" + strpass + "' AND UID = '" + id + "'  ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count.ToString() == "1")
            {
                string strnewpass = Encrypt(NewPassword.Value);
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Users SET Password ='" + strnewpass + "' WHERE [UID] = '" + id + "'", con);
                cmd.ExecuteNonQuery();


                Lable.Text = "Password Updated Successfully";
                con.Close();

            }
            else
            {
                Lable.Text = "Current Password Is Wrong";

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