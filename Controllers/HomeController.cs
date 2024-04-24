using Microsoft.AspNetCore.Mvc;
using pruebaConecta.Models;
using System.Diagnostics;
using pruebaConecta.Services;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.Runtime.InteropServices;

namespace pruebaConecta.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicesApi _servicesApi;

        public HomeController(IServicesApi servicesApi)
        {
            _servicesApi = servicesApi;
        }

        public async Task<IActionResult> Index(PropertySearch propertySearch, int pageNumber = 1)
        {
            List<Region> regions = await _servicesApi.getAllRegions();
            List<Comuna> comuna = await _servicesApi.getAllComuna();
            List<PropertyType> propertyTypes = await _servicesApi.getAllPropertyType();
            ViewBag.PropertyType = propertyTypes;
            ViewBag.Comuna = comuna;
            ViewBag.Regions = regions;
            ViewBag.CurrentPage = pageNumber;
            if (propertySearch.idType == 0 &&  propertySearch.idBorough == 0)
            {
                if (pageNumber == 1)
                {
                    ApiResult apiResult = await _servicesApi.GetAllResult(propertySearch);
                    var property = apiResult.items;
                    var pagination = apiResult.pagination;
                    HttpContext.Session.SetString("Pagination", JsonConvert.SerializeObject(pagination));
                    ViewBag.Pagination = pagination;
                    ViewBag.Property = property;
                    return View();
                }
                else
                {
                    string paginatioonJson = HttpContext.Session.GetString("Pagination");
                    ViewBag.Pagination = JsonConvert.DeserializeObject<Pagination>(paginatioonJson);
                    List<Property> properties = await _servicesApi.GetAllPropertiesByPage(pageNumber);
                    ViewBag.Property = properties;
                    return View();
                }
            }
            else
            {
                ApiResult apiResult = await _servicesApi.GetAllResult(propertySearch);
                var property = apiResult.items;
                var pagination = apiResult.pagination;
                HttpContext.Session.SetString("Pagination", JsonConvert.SerializeObject(pagination));
                ViewBag.Pagination = pagination;
                ViewBag.Property = property;
                return View();
            }                                  
        }

        [HttpPost]
        public async Task<IActionResult> SearchProperty(PropertySearch propertySearch)
        {
            return RedirectToAction("Index", propertySearch);
        }
        public async Task<IActionResult> Property(int idProperty)
        {
            PropertyDetails property = new PropertyDetails();
            property = await _servicesApi.GetPropertyById(idProperty);
            ViewBag.Accion = "Detalle propiedad";
            return View(property);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
