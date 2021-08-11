using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Cshar_Hotel_System
{
    public partial class ManageClientsForm : Form
    {
        CLIENT client = new CLIENT();
        public ManageClientsForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxID.Text = "";
            textBoxFirestName.Text = "";
            textBoxLastName.Text = "";
            textBoxPhone.Text = "";
            textBoxCountry.Text = "";
        }

        private void buttonAddClient_Click(object sender, EventArgs e)
        {
            String fname = textBoxFirestName.Text;
            String lname = textBoxLastName.Text;
            String phone = textBoxPhone.Text;
            String country = textBoxCountry.Text;

            if(fname.Trim().Equals("") || lname.Trim().Equals("") || phone.Trim().Equals("") || country.Trim().Equals(""))
            {
                MessageBox.Show("Reuired Fields - all", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Boolean insertClient = client.insetClient(fname,lname,phone,country);

                if (insertClient)
                {
                    dataGridView1.DataSource = client.getClient();
                    MessageBox.Show("New Client Inserted Successfully", "Add Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("ERROR - Client Not Inserted", "Add client", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
          
        }

        private void ManageClientsForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = client.getClient();
        }

        private void buttonEditClient_Click(object sender, EventArgs e)
        {
            int id;
            String fname = textBoxFirestName.Text;
            String lname = textBoxLastName.Text;
            String phone = textBoxPhone.Text;
            String country = textBoxCountry.Text;

            try
            {
                id = Convert.ToInt32(textBoxID.Text);
                if (fname.Trim().Equals("") || lname.Trim().Equals("") || phone.Trim().Equals("") || country.Trim().Equals(""))
                {
                    MessageBox.Show("Reuired Fields - all", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Boolean editClient = client.editClient(id, fname, lname, phone, country);

                    if (editClient)
                    {
                        dataGridView1.DataSource = client.getClient();
                        MessageBox.Show("New Client updated Successfully", "edit Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("ERROR - Client Not updated", "edit client", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        //display click in table to form datagridview to textboxess
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxFirestName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxLastName.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBoxPhone.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBoxCountry.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();

        }

        private void buttonRemoveClient_Click(object sender, EventArgs e)
        {
            try
            {


                int id = Convert.ToInt32(textBoxID.Text);
                if(client.removeClient(id))
                {
                    dataGridView1.DataSource = client.getClient();
                    MessageBox.Show("Client deleted sucessfull", "delete Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //get a deleted dataa
                    buttonClear.PerformClick();

                }
                else
                {
                    MessageBox.Show("ERROR - Client Not deleted", "deleted client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
