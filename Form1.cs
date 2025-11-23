using System;
using System.Windows.Forms;

namespace ese_prezzo_di_equilibrio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Imposto le colonne della DataGridView qui, NON nel designer
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

            // Leggi input
            if (!int.TryParse(txtQMin.Text, out int qMin) ||
                !int.TryParse(txtQMax.Text, out int qMax) ||
                !int.TryParse(txtPasso.Text, out int passo))
            {
                MessageBox.Show("Inserisci solo numeri interi!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!double.TryParse(txtDomandaA.Text, out double aD) ||
                !double.TryParse(txtDomandaB.Text, out double bD) ||
                !double.TryParse(txtOffertaA.Text, out double aS) ||
                !double.TryParse(txtOffertaB.Text, out double bS))
            {
                MessageBox.Show("Coefficienti non validi!", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double equilibrioQ = -1;
            double equilibrioP = -1;

            for (int q = qMin; q <= qMax; q += passo)
            {
                double pDom = aD + bD * q;    // funzione domanda
                double pOff = aS + bS * q;    // funzione offerta

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
        }
    }
}
