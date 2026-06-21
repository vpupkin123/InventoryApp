using System;
using System.Drawing;
using System.Windows.Forms;
using InventoryApp.Models;
using InventoryApp.Services;
using InventoryApp.Utils;

namespace InventoryApp
{
    public class MainForm : Form
    {
        // Controls
        private TextBox txtLastName;
        private TextBox txtFirstName;
        private TextBox txtMiddleName;
        private Button btnCollect;
        
        // Labels
        private Label lblLastName;
        private Label lblFirstName;
        private Label lblMiddleName;
        
        // Error labels
        private Label lblErrorLastName;
        private Label lblErrorFirstName;
        private Label lblErrorMiddleName;

        public MainForm()
        {
            // Load localization before creating UI
            LocalizationManager.Load();
            
            InitializeComponents();
            
            // Initial validation to disable button if fields are empty
            ValidateInput();
        }

        private void InitializeComponents()
        {
            // Form settings
            this.Text = LocalizationManager.Get("main", "WindowTitle");
            this.Size = new Size(400, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Create controls
            CreateLabels();
            CreateTextBoxes();
            CreateErrorLabels();
            CreateButton();
            
            // Add controls to form
            this.Controls.AddRange(new Control[] {
                lblLastName, txtLastName, lblErrorLastName,
                lblFirstName, txtFirstName, lblErrorFirstName,
                lblMiddleName, txtMiddleName, lblErrorMiddleName,
                btnCollect
            });
        }

        private void CreateLabels()
        {
            int labelX = 20;
            int labelWidth = 150;
            int labelHeight = 20;
            int spacing = 50;

            lblLastName = new Label
            {
                Text = $"{LocalizationManager.Get("main", "LastName")} {LocalizationManager.Get("main", "Required")}:",
                Location = new Point(labelX, 20),
                Size = new Size(labelWidth, labelHeight)
            };

            lblFirstName = new Label
            {
                Text = $"{LocalizationManager.Get("main", "FirstName")} {LocalizationManager.Get("main", "Required")}:",
                Location = new Point(labelX, 20 + spacing),
                Size = new Size(labelWidth, labelHeight)
            };

            lblMiddleName = new Label
            {
                Text = $"{LocalizationManager.Get("main", "MiddleName")}:",
                Location = new Point(labelX, 20 + spacing * 2),
                Size = new Size(labelWidth, labelHeight)
            };
        }

        private void CreateTextBoxes()
        {
            int textBoxX = 170;
            int textBoxWidth = 180;
            int textBoxHeight = 20;
            int spacing = 50;

            txtLastName = new TextBox
            {
                Location = new Point(textBoxX, 20),
                Size = new Size(textBoxWidth, textBoxHeight)
            };
            txtLastName.TextChanged += TxtLastName_TextChanged;

            txtFirstName = new TextBox
            {
                Location = new Point(textBoxX, 20 + spacing),
                Size = new Size(textBoxWidth, textBoxHeight)
            };
            txtFirstName.TextChanged += TxtFirstName_TextChanged;

            txtMiddleName = new TextBox
            {
                Location = new Point(textBoxX, 20 + spacing * 2),
                Size = new Size(textBoxWidth, textBoxHeight)
            };
            txtMiddleName.TextChanged += TxtMiddleName_TextChanged;
        }

        private void CreateErrorLabels()
        {
            int errorLabelX = 170;
            int errorLabelWidth = 180;
            int errorLabelHeight = 15;
            int spacing = 50;

            lblErrorLastName = new Label
            {
                Text = "",
                ForeColor = Color.Red,
                Font = new Font(this.Font.FontFamily, 7),
                Location = new Point(errorLabelX, 40),
                Size = new Size(errorLabelWidth, errorLabelHeight)
            };

            lblErrorFirstName = new Label
            {
                Text = "",
                ForeColor = Color.Red,
                Font = new Font(this.Font.FontFamily, 7),
                Location = new Point(errorLabelX, 40 + spacing),
                Size = new Size(errorLabelWidth, errorLabelHeight)
            };

            lblErrorMiddleName = new Label
            {
                Text = "",
                ForeColor = Color.Red,
                Font = new Font(this.Font.FontFamily, 7),
                Location = new Point(errorLabelX, 40 + spacing * 2),
                Size = new Size(errorLabelWidth, errorLabelHeight)
            };
        }

        private void CreateButton()
        {
            btnCollect = new Button
            {
                Text = LocalizationManager.Get("main", "CollectButton"),
                Location = new Point(170, 200),
                Size = new Size(120, 30),
                Enabled = false
            };
            
            btnCollect.Click += BtnCollect_Click;
        }

        private void ValidateInput()
        {
            bool isValid = true;

            // Validate Last Name (required)
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                lblErrorLastName.Text = LocalizationManager.Get("validation", "RequiredField");
                isValid = false;
            }
            else
            {
                lblErrorLastName.Text = "";
            }

            // Validate First Name (required)
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                lblErrorFirstName.Text = LocalizationManager.Get("validation", "RequiredField");
                isValid = false;
            }
            else
            {
                lblErrorFirstName.Text = "";
            }

            // Middle Name is optional - no validation needed
            lblErrorMiddleName.Text = "";

            // Enable/disable button based on validation
            btnCollect.Enabled = isValid;
        }

        private void TxtLastName_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void TxtFirstName_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void TxtMiddleName_TextChanged(object sender, EventArgs e)
        {
            ValidateInput();
        }

        private void BtnCollect_Click(object sender, EventArgs e)
        {
            // Final validation before collecting data
            ValidateInput();
            
            if (!btnCollect.Enabled)
            {
                MessageBox.Show(
                    LocalizationManager.Get("validation", "RequiredField"),
                    LocalizationManager.Get("messages", "ErrorTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            try
            {
                // Build filename
                string fileName = FileNameBuilder.BuildFileName(
                    txtLastName.Text,
                    txtFirstName.Text,
                    txtMiddleName.Text
                );

                // Collect all data
                var systemData = SystemCollector.Collect();
                var hardwareData = HardwareCollector.Collect();

                // Build report
                var report = new InventoryReport
                {
                    User = new UserInfo
                    {
                        FullName = $"{txtLastName.Text} {txtFirstName.Text} {txtMiddleName.Text}".Trim(),
                        FileName = fileName,
                        Timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")
                    },
                    System = systemData,
                    Hardware = hardwareData
                };

                // Export to JSON
                string filePath = JsonExporter.Export(report, fileName);

                // Show success message
                MessageBox.Show(
                    $"{LocalizationManager.Get("messages", "SuccessMessage")}\n\n{filePath}",
                    LocalizationManager.Get("messages", "SuccessTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Show flash drive message
                MessageBox.Show(
                    LocalizationManager.Get("messages", "FlashDriveMessage"),
                    LocalizationManager.Get("messages", "SuccessTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Close application
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"{LocalizationManager.Get("messages", "ErrorMessage")}\n\n{ex.Message}",
                    LocalizationManager.Get("messages", "ErrorTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}