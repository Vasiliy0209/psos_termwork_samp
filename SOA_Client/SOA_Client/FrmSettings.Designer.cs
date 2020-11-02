namespace SOA_Client
{
    partial class FrmSettings
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
            this.grp_Protocol = new System.Windows.Forms.GroupBox();
            this.rb_xmlrpc = new System.Windows.Forms.RadioButton();
            this.rb_soap = new System.Windows.Forms.RadioButton();
            this.rb_rest = new System.Windows.Forms.RadioButton();
            this.btn_Apply = new System.Windows.Forms.Button();
            this.grp_Protocol.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_Protocol
            // 
            this.grp_Protocol.Controls.Add(this.rb_rest);
            this.grp_Protocol.Controls.Add(this.rb_soap);
            this.grp_Protocol.Controls.Add(this.rb_xmlrpc);
            this.grp_Protocol.Location = new System.Drawing.Point(12, 25);
            this.grp_Protocol.Name = "grp_Protocol";
            this.grp_Protocol.Size = new System.Drawing.Size(260, 141);
            this.grp_Protocol.TabIndex = 0;
            this.grp_Protocol.TabStop = false;
            this.grp_Protocol.Text = "Выберите протокол";
            // 
            // rb_xmlrpc
            // 
            this.rb_xmlrpc.AutoSize = true;
            this.rb_xmlrpc.Location = new System.Drawing.Point(21, 32);
            this.rb_xmlrpc.Name = "rb_xmlrpc";
            this.rb_xmlrpc.Size = new System.Drawing.Size(72, 17);
            this.rb_xmlrpc.TabIndex = 0;
            this.rb_xmlrpc.TabStop = true;
            this.rb_xmlrpc.Text = "XML-RPC";
            this.rb_xmlrpc.UseVisualStyleBackColor = true;
            // 
            // rb_soap
            // 
            this.rb_soap.AutoSize = true;
            this.rb_soap.Location = new System.Drawing.Point(21, 64);
            this.rb_soap.Name = "rb_soap";
            this.rb_soap.Size = new System.Drawing.Size(54, 17);
            this.rb_soap.TabIndex = 0;
            this.rb_soap.TabStop = true;
            this.rb_soap.Text = "SOAP";
            this.rb_soap.UseVisualStyleBackColor = true;
            // 
            // rb_rest
            // 
            this.rb_rest.AutoSize = true;
            this.rb_rest.Location = new System.Drawing.Point(21, 98);
            this.rb_rest.Name = "rb_rest";
            this.rb_rest.Size = new System.Drawing.Size(54, 17);
            this.rb_rest.TabIndex = 0;
            this.rb_rest.TabStop = true;
            this.rb_rest.Text = "REST";
            this.rb_rest.UseVisualStyleBackColor = true;
            // 
            // btn_Apply
            // 
            this.btn_Apply.Location = new System.Drawing.Point(13, 183);
            this.btn_Apply.Name = "btn_Apply";
            this.btn_Apply.Size = new System.Drawing.Size(259, 40);
            this.btn_Apply.TabIndex = 1;
            this.btn_Apply.Text = "Применить";
            this.btn_Apply.UseVisualStyleBackColor = true;
            this.btn_Apply.Click += new System.EventHandler(this.btn_Apply_Click);
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 243);
            this.Controls.Add(this.btn_Apply);
            this.Controls.Add(this.grp_Protocol);
            this.Name = "FrmSettings";
            this.Text = "Настройки";
            this.grp_Protocol.ResumeLayout(false);
            this.grp_Protocol.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_Protocol;
        private System.Windows.Forms.RadioButton rb_rest;
        private System.Windows.Forms.RadioButton rb_soap;
        private System.Windows.Forms.RadioButton rb_xmlrpc;
        private System.Windows.Forms.Button btn_Apply;
    }
}