using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VehicleTradingSystem.Models;
using VehicleTradingSystem.Services;

namespace VehicleTradingSystem.Forms
{
    public partial class DashboardForm : Form
    {
        private readonly Customer _currentCustomer;
        private readonly VehicleService _vehicleService;
        private MenuStrip menuStrip;
        private StatusStrip statusStrip;
        private TabControl mainTabControl;

        // Tab Pages
        private TabPage tabHome;
        private TabPage tabVehicles;
        private TabPage tabCustomers;
        private TabPage tabReports;

        // Vehicle Tab Controls
        private DataGridView dgvVehicles;
        private Button btnAddVehicle;
        private Button btnEditVehicle;
        private Button btnDeleteVehicle;
        private Button btnRefreshVehicles;
        private TextBox txtSearchVehicles;
        private Button btnSearchVehicles;
        private Label lblTotalVehicles;

        public DashboardForm(Customer customer)
        {
            _currentCustomer = customer;
            _vehicleService = new VehicleService();
            InitializeDashboard();
            LoadData();
        }

        private void InitializeDashboard()
        {
            // Main Form Settings
            this.Text = $"Dashboard - {_currentCustomer.Username}";
            this.ClientSize = new Size(1200, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;

            // Create Menu System
            CreateMenuStrip();

            // Create Status Bar
            CreateStatusStrip();

            // Create Main Tab Control with Navigation Tabs
            CreateTabControl();

            // Add controls to form
            this.Controls.Add(mainTabControl);
            this.Controls.Add(menuStrip);
            this.Controls.Add(statusStrip);
            this.MainMenuStrip = menuStrip;
        }

        private void CreateMenuStrip()
        {
            menuStrip = new MenuStrip();

            // File Menu
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");
            fileMenu.DropDownItems.Add("Dashboard", null, (s, e) => mainTabControl.SelectedTab = tabHome);
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add("Logout", null, LogoutItem_Click);

            // View Menu
            ToolStripMenuItem viewMenu = new ToolStripMenuItem("View");
            viewMenu.DropDownItems.Add("Vehicles", null, (s, e) => mainTabControl.SelectedTab = tabVehicles);
            viewMenu.DropDownItems.Add("Customers", null, (s, e) => mainTabControl.SelectedTab = tabCustomers);

            // Vehicle Menu
            ToolStripMenuItem vehicleMenu = new ToolStripMenuItem("Vehicles");
            vehicleMenu.DropDownItems.Add("Add New Vehicle", null, BtnAddVehicle_Click);
            vehicleMenu.DropDownItems.Add("Refresh List", null, BtnRefreshVehicles_Click);

            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(viewMenu);
            menuStrip.Items.Add(vehicleMenu);
        }

        private void CreateStatusStrip()
        {
            statusStrip = new StatusStrip();
            statusStrip.Items.Add($"Logged in as: {_currentCustomer.Username}");
            statusStrip.Items.Add($"Last login: {DateTime.Now.ToString("g")}");
        }

        private void CreateTabControl()
        {
            mainTabControl = new TabControl();
            mainTabControl.Dock = DockStyle.Fill;
            mainTabControl.Appearance = TabAppearance.Normal;
            mainTabControl.ItemSize = new Size(120, 30);

            // Create and add all tab pages
            tabHome = CreateHomeTab();
            tabVehicles = CreateVehiclesTab();
            tabCustomers = CreateCustomersTab();
            tabReports = CreateReportsTab();

            mainTabControl.Controls.Add(tabHome);
            mainTabControl.Controls.Add(tabVehicles);
            mainTabControl.Controls.Add(tabCustomers);
            mainTabControl.Controls.Add(tabReports);
        }

        private TabPage CreateHomeTab()
        {
            TabPage tab = new TabPage("Dashboard");

            Label lblWelcome = new Label();
            lblWelcome.Text = $"Welcome, {_currentCustomer.FirstName}!";
            lblWelcome.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblWelcome.Location = new Point(50, 50);
            lblWelcome.AutoSize = true;

            Label lblSystemInfo = new Label();
            lblSystemInfo.Text = "Vehicle Trading System";
            lblSystemInfo.Font = new Font("Segoe UI", 12);
            lblSystemInfo.Location = new Point(50, 100);
            lblSystemInfo.AutoSize = true;

            tab.Controls.Add(lblWelcome);
            tab.Controls.Add(lblSystemInfo);
            return tab;
        }

        private TabPage CreateVehiclesTab()
        {
            TabPage tab = new TabPage("Vehicles");

            // Create toolbar panel
            Panel toolbarPanel = new Panel();
            toolbarPanel.Height = 80;
            toolbarPanel.Dock = DockStyle.Top;
            toolbarPanel.BorderStyle = BorderStyle.FixedSingle;

            // Search controls
            Label lblSearch = new Label();
            lblSearch.Text = "Search:";
            lblSearch.Location = new Point(10, 15);
            lblSearch.AutoSize = true;

            txtSearchVehicles = new TextBox();
            txtSearchVehicles.Location = new Point(60, 12);
            txtSearchVehicles.Size = new Size(200, 23);
            txtSearchVehicles.KeyPress += TxtSearchVehicles_KeyPress;

            btnSearchVehicles = new Button();
            btnSearchVehicles.Text = "Search";
            btnSearchVehicles.Location = new Point(270, 11);
            btnSearchVehicles.Size = new Size(75, 25);
            btnSearchVehicles.Click += BtnSearchVehicles_Click;

            btnRefreshVehicles = new Button();
            btnRefreshVehicles.Text = "Refresh";
            btnRefreshVehicles.Location = new Point(355, 11);
            btnRefreshVehicles.Size = new Size(75, 25);
            btnRefreshVehicles.Click += BtnRefreshVehicles_Click;

            // Action buttons
            btnAddVehicle = new Button();
            btnAddVehicle.Text = "Add Vehicle";
            btnAddVehicle.Location = new Point(10, 45);
            btnAddVehicle.Size = new Size(100, 30);
            btnAddVehicle.BackColor = Color.Green;
            btnAddVehicle.ForeColor = Color.White;
            btnAddVehicle.Click += BtnAddVehicle_Click;

            btnEditVehicle = new Button();
            btnEditVehicle.Text = "Edit Vehicle";
            btnEditVehicle.Location = new Point(120, 45);
            btnEditVehicle.Size = new Size(100, 30);
            btnEditVehicle.BackColor = Color.Orange;
            btnEditVehicle.ForeColor = Color.White;
            btnEditVehicle.Click += BtnEditVehicle_Click;
            btnEditVehicle.Enabled = false;

            btnDeleteVehicle = new Button();
            btnDeleteVehicle.Text = "Delete Vehicle";
            btnDeleteVehicle.Location = new Point(230, 45);
            btnDeleteVehicle.Size = new Size(100, 30);
            btnDeleteVehicle.BackColor = Color.Red;
            btnDeleteVehicle.ForeColor = Color.White;
            btnDeleteVehicle.Click += BtnDeleteVehicle_Click;
            btnDeleteVehicle.Enabled = false;

            // Total vehicles label
            lblTotalVehicles = new Label();
            lblTotalVehicles.Text = "Total Vehicles: 0";
            lblTotalVehicles.Location = new Point(450, 15);
            lblTotalVehicles.AutoSize = true;
            lblTotalVehicles.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Add controls to toolbar
            toolbarPanel.Controls.Add(lblSearch);
            toolbarPanel.Controls.Add(txtSearchVehicles);
            toolbarPanel.Controls.Add(btnSearchVehicles);
            toolbarPanel.Controls.Add(btnRefreshVehicles);
            toolbarPanel.Controls.Add(btnAddVehicle);
            toolbarPanel.Controls.Add(btnEditVehicle);
            toolbarPanel.Controls.Add(btnDeleteVehicle);
            toolbarPanel.Controls.Add(lblTotalVehicles);

            // Create DataGridView for vehicles
            dgvVehicles = new DataGridView();
            dgvVehicles.Dock = DockStyle.Fill;
            dgvVehicles.ReadOnly = true;
            dgvVehicles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVehicles.MultiSelect = false;
            dgvVehicles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVehicles.AllowUserToAddRows = false;
            dgvVehicles.AllowUserToDeleteRows = false;
            dgvVehicles.RowHeadersVisible = false;
            dgvVehicles.SelectionChanged += DgvVehicles_SelectionChanged;
            dgvVehicles.DoubleClick += DgvVehicles_DoubleClick;

            // Add controls to tab
            tab.Controls.Add(dgvVehicles);
            tab.Controls.Add(toolbarPanel);

            return tab;
        }

        private TabPage CreateCustomersTab()
        {
            TabPage tab = new TabPage("Customers");

            DataGridView dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.ReadOnly = true;

            tab.Controls.Add(dgv);
            return tab;
        }

        private TabPage CreateReportsTab()
        {
            TabPage tab = new TabPage("Reports");

            ComboBox cmb = new ComboBox();
            cmb.Items.AddRange(new string[] { "Sales", "Inventory", "Customers" });
            cmb.Dock = DockStyle.Top;

            tab.Controls.Add(cmb);
            return tab;
        }

        private void LoadData()
        {
            LoadVehicles();
            LoadSampleCustomers();
        }

        private void LoadVehicles()
        {
            try
            {
                var vehicles = _vehicleService.GetAllVehicles();

                // Create DataTable for binding
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Make", typeof(string));
                dt.Columns.Add("Model", typeof(string));
                dt.Columns.Add("Year", typeof(int));
                dt.Columns.Add("VIN", typeof(string));
                dt.Columns.Add("Color", typeof(string));
                dt.Columns.Add("Price", typeof(decimal));
                dt.Columns.Add("Status", typeof(string));
                dt.Columns.Add("Fuel Type", typeof(string));
                dt.Columns.Add("Transmission", typeof(string));
                dt.Columns.Add("Mileage", typeof(string));
                dt.Columns.Add("Date Added", typeof(DateTime));

                foreach (var vehicle in vehicles)
                {
                    dt.Rows.Add(
                        vehicle.VehicleID,
                        vehicle.Make,
                        vehicle.Model,
                        vehicle.Year,
                        vehicle.VIN,
                        vehicle.Color,
                        vehicle.Price,
                        vehicle.Status,
                        vehicle.FuelType ?? "N/A",
                        vehicle.Transmission ?? "N/A",
                        vehicle.Mileage?.ToString() ?? "N/A",
                        vehicle.DateAdded
                    );
                }

                dgvVehicles.DataSource = dt;

                // Hide ID column
                if (dgvVehicles.Columns["ID"] != null)
                    dgvVehicles.Columns["ID"].Visible = false;

                // Format Price column
                if (dgvVehicles.Columns["Price"] != null)
                {
                    dgvVehicles.Columns["Price"].DefaultCellStyle.Format = "C2";
                    dgvVehicles.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                // Format Date column
                if (dgvVehicles.Columns["Date Added"] != null)
                {
                    dgvVehicles.Columns["Date Added"].DefaultCellStyle.Format = "MM/dd/yyyy";
                }

                // Update total count
                lblTotalVehicles.Text = $"Total Vehicles: {vehicles.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vehicles: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSampleCustomers()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Email");
            dt.Columns.Add("Phone");
            dt.Rows.Add(_currentCustomer.FirstName + " " + _currentCustomer.LastName,
                        _currentCustomer.Email,
                        _currentCustomer.MobilePhone);
            dt.Rows.Add("John Smith", "john@example.com", "555-1234");

            ((DataGridView)tabCustomers.Controls[0]).DataSource = dt;
        }

        // Vehicle Management Event Handlers
        private void BtnAddVehicle_Click(object sender, EventArgs e)
        {
            using (var form = new AddEditVehicleForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadVehicles(); // Refresh the grid
                }
            }
        }

        private void BtnEditVehicle_Click(object sender, EventArgs e)
        {
            if (dgvVehicles.SelectedRows.Count > 0)
            {
                try
                {
                    var selectedRow = dgvVehicles.SelectedRows[0];
                    int vehicleId = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                    var vehicle = _vehicleService.GetVehicleById(vehicleId);
                    if (vehicle != null)
                    {
                        using (var form = new AddEditVehicleForm(vehicle))
                        {
                            if (form.ShowDialog() == DialogResult.OK)
                            {
                                LoadVehicles(); // Refresh the grid
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vehicle not found.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error editing vehicle: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnDeleteVehicle_Click(object sender, EventArgs e)
        {
            if (dgvVehicles.SelectedRows.Count > 0)
            {
                try
                {
                    var selectedRow = dgvVehicles.SelectedRows[0];
                    int vehicleId = Convert.ToInt32(selectedRow.Cells["ID"].Value);
                    string vehicleInfo = $"{selectedRow.Cells["Make"].Value} {selectedRow.Cells["Model"].Value} ({selectedRow.Cells["Year"].Value})";

                    var result = MessageBox.Show(
                        $"Are you sure you want to delete the vehicle: {vehicleInfo}?",
                        "Confirm Delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        bool success = _vehicleService.DeleteVehicle(vehicleId);
                        if (success)
                        {
                            MessageBox.Show("Vehicle deleted successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadVehicles(); // Refresh the grid
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting vehicle: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnRefreshVehicles_Click(object sender, EventArgs e)
        {
            LoadVehicles();
            txtSearchVehicles.Clear();
        }

        private void BtnSearchVehicles_Click(object sender, EventArgs e)
        {
            SearchVehicles();
        }

        private void TxtSearchVehicles_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SearchVehicles();
            }
        }

        private void SearchVehicles()
        {
            try
            {
                var vehicles = _vehicleService.SearchVehicles(txtSearchVehicles.Text);

                // Create DataTable for binding (same structure as LoadVehicles)
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Make", typeof(string));
                dt.Columns.Add("Model", typeof(string));
                dt.Columns.Add("Year", typeof(int));
                dt.Columns.Add("VIN", typeof(string));
                dt.Columns.Add("Color", typeof(string));
                dt.Columns.Add("Price", typeof(decimal));
                dt.Columns.Add("Status", typeof(string));
                dt.Columns.Add("Fuel Type", typeof(string));
                dt.Columns.Add("Transmission", typeof(string));
                dt.Columns.Add("Mileage", typeof(string));
                dt.Columns.Add("Date Added", typeof(DateTime));

                foreach (var vehicle in vehicles)
                {
                    dt.Rows.Add(
                        vehicle.VehicleID,
                        vehicle.Make,
                        vehicle.Model,
                        vehicle.Year,
                        vehicle.VIN,
                        vehicle.Color,
                        vehicle.Price,
                        vehicle.Status,
                        vehicle.FuelType ?? "N/A",
                        vehicle.Transmission ?? "N/A",
                        vehicle.Mileage?.ToString() ?? "N/A",
                        vehicle.DateAdded
                    );
                }

                dgvVehicles.DataSource = dt;

                // Hide ID column
                if (dgvVehicles.Columns["ID"] != null)
                    dgvVehicles.Columns["ID"].Visible = false;

                lblTotalVehicles.Text = $"Total Vehicles: {vehicles.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching vehicles: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvVehicles_SelectionChanged(object sender, EventArgs e)
        {
            bool hasSelection = dgvVehicles.SelectedRows.Count > 0;
            btnEditVehicle.Enabled = hasSelection;
            btnDeleteVehicle.Enabled = hasSelection;
        }

        private void DgvVehicles_DoubleClick(object sender, EventArgs e)
        {
            if (dgvVehicles.SelectedRows.Count > 0)
            {
                BtnEditVehicle_Click(sender, e);
            }
        }

        private void LogoutItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Confirm",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
                new LoginForm().Show();
                this.Close();
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _vehicleService?.Dispose();
            base.OnFormClosed(e);
        }
    }
}