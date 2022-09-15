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
    public partial class ClientTypeForm : Form
    {
        INTEC_AGU_OCT22Entities db = new INTEC_AGU_OCT22Entities();
        List<string> Msg = new List<string>();
        int iclientTypeId;
        string sclientTypeId = string.Empty;

        public ClientTypeForm()
        {
            InitializeComponent();
        }

        //Create
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var clientType = new ClientType();
            clientType.Name = txbName.Text;
            clientType.Description = txbDescription.Text;
            clientType.Enabled = chkEnabled.Checked;
            clientType.CreatedDate = DateTime.Now;

            db.ClientTypes.Add(clientType);
            var clientTypesSaved = db.SaveChanges() > 0;
            GetClientTypes();
        }
        //Read
        private void ClientTypeForm_Load(object sender, EventArgs e)
        {
            GetClientTypes();
        }
        private void GetClientTypes()
        {
            var clientTypes = db.ClientTypes.ToList();
            dgvClientType.DataSource = clientTypes;
        }
        //Update
        private void dgvClientType_Click(object sender, EventArgs e)
        {

            sclientTypeId = String.Empty;

            if (!string.IsNullOrEmpty(dgvClientType.SelectedCells[0].Value.ToString()))
            {
                iclientTypeId = Convert.ToInt32(dgvClientType.SelectedCells[0].Value);
                GetClientTypeById();
            }
            else
            {
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
            }
        }




        private void GetClientTypeById()
        {
            var clientType = db.ClientTypes.FirstOrDefault(x => x.Id == iclientTypeId);
            if (clientType != null)
            {
                txbName.Text = clientType.Name;
                txbDescription.Text = clientType.Description;
                chkEnabled.Checked = clientType.Enabled;
                chkEnabled.Text = clientType.Enabled ? "SI" : "NO";
                txbCreatedDate.Text = clientType.CreatedDate.ToString("MM / dd / yyyy");   

            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var update =
            from clientType in db.ClientTypes
            where clientType.Id == iclientTypeId
            select clientType;
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
            from clientType in db.ClientTypes
            where clientType.Id == iclientTypeId
            select clientType;

            foreach (var detail in delete)
            {
                db.ClientTypes.Remove(detail);

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
            Delete();
            GetClientTypes();
        }
    }
}
