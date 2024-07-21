using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using TicketManagement.Models;

namespace TicketManagement.View.Report
{
    /// <summary>
    /// Interaction logic for ReportWindowView.xaml
    /// </summary>
    public partial class ReportWindowView : Window
    {
        public ReportWindowView()
        {
            InitializeComponent();
        }

        private void WindowsFormsHost_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }
        public async void ReportDetail(List<TicketModelSTT> list, int revenue, string start, string end)
        {
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    try
                    {
                        reportViewer.ProcessingMode = ProcessingMode.Local;
                        reportViewer.LocalReport.ReportEmbeddedResource = "TicketManagement.ReportTheme.Report1.rdlc";
                        reportViewer.LocalReport.DisplayName = "Report";
                        ReportParameter[] rpt = new ReportParameter[]{
                        new ReportParameter("Revenue",revenue.ToString()),
                        new ReportParameter("pStartTime", start),
                        new ReportParameter("pEndTime", end)};
                        reportViewer.LocalReport.SetParameters(rpt);
                        ReportDataSource reportDataSource1 = new ReportDataSource();
                        reportDataSource1.Name = "DataSet1";
                        reportDataSource1.Value = list;
                        reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                        reportViewer.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                });

            });
        }
        public async void ReportQuantityVehicle(List<ReportByVehicle> list, int revenue, string start, string end)
        {
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    try
                    {
                        reportViewer.ProcessingMode = ProcessingMode.Local;
                        reportViewer.LocalReport.ReportEmbeddedResource = "TicketManagement.ReportTheme.Report2.rdlc";
                        reportViewer.LocalReport.DisplayName = "Report";
                        ReportParameter[] rpt = new ReportParameter[]{
                        new ReportParameter("Revenue",revenue.ToString()),
                        new ReportParameter("pStartTime", start),
                        new ReportParameter("pEndTime", end)};
                        reportViewer.LocalReport.SetParameters(rpt);
                        ReportDataSource reportDataSource1 = new ReportDataSource();
                        reportDataSource1.Name = "VehicleQuantityReport";
                        reportDataSource1.Value = list;
                        reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                        reportViewer.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                });

            });
        }
        public async void ReportQuantityLocation(List<Models.ReportByLocation> list, int revenue, string start, string end)
        {
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    try
                    {
                        reportViewer.ProcessingMode = ProcessingMode.Local;
                        reportViewer.LocalReport.ReportEmbeddedResource = "TicketManagement.ReportTheme.Report3.rdlc";
                        reportViewer.LocalReport.DisplayName = "Report";
                        ReportParameter[] rpt = new ReportParameter[]{
                        new ReportParameter("Revenue",revenue.ToString()),
                        new ReportParameter("pStartTime", start),
                        new ReportParameter("pEndTime", end)};
                        reportViewer.LocalReport.SetParameters(rpt);
                        ReportDataSource reportDataSource1 = new ReportDataSource();
                        reportDataSource1.Name = "DataSet1";
                        reportDataSource1.Value = list;
                        reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                        reportViewer.RefreshReport();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                });

            });
        }
    }
}
