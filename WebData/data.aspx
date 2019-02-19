<%@ Page Title="" Language="C#" MasterPageFile="~/login.Master" AutoEventWireup="true" Inherits="WebData.Data" Codebehind="data.aspx.cs" %>

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

                <h2 class="sub-header">Registros</h2>

                <div class=" tab-content">

                    <div id="registros" class="tab-pane fade in active  center">
                        <br />
                        <br />

                        <div class="panel-body">
                            <form id="form1" runat="server">
                                
                                <asp:HiddenField ID="recordId" runat="server" />                                
                                <table class='table table-responsive table-striped table-condensed table-hover'>
                                    <tr> 
                                        <td class='warning'><b>DN</b></td> 
                                        <td><asp:Label ID="lblDn" runat="server"></asp:Label></td> 
                                    </tr> 
                                    <tr> 
                                        <td class='warning'><b>Nombre</b></td> 
                                        <td><asp:Label ID="lblNombre" runat="server"></asp:Label></td> 
                                    </tr> 
                                    <tr> 
                                        <td class='warning'><b>Referencia 1</b></td> 
                                        <td><asp:Textbox ID="txtReferencia1" runat="server"></asp:Textbox></td> 
                                    </tr> 
                                    <tr> 
                                        <td class='warning'><b>Referencia 2</b></td> 
                                        <td><asp:Textbox ID="txtReferencia2" runat="server"></asp:Textbox></td> 
                                    </tr> 
                                    <tr> 
                                        <td class='warning'><b>CAC</b></td> 
                                        <td><asp:Textbox ID="txtCac" runat="server"></asp:Textbox></td> 
                                    </tr> 
                                    <tr> 
                                        <td class='warning'><b>FVC</b></td> 
                                        <td><asp:Label ID="lblFvc" runat="server"></asp:Label></td> 
                                    </tr> 
                                </table>
                                
                                <h3 class="sub-header">Tipificacion</h3>
                                <asp:HiddenField ID="hdfContacto" runat="server" />
                                <select name="contactoOption" id="contactoOption" class="form-control" onchange="javascript: contactoFunction()" required>
                                    <option data-icon="glyphicon glyphicon-heart" value="">Seleccionar</option>
                                    <option data-icon="glyphicon glyphicon-heart" value="Contacto efectivo">Contacto efectivo</option>
                                    <option data-icon="glyphicon glyphicon-heart" value="Contacto no efectivo">Contacto no efectivo</option>
                                </select>
                                <div id="contactoEfectivoDiv" style="display:none;">
                                    <asp:HiddenField ID="hdfEfectivoOption" runat="server" />
                                    <select name="efectivoOption" id="efectivoOption" class="form-control" required>
                                        <option data-icon="glyphicon glyphicon-heart" value="">Seleccionar</option>
                                        <option data-icon="glyphicon glyphicon-heart" value="Promesa">Promesa</option>
                                        <option data-icon="glyphicon glyphicon-heart" value="Recado">Recado</option>
                                    </select>
                                </div>
                                <div id="contactoNoEfectivoDiv" style="display:none;">
                                    <asp:HiddenField ID="hdfNoefectivoOption" runat="server" />
                                    <select name="NoefectivoOption" id="NoefectivoOption" class="form-control" onchange="javascript: rechazoFunction()" required>
                                        <option data-icon="glyphicon glyphicon-heart" value="">Seleccionar</option>
                                        <option data-icon="glyphicon glyphicon-heart" value="Buzon">Buzon</option>
                                        <option data-icon="glyphicon glyphicon-heart" value="Cuelga llamada">Cuelga llamada</option>
                                        <option data-icon="glyphicon glyphicon-heart" value="Llamar mas tarde">Llamar mas tarde</option>
                                        <option data-icon="glyphicon glyphicon-heart" value="No contesta">No contesta</option>
                                        <option data-icon="glyphicon glyphicon-heart" value="Rechazo">Rechazo</option>
                                        <option data-icon="glyphicon glyphicon-heart" value="Rechazo con queja">Rechazo con queja</option>
                                        <option data-icon="glyphicon glyphicon-heart" value="Se corta llamada">Se corta llamada</option>
                                    </select>
                                </div>
                                <div id="quejaDiv" style="display:none;">
                                    <asp:HiddenField ID="hdfQuejaOption" runat="server" />
                                    <select name="quejaOption" id="quejaOption" class="form-control" required>
                                        <option data-icon="glyphicon glyphicon-heart" value="">Seleccionar</option>
                                        <option data-icon="glyphicon glyphicon-heart" value="Omiten o mienten en la FVC">Omiten o mienten en la FVC</option>
                                        <option data-icon="glyphicon glyphicon-heart" value="CAC lejano">CAC lejano</option>
                                        <option data-icon="glyphicon glyphicon-heart" value="Ofrecen el primer mes gratis">Ofrecen el primer mes gratis</option>
                                        <option data-icon="glyphicon glyphicon-heart" value="Cliente queria portar otra linea">Cliente queria portar otra linea</option>
                                        <option data-icon="glyphicon glyphicon-heart" value="Numeros de referencia falsos">Numeros de referencia falsos</option>
                                        <option data-icon="glyphicon glyphicon-heart" value="Cliente rechazo portabilidad en validacion">Cliente rechazo portabilidad en validacion</option>
                                        <option data-icon="glyphicon glyphicon-heart" value="Referidos portados falsos">Referidos portados falsos</option>
                                        <option data-icon="glyphicon glyphicon-heart" value="No le asignan CAC">No le asignan CAC</option>
                                        <option data-icon="glyphicon glyphicon-heart" value="Mienten en la informacion del producto">Mienten en la informacion del producto</option>
                                        <option data-icon="glyphicon glyphicon-heart" value="Sugieren otra documentacion">Sugieren otra documentacion</option>
                                        <option data-icon="glyphicon glyphicon-heart" value="Aseguran liberacion del equipo">Aseguran liberacion del equipo</option>
                                    </select>
                                    <br />                                    
                                </div>
                                 <asp:LinkButton runat="server" ID="newuser2" OnClick="SubmitRecord" class="btn  btn-success" data-toggle="tooltip" data-placement="bottom" title="Guardar" OnClientClick="return validateFields();">
                                        <span aria-hidden="true" class="glyphicon glyphicon-check"></span>Guardar
                                    </asp:LinkButton><p id="validForm" class="text-danger"></p>        
                                </form>
                        </div>
                    </div>                    
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function validateFields() {
            var valid = true;
            if (document.getElementById("contactoOption").value == "")
            {
                document.getElementById("validForm").innerHTML = "Selecciona el tipo de contacto.";
                valid = false;
            }            
            if (contactoEfectivoDiv.style.getPropertyValue("display") == "block" && document.getElementById("efectivoOption").value == "")
            {
                valid = false;
            }
            if (contactoNoEfectivoDiv.style.getPropertyValue("display") == "block" && document.getElementById("NoefectivoOption").value == "")
            {
                valid = false;
            }         
            if (quejaDiv.style.getPropertyValue("display") == "block" && document.getElementById("quejaOption").value == "")
            {
                valid = false;
            }
            if (!valid)
            {
                document.getElementById("validForm").innerHTML = "Selecciona todas las opciones visibles";
                return valid;
            }      
            document.getElementById('<%= hdfContacto.ClientID %>').value = contactoOption.value;
            document.getElementById('<%= hdfEfectivoOption.ClientID %>').value = efectivoOption.value;
            document.getElementById('<%= hdfNoefectivoOption.ClientID %>').value = NoefectivoOption.value;
            document.getElementById('<%= hdfQuejaOption.ClientID %>').value = quejaOption.value;
            return valid;
        }

        function contactoFunction() {
            var x = document.getElementById("contactoOption").value;
            document.getElementById("efectivoOption").value = "";
            document.getElementById("NoefectivoOption").value = "";
            document.getElementById("quejaOption").value = "";
            if (x == "Contacto efectivo") {
                efectivoOption.required = true;
                NoefectivoOption.required = false;
                quejaOption.required = false;
                document.getElementById("contactoEfectivoDiv").style = "display:block;";
                document.getElementById("contactoNoEfectivoDiv").style = "display:none;";
                document.getElementById("quejaDiv").style = "display:none;";                
            }
            else if (x == "Contacto no efectivo") {
                efectivoOption.required = false;
                NoefectivoOption.required = true;
                quejaOption.required = false;
                document.getElementById("contactoEfectivoDiv").style = "display:none;";
                document.getElementById("contactoNoEfectivoDiv").style = "display:block;";
                document.getElementById("quejaDiv").style = "display:none;";
            }
            else {
                efectivoOption.required = false;
                NoefectivoOption.required = true;
                quejaOption.required = true;
                document.getElementById("contactoEfectivoDiv").style = "display:none;";
                document.getElementById("contactoNoEfectivoDiv").style = "display:none;";
                document.getElementById("quejaDiv").style = "display:none;";
            }
        }

        function rechazoFunction() {         
            document.getElementById("efectivoOption").value = "";
            var x = document.getElementById("NoefectivoOption").value;            
            if (x == "Rechazo con queja") {
                quejaOption.required = true;
                document.getElementById("quejaDiv").style = "display:block;";
            }
            else{
                quejaOption.required = false;
                document.getElementById("quejaDiv").style = "display:none;";
            }
        }
    </script>
</asp:Content>

