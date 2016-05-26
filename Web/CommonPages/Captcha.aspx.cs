using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;

namespace Web.CommonPages
{
    public partial class Captcha : System.Web.UI.Page
    {
        public static string CaptchaSessionKey = "Captcha";

        protected void Page_Init(object sender, EventArgs e)
        {
            int width = 55;
            int height = 24;
            int noiseLineNum = 3;
            int noisePointNum = 15;
            int wordLen = 4;
            Random r = new Random();            

            // (封裝 GDI+ 點陣圖) 新增一個 Bitmap 物件，並指定寬、高  
            Bitmap _bmp = new Bitmap(width, height);

            // (封裝 GDI+ 繪圖介面) 所有繪圖作業都需透過 Graphics 物件進行操作  
            Graphics _graphics = Graphics.FromImage(_bmp);
            _graphics.Clear(Color.Beige);

            // 如果想啟用「反鋸齒」功能，可以將以下這行取消註解  
            _graphics.TextRenderingHint = TextRenderingHint.AntiAlias;  

            // 設定要出現在圖片上的文字字型、大小與樣式  
            Font _fontR = new Font("Arial", 13, FontStyle.Bold);
            Font _fontI = new Font("Arial", 13, FontStyle.Italic);

            // 產生一個 4 個字元的亂碼字串，並直接寫入 Session 裡  
            Session[CaptchaSessionKey] = Util.RandomPassword.Generate(wordLen, wordLen, true, false, false, false);

            // 以較簡單的方式呈現
            _graphics.DrawString(Convert.ToString(Session[CaptchaSessionKey].ToString()), _fontR, Brushes.Black, 3, 3);

            // 增加噪線
            for (int i = 0; i < noiseLineNum; i++)
                DrawRandomLine(_graphics, height, width, r);

            // 增加噪點
            for (int i = 0; i < noisePointNum; i++)
            {
                _bmp.SetPixel(r.Next(width), r.Next(height), r.NextColor());
            }

            // 清除該頁輸出緩存，設置該頁無緩存
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");

            // 輸出之前 Captcha 圖示  
            Response.ContentType = "image/gif";
            _bmp.Save(Response.OutputStream, ImageFormat.Gif);

            // 釋放所有在 GDI+ 所佔用的記憶體空間 ( 非常重要!! )  
            _fontR.Dispose();
            _fontI.Dispose();
            _graphics.Dispose();
            _bmp.Dispose();

            // 由於我們要輸出的是「圖片」而非「網頁」，所以必須在此中斷網頁執行  
            Response.End();
        }

        private static void DrawRandomLine(Graphics graphics, int height, int width, Random random)
        {
            Pen pen = new Pen(random.NextColor());
            pen.Width = random.Next(3);
            Point p1 = new Point(random.Next(width), random.Next(height));
            Point p2 = new Point(random.Next(width), random.Next(height));
            graphics.DrawLine(pen, p1, p2);
        }
    }

    public static class RandomExtension
    {
        /// <summary>
        /// 取得下一個顏色
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public static Color NextColor(this Random random)
        {
            return Color.FromArgb(130, random.Next(255), random.Next(255), random.Next(255));
        }
    }
}