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
    public partial class ContactTypeForm : Form
    {
        INTEC_AGU_OCT22Entities db = new INTEC_AGU_OCT22Entities();
        List<string> Msg = new List<string>();
        int icontactType;
        string scontactType = string.Empty;

        public ContactTypeForm()
        {
            InitializeComponent();
        }

        //Create
        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            var contactType = new ContactType();
            contactType.Name = txbName.Text;
            contactType.Description = txbDescription.Text;
            contactType.Enabled = chkEnabled.Checked;
            contactType.CreatedDate = DateTime.Now;

            db.ContactTypes.Add(contactType);
            var contactTypeSaved = db.SaveChanges() > 0;
            GetContactTypes();
        }
        //Read
        private void ContactTypeForm_Load(object sender, EventArgs e)
        {
            GetContactTypes();
        }
        private void GetContactTypes()
        {
            var contactTypes = db.ContactTypes.ToList();
            dgvContactType.DataSource = contactTypes;
        }
        //Update
        private void dgvContactType_Click(object sender, EventArgs e)
        {
            scontactType = String.Empty;

            if (!string.IsNullOrEmpty(dgvContactType.SelectedCells[0].Value.ToString()))
            {
                icontactType = Convert.ToInt32(dgvContactType.SelectedCells[0].Value);
                GetContactTypeById();
            }
            else
            {
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
            }
        }




        private void GetContactTypeById()
        {
            var contacType = db.ContactTypes.FirstOrDefault(x => x.Id == icontactType);
            if (contacType != null)
            {
                txbName.Text = contacType.Name;
                txbDescription.Text = contacType.Description;
                chkEnabled.Checked = contacType.Enabled;
                chkEnabled.Text = contacType.Enabled ? "SI" : "NO";
                txbCreatedDate.Text = contacType.CreatedDate.ToString("MM / dd / yyyy");

            }
        }


        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            var update =
            from contactType in db.ContactTypes
            where contactType.Id == icontactType
            select contactType;
            foreach (var detail in update)
            {
                detail.Name = txbName.Text;
                detail.Description = txbDescription.Text;
                detail.Enabled = chkEnabled.Checked;
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
        //Delete
        private void Delete()
        {
            var delete =
            from contactType in db.ContactTypes
            where contactType.Id == icontactType
            select contactType;

            foreach (var detail in delete)
            {
                db.ContactTypes.Remove(detail);

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

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            Delete();
            GetContactTypes();
        }

    }
}
