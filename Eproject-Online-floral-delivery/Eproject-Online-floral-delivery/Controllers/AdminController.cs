using Eproject_Online_floral_delivery.common.utils;
using Eproject_Online_floral_delivery.DAL;
using Eproject_Online_floral_delivery.Models;
using Eproject_Online_floral_delivery.Repository;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Eproject_Online_floral_delivery.Controllers
{
    public class AdminController : Controller
    {
        private GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        private Eproject_FloralEntities DbEntities = new Eproject_FloralEntities();
        
        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Categories()
        {
            List<tbl_category> allCategories =
                _unitOfWork.GetRepositoryInstance<tbl_category>()
                .GetAllRecordsIQueryable()
                .Where(x => x.isDelete == false).ToList();
            return View(allCategories);
        }
        //GET: Category / getAll / ajax
        [HttpGet]
        public JsonResult CategoryAll()
        {
            List<tbl_category> allCategories =
                _unitOfWork.GetRepositoryInstance<tbl_category>()
                .GetAllRecordsIQueryable()
                .Where(x => x.isDelete == false).ToList();
            return Json(allCategories, JsonRequestBehavior.AllowGet);
        }
        //POST: Category/ ADD Category
        [HttpPost]
        public JsonResult AddCategory(tbl_category Model)
        {
            //Remove utf-8 of category name if any
            string nameNotUTF8 = ConvertStringNoUtf8.Convert_Unsigned_string(Model.name);
            //mapping from json to c# class
            tbl_category categoryModel = new tbl_category
            {
                name = Model.name,
                //fix every space in the string to a "-" sign
                code = nameNotUTF8.Replace(" ", "-"),
                parentID = 1,
                isActive = Model.isActive,
                isDelete = false
            };

            //Looking to see if the directory is already in the database?
            tbl_category checkCategoryEmpty = DbEntities.tbl_category
                .Where(x => x.name.ToLower()
                .Equals(categoryModel.name.ToLower()))
                .FirstOrDefault();
            //If not yet proceed to add new and return json of data otherwise return null
            if (checkCategoryEmpty == null)
            {
                _unitOfWork.GetRepositoryInstance<tbl_category>().Add(categoryModel);
                return Json(categoryModel, JsonRequestBehavior.AllowGet);
            }
            ErrorMessage errorMessage = new ErrorMessage
            {
                name = "warning",
                messageError = "The category you want to add already exists!"
            };
            return Json(errorMessage, JsonRequestBehavior.AllowGet);
        }

        //POST / Category - ajax json
        [HttpPost]
        public JsonResult editCategory(tbl_category model)
        {
            
            //Remove utf-8 of category name if any
            string nameNotUTF8 = ConvertStringNoUtf8.Convert_Unsigned_string(model.name);
            //mapping from json to c# class
            tbl_category CategoryModel = new tbl_category
            {
                categoryID = model.categoryID,
                name = model.name,
                code = model.name.Replace(" ","-"),
                parentID = 1,
                isActive = model.isActive,
                isDelete = false
            };
            //Looking to see if the directory is already in the database?
            var result = DbEntities.tbl_category.Count(x => x.name.ToLower().Equals(CategoryModel.name.ToLower()));
            //If not yet proceed to add new and return json of data otherwise return null
            if (result < 2)
            {
                _unitOfWork.GetRepositoryInstance<tbl_category>().Update(CategoryModel);
                return Json(CategoryModel, JsonRequestBehavior.AllowGet);
            }
            ErrorMessage errorMessage = new ErrorMessage
            {
                name = "Warning",
                messageError = "The category name you want to edit already exists!"
            };
            return Json(errorMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult removeCategory(tbl_category model)
        {
            tbl_category categoryModel = new tbl_category
            {
                categoryID = model.categoryID
            };
            _unitOfWork.GetRepositoryInstance<tbl_category>().Remove(categoryModel);
            return Json(categoryModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Product()
        {
            List<tbl_product> allProducts =
                _unitOfWork.GetRepositoryInstance<tbl_product>()
                .GetAllRecordsIQueryable()
                .Where(x => x.isActive == false).ToList();
            return View(allProducts);
        }

        //GET: find all product / ajax - json
        [HttpGet]
        public JsonResult allProduct()
        {
            List<tbl_product> allProducts = 
                _unitOfWork.GetRepositoryInstance<tbl_product>()
                .GetAllRecordsIQueryable()
                .Where(x => x.isActive == false).ToList();
            return Json(allProducts, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult addProduct()
        {
            List<tbl_category> getAllCategory =
                _unitOfWork.GetRepositoryInstance<tbl_category>()
                .GetAllRecordsIQueryable().Where(x => x.isDelete == false).ToList();
            return View(getAllCategory);
        }
        //POST: add product / ajax - json
        [HttpPost]
        public JsonResult addProduct(tbl_product model)
        {
            tbl_product productModel = new tbl_product
            {
                name = model.name,
                price = model.price,
                priceSale = model.priceSale,
                dayStartSale = model.dayStartSale,
                dayEndSale = model.dayEndSale,
                isActive = model.isActive,
                isFeatured = model.isFeatured,
                createdDate = model.createdDate,
                modifiedDate = model.modifiedDate,
                quantity = model.quantity,
                metaTitle = model.metaTitle,
                description = model.description,
                metaKeyword = model.metaKeyword,
                countView = model.countView,
                categoryID = model.categoryID
            };
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}