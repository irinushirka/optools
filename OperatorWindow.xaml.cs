using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Drawing.Imaging;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;

namespace IrynaSkurkoOptoolProgram
{
    /// <summary>
    /// Логика взаимодействия для OperatorWindow.xaml
    /// </summary>
    public partial class OperatorWindow : Window
    {
        ApplicationContext db;
        bool achiveOn = false;

        public OperatorWindow()
        {
            InitializeComponent();
            db = new ApplicationContext();

            User currUser = UserSession.GetUser();
            Operator currOperator = db.Operator.Where(b => b.account_id == currUser.user_id).FirstOrDefault();
            operatorIdField.Text = currOperator.operator_id.ToString();
            nameField.Text = currOperator.first_name;
            secondNameField.Text = currOperator.second_name;
            lastNameField.Text = currOperator.last_name;

            List<Document> docs = db.Document.ToList<Document>();
            docGrid.ItemsSource = docs;
        }

        private void personalPageButton_Click(object sender, RoutedEventArgs e)
        {
            personalPage.Visibility = System.Windows.Visibility.Visible;
            uploadDocPage.Visibility = System.Windows.Visibility.Hidden;
            docPage.Visibility = System.Windows.Visibility.Hidden;
            statsPage.Visibility = System.Windows.Visibility.Hidden;
            checkPage.Visibility = System.Windows.Visibility.Hidden;

            User currUser = UserSession.GetUser();
            Operator currOperator = db.Operator.Where(b => b.account_id == currUser.user_id).FirstOrDefault();
            operatorIdField.Text = currOperator.operator_id.ToString();
            nameField.Text = currOperator.first_name;
            secondNameField.Text = currOperator.second_name;
            lastNameField.Text = currOperator.last_name;
        }

        private void uploadDocButton_Click(object sender, RoutedEventArgs e)
        {
            personalPage.Visibility = System.Windows.Visibility.Hidden;
            uploadDocPage.Visibility = System.Windows.Visibility.Visible;
            docPage.Visibility = System.Windows.Visibility.Hidden;
            statsPage.Visibility = System.Windows.Visibility.Hidden;
            checkPage.Visibility = System.Windows.Visibility.Hidden;
        }

        private void docButton_Click(object sender, RoutedEventArgs e)
        {
            personalPage.Visibility = System.Windows.Visibility.Hidden;
            uploadDocPage.Visibility = System.Windows.Visibility.Hidden;
            docPage.Visibility = System.Windows.Visibility.Visible;
            statsPage.Visibility = System.Windows.Visibility.Hidden;
            checkPage.Visibility = System.Windows.Visibility.Hidden;

            List<Document> docs = db.Document.ToList<Document>();
            docGrid.ItemsSource = docs;
        }

        private void statsButton_Click(object sender, RoutedEventArgs e)
        {
            personalPage.Visibility = System.Windows.Visibility.Hidden;
            uploadDocPage.Visibility = System.Windows.Visibility.Hidden;
            docPage.Visibility = System.Windows.Visibility.Hidden;
            statsPage.Visibility = System.Windows.Visibility.Visible;
            checkPage.Visibility = System.Windows.Visibility.Hidden;
        }

        private void checkDoc_Click(object sender, RoutedEventArgs e)
        {
            personalPage.Visibility = System.Windows.Visibility.Hidden;
            uploadDocPage.Visibility = System.Windows.Visibility.Hidden;
            docPage.Visibility = System.Windows.Visibility.Hidden;
            statsPage.Visibility = System.Windows.Visibility.Hidden;
            checkPage.Visibility = System.Windows.Visibility.Visible;
        }

        private void uploadButton_30_31_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                page_30_31.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void uploadButton_32_33_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                page_32_33.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        private void uploadButton_selfie_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                selfie.Source = new BitmapImage(new Uri(op.FileName));
            }
        }

        public byte[] getJPGFromImageControl(BitmapImage bitmapImage)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }

        private void saveUploadedDocButton_Click(object sender, RoutedEventArgs e)
        {
            int client_id = Int32.Parse(clientIdField.Text.ToString());
            User currentUser = UserSession.GetUser();
            Operator currOperator = db.Operator.Where(b => b.account_id == currentUser.user_id).FirstOrDefault();
            string verification_type = verifType.Text.ToString();
            string document_type = docType.Text.ToString();
            string date_uploaded = DateTime.Now.ToString();

            Document newDocument = new Document(client_id: client_id,
                                                operator_id: currOperator.operator_id,
                                                document_type_id: db.DocumentType.FirstOrDefault(dt=>dt.document_type == "паспорт").document_type_id,
                                                date_uploaded: date_uploaded,
                                                status_id: db.DocumentStatus.FirstOrDefault(ds => ds.status == "Не проверен").status_id,
                                                verification_type_id: db.VerificationType.FirstOrDefault(dt => dt.verification_type == verification_type).verification_type_id);
            _ = db.Document.Add(newDocument);
            _ = db.SaveChanges();

            DocumentImage newImage1 = new DocumentImage(document_id: newDocument.document_id,
                                                       image_url: getJPGFromImageControl(page_30_31.Source as BitmapImage),
                                                       image_tag: "page_30_31");
            DocumentImage newImage2 = new DocumentImage(document_id: newDocument.document_id,
                                                       image_url: getJPGFromImageControl(page_32_33.Source as BitmapImage),
                                                       image_tag: "page_32_33");
            DocumentImage newImage3 = new DocumentImage(document_id: newDocument.document_id,
                                                       image_url: getJPGFromImageControl(selfie.Source as BitmapImage),
                                                       image_tag: "selfie");
            _ = db.DocumentImage.Add(newImage1);
            _ = db.DocumentImage.Add(newImage2);
            _ = db.DocumentImage.Add(newImage3);
            _ = db.SaveChanges();

            _ = MessageBox.Show("Документ успешно добавлен.");

            List<Document> docs = db.Document.ToList<Document>();
            docGrid.ItemsSource = docs;

            uploadDocPage.Visibility = System.Windows.Visibility.Hidden;
            docPage.Visibility = System.Windows.Visibility.Visible;
        }

        private void deleteDoc_Click(object sender, RoutedEventArgs e)
        {
            string answer = MessageBox.Show("Действительно удалить этот документ?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question).ToString();
            if (answer == "Yes")
            {
                Document selected = (Document)docGrid.SelectedItem;
                db.Document.Remove(db.Document.FirstOrDefault(d => d.document_id == selected.document_id));
                db.SaveChanges();
            }
        }

        private System.Drawing.Image ImageWpfToGDI(System.Windows.Media.ImageSource image)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            var encoder = new System.Windows.Media.Imaging.BmpBitmapEncoder();
            encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(image as System.Windows.Media.Imaging.BitmapSource));
            encoder.Save(ms);
            ms.Flush();
            return System.Drawing.Image.FromStream(ms);
        }

        private void picture_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            form.Size = new System.Drawing.Size(800, 1000);
            form.Show();
            
            System.Windows.Forms.PictureBox pb = new System.Windows.Forms.PictureBox();

            Image img = (Image)sender;
            pb.Image = ImageWpfToGDI(img.Source);

            Console.WriteLine(img.Width.ToString(), img.Height.ToString());

            pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;

            form.Controls.Add(pb);
        }

        private void signOutButton_Click(object sender, RoutedEventArgs e)
        {
            UserSession.Logout();
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void read_mrz(DocumentImage img, int idx)
        {
            /*var url = "https://api.mindee.net/v1/products/mindee/passport/v1/predict";
            System.Drawing.Image img_temp = byteArrayToImage(img.image_url);
            var filePath = "C:\\Users\\irair\\source\\repos\\IrynaSkurkoOptoolProgram\\temp" + idx.ToString() + ".jpg";
            img_temp.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);

            var token = "f48d45f76e75ff52f4eeea015c96904b";

            var file = File.OpenRead(filePath);
            var streamContent = new StreamContent(file);
            var imageContent = new ByteArrayContent(streamContent.ReadAsByteArrayAsync().Result);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

            var form = new MultipartFormDataContent();
            form.Add(imageContent, "document", System.IO.Path.GetFileName(filePath));

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", "f48d45f76e75ff52f4eeea015c96904b");
            var response = httpClient.PostAsync(url, form).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);*/

            string result;
            using (StreamReader reader = new StreamReader("C:\\Users\\irair\\source\\repos\\IrynaSkurkoOptoolProgram\\write.json"))
            {
                result = reader.ReadToEnd();
            }

            Console.WriteLine(result);
            JObject json = JObject.Parse(result);
            IList<string> keys = json.Properties().Select(p => p.Name).ToList();
            Console.WriteLine("KEYS: " + keys.ToString());

            Dictionary<string, string> passport_data = new Dictionary<string, string>(6);

            passport_data["passport_number"] = json["document"]["inference"]["pages"][0]["prediction"]["id_number"]["value"].ToString();
            Console.WriteLine("P. number " + passport_data["passport_number"]);
            passNumberBox.Text = passport_data["passport_number"];

            passport_data["name"] = json["document"]["inference"]["pages"][0]["prediction"]["given_names"][0]["value"].ToString();
            Console.WriteLine("Name " + passport_data["name"]);
            nameBox.Text = passport_data["name"];

            passport_data["surname"] = json["document"]["inference"]["pages"][0]["prediction"]["surname"]["value"].ToString();
            Console.WriteLine("Surname " + passport_data["surname"]);
            surnameBox.Text = passport_data["surname"];

            passport_data["gender"] = json["document"]["inference"]["pages"][0]["prediction"]["gender"]["value"].ToString();
            Console.WriteLine("Gender " + passport_data["gender"]);
            genderBox.Text = passport_data["gender"];

            passport_data["birth_date"] = json["document"]["inference"]["pages"][0]["prediction"]["birth_date"]["value"].ToString();
            Console.WriteLine("Birth date " + passport_data["birth_date"]);
            birthDateBox.Text = passport_data["birth_date"];

            passport_data["expiry_date"] = json["document"]["inference"]["pages"][0]["prediction"]["expiry_date"]["value"].ToString();
            Console.WriteLine("Expiry date " + passport_data["expiry_date"]);
            expiryDateBox.Text = passport_data["expiry_date"];

            DateTime date1 = DateTime.ParseExact(passport_data["birth_date"], "yyyy-MM-dd", null);
            DateTime date2 = date1.AddYears(21);
            DateTime now = DateTime.Now;

            if (DateTime.Compare(date2, now) < 0)
            {
                passResultBox.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BFF7AC");
                passResultBox.Content = "Есть 21 год";
            }
            else
            {
                passResultBox.Background = Brushes.LightPink;
                passResultBox.Content = "Нет 21 года";
            }

            DateTime date3 = DateTime.ParseExact(passport_data["expiry_date"], "yyyy-MM-dd", null);
            if (DateTime.Compare(date3, now) < 0)
            {
                passResultBox.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#BFF7AC");
                passResultBox.Content = "Срок действия документа истек.";
            }
        }

        public System.Drawing.Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream(1000000))
            {
                imageIn.Save(ms, imageIn.RawFormat);
                return ms.ToArray();
            }
        }

        public BitmapImage systemDrawingImageToBitmap(System.Drawing.Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        static string BytesToStringConverted(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                using (var streamReader = new StreamReader(stream, System.Text.Encoding.UTF8))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

            Document doc = (Document)((Button)e.Source).DataContext;
            docPage.Visibility = System.Windows.Visibility.Hidden;
            checkPage.Visibility = System.Windows.Visibility.Visible;

            List<DocumentImage> images = (from doc_img in db.DocumentImage
                         where doc_img.document_id == doc.document_id
                         select doc_img).ToList();

            List<System.Windows.Controls.Image> elements_list = new List<System.Windows.Controls.Image>();
            elements_list.Add(page_30_31_check);
            elements_list.Add(page_32_33_check); 
            elements_list.Add(selfie_check);

            for (int i = 0; i < images.Count; i++) {
                using (var ms = new MemoryStream())
                {
                    byteArrayToImage(images[i].image_url).Save(ms, ImageFormat.Bmp);
                    ms.Seek(0, SeekOrigin.Begin);

                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = ms;
                    bitmapImage.EndInit();
                    elements_list[i].Source = bitmapImage;
                    read_mrz(images[i], i);
                }
            }
            docIdTextbox.Text = doc.document_id.ToString();

            using (var tcp = new System.Net.Sockets.TcpClient("192.168.100.6", 11000))
            {
                System.Drawing.Image selfie_di = ImageWpfToGDI(selfie_check.Source);
                System.Drawing.Image passport_32_33_di = ImageWpfToGDI(page_32_33_check.Source);

                BitmapImage selfie = systemDrawingImageToBitmap(selfie_di);
                BitmapImage passport_32_33 = systemDrawingImageToBitmap(passport_32_33_di);

                byte[] a1 = getJPGFromImageControl(selfie);
                byte[] a2 = getJPGFromImageControl(passport_32_33);

                Console.WriteLine("a1 size: " + a1.Length.ToString());
                Console.WriteLine("a2 size: " + a2.Length.ToString());

                tcp.GetStream().Write(BitConverter.GetBytes(a2.Length), 0, sizeof(int));
                tcp.GetStream().Write(BitConverter.GetBytes(a1.Length), 0, sizeof(int));

             //   tcp.GetStream().Write(BitConverter.GetBytes(a2.Length + a1.Length), 0, sizeof(int));
                tcp.GetStream().Write(a2.Concat(a1).ToArray(), 0, a1.Length + a2.Length);
                // tcp.GetStream().Write(, 0, a1.Length);

                tcp.GetStream().Flush();

                byte[] response = new byte[4];
                tcp.GetStream().Read(response, 0, 4);
                int response_size = BitConverter.ToInt32(response, 0);

                response = new byte[response_size];
                tcp.GetStream().Read(response, 0, response_size);

                Console.WriteLine("RESPONSE: " + BytesToStringConverted(response));
                faceMatchLabel.Content = BytesToStringConverted(response);

            }
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
        }

        private void findButton_Click(object sender, RoutedEventArgs e)
        {
            int doc_id = Convert.ToInt32(idDocToFind.Text);

            List<Document> docs = db.Document.Where(doc => doc.document_id == doc_id).ToList<Document>();
            docGrid.ItemsSource = docs;
            docGrid.UpdateLayout();
        }

        private void archiveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!achiveOn)
            {
                List<Document> docs = db.Document.Where(doc => doc.status_id == 2).ToList<Document>();
                docGrid.ItemsSource = docs;
                docGrid.UpdateLayout();
                achiveOn = true;
                archiveButton.Content = "Все документы";
            }
            else
            {
                List<Document> docs = db.Document.ToList<Document>();
                docGrid.ItemsSource = docs;
                docGrid.UpdateLayout();
                achiveOn = false;
                archiveButton.Content = "Архив";
            }
        }

        private void refreshDocsButton_Click(object sender, RoutedEventArgs e)
        {
            List<Document> docs = db.Document.ToList<Document>();
            docGrid.ItemsSource = docs;
            docGrid.UpdateLayout();
        }
    }
}
