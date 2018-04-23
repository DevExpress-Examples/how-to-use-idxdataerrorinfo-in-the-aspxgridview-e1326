Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Drawing

Imports DevExpress.Web.ASPxGridView
Imports DevExpress.XtraEditors.DXErrorProvider

Partial Public Class _Default
	Inherits System.Web.UI.Page

	Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
		Grid.DataSource = CreateData()
		Grid.DataBind()
	End Sub

	Private Function CreateData() As IList
		Dim list As List(Of MyBusinessObject) = New List(Of MyBusinessObject)()
		list.Add(New MyBusinessObject("Alex", DateTime.Now.AddDays(-20)))
		list.Add(New MyBusinessObject("A", DateTime.Now.AddDays(-40)))
		list.Add(New MyBusinessObject("Kate", DateTime.Now.AddMonths(5)))
		Return list
	End Function

	Protected Sub Grid_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As ASPxGridViewTableDataCellEventArgs)
		PerformCellValidation(e)
	End Sub

	Private Sub PerformCellValidation(ByVal args As ASPxGridViewTableDataCellEventArgs)
		Dim infoSupport As IDXDataErrorInfo = TryCast(Grid.GetRow(args.VisibleIndex), IDXDataErrorInfo)
		If infoSupport Is Nothing Then
			Return
		End If
		Dim info As New ErrorInfo()
		infoSupport.GetPropertyError(args.DataColumn.FieldName, info)
		If info.ErrorType <> ErrorType.None Then
			args.Cell.BackColor = GetErrorColor(info.ErrorType)
			args.Cell.ToolTip = info.ErrorText
		End If
	End Sub

	Private Function GetErrorColor(ByVal type As ErrorType) As Color
		Select Case type
			Case ErrorType.Critical
				Return Color.Red
			Case ErrorType.Warning
				Return Color.Yellow
		End Select
		Return Color.Empty
	End Function
End Class
