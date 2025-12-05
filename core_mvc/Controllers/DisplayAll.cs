using Microsoft.AspNetCore.Mvc;
using core_mvc.Models;

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
        public IActionResult detalistab(int? id) //? is because geting details via url, like we did in query string
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
    }
}
