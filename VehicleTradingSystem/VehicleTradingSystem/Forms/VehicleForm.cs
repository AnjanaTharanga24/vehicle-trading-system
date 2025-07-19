using System;
using System.Drawing;
using System.Windows.Forms;

namespace VehicleTradingSystem.Forms
{
    public partial class VehicleForm : Form
    {
        // Form controls
        private Label lblTitle;
        private GroupBox gbVehicleDetails;
        private Label lblMake;
        private Label lblModel;
        private Label lblYear;
        private Label lblPrice;
        private Label lblMileage;
        private Label lblColor;
        private Label lblVIN;
        private Label lblStatus;
        private Label lblDescription;

        private TextBox txtMake;
        private TextBox txtModel;
        private NumericUpDown nudYear;
        private TextBox txtPrice;
        private TextBox txtMileage;
        private ComboBox cmbColor;
        private TextBox txtVIN;
        private ComboBox cmbStatus;
        private TextBox txtDescription;

        private Button btnSave;
        private Button btnCancel;
        private Button btnUploadImage;

        public VehicleForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Main Form Settings
            this.Text = "Add New Vehicle";
            this.ClientSize = new Size(600, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.WhiteSmoke;

            // Title Label
            lblTitle = new Label
            {
                Text = "Add New Vehicle",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(20, 20),
                AutoSize = true
            };

            // Vehicle Details GroupBox
            gbVehicleDetails = new GroupBox
            {
                Text = "Vehicle Details",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(20, 60),
                Size = new Size(550, 400)
            };

            // Labels
            lblMake = CreateLabel("Make:", 20, 30);
            lblModel = CreateLabel("Model:", 20, 70);
            lblYear = CreateLabel("Year:", 20, 110);
            lblPrice = CreateLabel("Price ($):", 20, 150);
            lblMileage = CreateLabel("Mileage:", 300, 150);
            lblColor = CreateLabel("Color:", 300, 110);
            lblVIN = CreateLabel("VIN:", 300, 30);
            lblStatus = CreateLabel("Status:", 300, 70);
            lblDescription = CreateLabel("Description:", 20, 190);

            // Input Controls
            txtMake = CreateTextBox(120, 30, 150);
            txtModel = CreateTextBox(120, 70, 150);

            nudYear = new NumericUpDown
            {
                Location = new Point(120, 110),
                Width = 150,
                Minimum = 1990,
                Maximum = DateTime.Now.Year + 1,
                Value = DateTime.Now.Year
            };

            txtPrice = CreateTextBox(120, 150, 150);
            txtMileage = CreateTextBox(380, 150, 150);

            cmbColor = new ComboBox
            {
                Location = new Point(380, 110),
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Items = { "Black", "White", "Red", "Blue", "Silver", "Gray", "Green", "Other" }
            };

            txtVIN = CreateTextBox(380, 30, 150);

            cmbStatus = new ComboBox
            {
                Location = new Point(380, 70),
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Items = { "Available", "Sold", "Reserved", "In Service" }
            };

            txtDescription = new TextBox
            {
                Location = new Point(120, 190),
                Width = 410,
                Height = 100,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            // Buttons
            btnUploadImage = new Button
            {
                Text = "Upload Image",
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(20, 310),
                Size = new Size(120, 30)
            };
            btnUploadImage.Click += BtnUploadImage_Click;

            btnSave = new Button
            {
                Text = "Save",
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(350, 310),
                Size = new Size(80, 30)
            };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button
            {
                Text = "Cancel",
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(450, 310),
                Size = new Size(80, 30)
            };
            btnCancel.Click += BtnCancel_Click;

            // Add controls to group box
            gbVehicleDetails.Controls.AddRange(new Control[] {
                lblMake, lblModel, lblYear, lblPrice, lblMileage, lblColor, lblVIN, lblStatus, lblDescription,
                txtMake, txtModel, nudYear, txtPrice, txtMileage, cmbColor, txtVIN, cmbStatus, txtDescription,
                btnUploadImage, btnSave, btnCancel
            });

            // Add controls to form
            this.Controls.Add(lblTitle);
            this.Controls.Add(gbVehicleDetails);
        }

        private Label CreateLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F)
            };
        }

        private TextBox CreateTextBox(int x, int y, int width)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Width = width,
                Font = new Font("Segoe UI", 9F)
            };
        }

        private void BtnUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg, *.png)|*.jpg;*.png",
                Title = "Select Vehicle Image"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Handle image upload (save path, display thumbnail, etc.)
                MessageBox.Show($"Image selected: {openFileDialog.FileName}", "Image Upload",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (ValidateVehicleForm())
            {
                // Save vehicle logic would go here
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool ValidateVehicleForm()
        {
            if (string.IsNullOrWhiteSpace(txtMake.Text))
            {
                MessageBox.Show("Please enter the vehicle make.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMake.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtModel.Text))
            {
                MessageBox.Show("Please enter the vehicle model.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtModel.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtVIN.Text) || txtVIN.Text.Length != 17)
            {
                MessageBox.Show("Please enter a valid 17-character VIN.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVIN.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Please enter a valid price greater than 0.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }

            return true;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}