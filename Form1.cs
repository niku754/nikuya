using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.Sockets;

namespace NoxYan
{
    public partial class NoxYan : Form
    {
        public NoxYan()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void BlockIP(string ip)
        {
            string ruleName = $"NoxYan - {ip}";

            Process.Start(new ProcessStartInfo
            {
                FileName = "netsh",
                Arguments =
                    $"advfirewall firewall add rule " +
                    $"name=\"{ruleName}\" " +
                    $"dir=out action=block remoteip={ip}",
                UseShellExecute = true,
                Verb = "runas",
                CreateNoWindow = true
            });
        }

        private void UnblockIP(string ip)
        {
            string ruleName = $"NoxYan - {ip}";

            Process.Start(new ProcessStartInfo
            {
                FileName = "netsh",
                Arguments =
                    $"advfirewall firewall delete rule " +
                    $"name=\"{ruleName}\"",
                UseShellExecute = true,
                Verb = "runas",
                CreateNoWindow = true
            });
        }

        private async Task<string[]> GetIPs(string domain)
        {
            var addresses = await Dns.GetHostAddressesAsync(domain);

            return addresses
                .Where(ip => ip.AddressFamily == AddressFamily.InterNetwork)
                .Select(ip => ip.ToString())
                .Distinct()
                .ToArray();
        }

        private async Task UnblockDomain(string domain)
        {
            string[] ips = await GetIPs(domain);

            foreach (string ip in ips)
            {
                UnblockIP(ip);
            }
        }

        private async Task BlockDomain(string domain)
        {
            var addresses = await Dns.GetHostAddressesAsync(domain);

            foreach (var address in addresses)
            {
                BlockIP(address.ToString());
            }
        }

        bool isChecked;

        private async void button1_Click(object sender, EventArgs e)
        {
            Microsoft.Win32.Registry.CurrentUser.CreateSubKey(
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer")
                .SetValue("DisallowRun", "1");
            Microsoft.Win32.Registry.CurrentUser.CreateSubKey(
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun")
                .SetValue("1", "Yandex.exe");
            Microsoft.Win32.Registry.CurrentUser.CreateSubKey(
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisallowRun")
                .SetValue("2", "browser.exe");
            if (isChecked)
            {
                BlockDomain("ya.ru");
                MessageBox.Show("IP-адреса яндекса заблокированны!", "NikuYa");
            }
            MessageBox.Show("Яндекс заблокирован! \nПерезагрузите ПК для применения настроек!", "NikuYa");
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked) {
                isChecked = true;
                button1.Text = "Заблокировать и IP";
            } else
            {
                isChecked = false;
                button1.Text = "Заблокировать";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.tbank.ru/rm/r_VEXNOLzoZn.nTcxlYxeZZ/ElkDD71717",
                UseShellExecute = true
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/niku754",
                UseShellExecute = true
            });
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UnblockDomain("ya.ru");
            MessageBox.Show("IP-адреса яндекса разблокированны!", "NikuYa");

            Microsoft.Win32.Registry.CurrentUser.CreateSubKey(
                @"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer")
                .SetValue("DisallowRun", "0");
            MessageBox.Show("Яндекс разблокирован! \nПерезагрузите ПК для применения настроек!", "NikuYa");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
