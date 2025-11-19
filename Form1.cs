using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ese_prezzo_di_equilibrio
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        public class ParametriMercato
        {
            public double DomandaIntercetta { get; set; } = 90;
            public double DomandaCoefficiente { get; set; } = 4;
            public double OffertaCostante { get; set; } = 10;
            public double OffertaCoefficiente { get; set; } = 0.01;
            public double OffertaEsponente { get; set; } = 3;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
