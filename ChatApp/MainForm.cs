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

        btnSend.Enabled = false;

        btnStartServer.Click += btnStartServer_Click;
        btnConnect.Click += btnConnect_Click;
        btnSend.Click += btnSend_Click;
        txtMessage.KeyDown += txtMessage_KeyDown;
        FormClosing += MainForm_FormClosing;
    }

    private async void btnStartServer_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtUsername.Text))
        {
            MessageBox.Show("Lütfen bir kullanıcı adı girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtUsername.Focus();
            return;
        }

        if (listener != null || isConnected)
        {
            MessageBox.Show("Sunucu zaten çalışıyor.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        btnStartServer.Enabled = false;
        btnConnect.Enabled = false;

        try
        {
            listener = new TcpListener(IPAddress.Any, Port);
            listener.Start();

            lblStatus.Text = "Bağlantı Durumu: Sunucu başlatıldı, bağlantı bekleniyor...";

            client = await listener.AcceptTcpClientAsync();
            NetworkStream stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream) { AutoFlush = true };
            isConnected = true;

            btnSend.Enabled = true;
            lblStatus.Text = "Bağlantı Durumu: İstemci bağlandı!";
            lstMessages.Items.Add("[Sistem] Bir kullanıcı bağlandı.");

            MesajlariDinle();
        }
        catch (Exception ex)
        {
            if (isConnected)
            {
                return;
            }

            MessageBox.Show("Sunucu hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            lblStatus.Text = "Bağlantı Durumu: Sunucu başlatılamadı.";
            btnStartServer.Enabled = true;
            btnConnect.Enabled = true;
        }
    }

    private async void btnConnect_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtUsername.Text))
        {
            MessageBox.Show("Lütfen bir kullanıcı adı girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtUsername.Focus();
            return;
        }

        if (isConnected)
        {
            MessageBox.Show("Zaten bir bağlantı mevcut.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        btnStartServer.Enabled = false;
        btnConnect.Enabled = false;
        lblStatus.Text = "Bağlantı Durumu: Sunucuya bağlanılıyor...";

        try
        {
            client = new TcpClient();
            await client.ConnectAsync("127.0.0.1", Port);
            NetworkStream stream = client.GetStream();
            reader = new StreamReader(stream);
            writer = new StreamWriter(stream) { AutoFlush = true };
            isConnected = true;

            btnSend.Enabled = true;
            lblStatus.Text = "Bağlantı Durumu: Sunucuya bağlandı!";
            lstMessages.Items.Add("[Sistem] Sunucuya bağlanıldı.");

            MesajlariDinle();
        }
        catch (SocketException)
        {
            MessageBox.Show("Sunucu bulunamadı. Lütfen önce diğer uygulamada sunucuyu başlatın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            lblStatus.Text = "Bağlantı Durumu: Sunucuya bağlanılamadı.";
            btnStartServer.Enabled = true;
            btnConnect.Enabled = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Bağlantı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }

        isConnected = false;
        btnSend.Enabled = false;
        lblStatus.Text = "Bağlantı Durumu: Bağlantı kesildi.";
        lstMessages.Items.Add("[Sistem] Bağlantı kesildi.");

        btnStartServer.Enabled = true;
        btnConnect.Enabled = true;

        try
        {
            reader?.Close();
            writer?.Close();
            client?.Close();
            listener?.Stop();
            listener = null;
        }
        catch
        {
        }
    }

    private async void btnSend_Click(object? sender, EventArgs e)
    {
        if (!isConnected || writer == null)
        {
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
            MessageBox.Show("Mesaj gönderilemedi: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
