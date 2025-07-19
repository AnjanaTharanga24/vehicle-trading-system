using System;
using System.Drawing;
using System.Windows.Forms;

namespace VehicleTradingSystem.Forms
{
    public partial class CustomerForm : Form
    {
        // Form controls
        private Label lblTitle;
        private Label lblFirstName;
        private TextBox txtFirstName;
        private Label lblLastName;
        private TextBox txtLastName;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPhone;
        private MaskedTextBox txtPhone;
        private Label lblAddress;
        private TextBox txtAddress;
        private Label lblCity;
        private TextBox txtCity;
        private Label lblState;
        private ComboBox cmbState;
        private Label lblZipCode;
        private TextBox txtZipCode;
        private Label lblCustomerSince;
        private DateTimePicker dtpCustomerSince;
        private Button btnSave;
        private Button btnCancel;
        private Panel panelForm;

        public bool IsEditMode { get; set; }
        public int CustomerId { get; set; }

        public CustomerForm()
        {
            InitializeComponent();
            IsEditMode = false;
            CustomerId = 0;
        }

        public CustomerForm(int customerId) : this()
        {
            IsEditMode = true;
            CustomerId = customerId;
            lblTitle.Text = "Edit Customer";
            this.Text = "Edit Customer";
            // Load customer data here
            LoadCustomerData(customerId);
        }

        private void InitializeComponent()
        {
            // Main Form Settings
            this.ClientSize = new Size(600, 550);
            this.Text = "Add New Customer";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.WhiteSmoke;
            this.Padding = new Padding(10);

            // Title Label
            lblTitle = new Label
            {
                Text = "Add New Customer",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Form Panel
            panelForm = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            // First Name
            lblFirstName = new Label
            {
                Text = "First Name:",
                Location = new Point(30, 20),
                Width = 120,
                TextAlign = ContentAlignment.MiddleRight
            };

            txtFirstName = new TextBox
            {
                Location = new Point(160, 20),
                Size = new Size(350, 30),
                MaxLength = 50
            };

            // Last Name
            lblLastName = new Label
            {
                Text = "Last Name:",
                Location = new Point(30, 60),
                Width = 120,
                TextAlign = ContentAlignment.MiddleRight
            };

            txtLastName = new TextBox
            {
                Location = new Point(160, 60),
                Size = new Size(350, 30),
                MaxLength = 50
            };

            // Email
            lblEmail = new Label
            {
                Text = "Email:",
                Location = new Point(30, 100),
                Width = 120,
                TextAlign = ContentAlignment.MiddleRight
            };

            txtEmail = new TextBox
            {
                Location = new Point(160, 100),
                Size = new Size(350, 30),
                MaxLength = 100
            };

            // Phone
            lblPhone = new Label
            {
                Text = "Phone:",
                Location = new Point(30, 140),
                Width = 120,
                TextAlign = ContentAlignment.MiddleRight
            };

            txtPhone = new MaskedTextBox
            {
                Location = new Point(160, 140),
                Size = new Size(350, 30),
                Mask = "(999) 000-0000"
            };

            // Address
            lblAddress = new Label
            {
                Text = "Address:",
                Location = new Point(30, 180),
                Width = 120,
                TextAlign = ContentAlignment.MiddleRight
            };

            txtAddress = new TextBox
            {
                Location = new Point(160, 180),
                Size = new Size(350, 30),
                MaxLength = 100
            };

            // City
            lblCity = new Label
            {
                Text = "City:",
                Location = new Point(30, 220),
                Width = 120,
                TextAlign = ContentAlignment.MiddleRight
            };

            txtCity = new TextBox
            {
                Location = new Point(160, 220),
                Size = new Size(350, 30),
                MaxLength = 50
            };

            // State
            lblState = new Label
            {
                Text = "State:",
                Location = new Point(30, 260),
                Width = 120,
                TextAlign = ContentAlignment.MiddleRight
            };

            cmbState = new ComboBox
            {
                Location = new Point(160, 260),
                Size = new Size(150, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            LoadStates();

            // Zip Code
            lblZipCode = new Label
            {
                Text = "Zip Code:",
                Location = new Point(30, 300),
                Width = 120,
                TextAlign = ContentAlignment.MiddleRight
            };

            txtZipCode = new TextBox
            {
                Location = new Point(160, 300),
                Size = new Size(150, 30),
                MaxLength = 10
            };

            // Customer Since
            lblCustomerSince = new Label
            {
                Text = "Customer Since:",
                Location = new Point(30, 340),
                Width = 120,
                TextAlign = ContentAlignment.MiddleRight
            };

            dtpCustomerSince = new DateTimePicker
            {
                Location = new Point(160, 340),
                Size = new Size(150, 30),
                Format = DateTimePickerFormat.Short
            };

            // Buttons
            btnSave = new Button
            {
                Text = "Save",
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(160, 400),
                Size = new Size(120, 40),
                DialogResult = DialogResult.OK
            };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button
            {
                Text = "Cancel",
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(300, 400),
                Size = new Size(120, 40),
                DialogResult = DialogResult.Cancel
            };
            btnCancel.Click += BtnCancel_Click;

            // Add controls to panel
            panelForm.Controls.Add(lblFirstName);
            panelForm.Controls.Add(txtFirstName);
            panelForm.Controls.Add(lblLastName);
            panelForm.Controls.Add(txtLastName);
            panelForm.Controls.Add(lblEmail);
            panelForm.Controls.Add(txtEmail);
            panelForm.Controls.Add(lblPhone);
            panelForm.Controls.Add(txtPhone);
            panelForm.Controls.Add(lblAddress);
            panelForm.Controls.Add(txtAddress);
            panelForm.Controls.Add(lblCity);
            panelForm.Controls.Add(txtCity);
            panelForm.Controls.Add(lblState);
            panelForm.Controls.Add(cmbState);
            panelForm.Controls.Add(lblZipCode);
            panelForm.Controls.Add(txtZipCode);
            panelForm.Controls.Add(lblCustomerSince);
            panelForm.Controls.Add(dtpCustomerSince);
            panelForm.Controls.Add(btnSave);
            panelForm.Controls.Add(btnCancel);

            // Add controls to form
            this.Controls.Add(lblTitle);
            this.Controls.Add(panelForm);

            // Set accept and cancel buttons
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }

        private void LoadStates()
        {
            string[] states = {
                "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA",
                "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD",
                "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ",
                "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC",
                "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY"
            };

            cmbState.Items.AddRange(states);
        }

        private void LoadCustomerData(int customerId)
        {
            // In a real application, you would load data from your database here
            // This is just sample data for demonstration
            txtFirstName.Text = "John";
            txtLastName.Text = "Smith";
            txtEmail.Text = "john.smith@example.com";
            txtPhone.Text = "(555) 123-4567";
            txtAddress.Text = "123 Main Street";
            txtCity.Text = "Anytown";
            cmbState.SelectedItem = "CA";
            txtZipCode.Text = "90210";
            dtpCustomerSince.Value = DateTime.Now.AddYears(-1);
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter first name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please enter last name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter email address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text) || txtPhone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "").Length < 10)
            {
                MessageBox.Show("Please enter a valid phone number.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Please enter address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAddress.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCity.Text))
            {
                MessageBox.Show("Please enter city.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCity.Focus();
                return false;
            }

            if (cmbState.SelectedIndex == -1)
            {
                MessageBox.Show("Please select state.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbState.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtZipCode.Text))
            {
                MessageBox.Show("Please enter zip code.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtZipCode.Focus();
                return false;
            }

            return true;
        }

        private void SaveCustomer()
        {
            // In a real application, you would save to database here
            // This is just a demonstration
            string message = IsEditMode ?
                $"Customer #{CustomerId} updated successfully!" :
                "New customer added successfully!";

            MessageBox.Show(message, "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region Event Handlers
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
            {
                this.DialogResult = DialogResult.None;
                return;
            }

            try
            {
                SaveCustomer();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving customer: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.None;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Public Properties to Access Form Data
        public string FirstName => txtFirstName.Text.Trim();
        public string LastName => txtLastName.Text.Trim();
        public string Email => txtEmail.Text.Trim();
        public string Phone => txtPhone.Text;
        public string Address => txtAddress.Text.Trim();
        public string City => txtCity.Text.Trim();
        public string State => cmbState.SelectedItem?.ToString();
        public string ZipCode => txtZipCode.Text.Trim();
        public DateTime CustomerSince => dtpCustomerSince.Value;
        #endregion
    }
}