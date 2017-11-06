using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CsvReader
{
    class Program
    {
        private static int DATE_KEY = 0;
        private static int OPEN_KEY = 1;
        private static int HIGH_KEY = 2;
        private static int LOW_KEY = 3;
        private static int VOLUME_KEY = 4;
        // why the hell wont it commit
        
        static void Main(string[] args)
        {
            DataTable data = new DataTable();
            data.Columns.Add("Date");
            data.Columns.Add("Open");
            data.Columns.Add("High");
            data.Columns.Add("Low");
            data.Columns.Add("Close");
            data.Columns.Add("Volume");
            using (var reader = new System.IO.StreamReader(@"D:\Downloads\aapl.csv"))
            {
                
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    data.Rows.Add(values[0], values[1], values[2], values[3], values[4], values[5]);
                }
                ShowTable(data, 6);
                Console.WriteLine("Arithmetic mean for open: " + ArithmeticMean(OPEN_KEY, data));
                Console.WriteLine("Arithmetic mean for high: " + ArithmeticMean(HIGH_KEY, data));
                Console.WriteLine("Arithmetic mean for low: " + ArithmeticMean(LOW_KEY, data));
                Console.WriteLine("Day with most money spent: " + HugeDay(data));
                string day1ToBuy = "";
                string day2ToBuy = "";
                BestDaysToBuy(data, ref day1ToBuy, ref day2ToBuy);
                Console.WriteLine("Best days to buy: " + day1ToBuy + " , " + day2ToBuy);
                Console.WriteLine("Best day to sell: " + BestDayToSell(data));

                Console.ReadKey();
            }
        }
       
       public static void BestDaysToBuy(DataTable data, ref string date1, ref string date2)
        {
            List<Auction> auctionList = new List<Auction>();
            for (int row = 1; row < data.Rows.Count; row++)
            {
                // low : 33
                string auxDate;
                string auxLow;
                double dAuxLow;
                auxLow = data.Rows[row].Field<string>(3);
                dAuxLow = Double.Parse(auxLow);
                auxDate = data.Rows[row].Field<string>(0);
                auctionList.Add(new Auction(dAuxLow, auxDate));
            }
            List<Auction> SortedList = auctionList.OrderBy(a => a.GetPrice()).ToList();
            Console.WriteLine(SortedList[1].GetPrice() + " | " + SortedList[1].GetDate());
            date1 = SortedList[0].GetDate();
            date2 = SortedList[1].GetDate();
        }

       
        public static string BestDayToSell(DataTable data)
        {
            string date = "";
            double max = 0;
            for (int row = 1; row < data.Rows.Count; row++)
            {
                double dAux;
                string aux;
                aux = data.Rows[row].Field<string>(2);
                dAux = Double.Parse(aux);
                if (dAux > max)
                {
                    max = dAux;
                    date = data.Rows[row].Field<string>(0);
                }
            }
            return date;
        }
        public static string HugeDay(DataTable data)
        {
            double max = 0;
            string date = "";
            for (int row = 1; row < data.Rows.Count; row++)
            {
                double toCompare;
                double dAux1, dAux2;
                string aux1 = data.Rows[row].Field<string>(1);
                string aux2 = data.Rows[row].Field<string>(5);
                dAux1 = Double.Parse(aux1);
                dAux2 = Double.Parse(aux2);
                toCompare = dAux2 * dAux1;
                if (toCompare > max)
                {
                    max = toCompare;
                    date = data.Rows[row].Field<string>(0);
                }
            }
            return date;
        }

        public static double ArithmeticMean(int col, DataTable data)
        {
            double average = 0;
            for (int row = 1; row < data.Rows.Count; row++)
            {
                string field = data.Rows[row].Field<string>(col);
                double aux = Double.Parse(field);
                average += aux;

                
            }
            average = average / data.Rows.Count;
            average = Math.Round(average, 3);
            return average;

        }

        public static void ShowTable(DataTable table, int columns)
        {
            for (int row  = 0; row < table.Rows.Count; row++)
            {
                string builder = "";
                for (int col = 0; col < columns; col++)
                {
                    string field = table.Rows[row].Field<string>(col);
                    builder = builder + field + "    |    ";
                }
                Console.WriteLine(builder);
            }
        }


    }

   
}
