using Sample.Models;
using Sample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AjaxOption.Samples.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            JsonFileProductService db = new JsonFileProductService();
            var objProducts = db.GetProducts();
            List<string> ProductMakerList = db.GetProducts().Select(i => i.Maker).Distinct<string>().ToList();
            ViewData["ProductMakerList"] = ProductMakerList.Select(i => new SelectListItem() { Value = i, Text = i }).ToList();
                //.ToList<string>().Select(i => new SelectListItem() { Value = i.ToString(), Text = i }).Distinct<string>();
            return View();
        }

       
        [HttpGet]
        public PartialViewResult AllProducts()
        {
            JsonFileProductService db = new JsonFileProductService();
            var objProducts = db.GetProducts();
            return PartialView("_AllProducts", objProducts);

        }
        [HttpGet]
        public ActionResult SearchedProducts(string SearchTerm)
        {
            if (SearchTerm != null)
            {
                JsonFileProductService db = new JsonFileProductService();
                var objFilteredProducts = db.GetProducts().Where(x => x.Maker.Contains(SearchTerm)).ToList();
                if(objFilteredProducts.Any())
                    return PartialView("_AllProducts", objFilteredProducts.ToList());
                return PartialView("_AllProducts", new List<Product>());
            }
            return View();
        } 
        [HttpGet]
        public ActionResult SearchedProductsDropDown(string SearchTermInfo)
        {
            //string SearchTermInfo = "";
            if (SearchTermInfo != null)
            {
                JsonFileProductService db = new JsonFileProductService();
                var objFilteredProducts = db.GetProducts().Where(x => x.Maker.Contains(SearchTermInfo)).ToList();
                if(objFilteredProducts.Any())
                    return PartialView("_AllProducts", objFilteredProducts.ToList());
                return PartialView("_AllProducts", new List<Product>());
            }
            return View();
        }
    }
}