namespace MattNode
{
    partial class LinkLine
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

            LinkLines.Remove(this);
            LinkedNode1.LinkLines.Remove(this);
            LinkedNode2.InvLinkLines.Remove(this);
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
            step = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // step
            // 
            step.Interval = 1;
            step.Tick += step_Step;
            // 
            // LinkLine
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            Name = "LinkLine";
            Size = new Size(214, 207);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer step;
    }
}
