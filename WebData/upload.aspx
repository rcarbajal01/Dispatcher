<%@ Page Title="" Language="C#" MasterPageFile="~/login.Master" AutoEventWireup="true" CodeFile="upload.aspx.cs" Inherits="WebData.upload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="search" runat="server">
    <!--
        Here we add the Search and the menus to the Navbar
        -->

    

    <!-- User Menu upon Login -->
    <ul class="nav navbar-nav navbar-right">
        <li><a href="login.aspx">Salir</a></li>
        <li><a href="#">Ayuda</a></li>
    </ul>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <!--
        This is the main content of the Data User Page
        -->

    

    <!-- Golbal Container -->
    <div class="container-fluid">

        <!-- We format the page to use rows -->
        <div class="row">

             

            <!-- The data Page Container -->
            <div class="center col-sm-5 col-sm-offset-3 col-md-6 col-md-offset-2 main">
                <h3 class="sub-header">Subir Archivo</h3>
                <div class="well">
                    <form class="navbar-form " id="frmadd" runat="server">
                        
                        <input id="addfile" runat="server" type="file" data-show-upload="false" multiple class="file file-loading"  accept="application/vnd.ms-excel" required /><br /><br />
                       <%-- Vigencia Del:   <input  type="date" id="dateIni" required /><asp:HiddenField runat="server" ID="hdf_dateIni" />
                        AL: <input  type="date" id="dateFin" required /><asp:HiddenField runat="server" ID="hdf_dateFin" />--%>
                        <asp:LinkButton runat="server" ID="newuser2" OnClick="submitRecord" class="btn  btn-success" data-toggle="tooltip" data-placement="bottom" title="Guardar" OnClientClick="return validateFields();">
                            <span aria-hidden="true" class="glyphicon glyphicon-import"></span>Guardar
                        </asp:LinkButton><p id="validForm" class="text-danger">
                        <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function validateFields()
        {
            //if (dateIni.value == "" || dateFin.value == "" || body_addfile.value == "")
            if (body_addfile.value == "")
            {
                alert("Completa todos los campos!");
            }
            <%--document.getElementById('<%= hdf_dateIni.ClientID %>').value = dateIni.value;
            document.getElementById('<%= hdf_dateFin.ClientID %>').value = dateFin.value;--%>
            return true;
        }
    </script>
</asp:Content>
