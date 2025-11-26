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
            // Inizializza DataGridView
            dgvRisultati.Columns.Clear();
            dgvRisultati.Columns.Add("q", "Quantità (q)");
            dgvRisultati.Columns.Add("domanda", "Prezzo Domanda");
            dgvRisultati.Columns.Add("offerta", "Prezzo Offerta");

            // Imposta valori di default
            ImpostaValoriDefault();
        }

        private void ImpostaValoriDefault()
        {
            // Valori Q
            txtQMin.Text = "0";
            txtQMax.Text = "20";
            txtPasso.Text = "1";

            // Domanda: d = 90 - 4q
            txtDomandaA.Text = "90";
            txtDomandaB.Text = "4";

            // Offerta: o = 10 + q³/100
            txtOffertaA.Text = "10";
            txtOffertaB.Text = "0.01";
            txtOffertaC.Text = "3";  // Esponente per q³
        }

        private void btnCalcola_Click(object sender, EventArgs e)
        {
            CalcolaEquilibrio();
        }

        private void CalcolaEquilibrio()
        {
            dgvRisultati.Rows.Clear();

            // Lettura parametri Q
            if (!int.TryParse(txtQMin.Text, out int qMin) ||
                !int.TryParse(txtQMax.Text, out int qMax) ||
                !int.TryParse(txtPasso.Text, out int passo))
            {
                MessageBox.Show("Inserisci solo numeri interi per Qmin, Qmax e passo!", "Errore",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validazione range
            if (qMin < 0 || qMax <= qMin || passo <= 0)
            {
                MessageBox.Show("Qmin >= 0, Qmax > Qmin, passo > 0", "Errore",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ------- LETTURA PARAMETRI DOMANDA -------
            if (!double.TryParse(txtDomandaA.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double aD) ||
                !double.TryParse(txtDomandaB.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double bD))
            {
                MessageBox.Show("Coefficienti della domanda non validi!", "Errore",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // ------- LETTURA PARAMETRI OFFERTA -------
            if (!double.TryParse(txtOffertaA.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double aS) ||
                !double.TryParse(txtOffertaB.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double bS) ||
                !double.TryParse(txtOffertaC.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double cS))
            {
                MessageBox.Show("Coefficienti dell'offerta non validi!", "Errore",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Calcola punti per la tabella
            for (int q = qMin; q <= qMax; q += passo)
            {
                double pDom = CalcolaDomanda(q, aD, bD);
                double pOff = CalcolaOfferta(q, aS, bS, cS);

                dgvRisultati.Rows.Add(q, pDom.ToString("F2"), pOff.ToString("F2"));
            }

            // Trova equilibrio con alta precisione (senza limite QMax)
            var equilibrio = TrovaEquilibrioPreciso(aD, bD, aS, bS, cS);

            if (equilibrio.HasValue)
            {
                lblRisultato.Text = $"Prezzo di equilibrio: P = {equilibrio.Value.p:F2}, Quantità = {equilibrio.Value.q:F2}";

                // Mostra calcoli di verifica
                MostraCalcoliVerifica(equilibrio.Value.q, equilibrio.Value.p, aD, bD, aS, bS, cS);
            }
            else
            {
                lblRisultato.Text = "Nessun equilibrio trovato nel range analizzato.";
            }

            // Disegno grafico
            DisegnaGrafico(qMin, qMax, aD, bD, aS, bS, cS, equilibrio);
        }

        private double CalcolaDomanda(int q, double aD, double bD)
        {
            // Formula: d = aD - bD * q
            return aD - (bD * q);
        }

        private double CalcolaOfferta(int q, double aS, double bS, double cS)
        {
            // Formula: o = aS + bS * q^cS
            return aS + bS * Math.Pow(q, cS);
        }

        private (double q, double p)? TrovaEquilibrioPreciso(double aD, double bD, double aS, double bS, double cS)
        {
            // Cerca equilibrio fino a Q=50 (più che sufficiente)
            for (double q = 0; q <= 50; q += 0.001)
            {
                double pDom = CalcolaDomanda((int)q, aD, bD);
                double pOff = CalcolaOfferta((int)q, aS, bS, cS);

                // Se offerta supera domanda, abbiamo trovato la zona di equilibrio
                if (pOff >= pDom)
                {
                    // Cerchiamo il punto esatto con ricerca binaria nell'intorno
                    return TrovaEquilibrioEsatto(q - 1, q, aD, bD, aS, bS, cS);
                }
            }

            return null;
        }

        private (double q, double p)? TrovaEquilibrioEsatto(double qStart, double qEnd, double aD, double bD, double aS, double bS, double cS)
        {
            // Ricerca binaria per equilibrio preciso
            int maxIterazioni = 100;
            double tolleranza = 0.0001;

            for (int i = 0; i < maxIterazioni; i++)
            {
                double qMid = (qStart + qEnd) / 2;
                double pDom = CalcolaDomanda((int)qMid, aD, bD);
                double pOff = CalcolaOfferta((int)qMid, aS, bS, cS);
                double differenza = pDom - pOff;

                if (Math.Abs(differenza) < tolleranza)
                {
                    return (Math.Round(qMid, 2), Math.Round(pDom, 2));
                }

                if (differenza > 0)
                {
                    qStart = qMid; // Domanda > Offerta, aumenta q
                }
                else
                {
                    qEnd = qMid; // Offerta > Domanda, diminuisci q
                }
            }

            // Se non converge, restituisci il migliore
            double qBest = (qStart + qEnd) / 2;
            double pBest = CalcolaDomanda((int)qBest, aD, bD);
            return (Math.Round(qBest, 2), Math.Round(pBest, 2));
        }

        private void MostraCalcoliVerifica(double q, double p, double aD, double bD, double aS, double bS, double cS)
        {
            double domandaVerifica = CalcolaDomanda((int)q, aD, bD);
            double offertaVerifica = CalcolaOfferta((int)q, aS, bS, cS);

            string verifica = $"\n\nVerifica equilibrio:\n" +
                            $"Domanda({q:F2}) = {aD} - ({bD} × {q:F2}) = {domandaVerifica:F2}\n" +
                            $"Offerta({q:F2}) = {aS} + ({bS} × {q:F2}³) = {offertaVerifica:F2}\n" +
                            $"Differenza: {Math.Abs(domandaVerifica - offertaVerifica):F4}";

            lblRisultato.Text += verifica;
        }

        private void DisegnaGrafico(int qMin, int qMax,
                                  double aD, double bD, double aS, double bS, double cS,
                                  (double q, double p)? equilibrio)
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            // Configura area del grafico
            ChartArea area = new ChartArea("AreaPrincipale");
            area.AxisX.Title = "Quantità (Q)";
            area.AxisY.Title = "Prezzo (P)";
            area.AxisX.Minimum = qMin;
            area.AxisX.Maximum = qMax;
            chart1.ChartAreas.Add(area);

            // Serie Domanda
            Series serieDomanda = new Series("Domanda");
            serieDomanda.ChartType = SeriesChartType.Line;
            serieDomanda.BorderWidth = 3;
            serieDomanda.Color = System.Drawing.Color.Blue;
            serieDomanda.LegendText = $"Domanda: {aD} - {bD}q";

            // Serie Offerta
            Series serieOfferta = new Series("Offerta");
            serieOfferta.ChartType = SeriesChartType.Line;
            serieOfferta.BorderWidth = 3;
            serieOfferta.Color = System.Drawing.Color.Red;
            serieOfferta.LegendText = $"Offerta: {aS} + {bS}q³";

            // Aggiungi punti al grafico
            for (int q = qMin; q <= qMax; q++)
            {
                double pDom = CalcolaDomanda(q, aD, bD);
                double pOff = CalcolaOfferta(q, aS, bS, cS);

                serieDomanda.Points.AddXY(q, pDom);
                serieOfferta.Points.AddXY(q, pOff);
            }

            chart1.Series.Add(serieDomanda);
            chart1.Series.Add(serieOfferta);

            // Aggiungi punto di equilibrio se trovato
            if (equilibrio.HasValue && equilibrio.Value.q >= qMin && equilibrio.Value.q <= qMax)
            {
                Series serieEquilibrio = new Series("Equilibrio");
                serieEquilibrio.ChartType = SeriesChartType.Point;
                serieEquilibrio.Color = System.Drawing.Color.Green;
                serieEquilibrio.MarkerSize = 10;
                serieEquilibrio.MarkerStyle = MarkerStyle.Circle;
                serieEquilibrio.LegendText = $"Equilibrio (Q={equilibrio.Value.q:F2}, P={equilibrio.Value.p:F2})";

                serieEquilibrio.Points.AddXY(equilibrio.Value.q, equilibrio.Value.p);
                chart1.Series.Add(serieEquilibrio);
            }

            // Abilita legenda
            chart1.Legends.Clear();
            Legend legenda = new Legend();
            chart1.Legends.Add(legenda);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ImpostaValoriDefault();
            dgvRisultati.Rows.Clear();
            chart1.Series.Clear();
            lblRisultato.Text = "Premi 'Calcola' per trovare l'equilibrio";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Eventuale codice aggiuntivo al caricamento
        }
    }
}