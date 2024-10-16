<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageAddVendor.aspx.cs" Inherits="CRMSystem.ManageAddVendor" %>
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

                            <h5 class="title">Add Vendor List</h5>
                            <button class="btn btn-primary" type="button" data-bs-toggle="modal" data-bs-target="#exampleModalCenter" style="margin-right: 2rem"><i class="fa fa-plus"></i></button>
                        </div>
                        <div class="profile-form">
                            <div class="card-body">
                                <div class="row">

                                    <div class="table-responsive">
                                        <table id="example" class="display table">

                                            <thead>
                                                <tr>
                                                    <th>Action</th>
                                                    <th>Vendor Name</th>
                                                    <th>Vendor Code </th>
                                                    <th>GST</th>
                                                    <th>Vendor Address</th>
                                                    <th>Contact Person</th>
                                                    <th>Contact Person Number</th>
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
    <div class="modal fade" id="exampleModalCenter">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">

                    <h6 class="modal-title">Manage Add Vendor</h6>
                    <button type="button" class="btn-close Clearbtn" data-bs-dismiss="modal">
                    </button>
                </div>



                <div class="modal-body">
                    <div class="row">
                        <div class="col-6 my-2">
                            <label>Vendor Name <span style="color: red">*</span></label>
                            <input type="text" class="form-control  validated" id="txtVendorName" data-label="VendorName" />
                            <label class="VendorName-error" style="color: red; margin-top: 0.2rem; display: none"></label>

                            <input type="hidden" id="txtCompanyID" data-label="Company ID" />

                        </div>
                        <div class="col-6 my-2">
                            <label class="form-label">Vendor Code<span style="color: red">*</span></label>

                            <input type="text" class="form-control validated" id="txtVendorCode" data-label="VendorCode" oninput="this.value = this.value.replace(/[^0-9]/g, '')" />
                            <label class="VendorCode-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div class="col-6 my-2">
                            <label class="form-label">Contact Person Name<span style="color: red">*</span></label>

                            <input type="text" class="form-control validated" id="txtContactPersonName" data-label="ContactPersonName" />
                            <label class="ContactPersonName-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div class="col-6 my-2">
                            <label class="form-label">Contact Person Contact<span style="color: red">*</span></label>

                            <input type="text" class="form-control validated" id="txtContactPersonContact" data-label="ContactPersonContact" />
                            <label class="ContactPersonContact-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div class="col-6 my-2">
                            <label class="form-label">Email<span style="color: red">*</span></label>

                            <input type="email" class="form-control validated" id="txtEmail" data-label="Email" />
                            <label class="Email-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div class="col-6 my-2">
                            <label class="form-label">PAN No<span style="color: red">*</span></label>

                            <input type="text" class="form-control validated" id="txtPAN" data-label="PAN" />
                            <label class="PAN-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div class="col-6 my-2">
                            <label class="form-label">Vendor Address<span style="color: red">*</span></label>

                            <input type="text" class="form-control validated" id="txtVendorAddress" data-label="VendorAddress" />
                            <label class="VendorAddress-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div class="col-6 my-2">
                            <label class="form-label">GST<span style="color: red">*</span></label>

                            <input type="text" class="form-control validated" id="txtGST" data-label="GST" />
                            <label class="GST-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div class="col-6 my-2">
                            <label class="form-label">Bank<span style="color: red">*</span></label>

                            <input type="text" class="form-control validated" id="txtBank" data-label="Bank" />
                            <label class="Bank-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div class="col-6 my-2">
                            <label class="form-label">Branch<span style="color: red">*</span></label>

                            <input type="text" class="form-control validated" id="txtBranch" data-label="Branch" />
                            <label class="Branch-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div class="col-6 my-2">
                            <label class="form-label">IFSC Code<span style="color: red">*</span></label>

                            <input type="text" class="form-control validated" id="txtIFSC" data-label="IFSC" />
                            <label class="IFSC-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div class="col-6 my-2">
                            <label class="form-label">A/C No<span style="color: red">*</span></label>

                            <input type="text" class="form-control validated" id="txtAC" data-label="AC" />
                            <label class="AC-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div class="col-6 my-2">
                            <label class="form-label">User Name<span style="color: red">*</span></label>

                            <input type="text" class="form-control validated" id="txtUsername" data-label="Username" />
                            <label class="Username-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div class="col-6 my-2">
                            <label class="form-label">Password<span style="color: red">*</span></label>

                            <input type="text" class="form-control validated" id="txtPassword" data-label="Password" />
                            <label class="Password-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                         <div class="col-6 my-2">
                            <label class="form-label">Security Deposit<span style="color: red">*</span></label>

                            <input type="text" class="form-control validated" id="txtSecurity" data-label="SecurityDeposit" />
                            <label class="SecurityDeposit-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                         <div class="col-6 my-2">
                            <label class="form-label">DOB<span style="color: red">*</span></label>

                            <input type="date" class="form-control validated" id="txtDOB" data-label="DOB" />
                            <label class="DOB-error" style="color: red; margin-top: 0.2rem; display: none"></label>
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


    <div class="modal stock fade " id="groupModal">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="groupModalLabel">Item with Distributor Stock</h5>

                    <button type="button" class="btn-close clear-btn" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <!-- Group List starts here -->
                    <div class="row">
                        <input type="hidden" id="hfCompanyID" value="0" />
                        <div class="col-6 my-2">
                            <label class="form-label">Dealer Name<span style="color: red">*</span></label>
                            <input type="text" class="form-control name validated1" id="txtName" data-label="Name" readonly="true" />
                            <label class="Name-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div class="col-6 my-2">
                            <label class="form-label">Category<span style="color: red">*</span></label>
                            <select id="category" class="form-control validated1" data-label="Category"></select>
                            <label class="Category-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>

                        <div class="table-responsive mt-4">
                            <table id="customtable" class="display table  vendortable">

                                <thead>
                                    <tr>
                                        <th>Sno</th>
                                        <th>Item Name</th>
                                        <th>Vendor Name</th>
                                        <th>Stock</th>
                                        <th>Add Stock</th>


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
                
    <%-- modal Manage dealer district relation--%> 

     <div class="modal stock fade " id="groupModalDistrict">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="groupModalDistrictLabel">Dealer District Relation</h5>

                    <button type="button" class="btn-close clear-btn" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <!-- Group List starts here -->
                    <div class="row   ">
                        <div class="col-6 my-2">
                            <label class="form-label">Dealer<span style="color: red">*</span></label>
                            <input type="text"  data-id="" class="form-control ignore_that name validated1" id="txtDealer" data-label="Name" readonly="true" />

                            <input type="hidden" class="ignore_that"  id="txtDealerid"  />
                            <label class="Name-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div class="col-6 my-2">
                            <label class="form-label">State<span style="color: red">*</span></label>
                            <select id="state" class="form-control validated1" data-label="State"></select>
                            <label class="State-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div class="col-6 my-2">
                            <label class="form-label">District<span style="color: red">*</span></label>
                            <select id="district"  class="form-control validated1" data-label="District"></select>
                                                    <input type="hidden" id="hfDistrictID" value="0" />

                            <label class="District-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                         <div class="col-6 my-2">
                            <label class="form-label">From (in no. of days)<span style="color: red">*</span></label>
                            <input type="text" class="form-control name validated1" id="from" data-label="From"  />
                            <label class="From-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                         <div class="col-6 my-2">
                            <label class="form-label">To (in no. of days)<span style="color: red">*</span></label>
                            <input type="text" class="form-control name validated1" id="to" data-label="To"  />
                            <label class="To-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div class="col-6 my-4 pt-3 form-group">
                    <button class="btn btn-primary me-3" id="save1" onclick="return validateAndPostBack(event);">Save Details</button>
<%--                            <input  type="hidden" id="txtdistributerdelerID" value="0"/>--%>
                    <button type="button" data-bs-dismiss="modal" class="btn btn-danger light Clearbtn" id="Clearbtn1">Cancel</button>
            </div>
                        <div class="table-responsive mt-4">
                            <table id="example1" class="display table  ">

                                <thead>
                                    <tr>
                                        <th>Action</th>
                                        <th>Sno</th>
                                        <th>District Name</th>
                                        <th>From Days</th>
                                        <th>To Days</th>
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
         
     <%-- modal Manage vendor range comission--%> 

     <div class="modal stock fade " id="groupModalRange">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="groupModalRangeLabel">Vendor Range Comission</h5>

                    <button type="button" class="btn-close clear-btn" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <!-- Group List starts here -->
                    <div class="row   ">
                        <div class="col-6 my-2">
                         <input  type="hidden" class="ignore_that" id="txtvendorid" value="0"/>

                            <label class="form-label">Vendor Name<span style="color: red">*</span></label>
                         <input type="text"  class="form-control  validated1 ignore_that" id="txtvendor" data-label="Vendor" readonly="true" />
                            <label class="Vendor-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div class="col-6 my-2">
                            <label class="form-label">Range<span style="color: red">*</span></label>
                            <select id="txtrange" class="form-control validated1" data-label="Range"></select>
                            <label class="Range-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        <div class="col-6 my-2">
                            <label class="form-label">Amount<span style="color: red">*</span></label>
                         <input type="text"  class="form-control  validated1" id="txtamount" data-label="Amount"  />
                            <label class="Amount-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                        </div>
                        
                        <div class="col-6 my-4 pt-3 form-group">
                    <button class="btn btn-primary me-3" id="save2" onclick="return validateAndPostBack(event);">Save Details</button>
                            <input  type="hidden" id="txtvendorrangeID" value="0"/>
                    <button type="button" data-bs-dismiss="modal" class="btn btn-danger light Clearbtn" id="Clearbtn2">Cancel</button>
            </div>
                        <div class="table-responsive mt-4">
                            <table  class="display table vendorRange ">

                                <thead>
                                    <tr>
                                        <th>Action</th>
                                        <th>Vendor</th>
                                        <th>Range</th>
                                        <th>Amount</th>
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
    
 




                <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js" integrity="sha512-v2CJ7UaYy4JwqLDIrZUI/4hqeoQieOmAZNXBeQyjo21dadnwR+8ZaIJVT8EE2iyI61OV8e6M8PP2/4hpQINQ/g==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
                <script src="js/jquery-ui.min.js"></script>
                <script src="js/jquery.min.js"></script>
                <script src="js/GetCookie.js"></script>
                <script src="js/CustomeValidation.js"></script>
                <script src="js/Ajax/ManageAddVendor.js"></script>

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
       
</asp:Content>
