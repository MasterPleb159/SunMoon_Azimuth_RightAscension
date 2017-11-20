﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SunMoon_Azimuth_RightAscension
{
  public  class New_Formula
    {
        public struct Position
    {
        public double Altitude {get;set;}
        public double Azimuth {get;set;}
    }

    // TO convert from degrees to radiants whenever needed
   private const double Deg2Rad = Math.PI / 180.0;  
   private const double Rad2Deg = 180.0 / Math.PI;  
        
 
   public static Position CalculateSunPosition(  
       DateTime dateTime, double latitude, double longitude)  
   {
            double latitudeInRad = latitude * Deg2Rad;
            double longitudeInRad = longitude * Deg2Rad;
       // Convert to UTC  
       dateTime = dateTime.ToUniversalTime();
 
       // Number of days from J2000.0.  
       double julianDate = 366 * dateTime.Year -  
           (int)((7.0 / 4.0) * (dateTime.Year +   
           (int)((dateTime.Month + 9.0) / 12.0))) +  
           (int)((275.0 * dateTime.Month) / 9.0) +  
           dateTime.Day - 730530.5;  
         
       double julianCenturies = julianDate / 36525.0;  
 
       // Sidereal Time  
       double siderealTimeHours = 6.6974 + 2400.0013 * julianCenturies;  
         
       double siderealTimeUT = siderealTimeHours +  
           (366.2422 / 365.2422) * (double)dateTime.TimeOfDay.TotalHours;  
         
       double siderealTime = siderealTimeUT * 15 + longitude;  
 
       // Refine to number of days (fractional) to specific time.  
       julianDate += (double)dateTime.TimeOfDay.TotalHours / 24.0;    
 
       // Solar Coordinates  
       double meanLongitude = CorrectAngle(Deg2Rad *  
           (280.466 + 36000.77 * julianCenturies));  
         
       double meanAnomaly = CorrectAngle(Deg2Rad *  
           (357.529 + 35999.05 * julianCenturies));  
         
       double equationOfCenter = Deg2Rad * ((1.915 - 0.005 * julianCenturies) *   
           Math.Sin(meanAnomaly) + 0.02 * Math.Sin(2 * meanAnomaly));  
         
       double elipticalLongitude =  
           CorrectAngle(meanLongitude + equationOfCenter);  
         
       double obliquity = (23.439 - 0.013 * julianCenturies) * Deg2Rad;  
 
       // Right Ascension  
       double rightAscension = Math.Atan2(  
           Math.Cos(obliquity) * Math.Sin(elipticalLongitude),  
           Math.Cos(elipticalLongitude));  
         
       double declination = Math.Asin(  
           Math.Sin(rightAscension) * Math.Sin(obliquity));  
 
       // Horizontal Coordinates  
       double hourAngle = CorrectAngle(siderealTime * Deg2Rad - rightAscension);  
         
       //if (hourAngle > Math.PI)  
       //{  
       //    hourAngle -= 2 * Math.PI;  
       //}  
 
       double altitude = Math.Asin(Math.Sin(latitudeInRad) *  
           Math.Sin(declination) + Math.Cos(latitudeInRad) *  
           Math.Cos(declination) * Math.Cos(hourAngle));  
 
       // Nominator and denominator for calculating Azimuth  
       // angle. Needed to test which quadrant the angle is in.  
       double aziNom = -Math.Sin(hourAngle);  
       double aziDenom =  
           Math.Tan(declination) * Math.Cos(latitudeInRad) -  
           Math.Sin(latitudeInRad) * Math.Cos(hourAngle);  
         
       double azimuth = Math.Atan(aziNom / aziDenom);  
         
       if (aziDenom < 0) // In 2nd or 3rd quadrant  
       {  
           azimuth += Math.PI;  
       }  
       else if (aziNom < 0) // In 4th quadrant  
       {  
           azimuth += 2 * Math.PI;  
       }  

       
       return new Position{ Altitude =altitude * Rad2Deg ,Azimuth =azimuth * Rad2Deg };
    }  
  
    /*! 
    * \brief Corrects an angle.
    * 
    * \param angleInRadians An angle expressed in radians. 
    * \return An angle in the range 0 to 2*PI. 
    */  
    private static double CorrectAngle(double angleInRadians)  
    {
         
        double twoPI = Math.PI * 2;
            while (angleInRadians >= twoPI)
            {
                 angleInRadians -= twoPI;
            }
            while (angleInRadians < 0)
            {
                angleInRadians += twoPI;
            }

            return angleInRadians;
    }  
}  
    }

