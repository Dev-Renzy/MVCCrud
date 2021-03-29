using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Web.Models;


namespace Web.Controllers
{
    public class AnimalsController : Controller
    {
        string connection = System.Configuration.ConfigurationManager.ConnectionStrings["TEST"].ConnectionString;  //Gets the ConnectionStringsSection data for the current application's default configuration.


        [HttpGet]
        // GET: Animals
        public ActionResult Index()
        {
            List<Animals> list = new List<Animals>();
            using (MySqlConnection MysqlCon = new MySqlConnection(connection))
            {
                MysqlCon.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT * from tblAnimals", MysqlCon))//Query
                {
                    MySqlDataReader rdr = command.ExecuteReader(); //method used to execute the query/mysql command or storedprocedure returns a set of rows from the database.
                    while (rdr.Read())
                    {
                        list.Add(new Animals
                        {
                            AnimalsID = Convert.ToInt32(rdr["AnimalsID"]),
                            AnimalName = rdr["AnimalName"].ToString(),
                            Price = rdr["Price"].ToString(),
                            Pcs = rdr["Pcs"].ToString()
                        }); 
                    }
                }
            }
            return View(list);
        }


        // POST: Animals/Create
        [HttpPost]
        public ActionResult Insert(Animals animals)
        {

           
            using (MySqlConnection MysqlCon = new MySqlConnection(connection))
            {
                MysqlCon.Open();
                using (MySqlCommand command = new MySqlCommand("INSERT INTO tblAnimals (AnimalName, Price, Pcs) VALUES (@AnimalName, @Price, @Pcs)", MysqlCon))
                {
                    
                    command.Parameters.AddWithValue("AnimalName", animals.AnimalName);
                    command.Parameters.AddWithValue("Price", animals.Price);
                    command.Parameters.AddWithValue("Pcs", animals.Pcs);
                    command.ExecuteNonQuery(); // ExecuteNonQuery method is used to execute SQL Command or the storeprocedure performs, INSERT, UPDATE or Delete operations
                    MysqlCon.Close();
                }

                return View("Index");
            }
        }



        // GET: Animals/Edit/5


        // POST: Animals/Edit/5
        [HttpGet]
        public ActionResult Edit(int id, Animals cid)
        {
            var data = new Animals();
            using (MySqlConnection MysqlCon = new MySqlConnection(connection))
            {
                MysqlCon.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM tblAnimals WHERE AnimalsID = @AnimalsID", MysqlCon))
                {
                    command.Parameters.AddWithValue("@AnimalsID", id);
                    MySqlDataReader rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {
                        data.AnimalName = rdr["AnimalName"].ToString();
                        data.Price = rdr["Price"].ToString();
                        data.Pcs = rdr["Pcs"].ToString();
                    }
                    MysqlCon.Close();
                }
                return View(data);
            }
            
        }
        [HttpPost]
        
        public ActionResult Edit(int id)
        {
            try
            {
                var data = new Animals();
                using (MySqlConnection MysqlCon = new MySqlConnection(connection))
                {
                    MysqlCon.Open();
                    using (MySqlCommand command = new MySqlCommand("UPDATE tblAnimals SET AnimalName = @AnimalName, Price = @Price, Pcs = @Pcs WHERE AnimalsID = @AnimalsID", MysqlCon))
                    {
                        command.Parameters.AddWithValue("@AnimalsID", id);
                        command.Parameters.AddWithValue("@AnimalName", Request.Form["AnimalName"]);
                        command.Parameters.AddWithValue("@Price", Request.Form["Price"]);
                        command.Parameters.AddWithValue("@Pcs", Request.Form["Pcs"]);
                        MySqlDataReader rdr = command.ExecuteReader();
                        while (rdr.Read())
                        {
                            data.AnimalName = rdr["AnimalName"].ToString();
                            data.Price = rdr["Price"].ToString();
                            data.Pcs = rdr["Pcs"].ToString();
                        }
                        MysqlCon.Close();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }

        }

        // GET: Animals/Delete/5
     [HttpGet]
        public ActionResult Delete(int id, Animals cid )
        {
            var data = new Animals();
            using (MySqlConnection MysqlCon = new MySqlConnection(connection))
            {
                MysqlCon.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM tblAnimals WHERE AnimalsID = @AnimalsID", MysqlCon))
                {
                    command.Parameters.AddWithValue("@AnimalsID", id);
                    MySqlDataReader rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {
                        data.AnimalName = rdr["AnimalName"].ToString();
                        data.Price = rdr["Price"].ToString();
                        data.Pcs = rdr["Pcs"].ToString();
                    }
                    MysqlCon.Close();
                }
                return View(data);
            }
        }

        // POST: Animals/Delete/5
        [HttpPost]
        public ActionResult Delete( int id)
        {
            try
            {
                var data = new Animals();
                using (MySqlConnection MysqlCon = new MySqlConnection(connection))
                {
                    MysqlCon.Open();
                    using (MySqlCommand command = new MySqlCommand("DELETE FROM tblAnimals WHERE AnimalsID = @AnimalsID", MysqlCon))
                    {
                        command.Parameters.AddWithValue("@AnimalsID", id);
                        MySqlDataReader rdr = command.ExecuteReader();
                        while (rdr.Read())
                        {
                            data.AnimalName = rdr["AnimalName"].ToString();
                            data.Price = rdr["Price"].ToString();
                            data.Pcs = rdr["Pcs"].ToString();
                        }
                        MysqlCon.Close();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
    }
}
