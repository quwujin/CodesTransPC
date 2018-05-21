using System;

namespace YHFramework.SysModel
{
	public class PageInfo
	{
		public string TableName
		{
			get;
			set;
		}

		public string SqlWhere
		{
			get;
			set;
		}

		public string ReturnFileds
		{
			get;
			set;
		}

		public string SelectFileds
		{
			get;
			set;
		}

		public int PageSize
		{
			get;
			set;
		}

		public string CountFields
		{
			get;
			set;
		}

		public int PageIndex
		{
			get;
			set;
		}

		public int doCount
		{
			get;
			set;
		}

		public string JoinTable
		{
			get;
			set;
		}

		public string OrderString
		{
			get;
			set;
		}
	}
}
