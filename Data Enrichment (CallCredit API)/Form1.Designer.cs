namespace Data_Enrichment__CallCredit_API_
{
    partial class MainWindow
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
            this.Button_Browse = new System.Windows.Forms.Button();
            this.fileSelected = new System.Windows.Forms.TextBox();
            this.label_Title = new System.Windows.Forms.Label();
            this.button_Enrich = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label_FileOkay = new System.Windows.Forms.Label();
            this.group_MapFields = new System.Windows.Forms.GroupBox();
            this.map_mob = new System.Windows.Forms.ComboBox();
            this.lab_mob = new System.Windows.Forms.Label();
            this.map_zip = new System.Windows.Forms.ComboBox();
            this.lab_zip = new System.Windows.Forms.Label();
            this.lab_a7 = new System.Windows.Forms.Label();
            this.map_a7 = new System.Windows.Forms.ComboBox();
            this.map_a6 = new System.Windows.Forms.ComboBox();
            this.map_a5 = new System.Windows.Forms.ComboBox();
            this.map_a4 = new System.Windows.Forms.ComboBox();
            this.map_a3 = new System.Windows.Forms.ComboBox();
            this.map_a2 = new System.Windows.Forms.ComboBox();
            this.map_a1 = new System.Windows.Forms.ComboBox();
            this.lab_a6 = new System.Windows.Forms.Label();
            this.lab_a5 = new System.Windows.Forms.Label();
            this.lab_a4 = new System.Windows.Forms.Label();
            this.lab_a3 = new System.Windows.Forms.Label();
            this.lab_a2 = new System.Windows.Forms.Label();
            this.lab_a1 = new System.Windows.Forms.Label();
            this.lab_dob = new System.Windows.Forms.Label();
            this.map_dob = new System.Windows.Forms.ComboBox();
            this.map_s = new System.Windows.Forms.ComboBox();
            this.map_f = new System.Windows.Forms.ComboBox();
            this.map_t = new System.Windows.Forms.ComboBox();
            this.map_n = new System.Windows.Forms.ComboBox();
            this.lab_s = new System.Windows.Forms.Label();
            this.lab_f = new System.Windows.Forms.Label();
            this.lab_t = new System.Windows.Forms.Label();
            this.lab_name = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.label_ETA = new System.Windows.Forms.Label();
            this.StatusBox = new System.Windows.Forms.GroupBox();
            this.StatusBoxText = new System.Windows.Forms.RichTextBox();
            this.group_MapFields.SuspendLayout();
            this.StatusBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button_Browse
            // 
            this.Button_Browse.BackColor = System.Drawing.Color.White;
            this.Button_Browse.Font = new System.Drawing.Font("Helvetica 45 Light", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Browse.Location = new System.Drawing.Point(12, 65);
            this.Button_Browse.Name = "Button_Browse";
            this.Button_Browse.Size = new System.Drawing.Size(83, 25);
            this.Button_Browse.TabIndex = 0;
            this.Button_Browse.Text = "Browse";
            this.Button_Browse.UseVisualStyleBackColor = false;
            this.Button_Browse.Click += new System.EventHandler(this.Button_Browse_Click);
            // 
            // fileSelected
            // 
            this.fileSelected.Enabled = false;
            this.fileSelected.Font = new System.Drawing.Font("Helvetica 45 Light", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileSelected.Location = new System.Drawing.Point(101, 66);
            this.fileSelected.Name = "fileSelected";
            this.fileSelected.Size = new System.Drawing.Size(451, 22);
            this.fileSelected.TabIndex = 1;
            // 
            // label_Title
            // 
            this.label_Title.AutoSize = true;
            this.label_Title.Font = new System.Drawing.Font("Lexia", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Title.ForeColor = System.Drawing.Color.White;
            this.label_Title.Location = new System.Drawing.Point(12, 9);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(268, 46);
            this.label_Title.TabIndex = 2;
            this.label_Title.Text = "CallCredit API";
            // 
            // button_Enrich
            // 
            this.button_Enrich.BackColor = System.Drawing.Color.White;
            this.button_Enrich.Enabled = false;
            this.button_Enrich.Font = new System.Drawing.Font("Helvetica 45 Light", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Enrich.Location = new System.Drawing.Point(417, 393);
            this.button_Enrich.Name = "button_Enrich";
            this.button_Enrich.Size = new System.Drawing.Size(135, 35);
            this.button_Enrich.TabIndex = 3;
            this.button_Enrich.Text = "Enrich Data";
            this.button_Enrich.UseVisualStyleBackColor = false;
            this.button_Enrich.Click += new System.EventHandler(this.button_Enrich_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.InitialDirectory = "W:\\";
            // 
            // label_FileOkay
            // 
            this.label_FileOkay.AutoSize = true;
            this.label_FileOkay.Font = new System.Drawing.Font("Helvetica 45 Light", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_FileOkay.ForeColor = System.Drawing.Color.White;
            this.label_FileOkay.Location = new System.Drawing.Point(98, 91);
            this.label_FileOkay.Name = "label_FileOkay";
            this.label_FileOkay.Size = new System.Drawing.Size(0, 17);
            this.label_FileOkay.TabIndex = 4;
            // 
            // group_MapFields
            // 
            this.group_MapFields.Controls.Add(this.map_mob);
            this.group_MapFields.Controls.Add(this.lab_mob);
            this.group_MapFields.Controls.Add(this.map_zip);
            this.group_MapFields.Controls.Add(this.lab_zip);
            this.group_MapFields.Controls.Add(this.lab_a7);
            this.group_MapFields.Controls.Add(this.map_a7);
            this.group_MapFields.Controls.Add(this.map_a6);
            this.group_MapFields.Controls.Add(this.map_a5);
            this.group_MapFields.Controls.Add(this.map_a4);
            this.group_MapFields.Controls.Add(this.map_a3);
            this.group_MapFields.Controls.Add(this.map_a2);
            this.group_MapFields.Controls.Add(this.map_a1);
            this.group_MapFields.Controls.Add(this.lab_a6);
            this.group_MapFields.Controls.Add(this.lab_a5);
            this.group_MapFields.Controls.Add(this.lab_a4);
            this.group_MapFields.Controls.Add(this.lab_a3);
            this.group_MapFields.Controls.Add(this.lab_a2);
            this.group_MapFields.Controls.Add(this.lab_a1);
            this.group_MapFields.Controls.Add(this.lab_dob);
            this.group_MapFields.Controls.Add(this.map_dob);
            this.group_MapFields.Controls.Add(this.map_s);
            this.group_MapFields.Controls.Add(this.map_f);
            this.group_MapFields.Controls.Add(this.map_t);
            this.group_MapFields.Controls.Add(this.map_n);
            this.group_MapFields.Controls.Add(this.lab_s);
            this.group_MapFields.Controls.Add(this.lab_f);
            this.group_MapFields.Controls.Add(this.lab_t);
            this.group_MapFields.Controls.Add(this.lab_name);
            this.group_MapFields.Font = new System.Drawing.Font("Helvetica 45 Light", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.group_MapFields.ForeColor = System.Drawing.Color.White;
            this.group_MapFields.Location = new System.Drawing.Point(12, 111);
            this.group_MapFields.Name = "group_MapFields";
            this.group_MapFields.Size = new System.Drawing.Size(540, 276);
            this.group_MapFields.TabIndex = 5;
            this.group_MapFields.TabStop = false;
            this.group_MapFields.Text = "Map Fields";
            // 
            // map_mob
            // 
            this.map_mob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.map_mob.Enabled = false;
            this.map_mob.FormattingEnabled = true;
            this.map_mob.Location = new System.Drawing.Point(100, 237);
            this.map_mob.Name = "map_mob";
            this.map_mob.Size = new System.Drawing.Size(168, 24);
            this.map_mob.TabIndex = 27;
            // 
            // lab_mob
            // 
            this.lab_mob.AutoSize = true;
            this.lab_mob.Location = new System.Drawing.Point(34, 240);
            this.lab_mob.Name = "lab_mob";
            this.lab_mob.Size = new System.Drawing.Size(49, 17);
            this.lab_mob.TabIndex = 26;
            this.lab_mob.Text = "Mobile";
            this.lab_mob.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // map_zip
            // 
            this.map_zip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.map_zip.Enabled = false;
            this.map_zip.FormattingEnabled = true;
            this.map_zip.Location = new System.Drawing.Point(330, 237);
            this.map_zip.Name = "map_zip";
            this.map_zip.Size = new System.Drawing.Size(173, 24);
            this.map_zip.TabIndex = 25;
            // 
            // lab_zip
            // 
            this.lab_zip.AutoSize = true;
            this.lab_zip.Location = new System.Drawing.Point(288, 239);
            this.lab_zip.Name = "lab_zip";
            this.lab_zip.Size = new System.Drawing.Size(27, 17);
            this.lab_zip.TabIndex = 24;
            this.lab_zip.Text = "Zip";
            // 
            // lab_a7
            // 
            this.lab_a7.AutoSize = true;
            this.lab_a7.Location = new System.Drawing.Point(288, 210);
            this.lab_a7.Name = "lab_a7";
            this.lab_a7.Size = new System.Drawing.Size(25, 17);
            this.lab_a7.TabIndex = 23;
            this.lab_a7.Text = "A7";
            // 
            // map_a7
            // 
            this.map_a7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.map_a7.Enabled = false;
            this.map_a7.FormattingEnabled = true;
            this.map_a7.Location = new System.Drawing.Point(330, 207);
            this.map_a7.Name = "map_a7";
            this.map_a7.Size = new System.Drawing.Size(173, 24);
            this.map_a7.TabIndex = 22;
            // 
            // map_a6
            // 
            this.map_a6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.map_a6.Enabled = false;
            this.map_a6.FormattingEnabled = true;
            this.map_a6.Location = new System.Drawing.Point(330, 177);
            this.map_a6.Name = "map_a6";
            this.map_a6.Size = new System.Drawing.Size(173, 24);
            this.map_a6.TabIndex = 21;
            // 
            // map_a5
            // 
            this.map_a5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.map_a5.Enabled = false;
            this.map_a5.FormattingEnabled = true;
            this.map_a5.Location = new System.Drawing.Point(330, 147);
            this.map_a5.Name = "map_a5";
            this.map_a5.Size = new System.Drawing.Size(173, 24);
            this.map_a5.TabIndex = 20;
            // 
            // map_a4
            // 
            this.map_a4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.map_a4.Enabled = false;
            this.map_a4.FormattingEnabled = true;
            this.map_a4.Location = new System.Drawing.Point(330, 117);
            this.map_a4.Name = "map_a4";
            this.map_a4.Size = new System.Drawing.Size(173, 24);
            this.map_a4.TabIndex = 19;
            // 
            // map_a3
            // 
            this.map_a3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.map_a3.Enabled = false;
            this.map_a3.FormattingEnabled = true;
            this.map_a3.Location = new System.Drawing.Point(330, 87);
            this.map_a3.Name = "map_a3";
            this.map_a3.Size = new System.Drawing.Size(173, 24);
            this.map_a3.TabIndex = 18;
            // 
            // map_a2
            // 
            this.map_a2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.map_a2.Enabled = false;
            this.map_a2.FormattingEnabled = true;
            this.map_a2.Location = new System.Drawing.Point(330, 57);
            this.map_a2.Name = "map_a2";
            this.map_a2.Size = new System.Drawing.Size(173, 24);
            this.map_a2.TabIndex = 17;
            // 
            // map_a1
            // 
            this.map_a1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.map_a1.Enabled = false;
            this.map_a1.FormattingEnabled = true;
            this.map_a1.Location = new System.Drawing.Point(330, 24);
            this.map_a1.Name = "map_a1";
            this.map_a1.Size = new System.Drawing.Size(173, 24);
            this.map_a1.TabIndex = 16;
            // 
            // lab_a6
            // 
            this.lab_a6.AutoSize = true;
            this.lab_a6.Location = new System.Drawing.Point(288, 180);
            this.lab_a6.Name = "lab_a6";
            this.lab_a6.Size = new System.Drawing.Size(25, 17);
            this.lab_a6.TabIndex = 15;
            this.lab_a6.Text = "A6";
            // 
            // lab_a5
            // 
            this.lab_a5.AutoSize = true;
            this.lab_a5.Location = new System.Drawing.Point(288, 151);
            this.lab_a5.Name = "lab_a5";
            this.lab_a5.Size = new System.Drawing.Size(25, 17);
            this.lab_a5.TabIndex = 14;
            this.lab_a5.Text = "A5";
            // 
            // lab_a4
            // 
            this.lab_a4.AutoSize = true;
            this.lab_a4.Location = new System.Drawing.Point(288, 121);
            this.lab_a4.Name = "lab_a4";
            this.lab_a4.Size = new System.Drawing.Size(25, 17);
            this.lab_a4.TabIndex = 13;
            this.lab_a4.Text = "A4";
            // 
            // lab_a3
            // 
            this.lab_a3.AutoSize = true;
            this.lab_a3.Location = new System.Drawing.Point(288, 91);
            this.lab_a3.Name = "lab_a3";
            this.lab_a3.Size = new System.Drawing.Size(25, 17);
            this.lab_a3.TabIndex = 12;
            this.lab_a3.Text = "A3";
            // 
            // lab_a2
            // 
            this.lab_a2.AutoSize = true;
            this.lab_a2.Location = new System.Drawing.Point(288, 60);
            this.lab_a2.Name = "lab_a2";
            this.lab_a2.Size = new System.Drawing.Size(25, 17);
            this.lab_a2.TabIndex = 11;
            this.lab_a2.Text = "A2";
            // 
            // lab_a1
            // 
            this.lab_a1.AutoSize = true;
            this.lab_a1.Location = new System.Drawing.Point(288, 27);
            this.lab_a1.Name = "lab_a1";
            this.lab_a1.Size = new System.Drawing.Size(25, 17);
            this.lab_a1.TabIndex = 10;
            this.lab_a1.Text = "A1";
            // 
            // lab_dob
            // 
            this.lab_dob.AutoSize = true;
            this.lab_dob.Location = new System.Drawing.Point(46, 144);
            this.lab_dob.Name = "lab_dob";
            this.lab_dob.Size = new System.Drawing.Size(37, 17);
            this.lab_dob.TabIndex = 9;
            this.lab_dob.Text = "DOB";
            this.lab_dob.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // map_dob
            // 
            this.map_dob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.map_dob.Enabled = false;
            this.map_dob.FormattingEnabled = true;
            this.map_dob.Location = new System.Drawing.Point(100, 144);
            this.map_dob.Name = "map_dob";
            this.map_dob.Size = new System.Drawing.Size(168, 24);
            this.map_dob.TabIndex = 8;
            // 
            // map_s
            // 
            this.map_s.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.map_s.Enabled = false;
            this.map_s.FormattingEnabled = true;
            this.map_s.Location = new System.Drawing.Point(100, 114);
            this.map_s.Name = "map_s";
            this.map_s.Size = new System.Drawing.Size(168, 24);
            this.map_s.TabIndex = 7;
            // 
            // map_f
            // 
            this.map_f.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.map_f.Enabled = false;
            this.map_f.FormattingEnabled = true;
            this.map_f.Location = new System.Drawing.Point(100, 84);
            this.map_f.Name = "map_f";
            this.map_f.Size = new System.Drawing.Size(168, 24);
            this.map_f.TabIndex = 6;
            // 
            // map_t
            // 
            this.map_t.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.map_t.Enabled = false;
            this.map_t.FormattingEnabled = true;
            this.map_t.Location = new System.Drawing.Point(100, 54);
            this.map_t.Name = "map_t";
            this.map_t.Size = new System.Drawing.Size(168, 24);
            this.map_t.TabIndex = 5;
            // 
            // map_n
            // 
            this.map_n.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.map_n.Enabled = false;
            this.map_n.FormattingEnabled = true;
            this.map_n.Location = new System.Drawing.Point(100, 24);
            this.map_n.Name = "map_n";
            this.map_n.Size = new System.Drawing.Size(168, 24);
            this.map_n.TabIndex = 4;
            // 
            // lab_s
            // 
            this.lab_s.AutoSize = true;
            this.lab_s.Location = new System.Drawing.Point(20, 114);
            this.lab_s.Name = "lab_s";
            this.lab_s.Size = new System.Drawing.Size(63, 17);
            this.lab_s.TabIndex = 3;
            this.lab_s.Text = "Surname";
            this.lab_s.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lab_f
            // 
            this.lab_f.AutoSize = true;
            this.lab_f.Location = new System.Drawing.Point(14, 87);
            this.lab_f.Name = "lab_f";
            this.lab_f.Size = new System.Drawing.Size(69, 17);
            this.lab_f.TabIndex = 2;
            this.lab_f.Text = "Forename";
            this.lab_f.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lab_t
            // 
            this.lab_t.AutoSize = true;
            this.lab_t.Location = new System.Drawing.Point(50, 57);
            this.lab_t.Name = "lab_t";
            this.lab_t.Size = new System.Drawing.Size(33, 17);
            this.lab_t.TabIndex = 1;
            this.lab_t.Text = "Title";
            this.lab_t.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lab_name
            // 
            this.lab_name.AutoSize = true;
            this.lab_name.Location = new System.Drawing.Point(39, 31);
            this.lab_name.Name = "lab_name";
            this.lab_name.Size = new System.Drawing.Size(44, 17);
            this.lab_name.TabIndex = 0;
            this.lab_name.Text = "Name";
            this.lab_name.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label_ETA
            // 
            this.label_ETA.AutoSize = true;
            this.label_ETA.Font = new System.Drawing.Font("Helvetica 45 Light", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ETA.ForeColor = System.Drawing.Color.White;
            this.label_ETA.Location = new System.Drawing.Point(12, 398);
            this.label_ETA.Name = "label_ETA";
            this.label_ETA.Size = new System.Drawing.Size(0, 21);
            this.label_ETA.TabIndex = 6;
            // 
            // StatusBox
            // 
            this.StatusBox.Controls.Add(this.StatusBoxText);
            this.StatusBox.Font = new System.Drawing.Font("Helvetica 45 Light", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusBox.ForeColor = System.Drawing.Color.White;
            this.StatusBox.Location = new System.Drawing.Point(570, 12);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.Size = new System.Drawing.Size(329, 416);
            this.StatusBox.TabIndex = 7;
            this.StatusBox.TabStop = false;
            this.StatusBox.Text = "Status";
            // 
            // StatusBoxText
            // 
            this.StatusBoxText.BackColor = System.Drawing.Color.LightGray;
            this.StatusBoxText.Enabled = false;
            this.StatusBoxText.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusBoxText.Location = new System.Drawing.Point(6, 22);
            this.StatusBoxText.Name = "StatusBoxText";
            this.StatusBoxText.Size = new System.Drawing.Size(317, 385);
            this.StatusBoxText.TabIndex = 0;
            this.StatusBoxText.Text = "";
            this.StatusBoxText.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(911, 440);
            this.Controls.Add(this.StatusBox);
            this.Controls.Add(this.label_ETA);
            this.Controls.Add(this.group_MapFields);
            this.Controls.Add(this.label_FileOkay);
            this.Controls.Add(this.button_Enrich);
            this.Controls.Add(this.label_Title);
            this.Controls.Add(this.fileSelected);
            this.Controls.Add(this.Button_Browse);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Enrichment (CallCredit API)";
            this.group_MapFields.ResumeLayout(false);
            this.group_MapFields.PerformLayout();
            this.StatusBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_Browse;
        private System.Windows.Forms.TextBox fileSelected;
        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.Button button_Enrich;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label_FileOkay;
        private System.Windows.Forms.GroupBox group_MapFields;
        private System.Windows.Forms.ComboBox map_s;
        private System.Windows.Forms.ComboBox map_f;
        private System.Windows.Forms.ComboBox map_t;
        private System.Windows.Forms.ComboBox map_n;
        private System.Windows.Forms.Label lab_s;
        private System.Windows.Forms.Label lab_f;
        private System.Windows.Forms.Label lab_t;
        private System.Windows.Forms.Label lab_name;
        private System.Windows.Forms.ComboBox map_a6;
        private System.Windows.Forms.ComboBox map_a5;
        private System.Windows.Forms.ComboBox map_a4;
        private System.Windows.Forms.ComboBox map_a3;
        private System.Windows.Forms.ComboBox map_a2;
        private System.Windows.Forms.ComboBox map_a1;
        private System.Windows.Forms.Label lab_a6;
        private System.Windows.Forms.Label lab_a5;
        private System.Windows.Forms.Label lab_a4;
        private System.Windows.Forms.Label lab_a3;
        private System.Windows.Forms.Label lab_a2;
        private System.Windows.Forms.Label lab_a1;
        private System.Windows.Forms.Label lab_dob;
        private System.Windows.Forms.ComboBox map_dob;
        private System.Windows.Forms.ComboBox map_zip;
        private System.Windows.Forms.Label lab_zip;
        private System.Windows.Forms.Label lab_a7;
        private System.Windows.Forms.ComboBox map_a7;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label label_ETA;
        private System.Windows.Forms.ComboBox map_mob;
        private System.Windows.Forms.Label lab_mob;
        private System.Windows.Forms.GroupBox StatusBox;
        private System.Windows.Forms.RichTextBox StatusBoxText;
    }
}

