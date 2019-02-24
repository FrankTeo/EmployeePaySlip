using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using EmployeePaySlip.Web.Models;
using EmployeePaySlip.Web.Service;
using Microsoft.Extensions.Logging;

namespace EmployeePaySlip.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmployeeCsvHelper _employeeCsvHelper;
        private readonly ICalculator _calculator;

        private const string PaySlipSessionKey = "payslipdata";

        public HomeController(
            ILogger<HomeController> logger,
            IEmployeeCsvHelper employeeCsvHelper,
            ICalculator calculator
            )
        {
            _logger = logger;
            _employeeCsvHelper = employeeCsvHelper;
            _calculator = calculator;
        }

        public IActionResult Index()
        {
            var viewModel = new PaySlipViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(PaySlipViewModel viewModel)
        {
            if(viewModel.EmployeeSalaryFile == null)
            {
                viewModel.MessageLevel = "danger";
                viewModel.ErrorMessage = $"please select file first";
                return View(viewModel);
            }
            IEnumerable<Employee> employees = null;
            try
            {
                employees = LoadEmployeeFromCSVFile(viewModel);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Parse employee csv file failure: {e.Message}");
                viewModel.MessageLevel = "danger";
                viewModel.ErrorMessage = $"Parse CSV file {viewModel.EmployeeSalaryFile.FileName} failure";

                return View(viewModel);
            }

            if (employees == null)
            {
                return View(viewModel);
            }

            try
            {
                viewModel.Employees = employees.Select(employee => new EmployeeViewModel(employee));
            }
            catch(Exception e)
            {
                _logger.LogInformation($"Transfer employee failure: {e.Message}");

                return View(viewModel);
            }

            try
            {
                GeneratePaySlip(employees, viewModel);
                CachePaySlipsToSession(viewModel);
            }
            catch(Exception e)
            {
                _logger.LogInformation($"Generate Pay Slip error: {e.Message}");

                return View(viewModel);
            }

            viewModel.MessageLevel = "success";
            viewModel.ErrorMessage = "Generate pay slip success";
            return View(viewModel);
        }

        public void GeneratePaySlip(IEnumerable<Employee> employees, PaySlipViewModel viewModel)
        {
            IEnumerable<IPaySlip> PaySilps = _calculator.CalculatePaySlips(employees);

            viewModel.PaySilps = PaySilps.Select(paySlip => new PaySlipResultViewModel((PaySlip)paySlip));

        }

        /// <summary>
        /// Downloads the pay slip.
        /// </summary>
        /// <returns>The pay slip.</returns>
        [HttpGet]
        public IActionResult DownloadPaySlip(PaySlipViewModel viewModel)
        {
            var bytes = HttpContext.Session.Get(PaySlipSessionKey);
            return File(bytes, "text/csv", "output.csv");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Loads the employee from CSVF ile.
        /// </summary>
        /// <returns>The employee from CSV File.</returns>
        /// <param name="viewModel">View model.</param>
        private IEnumerable<Employee> LoadEmployeeFromCSVFile(PaySlipViewModel viewModel)
        {
            var stream = viewModel.EmployeeSalaryFile.OpenReadStream();
            IEnumerable<Employee> employees = _employeeCsvHelper.ReadFromCsvFile<Employee>(stream).ToList();

            return employees;
        }


        private void CachePaySlipsToSession(PaySlipViewModel viewModel)
        {
            var bytes = PublicCsvHelper.EnumerableToCsvBytes(viewModel.PaySilps);
            HttpContext.Session.Set(PaySlipSessionKey, bytes);
        }

    }
}
