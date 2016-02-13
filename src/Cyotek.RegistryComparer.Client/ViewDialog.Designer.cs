namespace Cyotek.RegistryComparer.Client
{
  partial class ViewDialog
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewDialog));
      this.closeButton = new System.Windows.Forms.Button();
      this.imageList = new System.Windows.Forms.ImageList(this.components);
      this.splitContainer = new System.Windows.Forms.SplitContainer();
      this.treeView = new global::Cyotek.RegistryComparer.Client.RegistrySnapshotTreeView();
      this.listView = new global::Cyotek.RegistryComparer.Client.RegistryValueSnapshotListView();
      this.splitContainer.Panel1.SuspendLayout();
      this.splitContainer.Panel2.SuspendLayout();
      this.splitContainer.SuspendLayout();
      this.SuspendLayout();
      //
      // closeButton
      //
      this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.closeButton.Location = new System.Drawing.Point(700, 412);
      this.closeButton.Name = "closeButton";
      this.closeButton.Size = new System.Drawing.Size(75, 23);
      this.closeButton.TabIndex = 1;
      this.closeButton.Text = "Close";
      this.closeButton.UseVisualStyleBackColor = true;
      this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
      //
      // imageList
      //
      this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
      this.imageList.TransparentColor = System.Drawing.Color.Transparent;
      this.imageList.Images.SetKeyName(0, "folder");
      this.imageList.Images.SetKeyName(1, "binaryvalue");
      this.imageList.Images.SetKeyName(2, "stringvalue");
      //
      // splitContainer
      //
      this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
      this.splitContainer.Location = new System.Drawing.Point(12, 12);
      this.splitContainer.Name = "splitContainer";
      //
      // splitContainer.Panel1
      //
      this.splitContainer.Panel1.Controls.Add(this.treeView);
      //
      // splitContainer.Panel2
      //
      this.splitContainer.Panel2.Controls.Add(this.listView);
      this.splitContainer.Size = new System.Drawing.Size(763, 394);
      this.splitContainer.SplitterDistance = 300;
      this.splitContainer.TabIndex = 0;
      //
      // treeView
      //
      this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.treeView.ImageIndex = 0;
      this.treeView.ImageList = this.imageList;
      this.treeView.Location = new System.Drawing.Point(0, 0);
      this.treeView.Name = "treeView";
      this.treeView.SelectedImageIndex = 0;
      this.treeView.Size = new System.Drawing.Size(300, 394);
      this.treeView.TabIndex = 0;
      this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
      //
      // listView
      //
      this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.listView.FullRowSelect = true;
      this.listView.Location = new System.Drawing.Point(0, 0);
      this.listView.Name = "listView";
      this.listView.Size = new System.Drawing.Size(459, 394);
      this.listView.SmallImageList = this.imageList;
      this.listView.TabIndex = 0;
      this.listView.UseCompatibleStateImageBehavior = false;
      this.listView.View = System.Windows.Forms.View.Details;
      //
      // ViewDialog
      //
      this.AcceptButton = this.closeButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.closeButton;
      this.ClientSize = new System.Drawing.Size(787, 447);
      this.Controls.Add(this.splitContainer);
      this.Controls.Add(this.closeButton);
      this.Name = "ViewDialog";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "View Snapshot";
      this.splitContainer.Panel1.ResumeLayout(false);
      this.splitContainer.Panel2.ResumeLayout(false);
      this.splitContainer.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private RegistrySnapshotTreeView treeView;
    private System.Windows.Forms.Button closeButton;
    private System.Windows.Forms.ImageList imageList;
    private System.Windows.Forms.SplitContainer splitContainer;
    private RegistryValueSnapshotListView listView;
  }
}
