using E_Apartment_DataAccess.EfCore;
using E_Apartment_Logic.ApartmentLogic;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dbCore = E_Apartment_DataAccess.EfCore;

namespace E_Apartment
{
    public partial class Form1
    {      

        void OccupierAddClear()
        {
            txtOccupierName.Text = "";
            txtAlternateAddress.Text = "";
            txtContactNo.Text = "";
            txtNicPassport.Text = "";
            comBApartments.DataSource= null;
        }

        void ApartmentAddClear()
        {
            txtFlowNumber.Text = "";
            txtapartmentDescription.Text = "";
            txtapartmentDescription.Text = "";
            comBApartmentTypes.DataSource= null;
        }

        void BuildingAddClear()
        {
            txtDescription.Text = "";
            txtAddress.Text = "";
            txtName.Text = "";
            comBApartmentBuilding.DataSource= null;
        }

        void MainControlsVisibility(bool visible)
        {
            btnViewApartment.Visible = visible;
            btnViewBuilding.Visible = visible;
            btnViewOccupier.Visible = visible;
            btnLogout.Visible = visible;
            btnAddUser.Visible = visible;
            btnHome.Visible = visible;
            btnLeaseDetails.Visible = visible;
        }
        void HideAllPanel()
        {
            panelViewApartment.Visible = false;
            panelViewBuilding.Visible = false;
            panelAddOccupier.Visible = false;
            panelAddBuilding.Visible = false;
            panelViewOccupier.Visible = false;
            panelAddApartment.Visible = false;
            panelAddUsers.Visible = false;
            panelAddLease.Visible = false;
            panelLeaseDetails.Visible = false;
            panelApprovedLeaseNote.Visible = false;
            panelExtendRequest.Visible = false;
            
        }

        void ShowApartment()
        {
            panelViewBuilding.Visible = false;
            panelViewOccupier.Visible = false;
            panelViewApartment.Visible = true;
            panelAddBuilding.Visible = false;
            panelAddOccupier.Visible = false;
            panelAddApartment.Visible = false;
            panelAddUsers.Visible = false;
            panelHome.Visible = false;
            panelAddLease.Visible = false;
            panelLeaseDetails.Visible = false;
            panelExtendRequest.Visible = false;
            panelApprovedLeaseNote.Visible = false;

            dgvApartment.DataSource = apartmentLogic.ListApartment();
        }

        async void ShowHome()
        {
            panelViewBuilding.Visible = false;
            panelViewOccupier.Visible = false;
            panelViewApartment.Visible = false;
            panelAddBuilding.Visible = false;
            panelAddOccupier.Visible = false;
            panelAddApartment.Visible = false;
            panelAddUsers.Visible = false;
            panelHome.Visible = true;
            panelAddLease.Visible = false;
            panelLeaseDetails.Visible = false;
            panelApprovedLeaseNote.Visible = false;
            panelExtendRequest.Visible = false;

            dgvNotApprovedLease.DataSource = await leaseLogic.FindAllNotApprovedLeaseNotes();
            lblAwailableApartmentCount.Text = apartmentLogic.AvailableApartmentCount().ToString();

            lblApartmentCount.Text = apartmentLogic.GetCount().ToString();
            lblBuildingCount.Text = buildingLogic.GetCount().ToString();
            lblOccupierCount.Text = occupierLogic.getCount().ToString();
        }

        void ShowBuilding()
        {
            panelViewApartment.Visible = false;
            panelViewOccupier.Visible = false;
            panelViewBuilding.Visible = true;
            panelAddBuilding.Visible = false;
            panelAddOccupier.Visible = false;
            panelAddApartment.Visible = false;
            panelAddUsers.Visible = false;
            panelHome.Visible = false;
            panelAddLease.Visible = false;
            panelApprovedLeaseNote.Visible = false;
            panelLeaseDetails.Visible = false;
            panelExtendRequest.Visible = false;
        }

        void ShowOccupier()
        {
            panelViewApartment.Visible = false;
            panelViewBuilding.Visible = false;
            panelViewOccupier.Visible = true;
            panelAddBuilding.Visible = false;
            panelAddOccupier.Visible = false;
            panelAddApartment.Visible = false;
            panelAddUsers.Visible = false;
            panelHome.Visible = false;
            panelAddLease.Visible = false;
            panelLeaseDetails.Visible = false;
            panelApprovedLeaseNote.Visible = false;
            panelExtendRequest.Visible = false;
        }

        void ShowAddBuilding()
        {
            panelViewApartment.Visible = false;
            panelViewBuilding.Visible = false;
            panelViewOccupier.Visible = false;
            panelAddBuilding.Visible = true;
            panelAddOccupier.Visible = false;
            panelAddApartment.Visible = false;
            panelAddUsers.Visible = false;
            panelHome.Visible = false;
            panelAddLease.Visible = false;
            panelApprovedLeaseNote.Visible = false;
            panelLeaseDetails.Visible = false;
            panelExtendRequest.Visible = false;

            var apartmentTypes = apartmentLogic.ListApartment();

            comBApartmentBuilding.DataSource = apartmentTypes;
            comBApartments.DisplayMember = "Code";
            comBApartments.ValueMember = "Id";
        }

        void ShowAddOccupier()
        {
            panelViewApartment.Visible = false;
            panelViewBuilding.Visible = false;
            panelViewOccupier.Visible = false;
            panelAddOccupier.Visible = true;
            panelAddBuilding.Visible = false;
            panelAddApartment.Visible = false;
            panelAddUsers.Visible = false;
            panelHome.Visible = false;
            panelApprovedLeaseNote.Visible = false;
            panelAddLease.Visible = false;
            panelLeaseDetails.Visible = false;
            panelExtendRequest.Visible = false;

            var apartmentTypes = apartmentLogic.ListApartment();

            comBApartments.DataSource = apartmentTypes;
            comBApartments.DisplayMember = "Code";
            comBApartments.ValueMember = "Id";

        }

        void ShowAddApartment()
        {
            panelViewApartment.Visible = false;
            panelViewBuilding.Visible = false;
            panelAddOccupier.Visible = false;
            panelAddBuilding.Visible = false;
            panelViewOccupier.Visible = false;
            panelAddApartment.Visible = true;
            panelAddUsers.Visible = false;
            panelApprovedLeaseNote.Visible = false;
            panelHome.Visible = false;
            panelAddLease.Visible = false;
            panelExtendRequest.Visible = false;
            panelLeaseDetails.Visible = false;

            var result = apartmentLogic.GetAllApartmentTypes();

            comBApartmentTypes.DataSource = result;
            comBApartmentTypes.DisplayMember = "Name";
            comBApartmentTypes.ValueMember = "Id";

            var statuses = apartmentLogic.GetAllStauses();
            comBApartmentStatus.DataSource = statuses;
            comBApartmentStatus.DisplayMember = "Name";
            comBApartmentStatus.ValueMember = "Id";
        }

        async Task ShowAddUsersAsync()
        {
            panelViewApartment.Visible = false;
            panelViewBuilding.Visible = false;
            panelAddOccupier.Visible = false;
            panelAddBuilding.Visible = false;
            panelViewOccupier.Visible = false;
            panelAddApartment.Visible = false;
            panelAddUsers.Visible = true;
            panelHome.Visible = false;
            panelAddLease.Visible = false;
            panelApprovedLeaseNote.Visible = false;
            panelLeaseDetails.Visible = false;
            panelExtendRequest.Visible = false;

            var userTypes = await loginLogic.GetAllTypes();

            comBUserType.DataSource= userTypes;
            comBUserType.DisplayMember = "Name";
            comBUserType.ValueMember = "Id";
        }

        void ShowViewLease()
        {
            panelViewApartment.Visible = false;
            panelViewBuilding.Visible = false;
            panelAddOccupier.Visible = false;
            panelAddBuilding.Visible = false;
            panelViewOccupier.Visible = false;
            panelApprovedLeaseNote.Visible = false;
            panelAddApartment.Visible = false;
            panelAddUsers.Visible = false;
            panelHome.Visible = false;
            panelAddLease.Visible = false;
            panelLeaseDetails.Visible = true;
            panelExtendRequest.Visible = false;

            dgvLeaseDetails.DataSource = leaseLogic.FindApprovedLeaseNotes();

        }

        async void ShowAddLease()
        {
            panelViewApartment.Visible = false;
            panelViewBuilding.Visible = false;
            panelAddOccupier.Visible = false;
            panelAddBuilding.Visible = false;
            panelViewOccupier.Visible = false;
            panelAddApartment.Visible = false;
            panelAddUsers.Visible = false;
            panelHome.Visible = false;
            panelApprovedLeaseNote.Visible = false;
            panelAddLease.Visible = true;
            panelLeaseDetails.Visible = false;
            panelExtendRequest.Visible = false;

            var result = apartmentLogic.ListApartment();

            comBLeaseApartment.DataSource = result;
            comBLeaseApartment.DisplayMember = "Code";
            comBLeaseApartment.ValueMember = "Id";

            var occupiers = await occupierLogic.GetOccupiers();

            comBLeaseOccupier.DisplayMember = "Code";
            comBLeaseOccupier.ValueMember = "Id";
        }

        void ShowExtendRequest()
        {
            panelViewApartment.Visible = false;
            panelViewBuilding.Visible = false;
            panelAddOccupier.Visible = false;
            panelAddBuilding.Visible = false;
            panelViewOccupier.Visible = false;
            panelAddApartment.Visible = false;
            panelAddUsers.Visible = false;
            panelHome.Visible = false;
            panelApprovedLeaseNote.Visible = false;
            panelAddLease.Visible = false;
            panelLeaseDetails.Visible = false;
            panelExtendRequest.Visible = true;

            dgvExtendRequest.DataSource = leaseLogic.FindAllExtendRequest();
            dgvApprovedExtendRequest.DataSource = leaseLogic.FindAllExtendedLease();


        }
        void ClearAddLease()
        {
            comBLeaseOccupier.DataSource= null;
            comBLeaseOccupier.DataSource= null;
            fromDateLease.Value = DateTime.Now;
            toDateLease.Value = DateTime.Now;
            txtMonthlyFee.Text = "";            
        }

        void ShowApprovedLeasePanel()
        {
            panelViewApartment.Visible = false;
            panelViewBuilding.Visible = false;
            panelAddOccupier.Visible = false;
            panelAddBuilding.Visible = false;
            panelViewOccupier.Visible = false;
            panelAddApartment.Visible = false;
            panelAddUsers.Visible = false;
            panelHome.Visible = false;
            panelAddLease.Visible = false;
            panelApprovedLeaseNote.Visible = true;
            panelLeaseDetails.Visible = false;
        }

        OccupierDetail CollectOccupierDetails()
        {
            var occupier = new OccupierDetail()
            {
                Name = txtOccupierName.Text,
                AlternateAddress = txtAlternateAddress.Text,
                ContactNo = txtContactNo.Text,
                NicOrPassportNo = txtNicPassport.Text,
                IsIncludeServant = comBIsServantInclude.Text == "Yes" ? true : false,
                ApartmentId = comBApartments.Text != ""? Guid.Parse(comBApartments.SelectedValue.ToString()) : null                
            };

            return occupier;
        }

        Building CollectBuildingDetail()
        {
            var building = new dbCore.Building()
            {
                Description = txtDescription.Text,
                Address = txtAddress.Text,
                IsDelete = false,
                Name = txtName.Text
            };

            return building;
        }

        Apartment CollectApartmentDetails()
        {
            var apartment = new dbCore.Apartment()
            {
                Description = txtDescription.Text,
                FlowNo = txtFlowNumber.Text,
                TypeId = Convert.ToInt16(comBApartmentTypes.SelectedValue.ToString()),
                StatusId = Convert.ToInt16(comBApartmentStatus.SelectedValue.ToString())
                
            };

            return apartment;
        }

        LeaseDetail CollectLeaseDetails()
        {
            return new dbCore.LeaseDetail()
            {
                MonthlyFee = Convert.ToInt16(txtMonthlyFee.Text),
                FromDate = fromDateLease.Value,
                ToDate= toDateLease.Value,
                ApartmentId = Guid.Parse(comBLeaseApartment.SelectedValue.ToString()),
                OccupierId = Guid.Parse(comBLeaseOccupier.SelectedValue.ToString())
            };
        }

        

        User CollectAddUser()
        {
            return new dbCore.User()
            {
                Username = txtAddUsername.Text,
                Password = txtAddPassword.Text,
                TypeId = Convert.ToInt16(comBUserType.SelectedValue.ToString())
            };
        }

        void ClearUserAdd()
        {
            txtAddUsername.Clear();
            txtAddPassword.Clear();
            comBUserType.DataSource = null;     
            
        }
    }
}
