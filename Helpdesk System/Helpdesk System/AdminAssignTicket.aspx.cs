using System;
using System.Configuration;
using System.Data.SqlClient;     // to connect to SQL Server
using System.Web;
using System.Data;

namespace Helpdesk_System
{
    public partial class AdminAssignTicket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoServerCaching();
            HttpContext.Current.Response.Cache.SetNoStore();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);

            if (Session["Email"] == null || Session["Typeofuser"].ToString() != "Admin")
            {
                Response.Redirect("LoginPage.aspx");
            }
            else
            {
                con.Open();
                string ID = Convert.ToString(Session["TicketID"]);
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM [Tickets] WHERE [TicketID] = '" + ID + "'", con)) 
                {

                    cmd.CommandType = CommandType.Text;
                    using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection | CommandBehavior.SingleResult | CommandBehavior.SingleRow))
                    {
                        if (reader.Read())
                        {

                            TicketID.Value = reader[14].ToString();
                            Users.Value = reader[1].ToString();
                            IssueTitle.Value = reader[2].ToString();
                            Email.Value = reader[3].ToString();
                            Department.Value = reader[4].ToString();
                            UserEmail.Value = reader[5].ToString();
                            UserID.Value = reader[6].ToString();
                            Description.Value = reader[7].ToString();
                            Date.Value = reader[9].ToString();
                            AssignedTo.Value = reader[10].ToString();
                            AssignedDate.Value = reader[11].ToString();
                            CompleteDate.Value = reader[12].ToString();

                            ProfileName.Text = reader[10].ToString();
                            ProfileName1.Text = reader[10].ToString();

   
                        }
                        con.Close();
                    }
                }
            }
        }


        protected void Complete(object sender, EventArgs e)
        {

            if (Session["Status"].ToString() == "In Progress")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);

                try
                {
                    con.Open();
                    string ID = Convert.ToString(Session["TicketID"]);
                    String Query = "UPDATE Tickets SET Status=@Status, Comment=@Comment ,Completedate=@Completedate WHERE TicketID=@TicketID ";
                    SqlCommand cmd1 = new SqlCommand(Query, con);
                    cmd1.Parameters.AddWithValue("@TicketID", ID);
                    cmd1.Parameters.AddWithValue("@Comment", Comment.Value);
                    cmd1.Parameters.AddWithValue("@Status", "Completed");
                    cmd1.Parameters.AddWithValue("@Completedate", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

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
            else
            {
                StatusLabel.Text = " Ticket is Completed";
            }
        }


        protected void DropTicket(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);

            try
            {
                con.Open();
                string ID = Convert.ToString(Session["TicketID"]);
                String Query = "UPDATE Tickets SET Status=@Status, AssignedUser = null ,Assigndate = null, Completedate = null, Comment = null  WHERE TicketID=@TicketID ";
                SqlCommand cmd1 = new SqlCommand(Query, con);
                cmd1.Parameters.AddWithValue("@TicketID", ID);
                cmd1.Parameters.AddWithValue("@Status", "Pending");

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
