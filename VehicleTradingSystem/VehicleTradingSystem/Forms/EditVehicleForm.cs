using System;
using System.Drawing;
using System.Windows.Forms;

namespace VehicleTradingSystem.Forms
{
    public partial class EditVehicleForm : Form
    {
        // Form controls (same as VehicleForm plus additional ones)
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
        private Label lblDateAdded;

        private TextBox txtMake;
        private TextBox txtModel;
        private NumericUpDown nudYear;
        private TextBox txtPrice;
        private TextBox txtMileage;
        private ComboBox cmbColor;
        private TextBox txtVIN;
        private ComboBox cmbStatus;
        private TextBox txtDescription;
        private TextBox txtDateAdded;

        private Button btnSave;
        private Button btnCancel;
        private Button btnUploadImage;
        private Button btnViewImages;
        private PictureBox pbVehicleImage;

        // Property to get the updated vehicle data
        public VehicleData UpdatedVehicle { get; private set; }

        public EditVehicleForm(VehicleData vehicle)
        {
            InitializeComponent();
            LoadVehicleData(vehicle);
        }

        private void InitializeComponent()
        {
            // Main Form Settings
            this.Text = "Edit Vehicle";
            this.ClientSize = new Size(650, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.WhiteSmoke;

            // Title Label
            lblTitle = new Label
            {
                Text = "Edit Vehicle Details",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(20, 20),
                AutoSize = true
            };

            // Vehicle Details GroupBox
            gbVehicleDetails = new GroupBox
            {
                Text = "Vehicle Information",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(20, 60),
                Size = new Size(600, 480)
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
            lblDateAdded = CreateLabel("Date Added:", 20, 350);

            // Input Controls
            txtMake = CreateTextBox(120, 30, 150);
            txtModel = CreateTextBox(120, 70, 150);

            nudYear = new NumericUpDown
            {
                Location = new Point(120, 110),
                Width = 150,
                Minimum = 1990,
                Maximum = DateTime.Now.Year + 1
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
            txtVIN.ReadOnly = true; // VIN shouldn't be editable

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

            txtDateAdded = CreateTextBox(120, 350, 150);
            txtDateAdded.ReadOnly = true;

            // Image Preview
            pbVehicleImage = new PictureBox
            {
                Location = new Point(120, 390),
                Size = new Size(200, 150),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            // Buttons
            btnUploadImage = new Button
            {
                Text = "Upload Image",
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(20, 390),
                Size = new Size(90, 30)
            };
            btnUploadImage.Click += BtnUploadImage_Click;

            btnViewImages = new Button
            {
                Text = "View Images",
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(20, 430),
                Size = new Size(90, 30)
            };
            btnViewImages.Click += BtnViewImages_Click;

            btnSave = new Button
            {
                Text = "Save Changes",
                BackColor = Color.Green,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(350, 430),
                Size = new Size(120, 30)
            };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button
            {
                Text = "Cancel",
                BackColor = Color.Red,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(480, 430),
                Size = new Size(80, 30)
            };
            btnCancel.Click += BtnCancel_Click;

            // Add controls to group box
            gbVehicleDetails.Controls.AddRange(new Control[] {
                lblMake, lblModel, lblYear, lblPrice, lblMileage, lblColor, lblVIN, lblStatus, lblDescription, lblDateAdded,
                txtMake, txtModel, nudYear, txtPrice, txtMileage, cmbColor, txtVIN, cmbStatus, txtDescription, txtDateAdded,
                pbVehicleImage, btnUploadImage, btnViewImages, btnSave, btnCancel
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

        private void LoadVehicleData(VehicleData vehicle)
        {
            txtMake.Text = vehicle.Make;
            txtModel.Text = vehicle.Model;
            nudYear.Value = vehicle.Year;
            txtPrice.Text = vehicle.Price.ToString("0.00");
            txtMileage.Text = vehicle.Mileage.ToString();
            cmbColor.SelectedItem = vehicle.Color;
            txtVIN.Text = vehicle.VIN;
            cmbStatus.SelectedItem = vehicle.Status;
            txtDescription.Text = vehicle.Description;
            txtDateAdded.Text = vehicle.DateAdded.ToShortDateString();

            // Load image if available
            if (!string.IsNullOrEmpty(vehicle.ImagePath))
            {
                try
                {
                    pbVehicleImage.Image = Image.FromFile(vehicle.ImagePath);
                }
                catch
                {
                    pbVehicleImage.Image = null;
                }
            }
        }

        private void BtnUploadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.jpg, *.png)|*.jpg;*.png",
                Title = "Select Vehicle Image",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pbVehicleImage.Image = Image.FromFile(openFileDialog.FileName);
                    // In a real app, you would save the image path or copy the image
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnViewImages_Click(object sender, EventArgs e)
        {
            // Show all images for this vehicle
            MessageBox.Show("This would show a gallery of all images for this vehicle.", "Vehicle Images",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (ValidateVehicleForm())
            {
                // Create updated vehicle data
                UpdatedVehicle = new VehicleData
                {
                    Make = txtMake.Text,
                    Model = txtModel.Text,
                    Year = (int)nudYear.Value,
                    Price = decimal.Parse(txtPrice.Text),
                    Mileage = int.Parse(txtMileage.Text),
                    Color = cmbColor.SelectedItem.ToString(),
                    VIN = txtVIN.Text,
                    Status = cmbStatus.SelectedItem.ToString(),
                    Description = txtDescription.Text,
                    // Image path would be updated if new image was uploaded
                    DateAdded = DateTime.Parse(txtDateAdded.Text)
                };

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

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Please enter a valid price greater than 0.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }

            if (!int.TryParse(txtMileage.Text, out int mileage) || mileage < 0)
            {
                MessageBox.Show("Please enter a valid mileage (positive number).", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMileage.Focus();
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

    // Simple data class to hold vehicle information
    public class VehicleData
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public int Mileage { get; set; }
        public string Color { get; set; }
        public string VIN { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime DateAdded { get; set; }
    }
}