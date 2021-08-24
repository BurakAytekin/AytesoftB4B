using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.ComponentModel;
using Aytesoft.Models;

namespace Aytesoft.DataAccessLayer
{
    [DataObject(true)]
    public static class ProductDB 
    {
        private static string getConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["MySQLConnectionString"].ConnectionString;
        }

        public static MySqlCommand generateCommand(string procedurename)
        {   
            MySqlCommand cmd = new MySqlCommand(procedurename, new MySqlConnection(getConnectionString()));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            return cmd;
        }

        public static List<Product> generateProductList(MySqlCommand cmd)
        {
            List<Product> productList = new List<Product>();
            MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                Product product = new Product();
                product.id = Convert.ToInt32(dr["id"].ToString());
                product.code = dr["code"].ToString();
                product.name = dr["name"].ToString();
                product.price = dr["price"].ToString();
                product.imagepath = dr["image_path"].ToString();
                product.stock = dr["stock"].ToString();
                product.status = Convert.ToInt32(dr["status"].ToString());
                productList.Add(product);
            }
            dr.Close();
            cmd.Connection.Close();
            return productList;

        }


        public static List<Order> generateOrderList(MySqlCommand cmd)
        {
            List<Order> orderList = new List<Order>();
            MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                Order or = new Order();
                or.id = Convert.ToInt32(dr["id"].ToString());
                or.date = dr["date"].ToString();
                or.status = Convert.ToInt32(dr["accepted"].ToString());
                or.productprice = Convert.ToInt32(dr["price"].ToString());
                orderList.Add(or);
                string valofquantity = dr["productid"].ToString();
                or.quantity = valofquantity.Length - valofquantity.Replace("<", "").Length;

            }
            cmd.Connection.Close();
            dr.Close();
            return orderList;
        }
        public static List<OrderDetail> generateOrderDetailList(MySqlCommand cmd)
        {
            List<OrderDetail> orderDetailList = new List<OrderDetail>();
            MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                OrderDetail od = new OrderDetail();
                od.id = Convert.ToInt32(dr["id"].ToString());
                od.orderid = Convert.ToInt32(dr["orderid"].ToString());
                od.productid = Convert.ToInt32(dr["productid"].ToString());
                od.quantity = Convert.ToInt32(dr["quantity"].ToString());
                od.productprice = Convert.ToInt32(dr["productprice"].ToString());
                od.product = getProductById(od.productid);
                orderDetailList.Add(od);
            }
            cmd.Connection.Close();
            dr.Close();
            return orderDetailList;
        }


        public static List<Basket> generateBasketList(MySqlCommand cmd, int userid)
        {
            List<Basket> basketList = new List<Basket>();
            MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                Basket basket = new Basket();
                basket.id = Convert.ToInt32(dr["id"].ToString());
                basket.productid = Convert.ToInt32(dr["productid"].ToString());
                basket.productprice = Convert.ToInt32(dr["productprice"].ToString());
                basket.quantity = Convert.ToInt32(dr["quantity"].ToString());
                basket.productname = dr["name"].ToString();
                basket.price = Convert.ToInt32(dr["price"].ToString());
                basket.userid = userid;
                basketList.Add(basket);
            }
            dr.Close();
            cmd.Connection.Close();
            return basketList;

        }



        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Product> getProductList()
        {
            MySqlCommand cmd = generateCommand("list_product");
            List<Product> ProductList = generateProductList(cmd);
            return ProductList;
        }


        [DataObjectMethod(DataObjectMethodType.Select)]
        public static Product getProductById(int id)
        {
            MySqlCommand cmd = generateCommand("getProductById");
            cmd.Parameters.Add(new MySqlParameter("param1", id));
            MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            Product product = new Product();
            dr.Read();
            if(dr["id"].ToString() != "")
            {
                product.id = Convert.ToInt32(dr["id"]);
                product.code = dr["code"].ToString();
                product.name = dr["name"].ToString();
                product.price = dr["price"].ToString();
            }
            cmd.Connection.Close();
            return product;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static User getUserWithLogin(string username, string pass)
        {
            MySqlCommand cmd = generateCommand("getUserWithLogin");
            cmd.Parameters.Add(new MySqlParameter("user_name", username));
            cmd.Parameters.Add(new MySqlParameter("pass", pass));
            MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            User user = new User();
            dr.Read();
            if(dr.HasRows)
            {
                user.id = Convert.ToInt32(dr["id"].ToString());
                user.username = username;
                user.password = pass;
                user.name = dr["name"].ToString();
            }
            cmd.Connection.Close();
            return user;
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public static int deleteProduct(int id)
        {
            MySqlCommand cmd = generateCommand("delete_product");
            cmd.Parameters.Add(new MySqlParameter("param1", id));
            int i = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return i;
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int insertProduct(Product product)
        {
            MySqlCommand cmd = generateCommand("insert_product");
            cmd.Parameters.Add(new MySqlParameter("param1", product.code));
            cmd.Parameters.Add(new MySqlParameter("param2", product.name));
            cmd.Parameters.Add(new MySqlParameter("param3", product.price));
            cmd.Parameters.Add(new MySqlParameter("param4", product.imagepath));
            cmd.Parameters.Add(new MySqlParameter("param5", product.stock));
            int i = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return i;
        }

        [DataObjectMethod(DataObjectMethodType.Update)]
        public static int updateProduct(Product product)
        {
            MySqlCommand cmd = generateCommand("update_product");
            cmd.Parameters.Add(new MySqlParameter("paramkey", Convert.ToInt32(product.id)));
            cmd.Parameters.Add(new MySqlParameter("param2", product.code));
            cmd.Parameters.Add(new MySqlParameter("param3", product.name));
            cmd.Parameters.Add(new MySqlParameter("param4", product.price));
            cmd.Parameters.Add(new MySqlParameter("param5", product.imagepath));
            cmd.Parameters.Add(new MySqlParameter("param6", product.stock));
            cmd.Parameters.Add(new MySqlParameter("param7", product.status));
            int i = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return i;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Product> getProductByName(string name)
        {
            MySqlCommand cmd = generateCommand("getProductByName");
            cmd.Parameters.Add(new MySqlParameter("param1",name));
            List<Product> ProductList = generateProductList(cmd);
            return ProductList;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Product> getProductByCode(string code)
        {
            MySqlCommand cmd = generateCommand("getProductByCode");
            cmd.Parameters.Add(new MySqlParameter("param1", code));
            List<Product> ProductList = generateProductList(cmd);
            return ProductList;
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int insertBasketItem(Basket bt)
        {
            MySqlCommand cmd = generateCommand("insertBasketItem");
            cmd.Parameters.Add(new MySqlParameter("param1", Convert.ToInt32(bt.productid)));
            cmd.Parameters.Add(new MySqlParameter("param2", bt.quantity));
            cmd.Parameters.Add(new MySqlParameter("param3", bt.price));
            cmd.Parameters.Add(new MySqlParameter("param4", bt.userid));
            int i = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return i;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static List<Basket> getBasketItems(int userid)
        {
            MySqlCommand cmd = generateCommand("getBasketItems");
            cmd.Parameters.Add(new MySqlParameter("param1", userid));
            List<Basket> basketList = generateBasketList(cmd, userid);
            return basketList;
        }

        [DataObjectMethod(DataObjectMethodType.Delete)]
        public static int deleteBasketItem(int id)
        {

            MySqlCommand cmd = generateCommand("deleteBasketItem");
            cmd.Parameters.Add(new MySqlParameter("param1", id));
            int i = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return i;
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public static int getLastOrderId()
        {
            MySqlCommand cmd = generateCommand("getLastOrderId");
            int i = Convert.ToInt32(cmd.ExecuteScalar());
            return i;
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int insertOrderDetail(OrderDetail orderdetail, int id)
        {
            MySqlCommand cmd = generateCommand("insertOrderDetail");
            cmd.Parameters.Add(new MySqlParameter("param1", orderdetail.orderid));
            cmd.Parameters.Add(new MySqlParameter("param2", orderdetail.productid));
            cmd.Parameters.Add(new MySqlParameter("param3", orderdetail.quantity));
            cmd.Parameters.Add(new MySqlParameter("param4", orderdetail.productprice));
            cmd.Parameters.Add(new MySqlParameter("param5", id));
            int i = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return i;
        }

        [DataObjectMethod(DataObjectMethodType.Insert)]
        public static int insertOrder(int id)
        {
            Order or = new Order();
            List<Basket> basket = getBasketItems(id);
            foreach (var item in basket)
            {
                or.productid += "<" + item.productid + ">";
                or.productprice += item.productprice * item.quantity;
                or.quantity += item.quantity;
            }
            or.productprice = or.productprice * 0.9;
            or.productprice = or.productprice + or.productprice * 0.18;
            or.userid = id;
            MySqlCommand cmd = generateCommand("insertOrder");
            cmd.Parameters.Add(new MySqlParameter("param1", or.productid));
            cmd.Parameters.Add(new MySqlParameter("param2", or.productprice));
            cmd.Parameters.Add(new MySqlParameter("param3", id));
            int i = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            int inserted_order_id = getLastOrderId();
            foreach (var item in basket)
            {
                OrderDetail od = new OrderDetail();
                od.productid = item.productid;
                od.productprice = item.productprice;
                od.quantity = item.quantity;
                od.orderid = inserted_order_id;
                insertOrderDetail(od, id);
            }
            cmd.Connection.Close();
            return i;
        }

        public static List<OrderDetail> getOrderDetail(int orderid)
        {
            MySqlCommand cmd = generateCommand("getOrderDetailWithId");
            cmd.Parameters.Add(new MySqlParameter("order_id", orderid));
            return generateOrderDetailList(cmd);
        }

        public static List<Order> getOrder(int userid)
        {
            MySqlCommand cmd = generateCommand("getOrderWithId");
            cmd.Parameters.Add(new MySqlParameter("user_id", userid));
            return generateOrderList(cmd);
        }
        public static List<Order> getOrderWithDate(DateTime startdate, DateTime enddate)
        {
            MySqlCommand cmd = generateCommand("getOrderWithDate");
            string startDate = startdate.ToString("yyyy-MM-dd HH:mm:ss");
            string endDate = enddate.ToString("yyyy-MM-dd HH:mm:ss");
            cmd.Parameters.Add(new MySqlParameter("param1", startDate));
            cmd.Parameters.Add(new MySqlParameter("param2", endDate));
            return generateOrderList(cmd);
        }
    }
}