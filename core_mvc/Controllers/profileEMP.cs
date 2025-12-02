using core_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using core_mvc.Models;

namespace core_mvc.Controllers
{
    public class profileEMP : Controller
    {
        EmployeeDB dbobj = new EmployeeDB();
        public IActionResult Userprofile_pageload(int id) //same variable name given in model
        {
            EmployeeInsert getlist = dbobj.selectprofiledb(id);
            return View(getlist);
        }
        public IActionResult profile_update_click(EmployeeInsert objcls)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string s = dbobj.updateprofile(objcls);
                    TempData["msg1"] = s;
                }
            }
            catch(Exception ex)
            {
                TempData["msg1"] = ex.Message;
            }
            return View("Userprofile_pageload");
        }
    }
}
