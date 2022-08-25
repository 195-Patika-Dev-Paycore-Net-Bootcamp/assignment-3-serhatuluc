using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PycApi.Extensions
{
    public static class Splitting
    {
        //This extension is used to split a list of container into certain size of sets and returns it as whole list of the sets
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> sourceList, int ListSize)
        {
            while (sourceList.Any())
            {
                yield return sourceList.Take(ListSize);
                sourceList = sourceList.Skip(ListSize);
            }
        }
    }
}
