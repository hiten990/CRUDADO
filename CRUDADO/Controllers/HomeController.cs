using CRUDADO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CRUDADO.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration Configuration { get; }

        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IActionResult Index(User usermngr)
        {
            return View(usermngr);
        }

        [HttpPost]
        public IActionResult Login(User usermngr)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"select * from UserMaster Where Email = '{usermngr.Email}' and PassWord = '{usermngr.Password}'", connection))
                {
                    command.CommandTimeout = 0;
                    command.CommandType = CommandType.Text;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usermngr.UserName = reader["UserName"].ToString();
                            usermngr.Email = reader["Email"].ToString();
                            usermngr.Password = reader["Password"].ToString();
                            usermngr.Gender = reader["Gender"].ToString();
                            usermngr.City = reader["City"].ToString();
                            usermngr.DateOfBirth = reader["DateOfBirth"].ToString();
                        }
                    }
                }
                connection.Close();

                if (usermngr.Email != null && usermngr.Password != null && usermngr.UserName != null)
                {
                    return View(usermngr);
                }
                else
                {
                    usermngr.Msg = "Invalid Email Or Password !";
                    return RedirectToAction("Index", usermngr);
                }
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User usermngr)
        {
            if (ModelState.IsValid)
            {
                string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = $"Insert Into UserMaster (UserName, Email, Password, Gender, City, DateOfBirth) Values ('{usermngr.UserName}', '{usermngr.Email}','{usermngr.Password}','{Request.Form["rbGender"].ToString()}','{Request.Form["ddlCity"].ToString()}','{usermngr.DateOfBirth}')";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    return RedirectToAction("Index");
                }
            }
            else
                return View();
        }
    }
}