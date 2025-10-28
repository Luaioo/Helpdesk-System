using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace Helpdesk_System
{
    public partial class UserList : System.Web.UI.Page
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


        protected void LogOut(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Response.Redirect("loginPage.aspx");
        }


        protected void View_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            string UserID = grdrow.Cells[0].Text;
            string Email = grdrow.Cells[2].Text;
            Session["UID"] = grdrow.Cells[0].Text;
            Session["UserEmail"] = grdrow.Cells[2].Text;


            Response.Redirect("AdminUserListDetails.aspx?id=" + UserID + "&Email=" + Email);
        }
    }
}