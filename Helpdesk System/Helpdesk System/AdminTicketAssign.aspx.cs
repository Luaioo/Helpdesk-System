using System;
using System.Configuration;
using System.Data.SqlClient;     // to connect to SQL Server
using System.Web;


namespace Helpdesk_System
{
    public partial class AdminTicketAssign : System.Web.UI.Page
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
                SqlCommand cmd = new SqlCommand("SELECT * FROM [Tickets] WHERE [TicketID] = '" + ID + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
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
                    Comments.Value = reader[13].ToString();
                }
                con.Close();

                con.Open();
                int id = Convert.ToInt32(Session["UserID"]);
                SqlCommand cmd1 = new SqlCommand("SELECT * FROM [Users] WHERE [UID] = '" + id + "'", con);
                SqlDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    ProfileName.Text = reader1[1].ToString();
                    ProfileName1.Text = reader1[1].ToString();


                }

                con.Close();
            }
        }


        protected void Assign_Click(object sender, EventArgs e)
        {
            string Status = Session["Status"].ToString();

            if (Status == "Pending")
            {

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString);

                try
                {

                    con.Open();
                    string ID = Convert.ToString(Session["TicketID"]);
                    String Query = "UPDATE Tickets SET AssignedUser=@AssignedUser, Status=@Status , Assigndate=@Assigndate WHERE TicketID=@TicketID ";
                    SqlCommand cmd1 = new SqlCommand(Query, con);
                    cmd1.Parameters.AddWithValue("@TicketID", ID);
                    cmd1.Parameters.AddWithValue("@AssignedUser", Convert.ToString(Session["Name"]));
                    cmd1.Parameters.AddWithValue("@Status", "In Progress");
                    cmd1.Parameters.AddWithValue("@Assigndate", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));


                    try
                    {
                        cmd1.ExecuteNonQuery();
                        Response.Redirect("/AdminAssignlist.aspx");

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
            else if (Session["Status"].ToString() == "In Progress")
            {

                StatusLabel.Text = " Ticket in Progress";

            }
            else

                StatusLabel.Text = " Ticket is Completed";

           
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