using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReader
{
    class Auction
    {
        private double price;
        private string date;

        public Auction(double price, string date)
        {
            SetPrice(price);
            SetDate(date);
        }


        public void SetPrice(double price)
        {
            this.price = price;
        }

        public void SetDate(string date)
        {
            this.date = date;
        }

        public double GetPrice()
        {
            return price;
        }

        public string GetDate()
        {
            return date;
        }




    }
}
