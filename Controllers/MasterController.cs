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
            /*if (!IsSessionValid())
                return RedirectToAction("Index", "Login");
            */

            return View();
        }

        [HttpPost]
        public ActionResult GetAreaList()
        {
            JsonResult result;
            try
            {
                // Initialization.
                var search = Request.Form.GetValues("search[value]")?[0];
                var draw = Request.Form.GetValues("draw")?[0];
                var order = Convert.ToInt32(Request.Form.GetValues("order[0][column]")?[0]);
                var orderDir = Request.Form.GetValues("order[0][dir]")?[0];
                var startRec = Convert.ToInt32(Request.Form.GetValues("start")?[0]);
                var pageSize = Convert.ToInt32(Request.Form.GetValues("length")?[0]);


                AreaMasterListDataModel objAreaMasterListDataModel = new AreaMasterListDataModel();

                MasterService objJobService = new MasterService();
                objAreaMasterListDataModel = objJobService.GetAreaList(order, orderDir.ToUpper(), startRec, pageSize, search);


                result = Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = objAreaMasterListDataModel.RecordCount,
                    recordsFiltered = objAreaMasterListDataModel.AreaMasterList.Count,
                    data = objAreaMasterListDataModel.AreaMasterList
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return result;
        }


        public ActionResult AreaForm()
        {
            AreaMaster objAreaMaster = new AreaMaster(); ;

            string AreaId = Request["AreaId"];

            if(!string.IsNullOrEmpty(AreaId))
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
            objResponceModel = obMasterService.InsertUpdateArea(objAreaMaster.AreaId, objAreaMaster.AreaName);

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
            /*if (!IsSessionValid())
                return RedirectToAction("Index", "Login");
            */

            return View();
        }

        [HttpPost]
        public ActionResult GetItemList()
        {
            JsonResult result;
            try
            {
                // Initialization.
                var search = Request.Form.GetValues("search[value]")?[0];
                var draw = Request.Form.GetValues("draw")?[0];
                var order = Convert.ToInt32(Request.Form.GetValues("order[0][column]")?[0]);
                var orderDir = Request.Form.GetValues("order[0][dir]")?[0];
                var startRec = Convert.ToInt32(Request.Form.GetValues("start")?[0]);
                var pageSize = Convert.ToInt32(Request.Form.GetValues("length")?[0]);


                ItemMasterListDataModel objItemMasterListDataModel = new ItemMasterListDataModel();

                MasterService objJobService = new MasterService();
                objItemMasterListDataModel = objJobService.GetItemList(order, orderDir.ToUpper(), startRec, pageSize, search);


                result = Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = objItemMasterListDataModel.RecordCount,
                    recordsFiltered = objItemMasterListDataModel.ItemMasterList.Count,
                    data = objItemMasterListDataModel.ItemMasterList
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return result;
        }


        public ActionResult ItemForm()
        {
            ItemMaster objItemMaster = new ItemMaster(); ;

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
            objResponceModel = obMasterService.InsertUpdateItem(objItemMaster.ItemId, objItemMaster.ItemName);

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

        public ActionResult AddTechnician()
        {
            return View();
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