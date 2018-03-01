using System;
using System.Diagnostics;
//using System.Data.SqlClient;

namespace NorthwindDemo
{
    public class DataHelper
    {
        public static OrderList Get100Orders(string connectionString)
        {
            const string GetOrdersQuery = "select top(100) Orders.OrderID, " +
                " Orders.OrderDate, Orders.CustomerID, Orders.EmployeeID, " +
                " sum(Details.Quantity * Details.UnitPrice) as OrderTotal " +
                " from Orders " +
                " left join [Order Details] Details on Details.OrderID = Orders.OrderID " +
                " group by Orders.OrderID, Orders.CustomerID, Orders.OrderDate, Orders.EmployeeID " +
                " order by Orders.OrderID desc";
            var orders = new OrderList();
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(connectionString))
            //    {
            //        conn.Open();
            //        if (conn.State == System.Data.ConnectionState.Open)
            //        {
            //            using (SqlCommand cmd = conn.CreateCommand())
            //            {
            //                cmd.CommandText = GetOrdersQuery;
            //                Console.WriteLine("Executing reader");
            //                using (SqlDataReader reader = cmd.ExecuteReader())
            //                {
            //                    while (reader.Read())
            //                    {
            //                        var order = new OrderHeader();
            //                        order.OrderID = reader.GetInt32(0);
            //                        order.CustomerID = reader.GetString(2);
            //                        order.EmployeeID = reader.GetInt32(3);
            //                        order.OrderTotal = reader.GetDecimal(4);
            //                        orders.Add(order);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    return orders;
            //}
            //catch (Exception eSql)
            //{
            //    Debug.WriteLine("Exception: " + eSql.Message);
            //}
            return null;
        }

        public static ProductList GetProducts(string connectionString)
        {
            const string GetProductsQuery = "select ProductID, ProductName, QuantityPerUnit," +
               " UnitPrice, UnitsInStock " +
               " from Products where Discontinued = 0";

            var products = new ProductList();
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(connectionString))
            //    {
            //        conn.Open();
            //        if (conn.State == System.Data.ConnectionState.Open)
            //        {
            //            using (SqlCommand cmd = conn.CreateCommand())
            //            {
            //                cmd.CommandText = GetProductsQuery;
            //                using (SqlDataReader reader = cmd.ExecuteReader())
            //                {
            //                    while (reader.Read())
            //                    {
            //                        var product = new Product();
            //                        product.ProductID = reader.GetInt32(0);
            //                        product.ProductName = reader.GetString(1);
            //                        product.QuantityPerUnit = reader.GetString(2);
            //                        product.UnitPrice = reader.GetDecimal(3);
            //                        product.UnitsInStock = reader.GetInt16(4);
            //                        products.Add(product);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    return products;
            //}
            //catch (Exception eSql)
            //{
            //    Debug.WriteLine("Exception: " + eSql.Message);
            //}
            return null;
        }

        public static string SaveOrderAndDetails(Order order, string connectionString)
        {
            const string InsertOrderQuery = "insert into Orders (CustomerID, EmployeeID, OrderDate) " +
                "values (@CustomerID, @EmployeeID, @OrderDate) select SCOPE_IDENTITY()";

            const string InsertOrderDetailQuery = "insert into [Order Details] " +
                " (OrderID, ProductID, UnitPrice, Quantity, Discount) " +
                " values (@OrderID, @ProductID, @UnitPrice, @Quantity, @Discount) ";

            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(connectionString))
            //    {
            //        conn.Open();
            //        if (conn.State == System.Data.ConnectionState.Open)
            //        {
            //            Debug.WriteLine("Creating SQL command...");
            //            using (SqlCommand cmd = conn.CreateCommand())
            //            {
            //                cmd.CommandText = InsertOrderQuery;
            //                cmd.Parameters.Clear();
            //                cmd.Parameters.Add(new SqlParameter("CustomerID", order.CustomerID));
            //                cmd.Parameters.Add(new SqlParameter("EmployeeID", order.EmployeeID));
            //                cmd.Parameters.Add(new SqlParameter("OrderDate", order.OrderDate));
            //                var orderID = cmd.ExecuteScalar();
            //                Debug.WriteLine("Inserted order {0}", orderID.ToString());

            //                foreach (OrderDetail detail in order.OrderDetails)
            //                {
            //                    cmd.CommandText = InsertOrderDetailQuery;
            //                    cmd.Parameters.Clear();
            //                    cmd.Parameters.Add(new SqlParameter("OrderID", orderID));
            //                    cmd.Parameters.Add(new SqlParameter("ProductID", detail.ProductID));
            //                    cmd.Parameters.Add(new SqlParameter("UnitPrice", detail.UnitPrice));
            //                    cmd.Parameters.Add(new SqlParameter("Quantity", detail.Quantity));
            //                    cmd.Parameters.Add(new SqlParameter("Discount", detail.Discount));
            //                    cmd.ExecuteNonQuery();
            //                    Debug.WriteLine("Inserted line item for product ID {0}", detail.ProductID.ToString());
            //                }
            //                return orderID.ToString();
            //            }
            //        }
            //    }
            //}
            //catch (Exception eSql)
            //{
            //    Debug.WriteLine("Exception " + eSql.Message);
            //}
            return "";
        }
    }
}

