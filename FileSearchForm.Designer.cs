namespace FileSearch
{
    partial class FileSearchForm
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
            this.fileSearchResultTreeView = new System.Windows.Forms.TreeView();
            this.stopSearchButton = new System.Windows.Forms.Button();
            this.chooseFolderButton = new System.Windows.Forms.Button();
            this.directoryLabel = new System.Windows.Forms.Label();
            this.continueFileSearchButton = new System.Windows.Forms.Button();
            this.expandAllCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBoxSearchParameters = new System.Windows.Forms.GroupBox();
            this.groupBoxVisualParameters = new System.Windows.Forms.GroupBox();
            this.groupBoxSearchString = new System.Windows.Forms.GroupBox();
            this.groupBoxDirectory = new System.Windows.Forms.GroupBox();
            this.textBoxRootDirectory = new System.Windows.Forms.TextBox();
            this.groupBoxSearch = new System.Windows.Forms.GroupBox();
            this.groupBoxSearchResult = new System.Windows.Forms.GroupBox();
            this.groupBoxSearchTime = new System.Windows.Forms.GroupBox();
            this.labelSearchTime = new System.Windows.Forms.Label();
            this.groupBoxFilesTotal = new System.Windows.Forms.GroupBox();
            this.labelTotalFiles = new System.Windows.Forms.Label();
            this.groupBoxFoundFiles = new System.Windows.Forms.GroupBox();
            this.labelFoundFiles = new System.Windows.Forms.Label();
            this.groupBoxCurrentDirectory = new System.Windows.Forms.GroupBox();
            this.textBoxCurrentDirectory = new System.Windows.Forms.TextBox();
            this.groupBoxSearchParameters.SuspendLayout();
            this.groupBoxVisualParameters.SuspendLayout();
            this.groupBoxSearchString.SuspendLayout();
            this.groupBoxDirectory.SuspendLayout();
            this.groupBoxSearch.SuspendLayout();
            this.groupBoxSearchResult.SuspendLayout();
            this.groupBoxSearchTime.SuspendLayout();
            this.groupBoxFilesTotal.SuspendLayout();
            this.groupBoxFoundFiles.SuspendLayout();
            this.groupBoxCurrentDirectory.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchButton
            // 
            this.searchButton.Enabled = false;
            this.searchButton.Location = new System.Drawing.Point(6, 25);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(161, 45);
            this.searchButton.TabIndex = 0;
            this.searchButton.Text = "Искать";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Location = new System.Drawing.Point(6, 25);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(481, 26);
            this.fileNameTextBox.TabIndex = 1;
            // 
            // fileSearchResultTreeView
            // 
            this.fileSearchResultTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileSearchResultTreeView.Location = new System.Drawing.Point(3, 22);
            this.fileSearchResultTreeView.Name = "fileSearchResultTreeView";
            this.fileSearchResultTreeView.Size = new System.Drawing.Size(716, 486);
            this.fileSearchResultTreeView.TabIndex = 3;
            // 
            // stopSearchButton
            // 
            this.stopSearchButton.Enabled = false;
            this.stopSearchButton.Location = new System.Drawing.Point(173, 25);
            this.stopSearchButton.Name = "stopSearchButton";
            this.stopSearchButton.Size = new System.Drawing.Size(160, 45);
            this.stopSearchButton.TabIndex = 4;
            this.stopSearchButton.Text = "Остановить";
            this.stopSearchButton.UseVisualStyleBackColor = true;
            this.stopSearchButton.Click += new System.EventHandler(this.stopSearchButton_Click);
            // 
            // chooseFolderButton
            // 
            this.chooseFolderButton.Location = new System.Drawing.Point(388, 21);
            this.chooseFolderButton.Name = "chooseFolderButton";
            this.chooseFolderButton.Size = new System.Drawing.Size(99, 34);
            this.chooseFolderButton.TabIndex = 6;
            this.chooseFolderButton.Text = "Выбрать";
            this.chooseFolderButton.UseVisualStyleBackColor = true;
            this.chooseFolderButton.Click += new System.EventHandler(this.chooseFolderButton_Click);
            // 
            // directoryLabel
            // 
            this.directoryLabel.AutoSize = true;
            this.directoryLabel.Location = new System.Drawing.Point(256, 47);
            this.directoryLabel.Name = "directoryLabel";
            this.directoryLabel.Size = new System.Drawing.Size(9, 20);
            this.directoryLabel.TabIndex = 7;
            this.directoryLabel.Text = "\r\n";
            // 
            // continueFileSearchButton
            // 
            this.continueFileSearchButton.Enabled = false;
            this.continueFileSearchButton.Location = new System.Drawing.Point(339, 25);
            this.continueFileSearchButton.Name = "continueFileSearchButton";
            this.continueFileSearchButton.Size = new System.Drawing.Size(160, 45);
            this.continueFileSearchButton.TabIndex = 8;
            this.continueFileSearchButton.Text = "Продолжить";
            this.continueFileSearchButton.UseVisualStyleBackColor = true;
            this.continueFileSearchButton.Click += new System.EventHandler(this.continueFileSearchButton_Click);
            // 
            // expandAllCheckBox
            // 
            this.expandAllCheckBox.AutoSize = true;
            this.expandAllCheckBox.Checked = true;
            this.expandAllCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.expandAllCheckBox.Location = new System.Drawing.Point(6, 25);
            this.expandAllCheckBox.Name = "expandAllCheckBox";
            this.expandAllCheckBox.Size = new System.Drawing.Size(183, 24);
            this.expandAllCheckBox.TabIndex = 9;
            this.expandAllCheckBox.Text = "Развернуть дерево";
            this.expandAllCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBoxSearchParameters
            // 
            this.groupBoxSearchParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSearchParameters.Controls.Add(this.groupBoxVisualParameters);
            this.groupBoxSearchParameters.Controls.Add(this.groupBoxSearchString);
            this.groupBoxSearchParameters.Controls.Add(this.groupBoxDirectory);
            this.groupBoxSearchParameters.Location = new System.Drawing.Point(740, 12);
            this.groupBoxSearchParameters.Name = "groupBoxSearchParameters";
            this.groupBoxSearchParameters.Size = new System.Drawing.Size(505, 226);
            this.groupBoxSearchParameters.TabIndex = 14;
            this.groupBoxSearchParameters.TabStop = false;
            this.groupBoxSearchParameters.Text = "Параметры поиска";
            // 
            // groupBoxVisualParameters
            // 
            this.groupBoxVisualParameters.Controls.Add(this.expandAllCheckBox);
            this.groupBoxVisualParameters.Location = new System.Drawing.Point(6, 161);
            this.groupBoxVisualParameters.Name = "groupBoxVisualParameters";
            this.groupBoxVisualParameters.Size = new System.Drawing.Size(493, 57);
            this.groupBoxVisualParameters.TabIndex = 10;
            this.groupBoxVisualParameters.TabStop = false;
            this.groupBoxVisualParameters.Text = "Параматры отображения";
            // 
            // groupBoxSearchString
            // 
            this.groupBoxSearchString.Controls.Add(this.fileNameTextBox);
            this.groupBoxSearchString.Location = new System.Drawing.Point(6, 96);
            this.groupBoxSearchString.Name = "groupBoxSearchString";
            this.groupBoxSearchString.Size = new System.Drawing.Size(493, 59);
            this.groupBoxSearchString.TabIndex = 8;
            this.groupBoxSearchString.TabStop = false;
            this.groupBoxSearchString.Text = "Строка поиска";
            // 
            // groupBoxDirectory
            // 
            this.groupBoxDirectory.Controls.Add(this.textBoxRootDirectory);
            this.groupBoxDirectory.Controls.Add(this.directoryLabel);
            this.groupBoxDirectory.Controls.Add(this.chooseFolderButton);
            this.groupBoxDirectory.Location = new System.Drawing.Point(6, 25);
            this.groupBoxDirectory.Name = "groupBoxDirectory";
            this.groupBoxDirectory.Size = new System.Drawing.Size(493, 65);
            this.groupBoxDirectory.TabIndex = 7;
            this.groupBoxDirectory.TabStop = false;
            this.groupBoxDirectory.Text = "Корневая директория";
            // 
            // textBoxRootDirectory
            // 
            this.textBoxRootDirectory.Location = new System.Drawing.Point(6, 25);
            this.textBoxRootDirectory.Name = "textBoxRootDirectory";
            this.textBoxRootDirectory.ReadOnly = true;
            this.textBoxRootDirectory.Size = new System.Drawing.Size(376, 26);
            this.textBoxRootDirectory.TabIndex = 8;
            // 
            // groupBoxSearch
            // 
            this.groupBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSearch.Controls.Add(this.searchButton);
            this.groupBoxSearch.Controls.Add(this.stopSearchButton);
            this.groupBoxSearch.Controls.Add(this.continueFileSearchButton);
            this.groupBoxSearch.Location = new System.Drawing.Point(740, 244);
            this.groupBoxSearch.Name = "groupBoxSearch";
            this.groupBoxSearch.Size = new System.Drawing.Size(505, 81);
            this.groupBoxSearch.TabIndex = 15;
            this.groupBoxSearch.TabStop = false;
            this.groupBoxSearch.Text = "Поиск";
            // 
            // groupBoxSearchResult
            // 
            this.groupBoxSearchResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSearchResult.Controls.Add(this.fileSearchResultTreeView);
            this.groupBoxSearchResult.Location = new System.Drawing.Point(12, 12);
            this.groupBoxSearchResult.Name = "groupBoxSearchResult";
            this.groupBoxSearchResult.Size = new System.Drawing.Size(722, 511);
            this.groupBoxSearchResult.TabIndex = 16;
            this.groupBoxSearchResult.TabStop = false;
            this.groupBoxSearchResult.Text = "Результат поиска";
            // 
            // groupBoxSearchTime
            // 
            this.groupBoxSearchTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSearchTime.Controls.Add(this.labelSearchTime);
            this.groupBoxSearchTime.Location = new System.Drawing.Point(740, 331);
            this.groupBoxSearchTime.Name = "groupBoxSearchTime";
            this.groupBoxSearchTime.Size = new System.Drawing.Size(505, 60);
            this.groupBoxSearchTime.TabIndex = 17;
            this.groupBoxSearchTime.TabStop = false;
            this.groupBoxSearchTime.Text = "Время поиска";
            // 
            // labelSearchTime
            // 
            this.labelSearchTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSearchTime.AutoSize = true;
            this.labelSearchTime.Location = new System.Drawing.Point(33, 22);
            this.labelSearchTime.Name = "labelSearchTime";
            this.labelSearchTime.Size = new System.Drawing.Size(47, 20);
            this.labelSearchTime.TabIndex = 0;
            this.labelSearchTime.Text = "--:--:--";
            // 
            // groupBoxFilesTotal
            // 
            this.groupBoxFilesTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFilesTotal.Controls.Add(this.labelTotalFiles);
            this.groupBoxFilesTotal.Location = new System.Drawing.Point(740, 397);
            this.groupBoxFilesTotal.Name = "groupBoxFilesTotal";
            this.groupBoxFilesTotal.Size = new System.Drawing.Size(505, 60);
            this.groupBoxFilesTotal.TabIndex = 18;
            this.groupBoxFilesTotal.TabStop = false;
            this.groupBoxFilesTotal.Text = "Всего файлов";
            // 
            // labelTotalFiles
            // 
            this.labelTotalFiles.AutoSize = true;
            this.labelTotalFiles.Location = new System.Drawing.Point(7, 26);
            this.labelTotalFiles.Name = "labelTotalFiles";
            this.labelTotalFiles.Size = new System.Drawing.Size(18, 20);
            this.labelTotalFiles.TabIndex = 0;
            this.labelTotalFiles.Text = "0";
            // 
            // groupBoxFoundFiles
            // 
            this.groupBoxFoundFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFoundFiles.Controls.Add(this.labelFoundFiles);
            this.groupBoxFoundFiles.Location = new System.Drawing.Point(740, 463);
            this.groupBoxFoundFiles.Name = "groupBoxFoundFiles";
            this.groupBoxFoundFiles.Size = new System.Drawing.Size(505, 60);
            this.groupBoxFoundFiles.TabIndex = 19;
            this.groupBoxFoundFiles.TabStop = false;
            this.groupBoxFoundFiles.Text = "Найдено файлов";
            // 
            // labelFoundFiles
            // 
            this.labelFoundFiles.AutoSize = true;
            this.labelFoundFiles.Location = new System.Drawing.Point(6, 22);
            this.labelFoundFiles.Name = "labelFoundFiles";
            this.labelFoundFiles.Size = new System.Drawing.Size(18, 20);
            this.labelFoundFiles.TabIndex = 0;
            this.labelFoundFiles.Text = "0";
            // 
            // groupBoxCurrentDirectory
            // 
            this.groupBoxCurrentDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCurrentDirectory.Controls.Add(this.textBoxCurrentDirectory);
            this.groupBoxCurrentDirectory.Location = new System.Drawing.Point(15, 526);
            this.groupBoxCurrentDirectory.Name = "groupBoxCurrentDirectory";
            this.groupBoxCurrentDirectory.Size = new System.Drawing.Size(1230, 64);
            this.groupBoxCurrentDirectory.TabIndex = 20;
            this.groupBoxCurrentDirectory.TabStop = false;
            this.groupBoxCurrentDirectory.Text = "Текущая директория";
            // 
            // textBoxCurrentDirectory
            // 
            this.textBoxCurrentDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCurrentDirectory.Location = new System.Drawing.Point(6, 26);
            this.textBoxCurrentDirectory.Name = "textBoxCurrentDirectory";
            this.textBoxCurrentDirectory.ReadOnly = true;
            this.textBoxCurrentDirectory.Size = new System.Drawing.Size(1218, 26);
            this.textBoxCurrentDirectory.TabIndex = 0;
            // 
            // FileSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 601);
            this.Controls.Add(this.groupBoxCurrentDirectory);
            this.Controls.Add(this.groupBoxFoundFiles);
            this.Controls.Add(this.groupBoxFilesTotal);
            this.Controls.Add(this.groupBoxSearchTime);
            this.Controls.Add(this.groupBoxSearchResult);
            this.Controls.Add(this.groupBoxSearch);
            this.Controls.Add(this.groupBoxSearchParameters);
            this.MinimumSize = new System.Drawing.Size(1279, 657);
            this.Name = "FileSearchForm";
            this.Text = "FileSearch";
            this.groupBoxSearchParameters.ResumeLayout(false);
            this.groupBoxVisualParameters.ResumeLayout(false);
            this.groupBoxVisualParameters.PerformLayout();
            this.groupBoxSearchString.ResumeLayout(false);
            this.groupBoxSearchString.PerformLayout();
            this.groupBoxDirectory.ResumeLayout(false);
            this.groupBoxDirectory.PerformLayout();
            this.groupBoxSearch.ResumeLayout(false);
            this.groupBoxSearchResult.ResumeLayout(false);
            this.groupBoxSearchTime.ResumeLayout(false);
            this.groupBoxSearchTime.PerformLayout();
            this.groupBoxFilesTotal.ResumeLayout(false);
            this.groupBoxFilesTotal.PerformLayout();
            this.groupBoxFoundFiles.ResumeLayout(false);
            this.groupBoxFoundFiles.PerformLayout();
            this.groupBoxCurrentDirectory.ResumeLayout(false);
            this.groupBoxCurrentDirectory.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.TreeView fileSearchResultTreeView;
        private System.Windows.Forms.Button stopSearchButton;
        private System.Windows.Forms.Button chooseFolderButton;
        private System.Windows.Forms.Label directoryLabel;
        private System.Windows.Forms.Button continueFileSearchButton;
        private System.Windows.Forms.CheckBox expandAllCheckBox;
        private System.Windows.Forms.GroupBox groupBoxSearchParameters;
        private System.Windows.Forms.GroupBox groupBoxDirectory;
        private System.Windows.Forms.GroupBox groupBoxSearch;
        private System.Windows.Forms.GroupBox groupBoxSearchString;
        private System.Windows.Forms.GroupBox groupBoxVisualParameters;
        private System.Windows.Forms.TextBox textBoxRootDirectory;
        private System.Windows.Forms.GroupBox groupBoxSearchResult;
        private System.Windows.Forms.GroupBox groupBoxSearchTime;
        private System.Windows.Forms.Label labelSearchTime;
        private System.Windows.Forms.GroupBox groupBoxFilesTotal;
        private System.Windows.Forms.GroupBox groupBoxFoundFiles;
        private System.Windows.Forms.Label labelTotalFiles;
        private System.Windows.Forms.Label labelFoundFiles;
        private System.Windows.Forms.GroupBox groupBoxCurrentDirectory;
        private System.Windows.Forms.TextBox textBoxCurrentDirectory;
    }
}

