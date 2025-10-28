<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="Helpdesk_System.LoginPage" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Login Page</title>
    <link rel="stylesheet" href="assets/vendors/mdi/css/materialdesignicons.min.css">
    <link rel="stylesheet" href="assets/vendors/css/vendor.bundle.base.css">
    <link rel="stylesheet" href="assets/css/style.css">
    <link rel="shortcut icon" href="assets/images/favicon.ico" />
  </head>
 <body>
    <div class="container-scroller">
      <div class="container-fluid page-body-wrapper full-page-wrapper">
        <div class="content-wrapper d-flex align-items-center auth">
          <div class="row flex-grow">
            <div class="col-lg-4 mx-auto">
              <div class="auth-form-light text-left p-5">
                <div class="brand-logo">
                  <img src="assets/images/logo.svg">
                </div>
                <h4>Hello! let's get started</h4>
                <h6 class="font-weight-light">Sign in to continue.</h6>
                   
                <form class="pt-3" runat="server">
                  <div class="form-group">
                    <input id="Email" type="email" class="form-control form-control-lg" placeholder="Email" runat="server"  required>
                  </div>
                  <div class="form-group">
                    <input id="Password" type="password" class="form-control form-control-lg" placeholder="Password" runat="server"  required>
                  </div>
                     <asp:Label ForeColor="Red" Font-Size="14px" ID="StatusLabel" runat="server" data-type="success" data-from="top" data-align="right"></asp:Label>
                  <div class="mt-3">
                   <asp:Button ID="Send" runat="server" type="button" class="btn btn-block btn-gradient-primary btn-lg font-weight-medium auth-form-btn" Text="SIGN IN" onclick="Login" /> 
                  </div>
                  <div class="my-2 d-flex justify-content-between align-items-center">
                    <div class="form-check">
                      <label class="form-check-label text-muted">
                        <input type="checkbox" class="form-check-input"> Keep me signed in </label>
                    </div>
                  </div>
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <script src="assets/vendors/js/vendor.bundle.base.js"></script>
    <script src="assets/js/off-canvas.js"></script>
    <script src="assets/js/hoverable-collapse.js"></script>
    <script src="assets/js/misc.js"></script>
  </body>
  </html>

