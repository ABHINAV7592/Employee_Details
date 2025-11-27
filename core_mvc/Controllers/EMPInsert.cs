using Microsoft.AspNetCore.Mvc;
using core_mvc.Models;

namespace core_mvc.Controllers
{
    public class EMPInsert : Controller
    {
        EmployeeDB dbobj = new EmployeeDB();
        public IActionResult index_pageload ()
        {
            return View();
        }
        [HttpPost]

        public IActionResult index_click(EmployeeInsert objcls)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string resp = dbobj.InsertDB(objcls);
                    TempData["msg"] = resp;
                }
            }
            catch(Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return View("index_pageload");
        }
    }
}
