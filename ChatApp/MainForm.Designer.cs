namespace ChatApp;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    private Panel panelTop;
    private Label lblUsername;
    private TextBox txtUsername;
    private Button btnStartServer;
    private Button btnConnect;
    private Label lblStatus;
    private ListBox lstMessages;
    private Panel panelBottom;
    private TextBox txtMessage;
    private Button btnSend;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        panelTop = new Panel();
        lblUsername = new Label();
        txtUsername = new TextBox();
        btnStartServer = new Button();
        btnConnect = new Button();
        lblStatus = new Label();
        lstMessages = new ListBox();
        panelBottom = new Panel();
        txtMessage = new TextBox();
        btnSend = new Button();
        panelTop.SuspendLayout();
        panelBottom.SuspendLayout();
        SuspendLayout();

        // panelTop
        panelTop.Dock = DockStyle.Top;
        panelTop.Height = 85;
        panelTop.Controls.Add(lblUsername);
        panelTop.Controls.Add(txtUsername);
        panelTop.Controls.Add(btnStartServer);
        panelTop.Controls.Add(btnConnect);
        panelTop.Controls.Add(lblStatus);
        panelTop.Padding = new Padding(10);

        // lblUsername
        lblUsername.AutoSize = true;
        lblUsername.Location = new Point(12, 15);
        lblUsername.Text = "Kullanıcı Adı:";

        // txtUsername
        txtUsername.Location = new Point(95, 12);
        txtUsername.Size = new Size(130, 23);
        txtUsername.Text = "Kullanici1";

        // btnStartServer
        btnStartServer.Location = new Point(240, 11);
        btnStartServer.Size = new Size(110, 26);
        btnStartServer.Text = "Sunucu Başlat";
        btnStartServer.UseVisualStyleBackColor = true;

        // btnConnect
        btnConnect.Location = new Point(360, 11);
        btnConnect.Size = new Size(120, 26);
        btnConnect.Text = "Sunucuya Bağlan";
        btnConnect.UseVisualStyleBackColor = true;

        // lblStatus
        lblStatus.AutoSize = true;
        lblStatus.Location = new Point(12, 50);
        lblStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        lblStatus.ForeColor = Color.DarkSlateGray;
        lblStatus.Text = "Bağlantı Durumu: Bağlantı yok";

        // panelBottom
        panelBottom.Dock = DockStyle.Bottom;
        panelBottom.Height = 55;
        panelBottom.Controls.Add(txtMessage);
        panelBottom.Controls.Add(btnSend);
        panelBottom.Padding = new Padding(10);

        // txtMessage
        txtMessage.Location = new Point(12, 15);
        txtMessage.Size = new Size(450, 23);

        // btnSend
        btnSend.Location = new Point(475, 14);
        btnSend.Size = new Size(95, 26);
        btnSend.Text = "Gönder";
        btnSend.UseVisualStyleBackColor = true;

        // lstMessages
        lstMessages.Dock = DockStyle.Fill;
        lstMessages.Font = new Font("Segoe UI", 9.5F);
        lstMessages.ItemHeight = 17;

        // MainForm
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(584, 440);
        Controls.Add(lstMessages);
        Controls.Add(panelTop);
        Controls.Add(panelBottom);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Chat Uygulaması";

        panelTop.ResumeLayout(false);
        panelTop.PerformLayout();
        panelBottom.ResumeLayout(false);
        panelBottom.PerformLayout();
        ResumeLayout(false);
    }
}
