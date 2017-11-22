using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace SunMoon_Azimuth_RightAscension
{
    class Senddata
    {
        SerialPort myport = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);

        private float x;
        private float y;
        private float z;

        public float Z
        {
            get { return z; }
            set { z = value; }
        }

        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public Senddata(float x,float y)
        {
            this.x = x;
            this.y = y;
        }

        public Senddata()
        {
        }

        public void SendVec()
        {
            DataLayer dl = new DataLayer();
            dl.CreateTable();
            dl.InsertData();
            myport.Open();
            myport.WriteLine(new Serializer(x, y).SendJson());
            myport.Close();
        }

        public int[] RecceiveVec(int[] vecs)
        {
            int[] myvecs = vecs;
            return myvecs;
            
           

        }

        public void GetVec()
        {
            myport.Open();
            string values = myport.ReadLine();
            int length = values.Length;
            int split = length / 2;
            x = int.Parse(values.Substring(0,split));

        }
    }
}
