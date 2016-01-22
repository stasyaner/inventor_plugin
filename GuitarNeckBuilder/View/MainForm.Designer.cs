namespace GuitarNeckBuilder.View
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.LengthTextBox = new System.Windows.Forms.TextBox();
            this.LengthLabel = new System.Windows.Forms.Label();
            this.FingerboardRadiusTextBox = new System.Windows.Forms.TextBox();
            this.FingerboardRadiusLabel = new System.Windows.Forms.Label();
            this.WorkGroupBox = new System.Windows.Forms.GroupBox();
            this.InlayCheckBox = new System.Windows.Forms.CheckBox();
            this.ReverseHeadstockCheckBox = new System.Windows.Forms.CheckBox();
            this.FingerboardMaterialComboBox = new System.Windows.Forms.ComboBox();
            this.MaterialComboBox = new System.Windows.Forms.ComboBox();
            this.BuildButton = new System.Windows.Forms.Button();
            this.FretHeightTextBox = new System.Windows.Forms.TextBox();
            this.FretHeightLabel = new System.Windows.Forms.Label();
            this.FretWidthTextBox = new System.Windows.Forms.TextBox();
            this.FretWeightLabel = new System.Windows.Forms.Label();
            this.FingerboardInlayTypeLabel = new System.Windows.Forms.Label();
            this.HeadstockTypeLabel = new System.Windows.Forms.Label();
            this.FretNumberTextBox = new System.Windows.Forms.TextBox();
            this.FretNumberLabel = new System.Windows.Forms.Label();
            this.FingerboardMaterialLabel = new System.Windows.Forms.Label();
            this.MaterialLabel = new System.Windows.Forms.Label();
            this.AtTwelveFretHeightTextBox = new System.Windows.Forms.TextBox();
            this.AtTwelveFretHeightLabel = new System.Windows.Forms.Label();
            this.AtNutHeightTextBox = new System.Windows.Forms.TextBox();
            this.AtNutHeightLlabel = new System.Windows.Forms.Label();
            this.AtLastFretWidthTextBox = new System.Windows.Forms.TextBox();
            this.AtLastFretWidthLabel = new System.Windows.Forms.Label();
            this.AtNutWidthTextBox = new System.Windows.Forms.TextBox();
            this.AtNutWidthLabel = new System.Windows.Forms.Label();
            this.mainToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.WorkGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // LengthTextBox
            // 
            this.LengthTextBox.Location = new System.Drawing.Point(228, 199);
            this.LengthTextBox.Name = "LengthTextBox";
            this.LengthTextBox.Size = new System.Drawing.Size(120, 28);
            this.LengthTextBox.TabIndex = 11;
            this.LengthTextBox.Text = "300";
            this.LengthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.LengthTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyUp);
            this.LengthTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // LengthLabel
            // 
            this.LengthLabel.AutoSize = true;
            this.LengthLabel.Location = new System.Drawing.Point(7, 202);
            this.LengthLabel.Name = "LengthLabel";
            this.LengthLabel.Size = new System.Drawing.Size(60, 21);
            this.LengthLabel.TabIndex = 10;
            this.LengthLabel.Text = "Длина:";
            // 
            // FingerboardRadiusTextBox
            // 
            this.FingerboardRadiusTextBox.Location = new System.Drawing.Point(228, 163);
            this.FingerboardRadiusTextBox.Name = "FingerboardRadiusTextBox";
            this.FingerboardRadiusTextBox.Size = new System.Drawing.Size(120, 28);
            this.FingerboardRadiusTextBox.TabIndex = 9;
            this.FingerboardRadiusTextBox.Text = "250";
            this.FingerboardRadiusTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.FingerboardRadiusTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyUp);
            this.FingerboardRadiusTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // FingerboardRadiusLabel
            // 
            this.FingerboardRadiusLabel.AutoSize = true;
            this.FingerboardRadiusLabel.Location = new System.Drawing.Point(7, 166);
            this.FingerboardRadiusLabel.Name = "FingerboardRadiusLabel";
            this.FingerboardRadiusLabel.Size = new System.Drawing.Size(136, 21);
            this.FingerboardRadiusLabel.TabIndex = 8;
            this.FingerboardRadiusLabel.Text = "Радиус накладки:";
            // 
            // WorkGroupBox
            // 
            this.WorkGroupBox.Controls.Add(this.InlayCheckBox);
            this.WorkGroupBox.Controls.Add(this.ReverseHeadstockCheckBox);
            this.WorkGroupBox.Controls.Add(this.FingerboardMaterialComboBox);
            this.WorkGroupBox.Controls.Add(this.MaterialComboBox);
            this.WorkGroupBox.Controls.Add(this.BuildButton);
            this.WorkGroupBox.Controls.Add(this.FretHeightTextBox);
            this.WorkGroupBox.Controls.Add(this.FretHeightLabel);
            this.WorkGroupBox.Controls.Add(this.FretWidthTextBox);
            this.WorkGroupBox.Controls.Add(this.FretWeightLabel);
            this.WorkGroupBox.Controls.Add(this.FingerboardInlayTypeLabel);
            this.WorkGroupBox.Controls.Add(this.HeadstockTypeLabel);
            this.WorkGroupBox.Controls.Add(this.FretNumberTextBox);
            this.WorkGroupBox.Controls.Add(this.FretNumberLabel);
            this.WorkGroupBox.Controls.Add(this.FingerboardMaterialLabel);
            this.WorkGroupBox.Controls.Add(this.MaterialLabel);
            this.WorkGroupBox.Controls.Add(this.LengthTextBox);
            this.WorkGroupBox.Controls.Add(this.LengthLabel);
            this.WorkGroupBox.Controls.Add(this.FingerboardRadiusTextBox);
            this.WorkGroupBox.Controls.Add(this.FingerboardRadiusLabel);
            this.WorkGroupBox.Controls.Add(this.AtTwelveFretHeightTextBox);
            this.WorkGroupBox.Controls.Add(this.AtTwelveFretHeightLabel);
            this.WorkGroupBox.Controls.Add(this.AtNutHeightTextBox);
            this.WorkGroupBox.Controls.Add(this.AtNutHeightLlabel);
            this.WorkGroupBox.Controls.Add(this.AtLastFretWidthTextBox);
            this.WorkGroupBox.Controls.Add(this.AtLastFretWidthLabel);
            this.WorkGroupBox.Controls.Add(this.AtNutWidthTextBox);
            this.WorkGroupBox.Controls.Add(this.AtNutWidthLabel);
            this.WorkGroupBox.Location = new System.Drawing.Point(13, 14);
            this.WorkGroupBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.WorkGroupBox.Name = "WorkGroupBox";
            this.WorkGroupBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.WorkGroupBox.Size = new System.Drawing.Size(367, 532);
            this.WorkGroupBox.TabIndex = 16;
            this.WorkGroupBox.TabStop = false;
            this.WorkGroupBox.Text = "Работать тут";
            // 
            // InlayCheckBox
            // 
            this.InlayCheckBox.AutoSize = true;
            this.InlayCheckBox.Location = new System.Drawing.Point(283, 379);
            this.InlayCheckBox.Name = "InlayCheckBox";
            this.InlayCheckBox.Size = new System.Drawing.Size(18, 17);
            this.InlayCheckBox.TabIndex = 28;
            this.InlayCheckBox.TabStop = false;
            this.InlayCheckBox.UseVisualStyleBackColor = true;
            // 
            // ReverseHeadstockCheckBox
            // 
            this.ReverseHeadstockCheckBox.AutoSize = true;
            this.ReverseHeadstockCheckBox.Location = new System.Drawing.Point(283, 344);
            this.ReverseHeadstockCheckBox.Name = "ReverseHeadstockCheckBox";
            this.ReverseHeadstockCheckBox.Size = new System.Drawing.Size(18, 17);
            this.ReverseHeadstockCheckBox.TabIndex = 27;
            this.ReverseHeadstockCheckBox.TabStop = false;
            this.ReverseHeadstockCheckBox.UseVisualStyleBackColor = true;
            // 
            // FingerboardMaterialComboBox
            // 
            this.FingerboardMaterialComboBox.FormattingEnabled = true;
            this.FingerboardMaterialComboBox.Location = new System.Drawing.Point(227, 269);
            this.FingerboardMaterialComboBox.Name = "FingerboardMaterialComboBox";
            this.FingerboardMaterialComboBox.Size = new System.Drawing.Size(121, 29);
            this.FingerboardMaterialComboBox.TabIndex = 15;
            // 
            // MaterialComboBox
            // 
            this.MaterialComboBox.FormattingEnabled = true;
            this.MaterialComboBox.Location = new System.Drawing.Point(227, 233);
            this.MaterialComboBox.Name = "MaterialComboBox";
            this.MaterialComboBox.Size = new System.Drawing.Size(121, 29);
            this.MaterialComboBox.TabIndex = 13;
            // 
            // BuildButton
            // 
            this.BuildButton.AutoSize = true;
            this.BuildButton.Location = new System.Drawing.Point(113, 491);
            this.BuildButton.Name = "BuildButton";
            this.BuildButton.Size = new System.Drawing.Size(134, 31);
            this.BuildButton.TabIndex = 26;
            this.BuildButton.Text = "Построить гриф";
            this.BuildButton.UseVisualStyleBackColor = true;
            this.BuildButton.Click += new System.EventHandler(this.BuildButton_Click);
            // 
            // FretHeightTextBox
            // 
            this.FretHeightTextBox.Location = new System.Drawing.Point(228, 443);
            this.FretHeightTextBox.Name = "FretHeightTextBox";
            this.FretHeightTextBox.Size = new System.Drawing.Size(120, 28);
            this.FretHeightTextBox.TabIndex = 25;
            this.FretHeightTextBox.Text = "1";
            this.FretHeightTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.FretHeightTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyUp);
            this.FretHeightTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // FretHeightLabel
            // 
            this.FretHeightLabel.AutoSize = true;
            this.FretHeightLabel.Location = new System.Drawing.Point(7, 446);
            this.FretHeightLabel.Name = "FretHeightLabel";
            this.FretHeightLabel.Size = new System.Drawing.Size(103, 21);
            this.FretHeightLabel.TabIndex = 24;
            this.FretHeightLabel.Text = "Высота лада:";
            // 
            // FretWidthTextBox
            // 
            this.FretWidthTextBox.Location = new System.Drawing.Point(228, 407);
            this.FretWidthTextBox.Name = "FretWidthTextBox";
            this.FretWidthTextBox.Size = new System.Drawing.Size(120, 28);
            this.FretWidthTextBox.TabIndex = 23;
            this.FretWidthTextBox.Text = "2";
            this.FretWidthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.FretWidthTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyUp);
            this.FretWidthTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // FretWeightLabel
            // 
            this.FretWeightLabel.AutoSize = true;
            this.FretWeightLabel.Location = new System.Drawing.Point(7, 410);
            this.FretWeightLabel.Name = "FretWeightLabel";
            this.FretWeightLabel.Size = new System.Drawing.Size(111, 21);
            this.FretWeightLabel.TabIndex = 22;
            this.FretWeightLabel.Text = "Ширина лада:";
            // 
            // FingerboardInlayTypeLabel
            // 
            this.FingerboardInlayTypeLabel.AutoSize = true;
            this.FingerboardInlayTypeLabel.Location = new System.Drawing.Point(7, 376);
            this.FingerboardInlayTypeLabel.Name = "FingerboardInlayTypeLabel";
            this.FingerboardInlayTypeLabel.Size = new System.Drawing.Size(179, 21);
            this.FingerboardInlayTypeLabel.TabIndex = 20;
            this.FingerboardInlayTypeLabel.Text = "Инкрустация накладки:";
            // 
            // HeadstockTypeLabel
            // 
            this.HeadstockTypeLabel.AutoSize = true;
            this.HeadstockTypeLabel.Location = new System.Drawing.Point(7, 340);
            this.HeadstockTypeLabel.Name = "HeadstockTypeLabel";
            this.HeadstockTypeLabel.Size = new System.Drawing.Size(195, 21);
            this.HeadstockTypeLabel.TabIndex = 18;
            this.HeadstockTypeLabel.Text = "Реверсная форма головы:";
            // 
            // FretNumberTextBox
            // 
            this.FretNumberTextBox.Location = new System.Drawing.Point(228, 303);
            this.FretNumberTextBox.Name = "FretNumberTextBox";
            this.FretNumberTextBox.Size = new System.Drawing.Size(120, 28);
            this.FretNumberTextBox.TabIndex = 17;
            this.FretNumberTextBox.Text = "22";
            this.FretNumberTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.FretNumberTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyUp);
            this.FretNumberTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // FretNumberLabel
            // 
            this.FretNumberLabel.AutoSize = true;
            this.FretNumberLabel.Location = new System.Drawing.Point(7, 306);
            this.FretNumberLabel.Name = "FretNumberLabel";
            this.FretNumberLabel.Size = new System.Drawing.Size(144, 21);
            this.FretNumberLabel.TabIndex = 16;
            this.FretNumberLabel.Text = "Количество ладов:";
            // 
            // FingerboardMaterialLabel
            // 
            this.FingerboardMaterialLabel.AutoSize = true;
            this.FingerboardMaterialLabel.Location = new System.Drawing.Point(7, 272);
            this.FingerboardMaterialLabel.Name = "FingerboardMaterialLabel";
            this.FingerboardMaterialLabel.Size = new System.Drawing.Size(158, 21);
            this.FingerboardMaterialLabel.TabIndex = 14;
            this.FingerboardMaterialLabel.Text = "Материал накладки:";
            // 
            // MaterialLabel
            // 
            this.MaterialLabel.AutoSize = true;
            this.MaterialLabel.Location = new System.Drawing.Point(7, 236);
            this.MaterialLabel.Name = "MaterialLabel";
            this.MaterialLabel.Size = new System.Drawing.Size(86, 21);
            this.MaterialLabel.TabIndex = 12;
            this.MaterialLabel.Text = "Материал:";
            // 
            // AtTwelveFretHeightTextBox
            // 
            this.AtTwelveFretHeightTextBox.Location = new System.Drawing.Point(228, 129);
            this.AtTwelveFretHeightTextBox.Name = "AtTwelveFretHeightTextBox";
            this.AtTwelveFretHeightTextBox.Size = new System.Drawing.Size(120, 28);
            this.AtTwelveFretHeightTextBox.TabIndex = 7;
            this.AtTwelveFretHeightTextBox.Text = "14";
            this.AtTwelveFretHeightTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AtTwelveFretHeightTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyUp);
            this.AtTwelveFretHeightTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // AtTwelveFretHeightLabel
            // 
            this.AtTwelveFretHeightLabel.AutoSize = true;
            this.AtTwelveFretHeightLabel.Location = new System.Drawing.Point(7, 132);
            this.AtTwelveFretHeightLabel.Name = "AtTwelveFretHeightLabel";
            this.AtTwelveFretHeightLabel.Size = new System.Drawing.Size(182, 21);
            this.AtTwelveFretHeightLabel.TabIndex = 6;
            this.AtTwelveFretHeightLabel.Text = "Толщина на 12-ом ладу:";
            // 
            // AtNutHeightTextBox
            // 
            this.AtNutHeightTextBox.Location = new System.Drawing.Point(228, 93);
            this.AtNutHeightTextBox.Name = "AtNutHeightTextBox";
            this.AtNutHeightTextBox.Size = new System.Drawing.Size(120, 28);
            this.AtNutHeightTextBox.TabIndex = 5;
            this.AtNutHeightTextBox.Text = "13";
            this.AtNutHeightTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AtNutHeightTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyUp);
            this.AtNutHeightTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // AtNutHeightLlabel
            // 
            this.AtNutHeightLlabel.AutoSize = true;
            this.AtNutHeightLlabel.Location = new System.Drawing.Point(7, 96);
            this.AtNutHeightLlabel.Name = "AtNutHeightLlabel";
            this.AtNutHeightLlabel.Size = new System.Drawing.Size(164, 21);
            this.AtNutHeightLlabel.TabIndex = 4;
            this.AtNutHeightLlabel.Text = "Толщина на порожке:";
            // 
            // AtLastFretWidthTextBox
            // 
            this.AtLastFretWidthTextBox.Location = new System.Drawing.Point(228, 59);
            this.AtLastFretWidthTextBox.Name = "AtLastFretWidthTextBox";
            this.AtLastFretWidthTextBox.Size = new System.Drawing.Size(121, 28);
            this.AtLastFretWidthTextBox.TabIndex = 3;
            this.AtLastFretWidthTextBox.Text = "38";
            this.AtLastFretWidthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AtLastFretWidthTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyUp);
            this.AtLastFretWidthTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // AtLastFretWidthLabel
            // 
            this.AtLastFretWidthLabel.AutoSize = true;
            this.AtLastFretWidthLabel.Location = new System.Drawing.Point(7, 62);
            this.AtLastFretWidthLabel.Name = "AtLastFretWidthLabel";
            this.AtLastFretWidthLabel.Size = new System.Drawing.Size(215, 21);
            this.AtLastFretWidthLabel.TabIndex = 2;
            this.AtLastFretWidthLabel.Text = "Ширина на последнем ладу:";
            // 
            // AtNutWidthTextBox
            // 
            this.AtNutWidthTextBox.Location = new System.Drawing.Point(228, 23);
            this.AtNutWidthTextBox.Name = "AtNutWidthTextBox";
            this.AtNutWidthTextBox.Size = new System.Drawing.Size(121, 28);
            this.AtNutWidthTextBox.TabIndex = 1;
            this.AtNutWidthTextBox.Text = "35";
            this.AtNutWidthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mainToolTip.SetToolTip(this.AtNutWidthTextBox, "Задайте ширину порожка, \r\nкоторая не может быть меньше 23мм\r\nи больше ширины на п" +
        "оследнем ладу.");
            this.AtNutWidthTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyUp);
            this.AtNutWidthTextBox.Leave += new System.EventHandler(this.TextBox_Leave);
            // 
            // AtNutWidthLabel
            // 
            this.AtNutWidthLabel.AutoSize = true;
            this.AtNutWidthLabel.Location = new System.Drawing.Point(7, 26);
            this.AtNutWidthLabel.Name = "AtNutWidthLabel";
            this.AtNutWidthLabel.Size = new System.Drawing.Size(161, 21);
            this.AtNutWidthLabel.TabIndex = 0;
            this.AtNutWidthLabel.Text = "Ширина на порожке:";
            // 
            // mainToolTip
            // 
            this.mainToolTip.AutoPopDelay = 5000;
            this.mainToolTip.InitialDelay = 50;
            this.mainToolTip.ReshowDelay = 100;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(393, 560);
            this.Controls.Add(this.WorkGroupBox);
            this.Font = new System.Drawing.Font("Calibri Light", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Guitar Neck Builder 3000";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.WorkGroupBox.ResumeLayout(false);
            this.WorkGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox LengthTextBox;
        private System.Windows.Forms.Label LengthLabel;
        private System.Windows.Forms.TextBox FingerboardRadiusTextBox;
        private System.Windows.Forms.Label FingerboardRadiusLabel;
        private System.Windows.Forms.GroupBox WorkGroupBox;
        private System.Windows.Forms.Button BuildButton;
        private System.Windows.Forms.TextBox FretHeightTextBox;
        private System.Windows.Forms.Label FretHeightLabel;
        private System.Windows.Forms.TextBox FretWidthTextBox;
        private System.Windows.Forms.Label FretWeightLabel;
        private System.Windows.Forms.Label FingerboardInlayTypeLabel;
        private System.Windows.Forms.Label HeadstockTypeLabel;
        private System.Windows.Forms.TextBox FretNumberTextBox;
        private System.Windows.Forms.Label FretNumberLabel;
        private System.Windows.Forms.Label FingerboardMaterialLabel;
        private System.Windows.Forms.Label MaterialLabel;
        private System.Windows.Forms.TextBox AtTwelveFretHeightTextBox;
        private System.Windows.Forms.Label AtTwelveFretHeightLabel;
        private System.Windows.Forms.TextBox AtNutHeightTextBox;
        private System.Windows.Forms.Label AtNutHeightLlabel;
        private System.Windows.Forms.TextBox AtLastFretWidthTextBox;
        private System.Windows.Forms.Label AtLastFretWidthLabel;
        private System.Windows.Forms.Label AtNutWidthLabel;
        private System.Windows.Forms.ComboBox MaterialComboBox;
        private System.Windows.Forms.ToolTip mainToolTip;
        private System.Windows.Forms.ComboBox FingerboardMaterialComboBox;
        private System.Windows.Forms.TextBox AtNutWidthTextBox;
        private System.Windows.Forms.CheckBox ReverseHeadstockCheckBox;
        private System.Windows.Forms.CheckBox InlayCheckBox;
    }
}

