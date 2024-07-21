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
            Step1 = new System.Windows.Forms.Timer(components);
            inspector = new Inspector();
            newNodeButton1 = new NewNodeButton();
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
            inspector.Visible = false;
            // 
            // newNodeButton1
            // 
            newNodeButton1.Location = new Point(1763, 12);
            newNodeButton1.Name = "newNodeButton1";
            newNodeButton1.Size = new Size(129, 30);
            newNodeButton1.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1904, 1041);
            Controls.Add(newNodeButton1);
            Controls.Add(inspector);
            Controls.Add(MainCamera1);
            Controls.Add(screenDragger1);
            Name = "Form1";
            FormClosed += MainForm_Close;
            ResumeLayout(false);
        }

        #endregion

        private Camera MainCamera1;
        private ScreenDragger screenDragger1;
        private System.Windows.Forms.Timer Step1;
        private Inspector inspector;
        private NewNodeButton newNodeButton1;
    }
}
