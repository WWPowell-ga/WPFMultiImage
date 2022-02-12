using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFMultiImage
{
    public static class PixelFormatDef
    {
        public static string Defintion(string pf)
        {
            string ret;

            switch (pf)
            {
                case "BGR32":
                    ret = "Bgr32 is a sRGB format with 32 bits per pixel (BPP). Each color channel (blue, green, and red) is allocated 8 bits per pixel (BPP).";
                    break;

                case "BGRA32":
                    ret = "Bgra32 is a sRGB format with 32 bits per pixel(BPP). Each channel (blue, green, red, and alpha) is allocated 8 bits per pixel(BPP).";
                    break;

                case "INDEXED8":
                    ret = "A paletted bitmap with 256 colors.";
                    break;

                default:
                    ret = "";
                    break;
            }//switch

            return ret;

        }//Defintion

    }//class
}//namespace
