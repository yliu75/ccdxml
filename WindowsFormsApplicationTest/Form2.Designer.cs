namespace XMLExplorer {
    partial class Form2 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing&&(components!=null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.textBox_url = new System.Windows.Forms.TextBox();
            this.button_comfirm = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_url
            // 
            this.textBox_url.Location = new System.Drawing.Point(12, 12);
            this.textBox_url.Name = "textBox_url";
            this.textBox_url.Size = new System.Drawing.Size(456, 21);
            this.textBox_url.TabIndex = 0;
            // 
            // button_comfirm
            // 
            this.button_comfirm.Location = new System.Drawing.Point(65, 39);
            this.button_comfirm.Name = "button_comfirm";
            this.button_comfirm.Size = new System.Drawing.Size(131, 23);
            this.button_comfirm.TabIndex = 1;
            this.button_comfirm.Text = "comfirm";
            this.button_comfirm.UseVisualStyleBackColor = true;
            this.button_comfirm.Click += new System.EventHandler(this.button_comfirm_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(289, 39);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(125, 23);
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 74);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_comfirm);
            this.Controls.Add(this.textBox_url);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBox_url;
        private System.Windows.Forms.Button button_comfirm;
        private System.Windows.Forms.Button button_cancel;
    }
}