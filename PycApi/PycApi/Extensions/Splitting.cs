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
            //An list of list is created
            List<List<T>> outerList = new List<List<T>>();

            //N number of lists are inserted
            for(int i = 0; i < N; i++)
            {
                outerList.Add(new List<T>());
            }

            //outer list is modified
            outerList = DoSplitting(sourceList, outerList,N);

            return outerList;
            
        }

        public static List<List<T>> DoSplitting<T>(List<T> sourceList, List<List<T>> outerList,int N)
        {
            //number is counted
            int count = 0;
            foreach (var i in sourceList)
            {
                //Mod of count to N is used to insert the object 
                outerList[count % (N)].Add(i);

                //Count is incremented 
                count++;
            }
            return outerList;
        }
    }
}
