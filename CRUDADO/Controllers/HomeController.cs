﻿using CRUDADO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
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
                            usermngr.Id = Int32.Parse(reader["Id"].ToString());
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

        public IActionResult EditRegister(User usermngr)
        {
            return View(usermngr);
        }

        [HttpPost]
        public IActionResult Register(User usermngr)
        {
            if (usermngr.Id != 0)
            {
                if (!ModelState.IsValid)
                    return View(usermngr);
                else
                {
                    string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string sql = $"Update UserMaster Set UserName = '{usermngr.UserName}', Email = '{usermngr.Email}', Password = '{usermngr.Password}', Gender = '{usermngr.Gender}', City = '{usermngr.City}', DateOfBirth = '{usermngr.DateOfBirth}' Where Id = '{usermngr.Id}'";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.CommandType = CommandType.Text;
                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                        return View("Login", usermngr);
                    }
                }
            }
            else
            {
                if (!ModelState.IsValid)
                    return View();
                else
                {
                    string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string sql = $"Insert Into UserMaster (UserName, Email, Password, Gender, City, DateOfBirth) output INSERTED.ID Values ('{usermngr.UserName}', '{usermngr.Email}','{usermngr.Password}','{Request.Form["rbGender"].ToString()}','{Request.Form["ddlCity"].ToString()}','{usermngr.DateOfBirth}')";
                        usermngr.Gender = Request.Form["rbGender"].ToString();
                        usermngr.City = Request.Form["ddlCity"].ToString();
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.CommandType = CommandType.Text;
                            connection.Open();
                            usermngr.Id = (int)command.ExecuteScalar();
                            connection.Close();
                        }
                        return View("Login", usermngr);
                    }
                }
            }
        }
    }
}