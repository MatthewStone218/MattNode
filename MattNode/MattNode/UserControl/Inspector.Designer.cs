namespace MattNode
{
    partial class Inspector
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            nameLabel = new Label();
            typeLabel = new Label();
            textBox = new TextBox();
            nameBox = new TextBox();
            typeBox = new TextBox();
            dragPanel = new Panel();
            dragPanelLabel = new Label();
            Step = new System.Windows.Forms.Timer(components);
            dragPanel.SuspendLayout();
            SuspendLayout();
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Font = new Font("맑은 고딕", 12F);
            nameLabel.Location = new Point(16, 20);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(53, 21);
            nameLabel.TabIndex = 0;
            nameLabel.Text = "Name";
            // 
            // typeLabel
            // 
            typeLabel.AutoSize = true;
            typeLabel.Font = new Font("맑은 고딕", 12F);
            typeLabel.Location = new Point(16, 53);
            typeLabel.Name = "typeLabel";
            typeLabel.Size = new Size(46, 21);
            typeLabel.TabIndex = 1;
            typeLabel.Text = "Type";
            // 
            // textBox
            // 
            textBox.Location = new Point(21, 89);
            textBox.Multiline = true;
            textBox.Name = "textBox";
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.Size = new Size(285, 963);
            textBox.TabIndex = 2;
            textBox.TextChanged += TextBox_TextChanged;
            // 
            // nameBox
            // 
            nameBox.Location = new Point(72, 22);
            nameBox.Name = "nameBox";
            nameBox.Size = new Size(234, 23);
            nameBox.TabIndex = 0;
            nameBox.TextChanged += NameBox_TextChanged;
            // 
            // typeBox
            // 
            typeBox.Location = new Point(72, 55);
            typeBox.Name = "typeBox";
            typeBox.Size = new Size(234, 23);
            typeBox.TabIndex = 1;
            typeBox.TextChanged += TypeBox_TextChanged;
            // 
            // dragPanel
            // 
            dragPanel.BackColor = SystemColors.ButtonShadow;
            dragPanel.Controls.Add(dragPanelLabel);
            dragPanel.Location = new Point(312, 0);
            dragPanel.Name = "dragPanel";
            dragPanel.Size = new Size(31, 1080);
            dragPanel.TabIndex = 7;
            dragPanel.MouseDown += dragPanel_MouseDown;
            dragPanel.MouseLeave += dragPanel_MouseLeave;
            dragPanel.MouseUp += dragPanel_MouseUp;
            // 
            // dragPanelLabel
            // 
            dragPanelLabel.AutoSize = true;
            dragPanelLabel.Font = new Font("맑은 고딕", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            dragPanelLabel.Location = new Point(0, 528);
            dragPanelLabel.Name = "dragPanelLabel";
            dragPanelLabel.Size = new Size(34, 21);
            dragPanelLabel.TabIndex = 0;
            dragPanelLabel.Text = "<>";
            // 
            // Step
            // 
            Step.Interval = 1;
            Step.Tick += StepTick;
            // 
            // Inspector
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(dragPanel);
            Controls.Add(typeBox);
            Controls.Add(nameBox);
            Controls.Add(textBox);
            Controls.Add(typeLabel);
            Controls.Add(nameLabel);
            Name = "Inspector";
            Size = new Size(343, 1080);
            dragPanel.ResumeLayout(false);
            dragPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label nameLabel;
        private Label typeLabel;
        private TextBox textBox;
        private TextBox nameBox;
        private TextBox typeBox;
        private Panel dragPanel;
        private Label dragPanelLabel;
        private System.Windows.Forms.Timer Step;
    }
}
