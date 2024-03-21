using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace FileSearch
{
    public partial class FileSearchForm : Form
    {
        private string directoryName;

        private Thread fileSearchThread;

        private Thread getAllFilesCountThread;

        private ManualResetEvent manualResetEvent = new ManualResetEvent(true);

        private System.Windows.Forms.Timer timer;

        private Stopwatch stopwatch;

        private UserSettings settings;

        private int foundFiles = 0;
        private int allFilesCount = 0;

        public FileSearchForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            settings = UserSettings.Load();

            directoryName = settings.Directory;
            if (directoryName != null)
            {
                searchButton.Enabled = true;
            }

            textBoxDirectory.Text = settings.Directory;

            fileNameTextBox.Text = settings.FileNameRegex;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            ClearFileSearchControls();
            SearchStartedUpdate();

            UpdateSettings();

            InitThreads();

            CreateStopWatchAndTimer();
        }

        private void ClearFileSearchControls()
        {

            fileSearchResultTreeView.Nodes.Clear();
            labelTotalFiles.Text = String.Empty;
            labelFoundFiles.Text = String.Empty;
        }

        private void CreateStopWatchAndTimer()
        {
            stopwatch = new Stopwatch();

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1;
            timer.Tick += Timer_Tick;
            timer.Start();

            stopwatch.Start();
        }

        private void InitThreads()
        {
            AbortThreadsIfExist();
            CreateAndStartThreads();
        }

        private void CreateAndStartThreads()
        {

            fileSearchThread = new Thread(new ThreadStart(SearchFiles));
            fileSearchThread.IsBackground = true;
            fileSearchThread.Start();

            getAllFilesCountThread = new Thread(new ThreadStart(GetAllFilesCount));
            getAllFilesCountThread.IsBackground = true;
            getAllFilesCountThread.Start();
        }

        private void AbortThreadsIfExist()
        {
            fileSearchThread?.Abort();
            getAllFilesCountThread?.Abort();
        }

        private void UpdateSettings()
        {
            settings.Directory = directoryName;
            settings.FileNameRegex = fileNameTextBox.Text;
            settings.Save();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            labelSearchTime.Text = stopwatch.Elapsed.ToString();
        }

        private void SearchFiles()
        {
            var directories = Directory.GetDirectories(directoryName);

            manualResetEvent.Set();

            try
            {
                foreach (var directory in directories)
                {
                    try
                    {
                        foreach (var file in Directory.EnumerateFiles(directory, "*", SearchOption.AllDirectories)
                                .Where(f => Regex.IsMatch(f, fileNameTextBox.Text)))
                        {
                            if (file != null)
                            {
                                var filePathSplit = file.Split('\\').ToList();

                                AddFileToTreeView(filePathSplit, 0, fileSearchResultTreeView.Nodes);

                                UpdateFoundFilesNumber();
                                
                            }

                            manualResetEvent.WaitOne();
                        }
                    }
                    catch (UnauthorizedAccessException) { }
                }

                foreach (var file in Directory.EnumerateFiles(directoryName, "*")
                            .Where(f => Regex.IsMatch(f, fileNameTextBox.Text)))
                {
                    if (file != null)
                    {
                        var filePathSplit = file.Split('\\').ToList();

                        AddFileToTreeView(filePathSplit, 0, fileSearchResultTreeView.Nodes);

                        UpdateFoundFilesNumber();
                    }

                    manualResetEvent.WaitOne();
                }
            }
            catch (UnauthorizedAccessException) { }

            if (stopSearchButton.InvokeRequired)
            {
                Action disableStopButton = delegate 
                {
                    SearchCompletedUpdate();
                };
                stopSearchButton.Invoke(disableStopButton);
            }

            stopwatch.Stop();
        }

        private void UpdateFoundFilesNumber()
        {
            if (labelFoundFiles.InvokeRequired)
            {
                foundFiles++;
                Action showAllFilesNumber = delegate { labelFoundFiles.Text = "Найдено файлов: " + foundFiles; };
                labelTotalFiles.Invoke(showAllFilesNumber);
            }
        }

        private void GetAllFilesCount()
        {
            var directories = Directory.GetDirectories(directoryName);

            manualResetEvent.Set();

            try
            {
                foreach (var directory in directories)
                {
                    try
                    {
                        allFilesCount += Directory.EnumerateFiles(directory, "*", SearchOption.AllDirectories).Count();
                        UpdateAllFilesCount();
                    }
                    catch (UnauthorizedAccessException) { }

                    manualResetEvent.WaitOne();
                }

                allFilesCount += Directory.EnumerateFiles(directoryName, "*").Count();

                manualResetEvent.WaitOne();

                UpdateAllFilesCount();
            }
            catch (UnauthorizedAccessException) { }

            return;
        }

        private void UpdateAllFilesCount()
        {
            if (labelTotalFiles.InvokeRequired)
            {
                Action showAllFilesNumber = delegate { labelTotalFiles.Text = "Всего файлов: " + allFilesCount; };
                labelTotalFiles.Invoke(showAllFilesNumber);
            }
        }

        private void AddFileToTreeView(List<string> filePath, int i, TreeNodeCollection nodes)
        {
            if (nodes.Count == 0 || i == filePath.Count - 1)
            {
                AddNodes(filePath, i, nodes);

                return;
            }

            var nodeMatch = false;

            foreach(TreeNode node in nodes)
            {
                if (node.Text == filePath[i])
                {
                    nodeMatch = true;
                    i++;
                    AddFileToTreeView(filePath, i, node.Nodes);
                }
            }

            if (i < filePath.Count - 1 && !nodeMatch)
            {
                AddNodes(filePath, i, nodes);

                return;
            }
        }

        private void AddNodes(List<string> v, int i, TreeNodeCollection nodes)
        {
            if (i == v.Count)
            {
                return;
            }

            if (fileSearchResultTreeView.InvokeRequired)
            {
                Action addNode = delegate 
                { 
                    nodes.Add(new TreeNode { Text = v[i], Name = v[i] });

                    if (expandAllCheckBox.Checked)
                    {
                        fileSearchResultTreeView.ExpandAll();
                    }
                };
                fileSearchResultTreeView.Invoke(addNode);
            }

            var child = nodes.Find(v[i], true);
            i++;

            AddNodes(v, i, child[0].Nodes);
        }

        private void stopSearchButton_Click(object sender, EventArgs e)
        {
            manualResetEvent.Reset();
            stopwatch.Stop();

            SearchStoppedUpdate();
        }

        private void chooseFolderButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();

                directoryName = dialog.SelectedPath;

                textBoxDirectory.Text = directoryName;

                DirectoryChosenUpdate();
            }
        }

        private void continueFileSearchButton_Click(object sender, EventArgs e)
        {
            manualResetEvent.Set();
            stopwatch.Start();

            SearchResumedUpdate();
        }

        private void DirectoryChosenUpdate()
        {
            searchButton.Enabled = true;
            continueFileSearchButton.Enabled = false;
        }

        private void SearchStartedUpdate()
        {
            searchButton.Enabled = false;
            stopSearchButton.Enabled = true;
            chooseFolderButton.Enabled = false;
            foundFiles = 0;
            allFilesCount = 0;
        }

        private void SearchStoppedUpdate()
        {
            stopSearchButton.Enabled = false;
            continueFileSearchButton.Enabled = true;
            chooseFolderButton.Enabled = true;
        }

        private void SearchResumedUpdate()
        {
            continueFileSearchButton.Enabled = false;
            searchButton.Enabled = false;
            stopSearchButton.Enabled = true;
        }

        private void SearchCompletedUpdate()
        {
            chooseFolderButton.Enabled = true;
            searchButton.Enabled = true;
            stopSearchButton.Enabled = false;
        }
    }
}
