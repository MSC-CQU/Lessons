using System.Linq;
using System.Windows.Media.Imaging;
using 人的类 = Microsoft.ProjectOxford.Face.Contract.Person;
using 人脸识别服务类 = Microsoft.ProjectOxford.Face.FaceServiceClient;
using 位图图片 = System.Windows.Media.Imaging.BitmapImage;
using 信息框类 = System.Windows.MessageBox;
using 基类 = System.Object;
using 字符串类 = System.String;
using 异常类 = System.Exception;
using 打开对话框类 = Microsoft.Win32.OpenFileDialog;
using 文件 = System.IO.File;
using 窗口基类 = System.Windows.Window;
using 系统环境类 = System.Environment;
using 系统统一资源标识符类 = System.Uri;
using 路由事件信息类 = System.Windows.RoutedEventArgs;
using 逻辑值类 = System.Boolean;






namespace 人脸识别和验证
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class 主窗口 : 窗口基类
    {
        protected const 字符串类 我的密钥 = "46271916f77d4e75917d19ca88539354";
        人脸识别服务类 人脸识别服务 = new 人脸识别服务类(我的密钥);

        public 主窗口()
        {
            InitializeComponent();
        }

        private async void 列出所有人信息按钮被点击(基类 发送者, 路由事件信息类 事件)
        {
            人的类[] 人们 = await 人脸识别服务.GetPersonsAsync("msc2016");
            
            foreach(人的类 人 in 人们)
            {
                可编辑文本框.Text += 人.Name + "  " + 人.UserData + "  " + 人.PersonId + 系统环境类.NewLine;
            }
        }

        private async void 添加按钮被点击(基类 发送者, 路由事件信息类 事件)
        {
            var 打开对话框 = new 打开对话框类();
            打开对话框.DefaultExt = ".jpg";
            打开对话框.Filter = "Image files(*.jpg, *.png, *.bmp, *.gif) | *.jpg; *.png; *.bmp; *.gif";
            逻辑值类? 打开对话框的结果 = 打开对话框.ShowDialog(this);
            if (!(逻辑值类)打开对话框的结果)
            {
                return;
            }
            字符串类 文件路径 = 打开对话框.FileName;
            var 文件流 = 文件.OpenRead(打开对话框.FileName);
            系统统一资源标识符类 文件统一资源标识符 = new 系统统一资源标识符类(文件路径);
            位图图片 位图图片源 = new 位图图片();
            位图图片源.BeginInit();
            位图图片源.CacheOption = BitmapCacheOption.None;
            位图图片源.UriSource = 文件统一资源标识符;
            位图图片源.EndInit();
            图片框.Source = 位图图片源;
            try
            {
                /*
                AddPersistedFaceResult x = await faceService.AddPersonFaceAsync("msc2016", Guid.Parse("7785adeb-1762-4d38-9675-3cc7b4cd3b1b"), fileStream);
                textBox.Text = x.PersistedFaceId.ToString();
                await faceService.TrainPersonGroupAsync("msc2016");
                MessageBox.Show("Finish!");
                */
                //为Group中的某个人添加一张脸
                var 人脸们 = await 人脸识别服务.DetectAsync(文件流);
                var 人脸的全局统一标识符们 = 人脸们.Select(ff => ff.FaceId);
                foreach (var 一个人脸全局统一标识符 in 人脸的全局统一标识符们)
                {
                    可编辑文本框.Text += 一个人脸全局统一标识符.ToString() + 系统环境类.NewLine;
                }

                var 人脸唯一编码 = 人脸们.Select(ff => ff.FaceId);

                var 全局统一标识符缓存 = 人脸唯一编码.ToArray();
                可编辑文本框一.Text += 全局统一标识符缓存[0].ToString() + 系统环境类.NewLine;
                var 验证结果们 = await 人脸识别服务.IdentifyAsync(人脸组名, 全局统一标识符缓存);
                信息框类.Show("人脸识别结束");
                //foreach(var i in identifyResult)
                //{
                //textBox.Text += i.ToString() + "\n";
                var 验证结果 = 验证结果们.FirstOrDefault();
                if (验证结果.Candidates.Length != 0)
                {
                    var 一个全局统一标识符 = 验证结果.Candidates.FirstOrDefault().PersonId;
                    if (一个类.personIdToPersonName.ContainsKey(一个全局统一标识符) == true)
                    {
                        可编辑文本框.Text += 一个类.map[一个类.personIdToPersonName[一个全局统一标识符]] + 系统环境类.NewLine;
                    }
                    信息框类.Show("人脸验证结束");
                    //var x = await faceService.AddPersonFaceAsync("msc2016", y, fileStream2);
                    //textBox.Text += x.PersistedFaceId.ToString() + Environment.NewLine;
                    //await faceService.TrainPersonGroupAsync("msc2016");
                    //MessageBox.Show("Finish!");
                }
                //}
            }
            catch (异常类 异常)
            {
                信息框类.Show(异常.ToString());
            }
        }
        
        private const 字符串类 人脸组名 = "msc2016";
    }
}
