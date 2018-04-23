Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace VCalendarCustomFields
	Public Class CustomObject
		Private name_Renamed As String
		Private objType As String
		Private valType As String
		Private val As String

		Public Sub New()
		End Sub
		Public Property Name() As String
			Get
				Return name_Renamed
			End Get
			Set(ByVal value As String)
				name_Renamed = value
			End Set
		End Property
		Public Property ObjectType() As String
			Get
				Return objType
			End Get
			Set(ByVal value As String)
				objType = value
			End Set
		End Property
		Public Property ValueType() As String
			Get
				Return valType
			End Get
			Set(ByVal value As String)
				valType = value
			End Set
		End Property
		Public Property Value() As String
			Get
				Return val
			End Get
			Set(ByVal value As String)
				val = value
			End Set
		End Property

		Public Overrides Function ToString() As String
			Return String.Format("NAME={0} TYPE={1} VALUE={2}: {3}", Name, ObjectType, ValueType, Value)
		End Function
	End Class
End Namespace
