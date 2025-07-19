using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using VehicleTradingSystem.Forms;

namespace VehicleTradingSystem
{
    public partial class DashboardForm : Form
    {
        private MenuStrip menuStrip;
        private StatusStrip statusStrip;
        private TabControl mainTabControl;

        // Home Tab
        private TabPage tabHome;
        private Label lblWelcome;
        private Label lblSystemName;
        private Panel infoPanel;

        // Vehicles Tab
        private TabPage tabVehicles;
        private DataGridView dgvVehicles;
        private Button btnAddVehicle;
        private Button btnEditVehicle;
        private Button btnDeleteVehicle;
        private Button btnSearchVehicle;
        private TextBox txtSearchVehicle;

        // Customers Tab
        private TabPage tabCustomers;
        private DataGridView dgvCustomers;
        private Button btnAddCustomer;
        private Button btnEditCustomer;
        private Button btnDeleteCustomer;

        // Reports Tab
        private TabPage tabReports;
        private ComboBox cmbReportType;
        private Button btnGenerateReport;
        private DataGridView dgvReportData;
        private DateTimePicker dtpReportStart;
        private DateTimePicker dtpReportEnd;

        public DashboardForm()
        {
            InitializeComponent();
            LoadSampleData();
        }

        private void InitializeComponent()
        {
            // Main Form Settings
            this.ClientSize = new Size(1200, 750);
            this.Text = "Dashboard -Vehicle buying and selling System";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

            // Menu Strip
            CreateMenuStrip();

            // Status Strip
            CreateStatusStrip();

            // Main Tab Control
            mainTabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 24),
                Size = new Size(1200, 700),
                ItemSize = new Size(120, 30),
                Appearance = TabAppearance.Normal
            };

            // Create Tabs
            CreateHomeTab();
            CreateVehiclesTab();
            CreateCustomersTab();
            CreateReportsTab();

            // Add tabs to tab control
            mainTabControl.Controls.Add(tabHome);
            mainTabControl.Controls.Add(tabVehicles);
            mainTabControl.Controls.Add(tabCustomers);
            mainTabControl.Controls.Add(tabReports);

            // Add controls to form
            this.Controls.Add(mainTabControl);
            this.Controls.Add(menuStrip);
            this.Controls.Add(statusStrip);
            this.MainMenuStrip = menuStrip;
        }

        private void CreateMenuStrip()
        {
            menuStrip = new MenuStrip
            {
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            // File Menu
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");

            ToolStripMenuItem dashboardItem = new ToolStripMenuItem("Dashboard");
            dashboardItem.Click += (s, e) => mainTabControl.SelectedTab = tabHome;

            ToolStripMenuItem logoutItem = new ToolStripMenuItem("Logout");
            logoutItem.Click += LogoutItem_Click;

            ToolStripMenuItem exitItem = new ToolStripMenuItem("Exit");
            exitItem.Click += ExitItem_Click;

            fileMenu.DropDownItems.Add(dashboardItem);
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add(logoutItem);
            fileMenu.DropDownItems.Add(new ToolStripSeparator());
            fileMenu.DropDownItems.Add(exitItem);

            // View Menu
            ToolStripMenuItem viewMenu = new ToolStripMenuItem("View");

            ToolStripMenuItem vehiclesItem = new ToolStripMenuItem("Vehicles");
            vehiclesItem.Click += (s, e) => mainTabControl.SelectedTab = tabVehicles;

            ToolStripMenuItem customersItem = new ToolStripMenuItem("Customers");
            customersItem.Click += (s, e) => mainTabControl.SelectedTab = tabCustomers;

            viewMenu.DropDownItems.Add(vehiclesItem);
            viewMenu.DropDownItems.Add(customersItem);

            // Reports Menu
            ToolStripMenuItem reportsMenu = new ToolStripMenuItem("Reports");
            reportsMenu.Click += (s, e) => mainTabControl.SelectedTab = tabReports;

            // Help Menu
            ToolStripMenuItem helpMenu = new ToolStripMenuItem("Help");

            ToolStripMenuItem aboutItem = new ToolStripMenuItem("About");
            aboutItem.Click += AboutItem_Click;

            helpMenu.DropDownItems.Add(aboutItem);

            // Add menus to menu strip
            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(viewMenu);
            menuStrip.Items.Add(reportsMenu);
            menuStrip.Items.Add(helpMenu);
        }

        private void CreateStatusStrip()
        {
            statusStrip = new StatusStrip
            {
                BackColor = Color.LightGray,
                Font = new Font("Segoe UI", 9F)
            };

            ToolStripStatusLabel lblStatus = new ToolStripStatusLabel
            {
                Text = "Ready",
                Spring = true,
                TextAlign = ContentAlignment.MiddleLeft
            };

            ToolStripStatusLabel lblUser = new ToolStripStatusLabel
            {
                Text = "User: Admin",
                TextAlign = ContentAlignment.MiddleRight
            };

            ToolStripStatusLabel lblDate = new ToolStripStatusLabel
            {
                Text = DateTime.Now.ToString("yyyy-MM-dd"),
                TextAlign = ContentAlignment.MiddleRight
            };

            statusStrip.Items.Add(lblStatus);
            statusStrip.Items.Add(lblUser);
            statusStrip.Items.Add(lblDate);
        }

        private void CreateHomeTab()
        {
            tabHome = new TabPage
            {
                Text = "Dashboard",
                BackColor = Color.WhiteSmoke
            };

            lblSystemName = new Label
            {
                Text = "Vehicle buying and selling System",
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(350, 50),
                AutoSize = true
            };

            lblWelcome = new Label
            {
                Text = "Welcome to your dashboard",
                Font = new Font("Segoe UI", 16F),
                ForeColor = Color.DarkGreen,
                Location = new Point(400, 120),
                AutoSize = true
            };

            infoPanel = new Panel
            {
                BackColor = Color.LightBlue,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(250, 200),
                Size = new Size(700, 300)
            };

            Label lblInfo = new Label
            {
                Text = "Quick Stats:\n\n" +
                      "• Total Vehicles: 42\n" +
                      "• Available Vehicles: 28\n" +
                      "• Total Customers: 156\n" +
                      "• Monthly Revenue: $85,420",
                Font = new Font("Segoe UI", 14F),
                ForeColor = Color.DarkBlue,
                Location = new Point(20, 20),
                Size = new Size(660, 260),
                TextAlign = ContentAlignment.MiddleLeft
            };

            infoPanel.Controls.Add(lblInfo);
            tabHome.Controls.Add(lblSystemName);
            tabHome.Controls.Add(lblWelcome);
            tabHome.Controls.Add(infoPanel);
        }

        private void CreateVehiclesTab()
        {
            tabVehicles = new TabPage
            {
                Text = "Vehicles",
                BackColor = Color.WhiteSmoke
            };

            dgvVehicles = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(1100, 450),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false
            };

            btnAddVehicle = new Button
            {
                Text = "Add New Vehicle",
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(20, 520),
                Size = new Size(150, 40)
            };
            btnAddVehicle.Click += BtnAddVehicle_Click;

            btnEditVehicle = new Button
            {
                Text = "Edit Vehicle",
                BackColor = Color.Blue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(180, 520),
                Size = new Size(150, 40)
            };
            btnEditVehicle.Click += BtnEditVehicle_Click;

            btnDeleteVehicle = new Button
            {
                Text = "Delete Vehicle",
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(340, 520),
                Size = new Size(150, 40)
            };
            btnDeleteVehicle.Click += BtnDeleteVehicle_Click;

            txtSearchVehicle = new TextBox
            {
                PlaceholderText = "Search vehicles...",
                Location = new Point(700, 20),
                Size = new Size(250, 30)
            };

            btnSearchVehicle = new Button
            {
                Text = "Search",
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(960, 20),
                Size = new Size(100, 30)
            };
            btnSearchVehicle.Click += BtnSearchVehicle_Click;

            tabVehicles.Controls.Add(dgvVehicles);
            tabVehicles.Controls.Add(btnAddVehicle);
            tabVehicles.Controls.Add(btnEditVehicle);
            tabVehicles.Controls.Add(btnDeleteVehicle);
            tabVehicles.Controls.Add(txtSearchVehicle);
            tabVehicles.Controls.Add(btnSearchVehicle);
        }

        private void CreateCustomersTab()
        {
            tabCustomers = new TabPage
            {
                Text = "Customers",
                BackColor = Color.WhiteSmoke
            };

            dgvCustomers = new DataGridView
            {
                Location = new Point(20, 60),
                Size = new Size(1100, 450),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false
            };

            btnAddCustomer = new Button
            {
                Text = "Add New Customer",
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(20, 520),
                Size = new Size(150, 40)
            };
            btnAddCustomer.Click += BtnAddCustomer_Click;

            btnEditCustomer = new Button
            {
                Text = "Edit Customer",
                BackColor = Color.Blue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(180, 520),
                Size = new Size(150, 40)
            };
            btnEditCustomer.Click += BtnEditCustomer_Click;

            btnDeleteCustomer = new Button
            {
                Text = "Delete Customer",
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(340, 520),
                Size = new Size(150, 40)
            };
            btnDeleteCustomer.Click += BtnDeleteCustomer_Click;

            tabCustomers.Controls.Add(dgvCustomers);
            tabCustomers.Controls.Add(btnAddCustomer);
            tabCustomers.Controls.Add(btnEditCustomer);
            tabCustomers.Controls.Add(btnDeleteCustomer);
        }

        private void CreateReportsTab()
        {
            tabReports = new TabPage
            {
                Text = "Reports",
                BackColor = Color.WhiteSmoke
            };

            Label lblReportType = new Label
            {
                Text = "Report Type:",
                Location = new Point(20, 20),
                AutoSize = true
            };

            cmbReportType = new ComboBox
            {
                Location = new Point(120, 20),
                Size = new Size(200, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbReportType.Items.AddRange(new object[] {
                "Sales Report",
                "Inventory Report",
                "Customer Report",
                "Financial Report"
            });

            Label lblDateRange = new Label
            {
                Text = "Date Range:",
                Location = new Point(20, 60),
                AutoSize = true
            };

            dtpReportStart = new DateTimePicker
            {
                Location = new Point(120, 60),
                Size = new Size(150, 30),
                Format = DateTimePickerFormat.Short
            };

            Label lblTo = new Label
            {
                Text = "to",
                Location = new Point(280, 60),
                AutoSize = true
            };

            dtpReportEnd = new DateTimePicker
            {
                Location = new Point(310, 60),
                Size = new Size(150, 30),
                Format = DateTimePickerFormat.Short
            };

            btnGenerateReport = new Button
            {
                Text = "Generate Report",
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(480, 20),
                Size = new Size(150, 30)
            };
            btnGenerateReport.Click += BtnGenerateReport_Click;

            dgvReportData = new DataGridView
            {
                Location = new Point(20, 100),
                Size = new Size(1100, 450),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false
            };

            tabReports.Controls.Add(lblReportType);
            tabReports.Controls.Add(cmbReportType);
            tabReports.Controls.Add(lblDateRange);
            tabReports.Controls.Add(dtpReportStart);
            tabReports.Controls.Add(lblTo);
            tabReports.Controls.Add(dtpReportEnd);
            tabReports.Controls.Add(btnGenerateReport);
            tabReports.Controls.Add(dgvReportData);
        }

        private void LoadSampleData()
        {
            // Sample Vehicles Data
            DataTable vehiclesTable = new DataTable();
            vehiclesTable.Columns.Add("ID", typeof(int));
            vehiclesTable.Columns.Add("Make", typeof(string));
            vehiclesTable.Columns.Add("Model", typeof(string));
            vehiclesTable.Columns.Add("Year", typeof(int));
            vehiclesTable.Columns.Add("Price", typeof(decimal));
            vehiclesTable.Columns.Add("Status", typeof(string));
            vehiclesTable.Columns.Add("Last Updated", typeof(DateTime));

            vehiclesTable.Rows.Add(1, "Toyota", "Camry", 2022, 28500.00, "Available", DateTime.Now.AddDays(-2));
            vehiclesTable.Rows.Add(2, "Honda", "Accord", 2021, 26500.00, "Sold", DateTime.Now.AddDays(-5));
            vehiclesTable.Rows.Add(3, "Ford", "F-150", 2023, 42500.00, "Available", DateTime.Now.AddDays(-1));
            vehiclesTable.Rows.Add(4, "Chevrolet", "Silverado", 2022, 38500.00, "Available", DateTime.Now.AddDays(-3));
            vehiclesTable.Rows.Add(5, "BMW", "X5", 2023, 62500.00, "Reserved", DateTime.Now);

            dgvVehicles.DataSource = vehiclesTable;

            // Sample Customers Data
            DataTable customersTable = new DataTable();
            customersTable.Columns.Add("ID", typeof(int));
            customersTable.Columns.Add("Name", typeof(string));
            customersTable.Columns.Add("Email", typeof(string));
            customersTable.Columns.Add("Phone", typeof(string));
            customersTable.Columns.Add("Address", typeof(string));
            customersTable.Columns.Add("Last Purchase", typeof(DateTime));

            customersTable.Rows.Add(1, "John Smith", "john.smith@email.com", "555-123-4567", "123 Main St, Anytown", DateTime.Now.AddDays(-10));
            customersTable.Rows.Add(2, "Sarah Johnson", "sarah.j@email.com", "555-987-6543", "456 Oak Ave, Somewhere", DateTime.Now.AddDays(-5));
            customersTable.Rows.Add(3, "Michael Brown", "michael.b@email.com", "555-456-7890", "789 Pine Rd, Nowhere", DateTime.Now.AddDays(-2));
            customersTable.Rows.Add(4, "Emily Davis", "emily.d@email.com", "555-789-0123", "321 Elm St, Anywhere", DateTime.Now.AddDays(-1));

            dgvCustomers.DataSource = customersTable;
        }

        #region Event Handlers
        private void LogoutItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                LoginForm loginForm = new LoginForm();
                loginForm.FormClosed += (s, args) => this.Close();
                loginForm.Show();
            }
        }

        private void ExitItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit the application?", "Confirm Exit",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void AboutItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vehicle buying and selling System v2.0\n\nDeveloped for ESOFT Metro Campus\nLevel 3 Diploma in Information Technology",
                "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAddVehicle_Click(object sender, EventArgs e)
        {
            VehicleForm vehicleForm = new VehicleForm();
            if (vehicleForm.ShowDialog() == DialogResult.OK)
            {
                // Refresh vehicle list
                MessageBox.Show("Vehicle added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnEditVehicle_Click(object sender, EventArgs e)
        {
            if (dgvVehicles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a vehicle to edit.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            VehicleForm vehicleForm = new VehicleForm();
            if (vehicleForm.ShowDialog() == DialogResult.OK)
            {
                // Refresh vehicle list
                MessageBox.Show("Vehicle updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnDeleteVehicle_Click(object sender, EventArgs e)
        {
            if (dgvVehicles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a vehicle to delete.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this vehicle?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Delete vehicle logic
                MessageBox.Show("Vehicle deleted successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnSearchVehicle_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearchVehicle.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Please enter a search term.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Search logic
            MessageBox.Show($"Searching for: {searchTerm}", "Search",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnAddCustomer_Click(object sender, EventArgs e)
        {
            CustomerForm customerForm = new CustomerForm();
            if (customerForm.ShowDialog() == DialogResult.OK)
            {
                // Refresh customer list
                MessageBox.Show("Customer added successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnEditCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to edit.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CustomerForm customerForm = new CustomerForm();
            if (customerForm.ShowDialog() == DialogResult.OK)
            {
                // Refresh customer list
                MessageBox.Show("Customer updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a customer to delete.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Delete customer logic
                MessageBox.Show("Customer deleted successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            string reportType = cmbReportType.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(reportType))
            {
                MessageBox.Show("Please select a report type.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime startDate = dtpReportStart.Value;
            DateTime endDate = dtpReportEnd.Value;

            if (startDate > endDate)
            {
                MessageBox.Show("Start date cannot be after end date.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Generate report logic
            MessageBox.Show($"Generating {reportType} from {startDate:d} to {endDate:d}", "Report",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
    }
}