using System;


namespace Railways.Models
{
    public class Route
    {
        public Stop Start { get; set; }
        public Stop Dest { get; set; }
        public double Weight { get; set; }
        public Route(){}
        ///Add a Route to the Network with START and END
        public Route(Stop START, Stop END)
        {
            if(null != START && null != END)
            {
                Start = START;
                Dest = END;
            }
            else
            {
                throw new Exception("Can not build this Route!");
            }
        }
        ///Add a Route to the Network with START, END and DISTANCE
        public Route(Stop START, Stop END, double DISTANCE)
        {
            if(null != START && null != END)
            {
                Start = START;
                Dest = END;
            }
            else
            {
                throw new Exception("Can't build the Route without START or END!");
            }
            Weight = DISTANCE; 
        }
        ///print out the route
        public override string ToString()
        {
            string s = "From Stop " + Start.ToString()
            + " to " + Dest.ToString()
            + ", Distance is " + Weight.ToString();
            return s;
        }

    }
}