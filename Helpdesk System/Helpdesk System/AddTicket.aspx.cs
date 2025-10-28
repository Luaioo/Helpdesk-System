using System;
using System.Configuration;
using System.Data.SqlClient;   // To Connect to SQL Server
using System.Web;

namespace Helpdesk_System
{
    public partial class AddTicket : System.Web.UI.Page
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

        protected void Add_Ticket(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);

            try
            {

                    con.Open();
                    String Query = "INSERT INTO [dbo].[Tickets] (Users, Title, Email, UserID ,Department ,UserEmail, Description, Status, Reportdate )  VALUES (@Users, @Title, @Email, @UserID ,@Department ,@UserEmail, @Description, @Status,  @Reportdate ) ";
                    SqlCommand cmd1 = new SqlCommand(Query, con);
                    cmd1.Parameters.AddWithValue("@Users", Convert.ToString(Session["Name"]));
                    cmd1.Parameters.AddWithValue("@Title", IssueTitle.Value);
                    cmd1.Parameters.AddWithValue("@Email", Convert.ToString(Session["Email"]));
                    cmd1.Parameters.AddWithValue("@UserID", UserID.Value);
                    cmd1.Parameters.AddWithValue("@Department", Department.SelectedValue);
                    cmd1.Parameters.AddWithValue("@UserEmail", UserEmail.Value);
                    cmd1.Parameters.AddWithValue("@Description", Description.Value);
                    cmd1.Parameters.AddWithValue("@Status", "Pending");
                    cmd1.Parameters.AddWithValue("@Reportdate", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));


                try
                {
                    cmd1.ExecuteNonQuery();
                    Response.Redirect("/AdminTicketList.aspx");

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