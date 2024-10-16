<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageEmployee.aspx.cs" Inherits="CRMSystem.ManageEmployee" %>
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
                                
                                <h5 class="title">Employee List</h5>
                                <span> <b><u> <a href="ManageOfferForm.aspx"> Offer Form</a></u>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                  <u>  <a href="ManageSetTarget.aspx"> Set Target</a></u> </b></span>
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
                                                    <th>Contact Number</th>                                                                                                     
                                                    <th>Role</th>
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
                        
                        <h6 class="modal-title">Manage Employee</h6>
                        <button type="button" class="btn-close Clearbtn" data-bs-dismiss="modal">
                        </button>
                    </div>
                    <div class="modal-body">                        
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
                            <div class="col-4 my-2">
                                    <label class="form-label">Report To<span style="color: red">*</span></label>
                                    <select class="form-control validated" aria-multiline="true" id="txtReport" data-label="ReportTo"></select>                                          
                                    <label class="ReportTo-error" style="color: red; margin-top: 0.2rem; display: none"></label>                                  
                                </div>   
                            <div class="col-4 my-2">
                                    <label class="form-label">Assign Auditor<span style="color: red">*</span></label>
                                    <select class="form-control validated" aria-multiline="true" id="txtAuditor" data-label="AssignAuditor"></select>                                          
                                    <label class="AssignAuditor-error" style="color: red; margin-top: 0.2rem; display: none"></label>                                  
                                </div> 
                            <div class="col-4 my-2">
                                    <label class="form-label">Salary<span style="color: red">*</span></label>
                                    <input type="text" class="form-control validated" id="txtSalary" oninput="this.value = this.value.replace(/[^0-9]/g, '')" data-label="Salary" />                                          
                                    <label class="Salary-error" style="color: red; margin-top: 0.2rem; display: none"></label>                                  
                                </div>   
                            <div class="col-4 my-2">
                                    <label class="form-label">Emergency Contact<span style="color: red">*</span></label>
                                    <input type="text" class="form-control validated" id="txtEmergencyContact" oninput="this.value = this.value.replace(/[^0-9]/g, '')" data-label="EmergencyContact" />                                          
                                    <label class="EmergencyContact-error" style="color: red; margin-top: 0.2rem; display: none"></label>                                  
                                </div> 
                            <div class="col-4 my-2">
                                    <label class="form-label">Joining Date<span style="color: red">*</span></label>
                                    <input type="date" class="form-control validated" aria-multiline="true" id="txtJoinDate" data-label="JoiningDate" />                                          
                                    <label class="JoiningDate-error" style="color: red; margin-top: 0.2rem; display: none"></label>                                  
                                </div> 
                            <div class="col-4 my-2">
                                    <label class="form-label">Task Limit<span style="color: red">*</span></label>
                                    <input type="text" class="form-control validated" id="txtTask" oninput="this.value = this.value.replace(/[^0-9]/g, '')" data-label="TaskLimit" />                                          
                                    <label class="TaskLimit-error" style="color: red; margin-top: 0.2rem; display: none"></label>                                  
                                </div>                   
                        </div>
                    </div>
                    <div class="modal-footer">                      
                        <button class="btn btn-primary me-3" id="save" onclick="return validateAndPostBack(event);">Save Details</button> 
                         <button type="button" data-bs-dismiss="modal" class="btn btn-danger light Clearbtn" id="Clearbtn">Cancel</button>
                    </div>
                       
                </div>
            </div>
        </div>
    
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js" integrity="sha512-v2CJ7UaYy4JwqLDIrZUI/4hqeoQieOmAZNXBeQyjo21dadnwR+8ZaIJVT8EE2iyI61OV8e6M8PP2/4hpQINQ/g==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="js/jquery-ui.min.js"></script>
    <script src="js/jquery.min.js"></script>
    <script src="js/GetCookie.js"></script>
    <script src="js/CustomeValidation.js"></script>
    <script src="js/Ajax/ManageEmployee.js"></script>
</asp:Content>
