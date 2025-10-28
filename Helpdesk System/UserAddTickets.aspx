<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserAddTickets.aspx.cs"  Inherits="Helpdesk_System.UserAddTickets" %>




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
                </span> Add New Ticket 
              </h3>
              <nav aria-label="breadcrumb">
                <ul class="breadcrumb">
                  <li class="breadcrumb-item active" aria-current="page">
                  </li>
                </ul>
              </nav>
            </div>      
        <div class="row">
        <div class="col-md-6 grid-margin stretch-card">
                <div class="card">
                  <div class="card-body">
                    <h2>Ticket Details</h2>
                    <p class="card-description"> 
                         <asp:Label ForeColor="Red" Font-Size="Medium" ID="StatusLabel" runat="server" data-type="success" data-from="top" data-align="right"></asp:Label>
                    </p>
                       <div class="form-group row">
                        <label for="exampleInputUsername2" class="col-sm-3 col-form-label">Issue Title</label>
                        <div class="col-sm-9">
                          <input type="text" id="IssueTitle" name="Title" placeholder="Title" runat="server"  class="form-control required" required>
                          <span class="messages"></span>
                        </div>
                      </div>
                       <div class="form-group row">
                        <label for="exampleInputUsername2" class="col-sm-3 col-form-label">Department</label>
                        <div class="col-sm-9">
                            <asp:DropDownList ID="Department" runat="server" name="position" class="form-control form-control-sm required" >
                               <asp:ListItem ></asp:ListItem>
                               <asp:ListItem>Finance</asp:ListItem>
                               <asp:ListItem>Marketing</asp:ListItem>
                               <asp:ListItem>Management</asp:ListItem>
                               <asp:ListItem>Visa Office</asp:ListItem>
                               <asp:ListItem>Registration</asp:ListItem>
                               <asp:ListItem>Other</asp:ListItem>
                              </asp:DropDownList> 
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ForeColor="Red" runat="server" ControlToValidate="Department" ErrorMessage="Required" Display="Dynamic"> </asp:RequiredFieldValidator>
                          <span class="messages"></span>
                        </div>
                      </div>
                       <div class="form-group row">
                        <label for="exampleInputUsername2" class="col-sm-3 col-form-label">Student ID</label>
                        <div class="col-sm-9">
                             <input type="text" id="UserID" name="UserID" placeholder="Student ID" runat="server"  class="form-control required" required>
                          <span class="messages"></span>
                        </div>
                      </div>
                      <div class="form-group row">
                        <label for="exampleInputUsername2" class="col-sm-3 col-form-label">Student Email</label>
                        <div class="col-sm-9">
                             <input type="Email" id="UserEmail" name="UserEmail" placeholder="Student Email (optional)" runat="server"  class="form-control required" >
                          <span class="messages"></span>
                        </div>
                      </div>
                      <div class="form-group row">
                        <label for="exampleInputEmail2" class="col-sm-3 col-form-label">Description</label>
                        <div class="col-sm-9">
                          <textarea  placeholder="Additional info" id="Description" spellcheck="false" name="message" runat="server" class="form-control required" required></textarea>
                            <span class="messages"></span>
                        </div>
                      </div>
                      <div class="form-group row">
                        <div class="col-sm-9">
                           <asp:Button ID="Send" type="submit" runat="server" Class="btn btn-primary m-b-0" Text="Submit" onclick="Add_Ticket" />
                        </div>
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
     

