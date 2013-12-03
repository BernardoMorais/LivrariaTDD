using System.Globalization;
using System.IO;
using System.Web.Mvc;
using LivrariaTDD.Infrastructure.BRL.Product;
using LivrariaTDD.Models.Product;
using Omu.ValueInjecter;

namespace LivrariaTDD.Controllers.Admin
{
    public partial class AdminController
    {
        [HttpGet]
        [Authorize (Roles = "admin")]
        public ActionResult NewProduct()
        {
            return View(new Models.Product.Product());
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult NewProduct(Models.Product.Product model)
        {
            var newProduct = new Infrastructure.Models.Product();

            newProduct.InjectFrom(model);            

            var result = (model.ProductId == 0) ? _productBusiness.SalvarLivro(newProduct) : _productBusiness.GetInfo(model.ProductId);

            if(newProduct.Photo == null)
                newProduct.Photo = result.Photo;

            result.InjectFrom(newProduct);

            if(result != null)
            {
                var hfc = HttpContext.Request.Files;

                const string path = "/Content/Images/Products/";

                var fileName = result.ProductId.ToString(CultureInfo.InvariantCulture);

                for (var i = 0; i < hfc.Count; i++)
                {
                    var hpf = hfc[i];

                    if (hpf == null || hpf.ContentLength <= 0) continue;

                    //fileName = Request.Browser.Browser == "IE" ? Path.GetFileName(hpf.FileName) : hpf.FileName;

                    fileName = fileName + Path.GetExtension(hpf.FileName);

                    if (!Directory.Exists(Server.MapPath(path)))
                        Directory.CreateDirectory(Server.MapPath(path));

                    var fullPathWithFileName = path + fileName;

                    hpf.SaveAs(Server.MapPath(fullPathWithFileName));

                    model.Photo = fileName;

                    result.Photo = fileName;
                }

                _productBusiness.Update(result);

                return RedirectToAction("Index", "Admin");
            }

            return View(model);
        }

    }
}
