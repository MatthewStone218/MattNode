namespace MattNode
{
    partial class Node
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
            textBox = new TextBox();
            TypeBox = new TextBox();
            NameBox = new TextBox();
            NameLabel = new Label();
            TypeLabel = new Label();
            SuspendLayout();
            // 
            // textBox
            // 
            textBox.Location = new Point(20, 106);
            textBox.Multiline = true;
            textBox.Name = "textBox";
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.Size = new Size(368, 162);
            textBox.TabIndex = 1;
            textBox.TextChanged += TextBox_TextChanged;
            textBox.MouseDown += ShowInspector;
            // 
            // TypeBox
            // 
            TypeBox.Location = new Point(79, 74);
            TypeBox.Name = "TypeBox";
            TypeBox.Size = new Size(309, 23);
            TypeBox.TabIndex = 2;
            TypeBox.TextChanged += TypeBox_TextChanged;
            TypeBox.MouseDown += ShowInspector;
            // 
            // NameBox
            // 
            NameBox.Location = new Point(79, 41);
            NameBox.Name = "NameBox";
            NameBox.Size = new Size(309, 23);
            NameBox.TabIndex = 3;
            NameBox.TextChanged += NameBox_TextChanged;
            NameBox.MouseDown += ShowInspector;
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Font = new Font("맑은 고딕", 12F);
            NameLabel.Location = new Point(20, 42);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(53, 21);
            NameLabel.TabIndex = 4;
            NameLabel.Text = "Name";
            NameLabel.MouseDown += Drag_MouseDown;
            NameLabel.MouseLeave += Drag_MouseLeave;
            NameLabel.MouseMove += Drag_MouseMove;
            NameLabel.MouseUp += Drag_MouseUp;
            // 
            // TypeLabel
            // 
            TypeLabel.AutoSize = true;
            TypeLabel.Font = new Font("맑은 고딕", 12F);
            TypeLabel.Location = new Point(20, 74);
            TypeLabel.Name = "TypeLabel";
            TypeLabel.Size = new Size(46, 21);
            TypeLabel.TabIndex = 5;
            TypeLabel.Text = "Type";
            TypeLabel.MouseDown += Drag_MouseDown;
            TypeLabel.MouseLeave += Drag_MouseLeave;
            TypeLabel.MouseMove += Drag_MouseMove;
            TypeLabel.MouseUp += Drag_MouseUp;
            // 
            // Node
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(TypeLabel);
            Controls.Add(NameLabel);
            Controls.Add(NameBox);
            Controls.Add(TypeBox);
            Controls.Add(textBox);
            Name = "Node";
            Size = new Size(407, 289);
            MouseDown += Drag_MouseDown;
            MouseLeave += Drag_MouseLeave;
            MouseMove += Drag_MouseMove;
            MouseUp += Drag_MouseUp;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox textBox;
        private TextBox TypeBox;
        private TextBox NameBox;
        private Label NameLabel;
        private Label TypeLabel;
    }
}
