using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using YHFramework.DB;

namespace YHFramework.DAL
{
    public class ApployCheckLogDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(YHFramework.SysModel.ApployCheckLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [ApployCheckLog]");
            strSql.Append("(ApployId,TypeCode,FileName,Number,Message,CreateOn,UpdateOn)");
            strSql.Append(" values (@ApployId,@TypeCode,@FileName,@Number,@Message,@CreateOn,@UpdateOn)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@ApployId", DbTool.FixSqlParameter(model.ApployId))
,					new SqlParameter("@TypeCode", DbTool.FixSqlParameter(model.TypeCode))
,					new SqlParameter("@FileName", DbTool.FixSqlParameter(model.FileName))
,					new SqlParameter("@Number", DbTool.FixSqlParameter(model.Number))
,					new SqlParameter("@Message", DbTool.FixSqlParameter(model.Message))
,					new SqlParameter("@CreateOn", DbTool.FixSqlParameter(model.CreateOn))
,					new SqlParameter("@UpdateOn", DbTool.FixSqlParameter(model.UpdateOn))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(YHFramework.SysModel.ApployCheckLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ApployCheckLog set ");
            strSql.Append("ApployId=@ApployId,TypeCode=@TypeCode,FileName=@FileName,Number=@Number,Message=@Message,CreateOn=@CreateOn,UpdateOn=@UpdateOn ");
            strSql.Append(" where CheckLogId=@CheckLogId ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@ApployId", DbTool.FixSqlParameter(model.ApployId))
,					new SqlParameter("@TypeCode", DbTool.FixSqlParameter(model.TypeCode))
,					new SqlParameter("@FileName", DbTool.FixSqlParameter(model.FileName))
,					new SqlParameter("@Number", DbTool.FixSqlParameter(model.Number))
,					new SqlParameter("@Message", DbTool.FixSqlParameter(model.Message))
,					new SqlParameter("@CreateOn", DbTool.FixSqlParameter(model.CreateOn))
,					new SqlParameter("@UpdateOn", DbTool.FixSqlParameter(model.UpdateOn))
,					new SqlParameter("@CheckLogId", model.CheckLogId)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
         #endregion
         
        #region Delete
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from ApployCheckLog where CheckLogId = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(YHFramework.SysModel.ApployCheckLogModel model, SqlDataReader dr)
        {

                model.CheckLogId = DbTool.ConvertObject<System.Int32>(dr["CheckLogId"]);
                model.ApployId = DbTool.ConvertObject<System.Int32>(dr["ApployId"]);
                model.TypeCode = DbTool.ConvertObject<System.String>(dr["TypeCode"]);
                model.FileName = DbTool.ConvertObject<System.String>(dr["FileName"]);
                model.Number = DbTool.ConvertObject<System.Int64>(dr["Number"]);
                model.Message = DbTool.ConvertObject<System.String>(dr["Message"]);
                model.CreateOn = DbTool.ConvertObject<System.DateTime>(dr["CreateOn"]);
                model.UpdateOn = DbTool.ConvertObject<System.DateTime>(dr["UpdateOn"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected YHFramework.SysModel.ApployCheckLogModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

            var model = new YHFramework.SysModel.ApployCheckLogModel();
                if (DbTool.HasFields("CheckLogId", fields)) model.CheckLogId = DbTool.ConvertObject<System.Int32>(dr["CheckLogId"]);
                if (DbTool.HasFields("ApployId", fields)) model.ApployId = DbTool.ConvertObject<System.Int32>(dr["ApployId"]);
                if (DbTool.HasFields("TypeCode", fields)) model.TypeCode = DbTool.ConvertObject<System.String>(dr["TypeCode"]);
                if (DbTool.HasFields("FileName", fields)) model.FileName = DbTool.ConvertObject<System.String>(dr["FileName"]);
                if (DbTool.HasFields("Number", fields)) model.Number = DbTool.ConvertObject<System.Int64>(dr["Number"]);
                if (DbTool.HasFields("Message", fields)) model.Message = DbTool.ConvertObject<System.String>(dr["Message"]);
                if (DbTool.HasFields("CreateOn", fields)) model.CreateOn = DbTool.ConvertObject<System.DateTime>(dr["CreateOn"]);
                if (DbTool.HasFields("UpdateOn", fields)) model.UpdateOn = DbTool.ConvertObject<System.DateTime>(dr["UpdateOn"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from ApployCheckLog where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        /// <summary>
        /// 获取记录数
        /// </summary>
        public int GetRowCount()
        {
            string sql = "select count(*) from ApployCheckLog ";

            var obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, sql,null); 
            return DbTool.ConvertObject<int>(obj,0);
        }

        #region GetModel
        public YHFramework.SysModel.ApployCheckLogModel GetModel(int Id)
        {

            string sql = "select top 1 * from ApployCheckLog where CheckLogId =" + Id;
            YHFramework.SysModel.ApployCheckLogModel model = new YHFramework.SysModel.ApployCheckLogModel();
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
        public List<YHFramework.SysModel.ApployCheckLogModel> GetModelList()
        {

            List<YHFramework.SysModel.ApployCheckLogModel> result = new List<YHFramework.SysModel.ApployCheckLogModel>();
            string sql = "select * from ApployCheckLog where 1=1";
            YHFramework.SysModel.ApployCheckLogModel model = new YHFramework.SysModel.ApployCheckLogModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                model = new YHFramework.SysModel.ApployCheckLogModel(); 
                 BindDataReader(model, dr);
                 result.Add(model);
            }
            dr.Close();
            return result;
        }
         #endregion
         

        
 
    }
}
