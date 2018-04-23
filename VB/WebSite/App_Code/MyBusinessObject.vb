Imports Microsoft.VisualBasic
Imports DevExpress.XtraEditors.DXErrorProvider
Imports System

Public Class MyBusinessObject
	Inherits Object
	Implements IDXDataErrorInfo
	Private m_name As String
	Private m_date As Nullable(Of DateTime)

	Public Sub New(ByVal name As String, ByVal [date] As Nullable(Of DateTime))
		m_name = name
		m_date = [date]
	End Sub

	Public ReadOnly Property Name() As String
		Get
			Return m_name
		End Get
	End Property
	Public ReadOnly Property [Date]() As Nullable(Of DateTime)
		Get
			Return m_date
		End Get
	End Property


	Private Sub GetError(ByVal info As ErrorInfo) Implements IDXDataErrorInfo.GetError
		' stub
	End Sub

	Private Sub GetPropertyError(ByVal propertyName As String, ByVal info As ErrorInfo) Implements IDXDataErrorInfo.GetPropertyError
		Select Case propertyName
			Case "Name"
				If Name Is Nothing OrElse Name.Length < 2 Then
					info.ErrorType = ErrorType.Critical
					info.ErrorText = "Name is too short"
				End If
				Return
			Case "Date"
				If [Date] > DateTime.Now Then
					info.ErrorType = ErrorType.Warning
					info.ErrorText = "Possibly incorrect date"
				End If
				Return
		End Select
	End Sub

End Class