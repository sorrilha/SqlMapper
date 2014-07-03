using System;
using System.Data.SqlClient;

namespace SqlMapper.Framework.MapTypes
{
    class MapWithFields<T> : TypeMapers<T>
    {
        public MapWithFields()
        {
           Type type = typeof (T);
           _params = type.GetFields();
        }

       /* public override void update_query(T val, SqlCommand cmd, string tableName, string pkName)
        {
            String aux = "";
            var key = val.GetType().GetField(pkName);

            foreach (var f in val.GetType().GetFields())
            {
                String fieldName = f.Name;
                aux += fieldName + " = @" + fieldName + ", ";
                var value = f.GetValue(val);
                if (value != null)
                    cmd.Parameters.AddWithValue("@" + fieldName, value);
            }
            aux = aux.Remove(aux.Length - 2);
            aux += " WHERE " + pkName + " = @" + pkName;
            cmd.Parameters.AddWithValue("@" + key.Name, key);
            cmd.CommandText = "UPDATE " + tableName + "  SET " + aux;
        }

        public override void getAll_query(SqlCommand cmd, string tableName)
        {
            cmd.CommandText = "SELECT * FROM " + tableName;
        }

        public override void delete_query(T val, SqlCommand cmd, string tableName, string pkName)
        {
            var key = val.GetType().GetProperty(pkName);
            cmd.Parameters.AddWithValue("@" + key.Name, key);
            cmd.CommandText = "DELETE FROM " + tableName + "  WHERE " + pkName + " = @" + pkName;
        }

        public override void insert_query(T val, SqlCommand cmd, string tableName)
        {
            String aux = "";
            String parameters = "";
            foreach (var f in val.GetType().GetFields())
            {
                String fieldName = f.Name;
                aux += fieldName + ", ";
                parameters += " @" + fieldName + ", ";
                var value = f.GetValue(val);
                if (value != null)
                    cmd.Parameters.AddWithValue("@" + fieldName, value);
            }
            aux = aux.Remove(aux.Length - 2);
            parameters = parameters.Remove(parameters.Length - 2);
            cmd.CommandText = "INSERT INTO " + tableName + " ( " + aux + " ) Values (" + parameters + " )";
        }*/
    }
}
