using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ChatApp;

public partial class MainForm : Form
{
    private TcpListener? listener;
    private TcpClient? client;
    private StreamReader? reader;
    private StreamWriter? writer;
    private const int Port = 5000;
    private bool isConnected = false;

    public MainForm()
    {
        InitializeComponent();

        btnStartServer.Click += btnStartServer_Click;
        btnConnect.Click += btnConnect_Click;
        btnSend.Click += btnSend_Click;
        txtMessage.KeyDown += txtMessage_KeyDown;
        FormClosing += MainForm_FormClosing;
    }

    private async void btnStartServer_Click(object? sender, EventArgs e)
    {
        try
        {
            listener = new TcpListener(IPAddress.Any, Port);
            listener.Start();

            lblStatus.Text = "Bağlantı Durumu: Sunucu başlatıldı, bağlantı bekleniyor...";
            btnStartServer.Enabled = false;
            btnConnect.Enabled = false;

            client = await listener.AcceptTcpClientAsync();
            NetworkStream stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream) { AutoFlush = true };
            isConnected = true;

            lblStatus.Text = "Bağlantı Durumu: İstemci bağlandı!";
            lstMessages.Items.Add("[Sistem] Bir kullanıcı bağlandı.");

            MesajlariDinle();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Sunucu hatası: " + ex.Message);
            btnStartServer.Enabled = true;
            btnConnect.Enabled = true;
        }
    }

    private async void btnConnect_Click(object? sender, EventArgs e)
    {
        try
        {
            client = new TcpClient();
            lblStatus.Text = "Bağlantı Durumu: Sunucuya bağlanılıyor...";
            btnStartServer.Enabled = false;
            btnConnect.Enabled = false;

            await client.ConnectAsync("127.0.0.1", Port);
            NetworkStream stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream) { AutoFlush = true };
            isConnected = true;

            lblStatus.Text = "Bağlantı Durumu: Sunucuya bağlandı!";
            lstMessages.Items.Add("[Sistem] Sunucuya bağlanıldı.");

            MesajlariDinle();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Bağlantı hatası: " + ex.Message);
            lblStatus.Text = "Bağlantı Durumu: Bağlantı başarısız.";
            btnStartServer.Enabled = true;
            btnConnect.Enabled = true;
        }
    }

    private async void MesajlariDinle()
    {
        try
        {
            while (isConnected && reader != null)
            {
                string? gelenMesaj = await reader.ReadLineAsync();
                if (gelenMesaj == null)
                {
                    break;
                }

                lstMessages.Items.Add(gelenMesaj);
                lstMessages.TopIndex = lstMessages.Items.Count - 1;
            }
        }
        catch
        {
            // Bağlantı koptuğunda döngü sonlanır
        }

        lblStatus.Text = "Bağlantı Durumu: Bağlantı koptu.";
        lstMessages.Items.Add("[Sistem] Bağlantı kesildi.");
        isConnected = false;
        btnStartServer.Enabled = true;
        btnConnect.Enabled = true;
    }

    private async void btnSend_Click(object? sender, EventArgs e)
    {
        if (!isConnected || writer == null)
        {
            MessageBox.Show("Önce bağlantı kurmalısınız.");
            return;
        }

        string mesaj = txtMessage.Text.Trim();
        if (string.IsNullOrEmpty(mesaj))
        {
            return;
        }

        string kullaniciAdi = string.IsNullOrWhiteSpace(txtUsername.Text) ? "İsimsiz" : txtUsername.Text.Trim();
        string gonderilecekMetin = kullaniciAdi + ": " + mesaj;

        try
        {
            await writer.WriteLineAsync(gonderilecekMetin);
            lstMessages.Items.Add(gonderilecekMetin);
            lstMessages.TopIndex = lstMessages.Items.Count - 1;
            txtMessage.Clear();
            txtMessage.Focus();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Mesaj gönderilemedi: " + ex.Message);
        }
    }

    private void txtMessage_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            btnSend.PerformClick();
            e.SuppressKeyPress = true;
        }
    }

    private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        isConnected = false;
        try
        {
            reader?.Close();
            writer?.Close();
            client?.Close();
            listener?.Stop();
        }
        catch
        {
        }
    }
}
