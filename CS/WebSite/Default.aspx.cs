using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

using DevExpress.Web.ASPxGridView;
using DevExpress.XtraEditors.DXErrorProvider;

public partial class _Default : System.Web.UI.Page {

    protected void Page_Init(object sender, EventArgs e) {
        Grid.DataSource = CreateData();
        Grid.DataBind();
    }

    IList CreateData() {
        List<MyBusinessObject> list = new List<MyBusinessObject>();
        list.Add(new MyBusinessObject("Alex", DateTime.Now.AddDays(-20)));
        list.Add(new MyBusinessObject("A", DateTime.Now.AddDays(-40)));
        list.Add(new MyBusinessObject("Kate", DateTime.Now.AddMonths(5)));
        return list;
    }

    protected void Grid_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e) {
        PerformCellValidation(e);
    }

    void PerformCellValidation(ASPxGridViewTableDataCellEventArgs args) {
        IDXDataErrorInfo infoSupport = Grid.GetRow(args.VisibleIndex) as IDXDataErrorInfo;
        if(infoSupport == null) return;
        ErrorInfo info = new ErrorInfo();
        infoSupport.GetPropertyError(args.DataColumn.FieldName, info);
        if(info.ErrorType != ErrorType.None) {
            args.Cell.BackColor = GetErrorColor(info.ErrorType);
            args.Cell.ToolTip = info.ErrorText;
        }
    }

    Color GetErrorColor(ErrorType type) {
        switch(type) {
            case ErrorType.Critical:
                return Color.Red;
            case ErrorType.Warning:
                return Color.Yellow;
        }
        return Color.Empty;
    }
}
