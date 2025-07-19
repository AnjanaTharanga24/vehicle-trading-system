using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace VehicleTradingSystem.Forms
{
    public partial class LoginForm : Form
    {
        private PictureBox pictureBox1;
        private GroupBox groupBox1;
        private Label lblTitle;
        private Label lblUsername;
        private Label lblPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnClear;
        private Button btnLogin;
        private Button btnExit;
        private LinkLabel lnkRegister;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.pictureBox1 = new PictureBox();
            this.groupBox1 = new GroupBox();
            this.lblTitle = new Label();
            this.lblUsername = new Label();
            this.lblPassword = new Label();
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.btnClear = new Button();
            this.btnLogin = new Button();
            this.btnExit = new Button();
            this.lnkRegister = new LinkLabel();

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();

            // LoginForm
            this.ClientSize = new Size(450, 400);
            this.Text = "Login - Vehicle buying and selling System";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.White;
            this.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

            // pictureBox1
            this.pictureBox1.Location = new Point(150, 20);
            this.pictureBox1.Size = new Size(150, 80);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.BackColor = Color.LightBlue;
            this.pictureBox1.BorderStyle = BorderStyle.FixedSingle;

            // Load image from URL
            try
            {
                WebRequest request = WebRequest.Create("https://logodix.com/logo/1342321.png");
                using (WebResponse response = request.GetResponse())
                using (System.IO.Stream stream = response.GetResponseStream())
                {
                    pictureBox1.Image = Image.FromStream(stream);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load logo: " + ex.Message);
            }

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Times New Roman", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.DarkBlue;
            this.lblTitle.Location = new Point(80, 110);
            this.lblTitle.Size = new Size(190, 25);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Vehicle buying and selling System";

            // groupBox1
            this.groupBox1.Controls.Add(this.lblUsername);
            this.groupBox1.Controls.Add(this.lblPassword);
            this.groupBox1.Controls.Add(this.txtUsername);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnLogin);
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Font = new Font("Times New Roman", 12F, FontStyle.Regular);
            this.groupBox1.Location = new Point(50, 150);
            this.groupBox1.Size = new Size(350, 200);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Login Details";

            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new Point(30, 40);
            this.lblUsername.Size = new Size(77, 19);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Username:";

            // txtUsername
            this.txtUsername.Location = new Point(130, 37);
            this.txtUsername.Size = new Size(180, 26);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.Font = new Font("Times New Roman", 12F);

            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new Point(30, 80);
            this.lblPassword.Size = new Size(72, 19);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password:";

            // txtPassword
            this.txtPassword.Location = new Point(130, 77);
            this.txtPassword.Size = new Size(180, 26);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Font = new Font("Times New Roman", 12F);

            // btnClear
            this.btnClear.BackColor = Color.Orange;
            this.btnClear.FlatStyle = FlatStyle.Flat;
            this.btnClear.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            this.btnClear.Location = new Point(30, 130);
            this.btnClear.Size = new Size(80, 35);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new EventHandler(this.btnClear_Click);

            // btnLogin
            this.btnLogin.BackColor = Color.Green;
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.Location = new Point(130, 130);
            this.btnLogin.Size = new Size(80, 35);
            this.btnLogin.TabIndex = 5;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);

            // btnExit
            this.btnExit.BackColor = Color.Red;
            this.btnExit.FlatStyle = FlatStyle.Flat;
            this.btnExit.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            this.btnExit.ForeColor = Color.White;
            this.btnExit.Location = new Point(230, 130);
            this.btnExit.Size = new Size(80, 35);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new EventHandler(this.btnExit_Click);

            // lnkRegister
            this.lnkRegister.AutoSize = true;
            this.lnkRegister.Location = new Point(180, 360);
            this.lnkRegister.Size = new Size(90, 19);
            this.lnkRegister.TabIndex = 7;
            this.lnkRegister.TabStop = true;
            this.lnkRegister.Text = "New User? Register";
            this.lnkRegister.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lnkRegister_LinkClicked);

            // Add controls to form
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lnkRegister);

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtUsername.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter both username and password", "Login Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Navigate to Dashboard after successful login
            this.Hide();
            DashboardForm dashboard = new DashboardForm();
            dashboard.FormClosed += (s, args) => this.Close();
            dashboard.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure, Do you really want to Exit...?",
                "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void lnkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Open registration form
            this.Hide();
            RegistrationForm registrationForm = new RegistrationForm();
            registrationForm.FormClosed += (s, args) => this.Show();
            registrationForm.Show();
        }
    }
}
