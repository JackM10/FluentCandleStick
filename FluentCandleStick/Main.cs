using FluentCandleStick.Services;

namespace fluenttechFinancial
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ToDo: DI or not DI?
            var service = new MarketLineService();
            var rawData = service.GetMarketLines().ToList();
            var processedData = service.ProcessMarketLines(rawData);
            dataGridView1.DataSource = processedData;
            PaintRowsWithApropriateColors();
        }

        private void PaintRowsWithApropriateColors()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Cells[3].Style.BackColor = Color.Red;
                row.Cells[4].Style.BackColor = Color.Green;
            }
        }
    }
}