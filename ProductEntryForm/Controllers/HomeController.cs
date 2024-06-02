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
                    cmd.CommandText = "INSERT INTO product (PROD_NAME, PROD_PRICE, PROD_DESCRIPTION, PROD_ISBN, PROD_PUBLISHER, PROD_PAGE, PROD_WEIGHT, PROD_DIMENSION, PROD_STOCK, [PROD_IMAGE]) VALUES (@PROD_NAME, @PROD_PRICE, @PROD_DESCRIPTION, @PROD_ISBN, @PROD_PUBLISHER, @PROD_PAGE, @PROD_WEIGHT, @PROD_DIMENSION, @PROD_STOCK, @PROD_IMAGE)";
                    cmd.Parameters.AddWithValue("@PROD_NAME", prodName);
                    cmd.Parameters.AddWithValue("@PROD_PRICE", prodPrice);
                    cmd.Parameters.AddWithValue("@PROD_DESCRIPTION", prodDescript);
                    cmd.Parameters.AddWithValue("@PROD_ISBN", prodIsbn);
                    cmd.Parameters.AddWithValue("@PROD_PUBLISHER", prodPub);
                    cmd.Parameters.AddWithValue("@PROD_PAGE", prodPage);
                    cmd.Parameters.AddWithValue("@PROD_WEIGHT", prodWeight);
                    cmd.Parameters.AddWithValue("@PROD_DIMENSION", prodDimen);
                    cmd.Parameters.AddWithValue("@PROD_STOCK", prodStock);
                    cmd.Parameters.AddWithValue("@PROD_IMAGE", image);

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
                    cmd.CommandText = "DELETE FROM PRODUCT WHERE PROD_ID = @PROD_ID";
                    cmd.Parameters.AddWithValue("@PROD_ID", id);

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
        public ActionResult GetProduct(int id)
        {
            using (var db = new SqlConnection(connStr))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM PRODUCT WHERE PROD_ID = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            var imageUrl = Url.Action("Image", "Home", new { filename = reader["PROD_IMAGE"].ToString() });
                            var prod_data = new
                            {
                                id = reader["PROD_ID"].ToString(),
                                name = reader["PROD_NAME"].ToString(),
                                price = reader["PROD_PRICE"].ToString(),
                                desc = reader["PROD_DESCRIPTION"].ToString(),
                                isbn = reader["PROD_ISBN"].ToString(),
                                pub = reader["PROD_PUBLISHER"].ToString(),
                                page = reader["PROD_PAGE"].ToString(),
                                weight = reader["PROD_WEIGHT"].ToString(),
                                dimension = reader["PROD_DIMENSION"].ToString(),
                                stock = reader["PROD_STOCK"].ToString(),
                                image = imageUrl
                            };

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
        public ActionResult PostProductUpdate()
        {
            var prod_id = Request.Form["prod_id"];
            var prod_name = Request.Form["prod_name"];
            var prod_desc = Request.Form["prod_desc"];
            var prod_price = Request.Form["prod_price"];
            var prod_isbn = Request.Form["prod_isbn"];
            var prod_weight = Request.Form["prod_weight"];
            var prod_pub = Request.Form["prod_pub"];
            var prod_dimen = Request.Form["prod_dimen"];
            var prod_stock = Request.Form["prod_stock"];
            var prod_page = Request.Form["prod_page"];
            var insert_image = Request.Files["insert_image"];

            using (var db = new SqlConnection(connStr))
            {
                db.Open();
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;

                    string updateQuery = @"UPDATE PRODUCT SET 
                                    PROD_NAME = @prod_name, 
                                    PROD_DESCRIPTION = @prod_desc, 
                                    PROD_PRICE = @prod_price, 
                                    PROD_ISBN = @prod_isbn, 
                                    PROD_WEIGHT = @prod_weight, 
                                    PROD_PUBLISHER = @prod_pub, 
                                    PROD_DIMENSION = @prod_dimen, 
                                    PROD_STOCK = @prod_stock, 
                                    PROD_PAGE = @prod_page";

                    if (insert_image != null && insert_image.ContentLength > 0)
                    {
                        // Save file and update database
                        var fileName = Path.GetFileName(insert_image.FileName);
                        var filePath = Path.Combine(@"C:\Uploads", fileName); // Use the physical path
                        insert_image.SaveAs(filePath);

                        // Append the PROD_IMAGE field to the SQL command
                        updateQuery += ", PROD_IMAGE = @prod_image";
                        cmd.Parameters.AddWithValue("@prod_image", filePath); // Store the physical path in the database
                    }

                    updateQuery += " WHERE PROD_ID = @prod_id";

                    cmd.CommandText = updateQuery;

                    // Add the parameters for the command
                    cmd.Parameters.AddWithValue("@prod_id", prod_id);
                    cmd.Parameters.AddWithValue("@prod_name", prod_name);
                    cmd.Parameters.AddWithValue("@prod_desc", prod_desc);
                    cmd.Parameters.AddWithValue("@prod_price", prod_price);
                    cmd.Parameters.AddWithValue("@prod_isbn", prod_isbn);
                    cmd.Parameters.AddWithValue("@prod_weight", prod_weight);
                    cmd.Parameters.AddWithValue("@prod_pub", prod_pub);
                    cmd.Parameters.AddWithValue("@prod_dimen", prod_dimen);
                    cmd.Parameters.AddWithValue("@prod_page", prod_page);
                    cmd.Parameters.AddWithValue("@prod_stock", prod_stock);

                    var ctr = cmd.ExecuteNonQuery();
                    var result = new { success = ctr > 0 };

                    return Json(result, JsonRequestBehavior.AllowGet);
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

                            cmd.CommandText = "SELECT cart_count FROM Cart WHERE user_id = @user_id AND prod_id = @prod_id";
                            cmd.Parameters.AddWithValue("@user_id", userId);
                            cmd.Parameters.AddWithValue("@prod_id", productId);

                            var existingCartCount = cmd.ExecuteScalar();
                            cmd.Parameters.Clear(); 

                            int newCartCount;
                            decimal prodPrice;

                            cmd.CommandText = "SELECT prod_price FROM Product WHERE prod_id = @prod_id";
                            cmd.Parameters.AddWithValue("@prod_id", productId);
                            prodPrice = (decimal)cmd.ExecuteScalar();
                            cmd.Parameters.Clear();

                            if (existingCartCount != null)
                            {

                                newCartCount = Convert.ToInt32(existingCartCount) + quantity;
                                cmd.CommandText = "UPDATE Cart SET cart_count = @newCartCount, cart_totalprice = @cart_totalPrice WHERE user_id = @user_id AND prod_id = @prod_id";
                                cmd.Parameters.AddWithValue("@newCartCount", newCartCount);
                                cmd.Parameters.AddWithValue("@cart_totalPrice", newCartCount * prodPrice);
                                cmd.Parameters.AddWithValue("@user_id", userId);
                                cmd.Parameters.AddWithValue("@prod_id", productId);
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear(); 

                                response = new { success = true, message = "Product quantity updated successfully" };
                            }
                            else
                            {
                                newCartCount = quantity;
                                cmd.CommandText = "INSERT INTO Cart (user_id, prod_id, cart_count, cart_totalprice) VALUES (@user_id, @prod_id, @cart_count, @cart_totalprice)";
                                cmd.Parameters.AddWithValue("@user_id", userId);
                                cmd.Parameters.AddWithValue("@prod_id", productId);
                                cmd.Parameters.AddWithValue("@cart_count", newCartCount);
                                cmd.Parameters.AddWithValue("@cart_totalprice", newCartCount * prodPrice);
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear(); 

                                response = new { success = true, message = "Product added to cart successfully" };
                            }


                            cmd.CommandText = "UPDATE Product SET prod_stock = prod_stock - @quantity WHERE prod_id = @prod_id";
                            cmd.Parameters.AddWithValue("@quantity", quantity);
                            cmd.Parameters.AddWithValue("@prod_id", productId);
                            cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear(); 
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


        [HttpPost]
        public ActionResult UpdateQuantity(int productId, int newQuantity)
        {
            var userId = Session["UserId"];
            if (userId == null)
            {
                return Json(new { success = false, message = "User not logged in" });
            }

            using (var db = new SqlConnection(connStr))
            {
                db.Open();
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        int oldQuantity;
                        using (var cmd = db.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT cart_count FROM CART WHERE user_id = @userId AND prod_id = @productId";
                            cmd.Parameters.AddWithValue("@userId", userId);
                            cmd.Parameters.AddWithValue("@productId", productId);
                            oldQuantity = (int)cmd.ExecuteScalar();
                        }

                        using (var cmd = db.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = @"
                        UPDATE CART
                        SET cart_count = @newQuantity,
                            cart_totalprice = @newQuantity * (SELECT prod_price FROM PRODUCT WHERE prod_id = @productId)
                        WHERE user_id = @userId AND prod_id = @productId";
                            cmd.Parameters.AddWithValue("@newQuantity", newQuantity);
                            cmd.Parameters.AddWithValue("@productId", productId);
                            cmd.Parameters.AddWithValue("@userId", userId);
                            cmd.ExecuteNonQuery();
                        }

                        /*using (var cmd = db.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = @"
                        UPDATE PRODUCT
                        SET prod_stock = prod_stock + @oldQuantity - @newQuantity
                        WHERE prod_id = @productId";
                            cmd.Parameters.AddWithValue("@newQuantity", newQuantity);
                            cmd.Parameters.AddWithValue("@oldQuantity", oldQuantity);
                            cmd.Parameters.AddWithValue("@productId", productId);
                            cmd.ExecuteNonQuery();
                        }*/

                        transaction.Commit();
                        return Json(new { success = true });
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return Json(new { success = false, message = ex.Message });
                    }
                }
            }
        }

        [HttpPost]
        public ActionResult DeleteCartItem(int productId)
        {
            try
            {
                using (var connection = new SqlConnection(connStr))
                {
                    connection.Open();

                    // Step 1: Get the cart item details to determine quantity
                    string cartQuery = "SELECT * FROM CART WHERE prod_id = @productId";
                    using (var cartCommand = new SqlCommand(cartQuery, connection))
                    {
                        cartCommand.Parameters.AddWithValue("@productId", productId);
                        using (var cartReader = cartCommand.ExecuteReader())
                        {
                            if (cartReader.Read())
                            {
                                int cartId = Convert.ToInt32(cartReader["cart_id"]);
                                int cartQuantity = Convert.ToInt32(cartReader["cart_count"]);

                                // Step 2: Delete the item from the cart
                                string deleteCartQuery = "DELETE FROM CART WHERE cart_id = @cartId";
                                using (var deleteCartCommand = new SqlCommand(deleteCartQuery, connection))
                                {
                                    deleteCartCommand.Parameters.AddWithValue("@cartId", cartId);
                                    deleteCartCommand.ExecuteNonQuery();
                                }

                                // Step 3: Restore stock in the PRODUCT table
                                string updateStockQuery = "UPDATE PRODUCT SET prod_stock = prod_stock + @cartQuantity WHERE prod_id = @productId";
                                using (var updateStockCommand = new SqlCommand(updateStockQuery, connection))
                                {
                                    updateStockCommand.Parameters.AddWithValue("@cartQuantity", cartQuantity);
                                    updateStockCommand.Parameters.AddWithValue("@productId", productId);
                                    updateStockCommand.ExecuteNonQuery();
                                }

                                // If everything is successful, return success JSON
                                return Json(new { success = true });
                            }
                            else
                            {
                                // Cart item not found, return error JSON
                                return Json(new { success = false, error = "Cart item not found." });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions here, return error JSON
                Console.WriteLine("Error: " + ex.Message);
                return Json(new { success = false, error = "An error occurred while processing the request." });
            }
        }

    }
}