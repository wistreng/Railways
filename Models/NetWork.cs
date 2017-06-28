using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections;

namespace Railways.Models{
    public class NetWork
    {
        public List<Stop> Stops {set;get;}
        public List<Route> AllRoutes {set;get;}
        public Dictionary<Stop,List<Route>> NW;
        ///Initial method for NetWOrk
        public NetWork(){
            Stops = new List<Stop>();
            AllRoutes = new List<Route>();
            NW = new Dictionary<Stop,List<Route>>();
        }
        public void AddStop (string name, int num)
        {
            //判断是否添加重复stop
            Stops.Add(new Stop(name, num));
        }
        ///Find the node whose name is NAME
        public Stop FindStop(string NAME)
        {
            return Stops.FirstOrDefault(s => s.Name == NAME);
        }
        ///Add a route to the NetWork
        public void AddRoute (string from, string to, double weight)
        {
            //Determines whether duplicate routes are added
            if(FindRoute(from, to) == null)
            {
                AllRoutes.Add(new Route(FindStop(from), FindStop(to), weight));
            }
        }
        ///Find the Route in the NetWork given START and END
        public Route FindRoute(string from, string to)
        {
            return AllRoutes.FirstOrDefault(x => x.Start.Equals(FindStop(from)) && x.Dest.Equals(FindStop(to)));
        }
        ///All routes start from certain node s
        public List<Route> AdjRoutes(Stop s)
        {
            var tmpList = AllRoutes.Where(x => x.Start.Equals(s)).ToList();
            return tmpList;
        }
        public void GenNetWork(){
            foreach(Stop s in Stops)
            {
                NW.Add(s,AdjRoutes(s));
            }   
        }
        ///Print each node and its adjacent points for testing only
        public override string ToString() //仅用于测试
        {   
            string tmp = string.Empty;
            foreach(Stop s in Stops)
            {
                tmp += "The Routes start from "
                + s.ToString()
                + "AdjRoutes are ";
                foreach(Route x in AdjRoutes(s))
                {
                    tmp += x.ToString();
                }
            }
            return tmp;
        }
        
        
    }
}
