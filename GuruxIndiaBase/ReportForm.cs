using System.Data;

namespace Gurux_Testing
{
    public class ReportForm
    {
        string[] AllLines = null;
        DataSet ds1 = null;
        Decode decode = new Decode();
        string profile = "";
        public ReportForm(string aa)
        {
            InitializeComponent();
            profile = aa;

        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            try
            {
                ds1 = decode.InsertData();
                AllLines = File.ReadAllLines(Program.Default_Target_Directory + "\\" + Program.FileName);
                ShowReport(profile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowReport(string data)
        {
            tabControl1.TabPages.Remove(tabPage2);
            tabControl1.Visible = true;
            int index = Array.FindIndex(AllLines, r => r.Contains(data + "|"));
            if (index != -1)
            {
                string[] arr = AllLines[index].Split('|');
                //SelectTabPage(arr[0]);
                //VisibleInvisibleRB(false);
                //ds1.Tables["TAMPER"].Rows.Clear();
                switch (arr[0])
                {
                    case "Nameplate":
                        Reports.Nameplate crptNP = new Reports.Nameplate();
                        crptNP.SetDataSource(ds1);
                        crystalReportViewer1.Visible = true;
                        crystalReportViewer1.Dock = DockStyle.Fill;
                        crystalReportViewer1.ReportSource = crptNP;
                        crystalReportViewer1.Zoom(90);
                        break;
                    case "DailyLoadProfile":
                        Reports.DailyLoad NP = new Reports.DailyLoad();
                        NP.SetDataSource(ds1);
                        crystalReportViewer1.Visible = true;
                        crystalReportViewer1.Dock = DockStyle.Fill;
                        crystalReportViewer1.ReportSource = NP;
                        crystalReportViewer1.Zoom(90);
                        break;
                    case "Instant":
                        Reports.Instant1Ph crptIn = new Reports.Instant1Ph();
                        crptIn.SetDataSource(ds1);
                        crystalReportViewer1.Visible = true;
                        crystalReportViewer1.Dock = DockStyle.Fill;
                        crystalReportViewer1.ReportSource = crptIn;
                        crystalReportViewer1.Zoom(90);
                        break;
                    case "Billing":
                        tabControl1.TabPages.Add(tabPage2);
                        Reports.Billing1Ph crptBl = new Reports.Billing1Ph();
                        crptBl.SetDataSource(ds1);
                        crystalReportViewer1.Visible = true;
                        crystalReportViewer1.Dock = DockStyle.Fill;
                        crystalReportViewer1.ReportSource = crptBl;
                        crystalReportViewer1.Zoom(90);
                        Reports.TariffReport1PH tcrpt = new Reports.TariffReport1PH();
                        tcrpt.SetDataSource(ds1);
                        crystalReportViewer2.Visible = true;
                        crystalReportViewer2.Dock = DockStyle.Fill;
                        crystalReportViewer2.ReportSource = tcrpt;
                        crystalReportViewer2.Zoom(90);
                        break;
                    case "BlockLoadProfile":
                        Reports.Block crptBlo = new Reports.Block();
                        crptBlo.SetDataSource(ds1);
                        crystalReportViewer1.Visible = true;
                        crystalReportViewer1.Dock = DockStyle.Fill;
                        crystalReportViewer1.ReportSource = crptBlo;
                        crystalReportViewer1.Zoom(90);
                        break;
                    case "CurrentEvent":
                    case "OtherEvent":
                    case "VoltageEvent":
                        Reports.EventReport1PH crptCU = new Reports.EventReport1PH();
                        crptCU.SetDataSource(ds1);
                        crystalReportViewer1.Visible = true;
                        crystalReportViewer1.Dock = DockStyle.Fill;
                        crystalReportViewer1.ReportSource = crptCU;
                        crystalReportViewer1.Zoom(90);
                        break;
                    case "PowerFailEvent":
                    case "TransactionEvent":
                    case "CoverOpenEvent":
                        Reports.PowerFail crptPfail = new Reports.PowerFail();
                        crptPfail.SetDataSource(ds1);
                        crystalReportViewer1.Visible = true;
                        crystalReportViewer1.Dock = DockStyle.Fill;
                        crystalReportViewer1.ReportSource = crptPfail;
                        crystalReportViewer1.Zoom(90);
                        break;
                }
            }
        }
    }
}
