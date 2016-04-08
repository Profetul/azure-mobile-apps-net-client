// ----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ----------------------------------------------------------------------------

using Microsoft.WindowsAzure.MobileServices.Sync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.Query;
using Newtonsoft.Json.Linq;
using SQLite.Net;
using Microsoft.WindowsAzure.MobileServices;

namespace Microsoft.WindowsAzure.MobileServices.SQLiteAsyncStore
{
    /// <summary>
    /// SQLiteConnectionWithLock based implementation of MobileServiceSQLiteStore
    /// Added extensibility for developers that don't use SQLite only for Azure entities
    /// </summary>
    public class MobileServiceSQLiteAsyncStore : MobileServiceLocalStore
    {
        /// <summary>
        /// SQLiteConnectionWithLock supports locking to ensures thread safety. 
        /// Note: the ori
        /// </summary>
        protected SQLiteConnectionWithLock _connection;

        private Dictionary<string, TableDefinition> tableMap = new Dictionary<string, TableDefinition>(StringComparer.OrdinalIgnoreCase);

        protected MobileServiceSQLiteAsyncStore() : base() { }

        public MobileServiceSQLiteAsyncStore(SQLiteConnectionWithLock connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            _connection = connection;
        }


        public override async Task InitializeAsync()
        {
            //TODO
            await Task.Yield();
        }

        public override void DefineTable(string tableName, JObject item)
        {
            if(tableName == null)
            {
                throw new ArgumentNullException(nameof(tableName));
            }

            if(item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            //TODO not important for now

            JToken ignored;
            if (!item.TryGetValue(MobileServiceSystemColumns.Id, StringComparison.OrdinalIgnoreCase, out ignored))
            {
                item[MobileServiceSystemColumns.Id] = String.Empty;
            }

            var tableDefinition = (from property in item.Properties()
                                   let storeType = SqlHelpers.GetStoreType(property.Value.Type, allowNull: false)
                                   select new ColumnDefinition(property.Name, property.Value.Type, storeType))
                                  .ToDictionary(p => p.Name, StringComparer.OrdinalIgnoreCase);

            var sysProperties = GetSystemProperties(item);
        }

        public void DefineTable<T>() where T : class
        {
            //TODO let's do a real table definition
        }

        public override Task DeleteAsync(MobileServiceTableQueryDescription query)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteAsync(string tableName, IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public override Task<JObject> LookupAsync(string tableName, string id)
        {
            throw new NotImplementedException();
        }

        public override Task<JToken> ReadAsync(MobileServiceTableQueryDescription query)
        {
            throw new NotImplementedException();
        }

        public override Task UpsertAsync(string tableName, IEnumerable<JObject> items, bool ignoreMissingColumns)
        {
            throw new NotImplementedException();
        }

        private static MobileServiceSystemProperties GetSystemProperties(JObject item)
        {
            var sysProperties = MobileServiceSystemProperties.None;

            if (item[MobileServiceSystemColumns.Version] != null)
            {
                sysProperties = sysProperties | MobileServiceSystemProperties.Version;
            }
            if (item[MobileServiceSystemColumns.CreatedAt] != null)
            {
                sysProperties = sysProperties | MobileServiceSystemProperties.CreatedAt;
            }
            if (item[MobileServiceSystemColumns.UpdatedAt] != null)
            {
                sysProperties = sysProperties | MobileServiceSystemProperties.UpdatedAt;
            }
            if (item[MobileServiceSystemColumns.Deleted] != null)
            {
                sysProperties = sysProperties | MobileServiceSystemProperties.Deleted;
            }
            return sysProperties;
        }
    }
}
