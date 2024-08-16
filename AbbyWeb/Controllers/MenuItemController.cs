using Abby.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace AbbyWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public MenuItemController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Get()
        {

            return Json(new { data = _unitOfWork.MenuItem.GetAll(includeProperty: "Category,FoodType") });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id);
            var oldImagePaths = Path.Combine(_hostingEnvironment.WebRootPath, objFromDb.Image.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePaths))
            {
                System.IO.File.Delete(oldImagePaths);
            }
            _unitOfWork.MenuItem.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });
        }
    }
}
