using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class boxList : System.Web.UI.Page
{
    YHFramework.DAL.BoxNumberDal bdal = new YHFramework.DAL.BoxNumberDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            bd();
        }
    }

    public void bd() {
        string sql = " and IsBind=1 ";
        AspNetPager1.PageSize = 15;
        int count = bdal.GetCount(sql,"");
        AspNetPager1.RecordCount = count;
        int page = AspNetPager1.CurrentPageIndex;
        DataTable dd = bdal.GetList(sql, page, AspNetPager1.PageSize); ;
        this.boxLists.DataSource = dd;
        this.boxLists.DataBind();

    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        bd();
    }
}