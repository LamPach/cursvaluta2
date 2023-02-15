using Cureser.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cureser
{
    public partial class Exchange : Form
    {
        public Exchange(UserData userData)
        {
            InitializeComponent();
            this.userData = userData;
            this.exchangeProvider = new ExchangeProvider();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            turnFav();
        }

        private UserData userData;
        private ExchangeProvider exchangeProvider;

        private async void turnFav()
        {
            this.button2.Hide();
            this.button3.Show();
            this.button1.Hide();
            this.button4.Show();
            var data = await exchangeProvider.GetExchange(userData.FavCur);
            var bindingList = new BindingList<ExchangeModel>(data);
            this.dataGridView1.DataSource = bindingList;
        }

        private async void turnAll()
        {
            this.button3.Hide();
            this.button2.Show();
            this.button1.Show();
            this.button4.Hide();
            var data = await exchangeProvider.GetExchange();
            var bindingList = new BindingList<ExchangeModel>(data);
            this.dataGridView1.DataSource = bindingList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            turnFav();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            turnAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int idx = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[idx];
                string cellValue = Convert.ToString(selectedRow.Cells[0].Value);
                userData.AddCurrencyToFavourite(cellValue);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int idx = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[idx];
                string cellValue = Convert.ToString(selectedRow.Cells[0].Value);
                userData.DeleteCurrenctFromFavourite(cellValue);
                turnFav();
            }
        }
    }
}
