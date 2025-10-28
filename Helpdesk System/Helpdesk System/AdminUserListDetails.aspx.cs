using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;


namespace Helpdesk_System
{
    public partial class AdminUserListDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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

            if (!Page.IsPostBack)
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);
                int id = Convert.ToInt32(Session["UID"]);
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM[Users] WHERE[UID] = '" + id + "'", con))
                {

                    cmd.CommandType = CommandType.Text;

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult | CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                        {
                            Name.Value = reader[1].ToString();
                            Email.Value = reader[2].ToString();
                            Typeofuser.SelectedValue = reader[4].ToString();
                        }
                    }
                }
            }
        }


     

        protected void Edit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);

            try
            {
                con.Open();
                string ID = Session["UID"].ToString();
                String Query = "UPDATE Users SET Name=@Name, Email=@Email, Typeofuser=@Typeofuser WHERE UID = " + ID + " ";
                SqlCommand cmd1 = new SqlCommand(Query, con);
                cmd1.Parameters.AddWithValue("@Name", Name.Value);
                cmd1.Parameters.AddWithValue("@Email", Email.Value);
                cmd1.Parameters.AddWithValue("@Typeofuser", Typeofuser.SelectedValue);


                try
                {
                    cmd1.ExecuteNonQuery();
                    Response.Redirect("/AdminUserList.aspx");

                }
                catch (Exception ees)
                {
                    Response.Write(ees);
                }

                con.Close();

            }
            catch (Exception ex)
            {

                Response.Write(ex);

            }
        }


        protected void Delete_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);

            try
            {
                con.Open();
                string ID = Session["UID"].ToString();
                String Query = "DELETE FROM Users WHERE UID = " + ID + " ";
                SqlCommand cmd1 = new SqlCommand(Query, con);

                try
                {
                    cmd1.ExecuteNonQuery();
                    Response.Redirect("/AdminUserList.aspx");

                }
                catch (Exception ees)
                {
                    Response.Write(ees);
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