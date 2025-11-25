using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting; // <-- IMPORTANTE

namespace ese_prezzo_di_equilibrio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            dgvRisultati.Columns.Clear();
            dgvRisultati.Columns.Add("q", "Quantità (q)");
            dgvRisultati.Columns.Add("domanda", "Prezzo Domanda");
            dgvRisultati.Columns.Add("offerta", "Prezzo Offerta");
        }

        private void btnCalcola_Click(object sender, EventArgs e)
        {
            CalcolaEquilibrio();
        }

        private void CalcolaEquilibrio()
        {
            dgvRisultati.Rows.Clear();

            
            if (!int.TryParse(txtQMin.Text, out int qMin) ||
                !int.TryParse(txtQMax.Text, out int qMax) ||
                !int.TryParse(txtPasso.Text, out int passo))
            {
                MessageBox.Show("Inserisci solo numeri interi!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            if (!double.TryParse(txtDomandaA.Text, out double aD) ||
                !double.TryParse(txtDomandaB.Text, out double bD))
            {
                MessageBox.Show("Coefficienti della domanda non validi!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            if (!double.TryParse(txtOffertaA.Text, out double aS) ||
                !double.TryParse(txtOffertaB.Text, out double bS) ||
                !double.TryParse(txtOffertaC.Text, out double cS))
            {
                MessageBox.Show("Coefficienti dell'offerta non validi!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double equilibrioQ = -1;
            double equilibrioP = -1;

            for (int q = qMin; q <= qMax; q += passo)
            {
                
                double pDom = aD - (bD * q);

                
                double pOff = aS + bS * Math.Pow(q, cS);

                dgvRisultati.Rows.Add(q, pDom, pOff);

                if (Math.Abs(pDom - pOff) < 0.0001)
                {
                    equilibrioQ = q;
                    equilibrioP = pDom;
                }
            }

            if (equilibrioQ >= 0)
            {
                lblRisultato.Text = $"Prezzo di equilibrio: P = {equilibrioP:F2}, Quantità = {equilibrioQ}";
            }
            else
            {
                lblRisultato.Text = "Nessun equilibrio trovato nel range indicato.";
            }

            
            DisegnaGrafico(qMin, qMax, passo, aD, bD, aS, bS, cS, equilibrioQ, equilibrioP);
        }

        private void DisegnaGrafico(int qMin, int qMax, int passo,
                                    double aD, double bD,
                                    double aS, double bS, double cS,
                                    double qEquilibrio, double pEquilibrio)
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            ChartArea area = new ChartArea("area");
            area.AxisX.Title = "Quantità (Q)";
            area.AxisY.Title = "Prezzo (P)";
            chart1.ChartAreas.Add(area);

           
            Series domanda = new Series("Domanda");
            domanda.ChartType = SeriesChartType.Line;
            domanda.BorderWidth = 3;
            domanda.Color = System.Drawing.Color.Blue;

            
            Series offerta = new Series("Offerta");
            offerta.ChartType = SeriesChartType.Line;
            offerta.BorderWidth = 3;
            offerta.Color = System.Drawing.Color.Red;

            for (int q = qMin; q <= qMax; q += passo)
            {
                double pDom = aD - (bD * q);
                double pOff = aS + bS * Math.Pow(q, cS);

                domanda.Points.AddXY(q, pDom);
                offerta.Points.AddXY(q, pOff);
            }

            chart1.Series.Add(domanda);
            chart1.Series.Add(offerta);

            
            if (qEquilibrio >= 0)
            {
                Series eq = new Series("Equilibrio");
                eq.ChartType = SeriesChartType.Point;
                eq.Color = System.Drawing.Color.Green;
                eq.MarkerSize = 10;

                eq.Points.AddXY(qEquilibrio, pEquilibrio);

                chart1.Series.Add(eq);
            }
        }
    }
}
