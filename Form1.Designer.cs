namespace FileSearch
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.searchButton = new System.Windows.Forms.Button();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.directoryNameTextBox = new System.Windows.Forms.TextBox();
            this.fileSearchResultTreeView = new System.Windows.Forms.TreeView();
            this.stopSearchButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(311, 149);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(81, 42);
            this.searchButton.TabIndex = 0;
            this.searchButton.Text = "Поиск";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Location = new System.Drawing.Point(311, 117);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(134, 26);
            this.fileNameTextBox.TabIndex = 1;
            // 
            // directoryNameTextBox
            // 
            this.directoryNameTextBox.Location = new System.Drawing.Point(311, 85);
            this.directoryNameTextBox.Name = "directoryNameTextBox";
            this.directoryNameTextBox.Size = new System.Drawing.Size(134, 26);
            this.directoryNameTextBox.TabIndex = 2;
            // 
            // fileSearchResultTreeView
            // 
            this.fileSearchResultTreeView.Location = new System.Drawing.Point(28, 64);
            this.fileSearchResultTreeView.Name = "fileSearchResultTreeView";
            this.fileSearchResultTreeView.Size = new System.Drawing.Size(277, 201);
            this.fileSearchResultTreeView.TabIndex = 3;
            // 
            // stopSearchButton
            // 
            this.stopSearchButton.Location = new System.Drawing.Point(312, 198);
            this.stopSearchButton.Name = "stopSearchButton";
            this.stopSearchButton.Size = new System.Drawing.Size(80, 45);
            this.stopSearchButton.TabIndex = 4;
            this.stopSearchButton.Text = "СТОП";
            this.stopSearchButton.UseVisualStyleBackColor = true;
            this.stopSearchButton.Click += new System.EventHandler(this.stopSearchButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.stopSearchButton);
            this.Controls.Add(this.fileSearchResultTreeView);
            this.Controls.Add(this.directoryNameTextBox);
            this.Controls.Add(this.fileNameTextBox);
            this.Controls.Add(this.searchButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.TextBox directoryNameTextBox;
        private System.Windows.Forms.TreeView fileSearchResultTreeView;
        private System.Windows.Forms.Button stopSearchButton;
    }
}

