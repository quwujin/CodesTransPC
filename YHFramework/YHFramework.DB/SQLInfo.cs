using System;
using System.Data;
using System.Data.SqlClient;

namespace YHFramework.DB
{
	public class SQLInfo
	{
		public CommandType cmdType
		{
			get;
			set;
		}

		public string SQLStr
		{
			get;
			set;
		}

		public SqlParameter[] parameters
		{
			get;
			set;
		}
	}
}
