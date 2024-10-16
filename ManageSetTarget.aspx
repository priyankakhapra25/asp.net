<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageSetTarget.aspx.cs" Inherits="CRMSystem.ManageSetTarget" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link rel="stylesheet" href="css/tableresponsive.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <!--**********************************
            Content body start
        ***********************************-->
        <input type="hidden" id="hfID" value="0" />
        <div class="content-body" style="min-height: 500px !important;">
            <div class="container-fluid">
                <!-- row -->
                <div class="row">
                    <div class="col-xl-12 col-lg-12">
                        <div class="card profile-card card-bx m-b30">
                            <div class="card-header">
                                
                                <h5 class="title">Set Target List</h5>
                            </div>
                            <div class="profile-form">
                                <div class="card-body">
                                   
                                 <div class="row">
                                <div class="col-6 my-2">
                                    <label class="form-label">Role<span style="color: red">*</span></label>
                                 <select id="role" data-roleid="" class="form-control validated1" data-label="Role"> </select>
                                    <label class="Role-error" style="color: red; margin-top: 0.2rem; display: none"></label>                                  
                                </div>  
                                <div class="col-6 my-2">
                                    <label class="form-label">Month/ Year<span style="color: red">*</span></label>
                                  <input type="text" class="form-control name validated1" id="txtName" data-label="Name" readonly="true" />                                                                            
                                    <label class="Name-error" style="color: red; margin-top: 0.2rem; display: none"></label>                                  
                                </div> 
                                     <hr />
                                      <div class="table-responsive">
                                        <table id="customtable" class="display table  vendortable">

                                            <thead>
                                                <tr>
                                                    <th>Sno</th>
                                                    <th>Employee Name</th>
                                                    <th>Role Name</th>
                                                    <th>Amount</th>
                                                    <th>New Amount</th>

                                                    
                                                </tr>
                                            </thead>
                                            <tbody>
                                            </tbody>
                                        </table>
                                    </div>         
                        </div>
                                                           
                        </div>
                                </div>
                               <%-- <div class="card-footer justify-content-start">
                                                                
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        
             
    
                                                <!-- Button to open modal -->


           







    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js" integrity="sha512-v2CJ7UaYy4JwqLDIrZUI/4hqeoQieOmAZNXBeQyjo21dadnwR+8ZaIJVT8EE2iyI61OV8e6M8PP2/4hpQINQ/g==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="js/jquery-ui.min.js"></script>
    <script src="js/jquery.min.js"></script>
    <script src="js/GetCookie.js"></script>
    <script src="js/CustomeValidation.js"></script>
    <script src="js/Ajax/ManageSetTarget.js"></script>

</asp:Content>
