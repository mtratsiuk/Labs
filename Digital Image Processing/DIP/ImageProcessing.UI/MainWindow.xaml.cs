﻿using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using DIP.Linq;
using Microsoft.Win32;

namespace ImageProcessing.UI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        Bitmap imgOrig;
        Bitmap imgResult;

        private void scaleButton_Click(object sender, RoutedEventArgs e) {
            ProcessImage(ImageActions.Scale);
        }

        private void equalizeButton_Click(object sender, RoutedEventArgs e) {
            ProcessImage(ImageActions.Equalize);
        }

        private void grayscaleButton_Click(object sender, RoutedEventArgs e) {
            ProcessImage(ImageActions.Grayscale);
        }

        private void normalizeButton_Click(object sender, RoutedEventArgs e) {
            ProcessImage(ImageActions.Normalize);
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            var dial = new OpenFileDialog();
            if ((bool)dial.ShowDialog()) {
                imgOrig = new Bitmap(dial.FileName);
                SetImageSource(imageOriginal, imgOrig);
                imgResult = null;
            }
        }

        private async void ProcessImage(ImageActions action) {
            try {
                if (imgResult != null) {
                    imgOrig = imgResult;
                    SetImageSource(imageOriginal, imgOrig);
                }
                ToggleButtons(false);
                var coef = Convert.ToDouble(coefTextBox.Text);
                await Task.Run(() => {
                    
                    switch (action) {
                        case ImageActions.Scale:
                            imgResult = imgOrig.Scale(coef);
                            break;
                        case ImageActions.Equalize:
                            imgResult = imgOrig.Equalize();
                            break;
                        case ImageActions.Grayscale:
                            imgResult = imgOrig.Grayscale();
                            break;
                        case ImageActions.Normalize:
                            imgResult = imgOrig.Normalize();
                            break;
                    }
                    
                    Dispatcher.Invoke(() => {
                        SetImageSource(imageResult, imgResult);
                        ToggleButtons(true);
                    });
                    imgResult.Save("e:\\SAVED.bmp");
                });
            } catch (Exception ex) {
                MessageBox.Show($"Error: {ex.Message}");
                ToggleButtons(true);
            }
        }

        private void SetImageSource(System.Windows.Controls.Image image, Bitmap source) {
            image.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                            source.GetHbitmap(),
                            IntPtr.Zero,
                            Int32Rect.Empty,
                            BitmapSizeOptions.FromWidthAndHeight(source.Width, source.Height)
                        );
        }

        private void ToggleButtons(bool value) {
            grayscaleButton.IsEnabled = value;
            equalizeButton.IsEnabled = value;
            normalizeButton.IsEnabled = value;
            scaleButton.IsEnabled = value;
            button.IsEnabled = value;
        }

        private enum ImageActions {
            Scale,
            Grayscale,
            Equalize,
            Normalize
        }
        
    }
}
