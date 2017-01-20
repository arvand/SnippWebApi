using System.Drawing;

namespace SnippingTool.Helper
{
    public class ImageHelper
    {
        public static void PutWatermark(Image image)
        {
            using (Image watermarkImage = Image.FromFile(@"watermark.png"))
            using (Graphics imageGraphics = Graphics.FromImage(image))
            using (TextureBrush watermarkBrush = new TextureBrush(watermarkImage))
            {
                int x = (image.Width   - watermarkImage.Width );
                int y = (image.Height  - watermarkImage.Height );
                watermarkBrush.TranslateTransform(x, y);
                imageGraphics.FillRectangle(watermarkBrush, new Rectangle(new Point(x, y), 
                                            new Size(watermarkImage.Width + 1, watermarkImage.Height)));
            }
        }
    }
}
