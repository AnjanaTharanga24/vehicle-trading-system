using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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

        // Dashboard Elements
        private PictureBox dashboardImage;
        private Panel statsPanel;
        private System.Windows.Forms.Timer animationTimer;

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
            this.Text = $"Vehicle Trading System - {_currentCustomer.Username}";
            this.ClientSize = new Size(1200, 750);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(240, 240, 240);

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
            menuStrip.BackColor = Color.FromArgb(51, 51, 76);
            menuStrip.ForeColor = Color.White;

            // File Menu
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");
            fileMenu.ForeColor = Color.White;
            fileMenu.DropDownItems.Add("Dashboard", null, (s, e) => mainTabControl.SelectedTab = tabHome);
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add("Logout", null, LogoutItem_Click);

            // View Menu
            ToolStripMenuItem viewMenu = new ToolStripMenuItem("View");
            viewMenu.ForeColor = Color.White;
            viewMenu.DropDownItems.Add("Vehicles", null, (s, e) => mainTabControl.SelectedTab = tabVehicles);
            viewMenu.DropDownItems.Add("Reports", null, (s, e) => mainTabControl.SelectedTab = tabReports);

            // Vehicle Menu
            ToolStripMenuItem vehicleMenu = new ToolStripMenuItem("Vehicles");
            vehicleMenu.ForeColor = Color.White;
            vehicleMenu.DropDownItems.Add("Add New Vehicle", null, BtnAddVehicle_Click);
            vehicleMenu.DropDownItems.Add("Refresh List", null, BtnRefreshVehicles_Click);

            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(viewMenu);
            menuStrip.Items.Add(vehicleMenu);
        }

        private void CreateStatusStrip()
        {
            statusStrip = new StatusStrip();
            statusStrip.BackColor = Color.FromArgb(51, 51, 76);
            statusStrip.ForeColor = Color.White;
            statusStrip.Items.Add($"Logged in as: {_currentCustomer.Username}");
            statusStrip.Items.Add($"Last login: {DateTime.Now.ToString("g")}");
        }

        private void CreateTabControl()
        {
            mainTabControl = new TabControl();
            mainTabControl.Dock = DockStyle.Fill;
            mainTabControl.Appearance = TabAppearance.Normal;
            mainTabControl.ItemSize = new Size(120, 35);
            mainTabControl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            mainTabControl.BackColor = Color.White;

            // Create and add tab pages
            tabHome = CreateHomeTab();
            tabVehicles = CreateVehiclesTab();
            tabReports = CreateReportsTab();

            mainTabControl.Controls.Add(tabHome);
            mainTabControl.Controls.Add(tabVehicles);
            mainTabControl.Controls.Add(tabReports);
        }

        private TabPage CreateHomeTab()
        {
            TabPage tab = new TabPage("Dashboard");
            tab.BackColor = Color.FromArgb(248, 249, 250);

            // Main container panel
            Panel mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.BackColor = Color.FromArgb(248, 249, 250);
            mainPanel.Padding = new Padding(20);

            // Welcome header panel
            Panel headerPanel = new Panel();
            headerPanel.Height = 120;
            headerPanel.Dock = DockStyle.Top;
            headerPanel.BackColor = Color.FromArgb(51, 51, 76);

            Label lblWelcome = new Label();
            lblWelcome.Text = $"Welcome Back, {_currentCustomer.FirstName}!";
            lblWelcome.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            lblWelcome.ForeColor = Color.White;
            lblWelcome.Location = new Point(30, 30);
            lblWelcome.AutoSize = true;

            Label lblSubtitle = new Label();
            lblSubtitle.Text = "Vehicle Trading System Dashboard";
            lblSubtitle.Font = new Font("Segoe UI", 14);
            lblSubtitle.ForeColor = Color.FromArgb(200, 200, 200);
            lblSubtitle.Location = new Point(30, 70);
            lblSubtitle.AutoSize = true;

            headerPanel.Controls.Add(lblWelcome);
            headerPanel.Controls.Add(lblSubtitle);

            // Content panel for stats and image
            Panel contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = Color.FromArgb(248, 249, 250);
            contentPanel.Padding = new Padding(20);

            // Stats panel
            CreateStatsPanel(contentPanel);

            // Car image panel
            CreateDashboardImage(contentPanel);

            // Quick actions panel
            CreateQuickActionsPanel(contentPanel);

            mainPanel.Controls.Add(contentPanel);
            mainPanel.Controls.Add(headerPanel);
            tab.Controls.Add(mainPanel);

            return tab;
        }

        private void CreateStatsPanel(Panel parent)
        {
            statsPanel = new Panel();
            statsPanel.Size = new Size(parent.Width - 40, 150);
            statsPanel.Location = new Point(20, 20);
            statsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // Get vehicle statistics
            var vehicles = _vehicleService.GetAllVehicles();
            int totalVehicles = vehicles.Count;
            int availableVehicles = vehicles.Count(v => v.Status == "Available");
            int soldVehicles = vehicles.Count(v => v.Status == "Sold");
            decimal totalValue = vehicles.Where(v => v.Status == "Available").Sum(v => v.Price);

            // Create stat cards
            CreateStatCard("Total Vehicles", totalVehicles.ToString(), Color.FromArgb(52, 152, 219), 0);
            CreateStatCard("Available", availableVehicles.ToString(), Color.FromArgb(46, 204, 113), 1);
            CreateStatCard("Sold", soldVehicles.ToString(), Color.FromArgb(231, 76, 60), 2);
            CreateStatCard("Total Value", totalValue.ToString("C0"), Color.FromArgb(155, 89, 182), 3);

            parent.Controls.Add(statsPanel);
        }

        private void CreateStatCard(string title, string value, Color color, int index)
        {
            Panel card = new Panel();
            card.Size = new Size(200, 120);
            card.Location = new Point(index * 220 + 20, 20);
            card.BackColor = color;
            card.Cursor = Cursors.Hand;

            Label lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(15, 20);
            lblTitle.AutoSize = true;

            Label lblValue = new Label();
            lblValue.Text = value;
            lblValue.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblValue.ForeColor = Color.White;
            lblValue.Location = new Point(15, 50);
            lblValue.AutoSize = true;

            // Add hover effect
            card.MouseEnter += (s, e) => {
                card.BackColor = ControlPaint.Light(color, 0.2f);
            };
            card.MouseLeave += (s, e) => {
                card.BackColor = color;
            };

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblValue);
            statsPanel.Controls.Add(card);
        }

        private void CreateDashboardImage(Panel parent)
        {
            Panel imagePanel = new Panel();
            imagePanel.Size = new Size(600, 300);
            imagePanel.Location = new Point(20, 190);
            imagePanel.BackColor = Color.White;
            imagePanel.BorderStyle = BorderStyle.FixedSingle;

            dashboardImage = new PictureBox();
            dashboardImage.Dock = DockStyle.Fill;
            dashboardImage.SizeMode = PictureBoxSizeMode.Zoom;
            dashboardImage.BackColor = Color.White;

            // Load image from URL asynchronously
            LoadDashboardImage();

            Label lblImageTitle = new Label();
            lblImageTitle.Text = "Featured Vehicle";
            lblImageTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblImageTitle.ForeColor = Color.FromArgb(51, 51, 76);
            lblImageTitle.BackColor = Color.White;
            lblImageTitle.Location = new Point(20, 10);
            lblImageTitle.AutoSize = true;
            lblImageTitle.BringToFront();

            imagePanel.Controls.Add(dashboardImage);
            imagePanel.Controls.Add(lblImageTitle);
            parent.Controls.Add(imagePanel);
        }

        private void LoadDashboardImage()
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.DownloadDataCompleted += (sender, e) => {
                    try
                    {
                        if (e.Error == null)
                        {
                            using (var ms = new MemoryStream(e.Result))
                            {
                                dashboardImage.Image = Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            // If image fails to load, show placeholder
                            dashboardImage.BackColor = Color.FromArgb(51, 51, 76);
                        }
                    }
                    catch
                    {
                        dashboardImage.BackColor = Color.FromArgb(51, 51, 76);
                    }
                };
                webClient.DownloadDataAsync(new Uri("https://gvelondon.com/wp-content/uploads/2024/05/veyron-super-sport-300-bugatti-1-Upd.png"));
            }
            catch
            {
                dashboardImage.BackColor = Color.FromArgb(51, 51, 76);
            }
        }

        private void CreateQuickActionsPanel(Panel parent)
        {
            Panel actionsPanel = new Panel();
            actionsPanel.Size = new Size(300, 200);
            actionsPanel.Location = new Point(650, 190);
            actionsPanel.BackColor = Color.White;
            actionsPanel.BorderStyle = BorderStyle.FixedSingle;

            Label lblActionsTitle = new Label();
            lblActionsTitle.Text = "Quick Actions";
            lblActionsTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblActionsTitle.ForeColor = Color.FromArgb(51, 51, 76);
            lblActionsTitle.Location = new Point(20, 20);
            lblActionsTitle.AutoSize = true;

            Button btnQuickAddVehicle = new Button();
            btnQuickAddVehicle.Text = "Add New Vehicle";
            btnQuickAddVehicle.Size = new Size(250, 40);
            btnQuickAddVehicle.Location = new Point(20, 60);
            btnQuickAddVehicle.BackColor = Color.FromArgb(46, 204, 113);
            btnQuickAddVehicle.ForeColor = Color.White;
            btnQuickAddVehicle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnQuickAddVehicle.FlatStyle = FlatStyle.Flat;
            btnQuickAddVehicle.Click += BtnAddVehicle_Click;

            Button btnViewVehicles = new Button();
            btnViewVehicles.Text = "View All Vehicles";
            btnViewVehicles.Size = new Size(250, 40);
            btnViewVehicles.Location = new Point(20, 110);
            btnViewVehicles.BackColor = Color.FromArgb(52, 152, 219);
            btnViewVehicles.ForeColor = Color.White;
            btnViewVehicles.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnViewVehicles.FlatStyle = FlatStyle.Flat;
            btnViewVehicles.Click += (s, e) => mainTabControl.SelectedTab = tabVehicles;

            actionsPanel.Controls.Add(lblActionsTitle);
            actionsPanel.Controls.Add(btnQuickAddVehicle);
            actionsPanel.Controls.Add(btnViewVehicles);

            parent.Controls.Add(actionsPanel);
        }

        private TabPage CreateVehiclesTab()
        {
            TabPage tab = new TabPage("Vehicles");

            // Create toolbar panel
            Panel toolbarPanel = new Panel();
            toolbarPanel.Height = 80;
            toolbarPanel.Dock = DockStyle.Top;
            toolbarPanel.BackColor = Color.FromArgb(51, 51, 76);

            // Search controls
            Label lblSearch = new Label();
            lblSearch.Text = "Search:";
            lblSearch.Location = new Point(10, 15);
            lblSearch.ForeColor = Color.White;
            lblSearch.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblSearch.AutoSize = true;

            txtSearchVehicles = new TextBox();
            txtSearchVehicles.Location = new Point(70, 12);
            txtSearchVehicles.Size = new Size(200, 23);
            txtSearchVehicles.KeyPress += TxtSearchVehicles_KeyPress;

            btnSearchVehicles = new Button();
            btnSearchVehicles.Text = "Search";
            btnSearchVehicles.Location = new Point(280, 11);
            btnSearchVehicles.Size = new Size(75, 25);
            btnSearchVehicles.BackColor = Color.FromArgb(52, 152, 219);
            btnSearchVehicles.ForeColor = Color.White;
            btnSearchVehicles.FlatStyle = FlatStyle.Flat;
            btnSearchVehicles.Click += BtnSearchVehicles_Click;

            btnRefreshVehicles = new Button();
            btnRefreshVehicles.Text = "Refresh";
            btnRefreshVehicles.Location = new Point(365, 11);
            btnRefreshVehicles.Size = new Size(75, 25);
            btnRefreshVehicles.BackColor = Color.FromArgb(155, 89, 182);
            btnRefreshVehicles.ForeColor = Color.White;
            btnRefreshVehicles.FlatStyle = FlatStyle.Flat;
            btnRefreshVehicles.Click += BtnRefreshVehicles_Click;

            // Action buttons
            btnAddVehicle = new Button();
            btnAddVehicle.Text = "Add Vehicle";
            btnAddVehicle.Location = new Point(10, 45);
            btnAddVehicle.Size = new Size(100, 30);
            btnAddVehicle.BackColor = Color.FromArgb(46, 204, 113);
            btnAddVehicle.ForeColor = Color.White;
            btnAddVehicle.FlatStyle = FlatStyle.Flat;
            btnAddVehicle.Click += BtnAddVehicle_Click;

            btnEditVehicle = new Button();
            btnEditVehicle.Text = "Edit Vehicle";
            btnEditVehicle.Location = new Point(120, 45);
            btnEditVehicle.Size = new Size(100, 30);
            btnEditVehicle.BackColor = Color.FromArgb(243, 156, 18);
            btnEditVehicle.ForeColor = Color.White;
            btnEditVehicle.FlatStyle = FlatStyle.Flat;
            btnEditVehicle.Click += BtnEditVehicle_Click;
            btnEditVehicle.Enabled = false;

            btnDeleteVehicle = new Button();
            btnDeleteVehicle.Text = "Delete Vehicle";
            btnDeleteVehicle.Location = new Point(230, 45);
            btnDeleteVehicle.Size = new Size(100, 30);
            btnDeleteVehicle.BackColor = Color.FromArgb(231, 76, 60);
            btnDeleteVehicle.ForeColor = Color.White;
            btnDeleteVehicle.FlatStyle = FlatStyle.Flat;
            btnDeleteVehicle.Click += BtnDeleteVehicle_Click;
            btnDeleteVehicle.Enabled = false;

            // Total vehicles label
            lblTotalVehicles = new Label();
            lblTotalVehicles.Text = "Total Vehicles: 0";
            lblTotalVehicles.Location = new Point(450, 15);
            lblTotalVehicles.AutoSize = true;
            lblTotalVehicles.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTotalVehicles.ForeColor = Color.White;

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
            dgvVehicles.BackgroundColor = Color.White;
            dgvVehicles.GridColor = Color.FromArgb(200, 200, 200);
            dgvVehicles.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);
            dgvVehicles.SelectionChanged += DgvVehicles_SelectionChanged;
            dgvVehicles.DoubleClick += DgvVehicles_DoubleClick;

            // Add controls to tab
            tab.Controls.Add(dgvVehicles);
            tab.Controls.Add(toolbarPanel);

            return tab;
        }

        private TabPage CreateReportsTab()
        {
            TabPage tab = new TabPage("Reports");
            tab.BackColor = Color.FromArgb(248, 249, 250);

            Panel mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Padding = new Padding(30);
            mainPanel.BackColor = Color.FromArgb(248, 249, 250);

            Label lblTitle = new Label();
            lblTitle.Text = "Vehicle Reports";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(51, 51, 76);
            lblTitle.Location = new Point(30, 30);
            lblTitle.AutoSize = true;

            Label lblDescription = new Label();
            lblDescription.Text = "Generate comprehensive reports for all vehicles in your inventory.";
            lblDescription.Font = new Font("Segoe UI", 12);
            lblDescription.ForeColor = Color.FromArgb(100, 100, 100);
            lblDescription.Location = new Point(30, 70);
            lblDescription.AutoSize = true;

            // Report options panel
            Panel reportPanel = new Panel();
            reportPanel.Size = new Size(500, 300);
            reportPanel.Location = new Point(30, 120);
            reportPanel.BackColor = Color.White;
            reportPanel.BorderStyle = BorderStyle.FixedSingle;

            Label lblReportTitle = new Label();
            lblReportTitle.Text = "Available Reports";
            lblReportTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblReportTitle.ForeColor = Color.FromArgb(51, 51, 76);
            lblReportTitle.Location = new Point(20, 20);
            lblReportTitle.AutoSize = true;

            // Download buttons
            Button btnDownloadAllVehicles = new Button();
            btnDownloadAllVehicles.Text = "Download All Vehicles Report (CSV)";
            btnDownloadAllVehicles.Size = new Size(300, 40);
            btnDownloadAllVehicles.Location = new Point(20, 70);
            btnDownloadAllVehicles.BackColor = Color.FromArgb(46, 204, 113);
            btnDownloadAllVehicles.ForeColor = Color.White;
            btnDownloadAllVehicles.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnDownloadAllVehicles.FlatStyle = FlatStyle.Flat;
            btnDownloadAllVehicles.Click += BtnDownloadAllVehicles_Click;

            Button btnDownloadAvailableVehicles = new Button();
            btnDownloadAvailableVehicles.Text = "Download Available Vehicles Report (CSV)";
            btnDownloadAvailableVehicles.Size = new Size(300, 40);
            btnDownloadAvailableVehicles.Location = new Point(20, 120);
            btnDownloadAvailableVehicles.BackColor = Color.FromArgb(52, 152, 219);
            btnDownloadAvailableVehicles.ForeColor = Color.White;
            btnDownloadAvailableVehicles.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnDownloadAvailableVehicles.FlatStyle = FlatStyle.Flat;
            btnDownloadAvailableVehicles.Click += BtnDownloadAvailableVehicles_Click;

            Button btnDownloadSoldVehicles = new Button();
            btnDownloadSoldVehicles.Text = "Download Sold Vehicles Report (CSV)";
            btnDownloadSoldVehicles.Size = new Size(300, 40);
            btnDownloadSoldVehicles.Location = new Point(20, 170);
            btnDownloadSoldVehicles.BackColor = Color.FromArgb(231, 76, 60);
            btnDownloadSoldVehicles.ForeColor = Color.White;
            btnDownloadSoldVehicles.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnDownloadSoldVehicles.FlatStyle = FlatStyle.Flat;
            btnDownloadSoldVehicles.Click += BtnDownloadSoldVehicles_Click;

            Button btnDownloadSummaryReport = new Button();
            btnDownloadSummaryReport.Text = "Download Summary Report (TXT)";
            btnDownloadSummaryReport.Size = new Size(300, 40);
            btnDownloadSummaryReport.Location = new Point(20, 220);
            btnDownloadSummaryReport.BackColor = Color.FromArgb(155, 89, 182);
            btnDownloadSummaryReport.ForeColor = Color.White;
            btnDownloadSummaryReport.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnDownloadSummaryReport.FlatStyle = FlatStyle.Flat;
            btnDownloadSummaryReport.Click += BtnDownloadSummaryReport_Click;

            reportPanel.Controls.Add(lblReportTitle);
            reportPanel.Controls.Add(btnDownloadAllVehicles);
            reportPanel.Controls.Add(btnDownloadAvailableVehicles);
            reportPanel.Controls.Add(btnDownloadSoldVehicles);
            reportPanel.Controls.Add(btnDownloadSummaryReport);

            mainPanel.Controls.Add(lblTitle);
            mainPanel.Controls.Add(lblDescription);
            mainPanel.Controls.Add(reportPanel);

            tab.Controls.Add(mainPanel);
            return tab;
        }

        private void LoadData()
        {
            LoadVehicles();
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

                // Refresh stats panel if on dashboard
                if (mainTabControl.SelectedTab == tabHome && statsPanel != null)
                {
                    statsPanel.Controls.Clear();
                    CreateStatsPanel(tabHome.Controls[0].Controls[1] as Panel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading vehicles: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        // Report Download Event Handlers
        private void BtnDownloadAllVehicles_Click(object sender, EventArgs e)
        {
            try
            {
                var vehicles = _vehicleService.GetAllVehicles();
                DownloadVehiclesReport(vehicles, "All_Vehicles_Report");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating all vehicles report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDownloadAvailableVehicles_Click(object sender, EventArgs e)
        {
            try
            {
                var vehicles = _vehicleService.GetAllVehicles().Where(v => v.Status == "Available").ToList();
                DownloadVehiclesReport(vehicles, "Available_Vehicles_Report");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating available vehicles report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDownloadSoldVehicles_Click(object sender, EventArgs e)
        {
            try
            {
                var vehicles = _vehicleService.GetAllVehicles().Where(v => v.Status == "Sold").ToList();
                DownloadVehiclesReport(vehicles, "Sold_Vehicles_Report");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating sold vehicles report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDownloadSummaryReport_Click(object sender, EventArgs e)
        {
            try
            {
                var vehicles = _vehicleService.GetAllVehicles();
                DownloadSummaryReport(vehicles);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating summary report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DownloadVehiclesReport(System.Collections.Generic.List<Vehicle> vehicles, string reportName)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            saveFileDialog.FileName = $"{reportName}_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            saveFileDialog.Title = "Save Vehicle Report";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StringBuilder csv = new StringBuilder();

                    // Add header
                    csv.AppendLine("Vehicle ID,Make,Model,Year,VIN,Color,Price,Status,Fuel Type,Transmission,Mileage,Date Added");

                    // Add data
                    foreach (var vehicle in vehicles)
                    {
                        csv.AppendLine($"{vehicle.VehicleID}," +
                                     $"\"{vehicle.Make}\"," +
                                     $"\"{vehicle.Model}\"," +
                                     $"{vehicle.Year}," +
                                     $"\"{vehicle.VIN}\"," +
                                     $"\"{vehicle.Color}\"," +
                                     $"{vehicle.Price}," +
                                     $"\"{vehicle.Status}\"," +
                                     $"\"{vehicle.FuelType ?? "N/A"}\"," +
                                     $"\"{vehicle.Transmission ?? "N/A"}\"," +
                                     $"\"{vehicle.Mileage?.ToString() ?? "N/A"}\"," +
                                     $"{vehicle.DateAdded:MM/dd/yyyy}");
                    }

                    File.WriteAllText(saveFileDialog.FileName, csv.ToString());

                    MessageBox.Show($"Report saved successfully!\nLocation: {saveFileDialog.FileName}\nTotal vehicles: {vehicles.Count}",
                        "Report Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving report: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DownloadSummaryReport(System.Collections.Generic.List<Vehicle> vehicles)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            saveFileDialog.FileName = $"Vehicle_Summary_Report_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            saveFileDialog.Title = "Save Summary Report";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StringBuilder report = new StringBuilder();

                    // Report header
                    report.AppendLine("VEHICLE TRADING SYSTEM - SUMMARY REPORT");
                    report.AppendLine("=" + new string('=', 50));
                    report.AppendLine($"Generated on: {DateTime.Now:MMMM dd, yyyy 'at' HH:mm:ss}");
                    report.AppendLine($"Generated by: {_currentCustomer.FirstName} {_currentCustomer.LastName}");
                    report.AppendLine();

                    // Overall statistics
                    var totalVehicles = vehicles.Count;
                    var availableVehicles = vehicles.Count(v => v.Status == "Available");
                    var soldVehicles = vehicles.Count(v => v.Status == "Sold");
                    var totalValue = vehicles.Where(v => v.Status == "Available").Sum(v => v.Price);

                    report.AppendLine("OVERALL STATISTICS");
                    report.AppendLine("-" + new string('-', 30));
                    report.AppendLine($"Total Vehicles: {totalVehicles}");
                    report.AppendLine($"Available Vehicles: {availableVehicles}");
                    report.AppendLine($"Sold Vehicles: {soldVehicles}");
                    report.AppendLine($"Total Available Inventory Value: {totalValue:C2}");
                    report.AppendLine();

                    // Breakdown by make
                    report.AppendLine("BREAKDOWN BY MAKE");
                    report.AppendLine("-" + new string('-', 30));
                    var makeGroups = vehicles.GroupBy(v => v.Make).OrderByDescending(g => g.Count());
                    foreach (var group in makeGroups)
                    {
                        var available = group.Count(v => v.Status == "Available");
                        var sold = group.Count(v => v.Status == "Sold");
                        report.AppendLine($"{group.Key}: {group.Count()} total (Available: {available}, Sold: {sold})");
                    }
                    report.AppendLine();

                    // Breakdown by status
                    report.AppendLine("BREAKDOWN BY STATUS");
                    report.AppendLine("-" + new string('-', 30));
                    var statusGroups = vehicles.GroupBy(v => v.Status).OrderByDescending(g => g.Count());
                    foreach (var group in statusGroups)
                    {
                        var avgPrice = group.Average(v => v.Price);
                        report.AppendLine($"{group.Key}: {group.Count()} vehicles (Avg Price: {avgPrice:C2})");
                    }
                    report.AppendLine();

                    // Price analysis
                    if (availableVehicles > 0)
                    {
                        var availableVehiclesList = vehicles.Where(v => v.Status == "Available");
                        var minPrice = availableVehiclesList.Min(v => v.Price);
                        var maxPrice = availableVehiclesList.Max(v => v.Price);
                        var avgPrice = availableVehiclesList.Average(v => v.Price);

                        report.AppendLine("PRICE ANALYSIS (Available Vehicles Only)");
                        report.AppendLine("-" + new string('-', 40));
                        report.AppendLine($"Lowest Price: {minPrice:C2}");
                        report.AppendLine($"Highest Price: {maxPrice:C2}");
                        report.AppendLine($"Average Price: {avgPrice:C2}");
                        report.AppendLine();
                    }

                    // Recent additions (last 30 days)
                    var recentVehicles = vehicles.Where(v => v.DateAdded >= DateTime.Now.AddDays(-30)).ToList();
                    if (recentVehicles.Count > 0)
                    {
                        report.AppendLine("RECENT ADDITIONS (Last 30 Days)");
                        report.AppendLine("-" + new string('-', 40));
                        report.AppendLine($"Total Added: {recentVehicles.Count} vehicles");
                        foreach (var vehicle in recentVehicles.OrderByDescending(v => v.DateAdded))
                        {
                            report.AppendLine($"• {vehicle.Make} {vehicle.Model} ({vehicle.Year}) - {vehicle.DateAdded:MM/dd/yyyy}");
                        }
                        report.AppendLine();
                    }

                    report.AppendLine("END OF REPORT");
                    report.AppendLine("=" + new string('=', 50));

                    File.WriteAllText(saveFileDialog.FileName, report.ToString());

                    MessageBox.Show($"Summary report saved successfully!\nLocation: {saveFileDialog.FileName}",
                        "Report Generated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving summary report: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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