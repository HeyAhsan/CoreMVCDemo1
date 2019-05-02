using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreMVCDemo.Models;

namespace CoreMVCDemo.Controllers
{

    //public class ComViewModel
    //{
    //    public string CommentContent { get; set; }
    //    public int CommentID { get; set; }
    //    public int SpamCount { get; set; }
    //}

    public class ModuleController : Controller
    {

        //    [HttpPost]
        //    public JsonResult GetMoreComments(string IDProduct, string NameProduct)
        //    {
        //        ComViewModel com = new ComViewModel
        //        {
        //            CommentContent = NameProduct,
        //            CommentID = 99,
        //            SpamCount = 22
        //        };
        //        return Json(com);
        //    }

        [HttpPost]
        public JsonResult GetProducts(string ProductID)
        {
            CoreMVCClassLibrary.DataLayer objDataLayer = new CoreMVCClassLibrary.DataLayer();



            ModuleModels moduleModels = new ModuleModels();


            var objProducts = moduleModels.GetProducts();

            if (!string.IsNullOrEmpty(ProductID)) {
                objProducts = objProducts.Where(item => item.ProductId == Convert.ToInt16(ProductID)).ToList();
            }
            

        return Json(objProducts);
    }

    public IActionResult Index()
        {
            return View();
        }

        public IActionResult Source1()
        {
            return View();
        }

        public IActionResult Source2()
        {
            return View();
        }
    }
}