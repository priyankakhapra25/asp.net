<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageVendorEmployee.aspx.cs" Inherits="CRMSystem.ManageVendorEmployee" %>
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
                                
                                <h5 class="title">Employee Vendor List</h5>
                            </div>
                            <div class="profile-form">
                                <div class="card-body">
                                    <div class="row">
                                       
                                        <div class="col-4 my-2">
                                    <label>Name <span style="color: red">*</span></label>
                                    <input type="text" class="form-control validated" id="txtName" data-label="Name" />                                          
                                    <label class="Name-error" style="color: red; margin-top: 0.2rem; display: none"></label>

                                </div>
                               <div class="col-4 my-2">
                                    <label class="form-label">Address<span style="color: red">*</span></label>
                                    <input type="text" class="form-control validated" id="txtAddress" data-label="Address" />                                          
                                    <label class="Address-error" style="color: red; margin-top: 0.2rem; display: none"></label>                                  
                                </div> 
                            <div class="col-4 my-2">
                                    <label class="form-label">Contact Number<span style="color: red">*</span></label>
                                    <input type="text" class="form-control validated" id="txtContact" oninput="this.value = this.value.replace(/[^0-9]/g, '')" data-label="ContactNumber" />                                          
                                    <label class="ContactNumber-error" style="color: red; margin-top: 0.2rem; display: none"></label>                                  
                                </div> 
                            <div class="col-4 my-2">
                                    <label class="form-label">Email<span style="color: red">*</span></label>
                                    <input type="text" class="form-control validated" aria-multiline="true" id="txtEmailID" data-label="Email" />                                          
                                    <label class="Email-error" style="color: red; margin-top: 0.2rem; display: none"></label>                                  
                                </div>  
                            <div class="col-4 my-2">
                                    <label class="form-label">Date Of Birth<span style="color: red">*</span></label>
                                    <input type="date" class="form-control validated" aria-multiline="true" id="txtDOB" data-label="DateOfBirth" />                                          
                                    <label class="DateOfBirth-error" style="color: red; margin-top: 0.2rem; display: none"></label>                                  
                                </div>           
                            <div class="col-4 my-2">
                                    <label class="form-label">UserName<span style="color: red">*</span></label>
                                    <input type="text" class="form-control validated" aria-multiline="true" id="txtUserName" data-label="UserName" />                                          
                                    <label class="UserName-error" style="color: red; margin-top: 0.2rem; display: none"></label>                                  
                                </div> 
                            <div class="col-4 my-2">
                                    <label class="form-label">Password<span style="color: red">*</span></label>
                                    <input type="text" class="form-control validated" aria-multiline="true" id="txtPass" data-label="Password" />                                          
                                    <label class="Password-error" style="color: red; margin-top: 0.2rem; display: none"></label>                                  
                                </div>  
                            <div class="col-4 my-2">
                                    <label class="form-label">Role<span style="color: red">*</span></label>
                                    <select class="form-control validated" aria-multiline="true" id="txtRole" data-label="Password"></select>                                          
                                    <label class="Password-error" style="color: red; margin-top: 0.2rem; display: none"></label>                                  
                                </div>   
                                <div class="row">
                                    <div class="col-4">
                                        <button class="btn btn-primary my-3" id="save" onclick="return validateAndPostBack(event);">Save Details</button> 

                                    </div>
                                </div>
                                        <hr />


                                        <div class="table-responsive">
                                        <table id="example" class="display table">
                                            <thead>
                                                <tr>
                                                    <th>Action</th>
                                                    <th>Name</th> 
                                                    <th>Contact Number</th>                                                                                                     
                                                    <th>Date Of Birth</th>
                                                    <th>Status</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                            </tbody>
                                        </table>
                                    </div>                              
                                    </div>
                                </div>
                               
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js" integrity="sha512-v2CJ7UaYy4JwqLDIrZUI/4hqeoQieOmAZNXBeQyjo21dadnwR+8ZaIJVT8EE2iyI61OV8e6M8PP2/4hpQINQ/g==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="js/jquery-ui.min.js"></script>
    <script src="js/jquery.min.js"></script>
    <script src="js/GetCookie.js"></script>
    <script src="js/CustomeValidation.js"></script>
    <script src="js/Ajax/ManageVendorEmployee.js"></script>
</asp:Content>
