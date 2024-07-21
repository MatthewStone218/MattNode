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
            Form1.MainForm.Controls.Remove(this);

            if (LinkingNode == this) { LinkingNode = null; }
            for (int i = 0; i < LinkLines.Count; i++)
            {
                Form1.MainForm.Controls.Remove(LinkLines[i]);
                DestroyLink(i);
            }

            LinkLines = null;

            for (int i = 0; i < InvLinkLines.Count; i++)
            {
                Form1.MainForm.Controls.Remove(InvLinkLines[i]);
                DestroyInvLink(i);
            }

            InvLinkLines = null;

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
            textBox = new TextBox();
            TypeBox = new TextBox();
            NameBox = new TextBox();
            NameLabel = new Label();
            TypeLabel = new Label();
            step = new System.Windows.Forms.Timer(components);
            linkButtonRight = new Button();
            linkButtonLeft = new Button();
            linkButtonTop = new Button();
            linkButtonBottom = new Button();
            linkButtonTopRight = new Button();
            linkButtonBottomRight = new Button();
            linkButtonLeftTop = new Button();
            linkButtonBottomLeft = new Button();
            SuspendLayout();
            // 
            // textBox
            // 
            textBox.Location = new Point(26, 117);
            textBox.Multiline = true;
            textBox.Name = "textBox";
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.Size = new Size(368, 162);
            textBox.TabIndex = 2;
            textBox.TextChanged += TextBox_TextChanged;
            textBox.Enter += ShowInspector;
            textBox.MouseDown += NoneTextFocusLeaved;
            // 
            // TypeBox
            // 
            TypeBox.Location = new Point(85, 85);
            TypeBox.Name = "TypeBox";
            TypeBox.Size = new Size(309, 23);
            TypeBox.TabIndex = 1;
            TypeBox.TextChanged += TypeBox_TextChanged;
            TypeBox.Enter += ShowInspector;
            TypeBox.MouseDown += NoneTextFocusLeaved;
            // 
            // NameBox
            // 
            NameBox.Font = new Font("맑은 고딕", 24F);
            NameBox.Location = new Point(85, 24);
            NameBox.Name = "NameBox";
            NameBox.Size = new Size(309, 50);
            NameBox.TabIndex = 0;
            NameBox.TextChanged += NameBox_TextChanged;
            NameBox.Enter += ShowInspector;
            NameBox.MouseDown += NoneTextFocusLeaved;
            // 
            // NameLabel
            // 
            NameLabel.AutoSize = true;
            NameLabel.Font = new Font("맑은 고딕", 12F);
            NameLabel.Location = new Point(26, 37);
            NameLabel.Name = "NameLabel";
            NameLabel.Size = new Size(53, 21);
            NameLabel.TabIndex = 4;
            NameLabel.Text = "Name";
            NameLabel.MouseDown += NoneTextFocused;
            NameLabel.MouseLeave += Drag_MouseLeave;
            NameLabel.MouseMove += Drag_MouseMove;
            NameLabel.MouseUp += Drag_MouseUp;
            // 
            // TypeLabel
            // 
            TypeLabel.AutoSize = true;
            TypeLabel.Font = new Font("맑은 고딕", 12F);
            TypeLabel.Location = new Point(26, 85);
            TypeLabel.Name = "TypeLabel";
            TypeLabel.Size = new Size(46, 21);
            TypeLabel.TabIndex = 5;
            TypeLabel.Text = "Type";
            TypeLabel.MouseDown += NoneTextFocused;
            TypeLabel.MouseLeave += Drag_MouseLeave;
            TypeLabel.MouseMove += Drag_MouseMove;
            TypeLabel.MouseUp += Drag_MouseUp;
            // 
            // step
            // 
            step.Interval = 1;
            step.Tick += step_Step;
            // 
            // linkButtonRight
            // 
            linkButtonRight.Location = new Point(401, 20);
            linkButtonRight.Name = "linkButtonRight";
            linkButtonRight.Size = new Size(20, 264);
            linkButtonRight.TabIndex = 6;
            linkButtonRight.UseVisualStyleBackColor = true;
            linkButtonRight.Click += linkButtonRight_Click;
            // 
            // linkButtonLeft
            // 
            linkButtonLeft.Location = new Point(0, 20);
            linkButtonLeft.Name = "linkButtonLeft";
            linkButtonLeft.Size = new Size(20, 264);
            linkButtonLeft.TabIndex = 7;
            linkButtonLeft.UseVisualStyleBackColor = true;
            linkButtonLeft.Click += linkButtonLeft_Click;
            // 
            // linkButtonTop
            // 
            linkButtonTop.Location = new Point(20, 0);
            linkButtonTop.Name = "linkButtonTop";
            linkButtonTop.Size = new Size(381, 20);
            linkButtonTop.TabIndex = 8;
            linkButtonTop.UseVisualStyleBackColor = true;
            linkButtonTop.Click += linkButtonTop_Click;
            // 
            // linkButtonBottom
            // 
            linkButtonBottom.Location = new Point(20, 284);
            linkButtonBottom.Name = "linkButtonBottom";
            linkButtonBottom.Size = new Size(381, 20);
            linkButtonBottom.TabIndex = 9;
            linkButtonBottom.UseVisualStyleBackColor = true;
            linkButtonBottom.Click += linkButtonBottom_Click;
            // 
            // linkButtonTopRight
            // 
            linkButtonTopRight.Location = new Point(401, 0);
            linkButtonTopRight.Name = "linkButtonTopRight";
            linkButtonTopRight.Size = new Size(20, 20);
            linkButtonTopRight.TabIndex = 10;
            linkButtonTopRight.UseVisualStyleBackColor = true;
            linkButtonTopRight.Click += linkButtonTopRight_Click;
            // 
            // linkButtonBottomRight
            // 
            linkButtonBottomRight.Location = new Point(401, 284);
            linkButtonBottomRight.Name = "linkButtonBottomRight";
            linkButtonBottomRight.Size = new Size(20, 20);
            linkButtonBottomRight.TabIndex = 11;
            linkButtonBottomRight.UseVisualStyleBackColor = true;
            linkButtonBottomRight.Click += linkButtonBottomRight_Click;
            // 
            // linkButtonLeftTop
            // 
            linkButtonLeftTop.Location = new Point(0, 0);
            linkButtonLeftTop.Name = "linkButtonLeftTop";
            linkButtonLeftTop.Size = new Size(20, 20);
            linkButtonLeftTop.TabIndex = 12;
            linkButtonLeftTop.UseVisualStyleBackColor = true;
            linkButtonLeftTop.Click += linkButtonLeftTop_Click;
            // 
            // linkButtonBottomLeft
            // 
            linkButtonBottomLeft.Location = new Point(0, 284);
            linkButtonBottomLeft.Name = "linkButtonBottomLeft";
            linkButtonBottomLeft.Size = new Size(20, 20);
            linkButtonBottomLeft.TabIndex = 13;
            linkButtonBottomLeft.UseVisualStyleBackColor = true;
            linkButtonBottomLeft.Click += linkButtonBottomLeft_Click;
            // 
            // Node
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(linkButtonBottomLeft);
            Controls.Add(linkButtonLeftTop);
            Controls.Add(linkButtonBottomRight);
            Controls.Add(linkButtonTopRight);
            Controls.Add(linkButtonBottom);
            Controls.Add(linkButtonTop);
            Controls.Add(linkButtonLeft);
            Controls.Add(linkButtonRight);
            Controls.Add(TypeLabel);
            Controls.Add(NameLabel);
            Controls.Add(NameBox);
            Controls.Add(TypeBox);
            Controls.Add(textBox);
            Name = "Node";
            Size = new Size(421, 304);
            KeyDown += node_KeyDown;
            Leave += NoneTextFocusLeaved;
            MouseDown += NoneTextFocused;
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
        private System.Windows.Forms.Timer step;
        private Button linkButtonRight;
        private Button linkButtonLeft;
        private Button linkButtonTop;
        private Button linkButtonBottom;
        private Button linkButtonTopRight;
        private Button linkButtonBottomRight;
        private Button linkButtonLeftTop;
        private Button linkButtonBottomLeft;
    }
}
