namespace Darshana
{
    partial class SiteEngineer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonManageLabours = new System.Windows.Forms.Button();
            this.buttonManageSupervisors = new System.Windows.Forms.Button();
            this.buttonManageSkillLabours = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonManageLabours
            // 
            this.buttonManageLabours.Location = new System.Drawing.Point(25, 28);
            this.buttonManageLabours.Name = "buttonManageLabours";
            this.buttonManageLabours.Size = new System.Drawing.Size(138, 41);
            this.buttonManageLabours.TabIndex = 0;
            this.buttonManageLabours.Text = "Manage Labours";
            this.buttonManageLabours.UseVisualStyleBackColor = true;
            this.buttonManageLabours.Click += new System.EventHandler(this.buttonManageLabours_Click);
            // 
            // buttonManageSupervisors
            // 
            this.buttonManageSupervisors.Location = new System.Drawing.Point(25, 155);
            this.buttonManageSupervisors.Name = "buttonManageSupervisors";
            this.buttonManageSupervisors.Size = new System.Drawing.Size(138, 41);
            this.buttonManageSupervisors.TabIndex = 1;
            this.buttonManageSupervisors.Text = "Manage Supervisors";
            this.buttonManageSupervisors.UseVisualStyleBackColor = true;
            this.buttonManageSupervisors.Click += new System.EventHandler(this.buttonManageSupervisors_Click);
            // 
            // buttonManageSkillLabours
            // 
            this.buttonManageSkillLabours.Location = new System.Drawing.Point(25, 92);
            this.buttonManageSkillLabours.Name = "buttonManageSkillLabours";
            this.buttonManageSkillLabours.Size = new System.Drawing.Size(138, 41);
            this.buttonManageSkillLabours.TabIndex = 2;
            this.buttonManageSkillLabours.Text = "Manage Skilled Labours";
            this.buttonManageSkillLabours.UseVisualStyleBackColor = true;
            this.buttonManageSkillLabours.Click += new System.EventHandler(this.buttonManageSkillLabours_Click);
            // 
            // SiteEngineer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonManageSkillLabours);
            this.Controls.Add(this.buttonManageSupervisors);
            this.Controls.Add(this.buttonManageLabours);
            this.Name = "SiteEngineer";
            this.Text = "Site Engineer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonManageLabours;
        private System.Windows.Forms.Button buttonManageSupervisors;
        private System.Windows.Forms.Button buttonManageSkillLabours;
    }
}