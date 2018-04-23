Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraScheduler.VCalendar
Imports System.IO


Namespace VCalendarCustomFields
	Partial Public Class Form1
		Inherits Form
		Private Const CustomFieldName As String = "MyCustomField"
		Public ReadOnly Property OutFileName() As String
			Get
				Return "..\..\appointments.vcs"
			End Get
		End Property

		Public Sub New()
			InitializeComponent()

			schedulerControl1.Start = DateTime.Today
			schedulerControl1.DayView.DayCount = 5

			Dim mapping As New AppointmentCustomFieldMapping(CustomFieldName, CustomFieldName)
			schedulerStorage1.Appointments.CustomFieldMappings.Add(mapping)

			GenerateAppointments()
		End Sub

		Private Sub GenerateAppointments()
			Dim now As DateTime = DateTime.Now.Date
			Dim rand As New Random()

			schedulerStorage1.BeginUpdate()
			Dim currentDate As DateTime
			For n As Integer = 0 To 4
				currentDate = now.AddDays(n)

				For i As Integer = 0 To 4
					Dim start As DateTime = currentDate.AddHours(rand.Next(24))
					Dim apt As Appointment = schedulerStorage1.CreateAppointment(AppointmentType.Normal)
					apt.Start = start
					apt.Duration = TimeSpan.FromHours(4)
					apt.Subject = String.Format("Appointment {0}{1}", n, i)


					apt.CustomFields(CustomFieldName) = CreateCustomObject(CustomFieldName, rand.Next(2))
					schedulerStorage1.Appointments.Add(apt)
				Next i
			Next n
			schedulerStorage1.EndUpdate()
		End Sub
		Private objectTypes() As String = { "WAV", "BMP" }
		Private valueTypes() As String = { "URL", "FILE" }
		Private objectValues() As String = { "http://fakeserver.com/sample.wav", "c:\my_pic.bmp" }

		Private Function CreateCustomObject(ByVal name As String, ByVal index As Integer) As CustomObject
			Dim obj As New CustomObject()
			obj.Name = name
			obj.ObjectType = objectTypes(index)
			obj.ValueType = valueTypes(index)
			obj.Value = objectValues(index)
			Return obj
		End Function

		Private Sub schedulerControl1_InitAppointmentDisplayText(ByVal sender As Object, ByVal e As AppointmentDisplayTextEventArgs) Handles schedulerControl1.InitAppointmentDisplayText
			Dim obj As CustomObject = TryCast(e.Appointment.CustomFields(CustomFieldName), CustomObject)
			If (obj IsNot Nothing) Then
				e.Description = obj.ToString()
			Else
				e.Description = "(no custom info)"
			End If
		End Sub

		Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
			DoVCalendarExport()
		End Sub

		Private Sub DoVCalendarExport()
			Dim now As DateTime = DateTime.Now.Date

			' !!! only first 3 days
			Dim appointments As IList = schedulerStorage1.GetAppointments(New TimeInterval(now, now.AddDays(3)))

			Dim exporter As New VCalendarExporter(schedulerStorage1, appointments)
			AddHandler exporter.AppointmentExporting, AddressOf OnExportAppointment
			Using fs As New FileStream(OutFileName, FileMode.Create)
				exporter.Export(fs)
			End Using
		End Sub

		Private Sub OnExportAppointment(ByVal sender As Object, ByVal e As AppointmentExportingEventArgs)
			Dim args As VCalendarAppointmentExportingEventArgs = CType(e, VCalendarAppointmentExportingEventArgs)

			Dim apt As Appointment = args.Appointment
			Dim ev As VEvent = args.VEvent

			' !!! only work time
			If apt.Start.Hour < 8 OrElse apt.Start.Hour > 17 Then
				args.Cancel = True
				Return
			End If

			' save the custom information 
			Dim obj As CustomObject = CType(apt.CustomFields(CustomFieldName), CustomObject)
			Dim ext As New VEventExtension(obj.Name, obj.Value)
			ext.Parameters.Add(New VCalendarParameter("TYPE", obj.ObjectType))
			ext.Parameters.Add(New VCalendarParameter("VALUE", obj.ValueType))
			ev.Extensions.Add(ext)
		End Sub

		Private Sub btnImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImport.Click
			DoVCalendarImport()
		End Sub

		Private Sub DoVCalendarImport()
			Dim importer As New VCalendarImporter(schedulerStorage1)
			AddHandler importer.AppointmentImporting, AddressOf OnImportAppointment
			If File.Exists(OutFileName) Then
				Using fs As New FileStream(OutFileName, FileMode.Open)
					Try
						importer.Import(fs)
					Catch ex As System.Exception
						MessageBox.Show(ex.Message)
						Throw
					Finally
						fs.Close()
					End Try
				End Using
			Else
				MessageBox.Show("Cannot find the " & OutFileName & " file", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
			End If
		End Sub

		Private Sub OnImportAppointment(ByVal sender As Object, ByVal e As AppointmentImportingEventArgs)
			Dim args As VCalendarAppointmentImportingEventArgs = CType(e, VCalendarAppointmentImportingEventArgs)
			Dim apt As Appointment = args.Appointment
			Dim ev As VEvent = args.VEvent
			Dim ext As VEventExtension = ev.Extensions(CustomFieldName)

			' restore the custom information 
			If ext IsNot Nothing Then
				Dim obj As New CustomObject()
				obj.Name = ext.Name
				obj.ObjectType = ext.Parameters("TYPE").Value
				obj.ValueType = ext.Parameters("VALUE").Value
				obj.Value = ext.Value

				apt.CustomFields(CustomFieldName) = obj
			End If
		End Sub

		Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
			schedulerStorage1.BeginUpdate()
			schedulerStorage1.Appointments.Clear()
			schedulerStorage1.EndUpdate()
		End Sub

	End Class
End Namespace