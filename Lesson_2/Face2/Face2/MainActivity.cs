using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Views;
using Android.Widget;
using Java.IO;
using Microsoft.ProjectOxford.Face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Face2
{
    [Activity(Label = "Face", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private ImageView _imageView;
        private bool isFirst = true;
        private const string myKey = "46271916f77d4e75917d19ca88539354";
        private const string groupName = "msc2016";
        private FaceServiceClient faceServiceClient = new FaceServiceClient(myKey);
        private int[] count = new int[30];//每个人的脸出现的次数

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.Main);

            Button cameraButton, uploadButton, checkButton;
            CameraHelper.CreateDirectoryForPictures();
            cameraButton = FindViewById<Button>(Resource.Id.camera);
            _imageView = FindViewById<ImageView>(Resource.Id.imageView1);
            cameraButton.Click += TakePicture;
            uploadButton = FindViewById<Button>(Resource.Id.upload);
            uploadButton.Click += UploadFace;
            checkButton = FindViewById<Button>(Resource.Id.check);
            checkButton.Click += ShowPerson;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            //判断可用
            var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            var contentUri = Android.Net.Uri.FromFile(CameraHelper._file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            var height = Resources.DisplayMetrics.HeightPixels;
            var width = _imageView.Height;
            CameraHelper.bitmap = CameraHelper.LoadAndResizeBitmap(CameraHelper._file.Path, width, height);
            if (CameraHelper.bitmap != null)
            {
                _imageView.SetImageBitmap(CameraHelper.bitmap);
                CameraHelper.bitmap = null;
            }
            isFirst = false;
            //释放资源
            GC.Collect();
        }

        private void ShowPerson(object sender, EventArgs e)
        {
            TextView textView = FindViewById<TextView>(Resource.Id.textView1);
            textView.Text = string.Empty;
            for (int i = 0; i < 30; i++)
            {
                if (count[i] != 0) continue;
                if (AClass.intToPersonName.ContainsKey(i))
                {
                    textView.Text += AClass.personNameToChineseName[AClass.intToPersonName[i]] + "  ";
                }
            }
        }

        private void TakePicture(object sender, EventArgs eventArgs)
        {
            var intent = new Intent(MediaStore.ActionImageCapture);
            CameraHelper._file = new File(CameraHelper._dir, string.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(CameraHelper._file));
            StartActivityForResult(intent, 0);
        }

        private async void UploadFace(object sender, EventArgs eventArgs)
        {
            if (isFirst == true)
            {
                return;
            }
            string filePath = CameraHelper._file.Path;
            Uri fileUri = new Uri(filePath);

            TextView textView = FindViewById<TextView>(Resource.Id.textView1);
            textView.Text = "Loading";
            try
            {
                var facesIDs = await UploadAndDetectFaces(filePath);
                textView.Text = facesIDs.FirstOrDefault().ToString();
                var personIDs = await UploadAndIdentifyFaces(facesIDs);

                textView.Text = string.Empty;
                foreach (var personID in personIDs)
                {
                    string title = AClass.personNameToChineseName[AClass.personIdToPersonName[personID]];
                    count[AClass.personNameToInt[AClass.personIdToPersonName[personID]]]++;
                    textView.Text += title + "  ";
                }
            }
            catch (Exception ex)
            {
                textView.Text = ex.Message;
            }
        }

        private async Task<Guid[]> UploadAndDetectFaces(string imageFilePath)
        {
            try
            {
                using (System.IO.Stream imageFileStream = System.IO.File.OpenRead(imageFilePath))
                {
                    var faces = await faceServiceClient.DetectAsync(imageFileStream);
                    return faces.Select(ff => ff.FaceId).ToArray();
                }
            }
            catch (Exception ex)
            {
                return new Guid[0];
            }
        }

        private async Task<Guid[]> UploadAndIdentifyFaces(Guid[] facesIDs)
        {
            try
            {
                Toast.MakeText(this, facesIDs[0].ToString(), ToastLength.Long).Show();
                var identifyResult = await faceServiceClient.IdentifyAsync(groupName, facesIDs);
                List<Guid> personIDs = new List<Guid>();
                foreach (var i in identifyResult)
                {
                    if (i.Candidates.Length == 0) continue;
                    personIDs.Add(i.Candidates.FirstOrDefault().PersonId);
                }
                return personIDs.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}