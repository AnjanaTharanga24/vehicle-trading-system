using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using VehicleTradingSystem.Models;

namespace VehicleTradingSystem.Forms
{
    public partial class DashboardForm : Form
    {
        private readonly Customer _currentCustomer;
        private MenuStrip menuStrip;
        private StatusStrip statusStrip;
        private TabControl mainTabControl;

        // Tab Pages
        private TabPage tabHome;
        private TabPage tabVehicles;
        private TabPage tabCustomers;
        private TabPage tabReports;

        public DashboardForm(Customer customer)
        {
            _currentCustomer = customer;
            InitializeDashboard();
            LoadSampleData();
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

            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(viewMenu);
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

            tab.Controls.Add(lblWelcome);
            return tab;
        }

        private TabPage CreateVehiclesTab()
        {
            TabPage tab = new TabPage("Vehicles");

            DataGridView dgv = new DataGridView();
            dgv.Dock = DockStyle.Fill;
            dgv.ReadOnly = true;

            tab.Controls.Add(dgv);
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

        private void LoadSampleData()
        {
            // Sample data loading for each tab
            ((DataGridView)tabVehicles.Controls[0]).DataSource = GetSampleVehicles();
            ((DataGridView)tabCustomers.Controls[0]).DataSource = GetSampleCustomers();
        }

        private DataTable GetSampleVehicles()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Make");
            dt.Columns.Add("Model");
            dt.Columns.Add("Price", typeof(decimal));
            dt.Rows.Add("Toyota", "Camry", 25000);
            dt.Rows.Add("Honda", "Accord", 27000);
            return dt;
        }

        private DataTable GetSampleCustomers()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Email");
            dt.Columns.Add("Phone");
            dt.Rows.Add(_currentCustomer.FirstName + " " + _currentCustomer.LastName,
                        _currentCustomer.Email,
                        _currentCustomer.MobilePhone);
            dt.Rows.Add("John Smith", "john@example.com", "555-1234");
            return dt;
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
    }
}