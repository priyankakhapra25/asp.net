using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.UI;
using System.Data;
using System.IO;

namespace CRMSystem
{
    /// <summary>
    /// Summary description for Coding
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Coding : System.Web.Services.WebService
    {

        //================== Login Code ====================//

        [WebMethod]
        public string Login(LoginData data)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();

            var objUser = db.tb_Employee.FirstOrDefault(s => s.UserName == data.UserName && s.Password == data.Password);
            if (objUser != null)
            {

                EncryptString objEncrp = new EncryptString();
                var UserID = objEncrp.EncryptStringAES(objUser.ID.ToString());
                HttpCookie cookie = new HttpCookie("aiess");
                cookie.Value = UserID;
                cookie.Expires = DateTime.Now.AddMonths(1);
                HttpContext.Current.Response.Cookies.Add(cookie);
                //var BranchID = objEncrp.EncryptStringAES(objUser.BranchID != null ? objUser.BranchID.ToString() : "0");
                //HttpCookie cookie2 = new HttpCookie("aiessB");
                //cookie2.Value = BranchID;
                //cookie2.Expires = DateTime.Now.AddMonths(1);
                //HttpContext.Current.Response.Cookies.Add(cookie2);


                //HttpCookie layout = new HttpCookie("layout");
                //layout.Value = "horizontal";
                //HttpContext.Current.Response.Cookies.Add(layout);

                return "Valid";
            }
            return "Invalid";
        }
        public class LoginData
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        //================== Login Code End ====================//

        //================== Get Cookie Value ====================//

        [WebMethod]
        public string GetCookie()
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            EncryptString encp = new EncryptString();
            HttpCookie cookie = HttpContext.Current.Request.Cookies["aiess"];
            HttpCookie cookie2 = HttpContext.Current.Request.Cookies["aiessB"];
            string DecrptUserID = "0", DecrptBranchID = "0";
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                DecrptUserID = encp.DecryptStringAES(cookie.Value);
            }
            if (cookie2 != null && !string.IsNullOrEmpty(cookie2.Value))
            {
                DecrptBranchID = encp.DecryptStringAES(cookie2.Value);
            }
            int uID = Convert.ToInt32(DecrptUserID);
            var objuser = db.tb_Employee.FirstOrDefault(s => s.ID == uID);
            CookieData data = new CookieData
            {
                UserID = DecrptUserID,
                UserName = objuser.Name,
                BranchID = DecrptBranchID
            };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string json = serial.Serialize(data);
            return json;
        }

        public class CookieData
        {
            public string UserID { get; set; }
            public string UserName { get; set; }
            public string BranchID { get; set; }
        }

        //================== Get Cookie Value End ====================//


        //================== Manage State ====================//

        /// <summary>
        /// Bind Code
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string BindState()
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = from a in db.tb_State
                  where a.Status == "Active"
                  orderby a.ID descending
                  select new
                  {
                      a.ID,
                      a.Name,
                      a.Status
                  };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string jason = serial.Serialize(obj);
            return jason;
        }

        /// <summary>
        /// Insert Code
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [WebMethod]
        public string InsertState(StateData data)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_State obj = new tb_State();
            int ID = Convert.ToInt32((data.StateID != null || data.StateID != "0") ? data.StateID.ToString() : "0");
            var objCheck = db.tb_State.FirstOrDefault(u => u.Name.ToLower() == data.State.ToLower() && u.ID != ID && u.Status == "Active");
            if (objCheck != null)
            {
                return "Exits";
            }
            if (ID == 0)
            {
                obj.CreatedBy = Convert.ToInt32(data.UserID);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedTime = DateTime.Now.TimeOfDay;
                db.tb_State.Add(obj);
            }
            else
            {
                obj = db.tb_State.FirstOrDefault(s => s.ID == ID);
                obj.ModifyBy = Convert.ToInt32(data.UserID);
                obj.ModifyDate = DateTime.Now;
                obj.ModifyTime = DateTime.Now.TimeOfDay;
            }

            obj.Status = "Active";
            obj.Name = data.State;
            db.SaveChanges();
            if (ID == 0)
            {
                return "Inserted";
            }
            else
            {
                return "Updated";
            }
        }
        public class StateData
        {
            public string StateID { get; set; }
            public string State { get; set; }
            public string UserID { get; set; }

        }


        [WebMethod]
        public string StateAction(string actionLabel, int editID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            var obj = db.tb_State.FirstOrDefault(s => s.ID == editID);
            if (actionLabel == "Edit")
            {
                if (obj != null)
                {
                    StateData data = new StateData
                    {
                        StateID = obj.ID.ToString(),
                        State = obj.Name,
                    };

                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    string json = serial.Serialize(data);
                    return json;
                }
            }
            else if (actionLabel == "Delete")
            {
                if (obj != null)
                {
                    if (obj.Status == "Active")
                    {
                        obj.Status = "InActive";
                        db.SaveChanges();
                    }
                    else
                    {
                        obj.Status = "Active";
                        db.SaveChanges();
                    }
                }
                return "Deleted Successfully";
            }

            return string.Empty;
        }

        //================== Manage State End====================//


        //====================  Manage District Start       =========================//

        // bind district
        [WebMethod]
        public string BindDistrict()
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = from a in db.tb_District
                  where a.Status == "Active"
                  orderby a.ID descending
                  select new
                  {
                      a.ID,
                      a.tb_State.Name,
                      a.DistrictName,
                      a.Status
                  };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string jason = serial.Serialize(obj);
            return jason;
        }


        //insert District
        [WebMethod]
        public string InsertDistrict(DistrictData data)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_District obj = new tb_District();
            int StateID = Convert.ToInt32(data.StateID);
            int ID = Convert.ToInt32((data.ID != null || data.ID != "0") ? data.ID.ToString() : "0");
            var objCheck = db.tb_District.FirstOrDefault(u => u.DistrictName.ToLower() == data.DistrictName.ToLower() && u.StateID == StateID && u.ID != ID && u.Status == "Active");
            if (objCheck != null)
            {
                return "Exits";
            }
            if (ID == 0)
            {
                obj.CreatedBy = Convert.ToInt32(data.UserID);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedTime = DateTime.Now.TimeOfDay;
                db.tb_District.Add(obj);
            }
            else
            {
                obj = db.tb_District.FirstOrDefault(s => s.ID == ID);
                obj.ModifyBy = Convert.ToInt32(data.UserID);
                obj.ModifyDate = DateTime.Now;
                obj.ModifyTime = DateTime.Now.TimeOfDay;
            }

            obj.Status = "Active";
            obj.StateID = Convert.ToInt32(data.StateID);
            obj.DistrictName = data.DistrictName;
            db.SaveChanges();
            if (ID == 0)
            {
                return "Inserted";
            }
            else
            {
                return "Updated";
            }
        }
        public class DistrictData
        {

            public string ID { get; set; }
            public string StateID { get; set; }
            public string State { get; set; }
            public string DistrictName { get; set; }
            public string UserID { get; set; }

        }

        /// <summary>
        /// Edit Delete Code
        /// </summary>
        /// <param name="actionLabel"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        [WebMethod]
        public string DistrictAction(string actionLabel, int editID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            var obj = db.tb_District.FirstOrDefault(s => s.ID == editID);
            // var obj = db.tb_District.Where(s => s.ID == editID);
            if (actionLabel == "Edit")
            {
                if (obj != null)
                {
                    DistrictData data = new DistrictData
                    {
                        ID = obj.ID.ToString(),
                        StateID = obj.StateID.ToString(),
                        State = obj.tb_State.Name,
                        DistrictName = obj.DistrictName,
                    };

                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    string json = serial.Serialize(data);
                    return json;
                }
            }
            else if (actionLabel == "Delete")
            {
                if (obj != null)
                {
                    if (obj.Status == "Active")
                    {
                        obj.Status = "InActive";
                        db.SaveChanges();
                    }
                    else
                    {
                        obj.Status = "Active";
                        db.SaveChanges();
                    }
                }
                return "Deleted Successfully";
            }

            return string.Empty;
        }

        //================== Manage District End====================//

        //===================   Manage Role Start  =======================
        [WebMethod]
        public string BindRole()
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = from a in db.tb_Role
                  where a.Status == "Active"
                  orderby a.ID descending
                  select new
                  {
                      a.ID,
                      Role = a.Name,
                      a.Status
                  };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string jason = serial.Serialize(obj);
            return jason;
        }

        // insert role
        [WebMethod]
        public string InsertRole(RoleData data)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_Role obj = new tb_Role();
            int ID = Convert.ToInt32((data.RoleID != null || data.RoleID != "0") ? data.RoleID.ToString() : "0");
            var objCheck = db.tb_Role.FirstOrDefault(u => u.Name.ToLower() == data.Role.ToLower() && u.ID != ID && u.Status == "Active");
            if (objCheck != null)
            {
                return "Exits";
            }
            if (ID == 0)
            {
                obj.CreatedBy = Convert.ToInt32(data.UserID);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedTime = DateTime.Now.TimeOfDay;
                db.tb_Role.Add(obj);
            }
            else
            {
                obj = db.tb_Role.FirstOrDefault(s => s.ID == ID);
                obj.ModifyBy = Convert.ToInt32(data.UserID);
                obj.ModifyDate = DateTime.Now;
                obj.ModifyTime = DateTime.Now.TimeOfDay;
            }

            obj.Status = "Active";
            obj.Name = data.Role;
            db.SaveChanges();
            if (ID == 0)
            {
                return "Inserted";
            }
            else
            {
                return "Updated";
            }
        }
        public class RoleData
        {
            public string RoleID { get; set; }
            public string Role { get; set; }
            public string UserID { get; set; }

        }

        // role action
        [WebMethod]
        public string RoleAction(string actionLabel, int editID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            var obj = db.tb_Role.FirstOrDefault(s => s.ID == editID);
            if (actionLabel == "Edit")
            {
                if (obj != null)
                {
                    RoleData data = new RoleData
                    {
                        RoleID = obj.ID.ToString(),
                        Role = obj.Name,
                    };

                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    string json = serial.Serialize(data);
                    return json;
                }
            }
            else if (actionLabel == "Delete")
            {
                if (obj != null)
                {
                    if (obj.Status == "Active")
                    {
                        obj.Status = "InActive";
                        db.SaveChanges();
                    }
                    else
                    {
                        obj.Status = "Active";
                        db.SaveChanges();
                    }
                }
                return "Deleted Successfully";
            }

            return string.Empty;
        }
        //===================   Manage Role End  =======================


        //=================== Manage occupation start ==================

        //bind occupation

        [WebMethod]
        public string BindOccupation()
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = from a in db.tb_Occupation
                  where a.Status == "Active"
                  orderby a.ID descending
                  select new
                  {
                      a.ID,
                      a.Name,
                      a.Status
                  };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string jason = serial.Serialize(obj);
            return jason;
        }
        // insert role

        [WebMethod]
        public string InsertOccupation(OccupationData data)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_Occupation obj = new tb_Occupation();
            int ID = Convert.ToInt32((data.ID != null || data.ID != "0") ? data.ID.ToString() : "0");
            var objCheck = db.tb_Occupation.FirstOrDefault(u => u.Name.ToLower() == data.Name.ToLower() && u.ID != ID && u.Status == "Active");
            if (objCheck != null)
            {
                return "Exits";
            }
            if (ID == 0)
            {
                obj.CreatedBy = Convert.ToInt32(data.UserID);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedTime = DateTime.Now.TimeOfDay;
                db.tb_Occupation.Add(obj);
            }
            else
            {
                obj = db.tb_Occupation.FirstOrDefault(s => s.ID == ID);
                obj.ModifyBy = Convert.ToInt32(data.UserID);
                obj.ModifyDate = DateTime.Now;
                obj.ModifyTime = DateTime.Now.TimeOfDay;
            }

            obj.Status = "Active";
            obj.Name = data.Name;
            db.SaveChanges();
            if (ID == 0)
            {
                return "Inserted";
            }
            else
            {
                return "Updated";
            }
        }
        public class OccupationData
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string UserID { get; set; }

        }

        // role action
        [WebMethod]
        public string OccupationAction(string actionLabel, int editID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            var obj = db.tb_Occupation.FirstOrDefault(s => s.ID == editID);
            if (actionLabel == "Edit")
            {
                if (obj != null)
                {
                    OccupationData data = new OccupationData
                    {
                        ID = obj.ID.ToString(),
                        Name = obj.Name,
                    };

                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    string json = serial.Serialize(data);
                    return json;
                }
            }
            else if (actionLabel == "Delete")
            {
                if (obj != null)
                {
                    if (obj.Status == "Active")
                    {
                        obj.Status = "InActive";
                        db.SaveChanges();
                    }
                    else
                    {
                        obj.Status = "Active";
                        db.SaveChanges();
                    }
                }
                return "Deleted Successfully";
            }

            return string.Empty;
        }

        //=================== Manage occupation end ==================

        //=================== Manage Category start ==================

        //bind Category

        [WebMethod]
        public string BindCategory()
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = from a in db.tb_Category
                  where a.Status == "Active"
                  orderby a.ID descending
                  select new
                  {
                      a.ID,
                      a.CategoryName,
                      a.Status
                  };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string jason = serial.Serialize(obj);
            return jason;
        }
        // insert role

        [WebMethod]
        public string InsertCategory(CategoryData data)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_Category obj = new tb_Category();
            int ID = Convert.ToInt32((data.ID != null || data.ID != "0") ? data.ID.ToString() : "0");
            var objCheck = db.tb_Category.FirstOrDefault(u => u.CategoryName.ToLower() == data.CategoryName.ToLower() && u.ID != ID && u.Status == "Active");
            if (objCheck != null)
            {
                return "Exits";
            }
            if (ID == 0)
            {
                obj.CreatedBy = Convert.ToInt32(data.UserID);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedTime = DateTime.Now.TimeOfDay;
                db.tb_Category.Add(obj);
            }
            else
            {
                obj = db.tb_Category.FirstOrDefault(s => s.ID == ID);
                obj.ModifyBy = Convert.ToInt32(data.UserID);
                obj.ModifyDate = DateTime.Now;
                obj.ModifyTime = DateTime.Now.TimeOfDay;
            }

            obj.Status = "Active";
            obj.CategoryName = data.CategoryName;
            db.SaveChanges();
            if (ID == 0)
            {
                return "Inserted";
            }
            else
            {
                return "Updated";
            }
        }
        public class CategoryData
        {
            public string ID { get; set; }
            public string CategoryName { get; set; }
            public string UserID { get; set; }

        }

        // role action
        [WebMethod]
        public string CategoryAction(string actionLabel, int editID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            var obj = db.tb_Category.FirstOrDefault(s => s.ID == editID);
            if (actionLabel == "Edit")
            {
                if (obj != null)
                {
                    CategoryData data = new CategoryData
                    {
                        ID = obj.ID.ToString(),
                        CategoryName = obj.CategoryName,
                    };

                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    string json = serial.Serialize(data);
                    return json;
                }
            }
            else if (actionLabel == "Delete")
            {
                if (obj != null)
                {
                    if (obj.Status == "Active")
                    {
                        obj.Status = "InActive";
                        db.SaveChanges();
                    }
                    else
                    {
                        obj.Status = "Active";
                        db.SaveChanges();
                    }
                }
                return "Deleted Successfully";
            }

            return string.Empty;
        }

        //=================== Manage Category end ==================


        //=================== Manage Sub status start ==================

        //bind sub status

        [WebMethod]
        public string BindSubStatus()
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = from a in db.tb_SubStatus
                  where a.Status == "Active"
                  orderby a.ID descending
                  select new
                  {
                      a.ID,
                      a.SubStatusName,
                      a.Status
                  };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string jason = serial.Serialize(obj);
            return jason;
        }
        // insert sub status

        [WebMethod]
        public string InsertSubStatus(SubStatusData data)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_SubStatus obj = new tb_SubStatus();
            int ID = Convert.ToInt32((data.ID != null || data.ID != "0") ? data.ID.ToString() : "0");
            var objCheck = db.tb_SubStatus.FirstOrDefault(u => u.SubStatusName.ToLower() == data.SubStatusName.ToLower() && u.ID != ID && u.Status == "Active");
            if (objCheck != null)
            {
                return "Exits";
            }
            if (ID == 0)
            {
                obj.CreatedBy = Convert.ToInt32(data.UserID);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedTime = DateTime.Now.TimeOfDay;
                db.tb_SubStatus.Add(obj);
            }
            else
            {
                obj = db.tb_SubStatus.FirstOrDefault(s => s.ID == ID);
                obj.ModifyBy = Convert.ToInt32(data.UserID);
                obj.ModifyDate = DateTime.Now;
                obj.ModifyTime = DateTime.Now.TimeOfDay;
            }

            obj.Status = "Active";
            obj.SubStatusName = data.SubStatusName;
            db.SaveChanges();
            if (ID == 0)
            {
                return "Inserted";
            }
            else
            {
                return "Updated";
            }
        }
        public class SubStatusData
        {
            public string ID { get; set; }
            public string SubStatusName { get; set; }
            public string UserID { get; set; }

        }

        // sub status action
        [WebMethod]
        public string SubStatusAction(string actionLabel, int editID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            var obj = db.tb_SubStatus.FirstOrDefault(s => s.ID == editID);
            if (actionLabel == "Edit")
            {
                if (obj != null)
                {
                    SubStatusData data = new SubStatusData
                    {
                        ID = obj.ID.ToString(),
                        SubStatusName = obj.SubStatusName,
                    };

                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    string json = serial.Serialize(data);
                    return json;
                }
            }
            else if (actionLabel == "Delete")
            {
                if (obj != null)
                {
                    if (obj.Status == "Active")
                    {
                        obj.Status = "InActive";
                        db.SaveChanges();
                    }
                    else
                    {
                        obj.Status = "Active";
                        db.SaveChanges();
                    }
                }
                return "Deleted Successfully";
            }

            return string.Empty;
        }

        //=================== Manage occupation end ==================


        //=================== Manage Client Type start ==================

        //bind client

        [WebMethod]
        public string BindClientType()
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = from a in db.tb_ClientType
                  where a.Status == "Active"
                  orderby a.ID descending
                  select new
                  {
                      a.ID,
                      a.ClientType,
                      a.Status
                  };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string jason = serial.Serialize(obj);
            return jason;
        }
        // insert client

        [WebMethod]
        public string InsertClientType(ClientTypeData data)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_ClientType obj = new tb_ClientType();
            int ID = Convert.ToInt32((data.ID != null || data.ID != "0") ? data.ID.ToString() : "0");
            var objCheck = db.tb_ClientType.FirstOrDefault(u => u.ClientType.ToLower() == data.ClientType.ToLower() && u.ID != ID && u.Status == "Active");
            if (objCheck != null)
            {
                return "Exits";
            }
            if (ID == 0)
            {
                obj.CreatedBy = Convert.ToInt32(data.UserID);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedTime = DateTime.Now.TimeOfDay;
                db.tb_ClientType.Add(obj);
            }
            else
            {
                obj = db.tb_ClientType.FirstOrDefault(s => s.ID == ID);
                obj.ModifyBy = Convert.ToInt32(data.UserID);
                obj.ModifyDate = DateTime.Now;
                obj.ModifyTime = DateTime.Now.TimeOfDay;
            }

            obj.Status = "Active";
            obj.ClientType = data.ClientType;
            db.SaveChanges();
            if (ID == 0)
            {
                return "Inserted";
            }
            else
            {
                return "Updated";
            }
        }
        public class ClientTypeData
        {
            public string ID { get; set; }
            public string ClientType { get; set; }
            public string UserID { get; set; }

        }

        // client action
        [WebMethod]
        public string ClientTypeAction(string actionLabel, int editID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            var obj = db.tb_ClientType.FirstOrDefault(s => s.ID == editID);
            if (actionLabel == "Edit")
            {
                if (obj != null)
                {
                    ClientTypeData data = new ClientTypeData
                    {
                        ID = obj.ID.ToString(),
                        ClientType = obj.ClientType,
                    };

                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    string json = serial.Serialize(data);
                    return json;
                }
            }
            else if (actionLabel == "Delete")
            {
                if (obj != null)
                {
                    if (obj.Status == "Active")
                    {
                        obj.Status = "InActive";
                        db.SaveChanges();
                    }
                    else
                    {
                        obj.Status = "Active";
                        db.SaveChanges();
                    }
                }
                return "Deleted Successfully";
            }

            return string.Empty;
        }

        //=================== Manage client end ==================

        //=================== Manage Relation start ==================

        //bind Relation

        [WebMethod]
        public string BindRelation()
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = from a in db.tb_Relation
                  where a.Status == "Active"
                  orderby a.ID descending
                  select new
                  {
                      a.ID,
                      a.RelationName,
                      a.Status
                  };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string jason = serial.Serialize(obj);
            return jason;
        }
        // insert client

        [WebMethod]
        public string InsertRelation(RelationData data)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_Relation obj = new tb_Relation();
            int ID = Convert.ToInt32((data.ID != null || data.ID != "0") ? data.ID.ToString() : "0");
            var objCheck = db.tb_Relation.FirstOrDefault(u => u.RelationName.ToLower() == data.RelationName.ToLower() && u.ID != ID && u.Status == "Active");
            if (objCheck != null)
            {
                return "Exits";
            }
            if (ID == 0)
            {
                obj.CreatedBy = Convert.ToInt32(data.UserID);
                obj.CreatedDate = DateTime.Now;
                obj.CretaedTime = DateTime.Now.TimeOfDay;
                db.tb_Relation.Add(obj);
            }
            else
            {
                obj = db.tb_Relation.FirstOrDefault(s => s.ID == ID);
                obj.ModifyBy = Convert.ToInt32(data.UserID);
                obj.ModifyDate = DateTime.Now;
                obj.ModifyTime = DateTime.Now.TimeOfDay;
            }

            obj.Status = "Active";
            obj.RelationName = data.RelationName;
            db.SaveChanges();
            if (ID == 0)
            {
                return "Inserted";
            }
            else
            {
                return "Updated";
            }
        }
        public class RelationData
        {
            public string ID { get; set; }
            public string RelationName { get; set; }
            public string UserID { get; set; }

        }

        // Relation action
        [WebMethod]
        public string RelationAction(string actionLabel, int editID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            var obj = db.tb_Relation.FirstOrDefault(s => s.ID == editID);
            if (actionLabel == "Edit")
            {
                if (obj != null)
                {
                    RelationData data = new RelationData
                    {
                        ID = obj.ID.ToString(),
                        RelationName = obj.RelationName,
                    };

                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    string json = serial.Serialize(data);
                    return json;
                }
            }
            else if (actionLabel == "Delete")
            {
                if (obj != null)
                {
                    if (obj.Status == "Active")
                    {
                        obj.Status = "InActive";
                        db.SaveChanges();
                    }
                    else
                    {
                        obj.Status = "Active";
                        db.SaveChanges();
                    }
                }
                return "Deleted Successfully";
            }

            return string.Empty;
        }

        //=================== Manage Relation end ==================

        //=================== Manage Message header start==================

        //bind Message header

        [WebMethod]
        public string BindMessageHeader()
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            var obj = db.tb_MessageHeader.FirstOrDefault();
            if (obj != null)
            {
                return obj.Message;
            }
            return string.Empty;
        }

        [WebMethod]
        public string InsertMessageHeader(string Message, int UserID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_MessageHeader obj = new tb_MessageHeader();

            // Update existing record
            obj = db.tb_MessageHeader.FirstOrDefault();
            if (obj != null)
            {
                obj.Message = Message;
                obj.ModifyBy = UserID;
                obj.ModifyDate = DateTime.Now;
                obj.ModifyTime = DateTime.Now.TimeOfDay;
                db.SaveChanges();
            }
            else
            {
                db.tb_MessageHeader.Add(obj);
                obj.Message = Message;
                obj.CretaedBY = UserID;
                obj.CreatedDate = DateTime.Now.Date;
                obj.CreatedTime = DateTime.Now.TimeOfDay;
                obj.Status = "Active";
                db.SaveChanges();
                return "Inserted";
            }
            return "Result Updated";
        }
        //=================== Manage Message header end  ==================


        //====================  Manage main master state Start  =========================//

        //bind Relation

        [WebMethod]
        public string BindStateMain()
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = from a in db.tb_StateMain
                  where a.Status == "Active"
                  orderby a.ID descending
                  select new
                  {
                      a.ID,
                      a.State,
                      a.DistrictName,
                      a.PostOfficeName,
                      a.Pincode,
                      a.Status
                  };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string jason = serial.Serialize(obj);
            return jason;
        }
        // insert client

        [WebMethod]
        public string InsertStateMain(StateMainData data)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_StateMain obj = new tb_StateMain();
            int ID = Convert.ToInt32((data.ID != null || data.ID != "0") ? data.ID.ToString() : "0");
            var objCheck = db.tb_StateMain.FirstOrDefault(u => u.State.ToLower() == data.State.ToLower() && u.DistrictName.ToLower() == data.District.ToLower() && u.PostOfficeName.ToLower() == data.PostOffice.ToLower() && u.Pincode.ToLower() == data.Pincode.ToLower() && u.ID != ID && u.Status == "Active");
            if (objCheck != null)
            {
                return "Exits";
            }
            if (ID == 0)
            {
                obj.CreatedBy = Convert.ToInt32(data.UserID);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedTime = DateTime.Now.TimeOfDay;
                db.tb_StateMain.Add(obj);
            }
            else
            {
                obj = db.tb_StateMain.FirstOrDefault(s => s.ID == ID);
                obj.ModfyBy = Convert.ToInt32(data.UserID);
                obj.MdifyDate = DateTime.Now;
                obj.ModifyTime = DateTime.Now.TimeOfDay;
            }

            obj.Status = "Active";
            obj.State = data.State;
            obj.DistrictName = data.District;
            obj.PostOfficeName = data.PostOffice;
            obj.Pincode = data.Pincode;

            db.SaveChanges();
            if (ID == 0)
            {
                return "Inserted";
            }
            else
            {
                return "Updated";
            }
        }
        public class StateMainData
        {
            public string ID { get; set; }
            public string State { get; set; }
            public string District { get; set; }
            public string PostOffice { get; set; }
            public string Pincode { get; set; }
            public string UserID { get; set; }

        }

        // Relation action
        [WebMethod]
        public string StateMainAction(string actionLabel, int editID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            var obj = db.tb_StateMain.FirstOrDefault(s => s.ID == editID);
            if (actionLabel == "Edit")
            {
                if (obj != null)
                {
                    StateMainData data = new StateMainData
                    {
                        ID = obj.ID.ToString(),
                        State = obj.State,
                        District = obj.DistrictName,
                        PostOffice = obj.PostOfficeName,
                        Pincode = obj.Pincode
                    };

                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    string json = serial.Serialize(data);
                    return json;
                }
            }
            else if (actionLabel == "Delete")
            {
                if (obj != null)
                {
                    if (obj.Status == "Active")
                    {
                        obj.Status = "InActive";
                        db.SaveChanges();
                    }
                    else
                    {
                        obj.Status = "Active";
                        db.SaveChanges();
                    }
                }
                return "Deleted Successfully";
            }

            return string.Empty;
        }
        //================== Manage State End====================//


        //====================  Manage client status Start  =========================//

        //bind status

        [WebMethod]
        public string BindClientStatus()
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = from a in db.tb_Status
                  where a.Status == "Active"
                  orderby a.ID descending
                  select new
                  {
                      a.ID,
                      a.Name,
                      a.Status,
                      a.BGColor

                  };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string jason = serial.Serialize(obj);
            return jason;
        }
        // insert client

        [WebMethod]
        public string InsertClientStatus(ClientStatusData data)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_Status obj = new tb_Status();
            int ID = Convert.ToInt32((data.ID != null || data.ID != "0") ? data.ID.ToString() : "0");
            var objCheck = db.tb_Status.FirstOrDefault(u => u.Name.ToLower() == data.Name.ToLower() && u.BGColor.ToLower() == data.Color.ToLower() && u.ID != ID && u.Status == "Active");
            if (objCheck != null)
            {
                return "Exits";
            }
            if (ID == 0)
            {
                obj.CreatedBy = Convert.ToInt32(data.UserID);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedTime = DateTime.Now.TimeOfDay;
                db.tb_Status.Add(obj);
            }
            else
            {
                obj = db.tb_Status.FirstOrDefault(s => s.ID == ID);
                obj.ModifyBy = Convert.ToInt32(data.UserID);
                obj.ModifyDate = DateTime.Now;
                obj.ModifyTime = DateTime.Now.TimeOfDay;
            }

            obj.Status = "Active";
            obj.Name = data.Name;
            obj.BGColor = data.Color;


            db.SaveChanges();
            if (ID == 0)
            {
                return "Inserted";
            }
            else
            {
                return "Updated";
            }
        }
        public class ClientStatusData
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Color { get; set; }
            public string UserID { get; set; }

        }

        // Relation action
        [WebMethod]
        public string ClientStatusAction(string actionLabel, int editID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            var obj = db.tb_Status.FirstOrDefault(s => s.ID == editID);
            if (actionLabel == "Edit")
            {
                if (obj != null)
                {
                    ClientStatusData data = new ClientStatusData
                    {
                        ID = obj.ID.ToString(),
                        Name = obj.Name,
                        Color = obj.BGColor

                    };

                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    string json = serial.Serialize(data);
                    return json;
                }
            }
            else if (actionLabel == "Delete")
            {
                if (obj != null)
                {
                    if (obj.Status == "Active")
                    {
                        obj.Status = "InActive";
                        db.SaveChanges();
                    }
                    else
                    {
                        obj.Status = "Active";
                        db.SaveChanges();
                    }
                }
                return "Deleted Successfully";
            }

            return string.Empty;
        }




        [WebMethod]
        public string InsertSubstatusRelation(SubStatusRelationData[] data, string StatusID)
        {
            int statusID = Convert.ToInt32(StatusID);
            PragticrmDBEntities db = new PragticrmDBEntities();
            var objList = from u in db.tb_RelationStatusSubStatus
                          where u.Status == "Active" && u.StatusID == statusID
                          select new
                          {
                              u.ID,
                              u.SubStatusID,
                              u.StatusID
                          };
            foreach (var item in objList.ToList())
            {
                tb_RelationStatusSubStatus objrel = db.tb_RelationStatusSubStatus.FirstOrDefault(a => a.StatusID == statusID && a.SubStatusID == item.SubStatusID);
                objrel.Status = "Inactive";
                db.SaveChanges();
            }

            foreach (SubStatusRelationData item in data)
            {
                tb_RelationStatusSubStatus obj = new tb_RelationStatusSubStatus();
                // int statusid = Convert.ToInt32(item.StatusID);
                int SubStatusID = Convert.ToInt32(item.SubStatusID);
                var objCheck = db.tb_RelationStatusSubStatus.FirstOrDefault(u => u.StatusID == statusID && u.SubStatusID == SubStatusID);
                if (objCheck != null)
                {
                    obj = db.tb_RelationStatusSubStatus.FirstOrDefault(s => s.ID == objCheck.ID);
                    obj.ModifyBy = Convert.ToInt32(item.UserID);
                    obj.ModifyDate = DateTime.Now;
                    obj.ModifyTime = DateTime.Now.TimeOfDay;
                }
                else
                {
                    obj.CreatedBy = Convert.ToInt32(item.UserID);
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedTime = DateTime.Now.TimeOfDay;
                    db.tb_RelationStatusSubStatus.Add(obj);
                }

                obj.Status = "Active";
                obj.StatusID = statusID;
                obj.SubStatusID = SubStatusID;


                db.SaveChanges();
            }
            return "";
        }

        public class SubStatusRelationData
        {
            public string ID { get; set; }
            public string StatusID { get; set; }
            public string SubStatusID { get; set; }
            public string UserID { get; set; }

        }


        [WebMethod]
        public string BindSubSTatusRelation(string StatusID)
        {
            int statusID = Convert.ToInt32(StatusID);
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = from a in db.tb_RelationStatusSubStatus
                  where a.Status == "Active" && a.StatusID == statusID
                  orderby a.ID descending
                  select new
                  {
                      a.ID,
                      a.StatusID,
                      a.SubStatusID
                  };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string jason = serial.Serialize(obj);
            return jason;
        }

        // ============================Post office===========================
        //bind postoffice
        [WebMethod]
        public string BindPostOffice()
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = (from a in db.tb_PostOffice
                   where a.Status == "Active"
                   orderby a.ID descending
                   select new
                   {
                       a.ID,
                       state = a.tb_State.Name,
                       StateID = a.StateID,
                       district = a.tb_District.DistrictName,
                       DistrictID = a.DistrictID,
                       a.Name,

                       a.Status

                   }).Take(300);
            // var data = obj.ToList().Count();
            JavaScriptSerializer serial = new JavaScriptSerializer();
            serial.MaxJsonLength = Int32.MaxValue;  // Set max length programmatically
            string json = serial.Serialize(obj);
            return json;
        }



        //=========================  auto bind        ===============================
        [WebMethod]
        public string GetDistricts(String name)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = from a in db.tb_District
                  where a.Status == "Active"
                  && (name != "" ? a.DistrictName.ToLower().Contains(name.ToLower()) : true)
                  orderby a.ID descending
                  select new
                  {
                      a.ID,
                      a.DistrictName,
                  };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string json = serial.Serialize(obj);
            return json;

        }

        // insert postoffice
        [WebMethod]
        public string InsertPostOffice(PostOfficeData data)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_PostOffice obj = new tb_PostOffice();
            int ID = Convert.ToInt32((data.ID != null || data.ID != "0") ? data.ID.ToString() : "0");
            int distID = Convert.ToInt32(data.DistrictID);

            var objCheck = db.tb_PostOffice.FirstOrDefault(u => u.Name.ToLower() == data.Name.ToLower() && u.ID != ID && u.Status == "Active" && u.DistrictID == distID);
            if (objCheck != null)
            {
                return "Exits";
            }
            if (ID == 0)
            {
                obj.CreatedBy = Convert.ToInt32(data.UserID);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedTime = DateTime.Now.TimeOfDay;
                db.tb_PostOffice.Add(obj);
            }
            else
            {
                obj = db.tb_PostOffice.FirstOrDefault(s => s.ID == ID);
                obj.ModifyBy = Convert.ToInt32(data.UserID);
                obj.ModifyDate = DateTime.Now;
                obj.ModifyTime = DateTime.Now.TimeOfDay;
            }

            obj.Status = "Active";
            obj.Name = data.Name;

            obj.DistrictID = distID;


            obj.StateID = db.tb_District.FirstOrDefault(s => s.ID == distID).StateID;
            db.SaveChanges();
            if (ID == 0)
            {
                return "Inserted";
            }
            else
            {
                return "Updated";
            }
        }

        // PostOffice action
        [WebMethod]
        public string PostOfficeAction(string actionLabel, int editID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            var obj = db.tb_PostOffice.FirstOrDefault(s => s.ID == editID);
            if (actionLabel == "Edit")
            {
                if (obj != null)
                {
                    PostOfficeData data = new PostOfficeData
                    {
                        ID = obj.ID.ToString(),
                        Name = obj.Name,
                        DistrictName = obj.tb_District.DistrictName,
                    };

                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    string json = serial.Serialize(data);
                    return json;
                }
            }
            else if (actionLabel == "Delete")
            {
                if (obj != null)
                {
                    if (obj.Status == "Active")
                    {
                        obj.Status = "InActive";
                        db.SaveChanges();
                    }
                    else
                    {
                        obj.Status = "Active";
                        db.SaveChanges();
                    }
                }
                return "Deleted Successfully";
            }

            return string.Empty;
        }
        public class PostOfficeData
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string StateID { get; set; }
            public string DistrictID { get; set; }
            public string DistrictName { get; set; }
            public string UserID { get; set; }

        }



        /// <summary>
        /// /////////////////////////////////////========PinCode ===================/////////////////////////////////////////////////

        //bind pincode

        [WebMethod]
        public string BindPinCode()
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = (from a in db.tb_Pincode
                   where a.Status == "Active"
                   orderby a.ID descending
                   select new
                   {
                       a.ID,
                       state = a.tb_State.Name,
                       district = a.tb_District.DistrictName,
                       postoffice = a.tb_PostOffice.Name,
                       a.Pincode,
                       a.Status
                   }).Take(300);
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string jason = serial.Serialize(obj);
            return jason;
        }

        [WebMethod]
        public string InsertPinCode(PinCode data)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_Pincode obj = new tb_Pincode();
            int ID = Convert.ToInt32((data.ID != null || data.ID != "0") ? data.ID.ToString() : "0");
            int distID = Convert.ToInt32(data.DistrictID);
            int StateID = Convert.ToInt32(data.StateID);
            int postofficeID = Convert.ToInt32(data.PostOfficeID);
            var objCheck = db.tb_Pincode.FirstOrDefault(u => u.Pincode.ToLower() == data.Pin.ToLower() && u.StateId == StateID && u.DistrictId == distID && u.PostofficeID == postofficeID && u.ID != ID && u.Status == "Active");
            if (objCheck != null)
            {
                return "Exits";
            }
            if (ID == 0)
            {
                obj.CreatedBy = Convert.ToInt32(data.UserID);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedTime = DateTime.Now.TimeOfDay;
                db.tb_Pincode.Add(obj);
            }
            else
            {
                obj = db.tb_Pincode.FirstOrDefault(s => s.ID == ID);
                obj.ModifyBy = Convert.ToInt32(data.UserID);
                obj.ModifyDate = DateTime.Now;
                obj.ModifyTime = DateTime.Now.TimeOfDay;
            }

            obj.Status = "Active";
            obj.Pincode = data.Pin;
            int postoffice = Convert.ToInt32(data.PostOfficeID);

            obj.PostofficeID = postoffice;
            obj.StateId = Convert.ToInt32(data.StateID);
            obj.DistrictId = Convert.ToInt32(data.DistrictID);

            //obj.StateId = db.tb_PostOffice.FirstOrDefault(s => s.ID == postoffice).StateID;
            //obj.DistrictId = db.tb_PostOffice.FirstOrDefault(s => s.ID == postoffice).DistrictID;


            db.SaveChanges();
            if (ID == 0)
            {
                return "Inserted";
            }
            else
            {
                return "Updated";
            }
        }

        // PostOffice action
        //[WebMethod]
        //public string PinCode2(string actionLabel, int editID)
        //{
        //    PragticrmDBEntities db = new PragticrmDBEntities();
        //    var obj = db.tb_Pincode.FirstOrDefault(s => s.ID == editID);
        //    if (actionLabel == "Edit")
        //    {
        //        if (obj != null)
        //        {
        //            PinCode data = new PinCode
        //            {
        //                ID = obj.ID.ToString(),
        //                Pin = obj.Pincode,
        //                DistrictName = obj.tb_District.DistrictName,
        //                StateName = obj.tb_State.Name,

        //            };

        //            JavaScriptSerializer serial = new JavaScriptSerializer();
        //            string json = serial.Serialize(data);
        //            return json;
        //        }
        //    }
        //    else if (actionLabel == "Delete")
        //    {
        //        if (obj != null)
        //        {
        //            if (obj.Status == "Active")
        //            {
        //                obj.Status = "InActive";
        //                db.SaveChanges();
        //            }
        //            else
        //            {
        //                obj.Status = "Active";
        //                db.SaveChanges();
        //            }
        //        }
        //        return "Deleted Successfully";
        //    }

        //    return null;
        //}
        public class PinCode
        {
            public string ID { get; set; }
            public string StateName { get; set; }
            public string PostOfficeName { get; set; }

            public string StateID { get; set; }
            public string DistrictID { get; set; }
            public string PostOfficeID { get; set; }

            public string DistrictName { get; set; }
            public string UserID { get; set; }
            public string Pin { get; set; }

        }

        //=========================  auto bind -state       ===============================
        [WebMethod]
        public string GetStates(String state)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = from a in db.tb_State
                  where a.Status == "Active"
                  && (state != "" ? a.Name.ToLower().Contains(state.ToLower()) : true)
                  orderby a.ID descending
                  select new
                  {
                      a.ID,
                      a.Name,
                  };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string json = serial.Serialize(obj);
            return json;

        }
        //=========================  auto bind - get district       ===============================
        [WebMethod]
        public string GetDistrict(String name, string stateid)
        {

            int StateID = Convert.ToInt32(stateid);
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = from a in db.tb_District
                  where a.Status == "Active"
                  && (name != "" ? a.DistrictName.ToLower().Contains(name.ToLower()) : true) && (stateid != "0" && StateID != 0 ? a.StateID == StateID : true)
                  orderby a.ID descending
                  select new
                  {
                      a.ID,
                      a.DistrictName,
                  };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string json = serial.Serialize(obj);
            return json;

        }

        //=========================  auto bind -get post office       ===============================
        [WebMethod]
        public string GetPostOffice(String name, string districtId)
        {
            int DistrictID = Convert.ToInt32(districtId);

            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = from a in db.tb_PostOffice
                  where a.Status == "Active"
                  && (name != "" ? a.Name.ToLower().Contains(name.ToLower()) : true) && (districtId != "0" && DistrictID != 0 ? a.DistrictID == DistrictID : true)
                  orderby a.ID descending
                  select new
                  {
                      a.ID,
                      a.Name,
                  };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            String json = new JavaScriptSerializer { MaxJsonLength = Int32.MaxValue }.Serialize(obj);
            return json;

        }

        //=================== Manage order place criteria start==================

        //bind order place criteria

        [WebMethod]
        public string BindOrderPlaceCriteria()
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            var obj = db.tb_OrderPlaceCriteria.FirstOrDefault();
            if (obj != null)
            {
                return obj.MaximumDate;
            }
            return string.Empty;
        }

        //insert order place criteria
        [WebMethod]
        public string InsertOrderPlaceCriteria(string MaximumDate, int UserID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_OrderPlaceCriteria obj = new tb_OrderPlaceCriteria();

            // Update existing record
            obj = db.tb_OrderPlaceCriteria.FirstOrDefault();
            if (obj != null)
            {
                obj.MaximumDate = MaximumDate;
                obj.ModifyBy = UserID;
                obj.ModifyDate = DateTime.Now;
                obj.ModifyTime = DateTime.Now.TimeOfDay;
                db.SaveChanges();
            }
            else
            {
                db.tb_OrderPlaceCriteria.Add(obj);
                obj.MaximumDate = MaximumDate;
                obj.CreatedBy = UserID;
                obj.CreateddDate = DateTime.Now.Date;
                obj.CreatedTime = DateTime.Now.TimeOfDay;
                db.SaveChanges();
                return "Inserted";
            }
            return "Result Updated";
        }
        //=================== Manage order place criteria end  ==================


        //=================== Manage Range Master start ==================

        //bind Range Master

        [WebMethod]
        public string BindRangeMaster()
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = from a in db.tb_RangeMaster
                  where a.Status == "Active"
                  orderby a.ID descending
                  select new
                  {
                      a.ID,
                      a.RangeMin,
                      a.RangeMax,
                      a.Status
                  };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string jason = serial.Serialize(obj);
            return jason;
        }
        // insert role

        [WebMethod]
        public string InsertRangeMaster(RangeMasterData data)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_RangeMaster obj = new tb_RangeMaster();
            int ID = Convert.ToInt32((data.ID != null || data.ID != "0") ? data.ID.ToString() : "0");

            var objCheck = db.tb_RangeMaster.FirstOrDefault(u => u.RangeMin == data.min && u.RangeMax == data.max && u.ID != ID && u.Status == "Active");
            if (objCheck != null)
            {
                return "Exits";
            }
            if (ID == 0)
            {
                obj.CreatedBy = Convert.ToInt32(data.UserID);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedTime = DateTime.Now.TimeOfDay;
                db.tb_RangeMaster.Add(obj);
            }
            else
            {
                obj = db.tb_RangeMaster.FirstOrDefault(s => s.ID == ID);
                obj.ModifyBy = Convert.ToInt32(data.UserID);
                obj.ModifyDate = DateTime.Now;
                obj.ModifyTime = DateTime.Now.TimeOfDay;
            }

            obj.Status = "Active";
            obj.RangeMin = data.min;
            obj.RangeMax = data.max;
            db.SaveChanges();
            if (ID == 0)
            {
                return "Inserted";
            }
            else
            {
                return "Updated";
            }
        }
        public class RangeMasterData
        {
            public string ID { get; set; }
            public decimal min { get; set; }
            public decimal max { get; set; }
            public string UserID { get; set; }

        }

        // role action
        [WebMethod]
        public string RangeMasterAction(string actionLabel, int editID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            var obj = db.tb_RangeMaster.FirstOrDefault(s => s.ID == editID);
            if (actionLabel == "Edit")
            {
                if (obj != null)
                {
                    RangeMasterData data = new RangeMasterData
                    {
                        ID = obj.ID.ToString(),
                        min = Convert.ToDecimal(obj.RangeMin),
                        max = Convert.ToDecimal(obj.RangeMax),

                    };

                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    string json = serial.Serialize(data);
                    return json;
                }
            }
            else if (actionLabel == "Delete")
            {
                if (obj != null)
                {
                    if (obj.Status == "Active")
                    {
                        obj.Status = "InActive";
                        db.SaveChanges();
                    }
                    else
                    {
                        obj.Status = "Active";
                        db.SaveChanges();
                    }
                }
                return "Deleted Successfully";
            }

            return string.Empty;
        }

        //=================== Manage Range Master end ==================


        //=================== Manage  vendor  range start==================

        //bind sub status

        [WebMethod]
        public string BindRelationRangeVendorCheckbox()
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = from a in db.tb_Vendor
                  where a.Satatus == "Active"
                  orderby a.ID descending
                  select new
                  {
                      a.ID,
                      a.Name,
                      a.Satatus
                  };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string jason = serial.Serialize(obj);
            return jason;
        }
        //insert vendor range
        //public string InsertRelationRangeVendor(RelationRangeVendoData data, int[] selectedCheckboxValues)
        //{
        //    PragticrmDBEntities db = new PragticrmDBEntities();
        //    tb_RelationRangeVendor obj = new tb_RelationRangeVendor();

        //    int ID = Convert.ToInt32((data.ID != null && data.ID != "0") ? data.ID.ToString() : "0");
        //    var objCheck = db.tb_RelationRangeVendor.FirstOrDefault(u => u.RangeID == data.RangeID && u.Rate == data.Rate && u.ID != ID && u.Status == "Active");

        //    if (objCheck != null)
        //    {
        //        return "Exits";
        //    }


        //    // If selectedCheckboxValues is not null or empty, handle them
        //    if (selectedCheckboxValues != null && selectedCheckboxValues.Length > 0)
        //    {
        //        var existingGroups = db.tb_RelationRangeVendor.Where(g => g.VendorID == obj.VendorID).ToList();
        //        foreach (var group in existingGroups)
        //        {
        //            //db.tb_RelationRangeVendor.Remove(group); // Deleting old relations
        //            tb_RelationRangeVendor groupRelation = new tb_RelationRangeVendor
        //            {
        //                ModifyBy = Convert.ToInt32(data.UserID),
        //                ModifyDate = DateTime.Now,
        //                ModifyTime = DateTime.Now.TimeOfDay,
        //            };
        //        }

        //        // Insert new group relations based on selectedCheckboxValues
        //        foreach (var groupId in selectedCheckboxValues)
        //        {
        //            tb_RelationRangeVendor groupRelation = new tb_RelationRangeVendor
        //            {
        //                VendorID = groupId,
        //                RangeID = data.RangeID, // GroupID from selectedCheckboxValues array
        //                Status = "Active",
        //                Rate=data.Rate,

        //                CreatedBy = Convert.ToInt32(data.UserID),
        //                CreatedDate = DateTime.Now,
        //                CreatedTime = DateTime.Now.TimeOfDay,
        //            };
        //            db.tb_RelationRangeVendor.Add(groupRelation);
        //        }

        //        db.SaveChanges();
        //    }

        //    if (ID == 0)
        //    {
        //        return "Inserted";
        //    }
        //    else
        //    {
        //        return "Updated";
        //    }
        //}

        [WebMethod]
        public string InsertRelationRangeVendor(RelationRangeVendoData data, int[] selectedCheckboxValues)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            bool checkduplicate = true;

            // Loop through the selected vendors and check for duplicates
            foreach (var vendorID in selectedCheckboxValues)
            {
                if (db.tb_RelationRangeVendor.Any(a => a.RangeID == data.RangeID && a.VendorID == vendorID && a.Status == "Active"))
                {
                    checkduplicate = false;
                    break;
                }
            }

            if (!checkduplicate)
            {
                return "Exits"; 
            }

            tb_RelationRangeVendor obj;
            foreach (var vendorID in selectedCheckboxValues)
            {
                obj = db.tb_RelationRangeVendor.FirstOrDefault(a => a.VendorID == vendorID && a.RangeID == data.RangeID && a.Status == "Active");

                if (obj == null)
                {
                    obj = new tb_RelationRangeVendor();
                    db.tb_RelationRangeVendor.Add(obj);
                    obj.CreatedBy = Convert.ToInt32(data.UserID);
                    obj.CreatedDate = DateTime.Now;
                    obj.CreatedTime = DateTime.Now.TimeOfDay;
                    obj.Status = "Active";
                }
                else
                {
                    obj.ModifyBy = Convert.ToInt32(data.UserID);
                    obj.ModifyDate = DateTime.Now;
                    obj.ModifyTime = DateTime.Now.TimeOfDay;
                }

                obj.RangeID = data.RangeID;
                obj.Rate = data.Rate;
                obj.VendorID = vendorID;

                db.SaveChanges();
            }

            return "Data Successfully Updated";
        }

        public class RelationRangeVendoData
        {

            public string ID { get; set; }
            public int RangeID { get; set; }
            public string RangeName { get; set; }
            public decimal Rate { get; set; }
            public int VendorID { get; set; }
            public string VendorName { get; set; }
            public string UserID { get; set; }
            public List<int> SelectedGroups { get; set; }
        }
        //bind method
        //[WebMethod]
        //public string BindRelationRangeVendor()
        //{
        //    PragticrmDBEntities db = new PragticrmDBEntities();
        //    IQueryable<object> obj = null;
        //    obj = from a in db.tb_RelationRangeVendor
        //          where a.Status == "Active"
        //          orderby a.ID descending
        //          select new
        //          {
        //              a.ID,
        //              min = a.tb_RangeMaster.RangeMin,
        //              max = a.tb_RangeMaster.RangeMax,


        //              a.VendorID,
        //              a.Rate,
        //              a.Status


        //          };
        //    JavaScriptSerializer serial = new JavaScriptSerializer();
        //    string jason = serial.Serialize(obj);
        //    return jason;
        //}

        //=================== Manage  vendor  range end==================








        // ============================Manage add company ===========================

        [WebMethod]
        public string BindAddCompany()
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = (from a in db.tb_Vendor
                   where a.Satatus == "Active"
                   && a.Type == "Company"
                   orderby a.ID descending
                   select new
                   {
                       a.ID,
                       a.VendorName,
                       a.VendorCode,
                       a.GSTno,
                       a.VendorAddress,
                       a.ContactPersonName,
                       a.ContactPersonNo,
                       a.Satatus

                   });
            // var data = obj.ToList().Count();
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string json = serial.Serialize(obj);
            return json;
        }

        [WebMethod]
        public string InsertAddCompany(AddCompanyData data)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_Vendor obj = new tb_Vendor();
            
            int ID = Convert.ToInt32((data.ID != null || data.ID != "0") ? data.ID.ToString() : "0");

            var objCheck = db.tb_Vendor.FirstOrDefault(a => a.VendorName.ToLower() == data.CompanyName.ToLower() && a.GSTno.ToLower() == data.GST.ToLower() && a.ID != ID);
            if (objCheck != null)
            {
                return "Exits";
            }
            if (ID == 0)
            {
                obj.CreatedBy = Convert.ToInt32(data.UserID);
                obj.CreatedDateAndTime = DateTime.Now.Date;
                db.tb_Vendor.Add(obj);
            }
            else
            {
                obj = db.tb_Vendor.FirstOrDefault(s => s.ID == ID);
                obj.ModifiedBy = Convert.ToInt32(data.UserID);
                obj.ModifiedDateAndTime = DateTime.Now.Date;
            }

            obj.Satatus = "Active";
            obj.VendorName = data.CompanyName;
            obj.VendorCode = Convert.ToInt32(data.CompanyCode);
            obj.ContactPersonName = data.ContactPersonName;
            obj.ContactPersonNo = data.ContactPersonContact;
            obj.Email = data.Email;
            obj.PanNo = data.PAN;
            obj.VendorAddress = data.VendorAddress;
            obj.GSTno = data.GST;
            obj.BankName = data.Bank;
            obj.Branch = data.Branch;
            obj.IFSC = data.IFSC;
            obj.AC = data.Account;



            db.SaveChanges();
            if (ID == 0)
            {
                return "Inserted";
            }
            else
            {
                return "Updated";
            }
        }
        [WebMethod]
        public string AddVendorAction(string actionLabel, int editID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            var obj = db.tb_Vendor.FirstOrDefault(s => s.ID == editID);
            if (actionLabel == "Edit")
            {
                if (obj != null)
                {
                    AddVendorData data = new AddVendorData
                    {
                        ID = obj.ID.ToString(),
                        VendorName=obj.VendorName,
                       VendorCode=Convert.ToString(obj.VendorCode),
                       ContactPersonName=obj.ContactPersonName,
                       ContactPersonContact=obj.ContactPersonNo,
                       Email=obj.Email,
                       PAN=obj.PanNo,
                       VendorAddress=obj.VendorAddress,
                       GST=obj.GSTno,
                       Bank=obj.BankName,
                       Branch=obj.Branch,
                       IFSC=obj.IFSC,
                       Account=obj.AC,
                         };

                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    string json = serial.Serialize(data);
                    return json;
                }
            }
            else if (actionLabel == "Delete")
            {
                if (obj != null)
                {
                    if (obj.Satatus == "Active")
                    {
                        obj.Satatus = "InActive";
                        db.SaveChanges();
                    }
                    else
                    {
                        obj.Satatus = "Active";
                        db.SaveChanges();
                    }
                }
                return "Deleted Successfully";
            }

            return string.Empty;
        }


        public class AddCompanyData
        {
            public string ID { get; set; }
            public string CompanyName { get; set; }
            public string CompanyCode { get; set; }
            public string ContactPersonName { get; set; }
            public string ContactPersonContact { get; set; }
            public string Email { get; set; }
            public string PAN { get; set; }
            public string VendorAddress { get; set; }
            public string GST { get; set; }
            public string Bank { get; set; }
            public string Branch { get; set; }
            public string IFSC { get; set; }
            public string Account { get; set; }
            public string UserID { get; set; }

        }

        ///-----------------------------Manage Stock----------------------------------
        [WebMethod]
        public string GetProducts(int vendorID, int categoryID)
        {
            using (PragticrmDBEntities db = new PragticrmDBEntities())
            {
                var vendorName = db.tb_Vendor.FirstOrDefault(v => v.ID == vendorID)?.VendorName;

                var obj = from u in db.tb_Product
                          where u.Status == "Active"
                          && (categoryID == 0 ? true : categoryID == u.CategoryID)
                          select new
                          {
                              u.ID,
                              u.ProductName,
                              VendorName = db.tb_Vendor.FirstOrDefault(a => a.ID == vendorID).VendorName,
                              VendorID = db.tb_Vendor.FirstOrDefault(a => a.ID == vendorID).ID,

                              Stock = (db.tb_ItemDIstributorStock.Count(a => a.ItemID == u.ID && a.VendorID == vendorID) > 0 ? db.tb_ItemDIstributorStock.FirstOrDefault(a => a.ItemID == u.ID && a.VendorID == vendorID).Stock : 0),
                              
                          };
                JavaScriptSerializer serial = new JavaScriptSerializer();
                string json = serial.Serialize(obj);
                return json;
            }
        }
        //update stock when leave 
        [WebMethod]
        public string UpdateStock( int VendorId, int ItemID, int Stock)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_ItemDIstributorStock obj = db.tb_ItemDIstributorStock.FirstOrDefault(a => a.ItemID == ItemID && a.VendorID == VendorId);
            if (obj == null)
            {
                obj = new tb_ItemDIstributorStock();
                db.tb_ItemDIstributorStock.Add(obj);
                //obj.CreatedBy
                obj.Status = "Active";
                obj.Stock = Convert.ToDecimal(Stock);
                obj.CreatedDate = DateTime.Now.Date;
                obj.ItemID = ItemID;
                obj.VendorID = VendorId;
                obj.CreatedTime = DateTime.Now.TimeOfDay;
            }
            else
            {
                obj.Stock += Convert.ToDecimal(Stock);
                obj.ModifyDate = DateTime.Now.Date;
                obj.ModifyTime = DateTime.Now.TimeOfDay;
                obj.Status = "Active";
            }
            tb_ItemDIstributorStock objvendor = db.tb_ItemDIstributorStock.FirstOrDefault(a => a.ItemID == ItemID && a.VendorID == 18);
            if (objvendor == null)
            {
                objvendor = new tb_ItemDIstributorStock();
                db.tb_ItemDIstributorStock.Add(objvendor);
                objvendor.Status = "Active";
                objvendor.Stock = (-1) * Convert.ToDecimal(Stock);
                objvendor.CreatedDate = DateTime.Now.Date;
                objvendor.ItemID = ItemID;
                objvendor.VendorID = VendorId;
                objvendor.CreatedTime = DateTime.Now.TimeOfDay;
            }
            else
            {
                objvendor.Stock -= Convert.ToDecimal(Stock);
                objvendor.ModifyDate = DateTime.Now.Date;
                objvendor.ModifyTime = DateTime.Now.TimeOfDay;
                objvendor.Status = "Active";
            }
         //   AddStockTransaction.AddTransaction(VendorId, ItemID, Convert.ToDecimal(Stock));
            db.SaveChanges();

            return "success";


        }

        //====================  Manage Employee Start  =========================//

        //bind ReportTo

        [WebMethod]
        public string BindReportTo()
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = from u in db.tb_Employee
                              where u.Status == "Active" && u.VendorID == null
                              select new
                              {
                                  u.ID,
                                  u.Name,
                              };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string jason = serial.Serialize(obj);
            return jason;
        }

        //bind Assign Auditor

        [WebMethod]
        public string BindAssignAuditor()
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = from u in db.tb_Employee
                               where u.Status == "Active" && u.VendorID == null && u.RoleID == 18
                               select new
                               {
                                   u.ID,
                                   u.Name,
                               };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string jason = serial.Serialize(obj);
            return jason;
        }


        //bind Employee

        [WebMethod]
        public string BindEmployee()
            {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = from u in db.tb_Employee
                      where u.VendorID == null
                 && u.Status == "Active"

                  orderby u.ID descending
                  select new 
                      {
                          u.ID,
                          u.Name,
                          u.ContactNo,
                          Role = db.tb_Role.FirstOrDefault(a => a.ID == u.RoleID).Name,
                          u.Dob,
                          u.Status,
                          u.VendorID,
                      };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string jason = serial.Serialize(obj);
            return jason;
        }
        // insert client

        [WebMethod]
        public string InsertEmployee(EmployeeData data)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_Employee obj = new tb_Employee();
            int ID = Convert.ToInt32((data.ID != null || data.ID != "0") ? data.ID.ToString() : "0");
            var objCheck = db.tb_Employee.FirstOrDefault(u => u.Name.ToLower() == data.Name.ToLower() && u.ID != ID && u.Status == "Active");
            if (objCheck != null)
            {
                return "Exits";
            }
            if (ID == 0)
            {
                obj.CreatedBY = Convert.ToInt32(data.UserID);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedTime = DateTime.Now.TimeOfDay;
                db.tb_Employee.Add(obj);
            }
            else
            {
                obj = db.tb_Employee.FirstOrDefault(s => s.ID == ID);
                obj.ModifyBy = Convert.ToInt32(data.UserID);
                obj.ModifyDate = DateTime.Now;
                obj.ModifyTime = DateTime.Now.TimeOfDay;
            }

            obj.Status = "Active";
            obj.Name = data.Name;
            obj.Address = data.Address;
            obj.ContactNo = data.Contact;
            obj.Email = data.Email;
            obj.Dob = Convert.ToDateTime(data.DOB);
            obj.UserName = data.UserName;
            obj.Password = data.Password;
            obj.RoleID = Convert.ToInt32(data.RoleID);
            obj.ReportTo = Convert.ToInt32(data.ReportToID);
            obj.AssignAuditor = Convert.ToInt32(data.AssignAuditorID);
            obj.Salary = Convert.ToDecimal(data.Salary);
            obj.EmergencyContact = data.EmergencyContact;
            obj.JoiningDate = Convert.ToDateTime(data.JoiningDate);
            obj.TaskOrder = Convert.ToInt32(data.TaskLimit);



            db.SaveChanges();
            if (ID == 0)
            {
                return "Inserted";
            }
            else
            {
                return "Updated";
            }
        }
        public class EmployeeData
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Contact { get; set; }
            public string Email { get; set; }
            public String DOB { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string RoleID { get; set; }
            public string ReportToID { get; set; }
            public string AssignAuditorID { get; set; }
            public string Salary { get; set; }
            public string EmergencyContact { get; set; }
            public string JoiningDate { get; set; }
            public string TaskLimit { get; set; }
            public string UserID { get; set; }

        }


        [WebMethod]
        public string EmployeeAction(string actionLabel, int editID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            var obj = db.tb_Employee.FirstOrDefault(s => s.ID == editID);
            if (actionLabel == "Edit")
            {
                if (obj != null)
                {
                    EmployeeData data = new EmployeeData
                    {
                        ID = obj.ID.ToString(),
                        Name = obj.Name,
                        Address = obj.Address,
                        Contact = obj.ContactNo,
                        Email = obj.Email,
                        DOB = (obj.Dob != null ? Convert.ToDateTime(obj.Dob).ToString("yyyy-MM-dd") : ""),
                        UserName = obj.UserName,
                        Password = obj.Password,
                        RoleID = Convert.ToString(obj.RoleID),
                        ReportToID=Convert.ToString(obj.ReportTo),
                        AssignAuditorID=Convert.ToString(obj.AssignAuditor),
                        Salary=Convert.ToString(obj.Salary),
                        EmergencyContact=obj.EmergencyContact,
                        JoiningDate = (obj.JoiningDate != null ? Convert.ToDateTime(obj.JoiningDate).ToString("yyyy-MM-dd") : ""),

                        TaskLimit = Convert.ToString(obj.TaskOrder),
                    };

                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    string json = serial.Serialize(data);
                    return json;
                }
            }
            else if (actionLabel == "Delete")
            {
                if (obj != null)
                {
                    if (obj.Status == "Active")
                    {
                        obj.Status = "InActive";
                        db.SaveChanges();
                    }
                    else
                    {
                        obj.Status = "Active";
                        db.SaveChanges();
                    }
                }
                return "Deleted Successfully";
            }
           

            return string.Empty;
        }

        //=================== Manage Employee end ==================//


        //=================== Manage offer form start ==================//
        [WebMethod]
        public string BindDate( string id)
        {
            id = "0";
            int days = DateTime.Now.Day;
            int DaysInMonth = 0;
            DateTime dtime = DateTime.Now.Date;
            string str = "";
            //for (int i = days; str != "Saturday"; i++)
            //{
            //    DateTime day = new DateTime(dtime.AddDays(i).Year, dtime.AddDays(i).Month, i);
            //    str = day.DayOfWeek.ToString();
            int x = Convert.ToInt32(dtime.DayOfWeek);
            DaysInMonth = (x - 1);
            //    if (day.DayOfWeek != DayOfWeek.Saturday)
            //    {
            //        DaysInMonth++;
            //    }
            //}
            DateTime afterFiveDays = DateTime.Now.AddDays(Convert.ToInt32(-DaysInMonth + 5));
            DateTime today = afterFiveDays.AddDays(-5);

            var gh = new
            {
                lblFromDate = Convert.ToDateTime(today).ToString("yyyy-MM-dd"),
                lblToDate = Convert.ToDateTime(afterFiveDays).ToString("yyyy-MM-dd"),
            };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string json = serial.Serialize(gh);
            return json;
        }


        [WebMethod]
        public string InsertOfferForm(OfferFormData data, int[] EmpChk)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();

                for (int i=0; i<EmpChk.Length; i++ )
                {
                    //string[] demo = item.Split(',');
                    //demo = demo[0].Split('[');
                    //int assignTo = Convert.ToInt32(demo[1].ToString());

                    tb_Offers obj = db.tb_Offers.FirstOrDefault(a => a.Offer == data.Offer && a.OfferAssignTo == data.OfferAssignTo && a.CreatedBy == data.UserID);

                    if (obj == null)
                    {
                    obj = new tb_Offers
                    {
                        CreatedBy = data.UserID,
                        CreatedDate = DateTime.Now.Date,
                        CreatedTime = DateTime.Now.TimeOfDay,
                        OfferAssignTo =Convert.ToInt16( EmpChk[i]),
                            Status = "Active",
                            Amount = Convert.ToDecimal(data.Amount),
                            Offer = data.Offer
                        };
                        db.tb_Offers.Add(obj);
                    }
                    else
                    {
                        obj.ModifyBy = data.UserID;
                        obj.ModifyDate = DateTime.Now.Date;
                        obj.ModifyTime = DateTime.Now.TimeOfDay;
                        obj.Status = "Active";
                        obj.Amount = Convert.ToDecimal(data.Amount);
                    obj.OfferAssignTo = Convert.ToInt16(EmpChk[i]);
                        obj.Offer = data.Offer;
                    }

                    db.SaveChanges();
                }

                return "Offer saved successfully";
           
        }
        

        public class OfferFormData
        {

            public string ID { get; set; }
            public string Amount { get; set; }
            public int OfferAssignTo { get; set; }
            public string Offer { get; set; }
            public int UserID { get; set; }
            public List<int> SelectedGroups { get; set; }
        }
        //--------------------------  Manage offer form end  --------------------------------------------



        //--------------------------  Manage set target start --------------------------------------------        

        [WebMethod]
        public string BindTarget(string RoleID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            
                int currentMonthYear = Convert.ToInt32(DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString());
                int parsedRoleID = Convert.ToInt32(RoleID);  // Directly parse RoleID from string
                IQueryable<object> obj = null;

            // Fetch employee data based on RoleID
            obj = from u in db.tb_Employee
                      where u.Status == "Active" && (RoleID !="" || RoleID != "0" ?u.RoleID == parsedRoleID : true)
                      select new
                      {
                          u.ID,
                          
                          EmployeeName = u.Name,
                          RoleName = u.tb_Role.Name,
                          Amount = (db.tb_TargetAmountMonthly.FirstOrDefault(a => a.EmployeeID == u.ID && a.Month == currentMonthYear) == null
                                      ? 0
                                      : db.tb_TargetAmountMonthly.FirstOrDefault(a => a.EmployeeID == u.ID && a.Month == currentMonthYear).TargetAmount)
                      };

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string jsonResponse = serializer.Serialize(obj);
                return jsonResponse;
            }


        //update stock when leave 
        [WebMethod]
        public string UpdateTarget(int EmpId, int NewAmount, int UserID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            int dt = Convert.ToInt32(DateTime.Now.Month + "" + DateTime.Now.Year);

            tb_TargetAmountMonthly obj = db.tb_TargetAmountMonthly.FirstOrDefault(a => a.EmployeeID == EmpId && a.Month == dt);

            if (obj == null)
            {
                // If no record exists, create a new one
                obj = new tb_TargetAmountMonthly
                {
                    CreatedBy = UserID,
                    Status = "Active",
                    TargetAmount = Convert.ToDecimal(NewAmount),
                    CreatedDate = DateTime.Now.Date,
                    CreatedTime = DateTime.Now.TimeOfDay,
                    EmployeeID = EmpId,
                    Month = dt
                };
                db.tb_TargetAmountMonthly.Add(obj);
            }
            else
            {
                // Update the existing record
                obj.ModifyBy = UserID;
                obj.TargetAmount = Convert.ToDecimal(NewAmount);
                obj.ModifyDate = DateTime.Now.Date;
                obj.ModifyTime = DateTime.Now.TimeOfDay;
                obj.Status = "Active";
            }

            db.SaveChanges();

            return "success";


        }
        //--------------------------  Manage set target end  --------------------------------------------        

        //---------------------------------  manage Vendor employee  start------------------------------------------
        [WebMethod]
        public string InsertVendorEmployee(EmployeeData data)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_Employee obj = new tb_Employee();
            int ID = Convert.ToInt32((data.ID != null || data.ID != "0") ? data.ID.ToString() : "0");
            var objCheck = db.tb_Employee.FirstOrDefault(u => u.Name.ToLower() == data.Name.ToLower() && u.ID != ID && u.Status == "Active");
            if (objCheck != null)
            {
                return "Exits";
            }
            if (ID == 0)
            {
                obj.CreatedBY = Convert.ToInt32(data.UserID);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedTime = DateTime.Now.TimeOfDay;
                db.tb_Employee.Add(obj);
            }
            else
            {
                obj = db.tb_Employee.FirstOrDefault(s => s.ID == ID);
                obj.ModifyBy = Convert.ToInt32(data.UserID);
                obj.ModifyDate = DateTime.Now;
                obj.ModifyTime = DateTime.Now.TimeOfDay;
            }

            obj.Status = "Active";
            obj.Name = data.Name;
            obj.Address = data.Address;
            obj.ContactNo = data.Contact;
            obj.Email = data.Email;
            obj.Dob = Convert.ToDateTime(data.DOB);
            obj.UserName = data.UserName;
            obj.Password = data.Password;
            obj.RoleID = Convert.ToInt32(data.RoleID);
      
            if (ID == 0)
            {
                return "Inserted";
            }
            else
            {
                return "Updated";
            }
        }
        //---------------------------------  manage Vendor employee  end------------------------------------------


        [WebMethod]
        public string BindEmployeeInTask(String RoleID)
        {
            int Roleid = Convert.ToInt32(RoleID);
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = from u in db.tb_Employee
                  where u.Status == "Active" && u.RoleID == Roleid

                  orderby u.ID descending
                  select new
                  {
                      u.ID,
                      u.Name,
                      
                  };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string jason = serial.Serialize(obj);
            return jason;
        }


        [WebMethod]
        public string BindTask( int UserID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
             obj = from u in db.tb_TaskManager
                      where ((u.CreatedBy == UserID || u.Employee == UserID) && u.Status=="Active")
                      select new
                      {
                          u.ID,
                          u.FromDate,
                          RoleName = u.tb_Role.Name,
                          u.ToDate,
                          u.Topic,
                          u.Task,
                          u.Status,
                          u.Role,
                          u.CreatedBy
                        
                      };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string jason = serial.Serialize(obj);
            return jason;
        }

        [WebMethod]
        public string InsertTaskForm(TaskFormData data)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_TaskManager obj;
            DateTime toDate = Convert.ToDateTime(data.ToDate);
            DateTime fromDate = Convert.ToDateTime(data.FromDate);
            int ID = Convert.ToInt32((data.ID != null || data.ID != "0") ? data.ID.ToString() : "0");
            var objCheck = db.tb_TaskManager.FirstOrDefault(u => u.ID != ID &&u.Role ==data.Role &&u.Employee==data.Employee && u.ToDate== toDate && u.FromDate== fromDate && u.Task==data.Task && u.Topic==data.Topic && u.NotificationBit==data.NotificationBit  && u.Status == "Active");
            if (objCheck != null)
            {
                return "Exits";
            }

            if (ID == 0)
            {
                obj = new tb_TaskManager();
                db.tb_TaskManager.Add(obj);
                obj.CreatedBy = data.UserID;
                obj.CreatedDate = DateTime.Now.Date;
                obj.CreatedTime = DateTime.Now.TimeOfDay;
            }
            else
            {
                obj = db.tb_TaskManager.FirstOrDefault(a => a.ID == ID);
                obj.ModifyBy = data.UserID;
                obj.ModifyDate = DateTime.Now.Date;
                obj.ModifyTime = DateTime.Now.TimeOfDay;
            }
            obj.FromDate = Convert.ToDateTime(data.FromDate);
            obj.ToDate = Convert.ToDateTime(data.ToDate);
            obj.Role = Convert.ToInt32(data.Role);
            obj.Employee = Convert.ToInt32(data.Employee);

            obj.Status = "Active";
            obj.Type = "Role";
            
            obj.NotificationBit = true;
            obj.Task = data.Task;
            obj.Topic = data.Topic;
            db.SaveChanges();
           

            if (ID == 0)
            {
                return "Inserted";
            }
            else
            {
                return "Updated";
            }
        }

             public class TaskFormData
        {

            public string ID { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int Role { get; set; }
            public int Employee { get; set; }
            public string Status { get; set; }
            public string Type { get; set; }
            public Boolean NotificationBit { get; set; }
            public string Task { get; set; }
            public string Topic { get; set; }
            public int UserID { get; set; }
            public List<int> SelectedGroups { get; set; }
        }


        [WebMethod]
        public string TaskFormAction(string actionLabel, int editID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            var obj = db.tb_TaskManager.FirstOrDefault(s => s.ID == editID);
            if (actionLabel == "Edit")
            {
                if (obj != null)
                {
                    TaskFormData data = new TaskFormData
                    {
                        ID = obj.ID.ToString(),                        
                       FromDate = (obj.FromDate != null ? Convert.ToDateTime(obj.FromDate).ToString("yyyy-MM-dd") : ""),
                        ToDate = (obj.ToDate != null ? Convert.ToDateTime(obj.ToDate).ToString("yyyy-MM-dd") : ""),
                        Role = Convert.ToInt32(obj.Role),           
                        Task = obj.Task,
                        Topic = obj.Topic,
                    };

                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    string json = serial.Serialize(data);
                    return json;
                }
            }
            else if (actionLabel == "Delete")
            {
                if (obj != null)
                {
                    if (obj.Status == "Active")
                    {
                        obj.Status = "InActive";
                        db.SaveChanges();
                    }
                    else
                    {
                        obj.Status = "Active";
                        db.SaveChanges();
                    }
                }
                return "Deleted Successfully";
            }


            return string.Empty;
        }

        // ============================Manage add vendor ===========================
        //bind postoffice

        [WebMethod]
        public string BindAddVendor()
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = (from a in db.tb_Vendor
                   where a.Satatus == "Active"
                   && a.Type == "Vendor"
                   orderby a.ID descending
                   select new
                   {
                       a.ID,
                       a.VendorName,
                       a.VendorCode,
                       a.GSTno,
                       a.VendorAddress,
                       a.ContactPersonName,
                       a.ContactPersonNo,
                       a.Satatus

                   });
            // var data = obj.ToList().Count();
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string json = serial.Serialize(obj);
            return json;
        }
        [WebMethod]
        public string InsertAddVendor(AddVendorData data)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_Vendor obj = new tb_Vendor();

            int ID = Convert.ToInt32((data.ID != null || data.ID != "0") ? data.ID.ToString() : "0");
            tb_Employee objemp = db.tb_Employee.FirstOrDefault(a => a.VendorID == ID && a.RoleName == "Admin");
            int EID = 0;
            if (objemp != null)
            {
                EID = objemp.ID;
            }
            if (db.tb_Employee.Any(a => a.UserName.ToLower() == data.userName.ToLower() && (a.ID != (EID != 0 ? EID : ID))))
            {
                return "employee already exists";
            }
            var objCheck = db.tb_Vendor.FirstOrDefault(a => a.VendorName.ToLower() == data.VendorName.ToLower() && a.GSTno.ToLower() == data.GST.ToLower() && a.ID != ID);
            if (objCheck != null)
            {
                return "Exits";
            }
            if (ID == 0)
            {
                obj.CreatedBy = Convert.ToInt32(data.UserID);
                obj.CreatedDateAndTime = DateTime.Now.Date;
                obj.WalletBalance = 0;
                db.tb_Vendor.Add(obj);
            }
            else
            {
                obj = db.tb_Vendor.FirstOrDefault(s => s.ID == ID);
                obj.ModifiedBy = Convert.ToInt32(data.UserID);
                obj.ModifiedDateAndTime = DateTime.Now.Date;
            }
            obj.Type = "Vendor";
            obj.Satatus = "Active";
            obj.VendorName = data.VendorName;
            obj.VendorCode = Convert.ToInt32(data.VendorCode);
            obj.ContactPersonName = data.ContactPersonName;
            obj.ContactPersonNo = data.ContactPersonContact;
            obj.Email = data.Email;
            obj.PanNo = data.PAN;
            obj.VendorAddress = data.VendorAddress;
            obj.GSTno = data.GST;
            obj.BankName = data.Bank;
            obj.Branch = data.Branch;
            obj.IFSC = data.IFSC;
            obj.AC = data.Account;
            obj.UserName = data.userName;
            obj.Password = data.Password;
            obj.SecurityDeposit = Convert.ToDecimal(data.Security);
            db.SaveChanges();
            if (objemp == null)
            {
                objemp = new tb_Employee();
                db.tb_Employee.Add(objemp);
                objemp.CreatedDate = DateTime.Now.Date;
                objemp.CreatedBY =Convert.ToInt32(data.UserID);
                objemp.CreatedDate = DateTime.Now.Date;
                objemp.CreatedTime = DateTime.Now.TimeOfDay;
            }
            else
            {
                objemp.ModifyBy = Convert.ToInt32(data.UserID);
                objemp.ModifyDate = DateTime.Now.Date;
                objemp.ModifyTime = DateTime.Now.TimeOfDay;
            }
            objemp.Dob = Convert.ToDateTime(data.DOB);
            objemp.VendorID = obj.ID;
            objemp.Status = "Active";
            objemp.Name = data.VendorName;
            objemp.Password = data.Password;
            objemp.UserName = data.userName.ToLower();
            objemp.Email = data.Email;
            objemp.ContactNo = data.ContactPersonContact;
            objemp.EmergencyContactPerson = data.ContactPersonName;
            objemp.EmergencyContact = data.ContactPersonContact;
            var objuserExpiry = db.tb_Employee.FirstOrDefault(a => a.ExpireDate != null);
            if (objuserExpiry != null)
                objemp.ExpireDate = objuserExpiry.ExpireDate; //Convert.ToDateTime("12-12-2020");
            objemp.RoleName = "Admin";
            objemp.JoiningDate = DateTime.Now.Date;
           




            db.SaveChanges();
            if (ID == 0)
            {
                return "Inserted";
            }
            else
            {
                return "Updated";
            }
        }
        [WebMethod]
        public string AddVendorActionofVendor(string actionLabel, int editID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            var obj = db.tb_Vendor.FirstOrDefault(s => s.ID == editID);
            if (actionLabel == "Edit")
            {
                if (obj != null)
                {
                    //var dob  = db.tb_Employee.FirstOrDefault(s => s.ID == obj.CreatedBy).Dob.ToString();
                    var objemp = db.tb_Employee.FirstOrDefault(a => a.VendorID == obj.ID && a.RoleName == "Admin");///&& a.Status == "Active");


                    AddVendorData data = new AddVendorData
                    {
                        ID = obj.ID.ToString(),
                        VendorName = obj.VendorName,
                        VendorCode = Convert.ToString(obj.VendorCode),
                        ContactPersonName = obj.ContactPersonName,
                        ContactPersonContact = obj.ContactPersonNo,
                        Email = obj.Email,
                        PAN = obj.PanNo,
                        VendorAddress = obj.VendorAddress,
                        GST = obj.GSTno,
                        Bank = obj.BankName,
                        Branch = obj.Branch,
                        IFSC = obj.IFSC,
                        Account = obj.AC,
                        userName = obj.UserName,
                        Password = obj.Password,
                        Security = Convert.ToString(obj.SecurityDeposit),

                        //DOB = Convert.ToDateTime(objemp.Dob).ToString("yyyy-MM-dd")
                        DOB = objemp != null && objemp.Dob != null ? Convert.ToDateTime(objemp.Dob).ToString("yyyy-MM-dd") : null


                    };

                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    string json = serial.Serialize(data);
                    return json;
                }
            }
            else if (actionLabel == "Delete")
            {
                if (obj != null)
                {
                    if (obj.Satatus == "Active")
                    {
                        obj.Satatus = "InActive";
                        db.SaveChanges();
                    }
                    else
                    {
                        obj.Satatus = "Active";
                        db.SaveChanges();
                    }
                }
                return "Deleted Successfully";
            }

            return string.Empty;
        }


        public class AddVendorData
        {
            public string ID { get; set; }
            public string VendorName { get; set; }
            public string VendorCode { get; set; }
            public string ContactPersonName { get; set; }
            public string ContactPersonContact { get; set; }
            public string Email { get; set; }
            public string PAN { get; set; }
            public string VendorAddress { get; set; }
            public string GST { get; set; }
            public string Bank { get; set; }
            public string Branch { get; set; }
            public string IFSC { get; set; }
            public string Account { get; set; }
            public string userName { get; set; }
            public string Password { get; set; }
            public string Security { get; set; }
            public string DOB { get; set; }
            public string UserID { get; set; }

        }
        //---------------------------------------      manage dealer district relation --------------------------------

        // insert 
        [WebMethod]
        public string InsertDealerDistrictRelation(DealerDistrictRelationData data)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_DistrictDistributor obj = new tb_DistrictDistributor();

           int  DistrictID = Convert.ToInt32(data.DistrictID);
            int dealerID = Convert.ToInt32(data.dealerID);
            int ID = Convert.ToInt32(( data.ID != "0" && !string.IsNullOrEmpty(data.ID)) ? data.ID.ToString() : "0");
            var objCheck = db.tb_DistrictDistributor.FirstOrDefault(u => u.VendorID == dealerID && u.DistrictID == DistrictID && u.ID != ID && u.Status == "Active");
            if (objCheck != null)
            {
                return "Exits";
            }
            if (ID == 0)
            {
                obj.CreatedBy = Convert.ToInt32(data.UserID);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedTime = DateTime.Now.TimeOfDay;
                db.tb_DistrictDistributor.Add(obj);
            }
            else
            {
                obj = db.tb_DistrictDistributor.FirstOrDefault(s => s.ID == ID);
                obj.ModifyBy = Convert.ToInt32(data.UserID);
                obj.ModifyDate = DateTime.Now;
                obj.ModifyTime = DateTime.Now.TimeOfDay;
            }

            obj.Status = "Active";
            obj.VendorID = Convert.ToInt32(data.dealerID);
            obj.DistrictID = Convert.ToInt32(data.DistrictID);
            obj.DelieveryDayFrom = data.From;
            obj.DelieveryDayTo = data.To;
            db.SaveChanges();
            if (ID == 0)
            {
                return "Inserted";
            }
            else
            {
                return "Updated";
            }
        }
        public class DealerDistrictRelationData
        {
            public string ID { get; set; }
            public string dealerID { get; set; }
            public string State{ get; set; }
            public string DistrictID { get; set; }
            public string From { get; set; }
            public string To { get; set; }
            public string UserID { get; set; }

        }
        [WebMethod]
        public string BindDistrictinDealerDistrict( String state)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            int state1 = Convert.ToInt32(state);
            
                obj = from a in db.tb_District
                      where a.Status == "Active" && a.StateID == state1
                      orderby a.ID descending
                      select new
                      {
                          a.ID,
                          a.DistrictName,

                      };
            
           
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string jason = serial.Serialize(obj);
            return jason;
        }

        [WebMethod]
        public string BindDealerDistrictRelation(string dealerID)
        {
            int DealerID = Convert.ToInt32(dealerID);
            PragticrmDBEntities db = new PragticrmDBEntities();
            IQueryable<object> obj = null;
            obj = from a in db.tb_DistrictDistributor
                  where a.Status == "Active" && a.VendorID== DealerID
                  orderby a.ID descending
                  select new
                  {
                      a.ID,
                      a.tb_District.DistrictName,
                      a.DelieveryDayFrom,
                      a.DelieveryDayTo,
                      a.Status
                  };
            JavaScriptSerializer serial = new JavaScriptSerializer();
            string jason = serial.Serialize(obj);
            return jason;
        }



        [WebMethod]
        public string DealerDistrictRelationAction(string actionLabel, int editID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            var obj = db.tb_DistrictDistributor.FirstOrDefault(s => s.ID == editID);
            if (actionLabel == "Edit")
            {
                if (obj != null)
                {
                    DealerDistrictRelationData data = new DealerDistrictRelationData
                    {
                        ID = obj.ID.ToString(),
                        dealerID = obj.tb_Vendor.VendorName,
                        State=obj.tb_District.tb_State.ID.ToString(),
                        DistrictID=obj.tb_District.ID.ToString(),
                        From=obj.DelieveryDayFrom,
                        To=obj.DelieveryDayTo
                    };

                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    string json = serial.Serialize(data);
                    return json;
                }
            }
            else if (actionLabel == "Delete")
            {
                if (obj != null)
                {
                    if (obj.Status == "Active")
                    {
                        obj.Status = "InActive";
                        db.SaveChanges();
                    }
                    else
                    {
                        obj.Status = "Active";
                        db.SaveChanges();
                    }
                }
                return "Deleted Successfully";
            }

            return string.Empty;
        }
        //===================   Manage dealer district relation End  =======================


        //---------------------------------------      manage vendor range commission --------------------------------

        // insert 
        [WebMethod]
        public string InsertVendorRangeCommission(VendorRangeCommissionData data)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            tb_RelationRangeVendor obj = new tb_RelationRangeVendor();

            int dealerID = Convert.ToInt32(data.VendorID);
            int Range = Convert.ToInt32(data.Range);
            decimal Amount = Convert.ToDecimal(data.Amount);
            int ID = Convert.ToInt32((data.ID != "0" && !string.IsNullOrEmpty(data.ID)) ? data.ID.ToString() : "0");
            var objCheck = db.tb_RelationRangeVendor.FirstOrDefault(u => u.VendorID == dealerID && u.RangeID == Range &&  u.Rate==Amount &&  u.ID != ID && u.Status == "Active");
            if (objCheck != null)
            {
                return "Exits";
            }
            if (ID == 0)
            {
                obj.CreatedBy = Convert.ToInt32(data.UserID);
                obj.CreatedDate = DateTime.Now;
                obj.CreatedTime = DateTime.Now.TimeOfDay;
                db.tb_RelationRangeVendor.Add(obj);
            }
            else
            {
                obj = db.tb_RelationRangeVendor.FirstOrDefault(s => s.ID == ID);
                obj.ModifyBy = Convert.ToInt32(data.UserID);
                obj.ModifyDate = DateTime.Now;
                obj.ModifyTime = DateTime.Now.TimeOfDay;
            }

            obj.Status = "Active";
            obj.VendorID = Convert.ToInt32(data.VendorID);
            obj.RangeID = Convert.ToInt32(data.Range);
            obj.Rate =Convert.ToDecimal( data.Amount);
            db.SaveChanges();
            if (ID == 0)
            {
                return "Inserted";
            }
            else
            {
                return "Updated";
            }
        }
        public class VendorRangeCommissionData
        {
            public string ID { get; set; }
            public string VendorID { get; set; }
            public string Range { get; set; }
            public string Amount { get; set; }            
            public string UserID { get; set; }

        }
        [WebMethod]
        public string BindVendorRangeCommission(string vendorid)
        {
            int DealerID = Convert.ToInt32(vendorid);
            PragticrmDBEntities db = new PragticrmDBEntities();

            IQueryable<object> obj = null;
            obj = from a in db.tb_RelationRangeVendor
                  where a.Status == "Active" && a.VendorID == DealerID
                  orderby a.ID descending
                  select new
                  {
                      a.ID,
                      Vendor = a.tb_Vendor.VendorName,                      
                      minRange = a.tb_RangeMaster.RangeMin ,
                      maxRange=a.tb_RangeMaster.RangeMax ,
                      a.Rate,
                      a.Status
                  };

            JavaScriptSerializer serial = new JavaScriptSerializer();
            string jason = serial.Serialize(obj);
            return jason;
        }

        [WebMethod]
        public string VendorRangeCommissionAction(string actionLabel, int editID)
        {
            PragticrmDBEntities db = new PragticrmDBEntities();
            var obj = db.tb_RelationRangeVendor.FirstOrDefault(s => s.ID == editID);
            if (actionLabel == "Edit")
            {
                if (obj != null)
                {
                    VendorRangeCommissionData data = new VendorRangeCommissionData
                    {
                        ID = obj.ID.ToString(),

                        VendorID = obj.VendorID.ToString(),
                        Range = obj.RangeID.ToString(),
                        Amount = obj.Rate.ToString()
                        
                    };

                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    string json = serial.Serialize(data);
                    return json;
                }
            }
            else if (actionLabel == "Delete")
            {
                if (obj != null)
                {
                    if (obj.Status == "Active")
                    {
                        obj.Status = "InActive";
                        db.SaveChanges();
                    }
                    else
                    {
                        obj.Status = "Active";
                        db.SaveChanges();
                    }
                }
                return "Deleted Successfully";
            }

            return string.Empty;
        }
        //===================   Manage vendor range commission End  =======================




    }
}
