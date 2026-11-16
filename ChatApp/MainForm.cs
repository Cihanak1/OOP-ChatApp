using System;
using System.Windows.Forms;

namespace ChatApp;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();

        btnStartServer.Click += btnStartServer_Click;
        btnConnect.Click += btnConnect_Click;
        btnSend.Click += btnSend_Click;
        txtMessage.KeyDown += txtMessage_KeyDown;
    }

    private void btnStartServer_Click(object? sender, EventArgs e)
    {
        // Sunucu başlatma işlemi Aşama 2'de eklenecektir
    }

    private void btnConnect_Click(object? sender, EventArgs e)
    {
        // Sunucuya bağlanma işlemi Aşama 2'de eklenecektir
    }

    private void btnSend_Click(object? sender, EventArgs e)
    {
        // Mesaj gönderme işlemi Aşama 2'de eklenecektir
    }

    private void txtMessage_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            btnSend.PerformClick();
            e.SuppressKeyPress = true;
        }
    }
}
