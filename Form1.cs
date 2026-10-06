// Name : Alvonso Chandra Siahaan
// NIM  : 2381071

using System;
using System.Drawing;
using System.Windows.Forms;

namespace StudentsGPACalculator
{
    public class Form1 : Form
    {
        private Label lblTitle;
        private Label lblName;
        private TextBox txtName;
        private Label lblNIM;
        private TextBox txtNIM;

        private Label lblCourseName;
        private TextBox txtCourseName;
        private Label lblCredits;
        private TextBox txtCredits;
        private Label lblGrade;
        private ComboBox cmbGrade;
        private Button btnAdd;
        private Button btnCalculate;
        private Button btnReset;
        private ListView lvCourses;
        private Label lblGPA;
        private Label lblStatus;
        private Label lblTotalCredits;

        private double totalQualityPoints = 0;
        private int totalCredits = 0;

        public Form1()
        {
            Text = "Students GPA Calculator";
            Width = 720;
            Height = 560;
            StartPosition = FormStartPosition.CenterScreen;

            lblTitle = new Label
            {
                Text = "STUDENTS GPA CALCULATOR",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(220, 15)
            };

            lblName = new Label { Text = "Name  :", Location = new Point(20, 60), AutoSize = true };
            txtName = new TextBox { Location = new Point(140, 57), Width = 250 };

            lblNIM = new Label { Text = "NIM   :", Location = new Point(20, 90), AutoSize = true };
            txtNIM = new TextBox { Location = new Point(140, 87), Width = 250 };

            lblCourseName = new Label { Text = "Course Name :", Location = new Point(20, 130), AutoSize = true };
            txtCourseName = new TextBox { Location = new Point(140, 127), Width = 200 };

            lblCredits = new Label { Text = "Credits :", Location = new Point(370, 130), AutoSize = true };
            txtCredits = new TextBox { Location = new Point(430, 127), Width = 60 };

            lblGrade = new Label { Text = "Grade :", Location = new Point(510, 130), AutoSize = true };
            cmbGrade = new ComboBox { Location = new Point(560, 127), Width = 80, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbGrade.Items.AddRange(new object[] { "A", "A-", "B+", "B", "B-", "C+", "C", "C-", "D+", "D", "F" });

            btnAdd = new Button { Text = "Add Course", Location = new Point(20, 165), Width = 110 };
            btnAdd.Click += BtnAdd_Click;

            lvCourses = new ListView
            {
                Location = new Point(20, 200),
                Width = 655,
                Height = 180,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true
            };
            lvCourses.Columns.Add("Course Name", 320);
            lvCourses.Columns.Add("Credits", 100);
            lvCourses.Columns.Add("Grade", 100);
            lvCourses.Columns.Add("Grade Point", 120);

            btnCalculate = new Button { Text = "Calculate GPA", Location = new Point(20, 395), Width = 130 };
            btnCalculate.Click += BtnCalculate_Click;

            btnReset = new Button { Text = "Reset", Location = new Point(170, 395), Width = 90 };
            btnReset.Click += BtnReset_Click;

            lblTotalCredits = new Label { Text = "Total Credits : 0", Location = new Point(20, 435), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            lblGPA = new Label { Text = "GPA : -", Location = new Point(20, 465), AutoSize = true, Font = new Font("Segoe UI", 12, FontStyle.Bold) };
            lblStatus = new Label { Text = "Status : -", Location = new Point(20, 500), AutoSize = true, Font = new Font("Segoe UI", 12, FontStyle.Bold) };

            Controls.AddRange(new Control[]
            {
                lblTitle, lblName, txtName, lblNIM, txtNIM,
                lblCourseName, txtCourseName, lblCredits, txtCredits, lblGrade, cmbGrade,
                btnAdd, lvCourses, btnCalculate, btnReset,
                lblTotalCredits, lblGPA, lblStatus
            });
        }

        private double GetGradePoint(string grade)
        {
            switch (grade)
            {
                case "A":  return 4.0;
                case "A-": return 3.7;
                case "B+": return 3.3;
                case "B":  return 3.0;
                case "B-": return 2.7;
                case "C+": return 2.3;
                case "C":  return 2.0;
                case "C-": return 1.7;
                case "D+": return 1.3;
                case "D":  return 1.0;
                default:   return 0.0;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (txtCourseName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Course name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCourseName.Focus();
                return;
            }

            if (!int.TryParse(txtCredits.Text, out int credits))
            {
                MessageBox.Show("Credits must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCredits.Focus();
                return;
            }

            if (credits <= 0 || credits > 6)
            {
                MessageBox.Show("Credits must be between 1 and 6.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCredits.Focus();
                return;
            }

            if (cmbGrade.SelectedItem == null)
            {
                MessageBox.Show("Please select a grade.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbGrade.Focus();
                return;
            }

            string grade = cmbGrade.SelectedItem.ToString();
            double gp = GetGradePoint(grade);

            ListViewItem item = new ListViewItem(txtCourseName.Text.Trim());
            item.SubItems.Add(credits.ToString());
            item.SubItems.Add(grade);
            item.SubItems.Add(gp.ToString("0.0"));
            lvCourses.Items.Add(item);

            totalQualityPoints += gp * credits;
            totalCredits += credits;
            lblTotalCredits.Text = "Total Credits : " + totalCredits;

            txtCourseName.Clear();
            txtCredits.Clear();
            cmbGrade.SelectedIndex = -1;
            txtCourseName.Focus();
        }

        private void BtnCalculate_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            if (txtNIM.Text.Trim().Length == 0)
            {
                MessageBox.Show("NIM cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNIM.Focus();
                return;
            }

            if (lvCourses.Items.Count == 0)
            {
                MessageBox.Show("Please add at least one course first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double gpa = Math.Round(totalQualityPoints / totalCredits, 2);
            string status;

            if (gpa >= 3.50)
            {
                status = "Cum Laude";
            }
            else if (gpa >= 3.00)
            {
                status = "Very Good (Sangat Memuaskan)";
            }
            else if (gpa >= 2.50)
            {
                status = "Good (Memuaskan)";
            }
            else if (gpa >= 2.00)
            {
                status = "Fair (Cukup)";
            }
            else
            {
                status = "Poor (Kurang) - Academic Probation";
            }

            lblGPA.Text = "GPA : " + gpa.ToString("0.00");
            lblStatus.Text = "Status : " + status;

            MessageBox.Show(
                "Name        : " + txtName.Text.Trim() + "\n" +
                "NIM         : " + txtNIM.Text.Trim() + "\n" +
                "Total SKS   : " + totalCredits + "\n" +
                "GPA         : " + gpa.ToString("0.00") + "\n" +
                "Status      : " + status,
                "Final Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtNIM.Clear();
            txtCourseName.Clear();
            txtCredits.Clear();
            cmbGrade.SelectedIndex = -1;
            lvCourses.Items.Clear();
            totalQualityPoints = 0;
            totalCredits = 0;
            lblTotalCredits.Text = "Total Credits : 0";
            lblGPA.Text = "GPA : -";
            lblStatus.Text = "Status : -";
        }
    }
}
