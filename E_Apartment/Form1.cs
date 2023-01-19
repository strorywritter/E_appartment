using E_Apartment_DataAccess.EfCore;
using E_Apartment_Logic.ApartmentLogic;
using E_Apartment_Logic.BuildingLogic;
using E_Apartment_Logic.LeaseLogic;
using E_Apartment_Logic.Logic;
using E_Apartment_Logic.Occupier;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using dbCore = E_Apartment_DataAccess.EfCore;

namespace E_Apartment
{
    public partial class Form1 : Form
    {
        protected readonly IApartmentLogic apartmentLogic;
        protected readonly IBuildingLogic buildingLogic;
        protected readonly IOccupierLogic occupierLogic;
        protected readonly ILoginLogic loginLogic;
        protected readonly ILeaseLogic leaseLogic;

        private Guid? OccupierDetailId;
        private Guid? ApartmentId;
        private Guid? BuildingId;
        private Guid? LeaseDetailId;
        private Guid? NotApprovedLeaseDetailId;

        //this id represent leaseDetailId
        private Guid? ExtentionLeaseId;

        private LeaseExtension extension;

        DateTime selectedExtendDate;

        /*
         * user types
         * 1 Admin  
         * 2 Customer
         * */



        public Form1(IApartmentLogic apartmentLogic, IBuildingLogic buildingLogic, IOccupierLogic occupierLogic,
            ILoginLogic loginLogic, ILeaseLogic leaseLogic)
        {
            InitializeComponent();
            this.apartmentLogic = apartmentLogic;
            this.buildingLogic = buildingLogic;
            this.occupierLogic = occupierLogic;
            this.loginLogic = loginLogic;
            this.leaseLogic = leaseLogic;

            comBIsServantInclude.Items.Add("Yes");
            comBIsServantInclude.Items.Add("No");

            HideAllPanel();
            panelLogin.Visible = true;
            panelLogin.BringToFront();

            lblApartmentCount.Text = apartmentLogic.GetCount().ToString();
            lblBuildingCount.Text = buildingLogic.GetCount().ToString();
            lblOccupierCount.Text = occupierLogic.getCount().ToString();

            MainControlsVisibility(false);
        }
        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var user = await loginLogic.FindByUserName(txtUsername.Text);
                if (user != null)
                {
                    if (user.Password == txtPassword.Text.Trim())
                    {
                        panelLogin.Visible = false;
                        MainControlsVisibility(true);
                        txtUsername.Text = "";
                        txtPassword.Text = "";

                        if (user.TypeId == 2)
                        {
                            btnViewApartment.Visible = false;
                            btnViewOccupier.Visible = false;
                            btnAddUser.Visible = false;
                            btnHome.Visible = false;
                        }

                        lblApartmentCount.Text = apartmentLogic.GetCount().ToString();
                        lblBuildingCount.Text = buildingLogic.GetCount().ToString();
                        lblOccupierCount.Text = occupierLogic.getCount().ToString();
                        dgvNotApprovedLease.DataSource = leaseLogic.FindAllNotApprovedLeaseNotes();
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Password...!");
                    }
                }
                else
                {
                    MessageBox.Show("Can not find user. Faild Login");
                }
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Db Update Error");
            }
        }

        private void btnViewApartment_Click(object sender, EventArgs e)
        {
            ShowApartment();
        }

        private async void btnViewBuilding_Click(object sender, EventArgs e)
        {
            ShowBuilding();
            dgvBuilding.DataSource = await buildingLogic.GetBuildings();            
        }

        private void btnViewOccupier_Click(object sender, EventArgs e)
        {
            ShowOccupier();
            dgvOccupier.DataSource = occupierLogic.GetOccupiers();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.BuildingId = null;
            ShowAddBuilding();
        }

        private async void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (this.BuildingId != null)
            {
                try
                {
                    await buildingLogic.Update(this.BuildingId, CollectBuildingDetail());
                    MessageBox.Show("Update Succeed...!");BuildingAddClear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Save Failed...!");
                }
            }
            else
            {
                try
                {
                    buildingLogic.Add(CollectBuildingDetail());
                    MessageBox.Show("Update Succeed...!");BuildingAddClear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Save Failed...!");
                }
            }

            BuildingAddClear();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            ShowBuilding();
        }

        //add occupier
        private void btnAddOccupier_Click(object sender, EventArgs e)
        {
            this.OccupierDetailId = null;
            ShowAddOccupier();
            OccupierAddClear();            
        }

        private void btnBackOccupier_Click(object sender, EventArgs e)
        {
            ShowOccupier();
        }

        //add apartment
        private void btnAddApartment_Click(object sender, EventArgs e)
        {
            this.ApartmentId = null;
            ShowAddApartment();
        }

        private void btnAddApartmentBack_Click(object sender, EventArgs e)
        {
            ShowApartment();
        }        

        private async void btnOccupierSave_Click(object sender, EventArgs e)
        {          
            if(this.OccupierDetailId != null)
            {
                try
                {
                    await occupierLogic.Update(this.OccupierDetailId, CollectOccupierDetails());
                    MessageBox.Show("Update Succeed...!"); OccupierAddClear();
                }
                catch(DbUpdateException ex)
                {
                    MessageBox.Show("Update Failed...!");
                }
            }
            else
            {
                try
                {
                    await occupierLogic.Add(CollectOccupierDetails());
                    MessageBox.Show("Save Succeed...!"); OccupierAddClear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Save Failed...!");
                }
            }

            
        }

        private async void btnApartmentSave_Click(object sender, EventArgs e)
        {
            if (this.ApartmentId != null)
            {
                try
                {
                    await apartmentLogic.Update(this.ApartmentId, CollectApartmentDetails());
                    MessageBox.Show("Update Succeed...!"); ApartmentAddClear();
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show("Update Failed...!");
                }
            }
            else
            {
                try
                {
                    await apartmentLogic.AddApartment(CollectApartmentDetails());
                    MessageBox.Show("Save Succeed...!"); ApartmentAddClear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Save Failed...!");
                }
            }
        }

        private void dgvOccupier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var search = dgvOccupier.Rows[e.RowIndex];
            this.OccupierDetailId = Guid.Parse(search.Cells[0].Value.ToString());
            txtOccupierName.Text = search.Cells[2].Value.ToString();
            txtAlternateAddress.Text = search.Cells[3].Value.ToString();
            txtContactNo.Text = search.Cells[4].Value.ToString();
            txtNicPassport.Text = search.Cells[5].Value.ToString();
            comBIsServantInclude.Text = search.Cells[6].Value.ToString();
            comBApartments.Text = search.Cells[7].Value?.ToString();
        }

        private async void btnOccupierUpdate_Click(object sender, EventArgs e)
        {
            ShowAddOccupier();
        }

        private void btnUpdateBuildingDetail_Click(object sender, EventArgs e)
        {
            ShowAddBuilding();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowAddApartment();
        }

        private void txtFilterApartment_TextChanged(object sender, EventArgs e)
        {
            var search = apartmentLogic.FilterByCode(txtFilterApartment.Text);
            dgvApartment.DataSource = search;
        }

        private void txtFilterBuilding_TextChanged(object sender, EventArgs e)
        {
            var search = buildingLogic.FilterByCode(txtFilterBuilding.Text);
            dgvBuilding.DataSource = search;
        }

        private void txtFilterOccupier_TextChanged(object sender, EventArgs e)
        {
            var search = occupierLogic.FilterByCode(txtFilterOccupier.Text);
            dgvBuilding.DataSource = search;
        }

        //add user panel
        private async void btnAddUser_Click(object sender, EventArgs e)
        {
            await ShowAddUsersAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            try
            {
                loginLogic.Add(CollectAddUser());
                MessageBox.Show("Save Succeed...!"); ClearUserAdd();
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Save Failed...!");
            }

            ClearUserAdd();
        }

        private void dgvApartment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var search = dgvApartment.Rows[e.RowIndex];
            this.ApartmentId = Guid.Parse(search.Cells[0].Value.ToString());
            txtFlowNumber.Text = search.Cells[3].Value.ToString();
            comBApartmentTypes.Text = search.Cells[7].ToString();
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            HideAllPanel();            
            panelLogin.Visible = true;
            MainControlsVisibility(false);
        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            ShowHome();           
        }

        private void btnAddLease_Click(object sender, EventArgs e)
        {
            this.LeaseDetailId = null;
            ClearAddLease();
            ShowAddLease();
        }

        private void btnUpdateLease_Click(object sender, EventArgs e)
        {
            ShowAddLease();
        }

        private async void dgvLeaseDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var result = dgvLeaseDetails.Rows[e.RowIndex];
            this.LeaseDetailId = Guid.Parse(result.Cells[0].Value.ToString());
            txtMonthlyFee.Text = result.Cells[1].Value.ToString();
            fromDateLease.Value = (DateTime)result.Cells[2].Value;
            toDateLease.Value = (DateTime)result.Cells[3].Value;
            comBLeaseApartment.Text = result.Cells[5].Value.ToString();
            comBLeaseOccupier.Text = result.Cells[6].Value.ToString();

            this.extension = new dbCore.LeaseExtension()
            {
                FromDate = (DateTime)result.Cells[2].Value,
                IsDelete = false,
                LeaseDetailsId = this.LeaseDetailId,
                LeaseStatusId = 2
            };
        }

        private async void btnLeaseSave_Click(object sender, EventArgs e)
        {
            if (this.ApartmentId != null)
            {
                try
                {
                    await leaseLogic.Update(this.LeaseDetailId, CollectLeaseDetails());
                    MessageBox.Show("Update Succeed...!"); 
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show("Update Failed...!");
                }
            }
            else
            {
                try
                {
                    await leaseLogic.AddAsync(CollectLeaseDetails());
                    MessageBox.Show("Save Succeed...!"); ApartmentAddClear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Save Failed...!");
                }
            }
        }

        private void btnLeaseDetails_Click(object sender, EventArgs e)
        {
            ShowViewLease();
        }

        private async void fromDateLease_ValueChanged(object sender, EventArgs e)
        {
            dgvLeaseDetails.DataSource = leaseLogic.FilterByDate(fromDateLease.Value, toDateLease.Value);
        }

        private void toDateLease_ValueChanged(object sender, EventArgs e)
        {
            dgvLeaseDetails.DataSource = leaseLogic.FilterByDate(fromDateLease.Value, toDateLease.Value);
        }

        private void dgvNotApprovedLease_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var search = dgvNotApprovedLease.Rows[e.RowIndex];
            this.NotApprovedLeaseDetailId = Guid.Parse(search.Cells[0].Value.ToString());
            lblSelectedLeaseId.Text = search.Cells[0].Value.ToString();
        }

        private async void btnApproveLease_Click(object sender, EventArgs e)
        {
            await leaseLogic.ApproveLeaseNote(this.NotApprovedLeaseDetailId);
            dgvNotApprovedLease.DataSource = leaseLogic.FindAllNotApprovedLeaseNotes();
        }

        private void button4_Click_2(object sender, EventArgs e)
        {

            ShowApprovedLeasePanel();

            dgvApprovedLeaseNote.DataSource = leaseLogic.FindApprovedLeaseNotes();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Create a new instance of the DialogBox
            Form dialog = new Form();
            dialog.Text = "Select Date to extend your lease agrement...!";
            dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
            dialog.StartPosition = FormStartPosition.CenterScreen;
            dialog.Width = 300;
            dialog.Height = 150;
            dialog.MaximizeBox = false;
            dialog.MinimizeBox = false;

            // Add the DateTimePicker control to the DialogBox
            DateTimePicker ExtendDate = new DateTimePicker();
            ExtendDate.Format = DateTimePickerFormat.Short;
            ExtendDate.Location = new Point(10, 10);
            dialog.Controls.Add(ExtendDate);

            // Add an OK button to the DialogBox
            Button okButton = new Button();
            okButton.Text = "OK";
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(dialog.Width - 100, ExtendDate.Bottom + 10);
            dialog.Controls.Add(okButton);

            // Show the DialogBox and get the result
            DialogResult result = dialog.ShowDialog();

            // Check if the user clicked OK
            if (result == DialogResult.OK)
            {
                // Get the selected date from the DateTimePicker
                selectedExtendDate = ExtendDate.Value;
                extension.ToDate= selectedExtendDate;

                leaseLogic.AddNewLeaseExtension(extension);
                // Use the selected date
                //...
            }
            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ShowExtendRequest();
        }

        private async void btnApproveExtendRequest_Click(object sender, EventArgs e)
        {
            try
            {
                await leaseLogic.UpdateExtentionStatusAsync(this.ExtentionLeaseId);
                dgvExtendRequest.DataSource = leaseLogic.FindAllExtendRequest();
                dgvApprovedExtendRequest.DataSource = leaseLogic.FindAllExtendedLease();
            }catch(Exception ex)
            {
                MessageBox.Show("Extension Failed");
            }
        }

        private void dgvExtendRequest_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var result = dgvExtendRequest.Rows[e.RowIndex];
            this.ExtentionLeaseId = Guid.Parse(result.Cells[4].Value.ToString());
        }

        private void dgvApprovedExtendRequest_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}