using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using YHFramework.SysModel;
using YHFramework.DB;

namespace YHFramework.DAL
{
    public class ActionLogDal
    {
        public string conn = SqlHelper.ConnectionString;



        #region Dal Core Functional

        #region Add

        public int Add(ActionLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [ActionLog]");
            strSql.Append("(ActionName,UserName,ActionResult,KeyData,Notes,CreateOn)");
            strSql.Append(" values (@ActionName,@UserName,@ActionResult,@KeyData,@Notes,@CreateOn)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@ActionName", DbTool.FixSqlParameter(model.ActionName))
,					new SqlParameter("@UserName", DbTool.FixSqlParameter(model.UserName))
,					new SqlParameter("@ActionResult", DbTool.FixSqlParameter(model.ActionResult))
,					new SqlParameter("@KeyData", DbTool.FixSqlParameter(model.KeyData))
,					new SqlParameter("@Notes", DbTool.FixSqlParameter(model.Notes))
,					new SqlParameter("@CreateOn", DbTool.FixSqlParameter(model.CreateOn))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters), 0);

        }
         
        #endregion

        #region Update
        public int Update(ActionLogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ActionLog set ");
            strSql.Append("ActionName=@ActionName,UserName=@UserName,ActionResult=@ActionResult,KeyData=@KeyData,Notes=@Notes,CreateOn=@CreateOn ");
            strSql.Append(" where ActionLogId=@ActionLogId ");

            SqlParameter[] parameters = {
					new SqlParameter("@ActionName", DbTool.FixSqlParameter(model.ActionName))
,					new SqlParameter("@UserName", DbTool.FixSqlParameter(model.UserName))
,					new SqlParameter("@ActionResult", DbTool.FixSqlParameter(model.ActionResult))
,					new SqlParameter("@KeyData", DbTool.FixSqlParameter(model.KeyData))
,					new SqlParameter("@Notes", DbTool.FixSqlParameter(model.Notes))
,					new SqlParameter("@CreateOn", DbTool.FixSqlParameter(model.CreateOn))
,					new SqlParameter("@ActionLogId", model.ActionLogId)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
        #endregion

        #region Delete
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from ActionLog where ActionLogId = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
        #endregion

        #region BindDataReader
        protected void BindDataReader(ActionLogModel model, SqlDataReader dr)
        {

            model.ActionLogId = DbTool.ConvertObject<System.Int32>(dr["ActionLogId"]);
            model.ActionName = DbTool.ConvertObject<System.String>(dr["ActionName"]);
            model.UserName = DbTool.ConvertObject<System.String>(dr["UserName"]);
            model.ActionResult = DbTool.ConvertObject<System.String>(dr["ActionResult"]);
            model.KeyData = DbTool.ConvertObject<System.String>(dr["KeyData"]);
            model.Notes = DbTool.ConvertObject<System.String>(dr["Notes"]);
            model.CreateOn = DbTool.ConvertObject<System.DateTime>(dr["CreateOn"]);


        }
        #endregion

        #region AutoBindDataReader
        protected ActionLogModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

            var model = new ActionLogModel();
            if (DbTool.HasFields("ActionLogId", fields)) model.ActionLogId = DbTool.ConvertObject<System.Int32>(dr["ActionLogId"]);
            if (DbTool.HasFields("ActionName", fields)) model.ActionName = DbTool.ConvertObject<System.String>(dr["ActionName"]);
            if (DbTool.HasFields("UserName", fields)) model.UserName = DbTool.ConvertObject<System.String>(dr["UserName"]);
            if (DbTool.HasFields("ActionResult", fields)) model.ActionResult = DbTool.ConvertObject<System.String>(dr["ActionResult"]);
            if (DbTool.HasFields("KeyData", fields)) model.KeyData = DbTool.ConvertObject<System.String>(dr["KeyData"]);
            if (DbTool.HasFields("Notes", fields)) model.Notes = DbTool.ConvertObject<System.String>(dr["Notes"]);
            if (DbTool.HasFields("CreateOn", fields)) model.CreateOn = DbTool.ConvertObject<System.DateTime>(dr["CreateOn"]);

            return model;

        }
        #endregion

        #endregion

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from ActionLog where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
        #endregion

        /// <summary>
        /// 获取记录数
        /// </summary>
        public int GetRowCount()
        {
            string sql = "select count(*) from ActionLog ";

            var obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, sql, null);
            return DbTool.ConvertObject<int>(obj, 0);
        }

        #region GetModel
        public ActionLogModel GetModel(int Id)
        {

            string sql = "select top 1 * from ActionLog where ActionLogId =" + Id;
            ActionLogModel model = new ActionLogModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            if (dr.Read())
            {
                //var fields = DbTool.GetReaderFieldNames(dr);
                //model = AutoBindDataReader(dr, fields);
                BindDataReader(model, dr);
            }
            dr.Close();
            return model;
        }
        #endregion

        #region GetModelList
        public List<ActionLogModel> GetModelList()
        {

            List<ActionLogModel> result = new List<ActionLogModel>();
            string sql = "select * from ActionLog where 1=1";
            ActionLogModel model = new ActionLogModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                //model = AutoBindDataReader(dr, fields);
                model = new ActionLogModel();
                BindDataReader(model, dr);
                result.Add(model);
            }
            dr.Close();
            return result;
        }
        #endregion




    }
}
