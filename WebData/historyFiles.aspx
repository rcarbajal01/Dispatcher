<%@ Page Title="" Language="C#" MasterPageFile="~/login.Master" AutoEventWireup="True" CodeFile="historyFiles.aspx.cs" Inherits="WebData.historyFiles" %>

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

            <!-- sidebar -->
            <div class="col-sm-3 col-md-2 sidebar">

                <!-- Fisrt Elemnt of the Sidebar and the default to show the Info -->
                <ul class="nav nav-sidebar">
                    <li><a href="data.aspx"><span aria-hidden="true" class="glyphicon glyphicon-book"></span>&nbsp;Obtener Registro</a></li>
                    <li class="active">
                        <a href="#"><span aria-hidden="true" class="glyphicon glyphicon-save"></span>&nbsp;Descargar Historico<span class="sr-only">(current)</span></a>
                    </li>
                    <li><a href="upload.aspx"><span aria-hidden="true" class="glyphicon glyphicon-cloud-upload"></span>&nbsp;Cargar Datos</a></li>
                    <li><a href="addusr.aspx"><span aria-hidden="true" class="glyphicon glyphicon-user"></span>&nbsp;Añadir Usuario</a></li>
                </ul>


                <ul class="nav nav-sidebar"></ul>
                <!-- End second item sidebar -->

            </div>
            <!--sidebar -->

            <!-- The data Page Container -->
            <div class=" col-sm-4 col-sm-offset-4 col-md-9 col-md-offset-2 main">
                <h2 class="sub-header">Archivos Cargados</h2>

                <!-- TODO replace the table with a script to pull the searched data -->
                <div class="table-responsive">
                    <br />
                    <form id="form1" runat="server">
                        <input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="" />
                        <input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="" />
                        <asp:PlaceHolder ID="historytable" runat="server"></asp:PlaceHolder>
                    </form>
                    <!-- End Table -->
                </div>
                <!-- End table container -->
            </div>
            <!-- End data container -->
        </div>
        <!-- End container as Row-->
    </div>
    <!-- End Global Container -->

    <!-- Simple script for the tooltips -->
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();

            $('#histtable').dataTable({
                "language": {
                    "lengthMenu": "Mostrando  _MENU_   Registros por Página",
                    "zeroRecords": "No se encontraron registros.",
                    "info": "Mostrando Página _PAGE_ de _PAGES_",
                    "infoEmpty": "No hay Registros Disponibles",
                    "infoFiltered": "(Filtrado de _MAX_ registros Totales)",
                    "search": "Buscar Archivo:"
                },
                "paginate": {
                    "next": "Siguiente",
                    "previous": "Anterior"
                }

            });

        });
    </script>
    <script type="text/javascript">
        //<![CDATA[
        var theForm = document.forms['form1'];
        if (!theForm) {
            theForm = document.form1;
        }
        function __doPostBack(eventTarget, eventArgument) {
            if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
                theForm.__EVENTTARGET.value = eventTarget;
                theForm.__EVENTARGUMENT.value = eventArgument;
                theForm.submit();
            }
        }

        //]]>
    </script>

</asp:Content>

