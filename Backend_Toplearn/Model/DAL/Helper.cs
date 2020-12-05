using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Backend_Toplearn.Utility;

namespace Backend_Toplearn.Model.DAL
{
    public class Helper
    {
        public SqlConnection DataBaseConnnection = new SqlConnection();
        public void SetConnection()
        {
            if (DataBaseConnnection.State == ConnectionState.Open)
            {
                DataBaseConnnection.Close();
            }

            DataBaseConnnection.Close();
            DataBaseConnnection.ConnectionString = @"Data Source=.;Initial Catalog=Db_Toplearn;Integrated Security=True";
            DataBaseConnnection.Open();
        }

        public async Task<List<T>> DataReaderMapToList<T>(SqlCommand cmd, string prochrName, Dictionary<string, string> procherparams)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            //
            SetConnection();
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = prochrName;
            if (procherparams != null && procherparams.Count >= 1)
            {
                foreach (KeyValuePair<string, string> val in procherparams)
                {
                    cmd.Parameters.AddWithValue("@" + val.Key, StringExtensions.CleanString(val.Value.ToString()));
                }
            }

            cmd.Connection = DataBaseConnnection;
            //DataBaseConnnection.Open();
            cmd.CommandTimeout = 0;
            cmd.ExecuteNonQuery();
            //var dataadap = new SqlDataAdapter(cmd);
            SqlDataReader dr = await cmd.ExecuteReaderAsync();
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();

                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    try
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                    catch
                    {
                        prop.SetValue(obj, default(T), null);
                    }
                }
                list.Add(obj);
            }

            return list;
        }

        //public void Select(SqlCommand cmd, string prochrName, Dictionary<string, string> procherparams, ref DataTable table)
        //{
        //    try
        //    {

        //        // MessageBox.Show(procherparams.Values.Count.ToString());
        //        table = new DataTable();
        //        SetConnection();
        //        cmd.Parameters.Clear();
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = prochrName;
        //        if (procherparams != null && procherparams.Count >= 1)
        //        {
        //            foreach (KeyValuePair<string, string> val in procherparams)
        //            {
        //                try
        //                {
        //                    cmd.Parameters.AddWithValue(val.Key, val.Value.ToString().Trim().Replace("ي", "ی").Replace("ك", "ک"));
        //                }
        //                catch (Exception ex)
        //                {
        //                }
        //            }
        //        }
        //        cmd.Connection = DataBaseConnnection;
        //        //DataBaseConnnection.Open();
        //        cmd.CommandTimeout = 0;

        //        cmd.ExecuteNonQuery();
        //        SqlDataAdapter dataadap = new SqlDataAdapter(cmd);


        //        dataadap.Fill(table);
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}
        ////

        ///// <summary>
        ///// مبدل شماره 2
        ///// </summary>
        //public List<T> CreateList<T>(SqlCommand cmd, string prochrName, Dictionary<string, string> procherparams)

        //{

        //    var results = new List<T>();

        //    var properties = typeof(T).GetProperties();

        //    SetConnection();
        //    cmd.Parameters.Clear();
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = prochrName;
        //    if (procherparams != null && procherparams.Count >= 1)
        //    {
        //        foreach (KeyValuePair<string, string> val in procherparams)
        //        {
        //            cmd.Parameters.AddWithValue(val.Key, val.Value);
        //        }
        //    }
        //    cmd.Connection = DataBaseConnnection;
        //    //DataBaseConnnection.Open();
        //    cmd.CommandTimeout = 0;
        //    cmd.ExecuteNonQuery();
        //    //var dataadap = new SqlDataAdapter(cmd);
        //    SqlDataReader reader = cmd.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        var item = Activator.CreateInstance<T>();
        //        foreach (var property in typeof(T).GetProperties())
        //        {
        //            //if (!reader.IsDBNull(reader.GetOrdinal(property.Name)))
        //            //{
        //            Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
        //            property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);

        //            //}

        //        }

        //        results.Add(item);

        //    }

        //    return results;

        //}
        ///// 
        //public void Select(SqlCommand cmd, string prochrName, Dictionary<string, string> procherparams, ref DataTable table)
        //{
        //    try
        //    {

        //        // MessageBox.Show(procherparams.Values.Count.ToString());
        //        table = new DataTable();
        //        SetConnection();
        //        cmd.Parameters.Clear();
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = prochrName;
        //        if (procherparams != null && procherparams.Count >= 1)
        //        {
        //            foreach (KeyValuePair<string, string> val in procherparams)
        //            {
        //                try
        //                {
        //                    cmd.Parameters.AddWithValue(val.Key, val.Value);
        //                }
        //                catch (Exception ex)
        //                {
        //                    Error(P_Error.Code, P_Error.Page, P_Error.Location, ex.Message);
        //                }
        //            }
        //        }
        //        cmd.Connection = DataBaseConnnection;
        //        //DataBaseConnnection.Open();
        //        cmd.CommandTimeout = 0;

        //        cmd.ExecuteNonQuery();
        //        SqlDataAdapter dataadap = new SqlDataAdapter(cmd);


        //        dataadap.Fill(table);
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}
        public async Task<bool> Operations(SqlCommand cmd, string prochrName, Dictionary<string, object> data)
        {
            try
            {
                //using (var ts = new System.Transactions.TransactionScope())
                //{
                SetConnection();
                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = prochrName;
                foreach (KeyValuePair<string, object> item in data)
                {
                    cmd.Parameters.AddWithValue("@" + item.Key,
                   StringExtensions.CleanString(item.Value.ToString()));
                }
                cmd.Connection = DataBaseConnnection;
                cmd.CommandTimeout = 0;
                //    ts.Complete();
                //}
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }

            return true;
        }
       
        public DataTable ToDataTable<T>(IEnumerable<T> items)
        {
            // Create the result table, and gather all properties of a T        
            DataTable table = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Add the properties as columns to the datatable
            foreach (PropertyInfo prop in props)
            {
                Type propType = prop.PropertyType;

                // Is it a nullable type? Get the underlying type 
                if (propType.IsGenericType && propType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                {
                    propType = new NullableConverter(propType).UnderlyingType;
                }

                table.Columns.Add(prop.Name, propType);
            }

            // Add the property values per T as rows to the datatable
            foreach (T item in items)
            {
                object[] values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                table.Rows.Add(values);
            }

            return table;
        }
        public IEnumerable<T> ToIEnumerable<T>(DataTable dt)
        {
            if (dt == null)
            {
                return null;
            }

            List<T> returnValue = new List<T>();
            List<string> typeProperties = new List<string>();

            T typeInstance = Activator.CreateInstance<T>();

            foreach (DataColumn column in dt.Columns)
            {
                var prop = typeInstance.GetType().GetProperty(column.ColumnName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                if (prop != null)
                {
                    typeProperties.Add(column.ColumnName);
                }
            }

            foreach (DataRow row in dt.Rows)
            {
                T entity = Activator.CreateInstance<T>();

                foreach (var propertyName in typeProperties)
                {

                    if (row[propertyName] != DBNull.Value)
                    {
                        string str = row[propertyName].GetType().FullName;

                        if (entity.GetType().GetProperty(propertyName).PropertyType == typeof(System.String))
                        {
                            object Val = row[propertyName].ToString();
                            entity.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public).SetValue(entity, Val, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public, null, null, null);
                        }
                        else if (entity.GetType().GetProperty(propertyName).PropertyType == typeof(System.Guid))
                        {
                            object Val = Guid.Parse(row[propertyName].ToString());
                            entity.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public).SetValue(entity, Val, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public, null, null, null);
                        }
                        else
                        {
                            entity.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public).SetValue(entity, row[propertyName], BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public, null, null, null);
                        }
                    }
                    else
                    {
                        entity.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public).SetValue(entity, null, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public, null, null, null);
                    }
                }

                returnValue.Add(entity);
            }

            return returnValue.AsEnumerable();
        }
    }
}