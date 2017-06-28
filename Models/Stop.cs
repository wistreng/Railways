using System;

namespace Railways.Models
{
    public class Stop
    {
        public string Name {set;get;}
        public int Num {set;get;} //not used
        public Boolean Marked {set;get;}
        public Stop(){}
        ///Initial method for a Stop
        public Stop(string name, int num)
        {
            if(name != ""){
                Name = name;
                Num = num;
                Marked = false;
            }
        }
        ///Use to mark the stop when traversing
        public Boolean IsMarked()
        {
            return Marked;
        }
        ///Print out the stop Name
        public override string ToString()
        {
            return Name;
        }
    }
}