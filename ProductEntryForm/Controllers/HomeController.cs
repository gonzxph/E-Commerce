﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ProductEntryForm.Controllers
{
    public class HomeController : Controller
    {


        string connStr = @"Data Source=localhost\mssqlserver04;Initial Catalog=student;Integrated Security=True";
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Product()
        {
            return View();
        }

        public ActionResult Customer()
        {
            return View();
        }


        public ActionResult Admin_Dashboard()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Product_Data(HttpPostedFileBase img)
        {
            var data = new List<object>();
            string prodName = Request["prodName"];
            double prodPrice = Convert.ToDouble(Request["prodPrice"]);
            string prodDescript = Request["prodDescript"];
            int prodIsbn = Convert.ToInt32(Request["prodIsbn"]);
            string prodPub = Request["prodPub"];
            int prodPage = Convert.ToInt32(Request["prodPage"]);
            double prodWeight = Convert.ToDouble(Request["prodWeight"]);
            string prodDimen = Request["prodDimen"];
            int prodStock = Convert.ToInt32(Request["prodStock"]);
            string image = Path.GetFileName(img.FileName);
            string file_path = "C:\\Uploads";
            string filepath = Path.Combine(file_path, image);
            img.SaveAs(filepath);



                using (var db = new SqlConnection(connStr))
                {

                    db.Open();
                    using (var cmd = db.CreateCommand())
                    {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO product (NAME, PRICE, DESCRIPTION, ISBN, PUBLISHER, PAGE, WEIGHT, DIMENSION, STOCK, [IMAGE]) VALUES (@NAME, @PRICE, @DESCRIPTION, @ISBN, @PUBLISHER, @PAGE, @WEIGHT, @DIMENSION, @STOCK, @IMAGE)";
                    cmd.Parameters.AddWithValue("@NAME", prodName);
                    cmd.Parameters.AddWithValue("@PRICE", prodPrice);
                    cmd.Parameters.AddWithValue("@DESCRIPTION", prodDescript);
                    cmd.Parameters.AddWithValue("@ISBN", prodIsbn);
                    cmd.Parameters.AddWithValue("@PUBLISHER", prodPub);
                    cmd.Parameters.AddWithValue("@PAGE", prodPage);
                    cmd.Parameters.AddWithValue("@WEIGHT", prodWeight);
                    cmd.Parameters.AddWithValue("@DIMENSION", prodDimen);
                    cmd.Parameters.AddWithValue("@STOCK", prodStock);
                    cmd.Parameters.AddWithValue("@IMAGE", image);

                    var ctr = cmd.ExecuteNonQuery();
                    if(ctr >= 1)
                    {
                        data.Add(new
                        {
                            mess = 1,
                        }) ;
                    }

                }


            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteProduct()
        {
            var data = new List<object>();
            var id = Request["prod_id"];

            using (var db = new SqlConnection(connStr))
            {
                db.Open();
                using(var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM PRODUCT WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", id);

                    var ctr = cmd.ExecuteNonQuery();

                    if (ctr >= 1)
                    {
                        data.Add(new
                        {
                            mess = 1
                        });
                    }

                }
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public ActionResult postCustomer()
        {
            var data = new List<object>();
            string firstname = Request["firstname"];
            string lastname = Request["lastname"];
            int phonenumber = Convert.ToInt32(Request["phonenumber"]);
            string email = Request["email"];
            string pwd = Request["pwd"];
            string pwd2 = Request["pwd2"];
            string profile = Request["profile"];




            using (var db = new SqlConnection(connStr))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO customer (CUS_FIRSTNAME, CUS_LASTNAME, CUS_PHONE_NUMBER, CUS_EMAIL, CUS_PASSWORD) VALUES (@CUS_FIRSTNAME, @CUS_LASTNAME, @CUS_PHONE_NUMBER, @CUS_EMAIL, @CUS_PASSWORD)";
                    cmd.Parameters.AddWithValue("@CUS_FIRSTNAME", firstname);
                    cmd.Parameters.AddWithValue("@CUS_LASTNAME", lastname);
                    cmd.Parameters.AddWithValue("@CUS_PHONE_NUMBER", phonenumber);
                    cmd.Parameters.AddWithValue("@CUS_EMAIL", email);
                    cmd.Parameters.AddWithValue("@CUS_PASSWORD", pwd);


                    var ctr = cmd.ExecuteNonQuery();
                    if(ctr >= 1)
                    {
                        data.Add(new
                        {
                            mess = 1,
                        }) ;
                    }
                    
                }
            }




            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public FileResult Image(string filename)
        {
            var folder = "C:\\Uploads";
            var filepath = Path.Combine(folder, filename);

            /*if (!System.IO.File.Exists(filepath))
            {
                throw new FileNotFoundException("File not found", filename);
            }*/

            var mime = System.Web.MimeMapping.GetMimeMapping(Path.GetFileName(filepath));
            Response.Headers.Add("content-disposition", "inline");
            return new FilePathResult(filepath, mime);
        }

        [HttpGet]
        public ActionResult GetProduct(int prod_id)
        {
            var data = new List<object>();
            var id = Request["id"];

            using (var db = new SqlConnection(connStr))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT FIRSTNAME FROM IMAGE WHERE ID = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var studentData = new
                            {
                                name = reader["NAME"].ToString(),
                                price = reader["PRICE"].ToString()
                            };
                            return Json(studentData, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(null, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
        }
        [HttpPost]
        public ActionResult StudentUpdate()
        {
            var data = new List<object>();
            var idno = Request["search_id"];
            var fname = Request["firstname"];

            using (var db = new SqlConnection(connStr))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE IMAGE SET FIRSTNAME = @fname WHERE ID = @id";
                    cmd.Parameters.AddWithValue("@fname", fname);
                    cmd.Parameters.AddWithValue("@id", idno);
                    var ctr = cmd.ExecuteNonQuery();
                    if (ctr > 0)
                    {
                        data.Add(new { mess = 0 });
                    }
                }
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}