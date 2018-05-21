using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using YHFramework.DB;

namespace YHFramework.DAL
{
	public class TableDAL
	{
		public static string DefaultConnectionString
		{
			get
			{
				string strConn = "";
				try
				{
					strConn = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
				}
				catch
				{
				}
				return strConn;
			}
		}

		internal static DataTable GetTableInsertSchema(DataTable schema)
		{
			DataTable newTableSchema = schema.Copy();
			try
			{
				DataColumn[] primaryKey2 = schema.PrimaryKey;
				for (int i = 0; i < primaryKey2.Length; i++)
				{
					DataColumn primaryKey = primaryKey2[i];
					if (schema.Columns[primaryKey.ColumnName].AutoIncrement)
					{
						newTableSchema.Columns.Remove(primaryKey.ColumnName);
					}
				}
			}
			catch
			{
			}
			return newTableSchema;
		}

		public static void GetDataAndExecute(string tableName, int bigCount, string condition, ExecuteWithDataTable exec)
		{
			int maxCountEach = 100;
			int totalCount = 0;
			int remainCount;
			do
			{
				remainCount = bigCount - totalCount;
				int topCount = Math.Min(maxCountEach, remainCount);
				DataTable dt = TableDAL.GetTopTable(topCount, tableName, condition);
				if (dt != null && 0 < dt.Rows.Count)
				{
					if (null != exec)
					{
						exec(dt);
					}
					totalCount += topCount;
					remainCount = bigCount - totalCount;
				}
				else
				{
					remainCount = 0;
				}
			}
			while (remainCount > 0);
		}

		public static DataRow GetNewNullRow(string tableName)
		{
			DataTable dtNew = TableDAL.GetNullValueTable(tableName);
			DataRow dr = dtNew.NewRow();
			dtNew.Rows.Add(dr);
			return dr;
		}

		public static DataTable GetNullValueTable(string tableName)
		{
			return TableDAL.GetNullValueTable(TableDAL.DefaultConnectionString, tableName);
		}

		public static DataTable GetNullValueTable(string connectionString, string tableName)
		{
			string strSql = "select * from [{0}] where 1=0";
			strSql = string.Format(strSql, tableName.Replace("[", "").Replace("]", ""));
			return SqlHelper.ExecuteDataTable(connectionString, CommandType.Text, strSql);
		}

		public static DataTable GetTopTable(int topCount, string tableName, string condition)
		{
			SqlConnection cn = new SqlConnection(TableDAL.DefaultConnectionString);
			return TableDAL.GetTable(tableName, condition, " top " + topCount + " * ");
		}

		public static DataTable GetTable(string tableName, string condition)
		{
			SqlConnection cn = new SqlConnection(TableDAL.DefaultConnectionString);
			return TableDAL.GetTable(cn, tableName, condition, "");
		}

		public static DataTable GetTable(string tableName, string condition, string fieldList)
		{
			SqlConnection cn = new SqlConnection(TableDAL.DefaultConnectionString);
			return TableDAL.GetTable(cn, tableName, condition, fieldList);
		}

		public static DataTable GetTable(SqlConnection connection, string tableName, string condition)
		{
			return TableDAL.GetTable(connection, tableName, condition, "");
		}

		public static DataTable GetTable(SqlConnection connection, string tableName, string condition, string fieldList)
		{
			string strSql = "select {2} from {0} where 1=1 and {1}";
			strSql = string.Format(strSql, tableName, string.IsNullOrEmpty(condition) ? "1=1" : condition, string.IsNullOrEmpty(fieldList) ? "*" : fieldList);
			if (connection != null && connection.State == ConnectionState.Closed)
			{
				connection.Open();
			}
			return SqlHelper.ExecuteDataTable(connection, CommandType.Text, strSql);
		}

		public static int GetTableCount(string tableName, string condition)
		{
			SqlConnection cn = new SqlConnection(TableDAL.DefaultConnectionString);
			return TableDAL.GetTableCount(cn, tableName, condition);
		}

		public static int GetTableCount(SqlConnection connection, string tableName, string condition)
		{
			int count = 0;
			DataTable dt = TableDAL.GetTable(connection, tableName, condition);
			if (null != dt & 0 < dt.Rows.Count)
			{
				count = dt.Rows.Count;
			}
			return count;
		}

		public static DataTable GetTableSchema(string tableName)
		{
			return TableDAL.GetTableSchema(TableDAL.DefaultConnectionString, tableName);
		}

		public static DataTable GetTableSchema(string connectionString, string tableName)
		{
			return SqlHelper.GetTableSchema(connectionString, tableName);
		}

		public static int InsertDataTable(DataTable tableToInsert, string tableName)
		{
			return TableDAL.InsertDataTable(tableToInsert, TableDAL.GetTableSchema(tableName));
		}

		public static int InsertDataTable(DataTable tableToInsert, DataTable tableSchema)
		{
			return TableDAL.InsertDataTable(TableDAL.DefaultConnectionString, tableToInsert, tableSchema);
		}

		public static int InsertDataTable(string connectionString, DataTable tableToInsert, DataTable tableSchema)
		{
			DataTable schema = TableDAL.GetTableInsertSchema(tableSchema.Copy());
			return TableDAL.Insert(connectionString, tableToInsert, tableSchema);
		}

		public static int InsertOneRowDataTable(DataTable table, string tableName)
		{
			int id = 0;
			DataTable tableSchema = TableDAL.GetTableSchema(tableName);
			if (string.IsNullOrEmpty(tableSchema.TableName))
			{
			}
			string strSql = " insert into {0}({1}) values({2}); select @@identity";
			StringBuilder insertFieldSqlExp = new StringBuilder();
			StringBuilder insertValueSqlExp = new StringBuilder();
			foreach (DataColumn insertColumn in tableSchema.Columns)
			{
				if (!insertColumn.AutoIncrement)
				{
					insertFieldSqlExp.Append(insertColumn.ColumnName + ", ");
					insertValueSqlExp.Append("@" + insertColumn.ColumnName + ", ");
				}
			}
			string strInserFieldSqlExp = insertFieldSqlExp.ToString().TrimEnd(new char[]
			{
				',',
				' '
			});
			string strInsertValueSqlExp = insertValueSqlExp.ToString().TrimEnd(new char[]
			{
				',',
				' '
			});
			strSql = string.Format(strSql, tableSchema.TableName, strInserFieldSqlExp, strInsertValueSqlExp);
			List<SqlParameter> parameters = new List<SqlParameter>();
			if (table != null && 0 < table.Rows.Count)
			{
				DataRow dataRow = table.Rows[0];
				foreach (DataColumn schemaColumn in tableSchema.Columns)
				{
					SqlParameter dbParam = new SqlParameter("@" + schemaColumn.ColumnName, dataRow[schemaColumn.ColumnName]);
					parameters.Add(dbParam);
				}
			}
			try
			{
				id = Convert.ToInt32(SqlHelper.ExecuteScalar(TableDAL.DefaultConnectionString, CommandType.Text, strSql, parameters.ToArray()));
			}
			catch
			{
			}
			return id;
		}

		internal static int Insert(string connectionString, DataTable tableToInsert, DataTable tableSchema)
		{
			int effectCount = 0;
			int result;
			try
			{
				if (string.IsNullOrEmpty(tableSchema.TableName))
				{
				}
				string strSql = " insert into {0}({1}) values({2})";
				StringBuilder insertFieldSqlExp = new StringBuilder();
				StringBuilder insertValueSqlExp = new StringBuilder();
				foreach (DataColumn insertColumn in tableSchema.Columns)
				{
					if (!insertColumn.AutoIncrement)
					{
						insertFieldSqlExp.Append(insertColumn.ColumnName + ", ");
						insertValueSqlExp.Append("@" + insertColumn.ColumnName + ", ");
					}
				}
				string strInserFieldSqlExp = insertFieldSqlExp.ToString().TrimEnd(new char[]
				{
					',',
					' '
				});
				string strInsertValueSqlExp = insertValueSqlExp.ToString().TrimEnd(new char[]
				{
					',',
					' '
				});
				strSql = string.Format(strSql, tableSchema.TableName, strInserFieldSqlExp, strInsertValueSqlExp);
				foreach (DataRow dataRow in tableToInsert.Rows)
				{
					List<SqlParameter> parameters = new List<SqlParameter>();
					foreach (DataColumn schemaColumn in tableSchema.Columns)
					{
						SqlParameter dbParam = new SqlParameter("@" + schemaColumn.ColumnName, dataRow[schemaColumn.ColumnName]);
						parameters.Add(dbParam);
					}
					SqlHelper.ExecuteScalar(connectionString, CommandType.Text, strSql, parameters.ToArray());
					effectCount++;
				}
				result = effectCount;
				return result;
			}
			catch (Exception ex_212)
			{
			}
			result = effectCount;
			return result;
		}

		public static int SaveTable(DataTable table, string tableName)
		{
			return TableDAL.UpdateDataTable(table, TableDAL.GetTableSchema(tableName));
		}

		public static int UpdateDataTable(DataTable tableToUpdate, DataTable schema)
		{
			return TableDAL.UpdateDataTable(TableDAL.DefaultConnectionString, tableToUpdate, schema);
		}

		public static int UpdateDataTable(string connectionString, DataTable tableToUpdate, DataTable schema)
		{
			int effectCount = 0;
			try
			{
				if (!string.IsNullOrEmpty(schema.TableName))
				{
					string strSql = " update {0} set {1} where {2}";
					StringBuilder updateSetSqlExp = new StringBuilder();
					DataColumn[] primaryKey;
					foreach (DataColumn dataCol in schema.Columns)
					{
						primaryKey = schema.PrimaryKey;
						for (int i = 0; i < primaryKey.Length; i++)
						{
							DataColumn primaryKeyCol = primaryKey[i];
					 
							if (dataCol.ColumnName != primaryKeyCol.ColumnName)
							{
								updateSetSqlExp.Append(dataCol.ColumnName + "=@" + dataCol.ColumnName + ", ");
							}
						}
					}
					string strUpdateSqlExp = updateSetSqlExp.ToString().TrimEnd(new char[]
					{
						',',
						' '
					});
					string strUpdateConditionExp = "";
					primaryKey = schema.PrimaryKey;
					for (int i = 0; i < primaryKey.Length; i++)
					{
						DataColumn dataCol = primaryKey[i];
						strUpdateConditionExp += (string.IsNullOrEmpty(strUpdateConditionExp) ? (dataCol.ColumnName + "=@" + dataCol.ColumnName) : (" and " + dataCol.ColumnName + "=@" + dataCol.ColumnName));
					}
					strSql = string.Format(strSql, schema.TableName, strUpdateSqlExp, strUpdateConditionExp);
					foreach (DataRow dataRow in tableToUpdate.Rows)
					{
						List<SqlParameter> parameters = new List<SqlParameter>();
						foreach (DataColumn schemaColumn in schema.Columns)
						{
							SqlParameter param = new SqlParameter("@" + schemaColumn.ColumnName, dataRow[schemaColumn.ColumnName]);
							parameters.Add(param);
						}
						int currentEffectCount = SqlHelper.ExecuteNonQuery(connectionString, CommandType.Text, strSql, parameters.ToArray());
						effectCount += currentEffectCount;
					}
				}
			}
			catch (Exception ex_287)
			{
			}
			return effectCount;
		}

		public static int Delete(string tableName, int id)
		{
			string sql = string.Concat(new object[]
			{
				"delete from [",
				tableName,
				"] where id = ",
				id
			});
			return SqlHelper.ExecuteNonQuery(TableDAL.DefaultConnectionString, CommandType.Text, sql, new SqlParameter[0]);
		}
	}
}
