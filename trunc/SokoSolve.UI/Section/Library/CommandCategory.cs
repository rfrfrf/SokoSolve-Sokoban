using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SokoSolve.Common.Structures;
using SokoSolve.Core.Model;
using SokoSolve.Core.Model.DataModel;
using SokoSolve.UI.Controls.Secondary;
using SokoSolve.UI.Section.Library.Items;

namespace SokoSolve.UI.Section.Library
{
    //#################################################################
    //#################################################################
    //#################################################################


    class CategoryNew : CommandLibraryBase
    {
        public CategoryNew(Controller<ExplorerItem> controller, object[] buttonControls) : base(controller, buttonControls)
        {
            Init("New Category", "Create a new sub-category");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            ItemCategory cat = instance.Context[0] as ItemCategory;
            if (cat != null)
            {
                Category newCat = new Category();
                newCat.CategoryID = Controller.Current.IdProvider.GetNextIDString("C{0}");
                newCat.CategoryParentREF = cat.DomainData.CategoryID;
                newCat.Details = new GenericDescription();
                newCat.Details.Name = "New Category";
                newCat.Order = cat.DomainData.TreeNode.Tree.Nodes.Count + 1;
                cat.DomainData.TreeNode.Add(newCat);

                // Select new cat, set to edit...

                // Refresh the UI model to updated domain data
                cat.SyncDomain();

                // Refresh enture tree
                Controller.Explorer.SyncUI();
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = ExplorerItem.SelectionHelper(selection, true, 1, 1, typeof (ItemCategory));
        }
    }

    //#################################################################
    //#################################################################
    //#################################################################

    class CategoryDelete : CommandLibraryBase
    {
        public CategoryDelete(Controller<ExplorerItem> controller, object[] buttonControls) : base(controller, buttonControls)
        {
            Init("Delete Category", "Delete an existing category and related");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            ItemCategory delMe = instance.Context[0] as ItemCategory;
            if (delMe != null)
            {
                // Find parent, then remove current
                TreeNode<ExplorerItem> parent = delMe.TreeNode.Parent;
                ItemCategory catParent = parent.Data as ItemCategory;
                if (catParent != null)
                {
                    catParent.DomainData.TreeNode.RemoveChild(delMe.DomainData);

                    // Refresh the UI model to updated domain data
                    catParent.SyncDomain();
                }

                // Set selection as parent
                Controller.UpdateSelectionSingle(catParent);

                // Refresh enture tree
                Controller.Explorer.SyncUI();
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = ExplorerItem.SelectionHelper(selection, true, 1, 1, typeof(ItemCategory));
        }
    }

    //#################################################################
    //#################################################################
    //#################################################################

    class CategoryEdit : CommandLibraryBase
    {
        public CategoryEdit(Controller<ExplorerItem> controller, object[] buttonControls)
            : base(controller, buttonControls)
        {
            Init("Edit Category", "Edit category properties");
        }

        protected override void ExecuteImplementation(CommandInstance<ExplorerItem> instance)
        {
            if (!instance.hasSelection) return;

            ExplorerItem item = instance.Context[0];

            if (instance.Status == ExecutionStatus.Working)
            {

                item.IsEditable = true;
                Control implUI = item.ShowDetail();

                // Wait for call-back
                instance.Status = ExecutionStatus.AwaitingCallback;

                // Register for the call-bak events
                ucGenericDescription details = implUI as ucGenericDescription;
                if (details != null)
                {
                    details.Tag = instance;
                    details.ButtonOK.Tag = instance;
                    details.ButtonOK.Click += new EventHandler(ButtonOK_Click);
                    details.ButtonCancel.Tag = instance;
                    details.ButtonCancel.Click += new EventHandler(ButtonCancel_Click);
                }
                return;
            }

            if (instance.Status == ExecutionStatus.AwaitingCallback)
            {
                if (instance.Param != null && instance.Param.ToString() == "Ok")
                {
                    ucGenericDescription editControl = item.ShowDetail() as ucGenericDescription;
                    if (editControl == null) throw new InvalidCastException("Detail should be ucGenericDescription");
                    // Replace details
                    Core.Model.Category category = (Core.Model.Category)item.DataUnTyped;
                    category.Details = editControl.Data;

                    item.IsEditable = false;
                    item.ShowDetail();
                }

                if (instance.Param != null && instance.Param.ToString() == "Cancel")
                {
                    item.IsEditable = false;
                    item.ShowDetail();
                    return;
                }
            }
        }

        void ButtonCancel_Click(object sender, EventArgs e)
        {
            Control senderCtrl = sender as Control;
            if (senderCtrl != null && senderCtrl.Tag != null)
            {
                CommandInstance<ExplorerItem> cmdInst = senderCtrl.Tag as CommandInstance<ExplorerItem>;
                if (cmdInst != null)
                {
                    cmdInst.Param = "Cancel";

                    // Perform call-back
                    cmdInst.Command.Execute(cmdInst);
                }
            }
        }

        void ButtonOK_Click(object sender, EventArgs e)
        {
            Control senderCtrl = sender as Control;
            if (senderCtrl != null && senderCtrl.Tag != null)
            {
                CommandInstance<ExplorerItem> cmdInst = senderCtrl.Tag as CommandInstance<ExplorerItem>;
                if (cmdInst != null)
                {
                    cmdInst.Param = "Ok";

                    // Perform call-back
                    cmdInst.Command.Execute(cmdInst);
                }
            }
        }

        public override void UpdateForSelection(List<ExplorerItem> selection)
        {
            Enabled = ExplorerItem.SelectionHelper(selection, true, 1, 1, typeof(ItemCategory));
        }


    }

    //#################################################################
    //#################################################################
    //#################################################################
}
