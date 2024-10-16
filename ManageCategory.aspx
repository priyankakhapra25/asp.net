<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageCategory.aspx.cs" Inherits="CRMSystem.ManageCategory" %>
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
                                
                                <h5 class="title">Category List</h5>
                                  <button class="btn btn-primary" type="button" data-bs-toggle="modal" data-bs-target="#exampleModalCenter" style="margin-right:2rem"><i class="fa fa-plus"></i></button>
                            </div>
                            <div class="profile-form">
                                <div class="card-body">
                                    <div class="row">
                                       
                                        <div class="table-responsive">
                                        <table id="example" class="display table">
                                            <thead>
                                                <tr>
                                                    <th>Action</th>
                                                    <th>Name</th>                                                                                                     
                                                    <th>Status</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                            </tbody>
                                        </table>
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
        </div>
        <div class="modal fade" id="exampleModalCenter" >
            <div class="modal-dialog modal-dialog-centered" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        
                        <h6 class="modal-title">Manage Category</h6>
                        <button type="button" class="btn-close Clearbtn" data-bs-dismiss="modal">
                        </button>
                    </div>
                    <div class="modal-body">                        
                        <div class="row">
                               <div class="col-sm-12 my-2">
                                    <label class="form-label">Name<span style="color: red">*</span></label>
                                    <input type="text" class="form-control validated" id="txtName" data-label="Name" />                                          
                                    <label class="Name-error" style="color: red; margin-top: 0.2rem; display: none"></label>                                  
                                </div>                                   
                        </div>
                    </div>
                    <div class="modal-footer">                      
                        <button class="btn btn-primary me-3" id="save" onclick="return validateAndPostBack(event);">Save Details</button> 
                         <button data-bs-dismiss="modal" type="button" class="btn  btn-danger light Clearbtn" id="Clearbtn">Cancel</button>
                    </div>
                       
                </div>
            </div>
        </div>
    
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js" integrity="sha512-v2CJ7UaYy4JwqLDIrZUI/4hqeoQieOmAZNXBeQyjo21dadnwR+8ZaIJVT8EE2iyI61OV8e6M8PP2/4hpQINQ/g==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="js/jquery-ui.min.js"></script>
    <script src="js/jquery.min.js"></script>
    <script src="js/GetCookie.js"></script>
    <script src="js/CustomeValidation.js"></script>
    <script src="js/Ajax/ManageCategory.js"></script>

</asp:Content>
