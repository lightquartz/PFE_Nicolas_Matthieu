namespace BIDONNNN
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Lampe1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Lampe2");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Lampes", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Plafond", new System.Windows.Forms.TreeNode[] {
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Sol");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Radiateur1");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Radiateur2");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Radiateurs", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Mur1", new System.Windows.Forms.TreeNode[] {
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Mur2");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Mur3");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Mur4");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Murs", new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12});
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Piece1", new System.Windows.Forms.TreeNode[] {
            treeNode13});
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Piece2");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Piece3");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Batiment0", new System.Windows.Forms.TreeNode[] {
            treeNode14,
            treeNode15,
            treeNode16});
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.treeView1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.treeView1.Location = new System.Drawing.Point(12, 12);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Nœud12";
            treeNode1.Text = "Lampe1";
            treeNode2.Name = "Nœud13";
            treeNode2.Text = "Lampe2";
            treeNode3.Name = "Nœud11";
            treeNode3.Text = "Lampes";
            treeNode4.Name = "Nœud5";
            treeNode4.Text = "Plafond";
            treeNode5.Name = "Nœud6";
            treeNode5.Text = "Sol";
            treeNode6.Name = "Nœud15";
            treeNode6.Text = "Radiateur1";
            treeNode7.Name = "Nœud16";
            treeNode7.Text = "Radiateur2";
            treeNode8.Name = "Nœud14";
            treeNode8.Text = "Radiateurs";
            treeNode9.Name = "Nœud7";
            treeNode9.Text = "Mur1";
            treeNode10.Name = "Nœud8";
            treeNode10.Text = "Mur2";
            treeNode11.Name = "Nœud9";
            treeNode11.Text = "Mur3";
            treeNode12.Name = "Nœud10";
            treeNode12.Text = "Mur4";
            treeNode13.Name = "Nœud4";
            treeNode13.Text = "Murs";
            treeNode14.Name = "Nœud1";
            treeNode14.Text = "Piece1";
            treeNode15.Name = "Nœud2";
            treeNode15.Text = "Piece2";
            treeNode16.Name = "Nœud3";
            treeNode16.Text = "Piece3";
            treeNode17.Checked = true;
            treeNode17.Name = "Nœud0";
            treeNode17.Text = "Batiment0";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode17});
            this.treeView1.Size = new System.Drawing.Size(214, 421);
            this.treeView1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 457);
            this.Controls.Add(this.treeView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
    }
}

