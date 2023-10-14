using System.Data;
using System.Data.SQLite;

namespace MySQLite.ConsoleApp
{
    class Program
    {
        static string dbConnectionString = "Data Source= database.db; Version = 3; New = True; Compress = True;";

        static void Main(string[] args)
        {

            using (SQLiteConnection connection = new SQLiteConnection(dbConnectionString))
            {
                // open connection
                connection.Open();

                //Create a table (drop if already exists first):
                var delTableCmd = connection.CreateCommand();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS SampleTable";
                delTableCmd.ExecuteNonQuery();
                var delTableCmd1 = connection.CreateCommand();
                delTableCmd1.CommandText = "DROP TABLE IF EXISTS SampleTable1";
                delTableCmd1.ExecuteNonQuery();


                // create table
                string Createsql = "CREATE TABLE IF NOT EXISTS SampleTable (id INTEGER PRIMARY KEY, Col1 VARCHAR(20), Col2 INT)";
                string Createsql1 = "CREATE TABLE IF NOT EXISTS SampleTable1 (id INTEGER PRIMARY KEY, Col1 VARCHAR(20), Col2 INT)";

                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = Createsql;
                createTableCmd.ExecuteNonQuery();

                createTableCmd.CommandText = Createsql1;
                createTableCmd.ExecuteNonQuery();


                string insertQuery = "";
                //Seed some data: 
                // insert data 
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();

                    insertQuery = "INSERT INTO SampleTable (id, Col1, Col2) VALUES (1, 'Test Text', 100);";
                    insertCmd.CommandText = insertQuery;
                    insertCmd.ExecuteNonQuery();

                    insertQuery = "INSERT INTO SampleTable (id, Col1, Col2) VALUES (2, 'Test Text', 200);";
                    insertCmd.CommandText = insertQuery;
                    insertCmd.ExecuteNonQuery();

                    insertQuery = "INSERT INTO SampleTable (id, Col1, Col2) VALUES (3, 'Test Text', 300);";
                    insertCmd.CommandText = insertQuery;
                    insertCmd.ExecuteNonQuery();

                    transaction.Commit();
                }

                // insert data 1
                insertQuery = "INSERT INTO SampleTable1 (id, Col1, Col2) VALUES (1, 'Test Text', 110);";
                try
                {
                    using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (SQLiteException ex)
                {
                    if (ex.ErrorCode == 19) // constraint violation error code
                    {
                        Console.WriteLine("Error: The name already exists in the database.");
                    }
                    else
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }


                // select data
                string selectQuery = "SELECT * FROM SampleTable";
                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        Console.WriteLine("+----+----------+----+");
                        Console.WriteLine("| ID |   Col1   |Col2|");
                        Console.WriteLine("+----+----------+----+");
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string col1 = reader.GetString(1);
                            int col2 = reader.GetInt32(2);
                            Console.WriteLine("{0}, {1}, {2}", id, col1, col2);
                        }
                        Console.WriteLine("+----+----------+----+");
                    }
                }

                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        // use the data in the dataTable

                        Console.WriteLine("Datatable row count : " + dataTable.Rows.Count);
                    }
                }

                //
                using (SQLiteCommand selectCMD = connection.CreateCommand())
                {
                    selectCMD.CommandText = "SELECT * FROM SampleTable1";
                    selectCMD.CommandType = CommandType.Text;

                    SQLiteDataReader myReader = selectCMD.ExecuteReader();
                    while (myReader.Read())
                    {
                        Console.WriteLine(myReader["id"] + " " + myReader["Col1"] + " " + myReader["Col2"]);
                    }

                }



            } // conn.Close();





        }


        //
    }

}