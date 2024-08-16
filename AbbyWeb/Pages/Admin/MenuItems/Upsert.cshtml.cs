using AbbyWeb.DataAccess.Data;
using Abby.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Abby.DataAccess.Repository.IRepository;
using Abby.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AbbyWeb.Pages.Admin.MenuItems
{
    [BindProperties]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public MenuItem MenuItem { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> FoodTypeList { get; set; }
        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
            MenuItem = new MenuItem();
        }
        public void OnGet(int? id)
        {
            if(id != null)
            {
                MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id);
            }

            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            FoodTypeList = _unitOfWork.FoodType.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }
        public async Task<IActionResult> OnPost()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if(MenuItem.Id == 0)
            {
                //create
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\menuitems");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStreams);
                }
                MenuItem.Image = @"\images\menuitems\" + fileName + extension;
                _unitOfWork.MenuItem.Add(MenuItem);
                _unitOfWork.Save();
            }
            else
            {
                //Edit
                var objFromDb = _unitOfWork.MenuItem.Get(MenuItem.Id);
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\menuitems");
                    var extension_new = Path.GetExtension(files[0].FileName);

                    var imagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension_new), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    MenuItem.Image = @"\images\menuitems\" + fileName + extension_new;
                }
                else
                {
                    MenuItem.Image = objFromDb.Image;
                }
                _unitOfWork.MenuItem.Update(MenuItem);
                _unitOfWork.Save();
            }
            return RedirectToPage("Index");
        }
    }
}
