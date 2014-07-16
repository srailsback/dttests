using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace dttests.Models
{
    public static class LinqHelpers
    {

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
            return collection;
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, bool desc = false)
        {
            return (IQueryable<T>)OrderBy((IQueryable)source, propertyName, desc);
        }

        public static IQueryable OrderBy(this IQueryable source, string propertyName, bool desc = false)
        {
            var x = Expression.Parameter(source.ElementType, "x");
            var selector = Expression.Lambda(Expression.PropertyOrField(x, propertyName), x);
            if (!desc)
            {
                return source.Provider.CreateQuery(
                    Expression.Call(typeof(Queryable), "OrderBy", new Type[] { source.ElementType, selector.Body.Type },
                         source.Expression, selector));
            }
            else
            {
                return source.Provider.CreateQuery(
                    Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { source.ElementType, selector.Body.Type },
                         source.Expression, selector));

            }
        }


        //public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, object[] ordering)
        //{
        //    var query = source;
        //    var arr = ordering.ToArray();

        //    // order by the first condition
        //    if (arr.Count() == 0)
        //    {
        //        throw new ArgumentException("Nothing to order by");
        //    }

        //    for (int i = 0; i < arr.Count(); i++)
        //    {
        //        string[] element = (string[])arr[0];
        //        var x = Expression.Parameter(source.ElementType, "x");
        //        var selector = Expression.Lambda(Expression.PropertyOrField(x, element[0].ToString()), x);

        //        if (element[1].ToString() == "True")
        //        {
        //            // asc
        //            //query.Provider.CreateQuery(
        //            //    Expression.Call(typeof(Queryable), i == 0 ? "OrderBy" : "ThenBy", new Type[] { source.ElementType, selector.Body.Type },
        //            //         source.Expression, selector));
                    
        //            query.Provider.CreateQuery(
        //                Expression.Call(typeof(Queryable), i == 0 ? "OrderBy" : "ThenBy", new Type[] { source.ElementType, selector.Body.Type },
        //                     source.Expression, selector));
        //        }
        //        else
        //        {
        //            // desc
        //            query.Provider.CreateQuery(
        //                Expression.Call(typeof(Queryable), i == 0 ? "OrderByDescending" : "ThenByDescending", new Type[] { source.ElementType, selector.Body.Type },
        //                     source.Expression, selector));
        //        }
        //    }

        //    return query;
        //}


        public static TSource Set<TSource>(this TSource input, Action<TSource> updater)
        {
            updater(input);
            return input;

        }
    }
}