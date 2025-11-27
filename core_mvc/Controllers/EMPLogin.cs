using Microsoft.AspNetCore.Mvc;
using core_mvc.Models;

namespace core_mvc.Controllers
{
    public class EMPLogin : Controller
    {
        EmployeeDB dbobj = new EmployeeDB();
        public IActionResult Login_pageload()
        {
            return View();
        }
        [HttpPost]

        public IActionResult login_click(EmployeeInsert objcls)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int cid =Convert.ToInt32(dbobj.LoginDB(objcls));
                    if (cid == 1)
                    {
                        //TempData["msglog"] = "login successful";
                        return RedirectToAction("Userprofile_pageload", "profileEMP", new { id = objcls.emp_id });
                    }
                    else
                    {
                        TempData["msglog"] = "invalid login";
                    }
                }
            }
            catch(Exception ex)
            {
                TempData["msglog"] = ex.Message;
            }
            return View("Login_pageload");
            
        }
    }
}
