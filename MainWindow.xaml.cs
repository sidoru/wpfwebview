using Microsoft.Web.WebView2.Core;
using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace WpfWebView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeAsync();
        }

        async void InitializeAsync()
        {
            await webView.EnsureCoreWebView2Async(null);
            webView.Source = new Uri(System.IO.Path.Combine(Environment.CurrentDirectory, "index.html"));
            webView.CoreWebView2.AddHostObjectToScript("JsBridge", new JsBridge());
            webView.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;
        }

        private void CoreWebView2_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var js =$"showMessage(\"{messageTextBox.Text}\")";
            var r = await webView.CoreWebView2.ExecuteScriptAsync(js);
            Console.WriteLine(r);
        }
    }

    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class JsBridge
    {
        public void MessageShow(string msg) => MessageBox.Show(msg, "C#が表示してるメッセージボックス");
    }
}
