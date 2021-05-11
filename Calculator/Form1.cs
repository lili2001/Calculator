using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        double resultValue = 0;
        double memory = 0;
        bool isMemoryUsed = false;
        string operation = "";
        bool isOperationPerformed = false;
        bool specialOp = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void EnableMemoryButtons()
        {
            btnMC.Enabled = true;
            btnMR.Enabled = true;
            btnShowMemory.Enabled = true;
        }

        private void ValidateDecimalDot()
        {
            string s = textBoxResult.Text;
            int lenght = textBoxResult.Text.Length;
            if (textBoxResult.Text[lenght-1] == '.') textBoxResult.Text = s.Substring(0, s.Length - 1);
        }

        private void CheckForIncompleteOp()
        {
            char[] ops = { '+', '-', '*', '/' };
            if (labelCurrent.Text!="")
            {
                if (ops.Contains(labelCurrent.Text[labelCurrent.Text.Length - 1])) btnEqual.PerformClick();
            }   
        }

        private void button_click(object sender, EventArgs e)
        {
            if (textBoxResult.Text == "0" || isOperationPerformed) textBoxResult.Clear();
            isOperationPerformed = false;
            var button = (Button)sender;
            if (button.Text == ".")
            {
                if (textBoxResult.Text == "") textBoxResult.Text = "0";
                if (!textBoxResult.Text.Contains(".")) textBoxResult.Text = textBoxResult.Text + button.Text;
            }
            else
            {
                textBoxResult.Text = textBoxResult.Text + button.Text;
            }
        }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            string s = textBoxResult.Text;
            if (s.Length > 1)
            {
                s = s.Substring(0, s.Length - 1);
            }
            else
            {
                s = "0";
            }
            textBoxResult.Text = s;
        }

        private void operator_click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (resultValue != 0 && labelCurrent.Text!="" && !specialOp)
            {
                btnEqual.PerformClick();
                operation = button.Text;
                labelCurrent.Text = resultValue + " " + operation;
                isOperationPerformed = true;    
            }
            else
            {
                ValidateDecimalDot();
                operation = button.Text;
                resultValue = double.Parse(textBoxResult.Text);
                labelCurrent.Text = resultValue + " " + operation;
                isOperationPerformed = true;
                specialOp = false;
            }         
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            ValidateDecimalDot();
            switch (operation)
            {
                case "+":
                    textBoxResult.Text = (resultValue + double.Parse(textBoxResult.Text)).ToString();
                    break;
                case "-":
                    textBoxResult.Text = (resultValue - double.Parse(textBoxResult.Text)).ToString();
                    break;
                case "*":
                    textBoxResult.Text = (resultValue * double.Parse(textBoxResult.Text)).ToString();
                    break;
                case "/":
                    textBoxResult.Text = (resultValue / double.Parse(textBoxResult.Text)).ToString();
                    break;
                default:
                    break;
            }
            resultValue = double.Parse(textBoxResult.Text);
            labelCurrent.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textBoxResult.Text = "0";
            labelCurrent.Text = "";
            resultValue = 0;
        }

        private void btnClearEntry_Click(object sender, EventArgs e)
        {
            textBoxResult.Text="0";
        }

        private void btnSquare_Click(object sender, EventArgs e)
        {
            CheckForIncompleteOp();
            labelCurrent.Text = textBoxResult.Text + "²";
            resultValue=Math.Pow(double.Parse(textBoxResult.Text),2);
            textBoxResult.Text = resultValue.ToString();
            isOperationPerformed = true;
            specialOp = true;
        }

        private void btnCubed_Click(object sender, EventArgs e)
        {
            CheckForIncompleteOp();
            labelCurrent.Text = textBoxResult.Text + "³";
            textBoxResult.Text = (Math.Pow(double.Parse(textBoxResult.Text), 3)).ToString();
            isOperationPerformed = true;
            specialOp = true;
        }

        private void btnSqrt_Click(object sender, EventArgs e)
        {
            CheckForIncompleteOp();
            if (double.Parse(textBoxResult.Text) < 0) MessageBox.Show("Invalid input! Enter a positive number or choose another operation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                labelCurrent.Text = "√(" + textBoxResult.Text + ")";
                textBoxResult.Text = (Math.Sqrt(double.Parse(textBoxResult.Text))).ToString();
                isOperationPerformed = true;
                specialOp = true;
            }
        }

        private void btnCubicRt_Click(object sender, EventArgs e)
        {
            CheckForIncompleteOp();
            labelCurrent.Text = "³√(" + textBoxResult.Text + ")";
            double tempCubRt=double.Parse(textBoxResult.Text);
            if (tempCubRt < 0) textBoxResult.Text = (-Math.Pow(-tempCubRt, (double)1 / 3)).ToString();
            else textBoxResult.Text = (Math.Pow(tempCubRt, (double)1 / 3)).ToString();
            isOperationPerformed = true;
            specialOp = true;
        }

        private void btnMemoryAdd_Click(object sender, EventArgs e)
        {
            if (!isMemoryUsed)
            {
                memory += double.Parse(textBoxResult.Text);
                listBox1.Items.Insert(0, memory);
                isMemoryUsed = true;
                EnableMemoryButtons();
                
            }
            else
            {
                memory += double.Parse(textBoxResult.Text);
                listBox1.Items[0] = memory;
                listBox1.Items[0] = listBox1.Items[0];
            } 
        }

        private void btnMemorySub_Click(object sender, EventArgs e)
        {
            if (!isMemoryUsed)
            {
                memory -= double.Parse(textBoxResult.Text);
                listBox1.Items.Insert(0, memory);
                isMemoryUsed = true;
                EnableMemoryButtons();
                
            }
            else
            {
                memory -= double.Parse(textBoxResult.Text);
                listBox1.Items[0] = memory;
                listBox1.Items[0] = listBox1.Items[0];
            }  
        }

        private void btnMR_Click(object sender, EventArgs e)
        {
            textBoxResult.Text = memory.ToString();
        }

        private void btnMC_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            memory = 0;
            btnMC.Enabled = false;
            btnMR.Enabled = false;
            btnShowMemory.Enabled = false;
            isMemoryUsed = false;
        }

        private void btnNegate_Click(object sender, EventArgs e)
        {
            if (textBoxResult.Text!="0")
            {
                double tempNeg = double.Parse(textBoxResult.Text) * -1;
                textBoxResult.Text = tempNeg.ToString();
            }
        }

        private void btnDivideByValue_Click(object sender, EventArgs e)
        {
            CheckForIncompleteOp();
            if (textBoxResult.Text != "0")
            {
                double tempDiv = 1.0 / double.Parse(textBoxResult.Text);
                labelCurrent.Text = "1 / " + textBoxResult.Text;
                textBoxResult.Text = tempDiv.ToString();
            }
            else MessageBox.Show("Cannot divide by 0!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnMemoryStore_Click(object sender, EventArgs e)
        {
            memory =double.Parse(textBoxResult.Text);
            listBox1.Items.Insert(0,memory);
            EnableMemoryButtons();
        }

        private void btnMSAdd_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0) MessageBox.Show("Select an item!");
            else
            {
                int id = listBox1.SelectedIndex;
                int temp = int.Parse(textBoxResult.Text) + int.Parse(listBox1.SelectedItem.ToString());
                listBox1.Items[id] = temp;
                listBox1.Items[id] = listBox1.Items[id];
            }
        }

        private void btnShowMemory_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void btnMSSub_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0) MessageBox.Show("Select an item!");
            else
            {
                int id = listBox1.SelectedIndex;
                int temp = int.Parse(listBox1.SelectedItem.ToString()) - int.Parse(textBoxResult.Text);
                listBox1.Items[id] = temp;
                listBox1.Items[id] = listBox1.Items[id];
            }
        }

        private void btnHideMemory_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0) memory = double.Parse(listBox1.Items[0].ToString());
            else btnMC.PerformClick();
            panel1.Visible = false;
        }

        private void btnMSClearItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0) MessageBox.Show("Select an item!");
            else
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            textBoxResult.Text = listBox1.SelectedItem.ToString();
            listBox1.SetSelected(listBox1.SelectedIndex, false);
            panel1.Visible = false;
        }
    }
}
