using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace Helpdesk_System
{
    public partial class UserDashboard : System.Web.UI.Page
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
            try
            {

                con.Open();
                System.Data.SqlClient.SqlCommand AllTickets = new System.Data.SqlClient.SqlCommand("SELECT COUNT(*) FROM Tickets Where Users=@Name", con);
                AllTickets.Parameters.AddWithValue("@Name", Session["Name"].ToString());
                AllTickets.Connection = con;
                int RecordCount = Convert.ToInt32(AllTickets.ExecuteScalar());
                TotalTicket.Text = Convert.ToString(RecordCount);


                System.Data.SqlClient.SqlCommand CompTickets = new System.Data.SqlClient.SqlCommand("SELECT COUNT(*) FROM Tickets Where Status='Completed' AND Users=@Name", con);
                CompTickets.Parameters.AddWithValue("@Name", Session["Name"].ToString());
                CompTickets.Connection = con;
                int RecordCount1 = Convert.ToInt32(CompTickets.ExecuteScalar());
                CompletedTickets.Text = Convert.ToString(RecordCount1);


                System.Data.SqlClient.SqlCommand InprogressTicket = new System.Data.SqlClient.SqlCommand("SELECT COUNT(*) FROM Tickets Where Status='In Progress' AND Users=@Name", con);
                InprogressTicket.Parameters.AddWithValue("@Name", Session["Name"].ToString());
                InprogressTicket.Connection = con;
                int RecordCount2 = Convert.ToInt32(InprogressTicket.ExecuteScalar());
                InProgress.Text = Convert.ToString(RecordCount2);

                System.Data.SqlClient.SqlCommand Pending = new System.Data.SqlClient.SqlCommand("SELECT COUNT(*) FROM Tickets Where Status='Pending' AND Users=@Name", con);
                Pending.Parameters.AddWithValue("@Name", Session["Name"].ToString());
                Pending.Connection = con;
                int RecordCount3 = Convert.ToInt32(Pending.ExecuteScalar());
                PendingTickets.Text = Convert.ToString(RecordCount3);


                int id = Convert.ToInt32(Session["UserID"]);
                SqlCommand cmd = new SqlCommand("SELECT * FROM [Users] WHERE [UID] = '" + id + "'", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ProfileName.Text = reader[1].ToString();
                    ProfileName1.Text = reader[1].ToString();


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

            Response.Redirect("/UserTicketDetails.aspx?TicketID=" + TicketID + "&Title=" + Title);



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