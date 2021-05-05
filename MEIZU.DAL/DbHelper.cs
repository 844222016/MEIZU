using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using MEIZU.Model;

namespace MEIZU.DAL
{

    public class SuperDbHelper<T> where T : BaseModel, new()
    {
        private readonly string conStr;

        public SuperDbHelper()
        {
            conStr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }
        /// <summary>
        /// 链接对象
        /// </summary>
        private SqlConnection Con
        {
            get
            {
                var con = new SqlConnection(conStr);
                con.Open();
                return con;
            }
        }

        /// <summary>
        /// 指令对象
        /// </summary>
        public SqlCommand Cmd => Con.CreateCommand();
        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool Update(string sql)
        {
            var cmd = Cmd;
            cmd.CommandText = sql;
            try
            {
                return cmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        public bool Update(string sql, params SqlParameter[] pars)
        {
            var cmd = Cmd;
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(pars);
            try
            {
                return cmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public object SelectForScalar(string sql)
        {

            var cmd = Cmd;
            cmd.CommandText = sql;
            try
            {
                return cmd.ExecuteScalar();
            }
            finally
            {
                cmd.Connection.Close();

            }
        }
        public object SelectForScalar(string sql, params SqlParameter[] pars)
        {
            var cmd = Cmd;
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(pars);
            try
            {
                return cmd.ExecuteScalar();
            }
            finally
            {
                cmd.Connection.Close();

            }
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public SqlDataReader SelectForDataReader(string sql)
        {
            var cmd = Cmd;
            cmd.CommandText = sql;
            try
            {
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception e)
            {
                cmd.Connection.Close();
                throw;
            }
        }
        public SqlDataReader SelectForDataReader(string sql, params SqlParameter[] pars)
        {
            var cmd = Cmd;
            cmd.CommandText = sql;
            cmd.Parameters.AddRange(pars);
            try
            {
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception e)

            {
                cmd.Connection.Close();
                throw;
            }
        }

        public void Create(T t)
        {
            var tableName = typeof(T).Name;
            var type = typeof(T);
            var props = new List<string>();
            var valNames = new List<string>();
            var valPars = new List<SqlParameter>();

            foreach (var propertyInfo in type.GetProperties())
            {
                if (propertyInfo.Name == "Id") continue;
                props.Add($"[{propertyInfo.Name}]");
                valNames.Add($"@{propertyInfo.Name}");
                valPars.Add(new SqlParameter($"@{propertyInfo.Name}", propertyInfo.GetValue(t) == null ? DBNull.Value : propertyInfo.GetValue(t)));
            }

            var sql = $"insert into {tableName}({string.Join(",", props)}) values({string.Join(",", valNames)})";
            Update(sql, valPars.ToArray());
        }

        public void Delete(T t)
        {
            var tableName = typeof(T).Name;
            var sql = $"delete from {tableName} where id = {t.Id}";
            Update(sql);
        }
        public void Delete(long id)
        {
            var tableName = typeof(T).Name;
            var sql = $"delete from {tableName} where id = {id}";
            Update(sql);
        }

        public void DeleteByRoleId(long id)
        {
            var tableName = typeof(T).Name;
            var sql = $"delete from {tableName} where RoleId = {id} ";
            Update(sql);
        }
        public void DeleteByUserId(long id)
        {
            var tableName = typeof(T).Name;
            var sql = $"delete from {tableName} where UserId = {id} ";
            Update(sql);
        }


        public void Updates(T t)
        {
            var tableName = typeof(T).Name;
            var type = typeof(T);

            var valPars = new List<SqlParameter>();

            foreach (var propertyInfo in type.GetProperties())
            {
                if (propertyInfo.Name == "Id") continue;

                valPars.Add(new SqlParameter($"@{propertyInfo.Name}", propertyInfo.GetValue(t) == null ? DBNull.Value : propertyInfo.GetValue(t)));
            }

            var setStr = string.Join(",",
                type.GetProperties().Where(m => m.Name != "Id").Select(m => $"[{m.Name}]=@{m.Name}"));

            var sql = $"update {tableName} set {setStr} where id = {t.Id}";
            Update(sql, valPars.ToArray());
        }


        public T GetOne(long id)
        {
            var tableName = typeof(T).Name;
            var type = typeof(T);
            var props = string.Join(",", type.GetProperties().Select(m => $"[{m.Name}]"));
            var sql = $"select {props} from {tableName} where id = {id}";
            T t = new T();

            var dr = SelectForDataReader(sql);
            if (dr.Read())
            {
                foreach (var propertyInfo in type.GetProperties())
                {
                    if (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType != typeof(string)) continue;

                    propertyInfo.SetValue(t, dr[propertyInfo.Name] is DBNull ? null : dr[propertyInfo.Name]);
                }
            }
            dr.Close();
            return t;
        }


        public List<T> GetAll()
        {
            var tableName = typeof(T).Name;
            var type = typeof(T);
            var props = string.Join(",", type.GetProperties().Select(m => $"[{m.Name}]"));
            var sql = $"select {props} from {tableName}";
            List<T> list = new List<T>();

            var dr = SelectForDataReader(sql);
            while (dr.Read())
            {
                T t = new T();
                foreach (var propertyInfo in type.GetProperties())
                {
                    if (dr[propertyInfo.Name] == "ImgPic") continue;
                    if (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType != typeof(string)) continue;
                    propertyInfo.SetValue(t, dr[propertyInfo.Name] is DBNull ? null : dr[propertyInfo.Name]);
                }
                list.Add(t);
            }
            dr.Close();
            return list;
        }

        public List<T> GetAll(string where = "1=1")
        {
            var tableName = typeof(T).Name;
            var type = typeof(T);
            var props = string.Join(",", type.GetProperties().Select(m => $"[{m.Name}]"));
            var sql = $"select {props} from {tableName} where {where}";
            List<T> list = new List<T>();

            var dr = SelectForDataReader(sql);
            while (dr.Read())
            {
                T t = new T();
                foreach (var propertyInfo in type.GetProperties())
                {
                    if (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType != typeof(string)) continue;
                    propertyInfo.SetValue(t, dr[propertyInfo.Name] is DBNull ? null : dr[propertyInfo.Name]);
                }
                list.Add(t);
            }

            dr.Close();
            return list;
        }


        public List<T> GetAllByPage(string where = "1=1", int pageIndex = 0, int pageSize = 10)
        {
            var tableName = typeof(T).Name;
            var type = typeof(T);
            var props = string.Join(",", type.GetProperties().Select(m => $"[{m.Name}]"));
            var sql = $"select top {pageSize} {props} from {tableName} where {where} and Id not in (select top {pageIndex * pageSize} Id from {tableName}) ";
            List<T> list = new List<T>();

            var dr = SelectForDataReader(sql);
            while (dr.Read())
            {
                T t = new T();
                foreach (var propertyInfo in type.GetProperties())
                {
                    if (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType != typeof(string)) continue;
                    propertyInfo.SetValue(t, dr[propertyInfo.Name]);
                }
                list.Add(t);
            }

            dr.Close();
            return list;
        }
        public List<T> GetAllByPageOrderBy(string orderby, string where = "1=1", int pageIndex = 0, int pageSize = 10)
        {
            var tableName = typeof(T).Name;
            var type = typeof(T);
            var props = string.Join(",", type.GetProperties().Select(m => $"[{m.Name}]"));
            var sql = $"select top {pageSize} {props} from {tableName} where {where} and id not in(select top {pageIndex * pageSize} id from {tableName} order by {orderby} )  order by  {orderby}";

            List<T> list = new List<T>();

            var dr = SelectForDataReader(sql);
            while (dr.Read())
            {
                T t = new T();
                foreach (var propertyInfo in type.GetProperties())
                {
                    if (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType != typeof(string)) continue;
                    propertyInfo.SetValue(t, dr[propertyInfo.Name]);
                }
                list.Add(t);
            }

            dr.Close();
            return list;
        }

        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="like"></param>
        /// <returns></returns>
        public List<T> GetAllByLike(string like)
        {
            var tableName = typeof(T).Name;
            var type = typeof(T);
            var props = string.Join(",", type.GetProperties().Select(m => $"[{m.Name}]"));
            var sql = $"select {props} from {tableName} where {like}";
            List<T> list = new List<T>();

            var dr = SelectForDataReader(sql);
            while (dr.Read())
            {
                T t = new T();
                foreach (var propertyInfo in type.GetProperties())
                {
                    if (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType != typeof(string)) continue;
                    propertyInfo.SetValue(t, dr[propertyInfo.Name] is DBNull ? null : dr[propertyInfo.Name]);
                }
                list.Add(t);
            }

            dr.Close();
            return list;
        }
        public List<K> GetListBySql<K>(string sql, params SqlParameter[] pars) where K : new()
        {
            var dr = SelectForDataReader(sql, pars);
            List<K> list = new List<K>();
            while (dr.Read())
            {
                K k = new K();
                foreach (var propertyInfo in typeof(K).GetProperties())
                {
                    if (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType != typeof(string)) continue;
                    propertyInfo.SetValue(k, dr[propertyInfo.Name] is DBNull ? null : dr[propertyInfo.Name]);
                }
                list.Add(k);
            }
            dr.Close();
            return list;
        }
        public int Count(string where)
        {
            var sql = $"select count(0) from {typeof(T).Name} where {where}";
            return (int)SelectForScalar(sql);
        }
    }

    public class DbHelper <T> where T : new()
    {
        private readonly string conStr;

        public DbHelper()
        {
            conStr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        }
        /// <summary>
        /// 链接对象
        /// </summary>
        private SqlConnection Con
        {
            get
            {
                var con = new SqlConnection(conStr);
                con.Open();
                return con;
            }
        }

        /// <summary>
        /// 指令对象
        /// </summary>
        public SqlCommand Cmd => Con.CreateCommand();
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public SqlDataReader SelectForDataReader(string sql)
        {
            var cmd = Cmd;
            cmd.CommandText = sql;
            try
            {
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception e)
            {
                cmd.Connection.Close();
                throw;
            }
        }
        public List<T> GetAllInnerJoin(string inner)
        {
            var tableNames = typeof(T).Name;
            string tableName;
            var nameInt = tableNames.ToUpper().LastIndexOf('D');
            var isDto = "DTO".Equals(tableNames.ToUpper().Substring(nameInt, 3));
            tableName = isDto ? tableNames.Substring(0, tableNames.Length - 3) : tableNames;
            var type = typeof(T);
            var props = "";
            foreach (var info in type.GetProperties())
            {
                var state = true;
                foreach (object attribute in info.GetCustomAttributes(true))
                {
                    if (attribute is AttClass)
                    {
                        AttClass att = (AttClass)attribute;
                        props += $"{att.Name + "[" + info.Name}],";
                        state = false;
                    }
                }
                if (state)
                {
                    props += $"[{info.Name}],";
                }
            }
            var trimEnd = props.TrimEnd(',');
            var sql = $"select {trimEnd} from {tableName} {inner}";
            List<T> list = new List<T>();

            var dr = SelectForDataReader(sql);
            while (dr.Read())
            {
                T t = new T();
                foreach (var propertyInfo in type.GetProperties())
                {
                    if (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType != typeof(string)) continue;
                    propertyInfo.SetValue(t, dr[propertyInfo.Name] is DBNull ? null : dr[propertyInfo.Name]);
                }
                list.Add(t);
            }
            dr.Close();
            return list;
        }
        public T GetOneInnerJoin(string inner,string where)
        {
            var tableNames = typeof(T).Name;
            string tableName;
            var nameInt = tableNames.ToUpper().LastIndexOf('D');
            var isDto = "DTO".Equals(tableNames.ToUpper().Substring(nameInt, 3));
            tableName = isDto ? tableNames.Substring(0, tableNames.Length - 3) : tableNames;
            var type = typeof(T);
            var props = "";
            foreach (var info in type.GetProperties())
            {
                var state = true;
                foreach (object attribute in info.GetCustomAttributes(true))
                {
                    if (attribute is AttClass)
                    {
                        AttClass att = (AttClass)attribute;
                        props += $"{att.Name + "[" + info.Name}],";
                        state = false;
                    }
                }
                if (state)
                {
                    props += $"[{info.Name}],";
                }
            }
            var trimEnd = props.TrimEnd(',');
            var sql = $"select {trimEnd} from {tableName} {inner} {where}";
            T t = new T();

            var dr = SelectForDataReader(sql);
            if (dr.Read())
            {
                foreach (var propertyInfo in type.GetProperties())
                {
                    if (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType != typeof(string)) continue;

                    propertyInfo.SetValue(t, dr[propertyInfo.Name] is DBNull ? null : dr[propertyInfo.Name]);
                }
            }
            dr.Close();
            return t;
        }
    }
}