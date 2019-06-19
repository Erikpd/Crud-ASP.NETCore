using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Practicas.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;



namespace Practicas.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration Configuration { get; }
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Alumnos alumno)
        {
            if (ModelState.IsValid)
            {
                string connectionString = Configuration["ConnectionStrings:SQLConnection"];
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string sql = $"Insert Into Alumnos (NombreAlumno, ApellidoAlumno, Calificacion, FechaNacimiento, PrecioUni, Total) Values ('{alumno.Clave}', '{alumno.Producto}','{alumno.Cantidad}','{alumno.Fecha_compra}','{alumno.Precio_uni}','{alumno.Total}')";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        connection.Dispose();
                    }
                    return RedirectToAction("List");
                }
            }
            else
                return View();
        }

        public IActionResult List()
        {
            List<Alumnos> alumnosList = new List<Alumnos>();
            string connectionString = Configuration["ConnectionStrings:SQLConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //SqlDataReader
                connection.Open();

                string sql = "Select * From Alumnos"; SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Alumnos alumno = new Alumnos();
                        alumno.Id = Convert.ToInt32(dataReader["Id"]);
                        alumno.Clave = Convert.ToString(dataReader["NombreAlumno"]);
                        alumno.Producto = Convert.ToString(dataReader["ApellidoAlumno"]);
                        alumno.Cantidad = Convert.ToInt32(dataReader["Calificacion"]);
                        alumno.Fecha_compra = Convert.ToString(dataReader["FechaNacimiento"]);
                        alumno.Precio_uni = Convert.ToDouble(dataReader["PrecioUni"]);
                        alumno.Total = Convert.ToDouble(dataReader["Total"]);
                        alumnosList.Add(alumno);
                    }
                }
                connection.Close();
                connection.Dispose();
            }
            return View(alumnosList);
        }

        public IActionResult Update(int id)
        {
            string connectionString = Configuration["ConnectionStrings:SQLConnection"];
            Alumnos alumno = new Alumnos();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From Alumnos Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        alumno.Id = Convert.ToInt32(dataReader["Id"]);
                        alumno.Clave = Convert.ToString(dataReader["NombreAlumno"]);
                        alumno.Producto = Convert.ToString(dataReader["ApellidoAlumno"]);
                        alumno.Cantidad = Convert.ToInt32(dataReader["Calificacion"]);
                        alumno.Fecha_compra = Convert.ToString(dataReader["FechaNacimiento"]);
                        alumno.Precio_uni = Convert.ToDouble(dataReader["PrecioUni"]);
                        alumno.Total = Convert.ToDouble(dataReader["Total"]);
                        
                    }
                }
                connection.Close();
                connection.Dispose();
            }
            return View(alumno);
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult Update(Alumnos alumno)
        {
            string connectionString = Configuration["ConnectionStrings:SQLConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Update Alumnos SET NombreAlumno='{alumno.Clave}', ApellidoAlumno='{alumno.Producto}', Calificacion='{alumno.Cantidad}', FechaNacimiento='{alumno.Fecha_compra}', PrecioUni='{alumno.Precio_uni}', Total='{alumno.Precio_uni*alumno.Cantidad}' Where Id='{alumno.Id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {               
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    connection.Dispose();
                }
            }
            return RedirectToAction("List");
        }

        public IActionResult Delete(int id)
        {
            string connectionString = Configuration["ConnectionStrings:SQLConnection"];
            Alumnos alumno = new Alumnos();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From Alumnos Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        alumno.Id = Convert.ToInt32(dataReader["Id"]);
                        alumno.Clave = Convert.ToString(dataReader["NombreAlumno"]);
                        alumno.Producto = Convert.ToString(dataReader["ApellidoAlumno"]);
                        alumno.Cantidad = Convert.ToInt32(dataReader["Calificacion"]);
                        alumno.Fecha_compra = Convert.ToString(dataReader["FechaNacimiento"]);
                        alumno.Precio_uni = Convert.ToDouble(dataReader["PrecioUni"]);
                        alumno.Total = Convert.ToDouble(dataReader["Total"]);
                    }
                }
                connection.Close();
                connection.Dispose();
            }
            return View(alumno);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult Detele(int id)
        {
            string connectionString = Configuration["ConnectionStrings:SQLConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete From Alumnos Where Id='{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        ViewBag.Result = "Operation error:" + ex.Message;
                    }
                }
                return RedirectToAction("List");
            }
        }




        public IActionResult Details(int id)
        {
            string connectionString = Configuration["ConnectionStrings:SQLConnection"];
            Alumnos alumno = new Alumnos();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Select * From Alumnos Where Id='{id}'";
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        alumno.Id = Convert.ToInt32(dataReader["Id"]);
                        alumno.Clave = Convert.ToString(dataReader["NombreAlumno"]);
                        alumno.Producto = Convert.ToString(dataReader["ApellidoAlumno"]);
                        alumno.Cantidad = Convert.ToInt32(dataReader["Calificacion"]);
                        alumno.Fecha_compra = Convert.ToString(dataReader["FechaNacimiento"]);
                        alumno.Precio_uni = Convert.ToDouble(dataReader["PrecioUni"]);
                        alumno.Total = Convert.ToDouble(dataReader["Total"]);
                    }
                }
                connection.Close();
                connection.Dispose();
            }
            return View(alumno);
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
    }
}
