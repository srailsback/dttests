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

        //public static IEnumerable<T> OrderByMany<T>(this IEnumerable<T> enumerable,
        //    params Expression<Func<T, object>>[] expressions)
        //{
        //    if (expressions.Length == 1)
        //        return enumerable.OrderBy(expressions[0].Compile());

        //    var query = enumerable.OrderBy(expressions[0].Compile());
        //    for (int i = 1; i < expressions.Length; i++)
        //    {
        //        query = query.ThenBy(expressions[i].Compile());
        //    }
        //    return query;

        //}


        public static IQueryable<T> OrderByMany<T>(this IQueryable<T> source, object[] ordering)
        {
            if (ordering.Length == 0 ) {
                throw new ArgumentException("No orders provided.");
            }


            for (int i = 0; i < ordering.Length; i++)
            {                
                //string[] item = (string[]) ordering[i];
                //var x = Expression.Parameter(source.ElementType, "x");
                //var selector = Expression.Lambda(Expression.PropertyOrField(x, item[0]), x);

                if (i == 0)
                {
                    //source.OrderBy(x => x.GetType()
                    //source.Provider.CreateQuery(
                    //    Expression.Call(typeof(Queryable),
                    //        item[1].ToLower().Contains("asc") ? "OrderBy" : "OrderByDescending", new Type[] { 
                    //            source.ElementType, selector.Body.Type },
                    //                         source.Expression, selector));
                }
                else
                {
                    //source.Provider.CreateQuery(
                    //    Expression.Call(typeof(Queryable),
                    //        item[1].ToLower().Contains("asc") ? "ThenBy" : "ThenByDescending", new Type[] { 
                    //            source.ElementType, selector.Body.Type },
                    //                         source.Expression, selector));

                }
            }

            return source;
        }

        public static TSource Set<TSource>(this TSource input, Action<TSource> updater)
        {
            updater(input);
            return input;
        }
    }
}