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
            try
            {
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
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return View(companyList);
        }

        public IActionResult AddCompany()
        {
            return View();
        }

        public IActionResult DeleteCompany(int id)
        {
            try
            {
                String conn = configuration.GetConnectionString("JobPortal");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();
                string query = $"DELETE FROM COMPANY WHERE id={id}";
                SqlCommand sqlCommand = new SqlCommand(query, connection);
                sqlCommand.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public IActionResult AddCompanyToTable() 
        {
            Console.WriteLine("In post");
            try
            {
                String conn = configuration.GetConnectionString("JobPortal");
                SqlConnection connection = new SqlConnection(conn);
                connection.Open();

                string companyName = Request.Form["name"];
                string location = Request.Form["location"];

                string query = $"INSERT INTO COMPANY VALUES('{companyName}','{location}')";
                SqlCommand sqlCommand = new SqlCommand(query, connection);

                sqlCommand.ExecuteNonQuery();

                connection.Close();

            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            
            return RedirectToAction("Admin"); 
        }

    }
}
