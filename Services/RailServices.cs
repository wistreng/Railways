using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections;

using Railways.Models;

namespace Railways.Services
{
    public class RailServices
    {   
        ///存放遍历出来的解
        private Solutions solu = new Solutions();
        ///存放路径的临时列表，遍历时作为路径的栈
        private List<Route> RouteStack = new List<Route>();
        
        ///<summary>查找所有可能的路径</summary>
        ///<param name="from">起点</param>
        ///<param name="to">终点</param>
        ///<param name="max">给定最大路车站数</param>
        ///<returns>返回一个存储路径信息和路径距离的Dictionary</returns>
        public Dictionary<string,double> FindRoutes(string from, string to, Double max)
        {
            NetWork nw = InitNetWork();
            if(max.Equals(0)) max = Double.PositiveInfinity;
            Traverse(nw.FindStop(from), nw.FindStop(to), nw, max);
            return this.solu.ResultSet;
        }

        ///<summary>遍历函数</summary>
        ///<param name="from">起点</param>
        ///<param name="to">终点</param>
        ///<param name="max">给定最大路车站数</param>
        ///<param name="nw">本例中用到的有向图</param>
        private void Traverse(Stop from, Stop to, NetWork nw, Double max)
        {
            from.Marked = true;
            
            foreach(Route v in nw.AdjRoutes(from))
            {
                //路径v入栈
                RouteStack.Add(v);
                //没有访问过且不是终点
                if( !v.Dest.IsMarked() && !v.Dest.Equals(to))
                {
                    //递归调用
                    Traverse(v.Dest, to, nw, max);
                }
                //访问到终点
                else if(v.Dest.Equals(to))
                {
                    if(RouteStack.Count <= max )
                    {
                        //将遍历出的结果存入
                        this.solu.AddResult(RouteStack);
                    }
                }
                else
                {
                    //访问过 且不是终点 就是说这是遍历中遇到了环 在本例中 因为每个节点只能访问一次 因此对这种情况不做处理
                }
                //路径v出栈
                RouteStack.Remove(v);
            }
            //遍历完毕 返回上一级 将该节点设置为未访问
            from.Marked = false;
        }
        
        ///<summary>判断路径是否联通</summary>
        ///<param name="s">用户按照一定格式输入的路径信息 e.g.“A-B-C"</param>
        ///<returns>0为不连通 或 返回路径长度</returns>
        public double IsSolution(string s)
        {
            NetWork nw = InitNetWork();
            double dist = 0;
            string[] tmp = s.Trim().Split('-');
            for(int i=0; i<tmp.Length-1; i++)
            {
                //在有向图中寻找路径是否存在
                if(nw.FindRoute(tmp[i],tmp[i+1])==null)
                {
                    return 0;
                }
                else
                {
                    dist += nw.FindRoute(tmp[i],tmp[i+1]).Weight;
                }
            }
            return dist;
        }
        ///<summary>返回最短路径</summary>
        ///<param name="resultSet">所有的可连通路径</param>
        public Dictionary<string,double> ShortestRoute(Dictionary<string,double> resultSet)
        {
            var tmpSet = new Dictionary<string,double>();
            //如果起点到终点的路径存在
            if(resultSet.Count > 0){
                var tmp = resultSet.OrderBy(x => x.Value).First();
                tmpSet.Add(tmp.Key, tmp.Value);
            }
            return tmpSet;
        }
        ///用本例中的数据集初始化有向图
        public NetWork InitNetWork()
        {
            var nw = new NetWork();
            nw.AddStop("A",0);
            nw.AddStop("B",1);
            nw.AddStop("C",2);
            nw.AddStop("D",3);
            nw.AddStop("E",4);
            nw.AddRoute("A","B",5);
            nw.AddRoute("A","D",5);
            nw.AddRoute("A","E",7);
            nw.AddRoute("B","C",4);
            nw.AddRoute("C","D",8);
            nw.AddRoute("C","E",2);
            nw.AddRoute("D","C",8);
            nw.AddRoute("D","E",6);
            nw.AddRoute("E","B",3);
            nw.GenNetWork();
            return nw;
        }
    }
}