using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MatGUI;
using Microsoft.Win32;
using System.Windows.Markup;
using System.IO;

namespace MatStudioROBOT2016.Views
{
    /* 
	 * ViewModelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedWeakEventListenerや
     * CollectionChangedWeakEventListenerを使うと便利です。独自イベントの場合はLivetWeakEventListenerが使用できます。
     * クローズ時などに、LivetCompositeDisposableに格納した各種イベントリスナをDisposeする事でイベントハンドラの開放が容易に行えます。
     *
     * WeakEventListenerなので明示的に開放せずともメモリリークは起こしませんが、できる限り明示的に開放するようにしましょう。
     */

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadLayouts(@"initial.matlayout");
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            MatWindow.AllWindowTopmostOn();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            MatWindow.AllWindowTopmostOff();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MatWindow.AllWindowClose();
            Application.Current.Shutdown();
        }


        private void SaveLayoutAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                FileName = ".matlayout",
                Filter = "MatGUI LayoutFile|*.matlayout|すべてのファイル(*.*)|*.*",
                Title = "ワークスペースのレイアウトを名前をつけて保存",
            };

            bool? result = false;
            result = dialog.ShowDialog();

            if ((bool)result)
            {
                SaveLayouts(dialog.FileName);
            }
        }

        private void LoadLayout_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "MatGUI LayoutFile|*.matlayout|すべてのファイル(*.*)|*.*",
                Title = "ワークスペースのレイアウトを読み込み",
            };

            bool? result = false;
            result = dialog.ShowDialog();

            if ((bool)result)
            {
                LoadLayouts(dialog.FileName);
            }
        }

        private void SaveLayoutToInitial(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("初期設定を上書きします", "確認", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
                SaveLayouts(@"initial.matlayout");
        }




        private static void SaveLayouts(string @FileName)
        {
            string layoutData = "MatGUI LayoutFile\n";
            foreach (MatWorkspace ws in MatWorkspace.AllMatWorkspace)
            {
                if (ws.IsMainWindowContent)
                {
                    layoutData += "// MainWindow" + "\n";
                    layoutData += "{" + "\n";
                    layoutData += "// MatWorkspace" + "\n";
                    layoutData += XamlWriter.Save(ws) + "\n";
                    layoutData += "}" + "\n";
                }
                else
                {
                    MatWindow w = ws.Parent as MatWindow;
                    if (w != null)
                    {
                        layoutData += "// Window" + "\n";
                        layoutData += "{" + "\n";
                        layoutData += "// Top" + "\n";
                        layoutData += w.Top.ToString() + "\n";
                        layoutData += "// Left" + "\n";
                        layoutData += w.Left.ToString() + "\n";
                        layoutData += "// Height" + "\n";
                        layoutData += w.ActualHeight.ToString() + "\n";
                        layoutData += "// Width" + "\n";
                        layoutData += w.ActualWidth.ToString() + "\n";
                        layoutData += "// MatWorkspace" + "\n";
                        layoutData += XamlWriter.Save(ws) + "\n";
                        layoutData += "}" + "\n";
                    }
                }
            }

            File.WriteAllText(@FileName, layoutData);
        }

        private static void LoadLayouts(string @FileName)
        {
            MatWindow.AllWindowClose();
            PhantasmagoriaTabItem.AllPhantasmagoriaTabItem.Clear();

            using (StreamReader sr = new StreamReader(FileName))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    switch (line)
                    {
                        case "// MainWindow":
                            LoadMainWindow(sr);
                            break;

                        case "// Window":
                            LoadWindow(sr);
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// .matlayoutファイルの一部からメインウインドウを読み込み、復元します。
        /// </summary>
        /// <param name="sr"></param>
        private static void LoadMainWindow(StreamReader sr)
        {
            string data = sr.ReadLine();

            if (data != "{")
                return;

            do
            {
                data = sr.ReadLine();
                switch (data)
                {
                    case "// MatWorkspace":
                        data = sr.ReadLine();
                        MatWorkspace temp = XamlReader.Parse(data) as MatWorkspace;
                        MatWorkspace.SetToMainwindowContent(temp);
                        MatWorkspace.AllMatWorkspace.Remove(temp);
                        break;

                    default:
                        break;
                }
            }
            while (data != "}");

            return;
        }

        /// <summary>
        /// .matlayoutファイルの一部からウインドウを一つ読み込み、復元します。
        /// </summary>
        /// <param name="sr"></param>
        private static void LoadWindow(StreamReader sr)
        {
            string data = sr.ReadLine();

            if (data != "{")
                return;

            MatWindow mw = new MatWindow();

            do
            {
                data = sr.ReadLine();
                switch (data)
                {
                    case "// Top":
                        mw.Top = double.Parse(sr.ReadLine());
                        break;

                    case "// Left":
                        mw.Left = double.Parse(sr.ReadLine());
                        break;

                    case "// Height":
                        mw.Height = double.Parse(sr.ReadLine());
                        break;

                    case "// Width":
                        mw.Width = double.Parse(sr.ReadLine());
                        break;

                    case "// MatWorkspace":
                        data = sr.ReadLine();
                        MatWorkspace temp = XamlReader.Parse(data) as MatWorkspace;
                        mw.Content = temp;
                        break;

                    default:
                        break;
                }
            }
            while (data != "}");

            mw.Show();

            return;
        }
    }
}
