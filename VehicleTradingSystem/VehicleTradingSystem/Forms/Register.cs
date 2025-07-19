using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace VehicleTradingSystem.Forms
{
    public partial class RegistrationForm : Form
    {
        private Label lblTitle;
        private GroupBox gbCustomerRegistration;
        private GroupBox gbPersonalDetails;
        private GroupBox gbContactDetails;
        private GroupBox gbPreferences;
        private GroupBox gbAccountDetails;

        // Personal Details
        private Label lblCustomerID;
        private Label lblFirstName;
        private Label lblLastName;
        private Label lblNIC;
        private Label lblGender;
        private Label lblDateOfBirth;

        private ComboBox cmbCustomerID;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private TextBox txtNIC;
        private RadioButton rbMale;
        private RadioButton rbFemale;
        private DateTimePicker dtpDateOfBirth;

        // Contact Details
        private Label lblEmail;
        private Label lblMobilePhone;
        private Label lblAddress;

        private TextBox txtEmail;
        private TextBox txtMobilePhone;
        private TextBox txtAddress;

        // Preferences
        private Label lblVehicleType;
        private Label lblBudgetRange;

        private ComboBox cmbVehicleType;
        private ComboBox cmbBudgetRange;

        // Account Details
        private Label lblUsername;
        private Label lblPassword;
        private Label lblConfirmPassword;

        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;

        // Buttons
        private Button btnRegister;
        private Button btnUpdate;
        private Button btnClear;
        private Button btnDelete;

        // Navigation Links
        private LinkLabel lnkBackToLogin;
        private LinkLabel lnkExit;

        public RegistrationForm()
        {
            InitializeComponent();
            PopulateComboBoxes();
        }

        private void InitializeComponent()
        {
            // Initialize all controls
            this.lblTitle = new Label();
            this.gbCustomerRegistration = new GroupBox();
            this.gbPersonalDetails = new GroupBox();
            this.gbContactDetails = new GroupBox();
            this.gbPreferences = new GroupBox();
            this.gbAccountDetails = new GroupBox();

            // Personal Details Controls
            this.lblCustomerID = new Label();
            this.lblFirstName = new Label();
            this.lblLastName = new Label();
            this.lblNIC = new Label();
            this.lblGender = new Label();
            this.lblDateOfBirth = new Label();

            this.cmbCustomerID = new ComboBox();
            this.txtFirstName = new TextBox();
            this.txtLastName = new TextBox();
            this.txtNIC = new TextBox();
            this.rbMale = new RadioButton();
            this.rbFemale = new RadioButton();
            this.dtpDateOfBirth = new DateTimePicker();

            // Contact Details Controls
            this.lblEmail = new Label();
            this.lblMobilePhone = new Label();
            this.lblAddress = new Label();

            this.txtEmail = new TextBox();
            this.txtMobilePhone = new TextBox();
            this.txtAddress = new TextBox();

            // Preferences Controls
            this.lblVehicleType = new Label();
            this.lblBudgetRange = new Label();

            this.cmbVehicleType = new ComboBox();
            this.cmbBudgetRange = new ComboBox();

            // Account Details Controls
            this.lblUsername = new Label();
            this.lblPassword = new Label();
            this.lblConfirmPassword = new Label();

            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.txtConfirmPassword = new TextBox();

            // Buttons
            this.btnRegister = new Button();
            this.btnUpdate = new Button();
            this.btnClear = new Button();
            this.btnDelete = new Button();

            // Navigation Links
            this.lnkBackToLogin = new LinkLabel();
            this.lnkExit = new LinkLabel();

            this.SuspendLayout();

            // ==================== FORM SETTINGS ====================
            this.ClientSize = new Size(900, 800);
            this.Text = "Customer Registration - Vehicle buying and selling System";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.White;
            this.Font = new Font("Times New Roman", 10F);

            // ==================== TITLE ====================
            this.lblTitle.Text = "Vehicle buying and selling System - Customer Registration";
            this.lblTitle.Font = new Font("Times New Roman", 20F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.DarkBlue;
            this.lblTitle.Location = new Point(180, 20);
            this.lblTitle.Size = new Size(540, 31);
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // ==================== PERSONAL DETAILS GROUP ====================
            this.gbPersonalDetails.Text = "Personal Details";
            this.gbPersonalDetails.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            this.gbPersonalDetails.Location = new Point(30, 70);
            this.gbPersonalDetails.Size = new Size(400, 280);
            this.gbPersonalDetails.ForeColor = Color.DarkGreen;

            // Customer ID
            this.lblCustomerID.Text = "Customer ID:";
            this.lblCustomerID.Location = new Point(20, 30);
            this.lblCustomerID.Size = new Size(100, 20);
            this.lblCustomerID.Font = new Font("Times New Roman", 11F);

            this.cmbCustomerID.Location = new Point(140, 28);
            this.cmbCustomerID.Size = new Size(200, 24);
            this.cmbCustomerID.Font = new Font("Times New Roman", 11F);
            this.cmbCustomerID.DropDownStyle = ComboBoxStyle.DropDownList;

            // First Name
            this.lblFirstName.Text = "First Name:";
            this.lblFirstName.Location = new Point(20, 65);
            this.lblFirstName.Size = new Size(100, 20);
            this.lblFirstName.Font = new Font("Times New Roman", 11F);

            this.txtFirstName.Location = new Point(140, 63);
            this.txtFirstName.Size = new Size(200, 24);
            this.txtFirstName.Font = new Font("Times New Roman", 11F);

            // Last Name
            this.lblLastName.Text = "Last Name:";
            this.lblLastName.Location = new Point(20, 100);
            this.lblLastName.Size = new Size(100, 20);
            this.lblLastName.Font = new Font("Times New Roman", 11F);

            this.txtLastName.Location = new Point(140, 98);
            this.txtLastName.Size = new Size(200, 24);
            this.txtLastName.Font = new Font("Times New Roman", 11F);

            // NIC
            this.lblNIC.Text = "NIC / National ID:";
            this.lblNIC.Location = new Point(20, 135);
            this.lblNIC.Size = new Size(115, 20);
            this.lblNIC.Font = new Font("Times New Roman", 11F);

            this.txtNIC.Location = new Point(140, 133);
            this.txtNIC.Size = new Size(200, 24);
            this.txtNIC.Font = new Font("Times New Roman", 11F);

            // Gender
            this.lblGender.Text = "Gender:";
            this.lblGender.Location = new Point(20, 170);
            this.lblGender.Size = new Size(100, 20);
            this.lblGender.Font = new Font("Times New Roman", 11F);

            this.rbMale.Text = "Male";
            this.rbMale.Location = new Point(140, 168);
            this.rbMale.Size = new Size(60, 24);
            this.rbMale.Font = new Font("Times New Roman", 11F);
            this.rbMale.Checked = true;

            this.rbFemale.Text = "Female";
            this.rbFemale.Location = new Point(220, 168);
            this.rbFemale.Size = new Size(80, 24);
            this.rbFemale.Font = new Font("Times New Roman", 11F);

            // Date of Birth
            this.lblDateOfBirth.Text = "Date of Birth:";
            this.lblDateOfBirth.Location = new Point(20, 205);
            this.lblDateOfBirth.Size = new Size(100, 20);
            this.lblDateOfBirth.Font = new Font("Times New Roman", 11F);

            this.dtpDateOfBirth.Location = new Point(140, 203);
            this.dtpDateOfBirth.Size = new Size(200, 24);
            this.dtpDateOfBirth.Font = new Font("Times New Roman", 11F);
            this.dtpDateOfBirth.Format = DateTimePickerFormat.Short;
            this.dtpDateOfBirth.Value = DateTime.Now.AddYears(-25);

            // Add controls to Personal Details group
            this.gbPersonalDetails.Controls.Add(this.lblCustomerID);
            this.gbPersonalDetails.Controls.Add(this.cmbCustomerID);
            this.gbPersonalDetails.Controls.Add(this.lblFirstName);
            this.gbPersonalDetails.Controls.Add(this.txtFirstName);
            this.gbPersonalDetails.Controls.Add(this.lblLastName);
            this.gbPersonalDetails.Controls.Add(this.txtLastName);
            this.gbPersonalDetails.Controls.Add(this.lblNIC);
            this.gbPersonalDetails.Controls.Add(this.txtNIC);
            this.gbPersonalDetails.Controls.Add(this.lblGender);
            this.gbPersonalDetails.Controls.Add(this.rbMale);
            this.gbPersonalDetails.Controls.Add(this.rbFemale);
            this.gbPersonalDetails.Controls.Add(this.lblDateOfBirth);
            this.gbPersonalDetails.Controls.Add(this.dtpDateOfBirth);

            // ==================== CONTACT DETAILS GROUP ====================
            this.gbContactDetails.Text = "Contact Details";
            this.gbContactDetails.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            this.gbContactDetails.Location = new Point(450, 70);
            this.gbContactDetails.Size = new Size(400, 200);
            this.gbContactDetails.ForeColor = Color.DarkGreen;

            // Email
            this.lblEmail.Text = "Email:";
            this.lblEmail.Location = new Point(20, 30);
            this.lblEmail.Size = new Size(100, 20);
            this.lblEmail.Font = new Font("Times New Roman", 11F);

            this.txtEmail.Location = new Point(140, 28);
            this.txtEmail.Size = new Size(230, 24);
            this.txtEmail.Font = new Font("Times New Roman", 11F);

            // Mobile Phone
            this.lblMobilePhone.Text = "Mobile Number:";
            this.lblMobilePhone.Location = new Point(20, 65);
            this.lblMobilePhone.Size = new Size(110, 20);
            this.lblMobilePhone.Font = new Font("Times New Roman", 11F);

            this.txtMobilePhone.Location = new Point(140, 63);
            this.txtMobilePhone.Size = new Size(230, 24);
            this.txtMobilePhone.Font = new Font("Times New Roman", 11F);

            // Address
            this.lblAddress.Text = "Address:";
            this.lblAddress.Location = new Point(20, 100);
            this.lblAddress.Size = new Size(100, 20);
            this.lblAddress.Font = new Font("Times New Roman", 11F);

            this.txtAddress.Location = new Point(140, 98);
            this.txtAddress.Size = new Size(230, 80);
            this.txtAddress.Font = new Font("Times New Roman", 11F);
            this.txtAddress.Multiline = true;
            this.txtAddress.ScrollBars = ScrollBars.Vertical;

            // Add controls to Contact Details group
            this.gbContactDetails.Controls.Add(this.lblEmail);
            this.gbContactDetails.Controls.Add(this.txtEmail);
            this.gbContactDetails.Controls.Add(this.lblMobilePhone);
            this.gbContactDetails.Controls.Add(this.txtMobilePhone);
            this.gbContactDetails.Controls.Add(this.lblAddress);
            this.gbContactDetails.Controls.Add(this.txtAddress);

            // ==================== PREFERENCES GROUP ====================
            this.gbPreferences.Text = "Vehicle Preferences (Optional)";
            this.gbPreferences.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            this.gbPreferences.Location = new Point(450, 290);
            this.gbPreferences.Size = new Size(400, 120);
            this.gbPreferences.ForeColor = Color.DarkGreen;

            // Vehicle Type
            this.lblVehicleType.Text = "Preferred Vehicle:";
            this.lblVehicleType.Location = new Point(20, 35);
            this.lblVehicleType.Size = new Size(120, 20);
            this.lblVehicleType.Font = new Font("Times New Roman", 11F);

            this.cmbVehicleType.Location = new Point(150, 33);
            this.cmbVehicleType.Size = new Size(220, 24);
            this.cmbVehicleType.Font = new Font("Times New Roman", 11F);
            this.cmbVehicleType.DropDownStyle = ComboBoxStyle.DropDownList;

            // Budget Range
            this.lblBudgetRange.Text = "Budget Range:";
            this.lblBudgetRange.Location = new Point(20, 70);
            this.lblBudgetRange.Size = new Size(120, 20);
            this.lblBudgetRange.Font = new Font("Times New Roman", 11F);

            this.cmbBudgetRange.Location = new Point(150, 68);
            this.cmbBudgetRange.Size = new Size(220, 24);
            this.cmbBudgetRange.Font = new Font("Times New Roman", 11F);
            this.cmbBudgetRange.DropDownStyle = ComboBoxStyle.DropDownList;

            // Add controls to Preferences group
            this.gbPreferences.Controls.Add(this.lblVehicleType);
            this.gbPreferences.Controls.Add(this.cmbVehicleType);
            this.gbPreferences.Controls.Add(this.lblBudgetRange);
            this.gbPreferences.Controls.Add(this.cmbBudgetRange);

            // ==================== ACCOUNT DETAILS GROUP ====================
            this.gbAccountDetails.Text = "Account Details";
            this.gbAccountDetails.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            this.gbAccountDetails.Location = new Point(30, 370);
            this.gbAccountDetails.Size = new Size(400, 160);
            this.gbAccountDetails.ForeColor = Color.DarkGreen;

            // Username
            this.lblUsername.Text = "Username:";
            this.lblUsername.Location = new Point(20, 35);
            this.lblUsername.Size = new Size(100, 20);
            this.lblUsername.Font = new Font("Times New Roman", 11F);

            this.txtUsername.Location = new Point(140, 33);
            this.txtUsername.Size = new Size(200, 24);
            this.txtUsername.Font = new Font("Times New Roman", 11F);

            // Password
            this.lblPassword.Text = "Password:";
            this.lblPassword.Location = new Point(20, 70);
            this.lblPassword.Size = new Size(100, 20);
            this.lblPassword.Font = new Font("Times New Roman", 11F);

            this.txtPassword.Location = new Point(140, 68);
            this.txtPassword.Size = new Size(200, 24);
            this.txtPassword.Font = new Font("Times New Roman", 11F);
            this.txtPassword.PasswordChar = '*';

            // Confirm Password
            this.lblConfirmPassword.Text = "Confirm Password:";
            this.lblConfirmPassword.Location = new Point(20, 105);
            this.lblConfirmPassword.Size = new Size(115, 20);
            this.lblConfirmPassword.Font = new Font("Times New Roman", 11F);

            this.txtConfirmPassword.Location = new Point(140, 103);
            this.txtConfirmPassword.Size = new Size(200, 24);
            this.txtConfirmPassword.Font = new Font("Times New Roman", 11F);
            this.txtConfirmPassword.PasswordChar = '*';

            // Add controls to Account Details group
            this.gbAccountDetails.Controls.Add(this.lblUsername);
            this.gbAccountDetails.Controls.Add(this.txtUsername);
            this.gbAccountDetails.Controls.Add(this.lblPassword);
            this.gbAccountDetails.Controls.Add(this.txtPassword);
            this.gbAccountDetails.Controls.Add(this.lblConfirmPassword);
            this.gbAccountDetails.Controls.Add(this.txtConfirmPassword);

            // ==================== BUTTONS ====================
            this.btnRegister.Text = "Register";
            this.btnRegister.Location = new Point(450, 430);
            this.btnRegister.Size = new Size(100, 40);
            this.btnRegister.BackColor = Color.Green;
            this.btnRegister.ForeColor = Color.White;
            this.btnRegister.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            this.btnRegister.FlatStyle = FlatStyle.Flat;
            this.btnRegister.Click += new EventHandler(this.btnRegister_Click);

            this.btnUpdate.Text = "Update";
            this.btnUpdate.Location = new Point(570, 430);
            this.btnUpdate.Size = new Size(100, 40);
            this.btnUpdate.BackColor = Color.Blue;
            this.btnUpdate.ForeColor = Color.White;
            this.btnUpdate.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            this.btnUpdate.FlatStyle = FlatStyle.Flat;
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);

            this.btnClear.Text = "Clear";
            this.btnClear.Location = new Point(450, 490);
            this.btnClear.Size = new Size(100, 40);
            this.btnClear.BackColor = Color.Orange;
            this.btnClear.ForeColor = Color.White;
            this.btnClear.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            this.btnClear.FlatStyle = FlatStyle.Flat;
            this.btnClear.Click += new EventHandler(this.btnClear_Click);

            this.btnDelete.Text = "Delete";
            this.btnDelete.Location = new Point(570, 490);
            this.btnDelete.Size = new Size(100, 40);
            this.btnDelete.BackColor = Color.Red;
            this.btnDelete.ForeColor = Color.White;
            this.btnDelete.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            this.btnDelete.FlatStyle = FlatStyle.Flat;
            this.btnDelete.Click += new EventHandler(this.btnDelete_Click);

            // ==================== NAVIGATION LINKS ====================
            this.lnkBackToLogin.Text = "← Back to Login";
            this.lnkBackToLogin.Location = new Point(30, 560);
            this.lnkBackToLogin.Size = new Size(120, 20);
            this.lnkBackToLogin.Font = new Font("Times New Roman", 11F);
            this.lnkBackToLogin.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnkBackToLogin_LinkClicked);

            this.lnkExit.Text = "Exit Application";
            this.lnkExit.Location = new Point(160, 560);
            this.lnkExit.Size = new Size(120, 20);
            this.lnkExit.Font = new Font("Times New Roman", 11F);
            this.lnkExit.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnkExit_LinkClicked);

            // ==================== ADD CONTROLS TO FORM ====================
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.gbPersonalDetails);
            this.Controls.Add(this.gbContactDetails);
            this.Controls.Add(this.gbPreferences);
            this.Controls.Add(this.gbAccountDetails);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lnkBackToLogin);
            this.Controls.Add(this.lnkExit);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void PopulateComboBoxes()
        {
            // Populate Customer ID ComboBox with auto-generated IDs
            for (int i = 1001; i <= 1100; i++)
            {
                cmbCustomerID.Items.Add("CUST" + i.ToString());
            }
            if (cmbCustomerID.Items.Count > 0)
                cmbCustomerID.SelectedIndex = 0;

            // Populate Vehicle Type ComboBox
            cmbVehicleType.Items.AddRange(new string[] {
                "Select Vehicle Type",
                "Sedan",
                "SUV",
                "Hatchback",
                "Truck",
                "Van",
                "Motorcycle",
                "Convertible",
                "Coupe",
                "Station Wagon"
            });
            cmbVehicleType.SelectedIndex = 0;

            // Populate Budget Range ComboBox
            cmbBudgetRange.Items.AddRange(new string[] {
                "Select Budget Range",
                "Under $10,000",
                "$10,000 - $20,000",
                "$20,000 - $35,000",
                "$35,000 - $50,000",
                "$50,000 - $75,000",
                "$75,000 - $100,000",
                "Above $100,000"
            });
            cmbBudgetRange.SelectedIndex = 0;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                // Here you would typically save to database
                MessageBox.Show($"Customer registration successful!\n\n" +
                    $"Customer ID: {cmbCustomerID.Text}\n" +
                    $"Name: {txtFirstName.Text} {txtLastName.Text}\n" +
                    $"Email: {txtEmail.Text}\n" +
                    $"Mobile: {txtMobilePhone.Text}",
                    "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearForm();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                DialogResult result = MessageBox.Show("Are you sure you want to update this customer record?",
                    "Update Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    MessageBox.Show("Customer record updated successfully!",
                        "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbCustomerID.Text))
            {
                MessageBox.Show("Please select a Customer ID to delete.", "Delete Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Are you sure you want to delete customer {cmbCustomerID.Text}?\n\nThis action cannot be undone!",
                "Delete Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Customer record deleted successfully!", "Delete Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
            }
        }

        private void lnkBackToLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.FormClosed += (s, args) => this.Close();
            loginForm.Show();
        }

        private void lnkExit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit the application?",
                "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private bool ValidateForm()
        {
            // Check required fields
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter First Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please enter Last Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNIC.Text))
            {
                MessageBox.Show("Please enter NIC/National ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNIC.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter Email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // Validate email format
            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMobilePhone.Text))
            {
                MessageBox.Show("Please enter Mobile Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMobilePhone.Focus();
                return false;
            }

            // Validate mobile number (basic validation)
            if (!IsValidMobileNumber(txtMobilePhone.Text))
            {
                MessageBox.Show("Please enter a valid mobile number (digits only, 10-15 characters).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMobilePhone.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter Username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter Password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            if (txtPassword.Text.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Password and Confirm Password do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                return emailRegex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidMobileNumber(string mobile)
        {
            return Regex.IsMatch(mobile, @"^\d{10,15}$");
        }

        private void ClearForm()
        {
            // Clear Personal Details
            if (cmbCustomerID.Items.Count > 0)
                cmbCustomerID.SelectedIndex = 0;
            txtFirstName.Clear();
            txtLastName.Clear();
            txtNIC.Clear();
            rbMale.Checked = true;
            dtpDateOfBirth.Value = DateTime.Now.AddYears(-25);

            // Clear Contact Details
            txtEmail.Clear();
            txtMobilePhone.Clear();
            txtAddress.Clear();

            // Clear Preferences
            cmbVehicleType.SelectedIndex = 0;
            cmbBudgetRange.SelectedIndex = 0;

            // Clear Account Details
            txtUsername.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();

            // Set focus to first field
            txtFirstName.Focus();
        }
    }
}