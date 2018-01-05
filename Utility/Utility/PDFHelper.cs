using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using O2S.Components.PDFRender4NET;

namespace TStar.Utility
{
    public class PDFHelper2
    {
        public static string ConvertPDF2Image(string pdfPath, int width, int height, bool isThumb)
        {
            PDFFile pdfFile = null;
            Image pageImage = null;

            try
            {
                int idx = pdfPath.LastIndexOf('\\');
                string outputPath = pdfPath.Substring(0, idx);
                string imageName = pdfPath.Substring(idx, (pdfPath.Length - idx) - 4);

                pdfFile = PDFFile.Open(pdfPath);
                pageImage = pdfFile.GetPageImage(0, 56 * 10);
                int max = 0, maxW = 0, maxH = 0;
                int _width = pageImage.Width;
                int _height = pageImage.Height;
                double scale = _width * 1.0 / _height;

                // 根据高宽比修正尺寸
                if (width * height == 0 && width != height) // 指定一个尺寸，另一个按比例
                {
                    maxW = width > 0 ? width : (int)(height * scale);
                    maxH = width > 0 ? (int)(width / scale) : height;
                }
                else
                {
                    if (width == 0 && height == 0) // 未指定尺寸，指定默认尺寸，并按比例
                    {
                        width = isThumb ? 200 : 800; // 默认最大缩略图200，普通800
                        height = isThumb ? 150 : 600; // 默认最大缩略图200，普通800
                        if (scale < 1) { int t = width; width = height; height = t; }
                    }
                    double _scale = width * 1.0 / height;
                    maxW = _scale > scale ? (int)(scale * height) : width;
                    maxH = _scale > scale ? height : (int)(width / scale);
                }

                string imagePath = outputPath + imageName + (isThumb ? "_thumb" : "") + ".jpg";
                pageImage = pageImage.GetThumbnailImage(maxW, maxH, null, new System.IntPtr());
                pageImage.Save(imagePath, ImageFormat.Jpeg);
                return imagePath;
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                if (pageImage != null) pageImage.Dispose();
                if (pdfFile != null) pdfFile.Dispose();
            }
        }
    }
}
