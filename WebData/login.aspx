<%@ Page Title="" Language="C#" MasterPageFile="~/login.Master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="WebData.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

    <!--
    This is the Login Page
    -->

    <!-- Some space between Navbar and the conatiner of Login -->
    <br />
    <br />
    <br />
    <br />


    <!-- Container of the Login elemnets -->
    <div class=" col-sm-3 center">
        <div class="well center-block ">
            <form class="form-signin" runat="server">
                <h2 class="form-signin-heading center">Inicie Sesión</h2>
                <label for="myusr" class="sr-only">Usuario</label>
                <input type="text" runat="server" id="myusr" class="form-control" placeholder="Usuario" required autofocus />
                <label for="mypwd" class="sr-only">Contraseña</label>
                <input type="password" runat="server" id="mypwd" class="form-control inp" placeholder="Contraseña" required />
                <br />
                <asp:LinkButton ID="submitlg" runat="server" OnClick="submitcred" Class="btn  btn-success" type="submit" data-toggle="tooltip" data-placement="bottom" title="Iniciar Sesión">
                            <span aria-hidden="true" class="glyphicon glyphicon-user"></span> Enviar
                </asp:LinkButton>
                
            </form>
        </div>

    </div>
    <!-- End Container -->

    <div class=" col-sm-5 center">
        <div runat="server" id="divmsg"></div>
    </div> 


    <script type="text/javascript">
        //$('#body_mypwd').keypress(function (e) {
        //    pwd = document.getElementById("body_mypwd").value;
        //    usr = document.getElementById("body_myusr").value;

        //    if (e.which == 13) {
        //        if (pwd != "" && usr != "") {
        //            __doPostBack("submitcred",usr+"^"+pwd);
        //        } else {
        //            alert("El campo es requerido");
        //        }
        //    }

        //});

    </script>

</asp:Content>
