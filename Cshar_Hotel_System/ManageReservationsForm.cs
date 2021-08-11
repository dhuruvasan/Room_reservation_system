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
    public partial class ManageReservationsForm : Form
    {
        public ManageReservationsForm()
        {
            InitializeComponent();
        }

        ROOM room = new ROOM();
        RESERVATION resrvation = new RESERVATION();


        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonAddReserv_Click(object sender, EventArgs e)
        {
            try
            {
                int number = Convert.ToInt32(comboBoxNumber.SelectedValue);
                int clientID = Convert.ToInt32(textBoxClientID.Text);
                //String roomType = ComboBoxRoomType.Text;
                DateTime dateIn = dateTimePickerIN.Value;
                DateTime dateo = dateTimePickerOUT.Value;

                //date in must ne = or > today date
                //date out must be = or > date in
                if(DateTime.Compare(dateIn.Date,DateTime.Now.Date) < 0)
                {
                    MessageBox.Show("the date in must be grater than today date", "date entery error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if(DateTime.Compare(dateo.Date,dateIn.Date) < 0)
                {
                    MessageBox.Show(dateo.Day + "-" + dateIn.Day);
                    MessageBox.Show("the date out must be grater than date In", "date entery error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (resrvation.addReservation(number, clientID, dateIn, dateo))
                    {
                        dataGridView1.DataSource = resrvation.getAllReserv();
                        //set the room free column to No
                        //you can add a message if the room is edited
                        room.seroomToNo(number,"No");
                        MessageBox.Show("New reservation added", "Add reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("reservation not added", "Add reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Add reservation error are you not a client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void ManageReservationsForm_Load(object sender, EventArgs e)
        {
            //fro room type compobox
            ComboBoxRoomType.DataSource = room.roomTypeList();
            ComboBoxRoomType.DisplayMember = "label";
            ComboBoxRoomType.ValueMember = "category_id";

            //for room number compobox
            int type = Convert.ToInt32(ComboBoxRoomType.SelectedValue.ToString());
            comboBoxNumber.DataSource = room.roomByType(type);
            comboBoxNumber.DisplayMember = "number";
            comboBoxNumber.ValueMember = "number";

            dataGridView1.DataSource = resrvation.getAllReserv();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxClientID.Text = "";
                textBoxReservid.Text = "";
                comboBoxNumber.SelectedIndex = 0;
                ComboBoxRoomType.SelectedIndex = 0;
                dateTimePickerIN.Value = DateTime.Now;
                dateTimePickerOUT.Value = DateTime.Now;
            }
            catch
            {

            }
        }

        private void ComboBoxRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int type = Convert.ToInt32(ComboBoxRoomType.SelectedValue.ToString());
                comboBoxNumber.DataSource = room.roomByType(type);
                comboBoxNumber.DisplayMember = "number";
                comboBoxNumber.ValueMember = "number";
            }
            catch(Exception)
            {

            }
            //for room number compobox
            

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxReservid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxClientID.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            int roomId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString());
            ComboBoxRoomType.SelectedValue = room.getroomtype(roomId);
            comboBoxNumber.SelectedValue = roomId;
            dateTimePickerIN.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[3].Value);
            dateTimePickerOUT.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[4].Value);
        }

        private void buttonEditReserv_Click(object sender, EventArgs e)
        {
            try
            {
                int number = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                int clientID = Convert.ToInt32(textBoxClientID.Text);
                int reserveID = Convert.ToInt32(textBoxReservid.Text);
                //String roomType = ComboBoxRoomType.Text;
                DateTime dateIn = dateTimePickerIN.Value;
                DateTime dateo = dateTimePickerOUT.Value;

                //date in must ne = or > today date
                //date out must be = or > date in
                if (dateIn.Day < DateTime.Now.Day)
                {
                    MessageBox.Show("the date in must be grater than today date", "date entery error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dateo.Day < dateIn.Day)
                {
                    MessageBox.Show("the date out must be grater than date In", "date entery error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (resrvation.editReservation(reserveID,number, clientID, dateIn, dateo))
                    {
                        dataGridView1.DataSource = resrvation.getAllReserv();
                        //set the room free column to No
                        //you can add a message if the room is edited
                        room.seroomToNo(number,"No");
                        MessageBox.Show("the reservation updated", "update reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show("reservation not updated", "update reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add reservation error are you not a client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRemoveReserv_Click(object sender, EventArgs e)
        {
            try
            {
                int reservId = Convert.ToInt32(textBoxReservid.Text);
                int number = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                if ( resrvation.removeReserv(reservId))
               {
                    dataGridView1.DataSource = resrvation.getAllReserv();
                    room.seroomToNo(number, "Yes");
                    MessageBox.Show("the reservation removed", "remove reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
               else
               {
                    
                    MessageBox.Show("reservation not removed", "remove reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "remove error your not a client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateTimePickerIN_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}


//the client want to check dose exits
//solve by foreignkey 
//to check reservation room date is reserved or not 
//after the user add a reservation we need to update the table rooms -> columns free , and set it to No
//check the date that not to be before today

// ->add the forgein keys for the client & the room

//ALTER TABLE rooms add CONSTRAINT fk_type_it FOREIGN KEY (type) ReFERENCES rooms_category(category_id) on UPDATE CASCADE ON DELETE CASCADE;


//ALTER TABLE reservationns add CONSTRAINT fk_room_number FOREIGN KEY (roomNumber) ReFERENCES rooms(number) on UPDATE CASCADE ON DELETE CASCADE;

/*ALTER TABLE reservationns add CONSTRAINT fk_client_id FOREIGN KEY (clientId) ReFERENCES clients(id) on UPDATE CASCADE ON DELETE CASCADE;*/

