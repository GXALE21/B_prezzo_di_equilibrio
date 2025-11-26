using System;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ese_prezzo_di_equilibrio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InizializzaComponenti();
        }

        private void InizializzaComponenti()
        {
            
            dgvRisultati.Columns.Clear();
            dgvRisultati.Columns.Add("q", "Quantità (q)");
            dgvRisultati.Columns.Add("domanda", "Prezzo Domanda");
            dgvRisultati.Columns.Add("offerta", "Prezzo Offerta");

            
            ImpostaValoriDefault();
        }

        private void ImpostaValoriDefault()
        {
           
            txtQMin.Text = "0";
            txtPasso.Text = "1";
            txtQMax.Enabled = false; 
            txtQMax.Text = "Auto";  

            
            txtDomandaA.Text = "90";
            txtDomandaB.Text = "4";

            
            txtOffertaA.Text = "10";
            txtOffertaB.Text = "0.01";
            txtOffertaC.Text = "3";  
        }

        private void btnCalcola_Click(object sender, EventArgs e)
        {
            CalcolaEquilibrio();
        }

        private void CalcolaEquilibrio()
        {
            dgvRisultati.Rows.Clear();

            
            if (!int.TryParse(txtQMin.Text, out int qMin) ||
                !int.TryParse(txtPasso.Text, out int passo))
            {
                MessageBox.Show("Inserisci solo numeri interi per Qmin e passo!", "Errore",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            if (qMin < 0 || passo <= 0)
            {
                MessageBox.Show("Qmin >= 0, passo > 0", "Errore",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            if (!double.TryParse(txtDomandaA.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double aD) ||
                !double.TryParse(txtDomandaB.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double bD))
            {
                MessageBox.Show("Coefficienti della domanda non validi!", "Errore",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           
            if (!double.TryParse(txtOffertaA.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double aS) ||
                !double.TryParse(txtOffertaB.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double bS) ||
                !double.TryParse(txtOffertaC.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double cS))
            {
                MessageBox.Show("Coefficienti dell'offerta non validi!", "Errore",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            var equilibrio = TrovaEquilibrioPreciso(aD, bD, aS, bS, cS);

            int qMax;
            if (equilibrio.HasValue)
            {
                
                qMax = (int)Math.Max(10, Math.Min(100, equilibrio.Value.q + 5));
                qMax = (int)Math.Ceiling(qMax / (double)passo) * passo; // Arrotonda al passo successivo
            }
            else
            {
                
                qMax = 20;
            }

            
            for (int q = qMin; q <= qMax; q += passo)
            {
                double pDom = CalcolaDomanda(q, aD, bD);
                double pOff = CalcolaOfferta(q, aS, bS, cS);

                dgvRisultati.Rows.Add(q, pDom.ToString("F2"), pOff.ToString("F2"));
            }

            
            if (equilibrio.HasValue)
            {
                lblRisultato.Text = $"Prezzo di equilibrio: P = {equilibrio.Value.p:F2}, Quantità = {equilibrio.Value.q:F2}\n" +
                                   $"QMax automatico: {qMax} (equilibrio + 5)";

                
                MostraCalcoliVerifica(equilibrio.Value.q, equilibrio.Value.p, aD, bD, aS, bS, cS);
            }
            else
            {
                lblRisultato.Text = $"Nessun equilibrio trovato. QMax usato: {qMax}";
            }

           
            DisegnaGrafico(qMin, qMax, aD, bD, aS, bS, cS, equilibrio);
        }

        private double CalcolaDomanda(double q, double aD, double bD)
        {
            
            return aD - (bD * q);
        }

        private double CalcolaOfferta(double q, double aS, double bS, double cS)
        {
            
            return aS + bS * Math.Pow(q, cS);
        }

        private (double q, double p)? TrovaEquilibrioPreciso(double aD, double bD, double aS, double bS, double cS)
        {
            
            double qStart = 0;
            double qEnd = 0;

            
            for (double q = 0; q <= 100; q += 0.1) 
            {
                double pDom = CalcolaDomanda(q, aD, bD);
                double pOff = CalcolaOfferta(q, aS, bS, cS);

                if (pOff >= pDom)
                {
                    qStart = q - 0.1; 
                    qEnd = q;         
                    break;
                }
            }

            
            if (qEnd == 0) return null;

            
            return TrovaEquilibrioEsatto(qStart, qEnd, aD, bD, aS, bS, cS);
        }

        private (double q, double p) TrovaEquilibrioEsatto(double qStart, double qEnd, double aD, double bD, double aS, double bS, double cS)
        {
            int maxIterazioni = 1000; 
            double tolleranza = 0.0000001; 

            double qEquilibrio = 0;
            double pEquilibrio = 0;

            for (int i = 0; i < maxIterazioni; i++)
            {
                double qMid = (qStart + qEnd) / 2;
                double pDom = CalcolaDomanda(qMid, aD, bD);
                double pOff = CalcolaOfferta(qMid, aS, bS, cS);
                double differenza = pDom - pOff;

              
                if (Math.Abs(differenza) < tolleranza)
                {
                    qEquilibrio = qMid;
                    pEquilibrio = pDom;
                    break;
                }

                if (differenza > 0)
                {
                    
                    qStart = qMid;
                }
                else
                {
                    
                    qEnd = qMid;
                }

                
                if (i == maxIterazioni - 1)
                {
                    qEquilibrio = qMid;
                    pEquilibrio = pDom;
                }
            }

            return (qEquilibrio, pEquilibrio);
        }

        private void MostraCalcoliVerifica(double q, double p, double aD, double bD, double aS, double bS, double cS)
        {
            double domandaVerifica = CalcolaDomanda(q, aD, bD);
            double offertaVerifica = CalcolaOfferta(q, aS, bS, cS);

            string verifica = $"\n\nVERIFICA PRECISIONE EQUILIBRIO:\n" +
                            $"Quantità esatta: q = {q:F8}\n" +
                            $"Domanda({q:F8}) = {aD} - ({bD} × {q:F8}) = {domandaVerifica:F8}\n" +
                            $"Offerta({q:F8}) = {aS} + ({bS} × {q:F8}³) = {offertaVerifica:F8}\n" +
                            $"DIFFERENZA: {Math.Abs(domandaVerifica - offertaVerifica):F10}";

            lblRisultato.Text += verifica;
        }

        private void DisegnaGrafico(int qMin, int qMax,
                                  double aD, double bD, double aS, double bS, double cS,
                                  (double q, double p)? equilibrio)
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

           
            ChartArea area = new ChartArea("AreaPrincipale");
            area.AxisX.Title = "Quantità (Q)";
            area.AxisY.Title = "Prezzo (P)";
            area.AxisX.Minimum = qMin;
            area.AxisX.Maximum = qMax;
            chart1.ChartAreas.Add(area);

         
            Series serieDomanda = new Series("Domanda");
            serieDomanda.ChartType = SeriesChartType.Line;
            serieDomanda.BorderWidth = 3;
            serieDomanda.Color = System.Drawing.Color.Blue;
            serieDomanda.LegendText = $"Domanda: {aD} - {bD}q";

         
            Series serieOfferta = new Series("Offerta");
            serieOfferta.ChartType = SeriesChartType.Line;
            serieOfferta.BorderWidth = 3;
            serieOfferta.Color = System.Drawing.Color.Red;
            serieOfferta.LegendText = $"Offerta: {aS} + {bS}q³";

        
            for (int q = qMin; q <= qMax; q++)
            {
                double pDom = CalcolaDomanda(q, aD, bD);
                double pOff = CalcolaOfferta(q, aS, bS, cS);

                serieDomanda.Points.AddXY(q, pDom);
                serieOfferta.Points.AddXY(q, pOff);
            }

            chart1.Series.Add(serieDomanda);
            chart1.Series.Add(serieOfferta);

           
            if (equilibrio.HasValue)
            {
                Series serieEquilibrio = new Series("Equilibrio");
                serieEquilibrio.ChartType = SeriesChartType.Point;
                serieEquilibrio.Color = System.Drawing.Color.Green;
                serieEquilibrio.MarkerSize = 10;
                serieEquilibrio.MarkerStyle = MarkerStyle.Circle;
                serieEquilibrio.LegendText = $"Equilibrio (Q={equilibrio.Value.q:F2}, P={equilibrio.Value.p:F2})";

                serieEquilibrio.Points.AddXY(equilibrio.Value.q, equilibrio.Value.p);
                chart1.Series.Add(serieEquilibrio);

                
                Series lineaEquilibrio = new Series("Linea Equilibrio");
                lineaEquilibrio.ChartType = SeriesChartType.Line;
                lineaEquilibrio.Color = System.Drawing.Color.Green;
                lineaEquilibrio.BorderWidth = 2;
                lineaEquilibrio.BorderDashStyle = ChartDashStyle.Dash;

                lineaEquilibrio.Points.AddXY(equilibrio.Value.q, 0);
                lineaEquilibrio.Points.AddXY(equilibrio.Value.q, equilibrio.Value.p);
                chart1.Series.Add(lineaEquilibrio);
            }

            
            chart1.Legends.Clear();
            Legend legenda = new Legend();
            chart1.Legends.Add(legenda);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ImpostaValoriDefault();
            dgvRisultati.Rows.Clear();
            chart1.Series.Clear();
            lblRisultato.Text = "Premi 'Calcola' per trovare l'equilibrio con QMax automatico";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}