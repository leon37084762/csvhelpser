using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace CSVHelper
{
    class Program
    {
        static void csv_file_save()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("column1");
            dt.Columns.Add("column2");
            dt.Columns.Add("column3");
            dt.Columns.Add("column4");
            dt.Columns.Add("column5");

            for (int i = 0; i < 100; i++)
            {
                DataRow dr = dt.NewRow();

                dr["column1"] = "column1 " + i;
                dr["column2"] = "column2 " + i;
                dr["column3"] = "column3 " + i;
                dr["column4"] = "column4 " + i;
                dr["column5"] = "column5 " + i;

                dt.Rows.Add(dr);
            }

            UtilHelper.CSVHelper.SaveCSV(dt, "csv1file");
        }

        static void csv_file_read()
        {
            DataTable dt = UtilHelper.CSVHelper.OpenCSV("csv1file");
            DataRow[] drs = dt.Select("column1='column1 1'");
            
            foreach(DataRow dr in drs)
            {
                dr.BeginEdit();
                dr["column2"] = "abcdefg";
                dr.EndEdit();

                Console.Write("{0} ", dr["column1"].ToString());
                Console.Write("{0} ", dr["column2"].ToString());
                Console.Write("{0} ", dr["column3"].ToString());
                Console.Write("{0} ", dr["column4"].ToString());
                Console.WriteLine("{0} ", dr["column5"].ToString());
            }

            DataRow[] delete_dr = dt.Select("column1 = 'column1 2'");
            foreach(DataRow dr in delete_dr)
            {
                dt.Rows.Remove(dr);
            }

            {
                DataRow dr = dt.NewRow();

                dr["column1"] = "column1 1254";
                dr["column2"] = "column2 1265";
                dr["column3"] = "column3 1265";
                dr["column4"] = "column4 1265";
                dr["column5"] = "column5 1265";

                dt.Rows.Add(dr);
            }
            var query = dt.AsEnumerable().Where<DataRow>(a => a["column1"].ToString() == "column1 3");//lambda

            foreach(DataRow dr in query)
            {
                Console.Write("{0} ", dr["column1"].ToString());
                Console.Write("{0} ", dr["column2"].ToString());
                Console.Write("{0} ", dr["column3"].ToString());
                Console.Write("{0} ", dr["column4"].ToString());
                Console.WriteLine("{0} ", dr["column5"].ToString());
            }

            //linq 排序
            var sort_query = from customer in dt.AsEnumerable()
                             orderby customer[0] descending
                             select customer;// dt.AsEnumerable().OrderByDescending<DataRow>(x => x.Field<string>("column1"));
            foreach (DataRow dr in sort_query)
            {
                Console.Write("{0} ", dr["column1"].ToString());
                Console.Write("{0} ", dr["column2"].ToString());
                Console.Write("{0} ", dr["column3"].ToString());
                Console.Write("{0} ", dr["column4"].ToString());
                Console.WriteLine("{0} ", dr["column5"].ToString());
            }
            UtilHelper.CSVHelper.SaveCSV(dt, "csv1file");
        }
        static void Main(string[] args)
        {
            csv_file_read();
            Console.ReadKey();
        }
    }
}
