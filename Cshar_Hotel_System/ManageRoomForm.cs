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
    public partial class ManageRoomForm : Form
    {
        public ManageRoomForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        ROOM room = new ROOM();
        private void ManageRoomForm_Load_1(object sender, EventArgs e)
        {
            ComboBoxRoomType.DataSource = room.roomTypeList();
            ComboBoxRoomType.DisplayMember = "label";
            ComboBoxRoomType.ValueMember = "category_id";

            dataGridView1.DataSource = room.getRoom();
        }

        //for create a function for new room
        private void buttonAddRoom_Click(object sender, EventArgs e)
        {

            int type = Convert.ToInt32(ComboBoxRoomType.SelectedValue.ToString());
            string phone = textBoxPhone.Text;
            String free = "";

            try
            {
                int number = Convert.ToInt32(textBoxNumber.Text);
                if (radioButton1.Checked)
                {
                    free = "Yes";
                }
                else
                {
                    free = "No";
                }


                if (room.addRoom(number, type, phone, free))
                {
                    dataGridView1.DataSource = room.getRoom();
                    MessageBox.Show("Room Added sucessfully", "add room", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Room Not sucessfully", "add room", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "room number error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }



        private void buttonEditRoom_Click(object sender, EventArgs e)
        {
           
            
            int type = Convert.ToInt32(ComboBoxRoomType.SelectedValue.ToString());
            string phone = textBoxPhone.Text;
            string free;

            try
            {
                int number = Convert.ToInt32(textBoxNumber.Text);

                if (radioButton1.Checked)
                {
                    free = "Yes";
                }
                else
                {
                    free = "No";
                }
                if (room.editRoom(number, type, phone, free))
                {
                    dataGridView1.DataSource = room.getRoom();
                    MessageBox.Show("Room updated sucessfully", "edit room", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Room Not updated sucessfully", "edit room", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "room number error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    

        private void buttonRemoveRoom_Click(object sender, EventArgs e)
        {
            try
            {

                int number = Convert.ToInt32(textBoxNumber.Text);
                if (room.removeRoom(number))
                {
                    dataGridView1.DataSource = room.getRoom();
                    MessageBox.Show("Room deleted sucessfull", "delete Room", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("ERROR - Room Not deleted", "deleted Room", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "room number error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxNumber.Text = "";
            ComboBoxRoomType.SelectedIndex= 0;
            textBoxPhone.Text = "";
        }

        // dilply selected in text boxes
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxNumber.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            ComboBoxRoomType.SelectedValue = dataGridView1.CurrentRow.Cells[1].Value;
            textBoxPhone.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            String free = dataGridView1.CurrentRow.Cells[3].Value.ToString();

            if(free.Equals("Yes"))
            {
                radioButton1.Checked = true;
            }
            else if(free.Equals("No"))
            {
                radioButton2.Checked = true;
            }
        }

        private void ComboBoxRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
