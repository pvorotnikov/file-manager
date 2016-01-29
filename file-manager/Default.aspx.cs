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
        protected FileInfo[] FileList;
        protected DirectoryInfo Dir;

        /// <summary>
        /// Catch load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object Sender, EventArgs E)
        {
            String path = "./";
            BuildTree(path, DirectoryView);
            Dir = GetDirectoryInfo(path);
            FileList = Dir.GetFiles();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="DirPath"></param>
        /// <returns></returns>
        private DirectoryInfo GetDirectoryInfo(string DirPath)
        {
            try
            {
                DirectoryInfo Dir = new DirectoryInfo(DirPath);
                return Dir;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DirPath"></param>
        /// <param name="Tv"></param>
        private void BuildTree(string DirPath, TreeView Tv)
        {
            //get root directory
            DirectoryInfo RootDir = new DirectoryInfo(DirPath);

            //create and add the root node to the tree view
            TreeNode RootNode = new TreeNode(RootDir.Name, RootDir.FullName);
            Tv.Nodes.Add(RootNode);

            //begin recursively traversing the directory structure
            TraverseTree(RootDir, RootNode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CurrentDir"></param>
        /// <param name="CurrentNode"></param>
        private void TraverseTree(DirectoryInfo CurrentDir, TreeNode CurrentNode)
        {
            //loop through each sub-directory in the current one
            foreach (DirectoryInfo dir in CurrentDir.GetDirectories())
            {
                //create node and add to the tree view
                TreeNode node = new TreeNode(dir.Name, dir.FullName);
                CurrentNode.ChildNodes.Add(node);

                //recursively call same method to go down the next level of the tree
                TraverseTree(dir, node);
            }
        }
    }
}