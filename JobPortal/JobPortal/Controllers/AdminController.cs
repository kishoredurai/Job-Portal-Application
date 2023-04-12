using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    public class AdminController : Controller
    {
        private IConfiguration configuration;
        public AdminController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Admin()
        {
            List<CompanyViewModel> companyList = new List<CompanyViewModel>();
            String conn = configuration.GetConnectionString("JobPortal");
            SqlConnection connection = new SqlConnection(conn);
            connection.Open();

            string query = "SELECT * FROM COMPANY";
            SqlCommand sqlCommand = new SqlCommand(query, connection);

            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                CompanyViewModel obj = new CompanyViewModel();
                obj.id = (int)reader[0];
                obj.name = "" + reader[1];
                obj.location = "" + reader[2];

                companyList.Add(obj);
            }
            return View(companyList);
        }

        public IActionResult AddCompany()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCompanyToTable() 
        {
            return RedirectToAction("Admin");
            
        }
    }
}
