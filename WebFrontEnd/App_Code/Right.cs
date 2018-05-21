using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using YHFramework.DAL;

/// <summary>
/// Right 的摘要说明
/// </summary>
public class Right
{
    public Right()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public static bool HasRole(int roleId, int userId)
    {
        bool has = false;
        string condition = "UserID=" + userId + " AND RoleID=" + roleId;
        int count = TableDAL.GetTableCount("UserRole", condition);
        if (0 < count)
        {
            has = true;
        }
        return has;
    }

    public static bool IsAdmmin()
    {
        return HasRole(1, UserId);
    }

    public static int UserId
    {
        get
        {
            int userId = 0;
            try
            {
                userId = Convert.ToInt32(System.Web.HttpContext.Current.Session["userId"]);
            }
            catch { }
            return userId;
        }
    }
}