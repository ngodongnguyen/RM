using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer_Object;

namespace DataLayer
{
    public class TableDL:DataProvider
    {
        public List<Tables> GetTables()
        {
            string id, name;
            List<Tables> tables = new List<Tables>();
            string sql = "Select * from Tables";
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text);
                while (reader.Read())
                {
                    id = reader[0].ToString();
                    name = reader[1].ToString();
                    Tables table = new Tables(id, name);
                    tables.Add(table);
                }
                reader.Close();
                return tables;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }

        }
        public int Add(Tables table)
        {
            string sql = "Insert into Tables (tName) Values('" + table.tName + "')";
            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex) { throw ex; }
        }
        public int Update(Tables tables)
        {
            string sql = "UPDATE Tables SET tName = '" + tables.tName + "' WHERE tId = " + tables.tId;
            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex) { throw ex; }
        }
        public int Delete(Tables tables)
        {
            string sql = "DELETE FROM Tables WHERE tId =" + tables.tId;
            try
            {
                return MyExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
