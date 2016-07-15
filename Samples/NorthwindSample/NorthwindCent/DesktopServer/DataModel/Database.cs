//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// This code is licensed under the MIT License (MIT).
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;

namespace NorthwindCent.DataModel
{
    /// <summary>
    /// Provide methods to query SQL CE database for required data
    /// </summary>
    public class Database
    {
        private string connectionString;

        public Database(string dbFilePath)
        {
            this.connectionString = "Data Source='" + dbFilePath + "'";
        }
        /// <summary>
        /// Get data of product categrories from database
        /// </summary>
        public IEnumerable<Category> GetCategories()
        {
            Console.WriteLine("Loading database from {0}", connectionString);
            using (var db = new SqlCeConnection(connectionString))
            {
                db.Open();

                var cmd = new SqlCeCommand("SELECT [CategoryID],[CategoryName],[Description] FROM [Categories];", db);
                var reader = cmd.ExecuteReader();

                var results = new List<Category>();
                while (reader.Read())
                {
                    results.Add(new Category
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                    });
                }

                return results;
            }
        }
        /// <summary>
        /// Get data of all products for the given product category
        /// </summary>
        /// <param name="categoryId">
        /// Category Id for product category to query for.
        /// </param>
        public IEnumerable<Product> GetProductsByCategory(int categoryId)
        {
            Console.WriteLine("Loading database from {0}", connectionString);
            using (var db = new SqlCeConnection(connectionString))
            {
                db.Open();

                var query = string.Format(
                    "SELECT [ProductID],[ProductName],[SupplierID],[CategoryID],[QuantityPerUnit],[UnitPrice],[UnitsInStock],[UnitsOnOrder],[ReorderLevel],[Discontinued] FROM [Products] WHERE [CategoryID] = {0}",
                    categoryId);
                var cmd = new SqlCeCommand(query, db);
                var reader = cmd.ExecuteReader();

                var results = new List<Product>();
                while (reader.Read())
                {
                    var t = reader.GetFieldType(5);
                    results.Add(new Product
                    {
                        ID = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        SupplierID = reader.GetInt32(2),
                        CategoryID = reader.GetInt32(3),
                        QuantityPerUnit = reader.GetString(4),
                        UnitPrice = (float)reader.GetDecimal(5),
                        UnitsInStock = reader.GetInt16(6),
                        UnitsOnOrder = reader.GetInt16(7),
                        ReorderLevel = reader.GetInt16(8),
                        Discontinued = reader.GetBoolean(9),
                    });
                }

                return results;
            }
        }
    }
}
