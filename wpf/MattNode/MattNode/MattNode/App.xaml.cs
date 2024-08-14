using System.Configuration;
using System.Data;
using System.Windows;

namespace MattNode
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string OpenFilePath = null;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 프로그램 인자 확인 (파일 경로)
            if (e.Args.Length > 0)
            {
                string filePath = e.Args[0];

                // 파일을 처리할 로직 추가
                OpenFilePath = filePath;
            }
        }
    }

}
