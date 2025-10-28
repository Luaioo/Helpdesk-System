using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace Helpdesk_System
{
    public partial class AdminDashboard : System.Web.UI.Page
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

            try
            {

                con.Open();
                System.Data.SqlClient.SqlCommand Staff = new System.Data.SqlClient.SqlCommand("SELECT COUNT(*) FROM Tickets");
                Staff.Connection = con;
                int RecordCount = Convert.ToInt32(Staff.ExecuteScalar());
                TotalTicket.Text = Convert.ToString(RecordCount);


                System.Data.SqlClient.SqlCommand Admin = new System.Data.SqlClient.SqlCommand("SELECT COUNT(*) FROM Tickets Where Status='Completed'");
                Admin.Connection = con;
                int RecordCount1 = Convert.ToInt32(Admin.ExecuteScalar());
                CompletedTickets.Text = Convert.ToString(RecordCount1);


                System.Data.SqlClient.SqlCommand Student = new System.Data.SqlClient.SqlCommand("SELECT COUNT(*) FROM Tickets Where Status='In Progress'");
                Student.Connection = con;
                int RecordCount2 = Convert.ToInt32(Student.ExecuteScalar());
                InProgress.Text = Convert.ToString(RecordCount2);

                System.Data.SqlClient.SqlCommand Intake = new System.Data.SqlClient.SqlCommand("SELECT COUNT(*) FROM Tickets Where Status='Pending'");
                Intake.Connection = con;
                int RecordCount3 = Convert.ToInt32(Intake.ExecuteScalar());
                PendingTickets.Text = Convert.ToString(RecordCount3);


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
            catch (Exception ex)
            {
                Response.Write(ex);

            }

        }


        protected void View(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            string TicketID = grdrow.Cells[0].Text;
            string Title = grdrow.Cells[1].Text;
            Session["TicketID"] = grdrow.Cells[0].Text;
            Session["Status"] = grdrow.Cells[4].Text;

            Response.Redirect("/AdminTicketAssign.aspx?TicketID=" + TicketID + "&Title=" + Title);



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