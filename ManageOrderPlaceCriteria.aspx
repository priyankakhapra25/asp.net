<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageOrderPlaceCriteria.aspx.cs" Inherits="CRMSystem.ManageOrderPlaceCriteria" %>
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

                    <div class="col-12">
                        <div class="col-12 ">

                            <div class="card profile-card card-bx m-b30">
                            <div class="card-header">
                                
                                <h5 class="title">Oder Place Criteria</h5>
                            </div>
                           <div class="mb-3 p-5">
                              <label for="exampleFormControlTextarea1" class="form-label">Order Place Maximum Difference</label>
                              <textarea class="form-control validate" id="exampleFormControlTextarea1" rows="7" ></textarea>
                           </div>
                            <button class="btn btn-primary me-3" id="save" onclick="return validateAndPostBack(event); ">Submit</button> 
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
    <script src="js/Ajax/ManageOrderPlaceCriteria.js"></script>

</asp:Content>
