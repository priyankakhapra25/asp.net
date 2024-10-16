<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageTaskForm.aspx.cs" Inherits="CRMSystem.ManageTaskForm" %>

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

                            <h5 class="title">Task Form List</h5>
                            <button class="btn btn-primary" type="button" data-bs-toggle="modal" data-bs-target="#exampleModalCenter" style="margin-right: 2rem"><i class="fa fa-plus"></i></button>
                        </div>
                        <div class="profile-form">
                            <div class="card-body">
                                <div class="row">

                                    <div class="table-responsive">
                                        <%--                                        <table id="example" class="display table">--%>
                                        <table id="example" class="display table">

                                            <thead>
                                                <tr>
                                                    <th>Action</th>
                                                    <th>Topic</th>
                                                    <th>Task</th>
                                                    <th>FromDate</th>
                                                    <th>ToDate</th>
                                                    <th>Role</th>
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
    <div class="modal fade" id="exampleModalCenter">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">

                    <h6 class="modal-title">Manage Task Form </h6>
                    <button type="button" class="btn-close Clearbtn" data-bs-dismiss="modal">
                    </button>
                </div>



                <div class="modal-body">
                    <div class="row">

                        <div class="col-6 my-2">
                            <label class="form-label">From<span style="color: red">*</span></label>
                            <input type="date" class="form-control validated" id="txtFrom" data-label="From" />
                            <label class="From-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div class="col-6 my-2">
                            <label class="form-label">To<span style="color: red">*</span></label>
                            <input type="date" class="form-control validated" id="txtTo" data-label="To" />
                            <label class="To-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div class="col-6 my-2">
                            <label class="form-label">Topic<span style="color: red">*</span></label>
                            <input type="text" class="form-control validated" id="txtTopic" data-label="Topic" />
                            <label class="Topic-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div class="col-6 my-2">
                            <label class="form-label">Role<span style="color: red">*</span></label>
                            <select class="form-control validated"  id="txtRole" data-label="Role"></select>
                            <label class="Role-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div  class="col-6 my-2   EmployeeDiv" style="display:none;" >
                            <label class="form-label">Employee<span style="color: red">*</span></label>
                            <select class="form-control validated" " id="txtEmployee" data-label="Employee"></select>
                            <label class="Employee-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div class="col-6 my-2">
                            <label class="form-label">Task<span style="color: red">*</span></label>
                            <textarea class="form-control validate" id="txtTask" rows="5" data-label="Task"></textarea>
                            <label class="Task-error" style="color: red; margin-top: 0.2rem; display: none"></label>
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

    <!-- Button to open modal -->

    <!-- Modal Structure -->






   

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js" integrity="sha512-v2CJ7UaYy4JwqLDIrZUI/4hqeoQieOmAZNXBeQyjo21dadnwR+8ZaIJVT8EE2iyI61OV8e6M8PP2/4hpQINQ/g==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>

    <script src="js/jquery-ui.min.js"></script>
    <script src="js/jquery.min.js"></script>
    <script src="js/GetCookie.js"></script>
    <script src="js/CustomeValidation.js"></script>
    <script src="js/Ajax/ManageTaskForm.js"></script>
</asp:Content>
