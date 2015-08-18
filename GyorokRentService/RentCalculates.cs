using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GyorokRentService
{
    class RentCalculates
    {
        static int hoursPerDay = 10;

        public RentCalculates()
        {

        }

        public float getIntervalInDays(DateTime start, DateTime end) {
            return (float)getIntervalInHours(start, end) / (float)hoursPerDay;
        }

        public int getIntervalInHours(DateTime start, DateTime end) {
            int dayNo;
            int hourNo;

            dayNo = (end - start).Days;
            hourNo = (end - start).Hours;
            if (hourNo > hoursPerDay)
            {
                hourNo = hoursPerDay;
            }

            return dayNo * hoursPerDay + hourNo;
        }

        public long getRentCost(DateTime start, DateTime end, long price, float discount)
        {
            int hours;

            hours = getIntervalInHours(start, end);
            if (hours == 0)
            {
                hours = 1;
            }


            return (long)Math.Round((double)(hours * (price / hoursPerDay) * (1 - discount)), 0);

        }

        public int getIntervalDays(DateTime start, DateTime end) {
            int dayNo;

            dayNo = (end - start).Days;
            if ((end - start).Hours >= hoursPerDay)
            {
                dayNo += 1;
            }
            return dayNo;
        }

        public int getIntervalHours(DateTime start, DateTime end) {
            int hourNo;

            hourNo = (end - start).Hours;
            if (hourNo >= hoursPerDay)
            {
                hourNo = 0;
            }
            return hourNo;
        }

        public DateTime getNowInHour()
        {
            int hour;
            if (DateTime.Now.Minute > 30)
            {
                if (DateTime.Now.Hour == 23)
                {
                    hour = 0;
                }
                else
                {
                    hour = DateTime.Now.Hour + 1;
                }
            }
            else
            {
                hour = DateTime.Now.Hour;
            }
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, 0, 0);
        }
    }
}
