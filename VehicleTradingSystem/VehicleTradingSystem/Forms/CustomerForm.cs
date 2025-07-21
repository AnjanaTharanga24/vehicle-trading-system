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
            lblTitle = new Label();
            panelForm = new Panel();
            lblFirstName = new Label();
            txtFirstName = new TextBox();
            lblLastName = new Label();
            txtLastName = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblPhone = new Label();
            txtPhone = new MaskedTextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            lblCity = new Label();
            txtCity = new TextBox();
            lblState = new Label();
            cmbState = new ComboBox();
            lblZipCode = new Label();
            txtZipCode = new TextBox();
            lblCustomerSince = new Label();
            dtpCustomerSince = new DateTimePicker();
            btnSave = new Button();
            btnCancel = new Button();
            panelForm.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(100, 23);
            lblTitle.TabIndex = 0;
            // 
            // panelForm
            // 
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
            panelForm.Location = new Point(0, 0);
            panelForm.Name = "panelForm";
            panelForm.Size = new Size(200, 100);
            panelForm.TabIndex = 1;
            // 
            // lblFirstName
            // 
            lblFirstName.Location = new Point(0, 0);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(100, 23);
            lblFirstName.TabIndex = 0;
            // 
            // txtFirstName
            // 
            txtFirstName.Location = new Point(0, 0);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(100, 27);
            txtFirstName.TabIndex = 1;
            // 
            // lblLastName
            // 
            lblLastName.Location = new Point(0, 0);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(100, 23);
            lblLastName.TabIndex = 2;
            // 
            // txtLastName
            // 
            txtLastName.Location = new Point(0, 0);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(100, 27);
            txtLastName.TabIndex = 3;
            // 
            // lblEmail
            // 
            lblEmail.Location = new Point(0, 0);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(100, 23);
            lblEmail.TabIndex = 4;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(0, 0);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(100, 27);
            txtEmail.TabIndex = 5;
            // 
            // lblPhone
            // 
            lblPhone.Location = new Point(0, 0);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(100, 23);
            lblPhone.TabIndex = 6;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(0, 0);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(100, 27);
            txtPhone.TabIndex = 7;
            // 
            // lblAddress
            // 
            lblAddress.Location = new Point(0, 0);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(100, 23);
            lblAddress.TabIndex = 8;
            // 
            // txtAddress
            // 
            txtAddress.Location = new Point(0, 0);
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(100, 27);
            txtAddress.TabIndex = 9;
            // 
            // lblCity
            // 
            lblCity.Location = new Point(0, 0);
            lblCity.Name = "lblCity";
            lblCity.Size = new Size(100, 23);
            lblCity.TabIndex = 10;
            // 
            // txtCity
            // 
            txtCity.Location = new Point(0, 0);
            txtCity.Name = "txtCity";
            txtCity.Size = new Size(100, 27);
            txtCity.TabIndex = 11;
            // 
            // lblState
            // 
            lblState.Location = new Point(0, 0);
            lblState.Name = "lblState";
            lblState.Size = new Size(100, 23);
            lblState.TabIndex = 12;
            // 
            // cmbState
            // 
            cmbState.Location = new Point(0, 0);
            cmbState.Name = "cmbState";
            cmbState.Size = new Size(121, 28);
            cmbState.TabIndex = 13;
            // 
            // lblZipCode
            // 
            lblZipCode.Location = new Point(0, 0);
            lblZipCode.Name = "lblZipCode";
            lblZipCode.Size = new Size(100, 23);
            lblZipCode.TabIndex = 14;
            // 
            // txtZipCode
            // 
            txtZipCode.Location = new Point(0, 0);
            txtZipCode.Name = "txtZipCode";
            txtZipCode.Size = new Size(100, 27);
            txtZipCode.TabIndex = 15;
            // 
            // lblCustomerSince
            // 
            lblCustomerSince.Location = new Point(0, 0);
            lblCustomerSince.Name = "lblCustomerSince";
            lblCustomerSince.Size = new Size(100, 23);
            lblCustomerSince.TabIndex = 16;
            // 
            // dtpCustomerSince
            // 
            dtpCustomerSince.Location = new Point(0, 0);
            dtpCustomerSince.Name = "dtpCustomerSince";
            dtpCustomerSince.Size = new Size(200, 27);
            dtpCustomerSince.TabIndex = 17;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(0, 0);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 18;
            btnSave.Click += BtnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(0, 0);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 19;
            btnCancel.Click += BtnCancel_Click;
            // 
            // CustomerForm
            // 
            AcceptButton = btnSave;
            BackColor = Color.WhiteSmoke;
            CancelButton = btnCancel;
            ClientSize = new Size(600, 550);
            Controls.Add(lblTitle);
            Controls.Add(panelForm);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CustomerForm";
            Padding = new Padding(10);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add New Customer";
            Load += CustomerForm_Load;
            panelForm.ResumeLayout(false);
            panelForm.PerformLayout();
            ResumeLayout(false);
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

        private void CustomerForm_Load(object sender, EventArgs e)
        {

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