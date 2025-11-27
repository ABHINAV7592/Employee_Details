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
    }
}
