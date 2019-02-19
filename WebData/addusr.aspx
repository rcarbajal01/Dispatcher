<%@ Page Title="" Language="C#" MasterPageFile="~/login.Master" AutoEventWireup="true" Inherits="WebData.addusr" Codebehind="addusr.aspx.cs" %>

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


    <!-- Golbal Container -->
    <div class="container-fluid">

        <!-- We format the page to use rows -->
        <div class="row">

            <div class="center col-sm-9 col-sm-offset-3 col-md-7 col-md-offset-2 main">

                <h2 class="sub-header">Usuarios</h2>

                <ul class="nav nav-pills nav-justified success">
                    <li role="presentation" class="active"><a href="#users" data-toggle="tab"><span aria-hidden="true" class="glyphicon glyphicon-user"></span>&nbsp;Usuarios Existentes</a></li>
                    <li role="presentation"><a href="#adduser" data-toggle="tab"><span aria-hidden="true" class="glyphicon glyphicon-plus-sign"></span>&nbsp;Añadir Usuarios</a></li>

                </ul>

                <div class=" tab-content">

                    <div id="users" class="tab-pane fade in active  center">
                        <br />
                        <br />

                        <div class="table-responsive">
                            <form id="form1" runat="server">
                                <input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="" />
                                <input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="" />
                                <asp:PlaceHolder ID="userstable" runat="server"></asp:PlaceHolder>
                            </form>

                        </div>
                    </div>



                    <div id="adduser" class="tab-pane fade">
                        <br />
                        <div class="panel panel-warning">
                            <div class="panel-heading">Añada un Nuevo Usuario</div>
                            <div class="panel-body">

                                <label for="myname" class="sr-only">Nombre</label>
                                <input type="text" id="myaddname" class="form-control" placeholder="Nombre Completo" required autofocus />
                                <label for="myusr" class="sr-only">Usuario</label><br />
                                <input type="text" id="myaddusr" class="form-control" placeholder="Usuario" required />
                                <label for="mypwd" class="sr-only">Contraseña</label><br />
                                <input type="password" id="myaddpwd" class="form-control" placeholder="Contraseña" required /><br />
                                <select name="usrtype" id="usrtype" class="form-control">
                                    <option data-icon="glyphicon glyphicon-heart" value="Despachador">Despachador</option>
                                    <option value="Admin">Administrador</option>
                                </select>
                                <br /><br />
                                <button id="newuser2" onclick="javascript:AddUser()" class="btn  btn-success" data-toggle="tooltip" data-placement="bottom" title="Crear Usuario">
                                    <span aria-hidden="true" class="glyphicon glyphicon-check"></span>Enviar
                                </button>

                                
                            </div>
                        </div>
                    </div>


                </div>
            </div>
        </div>
    </div>


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

        function del(action, id) {
            var ask = window.confirm("Se eliminará el usuario seleccionado")
            if (ask == true) {
                __doPostBack(action, id);
            }
        }

        function AddUser() {
            name = document.getElementById("myaddname").value;
            usr = document.getElementById("myaddusr").value;
            pwd = document.getElementById("myaddpwd").value;
            utype = document.getElementById("usrtype").value;
            if (name != "" && usr != "" && pwd != "") {

                __doPostBack("addreg", name + ":" + usr + ":" + pwd + ":" + utype);
            } else {

                alert("Todos los campos son Requeridos");


            }

        }
        //]]>
    </script>


    <script type="text/javascript">
        $(document).ready(function () {

            $('#users a').click(function (e) {
                e.preventDefault()
                $(this).tab('show')
            })

            $('#adduser a').click(function (e) {
                e.preventDefault()
                $(this).tab('show')
            })


            $('#usrtable').dataTable({
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





</asp:Content>
