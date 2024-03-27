using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

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

        private int foundFilesCount = 0;
        private int totalFilesCount = 0;

        private TreeNode currentNode;

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

            //fileSearchThread = new Thread(new ThreadStart(SearchFiles));
            

            fileSearchThread = new Thread(new ThreadStart(FileSearchWrapper));

            fileSearchThread.IsBackground = true;
            fileSearchThread.Start();

            //getAllFilesCountThread = new Thread(new ThreadStart(GetAllFilesCount));
            //getAllFilesCountThread.IsBackground = true;
            //getAllFilesCountThread.Start();
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

        private void FileSearchWrapper()
        {
            var rootNode = new TreeNode(directoryName);

            if (fileSearchResultTreeView.InvokeRequired)
            {
                Action addNode = delegate
                {
                    //fileSearchResultTreeView.Nodes.Add(rootNode);
                };

                fileSearchResultTreeView.Invoke(addNode);
            }
            
            manualResetEvent.Set();

            FileSearch(directoryName, rootNode, fileNameTextBox.Text, new List<TreeNode>());

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

        private TreeNode FileSearch(string searchDirectoryFullPath, TreeNode parentNode, string searchPattern, List<TreeNode> buffer)
        {
            var searchDirectoryName = new DirectoryInfo(searchDirectoryFullPath).Name;
            var currentNode = new TreeNode(searchDirectoryName);
            buffer.Add(currentNode);

            try
            {
                foreach (string directory in Directory.GetDirectories(searchDirectoryFullPath, "*", SearchOption.TopDirectoryOnly))
                {
                    var searchResult = FileSearch(directory, parentNode, searchPattern, buffer);

                    if (searchResult != null)
                    {
                        buffer = new List<TreeNode>();
                        parentNode = currentNode;
                    }

                    manualResetEvent.WaitOne();
                }

                foreach (string fileFullPath in Directory.GetFiles(searchDirectoryFullPath))
                {
                    IncrementTotalFilesCount();

                    var fileName = Path.GetFileName(fileFullPath);

                    if (Regex.IsMatch(fileName, searchPattern))
                    {
                        IncrementFoundFilesCount();

                        AddToCurrentNode(fileName, parentNode, buffer);

                        if (parentNode != currentNode)
                        {
                            buffer = new List<TreeNode>();
                            parentNode = currentNode;
                        }
                    }

                    manualResetEvent.WaitOne();
                }

            }
            catch (UnauthorizedAccessException) { }

            buffer.Remove(currentNode);

            return parentNode == currentNode ? currentNode : null;
        }

        private void AddToCurrentNode(string fileName, TreeNode parentNode, List<TreeNode> buffer)
        {
            var fileNode = new TreeNode(fileName);

            if (buffer.Count == 0)
            {
                if (fileSearchResultTreeView.InvokeRequired)
                {
                    Action addNode = delegate
                    {
                        parentNode.Nodes.Add(fileName);

                        if (expandAllCheckBox.Checked)
                        {
                            fileSearchResultTreeView.ExpandAll();
                        }
                    };

                    fileSearchResultTreeView.Invoke(addNode);
                }

                return;
            }

            for (int i = 0; i < buffer.Count - 1; i++)
            {
                buffer[i].Nodes.Add(buffer[i + 1]);
            }

            buffer[buffer.Count - 1].Nodes.Add(fileNode);

            if (fileSearchResultTreeView.InvokeRequired)
            {
                Action addNode = delegate
                {
                    parentNode.Nodes.Add(buffer[0]);

                    if (expandAllCheckBox.Checked)
                    {
                        fileSearchResultTreeView.ExpandAll();
                    }
                };

                fileSearchResultTreeView.Invoke(addNode);
            }
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

                                IncrementFoundFilesCount();
                                
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

                        IncrementFoundFilesCount();
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

        private void IncrementTotalFilesCount()
        {
            if (labelTotalFiles.InvokeRequired)
            {
                totalFilesCount++;
                Action showTotalFilesCount = delegate { labelTotalFiles.Text = "Всего файлов: " + totalFilesCount; };
                labelTotalFiles.Invoke(showTotalFilesCount);
            }
        }

        private void IncrementFoundFilesCount()
        {
            if (labelFoundFiles.InvokeRequired)
            {
                foundFilesCount++;
                Action showFoundFilesCount = delegate { labelFoundFiles.Text = "Найдено файлов: " + foundFilesCount; };
                labelFoundFiles.Invoke(showFoundFilesCount);
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
                        totalFilesCount += Directory.EnumerateFiles(directory, "*", SearchOption.AllDirectories).Count();
                        UpdateAllFilesCount();
                    }
                    catch (UnauthorizedAccessException) { }

                    manualResetEvent.WaitOne();
                }

                totalFilesCount += Directory.EnumerateFiles(directoryName, "*").Count();

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
                Action showAllFilesNumber = delegate { labelTotalFiles.Text = "Всего файлов: " + totalFilesCount; };
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
            foundFilesCount = 0;
            totalFilesCount = 0;
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
