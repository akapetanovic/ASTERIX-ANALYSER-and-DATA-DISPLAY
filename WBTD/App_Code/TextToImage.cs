using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;

/// <summary>
/// Summary description for TextToImage
/// </summary>
public class TextToImage : GooglePoint
{
	public TextToImage()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void GenerateAndStore(string txtText)
    {
        string text = txtText.Trim();
        Bitmap bitmap = new Bitmap(1, 1);
        Font font = new Font("Arial", 11, FontStyle.Regular, GraphicsUnit.Pixel);
        Graphics graphics = Graphics.FromImage(bitmap);
        int width = (int)graphics.MeasureString(text, font).Width;
        int height = (int)graphics.MeasureString(text, font).Height;
        bitmap = new Bitmap(bitmap, new Size(width, height));
        graphics = Graphics.FromImage(bitmap);
        graphics.Clear(Color.Black);
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        graphics.DrawString(text, font, new SolidBrush(Color.DarkCyan), 0, 0);
        graphics.Flush();
        graphics.Dispose();
        string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".png";
        bitmap.Save(HttpContext.Current.Server.MapPath("~/icons/") + "label.png", ImageFormat.Png);
    }
}