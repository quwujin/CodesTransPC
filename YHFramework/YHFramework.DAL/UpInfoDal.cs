using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using YHFramework.DB;
using YHFramework.SysModel;


namespace YHFramework.DAL
{
    public class UpInfoDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(UpInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [UpInfo]");
            strSql.Append("(Title,FileName,ApployTime,BathCode,Secret,CustomerEmail,Type,Url,Status,CreatedTime,UpdateTime,DownTime,CompleteTime,Remarks)");
            strSql.Append(" values (@Title,@FileName,@ApployTime,@BathCode,@Secret,@CustomerEmail,@Type,@Url,@Status,@CreatedTime,@UpdateTime,@DownTime,@CompleteTime,@Remarks)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@Title", DbTool.FixSqlParameter(model.Title))
,					new SqlParameter("@FileName", DbTool.FixSqlParameter(model.FileName))
,					new SqlParameter("@ApployTime", DbTool.FixSqlParameter(model.ApployTime))
,					new SqlParameter("@BathCode", DbTool.FixSqlParameter(model.BathCode))
,					new SqlParameter("@Secret", DbTool.FixSqlParameter(model.Secret))
,					new SqlParameter("@CustomerEmail", DbTool.FixSqlParameter(model.CustomerEmail))
,					new SqlParameter("@Type", DbTool.FixSqlParameter(model.Type))
,					new SqlParameter("@Url", DbTool.FixSqlParameter(model.Url))
,					new SqlParameter("@Status", DbTool.FixSqlParameter(model.Status))
,					new SqlParameter("@CreatedTime", DbTool.FixSqlParameter(model.CreatedTime))
,					new SqlParameter("@UpdateTime", DbTool.FixSqlParameter(model.UpdateTime))
,					new SqlParameter("@DownTime", DbTool.FixSqlParameter(model.DownTime))
,					new SqlParameter("@CompleteTime", DbTool.FixSqlParameter(model.CompleteTime))
,					new SqlParameter("@Remarks", DbTool.FixSqlParameter(model.Remarks))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(UpInfoModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UpInfo set ");
            strSql.Append("Title=@Title,FileName=@FileName,ApployTime=@ApployTime,BathCode=@BathCode,Secret=@Secret,CustomerEmail=@CustomerEmail,Type=@Type,Url=@Url,Status=@Status,CreatedTime=@CreatedTime,UpdateTime=@UpdateTime,DownTime=@DownTime,CompleteTime=@CompleteTime,Remarks=@Remarks ");
            strSql.Append(" where ID=@ID ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@Title", DbTool.FixSqlParameter(model.Title))
,					new SqlParameter("@FileName", DbTool.FixSqlParameter(model.FileName))
,					new SqlParameter("@ApployTime", DbTool.FixSqlParameter(model.ApployTime))
,					new SqlParameter("@BathCode", DbTool.FixSqlParameter(model.BathCode))
,					new SqlParameter("@Secret", DbTool.FixSqlParameter(model.Secret))
,					new SqlParameter("@CustomerEmail", DbTool.FixSqlParameter(model.CustomerEmail))
,					new SqlParameter("@Type", DbTool.FixSqlParameter(model.Type))
,					new SqlParameter("@Url", DbTool.FixSqlParameter(model.Url))
,					new SqlParameter("@Status", DbTool.FixSqlParameter(model.Status))
,					new SqlParameter("@CreatedTime", DbTool.FixSqlParameter(model.CreatedTime))
,					new SqlParameter("@UpdateTime", DbTool.FixSqlParameter(model.UpdateTime))
,					new SqlParameter("@DownTime", DbTool.FixSqlParameter(model.DownTime))
,					new SqlParameter("@CompleteTime", DbTool.FixSqlParameter(model.CompleteTime))
,					new SqlParameter("@Remarks", DbTool.FixSqlParameter(model.Remarks))
,					new SqlParameter("@ID", model.ID)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
         #endregion
         
        #region Delete
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from UpInfo where ID = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(UpInfoModel model,SqlDataReader dr)
        {

                model.ID = DbTool.ConvertObject<System.Int32>(dr["ID"]);
                model.Title = DbTool.ConvertObject<System.String>(dr["Title"]);
                model.FileName = DbTool.ConvertObject<System.String>(dr["FileName"]);
                model.ApployTime = DbTool.ConvertObject<System.DateTime>(dr["ApployTime"]);
                model.BathCode = DbTool.ConvertObject<System.String>(dr["BathCode"]);
                model.Secret = DbTool.ConvertObject<System.String>(dr["Secret"]);
                model.CustomerEmail = DbTool.ConvertObject<System.String>(dr["CustomerEmail"]);
                model.Type = DbTool.ConvertObject<System.Int32>(dr["Type"]);
                model.Url = DbTool.ConvertObject<System.String>(dr["Url"]);
                model.Status = DbTool.ConvertObject<System.Int32>(dr["Status"]);
                model.CreatedTime = DbTool.ConvertObject<System.DateTime>(dr["CreatedTime"]);
                model.UpdateTime = DbTool.ConvertObject<System.DateTime>(dr["UpdateTime"]);
                model.DownTime = DbTool.ConvertObject<System.DateTime>(dr["DownTime"]);
                model.CompleteTime = DbTool.ConvertObject<System.DateTime>(dr["CompleteTime"]);
                model.Remarks = DbTool.ConvertObject<System.String>(dr["Remarks"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected UpInfoModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new UpInfoModel();
                if (DbTool.HasFields("ID", fields)) model.ID = DbTool.ConvertObject<System.Int32>(dr["ID"]);
                if (DbTool.HasFields("Title", fields)) model.Title = DbTool.ConvertObject<System.String>(dr["Title"]);
                if (DbTool.HasFields("FileName", fields)) model.FileName = DbTool.ConvertObject<System.String>(dr["FileName"]);
                if (DbTool.HasFields("ApployTime", fields)) model.ApployTime = DbTool.ConvertObject<System.DateTime>(dr["ApployTime"]);
                if (DbTool.HasFields("BathCode", fields)) model.BathCode = DbTool.ConvertObject<System.String>(dr["BathCode"]);
                if (DbTool.HasFields("Secret", fields)) model.Secret = DbTool.ConvertObject<System.String>(dr["Secret"]);
                if (DbTool.HasFields("CustomerEmail", fields)) model.CustomerEmail = DbTool.ConvertObject<System.String>(dr["CustomerEmail"]);
                if (DbTool.HasFields("Type", fields)) model.Type = DbTool.ConvertObject<System.Int32>(dr["Type"]);
                if (DbTool.HasFields("Url", fields)) model.Url = DbTool.ConvertObject<System.String>(dr["Url"]);
                if (DbTool.HasFields("Status", fields)) model.Status = DbTool.ConvertObject<System.Int32>(dr["Status"]);
                if (DbTool.HasFields("CreatedTime", fields)) model.CreatedTime = DbTool.ConvertObject<System.DateTime>(dr["CreatedTime"]);
                if (DbTool.HasFields("UpdateTime", fields)) model.UpdateTime = DbTool.ConvertObject<System.DateTime>(dr["UpdateTime"]);
                if (DbTool.HasFields("DownTime", fields)) model.DownTime = DbTool.ConvertObject<System.DateTime>(dr["DownTime"]);
                if (DbTool.HasFields("CompleteTime", fields)) model.CompleteTime = DbTool.ConvertObject<System.DateTime>(dr["CompleteTime"]);
                if (DbTool.HasFields("Remarks", fields)) model.Remarks = DbTool.ConvertObject<System.String>(dr["Remarks"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from UpInfo where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        /// <summary>
        /// 获取记录数
        /// </summary>
        public int GetRowCount()
        {
            string sql = "select count(*) from UpInfo ";

            var obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, sql,null); 
            return DbTool.ConvertObject<int>(obj,0);
        }

        #region GetModel
        public UpInfoModel GetModel(int Id)
        {

            string sql = "select top 1 * from UpInfo where ID =" + Id;
            UpInfoModel model = new UpInfoModel();
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
        public List<UpInfoModel> GetModelList()
        {

            List<UpInfoModel> result = new List<UpInfoModel>();
            string sql = "select * from UpInfo where 1=1";
            UpInfoModel model = new UpInfoModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new UpInfoModel(); 
                 BindDataReader(model, dr);
                 result.Add(model);
            }
            dr.Close();
            return result;
        }
         #endregion
         

        
 
    }
}
