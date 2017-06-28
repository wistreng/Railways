using System.Collections.Generic;
using Railways.Models;
using System.Linq;
namespace Railways.Models
{
    public class Solutions
    {
        ///Used to store traversal results, KEY is routes info and the VALUE is the distance.
        public Dictionary<string,double> ResultSet = new Dictionary<string,double>();
        ///Used to transform a set of paths obtained during traversal into path information and store them
        public void AddResult(List<Route> result)
        {
            string solu = "";
            double dist = 0;
            foreach(Route x in result)
            {
                dist += x.Weight;
                solu += x.Start.ToString()
                    + x.Dest.ToString() 
                    + "(" 
                    + x.Weight.ToString() 
                    + ")"
                    + " ";
            }
            ResultSet.Add(solu,dist);
        }
        ///Return the number of result sets
        public int NumOfResult() 
        {
            return ResultSet.Count;
        }

        
        
    }
}