using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace FileSearch
{
    public partial class FileSearchForm : Form
    {
        private string rootDirectoryName;

        private Thread fileSearchThread;

        private readonly ManualResetEvent manualResetEvent = new ManualResetEvent(true);

        private System.Windows.Forms.Timer timer;

        private Stopwatch stopwatch;

        private UserSettings settings;

        private int foundFilesCount = 0;

        private int totalFilesCount = 0;

        public FileSearchForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        /// <summary>
        /// Загрузка параметров последнего поиска
        /// </summary>
        private void LoadSettings()
        {
            settings = UserSettings.Load();

            rootDirectoryName = settings.Directory;
            if (rootDirectoryName != null)
            {
                searchButton.Enabled = true;
            }

            textBoxRootDirectory.Text = settings.Directory;

            fileNameTextBox.Text = settings.FileNameRegex;
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            ClearFileSearchControls();
            SearchStartedUpdate();

            UpdateSettings();

            InitThread();

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

        private void InitThread()
        {
            fileSearchThread?.Abort();

            fileSearchThread = new Thread(new ThreadStart(FileSearchWrapper));

            fileSearchThread.IsBackground = true;
            fileSearchThread.Start();
        }

        private void UpdateSettings()
        {
            settings.Directory = rootDirectoryName;
            settings.FileNameRegex = fileNameTextBox.Text;
            settings.Save();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            labelSearchTime.Text = stopwatch.Elapsed.ToString();
        }

        /// <summary>
        /// Вспомогательный метод для запуска поиска в отдельном потоке
        /// </summary>
        private void FileSearchWrapper()
        {
            var rootNode = new TreeNode(rootDirectoryName);

            InvokeAddRootNodeToTreeView(rootNode);

            manualResetEvent.Set();

            FileSearch(rootDirectoryName, rootNode, fileNameTextBox.Text, new List<TreeNode>(), true);

            InvokeSearchCompletedUpdate();

            stopwatch.Stop();
        }

        private void InvokeSearchCompletedUpdate()
        {
            if (InvokeRequired)
            {
                Action searchCompleted = delegate
                {
                    SearchCompletedUpdate();
                };

                Invoke(searchCompleted);
            }
        }

        private void InvokeAddRootNodeToTreeView(TreeNode rootNode)
        {
            if (fileSearchResultTreeView.InvokeRequired)
            {
                Action addNode = delegate
                {
                    fileSearchResultTreeView.Nodes.Add(rootNode);
                };

                fileSearchResultTreeView.Invoke(addNode);
            }
        }

        /// <summary>
        /// Рекурсивная функция для поиска файлов
        /// </summary>
        /// <param name="searchDirectoryFullPath">Полное имя директории поиска</param>
        /// <param name="parentNode">Родительский узел, который был последним добавлен в дерево результатов поиска</param>
        /// <param name="searchPattern">Шаблон имени файла</param>
        /// <param name="buffer">Буфер узлов, в который добавляются папки, в которых был поиск, но еще не были найдены файлы. 
        /// Нужен, чтобы не добавлять в дерево папки, в которых не окажется файлов в дочерних папках</param>
        /// <param name="isRootNode">Флаг поиска в корневой папке</param>
        /// <returns>Признак наличия найденных файлов</returns>
        private bool FileSearch(string searchDirectoryFullPath, TreeNode parentNode, string searchPattern, List<TreeNode> buffer, bool isRootNode)
        {
            var searchDirectoryName = new DirectoryInfo(searchDirectoryFullPath).Name;

            TreeNode currentNode;

            if (isRootNode)
            {
                // Если поиск осуществляется в корневой директории, то мы можем не создавать узел для текущей папки, а использовать корневой узел
                currentNode = parentNode;
            }
            else
            {
                // Создаем новый узел для отображения текущей папки в TreeView
                currentNode = new TreeNode(searchDirectoryName);

                // В этой папке и её подпапках может не найтись файлов, поэтому добавляем ее не сразу в TreeView, а в буфер.
                // При добавлении файла этот узел мы добавим из буфера
                buffer.Add(currentNode);
            }

            try
            {
                foreach (string directory in Directory.GetDirectories(searchDirectoryFullPath, "*", SearchOption.TopDirectoryOnly))
                {
                    var isFilesFound = FileSearch(directory, parentNode, searchPattern, buffer, false);

                    if (isFilesFound)
                    {
                        // Если файлы найдены, то узел с этим файлом был добавлен в дерево вместе со всеми родительскими узлами,
                        // поэтому нужно отчистить буфер и последний добавленный в дерево узел становится текущим узлом
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
                            // Файл был добавлен в дерево вместе со всеми родительскими узлами,
                            // поэтому нужно отчистить буфер и последний добавленный в дерево узел становится текущим узлом
                            buffer = new List<TreeNode>();
                            parentNode = currentNode;
                        }
                    }

                    manualResetEvent.WaitOne();
                }

            }
            catch (UnauthorizedAccessException) 
            {
                // Игнорируем папки, к которым нет прав доступа
            }

            // Если в буфере остался узел, то удаляем его из буфера, потому что работа закончена
            buffer.Remove(currentNode);

            return currentNode.Nodes.Count > 0;
        }

        /// <summary>
        /// Добавление файла в TreeView
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <param name="parentNode">Родительский узел, который был последним добавлен в дерево результатов поиска</param>
        /// <param name="buffer">Буфер узлов, в который добавляются папки, в которых был поиск, но еще не были найдены файлы</param>
        private void AddToCurrentNode(string fileName, TreeNode parentNode, List<TreeNode> buffer)
        {
            var fileNode = new TreeNode(fileName);

            if (buffer.Count == 0)
            {
                // Если буфер пустой, то добавляем файл в родительский узел
                AddChildNode(parentNode, fileNode);

                return;
            }

            // Если буфер не пустой, то объединяем узлы их буфера
            for (int i = 0; i < buffer.Count - 1; i++)
            {
                buffer[i].Nodes.Add(buffer[i + 1]);
            }

            // В последний узел добавляем файл
            buffer[buffer.Count - 1].Nodes.Add(fileNode);

            // Добавляем ветку в родительский узел
            AddChildNode(parentNode, buffer[0]);
        }

        /// <summary>
        /// Добавление узла в родительский узел
        /// </summary>
        /// <param name="parentNode">Родительский узел</param>
        /// <param name="childNode">Дочерний узел</param>
        private void AddChildNode(TreeNode parentNode, TreeNode childNode)
        {
            if (fileSearchResultTreeView.InvokeRequired)
            {
                Action addNode = delegate
                {
                    parentNode.Nodes.Add(childNode);

                    if (expandAllCheckBox.Checked)
                    {
                        fileSearchResultTreeView.ExpandAll();
                    }
                };

                fileSearchResultTreeView.Invoke(addNode);
            }
        }

        /// <summary>
        /// Увелечиние количества всех файлов на форме
        /// </summary>
        private void IncrementTotalFilesCount()
        {
            if (labelTotalFiles.InvokeRequired)
            {
                totalFilesCount++;
                Action showTotalFilesCount = delegate { labelTotalFiles.Text = "Всего файлов: " + totalFilesCount; };
                labelTotalFiles.Invoke(showTotalFilesCount);
            }
        }

        /// <summary>
        /// Увелечиние количества найденных файлов на форме
        /// </summary>
        private void IncrementFoundFilesCount()
        {
            if (labelFoundFiles.InvokeRequired)
            {
                foundFilesCount++;
                Action showFoundFilesCount = delegate { labelFoundFiles.Text = "Найдено файлов: " + foundFilesCount; };
                labelFoundFiles.Invoke(showFoundFilesCount);
            }
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
                
                rootDirectoryName = dialog.SelectedPath;

                textBoxRootDirectory.Text = rootDirectoryName;

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
