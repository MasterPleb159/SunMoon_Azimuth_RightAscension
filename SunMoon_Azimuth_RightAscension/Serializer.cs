using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace SunMoon_Azimuth_RightAscension
{
    public class Serializer
    {
        private float xValue;
        private float yValue;

        public Serializer(float x, float y)
        {
            this.XValue = x;
            this.YValue = y;
        }

        public float XValue { get => xValue; set => xValue = value; }
        public float YValue { get => yValue; set => yValue = value; }

        public string SendJson()
        {
            string value = JsonConvert.SerializeObject(this);
            return value;
        }
    }

    
}
