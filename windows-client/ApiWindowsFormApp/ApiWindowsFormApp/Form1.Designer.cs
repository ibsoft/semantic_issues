namespace ApiWindowsFormApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblToken;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label lblSearchTitle;
        private System.Windows.Forms.TextBox txtSearchTitle;
        private System.Windows.Forms.Label lblSearchMessage;
        private System.Windows.Forms.TextBox txtSearchMessage;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblMemoryTitle;
        private System.Windows.Forms.TextBox txtMemoryTitle;
        private System.Windows.Forms.Label lblMemoryIssue;
        private System.Windows.Forms.TextBox txtMemoryIssue;
        private System.Windows.Forms.Label lblMemorySolution;
        private System.Windows.Forms.TextBox txtMemorySolution;
        private System.Windows.Forms.Label lblMemoryCategory;
        private System.Windows.Forms.TextBox txtMemoryCategory;
        private System.Windows.Forms.Label lblCustomFields;
        private System.Windows.Forms.TextBox txtMemoryCustomFields;
        private System.Windows.Forms.Button btnPostMemory;

        private void InitializeComponent()
        {
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            lblToken = new Label();
            txtToken = new TextBox();
            btnLogin = new Button();
            lblSearchTitle = new Label();
            txtSearchTitle = new TextBox();
            lblSearchMessage = new Label();
            txtSearchMessage = new TextBox();
            btnSearch = new Button();
            lblMemoryTitle = new Label();
            txtMemoryTitle = new TextBox();
            lblMemoryIssue = new Label();
            txtMemoryIssue = new TextBox();
            lblMemorySolution = new Label();
            txtMemorySolution = new TextBox();
            lblMemoryCategory = new Label();
            txtMemoryCategory = new TextBox();
            lblCustomFields = new Label();
            txtMemoryCustomFields = new TextBox();
            btnPostMemory = new Button();
            statusStrip1 = new StatusStrip();
            panel1 = new Panel();
            panel2 = new Panel();
            txtTime = new Label();
            panel3 = new Panel();
            txtResults = new TextBox();
            txtURL = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // lblUsername
            // 
            lblUsername.Location = new Point(57, 13);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(100, 23);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Username:";
            // 
            // txtUsername
            // 
            txtUsername.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUsername.Location = new Point(157, 13);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(329, 27);
            txtUsername.TabIndex = 1;
            txtUsername.Text = "demo";
            // 
            // lblPassword
            // 
            lblPassword.Location = new Point(57, 53);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(100, 23);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Password:";
            // 
            // txtPassword
            // 
            txtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPassword.Location = new Point(157, 53);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(329, 27);
            txtPassword.TabIndex = 3;
            txtPassword.Text = "demo";
            txtPassword.UseSystemPasswordChar = true;
            // 
            // lblToken
            // 
            lblToken.Location = new Point(12, 47);
            lblToken.Name = "lblToken";
            lblToken.Size = new Size(45, 19);
            lblToken.TabIndex = 4;
            lblToken.Text = "Token:";
            // 
            // txtToken
            // 
            txtToken.BorderStyle = BorderStyle.FixedSingle;
            txtToken.Location = new Point(12, 69);
            txtToken.Multiline = true;
            txtToken.Name = "txtToken";
            txtToken.ReadOnly = true;
            txtToken.Size = new Size(481, 135);
            txtToken.TabIndex = 5;
            // 
            // btnLogin
            // 
            btnLogin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogin.Location = new Point(282, 88);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(75, 31);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "Login";
            btnLogin.Click += btnLogin_Click;
            // 
            // lblSearchTitle
            // 
            lblSearchTitle.Location = new Point(25, 13);
            lblSearchTitle.Name = "lblSearchTitle";
            lblSearchTitle.Size = new Size(100, 23);
            lblSearchTitle.TabIndex = 7;
            lblSearchTitle.Text = "Search Title:";
            // 
            // txtSearchTitle
            // 
            txtSearchTitle.Location = new Point(125, 13);
            txtSearchTitle.Name = "txtSearchTitle";
            txtSearchTitle.Size = new Size(361, 27);
            txtSearchTitle.TabIndex = 8;
            txtSearchTitle.Text = "Login Failure";
            // 
            // lblSearchMessage
            // 
            lblSearchMessage.Location = new Point(48, 51);
            lblSearchMessage.Name = "lblSearchMessage";
            lblSearchMessage.Size = new Size(132, 23);
            lblSearchMessage.TabIndex = 9;
            lblSearchMessage.Text = "Search Message:";
            // 
            // txtSearchMessage
            // 
            txtSearchMessage.Location = new Point(48, 77);
            txtSearchMessage.Multiline = true;
            txtSearchMessage.Name = "txtSearchMessage";
            txtSearchMessage.Size = new Size(438, 305);
            txtSearchMessage.TabIndex = 10;
            txtSearchMessage.Text = "Users are unable to log in to the application";
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(411, 404);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 37);
            btnSearch.TabIndex = 11;
            btnSearch.Text = "Search";
            btnSearch.Click += btnSearch_Click;
            // 
            // lblMemoryTitle
            // 
            lblMemoryTitle.Location = new Point(13, 11);
            lblMemoryTitle.Name = "lblMemoryTitle";
            lblMemoryTitle.Size = new Size(100, 23);
            lblMemoryTitle.TabIndex = 12;
            lblMemoryTitle.Text = "Memory Title:";
            // 
            // txtMemoryTitle
            // 
            txtMemoryTitle.Location = new Point(157, 11);
            txtMemoryTitle.Name = "txtMemoryTitle";
            txtMemoryTitle.Size = new Size(265, 27);
            txtMemoryTitle.TabIndex = 13;
            txtMemoryTitle.Text = "Demo";
            // 
            // lblMemoryIssue
            // 
            lblMemoryIssue.Location = new Point(13, 51);
            lblMemoryIssue.Name = "lblMemoryIssue";
            lblMemoryIssue.Size = new Size(118, 23);
            lblMemoryIssue.TabIndex = 14;
            lblMemoryIssue.Text = "Memory Issue:";
            // 
            // txtMemoryIssue
            // 
            txtMemoryIssue.Location = new Point(157, 51);
            txtMemoryIssue.Name = "txtMemoryIssue";
            txtMemoryIssue.Size = new Size(265, 27);
            txtMemoryIssue.TabIndex = 15;
            txtMemoryIssue.Text = "My PC not working due lack of memory.";
            // 
            // lblMemorySolution
            // 
            lblMemorySolution.Location = new Point(15, 91);
            lblMemorySolution.Name = "lblMemorySolution";
            lblMemorySolution.Size = new Size(139, 23);
            lblMemorySolution.TabIndex = 16;
            lblMemorySolution.Text = "Memory Solution:";
            // 
            // txtMemorySolution
            // 
            txtMemorySolution.Location = new Point(157, 91);
            txtMemorySolution.Name = "txtMemorySolution";
            txtMemorySolution.Size = new Size(265, 27);
            txtMemorySolution.TabIndex = 17;
            txtMemorySolution.Text = "Upgrade RAM";
            // 
            // lblMemoryCategory
            // 
            lblMemoryCategory.Location = new Point(15, 131);
            lblMemoryCategory.Name = "lblMemoryCategory";
            lblMemoryCategory.Size = new Size(139, 23);
            lblMemoryCategory.TabIndex = 18;
            lblMemoryCategory.Text = "Memory Category:";
            // 
            // txtMemoryCategory
            // 
            txtMemoryCategory.Location = new Point(157, 131);
            txtMemoryCategory.Name = "txtMemoryCategory";
            txtMemoryCategory.Size = new Size(265, 27);
            txtMemoryCategory.TabIndex = 19;
            txtMemoryCategory.Text = "PC";
            // 
            // lblCustomFields
            // 
            lblCustomFields.Location = new Point(13, 171);
            lblCustomFields.Name = "lblCustomFields";
            lblCustomFields.Size = new Size(130, 23);
            lblCustomFields.TabIndex = 20;
            lblCustomFields.Text = "Memory Custom Fields:";
            // 
            // txtMemoryCustomFields
            // 
            txtMemoryCustomFields.Location = new Point(157, 171);
            txtMemoryCustomFields.Name = "txtMemoryCustomFields";
            txtMemoryCustomFields.Size = new Size(265, 27);
            txtMemoryCustomFields.TabIndex = 21;
            txtMemoryCustomFields.Text = "Custom";
            // 
            // btnPostMemory
            // 
            btnPostMemory.Location = new Point(157, 209);
            btnPostMemory.Name = "btnPostMemory";
            btnPostMemory.Size = new Size(265, 36);
            btnPostMemory.TabIndex = 22;
            btnPostMemory.Text = "Store Memory";
            btnPostMemory.Click += btnPostMemory_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Location = new Point(0, 823);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1091, 22);
            statusStrip1.TabIndex = 23;
            statusStrip1.Text = "statusStrip1";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(txtUsername);
            panel1.Controls.Add(txtPassword);
            panel1.Controls.Add(lblUsername);
            panel1.Controls.Add(lblPassword);
            panel1.Controls.Add(btnLogin);
            panel1.Location = new Point(543, 69);
            panel1.Name = "panel1";
            panel1.Size = new Size(537, 135);
            panel1.TabIndex = 24;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(txtTime);
            panel2.Controls.Add(btnSearch);
            panel2.Controls.Add(txtSearchTitle);
            panel2.Controls.Add(txtSearchMessage);
            panel2.Controls.Add(lblSearchMessage);
            panel2.Controls.Add(lblSearchTitle);
            panel2.Location = new Point(543, 229);
            panel2.Name = "panel2";
            panel2.Size = new Size(536, 459);
            panel2.TabIndex = 25;
            // 
            // txtTime
            // 
            txtTime.AutoSize = true;
            txtTime.Location = new Point(48, 412);
            txtTime.Name = "txtTime";
            txtTime.Size = new Size(18, 20);
            txtTime.TabIndex = 12;
            txtTime.Text = "...";
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(txtMemoryIssue);
            panel3.Controls.Add(txtMemoryCustomFields);
            panel3.Controls.Add(lblCustomFields);
            panel3.Controls.Add(txtMemoryCategory);
            panel3.Controls.Add(lblMemoryCategory);
            panel3.Controls.Add(txtMemorySolution);
            panel3.Controls.Add(btnPostMemory);
            panel3.Controls.Add(lblMemoryTitle);
            panel3.Controls.Add(lblMemorySolution);
            panel3.Controls.Add(txtMemoryTitle);
            panel3.Controls.Add(lblMemoryIssue);
            panel3.Location = new Point(12, 229);
            panel3.Name = "panel3";
            panel3.Size = new Size(481, 459);
            panel3.TabIndex = 26;
            // 
            // txtResults
            // 
            txtResults.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtResults.BorderStyle = BorderStyle.FixedSingle;
            txtResults.Location = new Point(12, 700);
            txtResults.Multiline = true;
            txtResults.Name = "txtResults";
            txtResults.Size = new Size(1067, 109);
            txtResults.TabIndex = 27;
            // 
            // txtURL
            // 
            txtURL.Location = new Point(78, 10);
            txtURL.Name = "txtURL";
            txtURL.Size = new Size(710, 27);
            txtURL.TabIndex = 28;
            txtURL.Text = "localhost:5000";
            // 
            // label1
            // 
            label1.Location = new Point(543, 47);
            label1.Name = "label1";
            label1.Size = new Size(70, 19);
            label1.TabIndex = 29;
            label1.Text = "Login:";
            // 
            // label2
            // 
            label2.Location = new Point(12, 13);
            label2.Name = "label2";
            label2.Size = new Size(60, 19);
            label2.TabIndex = 30;
            label2.Text = "API URL:";
            // 
            // label3
            // 
            label3.Location = new Point(542, 207);
            label3.Name = "label3";
            label3.Size = new Size(71, 19);
            label3.TabIndex = 31;
            label3.Text = "Search memories:";
            // 
            // label4
            // 
            label4.Location = new Point(12, 207);
            label4.Name = "label4";
            label4.Size = new Size(118, 19);
            label4.TabIndex = 32;
            label4.Text = "Store new memory:";
            // 
            // Form1
            // 
            ClientSize = new Size(1091, 845);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtURL);
            Controls.Add(txtResults);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            Controls.Add(lblToken);
            Controls.Add(txtToken);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "API Interaction App";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private StatusStrip statusStrip1;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private TextBox txtResults;
        private TextBox txtURL;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label txtTime;
    }
}
