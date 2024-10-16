<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="ManageOfferForm.aspx.cs" Inherits="CRMSystem.ManageOfferForm" %>
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
                                
                                <h5 class="title">Offer Form List</h5>
                            </div>
                           <div class="row m-2">

                                <div class="col-6 ">
                                <div class="col-12 my-2">
                                    <label> From Date </label>
                                    <input type="date" class="form-control  validated" id="txtFromDate" data-label="FromDate" /> 
                                    <label class="FromDate-error" style="color: red; margin-top: 0.2rem; display: none"></label>
                               
                                </div>
                               <div class="col-12 my-2">
                                    <label class="form-label">To Date</label>

                                    <input type="date" class="form-control validated" id="txtToDate" data-label="ToDate"  />                                          
                                    <label class="ToDate-error" style="color: red; margin-top: 0.2rem; display: none"></label>                                  
                                </div> 
                                 <div class="col-12 my-2">
                                    <label class="form-label">Amount<span style="color: red">*</span></label>

                                    <input type="text" class="form-control validated" id="txtAmount" data-label="Amount" oninput="this.value = this.value.replace(/[^0-9]/g, '')" />                                          
                                    <label class="Amount-error" style="color: red; margin-top: 0.2rem; display: none"></label>                                  
                                </div> 
                                <div class="col-12 my-2">
                                 <label class="form-label">Offer<span style="color: red">*</span></label>

                                   <textarea rows="6" class="form-control validated" id="txtOffer" data-label="Offer"></textarea>
                                    <label class="Offer-error" style="color: red; margin-top: 0.2rem; display: none"></label>                                  

                               </div>
                            </div>
                                 <div class="col-6 ">
                                <div class="basic-list-group" style="overflow:auto; max-height: 410px;" >
                                 <ul class="list-group" id="GroupList">
                                 </ul>
                              </div>
                            </div>
                              
                               
                            
                            
                           </div>   
                        </div>
                        <button class="btn btn-primary me-3" id="save" onclick="return validateAndPostBack(event);">Save Details</button> 
                    </div>
                                    
                
                       
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
   
    
                                                <!-- Button to open modal -->

<!-- Modal Structure -->


<div class="modal stock fade "  id="groupModal">
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
                                 <select id="category" class="form-control validated1" data-label="Category"> </select>
                                    <label class="Category-error" style="color: red; margin-top: 0.2rem; display: none"></label>                                  
                                </div>  
                                                           
                        </div>

      </div>
    
        <div class="container-fluid">
                <!-- row -->
                <div class="row">
                    <div class="col-xl-12 col-lg-12">
                        <div class="card profile-card card-bx m-b30">
                            
                                                  <div class="table-responsive">
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
           







    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js" integrity="sha512-v2CJ7UaYy4JwqLDIrZUI/4hqeoQieOmAZNXBeQyjo21dadnwR+8ZaIJVT8EE2iyI61OV8e6M8PP2/4hpQINQ/g==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="js/jquery-ui.min.js"></script>
    <script src="js/jquery.min.js"></script>
    <script src="js/GetCookie.js"></script>
    <script src="js/CustomeValidation.js"></script>
    <script src="js/Ajax/ManageOfferForm.js"></script>

</asp:Content>
