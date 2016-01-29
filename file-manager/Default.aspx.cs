using file_manager.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace file_manager
{
    public partial class _Default : Page
    {
        protected String RootPath;
        protected String CurrentPath;
        private String StorageRoot = "~/Storage/demouser";
        private String[] AllowedExtensions = new String[] {
            ".gif", ".pdf", ".png", ".jpg", ".jpeg",
            ".doc", ".docx", ".xls", ".xlsx", ".txt",
            ".psd", ".eps", ".zip", ".ai", ".ppt", ".pptx" };

        // Load page
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateTree();
        }

        private void CreateTree()
        {
            RootPath = Server.MapPath(StorageRoot);

            FilesTree.Nodes.Clear();

            DirectoryInfo rootDirectory = new DirectoryInfo(RootPath);
            PopulateFiles(rootDirectory.GetFiles());

            TreeNode tree = CreateDirectoryNode(rootDirectory);
            FilesTree.Nodes.Add(tree);
            FilesTree.ExpandAll();
        }

        private TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            TreeNode directoryNode = null;

            if (directoryInfo.Exists)
            {
                directoryNode = new TreeNode(directoryInfo.Name) { Value = directoryInfo.FullName };

                // directoryNode.NavigateUrl = "";
                directoryNode.SelectAction = TreeNodeSelectAction.Select;

                foreach (var directory in directoryInfo.GetDirectories())
                {
                    TreeNode tree = CreateDirectoryNode(directory);
                    directoryNode.ChildNodes.Add(tree);
                }
            }

            return directoryNode;
        }

        protected void CreateFolderBtn_Click(object sender, EventArgs e)
        {
            string directoryPath = Server.MapPath(string.Format("{0}/{1}", String.IsNullOrEmpty(CurrentPath) ?
                RelativePath(RootPath) :
                RelativePath(CurrentPath),
                CreateFolderName.Text.Trim()));
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                CreateTree();
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Directory already exists.');", true);
            }
        }

        protected void DeleteFolderBtn_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(CurrentPath) && !CurrentPath.Equals(RootPath))
            {
                string directoryPath = RelativePath(CurrentPath);

                if (Directory.Exists(directoryPath))
                {
                    Directory.Delete(directoryPath);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Directory does not exist.');", true);
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You can't delete your root folder.');", true);
            }

        }

        protected void FilesTree_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeView treeView = (TreeView)sender;
            CurrentPath = treeView.SelectedValue;
            DirectoryInfo currentDirectory = new DirectoryInfo(CurrentPath);
            PopulateFiles(currentDirectory.GetFiles());
        }


        protected void PopulateFiles(FileInfo[] files)
        {
            List<FileEntry> fileList = new List<FileEntry>();
            foreach (FileInfo file in files)
            {
                fileList.Add(new FileEntry()
                {
                    FileName = file.Name,
                    FileSize = GetFileSize(file.Length),
                    FilePath = file.FullName,
                    FileType = file.Extension
                });
            }

            FilesRepeater.DataSource = fileList;
            FilesRepeater.DataBind();
        }

        protected String RelativePath(string fullName)
        {
            return "/" + fullName.Replace(Server.MapPath("~/"), String.Empty).Replace(@"\", "/");
        }

        private String GetFileSize(long fileSize)
        {
            return fileSize <= 999999 ?
                String.Format("{0}{1}", (Math.Round((fileSize / 1024f), 2)).ToString(), " KB") :
                String.Format("{0}{1}", (Math.Round((fileSize / 1024000f), 2)).ToString(), " MB)");
        }

        protected void DeleteFiles_Click(object sender, EventArgs e)
        {

        }
    }
}