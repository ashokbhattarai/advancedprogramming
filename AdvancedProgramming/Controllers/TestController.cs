using AdvancedProgramming.Models;
using AdvancedProgramming.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AdvancedProgramming.Controllers
{
    public class TestController : Controller
    {

        private readonly IEmployeeService _employeeService;

        public TestController(IEmployeeService employeeService)
        {
           _employeeService = employeeService;
           
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NickName")] Employee employee)
        {
         
                var result = await _employeeService.CreateAsync(employee);
                return View();
            }
        }
    }

