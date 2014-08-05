﻿// ----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// ----------------------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace Microsoft.WindowsAzure.MobileServices
{
    /// <summary>
    /// Provides extension methods on <see cref="IMobileServiceSyncTable"/>
    /// </summary>
    public static class MobileServiceSyncTableExtensions
    {
        /// <summary>
        /// Pulls all items from the associated remote table.
        /// </summary>
        public static Task PullAsync(this IMobileServiceSyncTable table)
        {
            return table.PullAsync(String.Empty);
        }

        /// <summary>
        /// Pulls all items that match the given query
        /// from the associated remote table.
        /// </summary>
        /// <param name="table">The instance of table to execute pull on.</param>
        /// <param name="query">
        /// An OData query that determines which items to 
        /// pull from the remote table.
        /// </param>
        public static Task PullAsync(this IMobileServiceSyncTable table, string query)
        {
            return table.PullAsync(null, query, null, cancellationToken: CancellationToken.None);
        }

        /// <summary>
        /// Pulls all items that match the given query
        /// from the associated remote table.
        /// </summary>
        /// <param name="table">The instance of table to execute pull on.</param>
        /// <param name="query">
        /// An OData query that determines which items to 
        /// pull from the remote table.
        /// </param>
        /// <returns>
        /// A task that completes when pull operation has finished.
        /// </returns>
        public static Task PullAsync<T, U>(this IMobileServiceSyncTable<T> table, IMobileServiceTableQuery<U> query)
        {
            return table.PullAsync(null, query, cancellationToken: CancellationToken.None);
        }

        /// <summary>
        /// Pulls all items that match the given query
        /// from the associated remote table.
        /// </summary>
        /// <param name="table">The instance of table to execute pull on.</param>
        /// <param name="queryKey">
        /// A string that uniquely identifies this query and is used to keep track of its sync state. Supplying this parameter enables incremental sync whenever the same key is used again.
        /// </param>
        /// <param name="query">
        /// An OData query that determines which items to 
        /// pull from the remote table.
        /// </param>
        /// <returns>
        /// A task that completes when pull operation has finished.
        /// </returns>
        public static Task PullAsync<T, U>(this IMobileServiceSyncTable<T> table, string queryKey, IMobileServiceTableQuery<U> query)
        {
            return table.PullAsync(queryKey, query, cancellationToken: CancellationToken.None);
        }

        /// <summary>
        /// Deletes all the items in local table
        /// </summary>
        /// <param name="table">The instance of table to execute pull on.</param>
        /// <returns>A task that completes when purge operation has finished.</returns>
        public static Task PurgeAsync(this IMobileServiceSyncTable table)
        {
            return table.PurgeAsync(String.Empty);
        }

        /// <summary>
        /// Deletes all the items in local table that match the query.
        /// </summary>
        /// <param name="table">The instance of table to execute pull on.</param>
        /// <param name="query">An OData query that determines which items to delete.</param>
        /// <returns>A task that completes when purge operation has finished.</returns>
        public static Task PurgeAsync(this IMobileServiceSyncTable table, string query)
        {
            return table.PurgeAsync(null, query, CancellationToken.None);
        }


        /// <summary>
        /// Deletes all the items in local table that match the query.
        /// </summary>
        /// <param name="table">The instance of table to execute pull on.</param>
        /// <param name="query">An OData query that determines which items to delete.</param>
        /// <returns>A task that completes when purge operation has finished.</returns>
        public static Task PurgeAsync<T, U>(this IMobileServiceSyncTable<T> table, IMobileServiceTableQuery<U> query)
        {
            return table.PurgeAsync(null, query, CancellationToken.None);
        }
    }
}
