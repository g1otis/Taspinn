﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Taspin.Data.Models;

namespace Taspin.Data.Dac
{
    public class ShoppingListDac
    {
        private const string selectShoppingListSP = "retrieveShoppingList";
        private const string deleteShoppingListItemSP = "deleteItemFromShoppingList";

        private readonly string connstring;

        public ShoppingListDac(DatabaseOptions databaseOptions)
        {
            connstring = databaseOptions.ConnectionString;
        }

        public ShoppingList SelectShoppingList(string userNameToSelect)
        {
            using (var db = new SqlConnection(connstring))
            {
                return db.Query<ShoppingList>(selectShoppingListSP, new { input_username = userNameToSelect }, commandType: CommandType.StoredProcedure).First();
            }
        }

        public void DeleteItem(int shoppingListToItemId)
        {
            using (var db = new SqlConnection(connstring))
            {
                db.Query(deleteShoppingListItemSP, new { item_objid = shoppingListToItemId }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
