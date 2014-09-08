﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    public static class AsyncZip
    {
        public static async Task<IEnumerable<TResult>> ZipAsync<TFirst, TSecond, TResult>(
            this Task<IEnumerable<TFirst>> first, 
            Task<IEnumerable<TSecond>> second, 
            Func<TFirst, TSecond, TResult> resultSelector)
        {
            Contract.Requires<ArgumentNullException>(first != null);
            Contract.Requires<ArgumentNullException>(second != null);
            Contract.Requires<ArgumentNullException>(resultSelector != null);

            return (await first).Zip(await second, resultSelector);
        }

        public static async Task<IEnumerable<TResult>> ZipAsync<TFirst, TSecond, TResult>(
            this IEnumerable<Task<TFirst>> first,
            IEnumerable<Task<TSecond>> second,
            Func<TFirst, TSecond, TResult> resultSelector)
        {
            Contract.Requires<ArgumentNullException>(first != null);
            Contract.Requires<ArgumentNullException>(second != null);
            Contract.Requires<ArgumentNullException>(resultSelector != null);

            return (await Task.WhenAll(first)).Zip(await Task.WhenAll(second), resultSelector);
        }
    }
}
