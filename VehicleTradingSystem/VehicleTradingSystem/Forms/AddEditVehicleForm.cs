using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VehicleTradingSystem.Models;
using VehicleTradingSystem.Services;

namespace VehicleTradingSystem.Forms
{
    public partial class AddEditVehicleForm : Form
    {
        private readonly VehicleService _vehicleService;
        private readonly Vehicle _vehicle;
        private readonly bool _isEditMode;

        private TextBox txtMake;
        private TextBox txtModel;
        private NumericUpDown numYear;
        private TextBox txtVIN;
        private TextBox txtColor;
        private NumericUpDown numPrice;
        private ComboBox cmbStatus;
        private ComboBox cmbFuelType;
        private ComboBox cmbTransmission;
        private NumericUpDown numMileage;
        private TextBox txtDescription;
        private Button btnSave;
        private Button btnCancel;

        public AddEditVehicleForm(Vehicle vehicle = null)
        {
            _vehicleService = new VehicleService();
            _vehicle = vehicle;
            _isEditMode = vehicle != null;

            InitializeComponent();
            SetupForm();

            if (_isEditMode)
            {
                LoadVehicleData();
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form settings
            this.Text = _isEditMode ? "Edit Vehicle" : "Add New Vehicle";
            this.Size = new Size(500, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Create controls
            CreateControls();
            LayoutControls();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void CreateControls()
        {
            // Make
            txtMake = new TextBox() { Size = new Size(200, 23), MaxLength = 50 };

            // Model
            txtModel = new TextBox() { Size = new Size(200, 23), MaxLength = 50 };

            // Year
            numYear = new NumericUpDown()
            {
                Size = new Size(200, 23),
                Minimum = 1900,
                Maximum = DateTime.Now.Year + 2,
                Value = DateTime.Now.Year
            };

            // VIN
            txtVIN = new TextBox() { Size = new Size(200, 23), MaxLength = 20 };

            // Color
            txtColor = new TextBox() { Size = new Size(200, 23), MaxLength = 50 };

            // Price
            numPrice = new NumericUpDown()
            {
                Size = new Size(200, 23),
                DecimalPlaces = 2,
                Maximum = 9999999,
                Minimum = 0
            };

            // Status
            cmbStatus = new ComboBox()
            {
                Size = new Size(200, 23),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbStatus.Items.AddRange(new[] { "Available", "Sold", "Reserved" });

            // Fuel Type
            cmbFuelType = new ComboBox()
            {
                Size = new Size(200, 23),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbFuelType.Items.AddRange(new[] { "Petrol", "Diesel", "Hybrid", "Electric", "LPG" });

            // Transmission
            cmbTransmission = new ComboBox()
            {
                Size = new Size(200, 23),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbTransmission.Items.AddRange(new[] { "Manual", "Automatic", "CVT" });

            // Mileage
            numMileage = new NumericUpDown()
            {
                Size = new Size(200, 23),
                Maximum = 9999999,
                Minimum = 0
            };

            // Description
            txtDescription = new TextBox()
            {
                Size = new Size(200, 100),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                MaxLength = 500
            };

            // Buttons
            btnSave = new Button()
            {
                Text = "Save",
                Size = new Size(80, 30),
                DialogResult = DialogResult.OK
            };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button()
            {
                Text = "Cancel",
                Size = new Size(80, 30),
                DialogResult = DialogResult.Cancel
            };
        }

        private void LayoutControls()
        {
            int x = 20, y = 20, labelWidth = 100, spacing = 35;

            // Make
            this.Controls.Add(new Label() { Text = "Make:", Location = new Point(x, y), Size = new Size(labelWidth, 23) });
            txtMake.Location = new Point(x + labelWidth + 10, y);
            this.Controls.Add(txtMake);
            y += spacing;

            // Model
            this.Controls.Add(new Label() { Text = "Model:", Location = new Point(x, y), Size = new Size(labelWidth, 23) });
            txtModel.Location = new Point(x + labelWidth + 10, y);
            this.Controls.Add(txtModel);
            y += spacing;

            // Year
            this.Controls.Add(new Label() { Text = "Year:", Location = new Point(x, y), Size = new Size(labelWidth, 23) });
            numYear.Location = new Point(x + labelWidth + 10, y);
            this.Controls.Add(numYear);
            y += spacing;

            // VIN
            this.Controls.Add(new Label() { Text = "VIN:", Location = new Point(x, y), Size = new Size(labelWidth, 23) });
            txtVIN.Location = new Point(x + labelWidth + 10, y);
            this.Controls.Add(txtVIN);
            y += spacing;

            // Color
            this.Controls.Add(new Label() { Text = "Color:", Location = new Point(x, y), Size = new Size(labelWidth, 23) });
            txtColor.Location = new Point(x + labelWidth + 10, y);
            this.Controls.Add(txtColor);
            y += spacing;

            // Price
            this.Controls.Add(new Label() { Text = "Price:", Location = new Point(x, y), Size = new Size(labelWidth, 23) });
            numPrice.Location = new Point(x + labelWidth + 10, y);
            this.Controls.Add(numPrice);
            y += spacing;

            // Status
            this.Controls.Add(new Label() { Text = "Status:", Location = new Point(x, y), Size = new Size(labelWidth, 23) });
            cmbStatus.Location = new Point(x + labelWidth + 10, y);
            this.Controls.Add(cmbStatus);
            y += spacing;

            // Fuel Type
            this.Controls.Add(new Label() { Text = "Fuel Type:", Location = new Point(x, y), Size = new Size(labelWidth, 23) });
            cmbFuelType.Location = new Point(x + labelWidth + 10, y);
            this.Controls.Add(cmbFuelType);
            y += spacing;

            // Transmission
            this.Controls.Add(new Label() { Text = "Transmission:", Location = new Point(x, y), Size = new Size(labelWidth, 23) });
            cmbTransmission.Location = new Point(x + labelWidth + 10, y);
            this.Controls.Add(cmbTransmission);
            y += spacing;

            // Mileage
            this.Controls.Add(new Label() { Text = "Mileage:", Location = new Point(x, y), Size = new Size(labelWidth, 23) });
            numMileage.Location = new Point(x + labelWidth + 10, y);
            this.Controls.Add(numMileage);
            y += spacing;

            // Description
            this.Controls.Add(new Label() { Text = "Description:", Location = new Point(x, y), Size = new Size(labelWidth, 23) });
            txtDescription.Location = new Point(x + labelWidth + 10, y);
            this.Controls.Add(txtDescription);
            y += 120;

            // Buttons
            btnCancel.Location = new Point(x + labelWidth + 10, y);
            btnSave.Location = new Point(x + labelWidth + 100, y);
            this.Controls.Add(btnCancel);
            this.Controls.Add(btnSave);
        }

        private void SetupForm()
        {
            // Set default values
            cmbStatus.SelectedIndex = 0; // Available
            if (cmbFuelType.Items.Count > 0) cmbFuelType.SelectedIndex = 0;
            if (cmbTransmission.Items.Count > 0) cmbTransmission.SelectedIndex = 0;
        }

        private void LoadVehicleData()
        {
            if (_vehicle != null)
            {
                txtMake.Text = _vehicle.Make;
                txtModel.Text = _vehicle.Model;
                numYear.Value = _vehicle.Year;
                txtVIN.Text = _vehicle.VIN;
                txtColor.Text = _vehicle.Color;
                numPrice.Value = _vehicle.Price;

                cmbStatus.SelectedItem = _vehicle.Status;
                cmbFuelType.SelectedItem = _vehicle.FuelType;
                cmbTransmission.SelectedItem = _vehicle.Transmission;

                if (_vehicle.Mileage.HasValue)
                    numMileage.Value = _vehicle.Mileage.Value;

                txtDescription.Text = _vehicle.Description ?? string.Empty;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                try
                {
                    Vehicle vehicle = _isEditMode ? _vehicle : new Vehicle();

                    vehicle.Make = txtMake.Text.Trim();
                    vehicle.Model = txtModel.Text.Trim();
                    vehicle.Year = (int)numYear.Value;
                    vehicle.VIN = txtVIN.Text.Trim();
                    vehicle.Color = txtColor.Text.Trim();
                    vehicle.Price = numPrice.Value;
                    vehicle.Status = cmbStatus.SelectedItem?.ToString() ?? "Available";
                    vehicle.FuelType = cmbFuelType.SelectedItem?.ToString();
                    vehicle.Transmission = cmbTransmission.SelectedItem?.ToString();
                    vehicle.Mileage = numMileage.Value > 0 ? (int?)numMileage.Value : null;
                    vehicle.Description = txtDescription.Text.Trim();

                    bool success = _isEditMode ?
                        _vehicleService.UpdateVehicle(vehicle) :
                        _vehicleService.AddVehicle(vehicle);

                    if (success)
                    {
                        MessageBox.Show(
                            _isEditMode ? "Vehicle updated successfully!" : "Vehicle added successfully!",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving vehicle: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateForm()
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

            if (string.IsNullOrWhiteSpace(txtVIN.Text))
            {
                MessageBox.Show("Please enter the VIN.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtVIN.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtColor.Text))
            {
                MessageBox.Show("Please enter the vehicle color.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtColor.Focus();
                return false;
            }

            if (numPrice.Value <= 0)
            {
                MessageBox.Show("Please enter a valid price.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numPrice.Focus();
                return false;
            }

            return true;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _vehicleService?.Dispose();
            base.OnFormClosed(e);
        }
    }
}