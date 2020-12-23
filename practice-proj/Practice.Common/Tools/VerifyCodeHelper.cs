using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Practice.Common.Tools
{
    /// <summary>
    /// 验证码帮助类
    /// </summary>
    public static class VerifyCodeHelper
    {
        /// <summary>
        /// 生成英文数字混合的验证码
        /// </summary>
        /// <param name="length">验证码位数</param>
        /// <returns></returns>
        public static string GenerateRandomCode(int length)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,a,b,c,d,e,f,g,h,i,g,k,l,m,n,o,p,q,r,F,G,H,I,G,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,s,t,u,v,w,x,y,z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(35);
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }

        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        public static byte[] CodeImage(string code)
        {
            const int charLen = 35;

            byte[] bytes;
            var colors = new[]{
                new
                {
                     Background = Color.Gray,
                      Colors = new []{Color.Green,Color.White,Color.Red,Color.Orange,Color.Blue,Color.BlueViolet}
                },
                new
                {
                     Background = Color.White,
                      Colors = new []{Color.Green,Color.Black,Color.Red,Color.Orange, Color.Blue, Color.BlueViolet}
                }
            };
            //字体集合
            string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体", "华文彩云" };
            //角度
            var angle = new[] { -50, -35, -15, 0, 15, 30, 50 };
            using (var img = new Bitmap((int)code.Length * charLen + 40, 50))
            {
                using (var graphics = Graphics.FromImage(img))
                {
                    var data = colors[new Random().Next(0, colors.Length)];
                    graphics.Clear(data.Background);
                    //绘制噪点
                    for (int i = 0; i < 50; i++)
                    {
                        img.SetPixel(new Random().Next(img.Width), new Random().Next(img.Height), Color.FromArgb(new Random().Next()));
                    }
                    //绘制干扰线
                    for (int i = 0; i < 20; i++)
                    {
                        graphics.DrawLine(new Pen(data.Colors[new Random().Next(0, data.Colors.Length)], 1),
                            new Point(new Random().Next(img.Width), new Random().Next(img.Height)),
                            new Point(new Random().Next(img.Width), new Random().Next(img.Height)));
                    }
                    //绘制文字
                    for (int i = 0; i < code.Length; i++)
                    {
                        int y = 5 + new Random().Next(2, 8);
                        int x = 23 + (i * charLen);
                        var font = new Font(fonts[new Random().Next(fonts.Length)], 18, FontStyle.Bold);//字体  
                        var brush = new SolidBrush(data.Colors[new Random().Next(0, data.Colors.Length)]);//颜色  
                        var sf = graphics.MeasureString(code[i].ToString(), font); //计算出来文字所占矩形区域
                        //进行旋转
                        var matrix = graphics.Transform;
                        if (i > 0)
                        {
                            matrix.Reset();
                        }
                        matrix.RotateAt(angle[new Random().Next(0, angle.Length)], new PointF(x + sf.Width / 2, y + sf.Height / 2));
                        graphics.Transform = matrix;

                        graphics.DrawString(code[i].ToString(), font, brush, x, y);//绘制一个验证字符  
                    }
                    using (var ms = new MemoryStream())
                    {
                        img.Save(ms, ImageFormat.Gif);
                        bytes = ms.ToArray();
                    }
                }
            }
            return bytes;
        }
    }
}
