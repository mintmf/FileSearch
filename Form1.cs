using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FileSearch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            var directories = Directory.GetDirectories("C:\\Git");

            var files = new List<List<string>>();

            foreach(var directory in directories)
            {
                try
                {
                    var filesIEnumerable = Directory.EnumerateFiles(directory, "*", SearchOption.AllDirectories)
                        .Where(f => Regex.IsMatch(f, fileNameTextBox.Text));

                    files.Add(filesIEnumerable.ToList());
                }
                catch (UnauthorizedAccessException ex)
                {

                }
            }

            AddToTreeNode(files);

            //files = Directory.GetFiles("C:\\", "*foo*", SearchOption.AllDirectories);

            TreeNode tovarNode = new TreeNode("Товары");
            // Добавляем новый дочерний узел к tovarNode
            tovarNode.Nodes.Add(new TreeNode("Смартфоны"));
            // Добавляем tovarNode вместе с дочерними узлами в TreeView
            fileSearchResultTreeView.Nodes.Add(tovarNode);
            // Добавляем второй очерний узел к первому узлу в TreeView
            fileSearchResultTreeView.Nodes[0].Nodes.Add(new TreeNode("Планшеты"));

            TreeNode tovarNode2 = new TreeNode("Товары");
            // Добавляем новый дочерний узел к tovarNode
            tovarNode2.Nodes.Add(new TreeNode("Смартфоны"));
            // Добавляем tovarNode вместе с дочерними узлами в TreeView
            fileSearchResultTreeView.Nodes.Add(tovarNode2);
            // Добавляем второй очерний узел к первому узлу в TreeView
            fileSearchResultTreeView.Nodes[1].Nodes.Add(new TreeNode("Планшеты"));

            fileSearchResultTreeView.ExpandAll();
        }

        private void AddToTreeNode(List<List<string>> files)
        {
            foreach(var file in files)
            {
                if (file != null)
                {
                    foreach(var f in file)
                    {
                        
                    }
                }
            }
        }

        private void stopSearchButton_Click(object sender, EventArgs e)
        {

        }
    }
}
