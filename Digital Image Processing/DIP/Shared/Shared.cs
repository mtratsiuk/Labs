﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace DIP {
    public static class Shared {

        public static Bitmap Grayscale(this Bitmap image) {
            var res = new Bitmap(image.Width, image.Height);
            for (var i = 0; i < image.Width; i++) {
                for (var j = 0; j < image.Height; j++) {
                    var oc = image.GetPixel(i, j);
                    var grayScale = (int)((oc.R * 0.2126) + (oc.G * 0.7152) + (oc.B * 0.0722));
                    var nc = Color.FromArgb(oc.A, grayScale, grayScale, grayScale);
                    res.SetPixel(i, j, nc);
                }
            }
            return res;
        }

        public static byte[,] GetGrayscaleBytes(this Bitmap image) {
            var res = new byte[image.Width, image.Height];
            for (var i = 0; i < image.Width; i++) {
                for (var j = 0; j < image.Height; j++) {
                    var oc = image.GetPixel(i, j);
                    var grayScale = (byte)((oc.R * 0.2126) + (oc.G * 0.7152) + (oc.B * 0.0722));
                    res[i, j] = grayScale;
                }
            }
            return res;
        }

        public static IEnumerable<Color> GetPixels(this Bitmap bitmap) {
            for (int i = 0; i < bitmap.Height; i++) {
                for (int j = 0; j < bitmap.Width; j++) {
                    yield return bitmap.GetPixel(j, i);
                }
            }
        }

        public static T[] Get1dArray<T>(this T[,] bitmap) {
            var res = new T[bitmap.Length];
            Buffer.BlockCopy(bitmap, 0, res, 0, bitmap.Length);
            return res;
        }

        public static IEnumerable<int[]> GetSiblingCoordinates(int i, int j) {
            yield return new int[] { i - 1, j - 1 };
            yield return new int[] { i, j - 1 };
            yield return new int[] { i + 1, j - 1 };
            yield return new int[] { i - 1, j };
            yield return new int[] { i, j };
            yield return new int[] { i + 1, j };
            yield return new int[] { i + 1, j - 1 };
            yield return new int[] { i + 1, j };
            yield return new int[] { i + 1, j + 1 };
        }

    }
}
