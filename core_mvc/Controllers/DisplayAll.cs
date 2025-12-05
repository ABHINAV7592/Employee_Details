using Microsoft.AspNetCore.Mvc;
using core_mvc.Models;
using System.Net.WebSockets;

namespace core_mvc.Controllers
{
    public class DisplayAll : Controller
    {
        EmployeeDB dbobj = new EmployeeDB();
        public IActionResult displayall_pageoad()
        {
            //List<EmployeeInsert> getlist = dbobj.selectDB();
            //return View(getlist);
            List<EmployeeInsert> employees = null;
            using(var client=new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5163/EmpDetails/");
                var responseTask = client.GetAsync("getalltab"); //getalltab is the name given in route for display all in coreweb_api application.
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<EmployeeInsert>>();
                    readTask.Wait();
                    employees = readTask.Result;
                }
            }
            return View(employees);
        }
        public IActionResult detalistab(int? id) //? is because geting details via url, like we did in query string(not mandatory).
        {
            EmployeeInsert emp = null;
            using(var client=new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5163/EmpDetails/");
                var responseTask = client.GetAsync($"gettabwithid/{id}"); //$ is for concating id.
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<EmployeeInsert>();
                    readTask.Wait();
                    emp = readTask.Result;
                }
                else
                {
                    emp = new EmployeeInsert();
                }
            }
            return View(emp);
        }
        public IActionResult deleteDB(int id)
        {
            using(var client=new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5163/EmpDetails/");
                var postTask = client.DeleteAsync($"deletetab/{id}");
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("displayall_pageoad");
                }
            }
            return View("displayall_pageoad");
        }
        public IActionResult Edittab(int? id) //? is because geting details via url, like we did in query string(not mandatory).
        {
            EmployeeInsert emp = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5163/EmpDetails/");
                var responseTask = client.GetAsync($"gettabwithid/{id}"); //$ is for concating id.
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<EmployeeInsert>();
                    readTask.Wait();
                    emp = readTask.Result;
                }
                else
                {
                    emp = new EmployeeInsert();
                }
            }
            return View(emp);
        }
        [HttpPost]
        public IActionResult updatetab(EmployeeInsert empobj)
        {
            if (ModelState.IsValid)
            {
                using (var client=new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5163/EmpDetails/");
                    var postTask = client.PutAsJsonAsync<EmployeeInsert>($"puttab/{empobj.emp_id}", empobj);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("displayall_pageoad"); 
                    }
                }
                return View();
            }
            return View();
        }
    }
}
