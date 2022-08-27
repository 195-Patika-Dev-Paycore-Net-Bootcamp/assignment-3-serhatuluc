using PycApi.Model;
using System.Collections.Generic;
using System.Linq;

namespace PycApi.Extensions
{
    public static class Splitting
    {
        //This extension is used to split a list of container into certain size of sets and returns it as whole list of the sets
        public static List<List<T>> Split<T>(this List<T> sourceList, int N)
        {
            List<List<T>> outerList = new List<List<T>>();
            for(int i = 0; i < N; i++)
            {
                outerList.Add(new List<T>());
            }
            outerList = DoSplitting(sourceList, outerList,N);
            return outerList;
            
        }

        public static List<List<T>> DoSplitting<T>(List<T> sourceList, List<List<T>> outerList,int N)
        {
            int count = 0;
            foreach (var i in sourceList)
            {
                outerList[count % (N)].Add(i);
                count++;
            }
            return outerList;
        }
    }
}
