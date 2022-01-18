using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows.Media.Imaging;

namespace WPFMultiImage
{
    public static class SaveBitmaps
    {
        public static void BMPSave(string fn,ImagesClass i)
        {
            BmpBitmapEncoder enc = new BmpBitmapEncoder();
           
            enc.Frames.Add(BitmapFrame.Create(i.BitmapImage));

            using (var filestream = new FileStream(fn, FileMode.Create))
                enc.Save(filestream);
        }//BMP

        public static void GIFSave(string fn, ImagesClass i)
        {
            GifBitmapEncoder enc = new GifBitmapEncoder();

            enc.Frames.Add(BitmapFrame.Create(i.BitmapImage));

            using (var filestream = new FileStream(fn, FileMode.Create))
                enc.Save(filestream);
        }//GIF

        public static void JPGSave(string fn, ImagesClass i)
        {
            JpegBitmapEncoder enc = new JpegBitmapEncoder();

            enc.Frames.Add(BitmapFrame.Create(i.BitmapImage));

            using (var filestream = new FileStream(fn, FileMode.Create))
                enc.Save(filestream);
        }//JPG

        public static void PNGSave(string fn, ImagesClass i)
        {
            PngBitmapEncoder enc = new PngBitmapEncoder();

            enc.Frames.Add(BitmapFrame.Create(i.BitmapImage));

            using (var filestream = new FileStream(fn, FileMode.Create))
                enc.Save(filestream);
        }//PNG

        public static void TIFSave(string fn, ImagesClass i)
        {
            TiffBitmapEncoder enc = new TiffBitmapEncoder();

            enc.Frames.Add(BitmapFrame.Create(i.BitmapImage));

            using (var filestream = new FileStream(fn, FileMode.Create))
                enc.Save(filestream);
        }//TIF


    }//class
}//namespace
