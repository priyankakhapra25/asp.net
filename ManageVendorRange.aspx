<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageVendorRange.aspx.cs" Inherits="CRMSystem.ManageVendorRange" %>
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
                                
                                <h5 class="title"> Vendor Range List</h5>
                            </div>
                            

                <div class="row p-3">
                            <div class="col-6">
                               <div class="col-12 my-2">
                                    <label class="form-label">Select Range<span style="color: red">*</span></label>
                                      <select id="cbRange" class="form-control validated" data-label="range"> </select>
                                  
                                   <label class="range-error" style="color: red; margin-top: 0.2rem; display: none"></label>                                  
                                </div>
                            <div class="col-12 my-2">
                                    <label class="form-label">Rate<span style="color: red">*</span></label>
                                    <input type="text" class="form-control validated" id="txtrate" data-label="rate" oninput="this.value = this.value.replace(/[^0-9]/g, '')" />                                          
                                    <label class="rate-error" style="color: red; margin-top: 0.2rem; display: none"></label>                                  
                                </div>  
                                </div>
                            <div class="col-6">
                                <div class="basic-list-group" style="overflow:auto; max-height: 290px;" >
                                 <ul class="list-group" id="GroupList">
                                 </ul>
                              </div>
                            </div>
                                            <button class="btn btn-primary me-3 mt-3 " id="save" onclick="return validateAndPostBack(event);">Save Details</button> 
                         <%--<button  type="button" class="btn  btn-danger mt-2 light Clearbtn" id="Clearbtn">Cancel</button>--%>

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
    <script src="js/Ajax/ManageVendorRange.js"></script>

</asp:Content>

