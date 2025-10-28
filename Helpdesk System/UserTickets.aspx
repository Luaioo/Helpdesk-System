<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserTickets.aspx.cs"  Inherits="Helpdesk_System.UserTickets" %>

<!DOCTYPE html>
<html lang="en">
 <head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>User Panel</title>
    <link rel="stylesheet" href="assets/vendors/mdi/css/materialdesignicons.min.css">
    <link rel="stylesheet" href="assets/vendors/css/vendor.bundle.base.css">
    <link rel="stylesheet" href="assets/css/style.css">
    <link rel="shortcut icon" href="assets/images/favicon.ico" />
  </head>

   <body>
   <form runat="server">
    <div class="container-scroller">
	<!-- Navigation Bar -->
   <nav class="navbar default-layout-navbar col-lg-12 col-12 p-0 fixed-top d-flex flex-row">
        <div class="text-center navbar-brand-wrapper d-flex align-items-center justify-content-center">
          <a class="navbar-brand brand-logo" href="UserDashboard.aspx"><img src="assets/images/logo.svg" alt="logo" /></a>
          <a class="navbar-brand brand-logo-mini" href="UserDashboard.aspx"><img src="assets/images/logo-mini.svg" alt="logo" /></a>
        </div>          
        <div class="navbar-menu-wrapper d-flex align-items-stretch">
          <button class="navbar-toggler navbar-toggler align-self-center" type="button" data-toggle="minimize">
            <span class="mdi mdi-menu"></span>
          </button>
          <div class="search-field d-none d-md-block">
              <div class="input-group" style="padding-top: 11px;">
                <div class="input-group-prepend bg-transparent">
                  <i class="input-group-text border-0 mdi mdi-magnify"></i>
                </div>
                <asp:TextBox id="searchBox" type="text"  class="form-control bg-transparent border-0" runat="server" placeholder="Search Keyword" onkeyup="myFunction()"/>
              </div>
          </div>
          <ul class="navbar-nav navbar-nav-right">
            <li class="nav-item nav-profile dropdown">
              <a class="nav-link dropdown-toggle" id="profileDropdown" href="#" data-bs-toggle="dropdown" aria-expanded="false">
                <div class="nav-profile-img">
                  <img src="assets/images/faces/face1.jpg" alt="image">
                  <span class="availability-status online"></span>
                </div>
                <div class="nav-profile-text">
                  <p class="mb-1 text-black"><asp:Label ID="ProfileName" runat="server"></asp:Label></p>
                </div>
              </a>
                <div class="dropdown-menu navbar-dropdown" aria-labelledby="profileDropdown">
                 <a class="dropdown-item" href="UserChangePassword.aspx">
                  <i class="mdi mdi-cached me-2 text-success"></i>Change Password </a>
                <div class="dropdown-divider"></div>
                <a class="dropdown-item" href="LoginPage.aspx">
                  <i class="mdi mdi-logout me-2 text-primary"></i><asp:Button runat="server"  OnClick="LogOut" Text="Sign Out"  style="border: 0;background: border-box;" ></asp:Button></a>
              </div>
            </li>
            <li class="nav-item d-none d-lg-block full-screen-link">
              <a class="nav-link">
                <i class="mdi mdi-fullscreen" id="fullscreen-button"></i>
              </a>
            </li>
          </ul>
          <button class="navbar-toggler navbar-toggler-right d-lg-none align-self-center" type="button" data-toggle="offcanvas">
            <span class="mdi mdi-menu"></span>
          </button>
        </div>
      </nav>
	  
	  
      <div class="container-fluid page-body-wrapper">
	  	<!-- Side Bar -->
        <nav class="sidebar sidebar-offcanvas" id="sidebar">
          <ul class="nav">
            <li class="nav-item nav-profile">
                 <a href="UserDashboard.aspx" class="nav-link">
                <div class="nav-profile-image">
                  <img src="assets/images/faces/face1.jpg" alt="profile">
                  <span class="login-status online"></span>
                </div>
                <div class="nav-profile-text d-flex flex-column">
                  <span class="font-weight-bold mb-2"> <asp:Label ID="ProfileName1" runat="server"></asp:Label></span>
                  <span class="text-secondary text-small">User</span>
                </div>
              </a>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="UserDashboard.aspx">
                <span class="menu-title">Dashboard</span>
                <i class="mdi mdi-home menu-icon"></i>
              </a>
            </li>
            <li class="nav-item">
              <a class="nav-link" data-bs-toggle="collapse" href="#ui-basic" aria-expanded="false" aria-controls="ui-basic">
                <span class="menu-title">Tickets</span>
                <i class="menu-arrow"></i>
                <i class="mdi mdi-tooltip-text menu-icon"></i>
              </a>
              <div class="collapse" id="ui-basic">
                <ul class="nav flex-column sub-menu">
                  <li class="nav-item"> <a class="nav-link" href="UserAddTickets.aspx">Add New Ticket</a></li>
                  <li class="nav-item"> <a class="nav-link" href="UserTickets.aspx">My Tickets</a></li>
                </ul>
              </div>
            </li>
            <li class="nav-item">
              <a class="nav-link" href="UserChangePassword.aspx">
                <span class="menu-title">Change Password</span>
                <i class="mdi mdi-settings menu-icon"></i>
              </a>
            </li>	
          </ul>
        </nav>
		 <!-- End Side Bar -->

         <!-- Page Content -->
        <div class="main-panel">
          <div class="content-wrapper">
            <div class="page-header">
              <h3 class="page-title">
                <span class="page-title-icon bg-gradient-primary text-white me-2">
                  <i class="mdi mdi-home"></i>
                </span> My Tickets
              </h3>
              <nav aria-label="breadcrumb">
                <ul class="breadcrumb">
                  <li class="breadcrumb-item active" aria-current="page">
                  </li>
                </ul>
              </nav>
            </div>
            <div class="row">
              <div class="col-12 grid-margin">
                <div class="card">
                  <div class="card-body">
                    <h4 class="card-title">Ticket List</h4>
                    <div class="table-responsive">
                    <asp:Label ID="StatusLabel" runat="server" data-type="success" data-from="top" data-align="right"></asp:Label>

                                     <asp:SqlDataSource ID="Database" runat="server" ConnectionString="<%$ ConnectionStrings:Database %>" SelectCommand="SELECT * FROM [Tickets] WHERE (Users=@Name) ORDER BY Reportdate DESC "> 
                                        <SelectParameters>
                                            <asp:SessionParameter  Name="Name" SessionField="Name" Type="String"  />
                                        </SelectParameters>
                                      </asp:SqlDataSource>

                                      <asp:GridView class="table" ID="GridView" runat="server" DataKeyNames="TID" DataSourceID="Database" AllowPaging="True" AutoGenerateColumns="False" style="margin-left: 25px; margin-top: 10px; margin-bottom: 50px" BackColor="#ffffff" BorderColor="transparent" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2" Width="97%">
                                           <Columns>  
                                             <asp:BoundField DataField="TicketID" HeaderText="Ticket ID" SortExpression="Ticket ID" />
                                             <asp:BoundField DataField="Title" HeaderText="Issue Title" SortExpression="Issue Title" />
                                             <asp:BoundField DataField="Users" HeaderText="Submitted By" SortExpression="Users" />    
                                             <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />  
                                             <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />  
                                             <asp:BoundField DataField="Reportdate" HeaderText="Issue Date" SortExpression="Issue Date" /> 
                                                <asp:TemplateField HeaderText="View">
                                                  <ItemTemplate>                                                     
                                                   <asp:LinkButton runat="server" ID="View" OnClick="View" class="btn btn-block btn-lg btn-gradient-primary mt-4">View</asp:LinkButton>
                                                  </ItemTemplate>
                                                 </asp:TemplateField>
                                         </Columns>
                                           <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                           <HeaderStyle BackColor="#bb72ff" Font-Bold="True" ForeColor="White" />
                                           <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                           <RowStyle BackColor="#f5f5f5" ForeColor="#181824" />
                                           <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                           <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                           <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                           <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                           <SortedDescendingHeaderStyle BackColor="#93451F" />
                                       </asp:GridView>
         
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <footer class="footer">
            <div class="container-fluid d-flex justify-content-between">
              <span class="text-muted d-block text-center text-sm-start d-sm-inline-block">Copyright 2022 © Luai</span>
              <span class="float-none float-sm-end mt-1 mt-sm-0 text-end"> Designed By <a style="color: #a65fff">Luai</a></span>
            </div>
          </footer>
        </div>
	   <!-- End Page Content -->
      </div>
    </div>
        <script>
            function myFunction() {
                var input, filter, table, tr, td, i, txtValue;
                input = document.getElementById("searchBox");
                filter = input.value.toUpperCase();
                table = document.getElementById("GridView");
                tr = table.getElementsByTagName("tr");
                for (i = 0; i < tr.length; i++) {

                    td = tr[i].getElementsByTagName("td");

                    if (td.length > 0) { // to avoid th 

                        //Search the specific column
                        if (
                            td[0].innerHTML.toUpperCase().indexOf(filter) > -1 ||
                            td[1].innerHTML.toUpperCase().indexOf(filter) > -1 ||
                            td[2].innerHTML.toUpperCase().indexOf(filter) > -1 ||
                            td[3].innerHTML.toUpperCase().indexOf(filter) > -1 ||
                            td[4].innerHTML.toUpperCase().indexOf(filter) > -1 ||
                            td[5].innerHTML.toUpperCase().indexOf(filter) > -1) {
                            tr[i].style.display = "";
                        } else {
                            tr[i].style.display = "none";
                        }
                    }
                }
            }
        </script>  
    <script src="assets/vendors/js/vendor.bundle.base.js"></script>
    <script src="assets/vendors/chart.js/Chart.min.js"></script>
    <script src="assets/js/jquery.cookie.js" type="text/javascript"></script>
    <script src="assets/js/off-canvas.js"></script>
    <script src="assets/js/hoverable-collapse.js"></script>
    <script src="assets/js/misc.js"></script>
    <script src="assets/js/dashboard.js"></script>
    <script src="assets/js/todolist.js"></script>
  </form>
  </body>
</html>






                                               