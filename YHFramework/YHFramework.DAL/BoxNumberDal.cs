using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using YHFramework.DB;

namespace YHFramework.DAL
{
    public class BoxNumberDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(YHFramework.SysModel.BoxNumberModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [BoxNumber]");
            strSql.Append("(BoxNumber,ShortBoxNumber,IndexCode,BatchNumber,ContentStatusId,IsBind,BindTime,UpdateOn,CreateOn,ApployInfoId)");
            strSql.Append(" values (@BoxNumber,@ShortBoxNumber,@IndexCode,@BatchNumber,@ContentStatusId,@IsBind,@BindTime,@UpdateOn,@CreateOn,@ApployInfoId)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@BoxNumber", DbTool.FixSqlParameter(model.BoxNumber))
,					new SqlParameter("@ShortBoxNumber", DbTool.FixSqlParameter(model.ShortBoxNumber))
,					new SqlParameter("@IndexCode", DbTool.FixSqlParameter(model.IndexCode))
,					new SqlParameter("@BatchNumber", DbTool.FixSqlParameter(model.BatchNumber))
,					new SqlParameter("@ContentStatusId", DbTool.FixSqlParameter(model.ContentStatusId))
,					new SqlParameter("@IsBind", DbTool.FixSqlParameter(model.IsBind))
,					new SqlParameter("@BindTime", DbTool.FixSqlParameter(model.BindTime))
,					new SqlParameter("@UpdateOn", DbTool.FixSqlParameter(model.UpdateOn))
,					new SqlParameter("@CreateOn", DbTool.FixSqlParameter(model.CreateOn))
,					new SqlParameter("@ApployInfoId", DbTool.FixSqlParameter(model.ApployInfoId))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(YHFramework.SysModel.BoxNumberModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BoxNumber set ");
            strSql.Append("BoxNumber=@BoxNumber,ShortBoxNumber=@ShortBoxNumber,IndexCode=@IndexCode,BatchNumber=@BatchNumber,ContentStatusId=@ContentStatusId,IsBind=@IsBind,BindTime=@BindTime,UpdateOn=@UpdateOn,CreateOn=@CreateOn,ApployInfoId=@ApployInfoId ");
            strSql.Append(" where BoxNumberId=@BoxNumberId ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@BoxNumber", DbTool.FixSqlParameter(model.BoxNumber))
,					new SqlParameter("@ShortBoxNumber", DbTool.FixSqlParameter(model.ShortBoxNumber))
,					new SqlParameter("@IndexCode", DbTool.FixSqlParameter(model.IndexCode))
,					new SqlParameter("@BatchNumber", DbTool.FixSqlParameter(model.BatchNumber))
,					new SqlParameter("@ContentStatusId", DbTool.FixSqlParameter(model.ContentStatusId))
,					new SqlParameter("@IsBind", DbTool.FixSqlParameter(model.IsBind))
,					new SqlParameter("@BindTime", DbTool.FixSqlParameter(model.BindTime))
,					new SqlParameter("@UpdateOn", DbTool.FixSqlParameter(model.UpdateOn))
,					new SqlParameter("@CreateOn", DbTool.FixSqlParameter(model.CreateOn))
,					new SqlParameter("@ApployInfoId", DbTool.FixSqlParameter(model.ApployInfoId))
,					new SqlParameter("@BoxNumberId", model.BoxNumberId)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
         #endregion
         
        #region Delete
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from BoxNumber where BoxNumberId = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(YHFramework.SysModel.BoxNumberModel model,SqlDataReader dr)
        {

                model.BoxNumberId = DbTool.ConvertObject<System.Int32>(dr["BoxNumberId"]);
                model.BoxNumber = DbTool.ConvertObject<System.String>(dr["BoxNumber"]);
                model.ShortBoxNumber = DbTool.ConvertObject<System.String>(dr["ShortBoxNumber"]);
                model.IndexCode = DbTool.ConvertObject<System.String>(dr["IndexCode"]);
                model.BatchNumber = DbTool.ConvertObject<System.String>(dr["BatchNumber"]);
                model.ContentStatusId = DbTool.ConvertObject<System.String>(dr["ContentStatusId"]);
                model.IsBind = DbTool.ConvertObject<System.Boolean>(dr["IsBind"]);
                model.BindTime = DbTool.ConvertObject<System.DateTime>(dr["BindTime"]);
                model.UpdateOn = DbTool.ConvertObject<System.DateTime>(dr["UpdateOn"]);
                model.CreateOn = DbTool.ConvertObject<System.DateTime>(dr["CreateOn"]);
                model.ApployInfoId = DbTool.ConvertObject<System.Int32>(dr["ApployInfoId"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected YHFramework.SysModel.BoxNumberModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new YHFramework.SysModel.BoxNumberModel();
                if (DbTool.HasFields("BoxNumberId", fields)) model.BoxNumberId = DbTool.ConvertObject<System.Int32>(dr["BoxNumberId"]);
                if (DbTool.HasFields("BoxNumber", fields)) model.BoxNumber = DbTool.ConvertObject<System.String>(dr["BoxNumber"]);
                if (DbTool.HasFields("ShortBoxNumber", fields)) model.ShortBoxNumber = DbTool.ConvertObject<System.String>(dr["ShortBoxNumber"]);
                if (DbTool.HasFields("IndexCode", fields)) model.IndexCode = DbTool.ConvertObject<System.String>(dr["IndexCode"]);
                if (DbTool.HasFields("BatchNumber", fields)) model.BatchNumber = DbTool.ConvertObject<System.String>(dr["BatchNumber"]);
                if (DbTool.HasFields("ContentStatusId", fields)) model.ContentStatusId = DbTool.ConvertObject<System.String>(dr["ContentStatusId"]);
                if (DbTool.HasFields("IsBind", fields)) model.IsBind = DbTool.ConvertObject<System.Boolean>(dr["IsBind"]);
                if (DbTool.HasFields("BindTime", fields)) model.BindTime = DbTool.ConvertObject<System.DateTime>(dr["BindTime"]);
                if (DbTool.HasFields("UpdateOn", fields)) model.UpdateOn = DbTool.ConvertObject<System.DateTime>(dr["UpdateOn"]);
                if (DbTool.HasFields("CreateOn", fields)) model.CreateOn = DbTool.ConvertObject<System.DateTime>(dr["CreateOn"]);
                if (DbTool.HasFields("ApployInfoId", fields)) model.ApployInfoId = DbTool.ConvertObject<System.Int32>(dr["ApployInfoId"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from BoxNumber where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        /// <summary>
        /// 获取记录数
        /// </summary>
        public int GetRowCount()
        {
            string sql = "select count(*) from BoxNumber ";

            var obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, sql,null); 
            return DbTool.ConvertObject<int>(obj,0);
        }

        public int GetRowCount(string sqlwhere)
        {
            string sql = "select count(*) from BoxNumber where 1=1 " + sqlwhere;

            var obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, sql, null);
            return DbTool.ConvertObject<int>(obj, 0);
        }

        #region GetModel
        public YHFramework.SysModel.BoxNumberModel GetModel(int Id)
        {

            string sql = "select top 1 * from BoxNumber where BoxNumberId =" + Id;
            YHFramework.SysModel.BoxNumberModel model = new YHFramework.SysModel.BoxNumberModel();
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
        public List<YHFramework.SysModel.BoxNumberModel> GetModelList()
        {

            List<YHFramework.SysModel.BoxNumberModel> result = new List<YHFramework.SysModel.BoxNumberModel>();
            string sql = "select * from BoxNumber where 1=1";
            YHFramework.SysModel.BoxNumberModel model = new YHFramework.SysModel.BoxNumberModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new YHFramework.SysModel.BoxNumberModel(); 
                 BindDataReader(model, dr);
                 result.Add(model);
            }
            dr.Close();
            return result;
        }
        #endregion

        #region 分页计算总数
        public int GetCount(string sqlstr, string joinString)
        {
            YHFramework.SysModel.PageInfo pages = new SysModel.PageInfo();
            pages.SqlWhere = sqlstr;
            pages.ReturnFileds = "Id";
            pages.SqlWhere = sqlstr;
            pages.TableName = "BoxNumber";
            pages.JoinTable = "   ";
            pages.CountFields = " a.BoxNumberId ";
            pages.OrderString = " ";
            pages.SelectFileds = "  a.* ";
            pages.doCount = 1;
            PageHelper p = new PageHelper();
            DataTable dt = p.GetList(pages);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region 分页计算GetList
        public DataTable GetList(string sqlstr, int pageindex, int pagesize)
        {
            YHFramework.SysModel.PageInfo pages = new SysModel.PageInfo();
            pages.PageIndex = pageindex;
            pages.PageSize = pagesize;
            pages.SqlWhere = sqlstr;
            pages.ReturnFileds = "t.*";
            pages.TableName = "BoxNumber";
            pages.JoinTable = " ";
            pages.CountFields = " a.BoxNumberId ";
            pages.OrderString = " order by a.BoxNumberId desc";
            pages.SelectFileds = " a.* ";
            pages.doCount = 0;
            PageHelper p = new PageHelper();
            DataTable dt = p.GetList(pages);
            return dt;
        }
        #endregion


        #region 自定义方法
        public YHFramework.SysModel.BoxNumberModel GetModelByBoxNumber(string boxnumber)
        {

            string sql = "select top 1 * from BoxNumber where BoxNumber ='" + boxnumber.FixSql() + "'";
            YHFramework.SysModel.BoxNumberModel model = new YHFramework.SysModel.BoxNumberModel();
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

       
    }
}
