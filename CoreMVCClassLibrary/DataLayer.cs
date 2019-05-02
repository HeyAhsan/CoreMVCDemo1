using System;
using System.Data;
using System.Data.SqlClient;

namespace CoreMVCClassLibrary
{
    public class DataLayer
    {

        public DataTable ConvertToDataTable(string filePath, int numberOfColumns)
        {
            DataTable tbl = new DataTable();

            string[] lines = System.IO.File.ReadAllLines(filePath);

            for (int col = 0; col < numberOfColumns; col++)
                tbl.Columns.Add(new DataColumn(lines[0].Split(',')[col]));
            //tbl.Columns.Add(new DataColumn("Column" + (col + 1).ToString()));


            foreach (string line in lines)
            {
                var cols = line.Split(',');

                DataRow dr = tbl.NewRow();
                for (int cIndex = 0; cIndex < numberOfColumns; cIndex++)
                {
                    dr[cIndex] = cols[cIndex];
                }

                tbl.Rows.Add(dr);
            }
            tbl.Rows.RemoveAt(0);
            return tbl;
        }

        //private static DataTable GetDataTabletFromCSVFile(string csv_file_path)
        //{
        //    DataTable csvData = new DataTable();
        //    try
        //    {
        //        using (TextFieldParser csvReader = new TextFieldParser.TextFieldParser(csv_file_path))
        //        {
        //            csvReader.SetDelimiters(new string[] { "," });
        //            csvReader.HasFieldsEnclosedInQuotes = true;
        //            string[] colFields = csvReader.ReadFields();
        //            foreach (string column in colFields)
        //            {
        //                DataColumn datecolumn = new DataColumn(column);
        //                datecolumn.AllowDBNull = true;
        //                csvData.Columns.Add(datecolumn);
        //            }
        //            while (!csvReader.EndOfData)
        //            {
        //                string[] fieldData = csvReader.ReadFields();
        //                //Making empty value as null
        //                for (int i = 0; i < fieldData.Length; i++)
        //                {
        //                    if (fieldData[i] == "")
        //                    {
        //                        fieldData[i] = null;
        //                    }
        //                }
        //                csvData.Rows.Add(fieldData);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    return csvData;
        //}




        public void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable csvFileData)
        {
            using (SqlConnection dbConnection = new SqlConnection("Data Source=WHDQ4281\\SQLEXPRESS2012;Initial Catalog=LocalDBTest1;Integrated Security=SSPI;"))
            {
                dbConnection.Open();
                using (SqlBulkCopy slbulkcopy = new SqlBulkCopy(dbConnection))
                {
                    slbulkcopy.DestinationTableName = "UsersData";
                    foreach (var column in csvFileData.Columns)
                        slbulkcopy.ColumnMappings.Add(column.ToString(), column.ToString());
                    slbulkcopy.WriteToServer(csvFileData);
                }
            }

        }

    }
}
