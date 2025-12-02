using Microsoft.AspNetCore.Mvc;
using core_mvc.Models;

namespace core_mvc.Controllers
{
    public class DisplayAll : Controller
    {
        EmployeeDB dbobj = new EmployeeDB();
        public IActionResult displayall_pageoad()
        {
            List<EmployeeInsert> getlist = dbobj.selectDB();
            return View(getlist);
        }
    }
}
