using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace YHFramework.DB
{
	public abstract class SqlHelper
	{
		public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;

		private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

		public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
		{
			SqlCommand cmd = new SqlCommand();
			int result;
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				SqlHelper.PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
				int val = cmd.ExecuteNonQuery();
				cmd.Parameters.Clear();
				conn.Close();
				conn.Dispose();
				result = val;
			}
			return result;
		}

		public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
		{
			SqlCommand cmd = new SqlCommand();
			int val = 0;
			try
			{
				SqlHelper.PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
				val = cmd.ExecuteNonQuery();
				cmd.Parameters.Clear();
			}
			catch
			{
			}
			finally
			{
				connection.Close();
				connection.Dispose();
			}
			return val;
		}

		public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
		{
			SqlCommand cmd = new SqlCommand();
			SqlHelper.PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
			int val = cmd.ExecuteNonQuery();
			cmd.Parameters.Clear();
			return val;
		}

		public static void ExecuteSqlTran(List<SQLInfo> SQLInfoList)
		{
			using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = conn;
				SqlTransaction tx = conn.BeginTransaction();
				cmd.Transaction = tx;
				try
				{
					foreach (SQLInfo sqlinfo in SQLInfoList)
					{
						cmd.Parameters.Clear();
						SqlHelper.PrepareCommand(cmd, conn, tx, sqlinfo.cmdType, sqlinfo.SQLStr, sqlinfo.parameters);
						cmd.ExecuteNonQuery();
					}
					tx.Commit();
				}
				catch
				{
					tx.Rollback();
				}
			}
		}

		public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
		{
			SqlCommand cmd = new SqlCommand();
			SqlConnection conn = new SqlConnection(connectionString);
			SqlDataReader result;
			try
			{
				SqlHelper.PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
				SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
				cmd.Parameters.Clear();
				result = rdr;
			}
			catch
			{
				conn.Close();
				throw;
			}
			return result;
		}

		public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
		{
			SqlCommand cmd = new SqlCommand();
			object result;
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				SqlHelper.PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
				object val = cmd.ExecuteScalar();
				cmd.Parameters.Clear();
				connection.Close();
				connection.Dispose();
				result = val;
			}
			return result;
		}

		public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
		{
			SqlCommand cmd = new SqlCommand();
			SqlHelper.PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
			object val = cmd.ExecuteScalar();
			cmd.Parameters.Clear();
			return val;
		}

		public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
		{
			SqlHelper.parmCache[cacheKey] = commandParameters;
		}

		public static SqlParameter[] GetCachedParameters(string cacheKey)
		{
			SqlParameter[] cachedParms = (SqlParameter[])SqlHelper.parmCache[cacheKey];
			SqlParameter[] result;
			if (cachedParms == null)
			{
				result = null;
			}
			else
			{
				SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];
				int i = 0;
				int j = cachedParms.Length;
				while (i < j)
				{
					clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();
					i++;
				}
				result = clonedParms;
			}
			return result;
		}

		private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
		{
			if (conn.State != ConnectionState.Open)
			{
				conn.Open();
			}
			cmd.Connection = conn;
			cmd.CommandText = cmdText;
			cmd.CommandTimeout = 300;
			if (trans != null)
			{
				cmd.Transaction = trans;
			}
			cmd.CommandType = cmdType;
			if (cmdParms != null)
			{
				for (int i = 0; i < cmdParms.Length; i++)
				{
					SqlParameter parm = cmdParms[i];
					cmd.Parameters.Add(parm);
				}
			}
		}

		public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText)
		{
			return SqlHelper.ExecuteDataset(connectionString, commandType, commandText, null);
		}

		public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			DataSet result;
			using (SqlConnection cn = new SqlConnection(connectionString))
			{
				cn.Open();
				result = SqlHelper.ExecuteDataset(cn, commandType, commandText, commandParameters);
			}
			return result;
		}

		public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText)
		{
			return SqlHelper.ExecuteDataset(connection, commandType, commandText, null);
		}

		public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			SqlCommand cmd = new SqlCommand();
			SqlHelper.PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);
			cmd.Parameters.Clear();
			return ds;
		}

		public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText)
		{
			return SqlHelper.ExecuteDataset(transaction, commandType, commandText, null);
		}

		public static DataSet ExecuteDataset(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			SqlCommand cmd = new SqlCommand();
			SqlHelper.PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataSet ds = new DataSet();
			da.Fill(ds);
			cmd.Parameters.Clear();
			return ds;
		}

		public static DataTable GetTableSchema(string sqlConnString, string tableName)
		{
			SqlConnection sqlConnection = new SqlConnection(sqlConnString);
			return SqlHelper.GetTableSchema(sqlConnection, tableName);
		}

		public static DataTable GetTableSchema(SqlConnection sqlConnection, string tableName)
		{
			if (sqlConnection != null && sqlConnection.State == ConnectionState.Closed)
			{
				sqlConnection.Open();
			}
			string sqlCommand = "select * from " + tableName + " where 1=0";
			SqlDataAdapter da = new SqlDataAdapter(sqlCommand, sqlConnection);
			DataSet ds = new DataSet();
			da.FillSchema(ds, SchemaType.Source);
			ds.Tables[0].TableName = tableName;
			return ds.Tables[0];
		}

		public static DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText)
		{
			return SqlHelper.ExecuteDataTable(connectionString, commandType, commandText, null);
		}

		public static DataTable ExecuteDataTable(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			DataTable result;
			using (SqlConnection cn = new SqlConnection(connectionString))
			{
				cn.Open();
				result = SqlHelper.ExecuteDataTable(cn, commandType, commandText, commandParameters);
			}
			return result;
		}

		public static DataTable ExecuteDataTable(SqlConnection connection, CommandType commandType, string commandText)
		{
			return SqlHelper.ExecuteDataTable(connection, commandType, commandText, null);
		}

		public static DataTable ExecuteDataTable(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandTimeout = 0;
			SqlHelper.PrepareCommand(cmd, connection, null, commandType, commandText, commandParameters);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			cmd.Parameters.Clear();
			connection.Close();
			connection.Dispose();
			return dt;
		}

		public static DataTable ExecuteDataTable(SqlTransaction transaction, CommandType commandType, string commandText)
		{
			return SqlHelper.ExecuteDataTable(transaction, commandType, commandText, null);
		}

		public static DataTable ExecuteDataTable(SqlTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
		{
			SqlCommand cmd = new SqlCommand();
			SqlHelper.PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataTable dt = new DataTable();
			da.Fill(dt);
			cmd.Parameters.Clear();
			return dt;
		}
	}
}
