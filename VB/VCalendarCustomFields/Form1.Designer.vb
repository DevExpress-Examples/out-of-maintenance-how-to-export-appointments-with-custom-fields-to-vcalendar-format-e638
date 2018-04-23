Imports Microsoft.VisualBasic
Imports System
Namespace VCalendarCustomFields
	Partial Public Class Form1
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container()
			Dim timeRuler1 As New DevExpress.XtraScheduler.TimeRuler()
			Dim timeRuler2 As New DevExpress.XtraScheduler.TimeRuler()
			Me.schedulerControl1 = New DevExpress.XtraScheduler.SchedulerControl()
			Me.schedulerStorage1 = New DevExpress.XtraScheduler.SchedulerStorage(Me.components)
			Me.panelControl1 = New DevExpress.XtraEditors.PanelControl()
			Me.btnExport = New System.Windows.Forms.Button()
			Me.btnClear = New System.Windows.Forms.Button()
			Me.btnImport = New System.Windows.Forms.Button()
			CType(Me.schedulerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.schedulerStorage1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.panelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.panelControl1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' schedulerControl1
			' 
			Me.schedulerControl1.Dock = System.Windows.Forms.DockStyle.Fill
			Me.schedulerControl1.Location = New System.Drawing.Point(0, 0)
			Me.schedulerControl1.Name = "schedulerControl1"
			Me.schedulerControl1.Size = New System.Drawing.Size(589, 321)
			Me.schedulerControl1.Start = New System.DateTime(2008, 10, 29, 0, 0, 0, 0)
			Me.schedulerControl1.Storage = Me.schedulerStorage1
			Me.schedulerControl1.TabIndex = 0
			Me.schedulerControl1.Text = "schedulerControl1"
			Me.schedulerControl1.Views.DayView.TimeRulers.Add(timeRuler1)
			Me.schedulerControl1.Views.WorkWeekView.TimeRulers.Add(timeRuler2)
'			Me.schedulerControl1.InitAppointmentDisplayText += New DevExpress.XtraScheduler.AppointmentDisplayTextEventHandler(Me.schedulerControl1_InitAppointmentDisplayText);
			' 
			' panelControl1
			' 
			Me.panelControl1.Controls.Add(Me.btnImport)
			Me.panelControl1.Controls.Add(Me.btnClear)
			Me.panelControl1.Controls.Add(Me.btnExport)
			Me.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom
			Me.panelControl1.Location = New System.Drawing.Point(0, 321)
			Me.panelControl1.Name = "panelControl1"
			Me.panelControl1.Size = New System.Drawing.Size(589, 45)
			Me.panelControl1.TabIndex = 1
			' 
			' btnExport
			' 
			Me.btnExport.Location = New System.Drawing.Point(7, 10)
			Me.btnExport.Name = "btnExport"
			Me.btnExport.Size = New System.Drawing.Size(75, 23)
			Me.btnExport.TabIndex = 0
			Me.btnExport.Text = "Export"
			Me.btnExport.UseVisualStyleBackColor = True
'			Me.btnExport.Click += New System.EventHandler(Me.btnExport_Click);
			' 
			' btnClear
			' 
			Me.btnClear.Location = New System.Drawing.Point(115, 10)
			Me.btnClear.Name = "btnClear"
			Me.btnClear.Size = New System.Drawing.Size(75, 23)
			Me.btnClear.TabIndex = 1
			Me.btnClear.Text = "Clear"
			Me.btnClear.UseVisualStyleBackColor = True
'			Me.btnClear.Click += New System.EventHandler(Me.btnClear_Click);
			' 
			' btnImport
			' 
			Me.btnImport.Location = New System.Drawing.Point(226, 10)
			Me.btnImport.Name = "btnImport"
			Me.btnImport.Size = New System.Drawing.Size(75, 23)
			Me.btnImport.TabIndex = 2
			Me.btnImport.Text = "Import"
			Me.btnImport.UseVisualStyleBackColor = True
'			Me.btnImport.Click += New System.EventHandler(Me.btnImport_Click);
			' 
			' Form1
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(589, 366)
			Me.Controls.Add(Me.schedulerControl1)
			Me.Controls.Add(Me.panelControl1)
			Me.Name = "Form1"
			Me.Text = "Form1"
			CType(Me.schedulerControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.schedulerStorage1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.panelControl1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.panelControl1.ResumeLayout(False)
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private WithEvents schedulerControl1 As DevExpress.XtraScheduler.SchedulerControl
		Private schedulerStorage1 As DevExpress.XtraScheduler.SchedulerStorage
		Private panelControl1 As DevExpress.XtraEditors.PanelControl
		Private WithEvents btnImport As System.Windows.Forms.Button
		Private WithEvents btnClear As System.Windows.Forms.Button
		Private WithEvents btnExport As System.Windows.Forms.Button
	End Class
End Namespace

