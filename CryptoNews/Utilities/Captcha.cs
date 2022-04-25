using Microsoft.AspNetCore.Http;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Runtime.Versioning;

namespace CryptoNews.Utilities
{
    [SupportedOSPlatform("windows")]
    public static class Captcha
    {
        /// <summary>
        /// Upper case. Without (I,1) (O,0) (S,5)
        /// </summary>
        const string CHARS = "ABCDEFGHJKLMNPRTUVWXYZ2346789";

        public static string CreateCaptchaCode()
        {
            Random rnd = new();
            int maxRnd = CHARS.Length - 1;

            StringBuilder sb = new();

            for(byte i = 0; i < 4; i++)
            {
                int index = rnd.Next(maxRnd);
                sb.Append(CHARS[index]);
            }

            return sb.ToString();
        }

        public static bool ValidateCaptchaCode(string userInput, HttpContext context)
        {
            var captcha = context.Session.GetString("Captcha");
            bool isValid = userInput.ToUpper() == captcha;
            context.Session.Remove("Captcha");
            return isValid;
        }

        public static CaptchaResult CreateCaptchaImage(int width, int height, string code)
        {
            using Bitmap bitmap = new(width, height);
            using (Graphics graph = Graphics.FromImage(bitmap))
            {
                Random rnd = new();
                graph.Clear(GetLightColor());

                DrawCaptchaCode();
                DrawHinderingLine();
                AdjustRippleEffect();

                MemoryStream ms = new();
                bitmap.Save(ms, ImageFormat.Png);

                return new CaptchaResult
                {
                    CaptchaBytes = ms.ToArray(),
                    CaptchaCode = code,
                    TimeStamp = DateTime.Now
                };

                #region inside methods
                Color GetLightColor()
                {
                    int low = 150, high = 255;

                    int nRed = low + rnd.Next(high) % (high - low);
                    int nGreen = low + rnd.Next(high) % (high - low);
                    int nBlue = low + rnd.Next(high) % (high - low);

                    return Color.FromArgb(nRed, nGreen, nBlue);
                }

                Color GetDarkColor()
                {
                    //low = 0
                    int high = 100;
                    return Color.FromArgb(rnd.Next(high),
                                          rnd.Next(high),
                                          rnd.Next(high));
                }

                int GetFontSize(int imageWidth, int codeCount)
                {
                    var averageSize = imageWidth / codeCount;

                    return averageSize;
                }

                void DrawCaptchaCode()
                {
                    SolidBrush brush = new(Color.Black);
                    int fontSize = GetFontSize(width, code.Length);
                    Font font = new(FontFamily.GenericSansSerif, fontSize, FontStyle.Bold, GraphicsUnit.Pixel);

                    for (byte i = 0; i < code.Length; i++)
                    {
                        brush.Color = GetDarkColor();

                        int shiftPx = fontSize / 6;

                        float x = i * fontSize + rnd.Next(-shiftPx, shiftPx) + rnd.Next(-shiftPx, shiftPx);
                        int maxY = height - fontSize;
                        if (maxY < 0)
                            maxY = 0;
                        var y = rnd.Next(0, maxY);

                        graph.DrawString(code[i].ToString(), font, brush, x, y);
                    }
                }

                void DrawHinderingLine()
                {
                    SolidBrush brush = new(Color.Black);
                    Pen pen = new(brush, 1.5f);
                    int count = rnd.Next(2, 4);

                    for (byte i = 0; i < count; i++)
                    {
                        pen.Color = GetDarkColor();

                        Point begin = new(rnd.Next(width), rnd.Next(height));
                        Point end = new(rnd.Next(width), rnd.Next(height));

                        graph.DrawLine(pen, begin, end);

                        if (i == count-1) // +1 Bezier line
                        {
                            Point bezierPoint1 = new(rnd.Next(width), rnd.Next(height));
                            Point bezierPoint2 = new(rnd.Next(width), rnd.Next(height));
                            pen.Width = 1;
                            graph.DrawBezier(pen, begin, bezierPoint1, bezierPoint2, end);
                        }
                    }
                }

                void AdjustRippleEffect()
                {
                    byte nWave = 5;
                    int nWidth = bitmap.Width;
                    int nHeight = bitmap.Height;

                    Point[,] pt = new Point[nWidth, nHeight];

                    for (byte x = 0; x < nWidth; x++)
                    {
                        for (byte y = 0; y < nHeight; y++)
                        {
                            var xDelta = nWave * Math.Sin(2.0 * 3.14159 * y / 128.0);
                            var yDelta = nWave * Math.Cos(2.0 * 3.14159 * x / 128.0);

                            var newX = x + xDelta;
                            var newY = y + yDelta;

                            if (0 < newX && newX < nWidth)
                                pt[x, y].X = (int)newX;
                            else
                                pt[x, y].X = 0;

                            if (0 < newY && newY < nHeight)
                                pt[x, y].Y = (int)newY;
                            else
                                pt[x, y].Y = 0;
                        }
                    }

                    Bitmap bSrc = (Bitmap)bitmap.Clone();
                    BitmapData bitmapData = bitmap.LockBits(
                        new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);
                    BitmapData bmSrcData = bSrc.LockBits(
                        new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppRgb);

                    int scanLine = bitmapData.Stride;

                    IntPtr scan0 = bitmapData.Scan0;
                    IntPtr srcScan0 = bmSrcData.Scan0;

                    unsafe
                    {
                        byte* p = (byte*)(void*)scan0;
                        byte* pSrc = (byte*)(void*)srcScan0;

                        int nOffset = bitmapData.Stride - bitmap.Width * 3;

                        for (int y = 0; y < nHeight; ++y)
                        {
                            for (int x = 0; x < nWidth; ++x)
                            {
                                var xOffset = pt[x, y].X;
                                var yOffset = pt[x, y].Y;

                                if (yOffset >= 0 && yOffset < nHeight && xOffset >= 0 && xOffset < nWidth)
                                {
                                    if (pSrc != null)
                                    {
                                        p[0] = pSrc[yOffset * scanLine + xOffset * 3];
                                        p[1] = pSrc[yOffset * scanLine + xOffset * 3 + 1];
                                        p[2] = pSrc[yOffset * scanLine + xOffset * 3 + 2];
                                    }
                                }

                                p += 3;
                            }
                            p += nOffset;
                        }
                    }

                    bitmap.UnlockBits(bitmapData);
                    bSrc.UnlockBits(bmSrcData);
                    bSrc.Dispose();
                }
                #endregion
            }
        }
    }
}
