using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using DevExpress.XtraScheduler.VCalendar;
using System.IO;


namespace VCalendarCustomFields {
    public partial class Form1 : Form {
        const string CustomFieldName = "MyCustomField";
        public string OutFileName { get { return "..\\..\\appointments.vcs"; } }

        public Form1() {
            InitializeComponent();

            schedulerControl1.Start = DateTime.Today;
            schedulerControl1.DayView.DayCount = 5;

            AppointmentCustomFieldMapping mapping = new AppointmentCustomFieldMapping(CustomFieldName, CustomFieldName);
            schedulerStorage1.Appointments.CustomFieldMappings.Add(mapping);

            GenerateAppointments();
        }

        void GenerateAppointments() {
            DateTime now = DateTime.Now.Date;
            Random rand = new Random();

            schedulerStorage1.BeginUpdate();
            DateTime currentDate;
            for (int n = 0; n < 5; n++) {
                currentDate = now.AddDays(n);

                for (int i = 0; i < 5; i++) {
                    DateTime start = currentDate.AddHours(rand.Next(24));
                    Appointment apt = schedulerStorage1.CreateAppointment(AppointmentType.Normal);
                    apt.Start = start;
                    apt.Duration = TimeSpan.FromHours(4);
                    apt.Subject = String.Format("Appointment {0}{1}", n, i);


                    apt.CustomFields[CustomFieldName] = CreateCustomObject(CustomFieldName, rand.Next(2));
                    schedulerStorage1.Appointments.Add(apt);
                }
            }
            schedulerStorage1.EndUpdate();
        }
        string[] objectTypes = new string[2] { "WAV", "BMP" };
        string[] valueTypes = new string[2] { "URL", "FILE" };
        string[] objectValues = new string[2] { "http://fakeserver.com/sample.wav", "c:\\my_pic.bmp" };

        CustomObject CreateCustomObject(string name, int index) {
            CustomObject obj = new CustomObject();
            obj.Name = name;
            obj.ObjectType = objectTypes[index];
            obj.ValueType = valueTypes[index];
            obj.Value = objectValues[index];
            return obj;
        }

        private void schedulerControl1_InitAppointmentDisplayText(object sender, AppointmentDisplayTextEventArgs e) {
            CustomObject obj = e.Appointment.CustomFields[CustomFieldName] as CustomObject;
            e.Description = (obj != null) ? obj.ToString() : "(no custom info)";
        }

        private void btnExport_Click(object sender, System.EventArgs e) {
            DoVCalendarExport();
        }

        void DoVCalendarExport() {
            DateTime now = DateTime.Now.Date;

            // !!! only first 3 days
            IList appointments = schedulerStorage1.GetAppointments(new TimeInterval(now, now.AddDays(3)));

            VCalendarExporter exporter = new VCalendarExporter(schedulerStorage1, appointments);
            exporter.AppointmentExporting += new AppointmentExportingEventHandler(OnExportAppointment);
            using (FileStream fs = new FileStream(OutFileName, FileMode.Create)) {
                exporter.Export(fs);
            }
        }

        void OnExportAppointment(object sender, AppointmentExportingEventArgs e) {
            VCalendarAppointmentExportingEventArgs args = (VCalendarAppointmentExportingEventArgs)e;

            Appointment apt = args.Appointment;
            VEvent ev = args.VEvent;

            // !!! only work time
            if (apt.Start.Hour < 8 || apt.Start.Hour > 17) {
                args.Cancel = true;
                return;
            }

            // save the custom information 
            CustomObject obj = (CustomObject)apt.CustomFields[CustomFieldName];
            VEventExtension ext = new VEventExtension(obj.Name, obj.Value);
            ext.Parameters.Add(new VCalendarParameter("TYPE", obj.ObjectType));
            ext.Parameters.Add(new VCalendarParameter("VALUE", obj.ValueType));
            ev.Extensions.Add(ext);
        }

        private void btnImport_Click(object sender, System.EventArgs e) {
            DoVCalendarImport();
        }

        void DoVCalendarImport() {
            VCalendarImporter importer = new VCalendarImporter(schedulerStorage1);
            importer.AppointmentImporting += new AppointmentImportingEventHandler(OnImportAppointment);
            if (File.Exists(OutFileName)) {
                using (FileStream fs = new FileStream(OutFileName, FileMode.Open)) {
                    try {
                        importer.Import(fs);
                    }
                    catch (System.Exception ex) {
                        MessageBox.Show(ex.Message);
                        throw;
                    }
                    finally {
                        fs.Close();
                    }
                }
            }
            else MessageBox.Show("Cannot find the " + OutFileName+ " file", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        void OnImportAppointment(object sender, AppointmentImportingEventArgs e) {
            VCalendarAppointmentImportingEventArgs args = (VCalendarAppointmentImportingEventArgs)e;
            Appointment apt = args.Appointment;
            VEvent ev = args.VEvent;
            VEventExtension ext = ev.Extensions[CustomFieldName];

            // restore the custom information 
            if (ext != null) {
                CustomObject obj = new CustomObject();
                obj.Name = ext.Name;
                obj.ObjectType = ext.Parameters["TYPE"].Value;
                obj.ValueType = ext.Parameters["VALUE"].Value;
                obj.Value = ext.Value;

                apt.CustomFields[CustomFieldName] = obj;
            }
        }

        private void btnClear_Click(object sender, System.EventArgs e) {
            schedulerStorage1.BeginUpdate();
            schedulerStorage1.Appointments.Clear();
            schedulerStorage1.EndUpdate();
        }
        
    }
}