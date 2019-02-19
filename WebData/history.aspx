<%@ Page Title="" Language="C#" MasterPageFile="~/login.Master" AutoEventWireup="true" CodeFile="history.aspx.cs" Inherits="WebData.history" %>

<asp:Content ID="Content3" ContentPlaceHolderID="search" runat="server">
    <!--
        Here we add the Search and the menus to the Navbar
        -->

    <!-- User Menu upon Login -->
    <ul class="nav navbar-nav navbar-right">
        <li><a href="login.aspx">Salir</a></li>
        <li><a href="#">Ayuda</a></li>
    </ul>

    <!-- Here we create the search input box for get the data-->
    <!--<form class="navbar-form navbar-right" id="frmsearchH">
        <input type="text" class="form-control" placeholder="Buscar Archivo..." id="searchH" required autofocus autocomplete />
        <button class="btn btn-info" id="btnsearchH" data-toggle="tooltip" data-placement="bottom" title="Buscar">
            <span class="glyphicon glyphicon-search"></span>
        </button>
    </form>-->

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <!--
        This is the main content of the Data User Page
        -->

    <!-- Golbal Container -->
    <div class="container-fluid">

        <!-- We format the page to use rows -->
        <div class="row">

             

            <!-- The data Page Container -->
            <div class="center col-sm-5 col-sm-offset-3 col-md-6 col-md-offset-2 main">
                <h3 class="sub-header">Descargar Ventas</h3>
                <div class="well">
                    <form class="navbar-form " id="frmadd" runat="server">                        
                        Obtener registros Del:<input  type="date" id="dateIni" required /><asp:HiddenField runat="server" ID="hdf_dateIni" />
                        AL:<input  type="date" id="dateFin" required /><asp:HiddenField runat="server" ID="hdf_dateFin" />
                        <asp:LinkButton runat="server" ID="newuser2" OnClick="exportRecords" class="btn  btn-success" data-toggle="tooltip" data-placement="bottom" title="Guardar" OnClientClick="return validateFields();">
                            <span aria-hidden="true" class="glyphicon glyphicon-export"></span>Exportar
                        </asp:LinkButton><p id="validForm" class="text-danger">
                        <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="true" ></asp:Label>
                    </form>
                </div>
            </div>
        </div>
    </div>

    <!-- Simple script for the tooltips -->
    <script type="text/javascript">
        function validateFields() {
            if (dateIni.value == "" || dateFin.value == "") {
                document.getElementById('<%= lblError.ClientID %>').innerHTML = "Proveer rango!";
                return false;
            }
            document.getElementById('<%= lblError.ClientID %>').innerHTML = "";
            document.getElementById('<%= hdf_dateIni.ClientID %>').value = dateIni.value;
            document.getElementById('<%= hdf_dateFin.ClientID %>').value = dateFin.value;
            return true;
        }
    </script>

</asp:Content>

