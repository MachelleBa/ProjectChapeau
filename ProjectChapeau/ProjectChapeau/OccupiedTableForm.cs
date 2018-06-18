﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chapeau_Model;
using Chapeau_Logic;

namespace ProjectChapeau
{
    public partial class OccupiedTableForm : Form
    {
        private TableTop table;

        public OccupiedTableForm(TableTop table)
        {
            InitializeComponent();
            this.table = table;
            FillTableList();
        }

        private void FillTableList()
        {
            tableReceiptListView.HideSelection = false;
            tableReceiptListView.Items.Clear();
            List<OrderingModel.Item> TableItemsList = new List<OrderingModel.Item>();
            TableItemsList = OrderingLogic.CallTableItemsDB(this.table.GetTableId());
            decimal price = 0;

            foreach (OrderingModel.Item TableItem in TableItemsList)
            {

                ListViewItem LvTableItem = new ListViewItem(TableItem.Name);
                LvTableItem.SubItems.Add("...................");
                LvTableItem.SubItems.Add(TableItem.itemPrice.ToString());
                tableReceiptListView.Items.Add(LvTableItem);
                price = TableItem.itemPrice + price;
            }

            ListViewItem LvPriceItem = new ListViewItem("Total Price");
            LvPriceItem.SubItems.Add("...................");
            LvPriceItem.SubItems.Add(price.ToString());

        }

        private void tableReceiptListView_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void startPaymentButton_Click(object sender, EventArgs e)
        {

        }

        private void statusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            statusDisplayLabel.Text = statusComboBox.Text;
        }

        private void addItemButton_Click(object sender, EventArgs e)
        {
            OrderingForm ordering = new OrderingForm(table);
            ordering.ShowDialog();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cancelOrderButton_Click(object sender, EventArgs e)
        {
            DialogResult ContinueDialog = MessageBox.Show("Are you sure you want to cancel this order?", "Chapeau says", MessageBoxButtons.YesNo);
            if (ContinueDialog == DialogResult.Yes)
            {
                OrderingLogic.ActionDeleteOrdersDB(table.GetTableId());
                this.Close();
            }
            
        }
    }
}