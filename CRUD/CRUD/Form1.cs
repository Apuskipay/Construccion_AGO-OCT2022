using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public partial class CRUDForm : Form
    {
        INTEC_AGU_OCT22Entities db = new INTEC_AGU_OCT22Entities();
        List<string> Msg = new List<string>();
        string PeopleId = String.Empty;
        public CRUDForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetContactTypes();
            GetClientTypes();
            GetPermissions();
            GetRestrictions();
            GetPeoples();
        }

        private void GetPeoples()
        {
            var peoples = db.People.ToList();
            dgvPeople.DataSource = peoples;
        }

        private void GetRestrictions()
        {
            var restrictions = db.Restrictions.ToList();
            foreach (var item in restrictions)
            {
                cbListRestrictions.Items.Add(item.Name);
            }
        }

        private void GetPermissions()
        {
            var permissions = db.Permissions.ToList();
            foreach (var item in permissions)
            {
                cbListPermissions.Items.Add(item.Name);
            }
            
        }

        private void GetClientTypes()
        {
            var clientTypes = db.ClientTypes.ToList();
            cbClientType.DataSource = clientTypes;
            cbClientType.DisplayMember = "Name";
            cbClientType.ValueMember = "Id";
        }

        private void GetContactTypes()
        {
            var contactTypes = db.ContactTypes.ToList();
            cbContactType.DataSource = contactTypes;
            cbContactType.DisplayMember = "Name";
            cbContactType.ValueMember = "Id";

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            btnSave.Enabled = true;
            btnSave.ForeColor = Color.Green;
            btnCancel.Enabled = true;
        }

        private void SaveForm()
        {
            var people = new Person();
            people.Id = Guid.NewGuid().ToString();
            people.FirstName = txtFirstName.Text;
            people.MiddleName = txtMiddleName.Text;
            people.LastName = txtLastName.Text;
            people.ClientTypeId = Convert.ToInt32(cbClientType.SelectedValue);

            if(cbContactType.SelectedIndex != 0)
            {
                people.ContactTypeId = Convert.ToInt32(cbClientType.SelectedValue);
            }

            people.SupportStaff = chkSupportStaff.Checked;
            people.PhoneNumber = txtPhoneNumber.Text;
            people.EmailAddress = txtEmailAddress.Text;
            people.Enabled = true;
            people.CreatedDate = DateTime.Now;

            db.People.Add(people);
            
            var peopleSaved = db.SaveChanges() > 0;

            if (peopleSaved)
            {
                var user = new User();
                user.Id = Guid.NewGuid().ToString();
                user.Username = txtUserName.Text;
                user.Password = txtPassword.Text;
                user.LicenseTypeId = Convert.ToInt32(cbLicenseType.SelectedValue);
                user.PeopleId = people.Id;
                user.Enabled = true;
                user.CreatedDate = DateTime.Now;

                db.Users.Add(user);
                var userSaved = db.SaveChanges() > 0;

                if (userSaved)
                {
                    MessageBox.Show("The people has beeen added.");

                    GetPeoples();
                    DefaultControls();

                    btnAdd.Enabled = true;
                    btnSave.Enabled = false;
                    btnSave.ForeColor = Color.Black;
                    btnCancel.Enabled = false;
                }
            }
        }

        private void DefaultControls()
        {
            txtFirstName.Text = string.Empty;
            txtMiddleName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            cbClientType.SelectedIndex = 0;
            cbContactType.SelectedIndex = 0;
            chkSupportStaff.Checked = false;
            txtPhoneNumber.Text = string.Empty;
            txtEmailAddress.Text = string.Empty;
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            cbLicenseType.SelectedIndex = 0;

        }

        private bool ValidateForm()
        {
            Msg = new List<string>();

            lblFirstName.Visible = false;
            lblLastName.Visible = false;
            lblPhoneNumber.Visible = false;
            lblUsername.Visible = false;
            lblPassword.Visible = false;


            bool result = true;

            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                Msg.Add("The field (First Name) is required.");
                lblFirstName.Visible = true;
                result = false;
            }
            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                Msg.Add("The field (Last Name) is required.");
                lblLastName.Visible = true;
                result = false;
            }
            if (cbClientType.SelectedIndex == 0)
            {
                Msg.Add("The field (Client Type) is required.");
                result = false;
            }
            if (string.IsNullOrEmpty(txtPhoneNumber.Text))
            {
                Msg.Add("The field (Phone Number) is required.");
                lblPhoneNumber.Visible = true;
                result = false;
            }
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                Msg.Add("The field (Username) is required.");
                lblUsername.Visible = true;
                result = false;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                Msg.Add("The field (Password) is required.");
                lblPassword.Visible = true;
                result = false;
            }
            return result;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                SaveForm();
            }
            else
            {
                string errors = string.Empty;
                int errorIndex = 1;
                foreach (var item in Msg)
                {
                    errors += $"{item.ToString()}\n";
                    errorIndex++;
                }

                MessageBox.Show(errors, "ERRORS", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GetPeoples();
            DefaultControls();

            btnAdd.Enabled = true;
            btnSave.Enabled = false;
            btnSave.ForeColor = Color.Black;
            btnCancel.Enabled = false;
        }

        private void dgvPeople_Click(object sender, EventArgs e)
        {
            PeopleId = String.Empty;

            if (!string.IsNullOrEmpty(dgvPeople.SelectedRows[0].Cells["Id"].Value.ToString()))
            {
                PeopleId = dgvPeople.SelectedRows[0].Cells["Id"].Value.ToString();
                btnUpdate.Visible = true;
                btnDelete.Visible = true;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                GetPeopleById(PeopleId);
            }
            else
            {
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
            }

            
        }

        private void GetPeopleById(string peopleId)
        {
            DefaultControls();

            var people = db.People.FirstOrDefault(x => x.Id == PeopleId);
            if (people != null)
            {
                txtFirstName.Text = people.FirstName;
                txtMiddleName.Text = people.MiddleName;
                txtLastName.Text = people.LastName;
                chkSupportStaff.Checked = people.SupportStaff;
                chkSupportStaff.Text = people.SupportStaff ? "SI" : "NO";
                txtPhoneNumber.Text = people.PhoneNumber;
                txtEmailAddress.Text = people.EmailAddress;
                txtCreatedDate.Text = people.CreatedDate.ToString("MM/dd/yyyy");

                var user = db.Users.FirstOrDefault(x => x.PeopleId == peopleId);
                if (user != null)
                {
                    txtUserName.Text = user.Username;
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;

            var update =
            from people in db.People
            where people.Id == PeopleId
            select people;
            foreach (var detail in update)
            {
                detail.Id = Guid.NewGuid().ToString();
                detail.FirstName = txtFirstName.Text;
                detail.MiddleName = txtMiddleName.Text;
                detail.LastName = txtLastName.Text;
                detail.ClientTypeId = Convert.ToInt32(cbClientType.SelectedValue);
                detail.SupportStaff = chkSupportStaff.Checked;
                detail.PhoneNumber = txtPhoneNumber.Text;
                detail.EmailAddress = txtEmailAddress.Text;
                detail.Enabled = true;
                detail.CreatedDate = DateTime.Now;

            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception a)
            {
                Console.WriteLine(a);
            }
        }
        //

        private void Delete(string peopleId)
        {
            var deleteOrderDetails =
                from details in db.People
                where details.Id == peopleId
                select details;

            foreach (var detail in deleteOrderDetails)
            {
                db.People.Remove(detail);

            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // Provide for exceptions.
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete(PeopleId);
            GetPeoples();
        }

        //ClientTypesForm
        private void button4_Click(object sender, EventArgs e)
        {
           ClientTypeForm clientTypeForm = new ClientTypeForm();
            clientTypeForm.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
           ContactTypeForm contactTypeForm = new ContactTypeForm();
            contactTypeForm.Show();
        }
    }
}
