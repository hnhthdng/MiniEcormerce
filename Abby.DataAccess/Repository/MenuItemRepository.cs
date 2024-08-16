using Abby.DataAccess.Repository.IRepository;
using Abby.Model;
using AbbyWeb.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abby.DataAccess.Repository
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        private readonly ApplicationDbContext _db;
        public MenuItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(MenuItem menuItem)
        {
            var objFromDb = _db.MenuItem.FirstOrDefault(s => s.Id == menuItem.Id);
            objFromDb.Name = menuItem.Name;
            objFromDb.Description = menuItem.Description;
            objFromDb.CategoryId = menuItem.CategoryId;
            objFromDb.FoodTypeId = menuItem.FoodTypeId;
            objFromDb.Price = menuItem.Price;
            if (menuItem.Image != null)
            {
                objFromDb.Image = menuItem.Image;
            }
        }
    }
}
