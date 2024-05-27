using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Helpers;
using System.Runtime.ConstrainedExecution;

namespace ProductEntryForm.Controllers
{
    public class HomeController : Controller
    {


        string connStr = @"Data Source=gonzxph\MSSQLSERVER04;Initial Catalog=student;Integrated Security=True";
        
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

            var mime = System.Web.MimeMapping.GetMimeMapping(Path.GetFileName(filepath));
            Response.Headers.Add("content-disposition", "inline");
            return new FilePathResult(filepath, mime);
        }

        [HttpGet]
        public ActionResult UpdateProduct(int prod_id)
        {
            var prod_data = new List<object>();

            using (var db = new SqlConnection(connStr))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM PRODUCT WHERE ID = @id";
                    cmd.Parameters.AddWithValue("@id", prod_id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {


                            prod_data.Add(new
                            {
                                id = reader["ID"].ToString(),
                                name = reader["NAME"].ToString(),
                                price = reader["PRICE"].ToString(),
                                desc = reader["DESCRIPTION"].ToString(),
                                isbn = reader["ISBN"].ToString(),
                                pub = reader["PUBLISHER"].ToString(),
                                page = reader["PAGE"].ToString(),
                                weight = reader["WEIGHT"].ToString(),
                                dimension = reader["DIMENSION"].ToString(),
                                stock = reader["STOCK"].ToString(),
                                image = reader["IMAGE"].ToString(),
                            });

                            return Json(prod_data, JsonRequestBehavior.AllowGet);
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
            var idno = Request["id"];
            var name = Request["name"];
            var price = Request["price"];
            var desc = Request["desc"];
            var isbn = Request["isbn"];
            var pub = Request["pub"];
            var page = Request["page"];
            var weight = Request["weight"];
            var dimension = Request["dimnesion"];
            var stock = Request["stock"];
            var image = Request["image"];

            using (var db = new SqlConnection(connStr))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE IMAGE SET NAME = @name, PRICE = @price, DESCRIPTION= @desc, ISBN = @isbn,  PUBLISHER = @pub, PAGE = @page, WEIGHT = @weight, DIMENSION = @dimension, STOCK = @stock, IMAGE = @image" +
                                        "WHERE ID = @id";
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@desc", desc);
                    cmd.Parameters.AddWithValue("@isbn", isbn);
                    cmd.Parameters.AddWithValue("@pub", pub);
                    cmd.Parameters.AddWithValue("@page", page);
                    cmd.Parameters.AddWithValue("@weight", weight);
                    cmd.Parameters.AddWithValue("@dimension", dimension);
                    cmd.Parameters.AddWithValue("@stock", stock);
                    cmd.Parameters.AddWithValue("@image", image);
                    var ctr = cmd.ExecuteNonQuery();
                    if (ctr > 0)
                    {
                        data.Add(new { mess = 0 });
                    }
                }
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        ////Customer
        public ActionResult CustomerDashboard()
        {
            int userId = 0;
            if (Session["UserId"] != null)
            {
                // Convert session value to int
                userId = Convert.ToInt32(Session["UserId"]);
                // Proceed with dashboard logic
                return View();
            }
            else
            {
                // Redirect to login if user is not logged in
                return RedirectToAction("UserLogin");
            }
        }


        public ActionResult UserLogin()
        {
            return View();
        }


        [HttpPost]
        public ActionResult UserRegister()
        {
            var data = new List<object>();
            string fname = Request.Form["user_fname"];
            string lname = Request.Form["user_lname"];
            string phoneno = Request.Form["user_phoneno"];
            string email = Request.Form["user_email"];
            string pwd = Request.Form["user_pwd"];

            using (var db = new SqlConnection(connStr))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO [USER] (USER_FNAME, USER_LNAME, USER_PHONE_NO, USER_EMAIL, USER_PASSWORD) " +
                                      "VALUES (@USER_FNAME, @USER_LNAME, @USER_PHONE_NO, @USER_EMAIL, @USER_PASSWORD)";

                    cmd.Parameters.AddWithValue("@USER_FNAME", fname);
                    cmd.Parameters.AddWithValue("@USER_LNAME", lname);
                    cmd.Parameters.AddWithValue("@USER_PHONE_NO", phoneno);
                    cmd.Parameters.AddWithValue("@USER_EMAIL", email);
                    cmd.Parameters.AddWithValue("@USER_PASSWORD", pwd);

                    cmd.ExecuteNonQuery();
                }
            }

            data.Add(new { mess = "User registered successfully!" });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult checkLogin()
        {
            var data = new List<object>();
            var response = new { success = false, message = "Invalid email or password" };
            string email = Request["email"];
            string pwd = Request["pwd"];

            using (var db = new SqlConnection(connStr))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM [user] WHERE user_email = @user_email";
                    cmd.Parameters.AddWithValue("@user_email", email);

                    using (var reader = cmd.ExecuteReader()) {

                        if (reader.Read()) {
                            string userId = reader["user_id"].ToString();
                            string check_pass = reader["user_password"].ToString();
                            if(check_pass == pwd)
                            {
                                Session["UserId"] = userId;
                                response = new { success = true, message = "Login Successfull" };
                            }
                        }
                    }
                }
            }

            return Json(response, JsonRequestBehavior.AllowGet);    
        }

        [HttpPost]
        public ActionResult AddToCart(int productId, int quantity)
        {
            var response = new { success = false, message = "Error adding to cart" };
            var userId = Session["UserId"];

            if (userId == null)
            {
                response = new { success = false, message = "User not logged in" };
                return Json(response, JsonRequestBehavior.AllowGet);
            }

           
            using (var db = new SqlConnection(connStr))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandType = CommandType.Text;

                            // Check if the product already exists in the cart
                            cmd.CommandText = "SELECT cart_count FROM Cart WHERE user_id = @user_id AND prod_id = @prod_id";
                            cmd.Parameters.AddWithValue("@user_id", userId);
                            cmd.Parameters.AddWithValue("@prod_id", productId);

                            var existingCartCount = cmd.ExecuteScalar();
                            cmd.Parameters.Clear(); // Clear parameters after executing the query

                            int newCartCount;
                            decimal prodPrice;

                            // Fetch the product price
                            cmd.CommandText = "SELECT prod_price FROM Product WHERE prod_id = @prod_id";
                            cmd.Parameters.AddWithValue("@prod_id", productId);
                            prodPrice = (decimal)cmd.ExecuteScalar();
                            cmd.Parameters.Clear(); // Clear parameters after executing the query

                            if (existingCartCount != null)
                            {
                                // If the product exists, update the quantity and total price
                                newCartCount = Convert.ToInt32(existingCartCount) + quantity;
                                cmd.CommandText = "UPDATE Cart SET cart_count = @newCartCount, cart_totalprice = @cart_totalPrice WHERE user_id = @user_id AND prod_id = @prod_id";
                                cmd.Parameters.AddWithValue("@newCartCount", newCartCount);
                                cmd.Parameters.AddWithValue("@cart_totalPrice", newCartCount * prodPrice);
                                cmd.Parameters.AddWithValue("@user_id", userId);
                                cmd.Parameters.AddWithValue("@prod_id", productId);
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear(); // Clear parameters after executing the query

                                response = new { success = true, message = "Product quantity updated successfully" };
                            }
                            else
                            {
                                // If the product does not exist, insert a new record
                                newCartCount = quantity;
                                cmd.CommandText = "INSERT INTO Cart (user_id, prod_id, cart_count, cart_totalprice) VALUES (@user_id, @prod_id, @cart_count, @cart_totalprice)";
                                cmd.Parameters.AddWithValue("@user_id", userId);
                                cmd.Parameters.AddWithValue("@prod_id", productId);
                                cmd.Parameters.AddWithValue("@cart_count", newCartCount);
                                cmd.Parameters.AddWithValue("@cart_totalprice", newCartCount * prodPrice);
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear(); // Clear parameters after executing the query

                                response = new { success = true, message = "Product added to cart successfully" };
                            }

                            // Update the product's stock
                            cmd.CommandText = "UPDATE Product SET prod_stock = prod_stock - @quantity WHERE prod_id = @prod_id";
                            cmd.Parameters.AddWithValue("@quantity", quantity);
                            cmd.Parameters.AddWithValue("@prod_id", productId);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear(); // Clear parameters after executing the query
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        response = new { success = false, message = "Error adding to cart: " + ex.Message };
                        return Json(response, JsonRequestBehavior.AllowGet);
                    }
                }
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }







    }
}