# How to export appointments with custom fields to vCalendar format


<p><strong>Problem</strong>:<br />
 My scheduling application contains appointments with custom fields, and I need to export them in the vCalendar format for a particular time interval only. I've tried to use the <strong>SchedulerStorage.ExportToVCalendar</strong> method, but have no success. How can this be done?</p><p><strong>Solution</strong>:<br />
 To export and import scheduler's appointments to vCalendar format with custom settings you should use the <strong>VCalendarExporter</strong> and <strong>VCalendarImporter</strong> objects (they belong to the "DevExpress.XtraScheduler.VCalendar" namespace).</p><p>The following example demonstrates how to export appointments with custom fields to the vCalendar format. Also, the exporting time period is limited by <strong>3 days</strong>  and <strong>working time</strong> only.</p>

<br/>


