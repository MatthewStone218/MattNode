namespace MattNode
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            MainCamera1 = new Camera();
            screenDragger1 = new ScreenDragger();
            node2 = new Node();
            node3 = new Node();
            Step1 = new System.Windows.Forms.Timer(components);
            inspector = new Inspector();
            SuspendLayout();
            // 
            // MainCamera1
            // 
            MainCamera1.Location = new Point(-2, 0);
            MainCamera1.Name = "MainCamera1";
            MainCamera1.Size = new Size(0, 0);
            MainCamera1.TabIndex = 0;
            // 
            // screenDragger1
            // 
            screenDragger1.BackColor = SystemColors.ActiveCaption;
            screenDragger1.Location = new Point(0, 0);
            screenDragger1.Name = "screenDragger1";
            screenDragger1.Size = new Size(1920, 1080);
            screenDragger1.TabIndex = 2;
            // 
            // node2
            // 
            node2.Location = new Point(1112, 533);
            node2.Name = "node2";
            node2.Size = new Size(413, 299);
            node2.TabIndex = 3;
            // 
            // node3
            // 
            node3.Location = new Point(989, 203);
            node3.Name = "node3";
            node3.Size = new Size(409, 299);
            node3.TabIndex = 4;
            // 
            // Step1
            // 
            Step1.Interval = 1;
            Step1.Tick += Step;
            // 
            // inspector
            // 
            inspector.Location = new Point(0, 0);
            inspector.Name = "inspector";
            inspector.Size = new Size(343, 1080);
            inspector.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1904, 1041);
            Controls.Add(inspector);
            Controls.Add(node3);
            Controls.Add(node2);
            Controls.Add(MainCamera1);
            Controls.Add(screenDragger1);
            Name = "Form1";
            FormClosed += MainForm_Close;
            ResumeLayout(false);
        }

        #endregion

        private Camera MainCamera1;
        private ScreenDragger screenDragger1;
        private Node node2;
        private Node node3;
        private System.Windows.Forms.Timer Step1;
        private Inspector inspector;
    }
}
