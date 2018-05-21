using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using YHFramework.DB;

namespace YHFramework.DAL
{
    public class CodeDataDal
    {
        public string conn = SqlHelper.ConnectionString;

        
        
        #region Dal Core Functional

        #region Add
        public int Add(YHFramework.SysModel.CodeDataModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into  [CodeData]");
            strSql.Append("(CodeData,ShortCodeData,CodeIndex,BatchNumber,BoxNumberId,ContentStatusId,UpdateOn,Createon,ApployInfoId)");
            strSql.Append(" values (@CodeData,@ShortCodeData,@CodeIndex,@BatchNumber,@BoxNumberId,@ContentStatusId,@UpdateOn,@Createon,@ApployInfoId)");
            strSql.Append(";select SCOPE_IDENTITY()");
            SqlParameter[] parameters = {
					new SqlParameter("@CodeData", DbTool.FixSqlParameter(model.CodeData))
,					new SqlParameter("@ShortCodeData", DbTool.FixSqlParameter(model.ShortCodeData))
,					new SqlParameter("@CodeIndex", DbTool.FixSqlParameter(model.CodeIndex))
,					new SqlParameter("@BatchNumber", DbTool.FixSqlParameter(model.BatchNumber))
,					new SqlParameter("@BoxNumberId", DbTool.FixSqlParameter(model.BoxNumberId))
,					new SqlParameter("@ContentStatusId", DbTool.FixSqlParameter(model.ContentStatusId))
,					new SqlParameter("@UpdateOn", DbTool.FixSqlParameter(model.UpdateOn))
,					new SqlParameter("@Createon", DbTool.FixSqlParameter(model.Createon))
,					new SqlParameter("@ApployInfoId", DbTool.FixSqlParameter(model.ApployInfoId))
                 };


            return DbTool.ConvertObject<int>(SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters),0);
         
        }
         
         
         #endregion
         
        #region Update
        public int Update(YHFramework.SysModel.CodeDataModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CodeData set ");
            strSql.Append("CodeData=@CodeData,ShortCodeData=@ShortCodeData,CodeIndex=@CodeIndex,BatchNumber=@BatchNumber,BoxNumberId=@BoxNumberId,ContentStatusId=@ContentStatusId,UpdateOn=@UpdateOn,Createon=@Createon,ApployInfoId=@ApployInfoId ");
            strSql.Append(" where CodeDataId=@CodeDataId ");
       
            SqlParameter[] parameters = {
					new SqlParameter("@CodeData", DbTool.FixSqlParameter(model.CodeData))
,					new SqlParameter("@ShortCodeData", DbTool.FixSqlParameter(model.ShortCodeData))
,					new SqlParameter("@CodeIndex", DbTool.FixSqlParameter(model.CodeIndex))
,					new SqlParameter("@BatchNumber", DbTool.FixSqlParameter(model.BatchNumber))
,					new SqlParameter("@BoxNumberId", DbTool.FixSqlParameter(model.BoxNumberId))
,					new SqlParameter("@ContentStatusId", DbTool.FixSqlParameter(model.ContentStatusId))
,					new SqlParameter("@UpdateOn", DbTool.FixSqlParameter(model.UpdateOn))
,					new SqlParameter("@Createon", DbTool.FixSqlParameter(model.Createon))
,					new SqlParameter("@ApployInfoId", DbTool.FixSqlParameter(model.ApployInfoId))
,					new SqlParameter("@CodeDataId", model.CodeDataId)
                 };


            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

        }
        /// <summary>
        /// 串码自动分包
        /// </summary>
        /// <param name="count"></param>
        /// <param name="apployInfoId"></param>
        /// <returns></returns>
        public int SendPackage(int count, int apployInfoId)
        {

            // 如下SQL 取出来 ID 最小 最大 以及总数  更新ID id>= min and id <=max  检查数量是否足够
//select MAX(codeid) MaxId ,min(codeid) MinId,MAX(rownum) Rownum from (select top 100 ROW_NUMBER() Over( order by codeid asc )  as rownum ,CodeId from dbo.Project_Code9 ) a   


            StringBuilder sql = new StringBuilder();
 
            //sql.Append(" update top(" + count + ") CodeData ");
            //sql.Append(" set ContentStatusId=1,UpdateOn=GETDATE(),ApployInfoId=" + ContentStatusId);
            //sql.Append(" where ContentStatusId = 2 ");

            //sql.Append("update CodeData set ApployInfoId="+ContentStatusId+", UpdateOn = GETDATE(), ContentStatusId = 1 ");
            //sql.Append("from(select top "+count+" CodeDataId from CodeData where ContentStatusId = 2 order by CodeDataId) as th ");
            //sql.Append("where ContentStatusId = 2 and CodeData.CodeDataId = th.CodeDataId");
 
            sql.Append("update CodeData set ContentStatusId=1,UpdateOn=GETDATE(),ApployInfoId=" + apployInfoId + " from CodeData where ContentStatusId=2 and ");
            sql.Append(" CodeDataId in (");
            sql.Append(" select top " + count + " CodeDataId from dbo.CodeData where ContentStatusId=2 and isnull(ApployInfoId,0)=0 order by CodeDataId)");
 
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
        #endregion

        #region Delete
        public int Del(int id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from CodeData where CodeDataId = " + id);
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        #region BindDataReader
        protected void BindDataReader(YHFramework.SysModel.CodeDataModel model,SqlDataReader dr)
        {

                model.CodeDataId = DbTool.ConvertObject<System.Int32>(dr["CodeDataId"]);
                model.CodeData = DbTool.ConvertObject<System.String>(dr["CodeData"]);
                model.ShortCodeData = DbTool.ConvertObject<System.String>(dr["ShortCodeData"]);
                model.CodeIndex = DbTool.ConvertObject<System.String>(dr["CodeIndex"]);
                model.BatchNumber = DbTool.ConvertObject<System.String>(dr["BatchNumber"]);
                model.BoxNumberId = DbTool.ConvertObject<System.Int32>(dr["BoxNumberId"]);
                model.ContentStatusId = DbTool.ConvertObject<System.Int32>(dr["ContentStatusId"]);
                model.UpdateOn = DbTool.ConvertObject<System.DateTime>(dr["UpdateOn"]);
                model.Createon = DbTool.ConvertObject<System.DateTime>(dr["Createon"]);
                model.ApployInfoId = DbTool.ConvertObject<System.Int32>(dr["ApployInfoId"]);


        }
         #endregion
         
        #region AutoBindDataReader
        protected YHFramework.SysModel.CodeDataModel AutoBindDataReader(SqlDataReader dr, params string[] fields)
        {

           var model = new YHFramework.SysModel.CodeDataModel();
                if (DbTool.HasFields("CodeDataId", fields)) model.CodeDataId = DbTool.ConvertObject<System.Int32>(dr["CodeDataId"]);
                if (DbTool.HasFields("CodeData", fields)) model.CodeData = DbTool.ConvertObject<System.String>(dr["CodeData"]);
                if (DbTool.HasFields("ShortCodeData", fields)) model.ShortCodeData = DbTool.ConvertObject<System.String>(dr["ShortCodeData"]);
                if (DbTool.HasFields("CodeIndex", fields)) model.CodeIndex = DbTool.ConvertObject<System.String>(dr["CodeIndex"]);
                if (DbTool.HasFields("BatchNumber", fields)) model.BatchNumber = DbTool.ConvertObject<System.String>(dr["BatchNumber"]);
                if (DbTool.HasFields("BoxNumberId", fields)) model.BoxNumberId = DbTool.ConvertObject<System.Int32>(dr["BoxNumberId"]);
                if (DbTool.HasFields("ContentStatusId", fields)) model.ContentStatusId = DbTool.ConvertObject<System.Int32>(dr["ContentStatusId"]);
                if (DbTool.HasFields("UpdateOn", fields)) model.UpdateOn = DbTool.ConvertObject<System.DateTime>(dr["UpdateOn"]);
                if (DbTool.HasFields("Createon", fields)) model.Createon = DbTool.ConvertObject<System.DateTime>(dr["Createon"]);
                if (DbTool.HasFields("ApployInfoId", fields)) model.ApployInfoId = DbTool.ConvertObject<System.Int32>(dr["ApployInfoId"]);

           return model;

        }
         #endregion
         
        #endregion 

        #region GetList
        public DataTable GetList(string sqlwhere)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from CodeData where 1=1 ");
            sql.Append(sqlwhere);
            return SqlHelper.ExecuteDataTable(conn, CommandType.Text, sql.ToString());
        }
         #endregion
         
        /// <summary>
        /// 获取记录数
        /// </summary>
        public int GetRowCount(string sqlwhere)
        {
            string sql = "select count(*) from CodeData (nolock) where 1=1 " + sqlwhere;

            var obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, sql,null); 
            return DbTool.ConvertObject<int>(obj,0);
        }

        #region GetModel
        public YHFramework.SysModel.CodeDataModel GetModel(int Id)
        {

            string sql = "select top 1 * from CodeData where CodeDataId =" + Id;
            YHFramework.SysModel.CodeDataModel model = new YHFramework.SysModel.CodeDataModel();
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
        public List<YHFramework.SysModel.CodeDataModel> GetModelList()
        {

            List<YHFramework.SysModel.CodeDataModel> result = new List<YHFramework.SysModel.CodeDataModel>();
            string sql = "select * from CodeData where 1=1";
            YHFramework.SysModel.CodeDataModel model = new YHFramework.SysModel.CodeDataModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                 //model = AutoBindDataReader(dr, fields);
                 model = new YHFramework.SysModel.CodeDataModel(); 
                 BindDataReader(model, dr);
                 result.Add(model);
            }
            dr.Close();
            return result;
        }
         #endregion


        /// <summary>
        /// 分页计算总数
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <param name="joinString"></param>
        /// <returns></returns>
        public int GetCount(string sqlstr, string joinstr)
        {
            YHFramework.SysModel.PageInfo pages = new YHFramework.SysModel.PageInfo();
            pages.SqlWhere = sqlstr;
            pages.ReturnFileds = " * ";
            pages.TableName = " CodeData ";
            pages.JoinTable = joinstr;
            pages.CountFields = " a.CodeDataId ";
            pages.OrderString = " ";
            pages.SelectFileds = "";
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
         

        public DataTable GetPageList(string selectFileds, string joinstr, string sqlstr, int pageindex, int pagesize)
        {
            YHFramework.SysModel.PageInfo pages = new YHFramework.SysModel.PageInfo();
            pages.PageIndex = pageindex;
            pages.PageSize = pagesize;
            pages.SqlWhere = sqlstr;
            pages.ReturnFileds = "t.*";
            pages.TableName = " CodeData ";
            pages.JoinTable = joinstr;
            pages.CountFields = " a.CodeDataId ";
            pages.OrderString = " order by a.CodeDataId ";
            pages.SelectFileds = selectFileds;
            pages.doCount = 0;
            PageHelper p = new PageHelper(); 
            DataTable dt = p.GetList(pages);
            return dt;
        }



        #region 串码数据回传处理

        /// <summary>
        /// 获取记录数
        /// </summary>
        public int GetRowCountByCodelist(List<string> codelist)
        {
            List<YHFramework.SysModel.CodeDataModel> result = new List<YHFramework.SysModel.CodeDataModel>();
            string sql = "select count(*) from CodeData where isnull(BoxNumberId,0)=0 and CodeData in ({0})";
            StringBuilder codebuilder = new StringBuilder();
            foreach (var item in codelist)
            {
                codebuilder.Append("'" + item.FixSql() + "',");
            }
            sql = string.Format(sql, codebuilder.ToString().TrimEnd(','));

            var obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, sql, null);
            return DbTool.ConvertObject<int>(obj, 0);
        }


        public List<YHFramework.SysModel.CodeDataModel> GetModelListByCodelist(List<string> codelist)
        {
            List<YHFramework.SysModel.CodeDataModel> result = new List<YHFramework.SysModel.CodeDataModel>();
            string sql = "select * from CodeData where CodeData in ({0})";
            StringBuilder codebuilder = new StringBuilder();
            foreach (var item in codelist)
            {
                codebuilder.Append("'" + item.FixSql() + "',");
            }
            sql = string.Format(sql, codebuilder.ToString().TrimEnd(','));

            YHFramework.SysModel.CodeDataModel model = new YHFramework.SysModel.CodeDataModel();
            SqlDataReader dr = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString());
            //var fields = DbTool.GetReaderFieldNames(dr);
            while (dr.Read())
            {
                //model = AutoBindDataReader(dr, fields);
                model = new YHFramework.SysModel.CodeDataModel();
                BindDataReader(model, dr);
                result.Add(model);
            }
            dr.Close();
            return result;
        }


        public DB.ReturnValue BindBoxData(int boxNumberId, List<string> codelist,bool appendmode=false)
        {

            DB.ReturnValue _result = new ReturnValue();

            #region 准备执行计划
            List<SQLInfo> SQLInfoList = new List<SQLInfo>();
            SQLInfoList.Add(new SQLInfo()
            {
                cmdType = CommandType.Text,
                parameters = null,
                SQLStr = appendmode ? string.Format("update dbo.BoxNumber set IsBind=1,BindTime=GETDATE() where BoxNumberId={0}", boxNumberId) : string.Format("update dbo.BoxNumber set IsBind=1,BindTime=GETDATE() where BoxNumberId={0} and (IsBind=0 or IsBind is null)", boxNumberId)
            });

            foreach (var item in codelist)
            {
                SQLInfoList.Add(new SQLInfo()
                {
                    cmdType = CommandType.Text,
                    parameters = null,
                    SQLStr = string.Format("update dbo.CodeData set BoxNumberId={0},UpdateOn=GETDATE() where (BoxNumberId is null or BoxNumberId=0 ) and CodeData='{1}' ", boxNumberId, item.FixSql())
                    //SQLStr = string.Format("update dbo.CodeData set BoxNumberId={0},UpdateOn=GETDATE() where CodeData='{1}' ", boxNumberId, item.FixSql()) 
                });
            }

            #endregion


            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionString))
            {
                conn.Open();
                SqlTransaction tx = conn.BeginTransaction();

                try
                {
                    bool haserr = false;
                    string detailerror = "";
                    foreach (SQLInfo sqlinfo in SQLInfoList)
                    {
                        int rows = DB.SqlHelper.ExecuteNonQuery(tx, sqlinfo.cmdType, sqlinfo.SQLStr, sqlinfo.parameters);
                        if (rows == 0)
                        {
                            haserr = true;
                            detailerror = "当前SQL执行失败：" + sqlinfo.SQLStr;
                            break;
                        }

                    }
                    if (haserr)
                    {
                        tx.Rollback();
                        _result.Success = false;
                        _result.ErrMessage = "执行失败，部分数据没有正常更新";
                        LogTool.LogCommon.WriteLog(detailerror);
                    }
                    else
                    {
                        tx.Commit();
                        _result.Success = true;
                        _result.ErrMessage = "";
                    }
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    _result.Success = false;
                    _result.ErrMessage = "执行异常";
                    LogTool.LogCommon.WriteLog(e, "BindBoxData");
                    //throw new Exception(E.Message);
                }
            }


            return _result;

        }


        public int CancelCode(string codedata)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update CodeData set Contentstatusid=3,UpdateOn=getdate() from CodeData where CodeData = '" + codedata.FixSql() + "'");
            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }


        public int CancelCode(List<string> codelist)
        {
            string sql = "update CodeData set Contentstatusid=3,UpdateOn=getdate() from CodeData where CodeData in ({0})";
            StringBuilder codebuilder = new StringBuilder();
            foreach (var item in codelist)
            {
                codebuilder.Append("'" + item.FixSql() + "',");
            }
            sql = string.Format(sql, codebuilder.ToString().TrimEnd(','));

            return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString());
        }


        #endregion

    }
}
