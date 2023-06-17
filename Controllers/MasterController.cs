using ServiceCenter.Models;
using ServiceCenter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServiceCenter.Controllers
{
    public class MasterController : Controller
    {

        #region Area

        public ActionResult AreaList()
        {
            if (!IsSessionValid())
                return RedirectToAction("Logout", "Login");

            if (!CommonService.CheckForRightsByPageNameAndUserId("ARAALL", PageRightsEnum.List))
                return RedirectToAction("AccessDenied", "Home");

            return View();
        }

        [HttpPost]
        public JsonResult GetAreaList()
        {
            try
            {
                // Initialization.
                var draw = Request.Form.GetValues("draw")?[0];
                var order = Convert.ToInt32(Request.Form.GetValues("order[0][column]")?[0]);
                var orderDir = Request.Form.GetValues("order[0][dir]")?[0];
                var startRec = Convert.ToInt32(Request.Form.GetValues("start")?[0]);
                var pageSize = Convert.ToInt32(Request.Form.GetValues("length")?[0]);

                string AreaName = string.Empty, AreaPincode = string.Empty;

                if (!string.IsNullOrEmpty(Request.Form["AreaName"]))
                    AreaName = Convert.ToString(Request.Form["AreaName"]).Trim();

                if (!string.IsNullOrEmpty(Request.Form["AreaPincode"]))
                    AreaPincode = Convert.ToString(Request.Form["AreaPincode"]).Trim();

                AreaMasterListDataModel objAreaMasterListDataModel = new AreaMasterListDataModel();

                MasterService objJobService = new MasterService();
                objAreaMasterListDataModel = objJobService.GetAreaList(order, orderDir.ToUpper(), startRec, pageSize, AreaName, AreaPincode);


                return Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = objAreaMasterListDataModel.RecordCount,
                    recordsFiltered = objAreaMasterListDataModel.RecordCount,
                    data = objAreaMasterListDataModel.AreaMasterList
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new
                {
                    draw = Convert.ToInt32(0),
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = string.Empty
                });
            }
        }


        public ActionResult AreaForm()
        {
            if (!IsSessionValid())
                return RedirectToAction("Logout", "Login");

            if (!CommonService.CheckForRightsByPageNameAndUserId("ARAALL", PageRightsEnum.Add))
                return RedirectToAction("AccessDenied", "Home");


            AreaMaster objAreaMaster = new AreaMaster(); ;

            string AreaId = Request["AreaId"];

            if (!string.IsNullOrEmpty(AreaId))
            {
                MasterService obMasterService = new MasterService();
                objAreaMaster = obMasterService.GetAreaDetails(AreaId);
            }


            return View(objAreaMaster);
        }

        [HttpPost]
        public JsonResult AreaForm(AreaMaster objAreaMaster)
        {
            ResponceModel objResponceModel = new ResponceModel();

            MasterService obMasterService = new MasterService();
            objResponceModel = obMasterService.InsertUpdateArea(objAreaMaster);

            return Json(new { data = objResponceModel });
        }

        [HttpPost]
        public JsonResult DeleteAreaById(string AreaId)
        {
            ResponceModel objResponceModel = new ResponceModel();

            MasterService obMasterService = new MasterService();
            objResponceModel = obMasterService.DeleteAreaById(AreaId);

            return Json(new { data = objResponceModel });
        }



        #endregion

        #region Item

        public ActionResult ItemList()
        {
            if (!IsSessionValid())
                return RedirectToAction("Logout", "Login");

            if (!CommonService.CheckForRightsByPageNameAndUserId("ITMALL", PageRightsEnum.List))
                return RedirectToAction("AccessDenied", "Home");

            return View();
        }

        [HttpPost]
        public JsonResult GetItemList()
        {
            try
            {
                // Initialization.
                var draw = Request.Form.GetValues("draw")?[0];
                var order = Convert.ToInt32(Request.Form.GetValues("order[0][column]")?[0]);
                var orderDir = Request.Form.GetValues("order[0][dir]")?[0];
                var startRec = Convert.ToInt32(Request.Form.GetValues("start")?[0]);
                var pageSize = Convert.ToInt32(Request.Form.GetValues("length")?[0]);

                string ItemName = string.Empty, ItemKeyword = string.Empty;

                if (!string.IsNullOrEmpty(Request.Form["ItemName"]))
                    ItemName = Convert.ToString(Request.Form["ItemName"]).Trim();

                if (!string.IsNullOrEmpty(Request.Form["ItemKeyword"]))
                    ItemKeyword = Convert.ToString(Request.Form["ItemKeyword"]).Trim();


                ItemMasterListDataModel objItemMasterListDataModel = new ItemMasterListDataModel();

                MasterService objJobService = new MasterService();
                objItemMasterListDataModel = objJobService.GetItemList(order, orderDir.ToUpper(), startRec, pageSize, ItemName, ItemKeyword);


                return Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = objItemMasterListDataModel.RecordCount,
                    recordsFiltered = objItemMasterListDataModel.RecordCount,
                    data = objItemMasterListDataModel.ItemMasterList
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new
                {
                    draw = Convert.ToInt32(0),
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = string.Empty
                });
            }
        }

        public ActionResult ItemForm()
        {
            if (!IsSessionValid())
                return RedirectToAction("Logout", "Login");

            if (!CommonService.CheckForRightsByPageNameAndUserId("ITMALL", PageRightsEnum.Add))
                return RedirectToAction("AccessDenied", "Home");

            ItemMaster objItemMaster = new ItemMaster();

            string ItemId = Request["ItemId"];

            if (!string.IsNullOrEmpty(ItemId))
            {
                MasterService obMasterService = new MasterService();
                objItemMaster = obMasterService.GetItemDetails(ItemId);
            }


            return View(objItemMaster);
        }

        [HttpPost]
        public JsonResult ItemForm(ItemMaster objItemMaster)
        {
            ResponceModel objResponceModel = new ResponceModel();

            MasterService obMasterService = new MasterService();
            objResponceModel = obMasterService.InsertUpdateItem(objItemMaster);

            return Json(new { data = objResponceModel });
        }

        [HttpPost]
        public JsonResult DeleteItemById(string ItemId)
        {
            ResponceModel objResponceModel = new ResponceModel();

            MasterService obMasterService = new MasterService();
            objResponceModel = obMasterService.DeleteItemById(ItemId);

            return Json(new { data = objResponceModel });
        }



        #endregion

        #region Technician

        public ActionResult Technician()
        {
            if (!IsSessionValid())
                return RedirectToAction("Logout", "Login");

            if (!CommonService.CheckForRightsByPageNameAndUserId("TECALL", PageRightsEnum.List))
                return RedirectToAction("AccessDenied", "Home");

            return View();
        }

        [HttpPost]
        public JsonResult GetTechnicianList()
        {
            try
            {
                var draw = Convert.ToInt32(Request.Form["draw"]);
                var order = Convert.ToInt32(Request.Form["order[0][column]"]);
                var orderDir = Request.Form["order[0][dir]"];
                var startRec = Convert.ToInt32(Request.Form["start"]);
                var pageSize = Convert.ToInt32(Request.Form["length"]);

                string MobileNo = string.Empty, TechnicianName = String.Empty;

                if (!string.IsNullOrEmpty(Request.Form["MobileNo"]))
                    MobileNo = Convert.ToString(Request.Form["MobileNo"]).Trim();

                if (!string.IsNullOrEmpty(Request.Form["TechnicianName"]))
                    TechnicianName = Convert.ToString(Request.Form["TechnicianName"]).Trim();

                TechnicianListDataModel objTechnicianListDataModel = new TechnicianListDataModel();

                MasterService objMasterService = new MasterService();
                objTechnicianListDataModel = objMasterService.GetTechnicianList(order, orderDir.ToUpper(), startRec, pageSize, TechnicianName, MobileNo);


                return Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = objTechnicianListDataModel.RecordCount,
                    recordsFiltered = objTechnicianListDataModel.RecordCount,
                    data = objTechnicianListDataModel.TechnicianList
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new
                {
                    draw = Convert.ToInt32(0),
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = string.Empty
                });
            }

        }

        public ActionResult TechnicianAddEdit()
        {
            if (!IsSessionValid())
                return RedirectToAction("Logout", "Login");

            if (!CommonService.CheckForRightsByPageNameAndUserId("TECALL", PageRightsEnum.Add))
                return RedirectToAction("AccessDenied", "Home");

            string TechnicianId = Request["TechnicianId"];

            TechnicianMaster objTechnicianMaster;

            if (!string.IsNullOrEmpty(TechnicianId))
            {

                objTechnicianMaster = new TechnicianMaster();

                MasterService objMasterService = new MasterService();
                objTechnicianMaster = objMasterService.GetTechnicianDetailById(TechnicianId);

                string TechTypeId = objTechnicianMaster.TechType;

                objTechnicianMaster.TechnicianTypeDD = TechnicianTypeDD(TechTypeId);


            }
            else
            {
                objTechnicianMaster = new TechnicianMaster();
                objTechnicianMaster.TechnicianTypeDD = TechnicianTypeDD();
            }

            return View(objTechnicianMaster);

        }

        [HttpPost]
        public ActionResult TechnicianAddEdit(TechnicianMaster objTechnicianMaster)
        {
            string ErrorMsg = string.Empty;

            if (!ModelState.IsValid)
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));

                ErrorMsg = message;

            }

            MasterService objMasterService = new MasterService();
            objTechnicianMaster = objMasterService.AdUpdateTechnicianDetail(objTechnicianMaster);

            if (objTechnicianMaster.Responce == false)
            {
                objTechnicianMaster.TechnicianTypeDD = TechnicianTypeDD();

                ViewBag.ErrorMessage = objTechnicianMaster.Message;

                return View(objTechnicianMaster);
            }
            else
            {
                return RedirectToAction("Technician", "Master");
            }


        }

        public SelectList TechnicianTypeDD(string SelectedValue = "")
        {
            List<SelectListItem> ListItem = new List<SelectListItem>();

            MasterService objMasterService = new MasterService();
            ListItem = objMasterService.GetTechnicianTypeList(SelectedValue);

            return new SelectList(ListItem, "Value", "Text");
        }

        [HttpPost]
        public JsonResult UpdateTechnicianStatusById(string TechnicianId, bool Status)
        {
            ResponceModel objResponceModel = new ResponceModel();

            MasterService objMasterService = new MasterService();
            objResponceModel = objMasterService.UpdateTechnicianStatusById(TechnicianId, Status);

            return Json(new { data = objResponceModel });
        }


        public ActionResult TechnicianAttendance()
        {
            //if (!IsSessionValid())
              //  return RedirectToAction("Logout", "Login");

            return View();
        }

        [HttpPost]
        public JsonResult GetTechnicianAttendanceList()
        {
            try
            {
                var draw = Convert.ToInt32(Request.Form["draw"]);
                var order = Convert.ToInt32(Request.Form["order[0][column]"]);
                var orderDir = Request.Form["order[0][dir]"];
                var startRec = Convert.ToInt32(Request.Form["start"]);
                var pageSize = Convert.ToInt32(Request.Form["length"]);

                string TechnicianName = String.Empty;


                if (!string.IsNullOrEmpty(Request.Form["TechnicianName"]))
                    TechnicianName = Convert.ToString(Request.Form["TechnicianName"]).Trim();

                DateTime? FromDate, ToDate;

                FromDate = !string.IsNullOrEmpty(Request.Form["FromDate"]) ? DateTime.ParseExact(Request.Form["FromDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

                ToDate = !string.IsNullOrEmpty(Request.Form["ToDate"]) ? DateTime.ParseExact(Request.Form["ToDate"], "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : (DateTime?)null;

                TechnicianAttendanceListDataModel objTechnicianAttendanceListDataModel = new TechnicianAttendanceListDataModel();

                MasterService objMasterService = new MasterService();
                objTechnicianAttendanceListDataModel = objMasterService.TechnicianAttendanceList(order, orderDir.ToUpper(), startRec, pageSize, TechnicianName, FromDate, ToDate);


                return Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = objTechnicianAttendanceListDataModel.RecordCount,
                    recordsFiltered = objTechnicianAttendanceListDataModel.RecordCount,
                    data = objTechnicianAttendanceListDataModel.TechnicianAttendanceList
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new
                {
                    draw = Convert.ToInt32(0),
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = string.Empty
                });
            }

        }

        public JsonResult GetTechnicianNameList(string match, int page = 1, int pageSize = 5)
        {
            List<Select2> lstTechnician = new List<Select2>();

            MasterService objMasterService = new MasterService();
            lstTechnician = objMasterService.GetTechnicianNameList(match);

            ResultList<Select2> results = new ResultList<Select2>
            {
                items = lstTechnician,
                total_count = lstTechnician.Count,
            };

            return Json(results, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Common Method

        public bool IsSessionValid()
        {
            if (Session["User"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Ip Address

        public ActionResult IPAddressList()
        {
            if (!IsSessionValid())
                return RedirectToAction("Logout", "Login");

            if (!CommonService.CheckForRightsByPageNameAndUserId("IPAALL", PageRightsEnum.List))
                return RedirectToAction("AccessDenied", "Home");

            return View();
        }

        [HttpPost]
        public JsonResult GetIPAddressList()
        {
            try
            {
                // Initialization.
                var draw = Request.Form.GetValues("draw")?[0];
                var order = Convert.ToInt32(Request.Form.GetValues("order[0][column]")?[0]);
                var orderDir = Request.Form.GetValues("order[0][dir]")?[0];
                var startRec = Convert.ToInt32(Request.Form.GetValues("start")?[0]);
                var pageSize = Convert.ToInt32(Request.Form.GetValues("length")?[0]);

                string IPAddress = string.Empty;

                if (!string.IsNullOrEmpty(Request.Form["IPAddress"]))
                    IPAddress = Convert.ToString(Request.Form["IPAddress"]).Trim();


                IPAddressMasterListDataModel objIPAddressMasterListDataModel = new IPAddressMasterListDataModel();

                MasterService objJobService = new MasterService();
                objIPAddressMasterListDataModel = objJobService.GetIPAddressList(order, orderDir.ToUpper(), startRec, pageSize, IPAddress);


                return Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = objIPAddressMasterListDataModel.RecordCount,
                    recordsFiltered = objIPAddressMasterListDataModel.RecordCount,
                    data = objIPAddressMasterListDataModel.IPAddressMasterList
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new
                {
                    draw = Convert.ToInt32(0),
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = string.Empty
                });
            }
        }


        public ActionResult IPAddressForm()
        {
            if (!IsSessionValid())
                return RedirectToAction("Logout", "Login");

            if (!CommonService.CheckForRightsByPageNameAndUserId("IPAALL", PageRightsEnum.Add))
                return RedirectToAction("AccessDenied", "Home");

            clsIPAddress objIPAddress = new clsIPAddress(); ;

            string IPAddressId = Request["IPAddressId"];

            if (!string.IsNullOrEmpty(IPAddressId))
            {
                MasterService obMasterService = new MasterService();
                objIPAddress = obMasterService.GetIPAddressDetails(IPAddressId);
            }

            return View(objIPAddress);
        }

        [HttpPost]
        public JsonResult IPAddressForm(clsIPAddress objIPAddress)
        {
            ResponceModel objResponceModel = new ResponceModel();

            MasterService obMasterService = new MasterService();

            objResponceModel = obMasterService.InsertUpdateIPAddress(objIPAddress.Id, objIPAddress.IP);

            return Json(new { data = objResponceModel });
        }

        [HttpPost]
        public JsonResult DeleteIPAddressById(int IPAddressId)
        {
            ResponceModel objResponceModel = new ResponceModel();

            MasterService obMasterService = new MasterService();
            objResponceModel = obMasterService.DeleteIPAddressById(IPAddressId);

            return Json(new { data = objResponceModel });
        }

        #endregion

        #region Part

        public ActionResult PartList()
        {
            if (!IsSessionValid())
                return RedirectToAction("Logout", "Login");

            if (!CommonService.CheckForRightsByPageNameAndUserId("ITMALL", PageRightsEnum.List))
                return RedirectToAction("AccessDenied", "Home");

            return View();
        }

        [HttpPost]
        public JsonResult GetPartList()
        {
            try
            {
                // Initialization.
                var draw = Request.Form.GetValues("draw")?[0];
                var order = Convert.ToInt32(Request.Form.GetValues("order[0][column]")?[0]);
                var orderDir = Request.Form.GetValues("order[0][dir]")?[0];
                var startRec = Convert.ToInt32(Request.Form.GetValues("start")?[0]);
                var pageSize = Convert.ToInt32(Request.Form.GetValues("length")?[0]);

                string PartName = string.Empty, PartKeyword = string.Empty;

                if (!string.IsNullOrEmpty(Request.Form["PartName"]))
                    PartName = Convert.ToString(Request.Form["PartName"]).Trim();

                if (!string.IsNullOrEmpty(Request.Form["PartKeyword"]))
                    PartKeyword = Convert.ToString(Request.Form["PartKeyword"]).Trim();


                PartMasterListDataModel objPartMasterListDataModel = new PartMasterListDataModel();

                MasterService objJobService = new MasterService();
                objPartMasterListDataModel = objJobService.GetPartList(order, orderDir.ToUpper(), startRec, pageSize, PartName, PartKeyword);


                return Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = objPartMasterListDataModel.RecordCount,
                    recordsFiltered = objPartMasterListDataModel.RecordCount,
                    data = objPartMasterListDataModel.PartMasterList
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new
                {
                    draw = Convert.ToInt32(0),
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = string.Empty
                });
            }
        }

        public ActionResult PartForm()
        {
            if (!IsSessionValid())
                return RedirectToAction("Logout", "Login");

            if (!CommonService.CheckForRightsByPageNameAndUserId("ITMALL", PageRightsEnum.Add))
                return RedirectToAction("AccessDenied", "Home");

            PartMaster objPartMaster = new PartMaster();

            string PartId = Request["PartId"];

            if (!string.IsNullOrEmpty(PartId))
            {
                MasterService obMasterService = new MasterService();
                objPartMaster = obMasterService.GetPartDetails(PartId);
            }


            return View(objPartMaster);
        }

        [HttpPost]
        public JsonResult PartForm(PartMaster objPartMaster)
        {
            ResponceModel objResponceModel = new ResponceModel();

            MasterService obMasterService = new MasterService();
            objResponceModel = obMasterService.InsertUpdatePart(objPartMaster);

            return Json(new { data = objResponceModel });
        }

        [HttpPost]
        public JsonResult DeletePartById(string PartId)
        {
            ResponceModel objResponceModel = new ResponceModel();

            MasterService obMasterService = new MasterService();
            objResponceModel = obMasterService.DeletePartById(PartId);

            return Json(new { data = objResponceModel });
        }

        [HttpPost]
        public JsonResult UpdatePartStatusById(string PartId, bool Status)
        {
            ResponceModel objResponceModel = new ResponceModel();

            MasterService objMasterService = new MasterService();
            objResponceModel = objMasterService.UpdatePartStatusById(PartId, Status);

            return Json(new { data = objResponceModel });
        }

        #endregion


        #region Test Print PDF 

        public ActionResult UserDetail()
        {
            return View();
        }

        public ActionResult PrintUserDetail()
        {
            //var report = new Rotativa.ActionAsPdf("UserDetail");
            //return report;

            return new Rotativa.ActionAsPdf("UserDetail")
            {
                FileName = "PDF_Doc.pdf"
            };
        }

        #endregion
    }
}