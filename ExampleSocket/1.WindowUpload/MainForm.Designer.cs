namespace _1.WindowUpload
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtPath = new TextBox();
            label1 = new Label();
            btnSelect = new Button();
            btnUploadToServer = new Button();
            SuspendLayout();
            // 
            // txtPath
            // 
            txtPath.Location = new Point(24, 64);
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(456, 34);
            txtPath.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(33, 33);
            label1.Name = "label1";
            label1.Size = new Size(140, 28);
            label1.TabIndex = 1;
            label1.Text = "Шлях до фото";
            // 
            // btnSelect
            // 
            btnSelect.Location = new Point(513, 61);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(126, 41);
            btnSelect.TabIndex = 2;
            btnSelect.Text = "Обрати";
            btnSelect.UseVisualStyleBackColor = true;
            btnSelect.Click += btnSelect_Click;
            // 
            // btnUploadToServer
            // 
            btnUploadToServer.Location = new Point(516, 277);
            btnUploadToServer.Name = "btnUploadToServer";
            btnUploadToServer.Size = new Size(123, 56);
            btnUploadToServer.TabIndex = 3;
            btnUploadToServer.Text = "Загрузить";
            btnUploadToServer.UseVisualStyleBackColor = true;
            btnUploadToServer.Click += btnUploadToServer_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(11F, 28F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(673, 368);
            Controls.Add(btnUploadToServer);
            Controls.Add(btnSelect);
            Controls.Add(label1);
            Controls.Add(txtPath);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(4);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Загрузка фото на сервер";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPath;
        private Label label1;
        private Button btnSelect;
        private Button btnUploadToServer;
    }
}