using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace CoreMVCDB.Models
{
    public class CoreDataCollections
    {
        private void InsertDataIntoSQLServerUsingSQLBulkCopy(DataTable csvFileData)
        {
            using (SqlConnection dbConnection = new SqlConnection("Data Source=WHDQ4281\\SQLEXPRESS2012;Initial Catalog=delme;Integrated Security=SSPI;"))
            {
                dbConnection.Open();
                using (SqlBulkCopy slbulkcopy = new SqlBulkCopy(dbConnection))
                {
                    slbulkcopy.DestinationTableName = "CSVTable1";
                    foreach (var column in csvFileData.Columns)
                        slbulkcopy.ColumnMappings.Add(column.ToString(), column.ToString());
                    slbulkcopy.WriteToServer(csvFileData);
                }
            }

        }


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
    }
}
