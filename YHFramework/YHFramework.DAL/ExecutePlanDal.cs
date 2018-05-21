using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using YHFramework.DB;

namespace YHFramework.DAL
{
    public class ExecutePlanDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(YHFramework.SysModel.ExecutePlanModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [ExecutePlan]");
            strSql.Append("(CodeType,MaxCount,EachCount,BatchCodeBegin,EffectiveDateBegin,GeneratedCount,CurrentBatchCode,Status,CustomerEmail)");
            strSql.Append(" values (@CodeType,@MaxCount,@EachCount,@BatchCodeBegin,@EffectiveDateBegin,@GeneratedCount,@CurrentBatchCode,@Status,@CustomerEmail)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@CodeType", DbTool.FixSqlParameter(model.CodeType))
,					new SqlParameter("@MaxCount", DbTool.FixSqlParameter(model.MaxCount))
,					new SqlParameter("@EachCount", DbTool.FixSqlParameter(model.EachCount))
,					new SqlParameter("@BatchCodeBegin", DbTool.FixSqlParameter(model.BatchCodeBegin))
,					new SqlParameter("@EffectiveDateBegin", DbTool.FixSqlParameter(model.EffectiveDateBegin))
,					new SqlParameter("@GeneratedCount", DbTool.FixSqlParameter(model.GeneratedCount))
,					new SqlParameter("@CurrentBatchCode", DbTool.FixSqlParameter(model.CurrentBatchCode))
,					new SqlParameter("@Status", DbTool.FixSqlParameter(model.Status))
,					new SqlParameter("@CustomerEmail", DbTool.FixSqlParameter(model.CustomerEmail))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(YHFramework.SysModel.ExecutePlanModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ExecutePlan set ");
            strSql.Append("CodeType=@CodeType,MaxCount=@MaxCount,EachCount=@EachCount,BatchCodeBegin=@BatchCodeBegin,EffectiveDateBegin=@EffectiveDateBegin,GeneratedCount=@GeneratedCount,CurrentBatchCode=@CurrentBatchCode,Status=@Status,CustomerEmail=@CustomerEmail ");
            strSql.Append(" where Id=@Id ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@CodeType", DbTool.FixSqlParameter(model.CodeType))
,					new SqlParameter("@MaxCount", DbTool.FixSqlParameter(model.MaxCount))
,					new SqlParameter("@EachCount", DbTool.FixSqlParameter(model.EachCount))
,					new SqlParameter("@BatchCodeBegin", DbTool.FixSqlParameter(model.BatchCodeBegin))
,					new SqlParameter("@EffectiveDateBegin", DbTool.FixSqlParameter(model.EffectiveDateBegin))
,					new SqlParameter("@GeneratedCount", DbTool.FixSqlParameter(model.GeneratedCount))
,					new SqlParameter("@CurrentBatchCode", DbTool.FixSqlParameter(model.CurrentBatchCode))
,					new SqlParameter("@Status", DbTool.FixSqlParameter(model.Status))
,					new SqlParameter("@CustomerEmail", DbTool.FixSqlParameter(model.CustomerEmail))
,					new SqlParameter("@Id", model.Id)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
         #endregion
         
        #region Delete
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from ExecutePlan where Id = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(YHFramework.SysModel.ExecutePlanModel model,SqlDataReader dr)
        {

                model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                model.CodeType = DbTool.ConvertObject<System.Int32>(dr["CodeType"]);
                model.MaxCount = DbTool.ConvertObject<System.Int32>(dr["MaxCount"]);
                model.EachCount = DbTool.ConvertObject<System.Int32>(dr["EachCount"]);
                model.BatchCodeBegin = DbTool.ConvertObject<System.Int32>(dr["BatchCodeBegin"]);
                model.EffectiveDateBegin = DbTool.ConvertObject<System.DateTime>(dr["EffectiveDateBegin"]);
                model.GeneratedCount = DbTool.ConvertObject<System.Int32>(dr["GeneratedCount"]);
                model.CurrentBatchCode = DbTool.ConvertObject<System.Int32>(dr["CurrentBatchCode"]);
                model.Status = DbTool.ConvertObject<System.Int32>(dr["Status"]);
                model.CustomerEmail = DbTool.ConvertObject<System.String>(dr["CustomerEmail"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected YHFramework.SysModel.ExecutePlanModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new YHFramework.SysModel.ExecutePlanModel();
                if (DbTool.HasFields("Id", fields)) model.Id = DbTool.ConvertObject<System.Int32>(dr["Id"]);
                if (DbTool.HasFields("CodeType", fields)) model.CodeType = DbTool.ConvertObject<System.Int32>(dr["CodeType"]);
                if (DbTool.HasFields("MaxCount", fields)) model.MaxCount = DbTool.ConvertObject<System.Int32>(dr["MaxCount"]);
                if (DbTool.HasFields("EachCount", fields)) model.EachCount = DbTool.ConvertObject<System.Int32>(dr["EachCount"]);
                if (DbTool.HasFields("BatchCodeBegin", fields)) model.BatchCodeBegin = DbTool.ConvertObject<System.Int32>(dr["BatchCodeBegin"]);
                if (DbTool.HasFields("EffectiveDateBegin", fields)) model.EffectiveDateBegin = DbTool.ConvertObject<System.DateTime>(dr["EffectiveDateBegin"]);
                if (DbTool.HasFields("GeneratedCount", fields)) model.GeneratedCount = DbTool.ConvertObject<System.Int32>(dr["GeneratedCount"]);
                if (DbTool.HasFields("CurrentBatchCode", fields)) model.CurrentBatchCode = DbTool.ConvertObject<System.Int32>(dr["CurrentBatchCode"]);
                if (DbTool.HasFields("Status", fields)) model.Status = DbTool.ConvertObject<System.Int32>(dr["Status"]);
                if (DbTool.HasFields("CustomerEmail", fields)) model.CustomerEmail = DbTool.ConvertObject<System.String>(dr["CustomerEmail"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from ExecutePlan where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        /// <summary>
        /// 获取记录数
        /// </summary>
        public int GetRowCount()
        {
            string sql = "select count(*) from ExecutePlan ";

            var obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, sql,null); 
            return DbTool.ConvertObject<int>(obj,0);
        }

        #region GetModel
        public YHFramework.SysModel.ExecutePlanModel GetModel(int Id)
        {

            string sql = "select top 1 * from ExecutePlan where Id =" + Id;
            YHFramework.SysModel.ExecutePlanModel model = new YHFramework.SysModel.ExecutePlanModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read()) {
                 //var fields = DbTool.GetReaderFieldNames(dr);
                 //model = AutoBindDataReader(dr, fields);
                 BindDataReader(model, dr);
            }
            dr.Close();
            return model;
        }
         #endregion
         
        #region GetModelList
        public List<YHFramework.SysModel.ExecutePlanModel> GetModelList()
        {

            List<YHFramework.SysModel.ExecutePlanModel> result = new List<YHFramework.SysModel.ExecutePlanModel>();
            string sql = "select * from ExecutePlan where 1=1";
            YHFramework.SysModel.ExecutePlanModel model = new YHFramework.SysModel.ExecutePlanModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new YHFramework.SysModel.ExecutePlanModel(); 
                 BindDataReader(model, dr);
                 result.Add(model);
            }
            dr.Close();
            return result;
        }
         #endregion
         

        
 
    }
}
