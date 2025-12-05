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
            //core mvc code

            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //        string resp = dbobj.InsertDB(objcls);
            //        TempData["msg"] = resp;
            //    }
            //}
            //catch(Exception ex)
            //{
            //    TempData["msg"] = ex.Message;
            //}
            //return View("index_pageload");


            //core API calling code
            using(var client=new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5163/EmpDetails/"); //mvc_api uri and controller name of web_api
                var postTask = client.PostAsJsonAsync<EmployeeInsert>("posttab", objcls);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("displayall_pageoad", "DisplayAll");
                }
            }
            return RedirectToAction("displayall_pageoad", "DisplayAll");
        }
    }
}
