using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace YHFramework.DB
{
    public sealed class DBFactory : IDisposable
    {
        #region 数据库类型枚举
        /// <summary>
        /// 数据库类型
        /// </summary>
        public enum DBType
        {
            SQLSERVER,
            MDB,
            SQLITE
        }
        #endregion

        #region 公共成员
        public string ConnectionString { get; set; } //连接字符串

        DBType _DbType;

        public DBType DbType
        {
            get { return this._DbType; }
            set
            {
                this._DbType = value;
                switch (value)
                {
                    case DBType.SQLSERVER:
                        Factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                        break;
                    case DBType.MDB:
                        Factory = DbProviderFactories.GetFactory("System.Data.OleDb");
                        break;
                    case DBType.SQLITE:
                        Factory = DbProviderFactories.GetFactory("System.Data.SQLite");
                        break;
                }
            }
        } //数据库类型

        public string CommandText { get; set; } //查询语句

        //public DbParameterCollection Parameters { get; set; } //参数集合



        #endregion

        #region 私有成员

        private DbParameterCollection Parameters { get; set; } //参数集合

        #endregion

        #region 初始成员

        private DbConnection Conn = null; //连接对象

        private DbProviderFactory Factory = null; //数据库工厂                

        private List<myTran> TranList = new List<myTran>(); //事务集合

        #endregion

        #region 构造函数
        public DBFactory()
        { }

        public DBFactory(DBType dbType, string connectionString)
        {
            this.DbType = dbType;
            this.ConnectionString = connectionString;
            this.Parameters = Factory.CreateCommand().Parameters;
        }
        #endregion

        #region 初始化与自动释放

        public void Open()
        {
            try
            {
                if (Conn == null)
                {
                    Conn = Factory.CreateConnection();
                    Conn.ConnectionString = this.ConnectionString;
                    Conn.Open();
                }
                else
                {
                    if (Conn.State == ConnectionState.Closed)
                        Conn.Open();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Close()
        {
            try
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            try
            {
                if (Conn.State == ConnectionState.Open)
                    Conn.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 添加查询参数
        public void AddParameter(string name, object value)
        {
            var pa = Factory.CreateParameter();
            pa.ParameterName = name;
            pa.Value = value;
            this.Parameters.Add(pa);
        }

        public void AddParameters<T>(T model) where T : class,new()
        {
            Type t = typeof(T);
            Array.ForEach<System.Reflection.PropertyInfo>(t.GetProperties(), p =>
            {
                AddParameter("@" + p.Name, p.GetValue(model, null));
            });
        }

        public void AddParameters(string[] names, object[] values)
        {
            if (names.Length != values.Length)
                throw new Exception("参数名称跟参数值数量不匹配！");
            for (var i = 0; i < names.Length; i++)
            {
                var pa = Factory.CreateParameter();
                pa.ParameterName = names[i];
                pa.Value = values[i];
                this.Parameters.Add(pa);
            }
        }
        #endregion

        #region 创建查询参数
        public DbParameter CreateParameter(string name, object value)
        {
            var pa = Factory.CreateParameter();
            pa.ParameterName = name;
            pa.Value = value;
            return pa;
        }

        public List<DbParameter> CreateParameters(string[] names, object[] values)
        {
            if (names.Length != values.Length)
                throw new Exception("参数名称跟参数值数量不匹配！");
            var parameters = new List<DbParameter>();
            for (var i = 0; i < names.Length; i++)
            {
                parameters.Add(CreateParameter(names[i],values[i]));
            }
            return parameters;
        }

        public List<DbParameter> CreateParameters<T>(T model) where T : class,new()
        {
            var parameters = new List<DbParameter>();
            Type t = typeof(T);
            Array.ForEach<System.Reflection.PropertyInfo>(t.GetProperties(), p =>
            {                
                parameters.Add(CreateParameter(p.Name, p.GetValue(model, null)));
            });
            return parameters;
        }
        #endregion

        #region 清除查询字符串和查询参数
        /// <summary>
        /// 清除查询字符串和查询参数
        /// </summary>
        void Clear()
        {
            this.CommandText = "";
            if (this.Parameters != null)
                this.Parameters.Clear();
        }
        #endregion

        #region 返回一个DataTable
        /// <summary>
        /// 返回一个DataTable
        /// </summary>
        public DataTable ExecuteDataTable()
        {
            try
            {
                using (DbCommand cmd = Factory.CreateCommand())
                {
                    Open();
                    cmd.Connection = this.Conn;
                    cmd.CommandText = this.CommandText;
                    //cmd.Parameters.AddRange(this.Parameters);   
                    if (this.Parameters != null)
                        foreach (var para in this.Parameters)
                        {
                            var p = cmd.CreateParameter();
                            p.ParameterName = (para as DbParameter).ParameterName;
                            p.Value = (para as DbParameter).Value;
                            cmd.Parameters.Add(p);
                        }
                    Clear();

                    DbDataReader dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    return dt;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Clear();
            }
        }
        #endregion

        #region 执行一条更新语句
        /// <summary>
        /// 执行一条更新语句
        /// </summary>        
        public int ExecuteNonQuery()
        {
            try
            {
                using (DbCommand cmd = Factory.CreateCommand())
                {
                    Open();
                    cmd.Connection = this.Conn;
                    cmd.CommandText = this.CommandText;
                    if (this.Parameters != null)
                        foreach (var para in this.Parameters)
                        {
                            var p = cmd.CreateParameter();
                            p.ParameterName = (para as DbParameter).ParameterName;
                            p.Value = (para as DbParameter).Value;
                            cmd.Parameters.Add(p);
                        }
                    Clear();
                    if (this.Conn.State == ConnectionState.Closed)
                        Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Clear();
            }
        }
        #endregion

        #region 返回首行首列
        public object ExecuteScalar()
        {
            try
            {
                using (var cmd = Factory.CreateCommand())
                {
                    Open();
                    cmd.Connection = this.Conn;
                    cmd.CommandText = this.CommandText;
                    if (this.Parameters != null)
                        foreach (var para in this.Parameters)
                        {
                            var p = cmd.CreateParameter();
                            p.ParameterName = (para as DbParameter).ParameterName;
                            p.Value = (para as DbParameter).Value;
                            cmd.Parameters.Add(p);
                        }
                    Clear();
                    if (this.Conn.State == ConnectionState.Closed)
                        Open();
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Clear();
            }
        }
        #endregion

        #region 自定义事务类
        class myTran
        {
            public string queryString { get; set; }
            public List<DbParameter> parameters { get; set; }

            public myTran(string queryString, List<DbParameter> parameters)
            {
                this.queryString = queryString;
                this.parameters = parameters;
            }
        }
        #endregion

        #region 添加事务
        public void AddTran(string queryString, List<DbParameter> parameters)
        {
            var tran = new myTran(queryString, parameters);
            TranList.Add(tran);
        }

        public void AddTran(string queryString, DbParameter parameter)
        {
            List<DbParameter> paras = new List<DbParameter>();
            if (parameter != null)
                paras.Add(parameter);
            var tran = new myTran(queryString, paras);
            TranList.Add(tran);
        }
        #endregion

        #region 清除事务
        void ClearTran()
        {
            TranList.Clear();
        }
        #endregion

        #region 执行事务
        public void ExecuteTran()
        {
            try
            {
                using (DbTransaction tran = Conn.BeginTransaction())
                {
                    try
                    {
                        if (this.Conn.State == ConnectionState.Closed)
                            Open();
                        TranList.ForEach(m =>
                        {
                            using (var cmd = this.Factory.CreateCommand())
                            {
                                cmd.Connection = this.Conn;
                                cmd.CommandText = m.queryString;
                                cmd.Transaction = tran;
                                m.parameters.ForEach(n =>
                                {
                                    cmd.Parameters.Add(n);
                                });
                                cmd.ExecuteNonQuery();
                            }
                        });
                        tran.Commit();
                    }
                    catch (Exception)
                    {
                        tran.Rollback();
                        throw;
                    }
                    finally
                    {
                        ClearTran();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ClearTran();
            }
        }
        #endregion

        #region 根据对象生成更新语句
        /// <summary>
        /// 获取更新语句
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="Result"></param>
        /// <param name="TableName"></param>
        /// <param name="IndexFieldName"></param>
        /// <returns></returns>
        public string GetUpdateString<TResult>(string TableName, string IndexFieldName) where TResult : class,new()
        {
            string rt = "update " + TableName + " set";
            Type t = typeof(TResult);
            Array.ForEach<System.Reflection.PropertyInfo>(t.GetProperties(), p =>
            {
                if (p.Name != IndexFieldName) rt += " " + p.Name + " = @" + p.Name + " ,";
            });
            rt = rt.Substring(0, rt.Length - 2);
            if (IndexFieldName != null)
                rt += " where " + IndexFieldName + " = @" + IndexFieldName;
            return rt;
        }
        #endregion

        #region 根据对象生成插入语句
        /// <summary>
        /// 获取插入语句
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="Result"></param>
        /// <param name="TableName"></param>
        /// <param name="IndexFieldName"></param>
        /// <returns></returns>
        public string GetInsertString<TResult>(string TableName, string IndexFieldName) where TResult : class,new()
        {
            string rt = "insert into " + TableName + " (";
            Type t = typeof(TResult);
            Array.ForEach<System.Reflection.PropertyInfo>(t.GetProperties(), p =>
            {
                if (p.Name != IndexFieldName) rt += p.Name + " , ";
            });
            rt = rt.Substring(0, rt.Length - 3);
            rt += ") values (";
            Array.ForEach<System.Reflection.PropertyInfo>(t.GetProperties(), p =>
            {
                if (p.Name != IndexFieldName)
                    rt += "@" + p.Name + " , ";
            });
            rt = rt.Substring(0, rt.Length - 3);
            rt += ")";
            return rt;
        }
        #endregion

        #region 对象操作
        /// <summary>
        /// 将对象插入到数据库
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="model">对象</param>
        /// <param name="TableName">表名</param>
        /// <param name="IndexFieldName">主键ID</param>
        /// <returns></returns>
        public bool InsertModel<T>(T model, string TableName, string IndexFieldName) where T : class,new()
        {
            this.CommandText = GetInsertString<T>(TableName, IndexFieldName);
            this.AddParameters<T>(model);
            return this.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// 将对象更新到数据库
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="model">对象</param>
        /// <param name="TableName">表名</param>
        /// <param name="IndexFieldName">主键ID</param>
        /// <returns></returns>
        public bool UpdateModel<T>(T model, string TableName, string IndexFieldName) where T : class,new()
        {
            this.CommandText = GetUpdateString<T>(TableName, IndexFieldName);
            this.AddParameters<T>(model);
            return this.ExecuteNonQuery() > 0;
        }
        #endregion

        #region 数据库静态方法

        #region 生成查询字符串
        /// <summary>
        /// 返回SQLSERVER连接字符串
        /// </summary>
        /// <param name="serverIp">服务器IP</param>
        /// <param name="uid">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="catalog">库名</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public static string GetSQLConnectionString(string serverIp, string uid, string pwd, string catalog, int timeout)
        {
            return string.Format("Server={0};User ID={1};PWD={2};Initial Catalog={3};Connect TimeOut={4};", serverIp, uid, pwd, catalog, timeout.ToString());
        }

        /// <summary>
        /// 返回Mdb连接字符串
        /// </summary>
        /// <param name="filePath">数据库路径</param>
        /// <param name="password">数据库密码</param>
        /// <returns></returns>
        public static string GetMdbConnectionString(string filePath, string password)
        {
            return string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Persist Security Info=False;Jet OLEDB:Database Password={1}", filePath, password);
        }

        /// <summary>
        /// 返回SQLite连接字符串
        /// </summary>
        /// <param name="filePath">数据库路径</param>
        /// <returns></returns>
        public static string GetSQLiteConnectionString(string filePath)
        {
            return string.Format("Data Source={0}", filePath);
        }
        #endregion

        #endregion
    }
}
