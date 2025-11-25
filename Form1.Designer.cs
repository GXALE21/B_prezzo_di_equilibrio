namespace ese_prezzo_di_equilibrio
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtQMin;
        private System.Windows.Forms.TextBox txtQMax;
        private System.Windows.Forms.TextBox txtPasso;
        private System.Windows.Forms.TextBox txtDomandaA;
        private System.Windows.Forms.TextBox txtDomandaB;
        private System.Windows.Forms.TextBox txtOffertaA;
        private System.Windows.Forms.TextBox txtOffertaB;
        private System.Windows.Forms.Button btnCalcola;
        private System.Windows.Forms.DataGridView dgvRisultati;
        private System.Windows.Forms.Label lblRisultato;

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.txtQMin = new System.Windows.Forms.TextBox();
            this.txtQMax = new System.Windows.Forms.TextBox();
            this.txtPasso = new System.Windows.Forms.TextBox();
            this.txtDomandaA = new System.Windows.Forms.TextBox();
            this.txtDomandaB = new System.Windows.Forms.TextBox();
            this.txtOffertaA = new System.Windows.Forms.TextBox();
            this.txtOffertaB = new System.Windows.Forms.TextBox();
            this.btnCalcola = new System.Windows.Forms.Button();
            this.dgvRisultati = new System.Windows.Forms.DataGridView();
            this.lblRisultato = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtOffertaC = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRisultati)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtQMin
            // 
            this.txtQMin.Location = new System.Drawing.Point(83, 20);
            this.txtQMin.Name = "txtQMin";
            this.txtQMin.Size = new System.Drawing.Size(100, 20);
            this.txtQMin.TabIndex = 0;
            // 
            // txtQMax
            // 
            this.txtQMax.Location = new System.Drawing.Point(83, 60);
            this.txtQMax.Name = "txtQMax";
            this.txtQMax.Size = new System.Drawing.Size(100, 20);
            this.txtQMax.TabIndex = 1;
            // 
            // txtPasso
            // 
            this.txtPasso.Location = new System.Drawing.Point(83, 102);
            this.txtPasso.Name = "txtPasso";
            this.txtPasso.Size = new System.Drawing.Size(100, 20);
            this.txtPasso.TabIndex = 2;
            // 
            // txtDomandaA
            // 
            this.txtDomandaA.Location = new System.Drawing.Point(307, 20);
            this.txtDomandaA.Name = "txtDomandaA";
            this.txtDomandaA.Size = new System.Drawing.Size(100, 20);
            this.txtDomandaA.TabIndex = 3;
            // 
            // txtDomandaB
            // 
            this.txtDomandaB.Location = new System.Drawing.Point(307, 60);
            this.txtDomandaB.Name = "txtDomandaB";
            this.txtDomandaB.Size = new System.Drawing.Size(100, 20);
            this.txtDomandaB.TabIndex = 4;
            // 
            // txtOffertaA
            // 
            this.txtOffertaA.Location = new System.Drawing.Point(550, 20);
            this.txtOffertaA.Name = "txtOffertaA";
            this.txtOffertaA.Size = new System.Drawing.Size(100, 20);
            this.txtOffertaA.TabIndex = 5;
            // 
            // txtOffertaB
            // 
            this.txtOffertaB.Location = new System.Drawing.Point(550, 60);
            this.txtOffertaB.Name = "txtOffertaB";
            this.txtOffertaB.Size = new System.Drawing.Size(100, 20);
            this.txtOffertaB.TabIndex = 6;
            // 
            // btnCalcola
            // 
            this.btnCalcola.Location = new System.Drawing.Point(96, 151);
            this.btnCalcola.Name = "btnCalcola";
            this.btnCalcola.Size = new System.Drawing.Size(75, 23);
            this.btnCalcola.TabIndex = 7;
            this.btnCalcola.Text = "Calcola";
            this.btnCalcola.Click += new System.EventHandler(this.btnCalcola_Click);
            // 
            // dgvRisultati
            // 
            this.dgvRisultati.Location = new System.Drawing.Point(20, 200);
            this.dgvRisultati.Name = "dgvRisultati";
            this.dgvRisultati.Size = new System.Drawing.Size(424, 250);
            this.dgvRisultati.TabIndex = 8;
            // 
            // lblRisultato
            // 
            this.lblRisultato.AutoSize = true;
            this.lblRisultato.Location = new System.Drawing.Point(20, 470);
            this.lblRisultato.Name = "lblRisultato";
            this.lblRisultato.Size = new System.Drawing.Size(51, 13);
            this.lblRisultato.TabIndex = 9;
            this.lblRisultato.Text = "Risultato:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Qmin";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Qmax";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(210, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "domanda A";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(210, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "domanda B";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(452, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "offerta A";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(452, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "offerta B";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "passo";
            // 
            // txtOffertaC
            // 
            this.txtOffertaC.Location = new System.Drawing.Point(307, 102);
            this.txtOffertaC.Name = "txtOffertaC";
            this.txtOffertaC.Size = new System.Drawing.Size(100, 20);
            this.txtOffertaC.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(225, 105);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "offerta c";
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(455, 201);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(339, 249);
            this.chart1.TabIndex = 19;
            this.chart1.Text = "chart1";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(806, 569);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtOffertaC);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtQMin);
            this.Controls.Add(this.txtQMax);
            this.Controls.Add(this.txtPasso);
            this.Controls.Add(this.txtDomandaA);
            this.Controls.Add(this.txtDomandaB);
            this.Controls.Add(this.txtOffertaA);
            this.Controls.Add(this.txtOffertaB);
            this.Controls.Add(this.btnCalcola);
            this.Controls.Add(this.dgvRisultati);
            this.Controls.Add(this.lblRisultato);
            this.Name = "Form1";
            this.Text = "Calcolo Prezzo di Equilibrio";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRisultati)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtOffertaC;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}
