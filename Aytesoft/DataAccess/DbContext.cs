using Aytesoft.Models.Domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DbContext : MySqlHelper
    {
        public static List<Product> GenerateProductList(MySqlCommand cmd)
        {
            List<Product> productList = new List<Product>();
            MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                Product product = new Product();
                product.ID = Convert.ToInt32(dr["id"].ToString());
                product.Code = dr["code"].ToString();
                product.Name = dr["name"].ToString();
                product.Price = dr["price"].ToString();
                product.ImagePath = dr["image_path"].ToString();
                product.Stock = dr["stock"].ToString();
                product.Status = Convert.ToInt32(dr["status"].ToString());
                productList.Add(product);
            }
            dr.Close();
            cmd.Connection.Close();
            return productList;

        }


        public static List<Order> GenerateOrderList(MySqlCommand cmd)
        {
            List<Order> orderList = new List<Order>();
            MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                Order or = new Order();
                or.ID = Convert.ToInt32(dr["id"].ToString());
                or.Date = dr["date"].ToString();
                or.Status = Convert.ToInt32(dr["accepted"].ToString());
                or.TotalPrice = Convert.ToInt32(dr["price"].ToString());
                orderList.Add(or);
                string valofquantity = dr["productid"].ToString();
                or.Quantity = valofquantity.Length - valofquantity.Replace("<", "").Length;

            }
            cmd.Connection.Close();
            dr.Close();
            return orderList;
        }
        public static List<OrderDetail> GenerateOrderDetailList(MySqlCommand cmd)
        {
            List<OrderDetail> orderDetailList = new List<OrderDetail>();
            MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                OrderDetail od = new OrderDetail();
                od.ID = Convert.ToInt32(dr["id"].ToString());
                od.OrderID = Convert.ToInt32(dr["orderid"].ToString());
                od.ProductID = Convert.ToInt32(dr["productid"].ToString());
                od.Quantity = Convert.ToInt32(dr["quantity"].ToString());
                od.ProductPrice = Convert.ToInt32(dr["productprice"].ToString());
                od.Product = GetProductById(od.ProductID);
                orderDetailList.Add(od);
            }
            cmd.Connection.Close();
            dr.Close();
            return orderDetailList;
        }


        public static List<Basket> GenerateBasketList(MySqlCommand cmd, int userid)
        {
            List<Basket> basketList = new List<Basket>();
            MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                Basket basket = new Basket();
                basket.ID = Convert.ToInt32(dr["id"].ToString());
                basket.ProductID = Convert.ToInt32(dr["productid"].ToString());
                basket.ProductPrice = Convert.ToInt32(dr["productprice"].ToString());
                basket.Quantity = Convert.ToInt32(dr["quantity"].ToString());
                basket.ProductName = dr["name"].ToString();
                basket.Price = Convert.ToInt32(dr["price"].ToString());
                basket.UserID = userid;
                basketList.Add(basket);
            }
            dr.Close();
            cmd.Connection.Close();
            return basketList;

        }



        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Product> GetProductList()
        {
            MySqlCommand cmd = GenerateCommand("list_product");
            List<Product> ProductList = GenerateProductList(cmd);
            return ProductList;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static Product GetProductById(int id)
        {
            MySqlCommand cmd = GenerateCommand("getProductById");
            cmd.Parameters.Add(new MySqlParameter("param1", id));
            MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            Product product = new Product();
            dr.Read();
            if (dr["id"].ToString() != "")
            {
                product.ID = Convert.ToInt32(dr["id"]);
                product.Code = dr["code"].ToString();
                product.Name = dr["name"].ToString();
                product.Price = dr["price"].ToString();
            }
            cmd.Connection.Close();
            return product;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static User GetUserWithLogin(string username, string pass)
        {
            MySqlCommand cmd = GenerateCommand("getUserWithLogin");
            cmd.Parameters.Add(new MySqlParameter("user_name", username));
            cmd.Parameters.Add(new MySqlParameter("pass", pass));
            MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            User user = new User();
            dr.Read();
            if (dr.HasRows)
            {
                user.ID = Convert.ToInt32(dr["id"].ToString());
                user.UserName = username;
                user.Password = pass;
                user.Name = dr["name"].ToString();
                user.Status = Convert.ToInt32(dr["status"].ToString());
            }
            cmd.Connection.Close();
            return user;
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public static int DeleteProduct(int id)
        {
            MySqlCommand cmd = GenerateCommand("delete_product");
            cmd.Parameters.Add(new MySqlParameter("param1", id));
            int i = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return i;
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int InsertProduct(Product product)
        {
            MySqlCommand cmd = GenerateCommand("insert_product");
            cmd.Parameters.Add(new MySqlParameter("param1", product.Code));
            cmd.Parameters.Add(new MySqlParameter("param2", product.Name));
            cmd.Parameters.Add(new MySqlParameter("param3", product.Price));
            cmd.Parameters.Add(new MySqlParameter("param4", product.ImagePath));
            cmd.Parameters.Add(new MySqlParameter("param5", product.Stock));
            int i = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return i;
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public static int UpdateProduct(Product product)
        {
            MySqlCommand cmd = GenerateCommand("update_product");
            cmd.Parameters.Add(new MySqlParameter("paramkey", Convert.ToInt32(product.ID)));
            cmd.Parameters.Add(new MySqlParameter("param2", product.Code));
            cmd.Parameters.Add(new MySqlParameter("param3", product.Name));
            cmd.Parameters.Add(new MySqlParameter("param4", product.Price));
            cmd.Parameters.Add(new MySqlParameter("param5", product.ImagePath));
            cmd.Parameters.Add(new MySqlParameter("param6", product.Stock));
            cmd.Parameters.Add(new MySqlParameter("param7", product.Status));
            int i = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return i;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Product> GetProductByName(string name)
        {
            MySqlCommand cmd = GenerateCommand("getProductByName");
            cmd.Parameters.Add(new MySqlParameter("param1", name));
            List<Product> ProductList = GenerateProductList(cmd);
            return ProductList;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Product> GetProductByCode(string code)
        {
            MySqlCommand cmd = GenerateCommand("getProductByCode");
            cmd.Parameters.Add(new MySqlParameter("param1", code));
            List<Product> ProductList = GenerateProductList(cmd);
            return ProductList;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Product> GetProductBySearch(string key)
        {
            MySqlCommand cmd = GenerateCommand("getProductBySearch");
            cmd.Parameters.Add(new MySqlParameter("param1", key));
            List<Product> ProductList = GenerateProductList(cmd);
            return ProductList;
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int InsertBasketItem(Basket bt)
        {
            MySqlCommand cmd = GenerateCommand("insertBasketItem");
            cmd.Parameters.Add(new MySqlParameter("param1", Convert.ToInt32(bt.ProductID)));
            cmd.Parameters.Add(new MySqlParameter("param2", bt.Quantity));
            cmd.Parameters.Add(new MySqlParameter("param3", bt.Price));
            cmd.Parameters.Add(new MySqlParameter("param4", bt.UserID));
            int i = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return i;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Basket> GetBasketItems(int userid)
        {
            MySqlCommand cmd = GenerateCommand("getBasketItems");
            cmd.Parameters.Add(new MySqlParameter("param1", userid));
            List<Basket> basketList = GenerateBasketList(cmd, userid);
            return basketList;
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public static int DeleteBasketItem(int id)
        {

            MySqlCommand cmd = GenerateCommand("deleteBasketItem");
            cmd.Parameters.Add(new MySqlParameter("param1", id));
            int i = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return i;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static int GetLastOrderId()
        {
            MySqlCommand cmd = GenerateCommand("getLastOrderId");
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            return i;
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int InsertOrderDetail(OrderDetail orderdetail, int id)
        {
            MySqlCommand cmd = GenerateCommand("insertOrderDetail");
            cmd.Parameters.Add(new MySqlParameter("param1", orderdetail.OrderID));
            cmd.Parameters.Add(new MySqlParameter("param2", orderdetail.ProductID));
            cmd.Parameters.Add(new MySqlParameter("param3", orderdetail.Quantity));
            cmd.Parameters.Add(new MySqlParameter("param4", orderdetail.ProductPrice));
            cmd.Parameters.Add(new MySqlParameter("param5", id));
            int i = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return i;
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int InsertOrder(int id)
        {
            Order or = new Order();
            List<Basket> basket = GetBasketItems(id);
            foreach (var item in basket)
            {
                or.ProductID += "<" + item.ProductID + ">";
                or.TotalPrice += item.ProductPrice * item.Quantity;
                or.Quantity += item.Quantity;
            }
            or.TotalPrice = or.TotalPrice * 0.9;
            or.TotalPrice = or.TotalPrice + or.TotalPrice * 0.18;
            or.UserID = id;
            MySqlCommand cmd = GenerateCommand("insertOrder");
            cmd.Parameters.Add(new MySqlParameter("param1", or.ProductID));
            cmd.Parameters.Add(new MySqlParameter("param2", or.TotalPrice));
            cmd.Parameters.Add(new MySqlParameter("param3", id));
            int i = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            int inserted_order_id = GetLastOrderId();
            foreach (var item in basket)
            {
                OrderDetail od = new OrderDetail();
                od.ProductID = item.ProductID;
                od.ProductPrice = item.ProductPrice;
                od.Quantity = item.Quantity;
                od.OrderID = inserted_order_id;
                InsertOrderDetail(od, id);
            }
            cmd.Connection.Close();
            return i;
        }

        public static List<OrderDetail> GetOrderDetail(int orderid)
        {
            MySqlCommand cmd = GenerateCommand("getOrderDetailWithId");
            cmd.Parameters.Add(new MySqlParameter("order_id", orderid));
            return GenerateOrderDetailList(cmd);
        }

        public static List<Order> GetOrders(int userid)
        {
            MySqlCommand cmd = GenerateCommand("getOrderWithId");
            cmd.Parameters.Add(new MySqlParameter("user_id", userid));
            return GenerateOrderList(cmd);
        }
        public static List<Order> GetOrderWithDate(DateTime startdate, DateTime enddate)
        {
            MySqlCommand cmd = GenerateCommand("getOrderWithDate");
            string startDate = startdate.ToString("yyyy-MM-dd HH:mm:ss");
            string endDate = enddate.ToString("yyyy-MM-dd HH:mm:ss");
            cmd.Parameters.Add(new MySqlParameter("param1", startDate));
            cmd.Parameters.Add(new MySqlParameter("param2", endDate));
            return GenerateOrderList(cmd);
        }
    }
}
