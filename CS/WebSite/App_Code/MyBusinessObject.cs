using DevExpress.XtraEditors.DXErrorProvider;
using System;

public class MyBusinessObject : Object, IDXDataErrorInfo {
    string m_name;
    DateTime? m_date;

    public MyBusinessObject(string name, DateTime? date) {
        m_name = name;
        m_date = date;
    }

    public string Name { get { return m_name; } }
    public DateTime? Date { get { return m_date; } }


    void IDXDataErrorInfo.GetError(ErrorInfo info) {
        // stub
    }

    void IDXDataErrorInfo.GetPropertyError(string propertyName, ErrorInfo info) {
        switch(propertyName) {
            case "Name":
                if(Name == null || Name.Length < 2) {
                    info.ErrorType = ErrorType.Critical;
                    info.ErrorText = "Name is too short";
                }
                return;
            case "Date":
                if(Date > DateTime.Now) {
                    info.ErrorType = ErrorType.Warning;
                    info.ErrorText = "Possibly incorrect date";
                }
                return;
        }
    }

}