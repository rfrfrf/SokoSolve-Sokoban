using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common.Structures;
using SokoSolve.UI.Section.Library;

namespace SokoSolve.UI.Section
{
    /// <summary>
    /// Strongly associated with <see cref="ExplorerPattern"/>, this class encapsulates
    /// an explorer's tree item. It has a weakly typed encapsulation of the domain <see cref="domainData"/> and 
    /// the UI representation <see cref="treeViewUINode"/>. It also has back references to the domain.
    /// </summary>
    public abstract class ExplorerItem : ITreeNodeBackReference<ExplorerItem>
    {
        protected TreeNode treeViewUINode;
        protected object domainData;
        private TreeNode<ExplorerItem> treeNode;
        private bool isEditable;

        /// <summary>
        /// Abstract constructor
        /// </summary>
        /// <param name="domainData"></param>
        protected ExplorerItem(object domainData)
        {
            this.domainData = domainData;
        }

        /// <summary>
        /// Syncronise the model data <see cref="DataUnTyped"/> children with <see cref="ExplorerItem"/> (the UI Model)
        /// </summary>
        public virtual void SyncDomain()
        {
            // Sync children
            foreach (TreeNode<ExplorerItem> kid in TreeNode.Children)
            {
                kid.Data.SyncDomain();
            }
        }

        void SyncCollections(IList<ExplorerItem> UIModel, TreeNodeCollection UIControls)
        {
            List<TreeNode> removeList = new List<TreeNode>();

            foreach (TreeNode node in UIControls)
            {
                ExplorerItem tag = node.Tag as ExplorerItem;
                if (tag == null)
                {
                    removeList.Add(node);
                }
                else
                {
                    if (!UIModel.Contains(tag))
                    {
                        removeList.Add(node);
                    }
                }
            }

            foreach (TreeNode node in removeList)
            {
                UIControls.Remove(node);
            }
        }

        /// <summary>
        /// Sync ExplorerModel with the UI TreeView.TreeNodes
        /// </summary>
        public virtual void SyncUI()
        {
            if (treeViewUINode == null)
            {
                // UI TreeNode required
                if (TreeNode.IsRoot)
                {
                    treeViewUINode = Explorer.TreeView.Nodes[0];
                    if (treeViewUINode == null) throw new NullReferenceException("TreeView must already have a Root node");
                }
                else
                {
                    // Add a node to the parent
                    treeViewUINode = TreeNode.Parent.Data.TreeViewUINode.Nodes.Add("Node:  " + this.GetType().ToString());
                }
              
                treeViewUINode.Tag = this;
                treeViewUINode.ImageIndex = Explorer.Controller.IconBinder.getIcon(IconSizes.Small, DataUnTyped);
                treeViewUINode.SelectedImageIndex = TreeViewUINode.ImageIndex;
            }
            else
            {
                // Already have one, but we need to check it is correctly bound...
                if (treeViewUINode.Tag == null)
                {
                    treeViewUINode.Tag = this;
                    treeViewUINode.ImageIndex = Explorer.Controller.IconBinder.getIcon(IconSizes.Small, DataUnTyped);
                    treeViewUINode.SelectedImageIndex = TreeViewUINode.ImageIndex;
                }
                else
                {
                    if (treeViewUINode.Tag != this) throw new InvalidOperationException("This should not happen");
                }
            }

            SyncCollections(TreeNode.ChildrenData, treeViewUINode.Nodes);

            // Databind the ExplorerItem to the TreeView TreeNode#
            if (domainData != null) BindUI();
        }

        /// <summary>
        /// Bind ExplorerModel with the UI TreeView.TreeNodes
        /// </summary>
        public virtual void BindUI()
        {
            if (Explorer.Controller.HasSelection)
            {
                if (Explorer.Controller.Selection[0] == treeViewUINode.Tag)
                {
                    if (Explorer.TreeView.SelectedNode != treeViewUINode)
                    {
                        Explorer.TreeView.SelectedNode = treeViewUINode;
                    }
                    return;
                }
            }
        }

        public virtual Control ShowDetail()
        {
            return null;
        }

        /// <summary>
        /// Remove any child controls
        /// </summary>
        public virtual void CleanUpDetail()
        {
            Explorer.DetailPayload.Controls.Clear();
        }


        /// <summary>
        /// Swap between a readonly and editable form
        /// </summary>
        public virtual bool IsEditable
        {
            get { return isEditable; }
            set
            {
                isEditable = value;
                SyncUI();
            }
        }


        /// <summary>
        /// Domain data (weakly typed)
        /// </summary>
        public object DataUnTyped
        {
            get { return domainData; }
            set { domainData = value; }
        }

        /// <summary>
        /// UI Tree node
        /// </summary>
        public TreeNode TreeViewUINode
        {
            get { return treeViewUINode; }
            set { treeViewUINode = value; }
        }

        /// <summary>
        /// Parent explorer, master class
        /// </summary>
        public ExplorerPattern Explorer
        {
            get { return treeNode.Tree as ExplorerPattern; }
        }

        #region ITreeNodeBackReference<ExplorerItem> Members

        /// <summary>
        /// Allow the data payload to have a referrence with its structure
        /// </summary>
        public TreeNode<ExplorerItem> TreeNode
        {
            get { return treeNode; }
            set { treeNode = value; }
        }

        /// <summary>
        /// Does a data item exist in the UI already?
        /// </summary>
        /// <param name="currentData">untyped data object</param>
        /// <returns>true if found</returns>
        protected bool ExistsInUI(object currentData)
        {
            foreach (TreeNode<ExplorerItem> childUI in TreeNode.Children)
            {
                if (object.Equals(childUI.Data.DataUnTyped, currentData))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// <see cref="SyncUICollectionWithData"/> Simple Factory Method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        protected delegate ExplorerItem CreateExplorerItem<T>(T data);

        /// <summary>
        /// This method syncronises the actual domain class T with UI Model (<see cref="ExplorerItem"/>). Its implemenation 
        /// is surprisingly complex as it has to manage changes by way of Delete and Add. I 
        /// </summary>
        /// <typeparam name="T">Domain Class</typeparam>
        /// <param name="Collection">List of domain objects to sync with</param>
        /// <param name="FactoryMethod">FactoryMethod to create the correct top type for ExplorerItem</param>
        protected void SyncUICollectionWithData<T>(IEnumerable<T> Collection, CreateExplorerItem<T> FactoryMethod)
        {
            // Remove old
            List<TreeNode<ExplorerItem>> removeList = new List<TreeNode<ExplorerItem>>();

            foreach (TreeNode<ExplorerItem> existingUI in TreeNode.Children)
            {
                // Only compare exact types
                if (existingUI.Data != null && existingUI.Data.DataUnTyped != null)
                {
                    if (existingUI.Data.DataUnTyped.GetType() != typeof(T)) continue;
                }

                bool found = false;
                foreach (T dataItem in Collection)
                {

                    if (object.ReferenceEquals(existingUI.Data.DataUnTyped, dataItem))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found) removeList.Add(existingUI);
            }

            if (removeList.Count > 0)
            {
                foreach (TreeNode<ExplorerItem> remove in removeList)
                {
                    // Remove from UI model
                    TreeNode.Children.Remove(remove);
                }
            }


            // Add new
            foreach (T dataItem in Collection)
            {
                if (!ExistsInUI(dataItem))
                {
                    // Does not exist, so add
                    TreeNode.Add(FactoryMethod(dataItem));
                }
            }
        }

     
        #endregion

        /// <summary>
        /// Helper method. Allow easy checking for selection requirements.
        /// </summary>
        /// <param name="Selection">Current selection</param>
        /// <param name="HasData">Should have data</param>
        /// <param name="Min">Min number of items</param>
        /// <param name="Max">Max number of items</param>
        /// <param name="MustBe">Required type</param>
        /// <returns>false if invalid</returns>
        static public bool SelectionHelper(List<ExplorerItem> Selection, bool HasData, int Min, int Max, Type MustBe)
        {
            if (HasData)
            {
                if (Selection == null || Selection.Count == 0) return false;
                if (Selection[0] == null|| Selection[0].DataUnTyped == null) return false;
            }
            if (Min > Selection.Count) return false;
            if (Max < Selection.Count) return false;

            if (MustBe != null)
            {
                if (Selection != null && Selection.Count > 0)
                {
                    if (Selection[0].GetType() != MustBe) return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", GetType().Name, DataUnTyped);
        }
    }

    /// <summary>
    /// Strongly type the Explorer item
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ExplorerItemBase<T> : ExplorerItem
    {
        protected ExplorerItemBase(T domainData)
            : base(domainData)
        {
        }

        /// <summary>
        /// Not sure is this reallu fits with the model
        /// </summary>
        protected LibraryController Controller
        {
            get
            {
                return base.Explorer.Controller as LibraryController;
            }
        }


        public T DomainData
        {
            get { return (T)domainData; }
            set { domainData = value; }
        }
    }

}
