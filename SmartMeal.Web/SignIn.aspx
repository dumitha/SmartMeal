<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="SmartMeal.Web.SignIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign In</title>
    <link href="Content/bootstrap.css" rel="stylesheet" />

    <script src="Scripts/jquery-1.10.2.js"></script>


    <script type="text/javascript" >
        function signIn() {
            var email = $("#email").val();
            var password = $("#password").val();

            if (email == null || email == "") {
                alert("An email address is required");
                return;
            }
            if (password == null || password == "") {
                alert("A password is required");
                return;
            }

            var data = "{'email':'" + email + "', 'psswd': '" + password + "'}";
            //alert(data);
            $.ajax({
                type: "POST",
                url: "SignIn.aspx/SignInUser",
                data: [],
                dataType: "json",
                contentType:"application/json; charset=utf-8",
                success: function (msg) {
                    alert("Success");
                    alert(msg.d);
                    
                },
                error: function onError(err) {

                    alert(err.status);

                }
               
            });
        }

    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class ="form-group"></div>

            <img src="content/images/MainBanner.jpg" class ="img-responsive center-block"  />
            <div class="form-group">
                <label for="email">Email address</label>
                <input type="email" class="form-control" id="email" placeholder="Enter email"  />
            </div>

            <div class="form-group">
                <label for="password">Password</label>
                <input type="password" class="form-control" id="password" placeholder="Password" />
            </div>

            <button type="button" class="btn btn-primary" onclick="signIn()">Submit

            </button>

        </div>
    </form>
</body>
</html>
