﻿using UnityEngine;
using System.Collections.Generic;

namespace DG.Util
{
    public class Drawing
    {
        public enum Samples
        {
            None,
            Samples2,
            Samples4,
            Samples8,
            Samples16,
            Samples32,
            RotatedDisc
        }

        public static Samples NumSamples = Samples.Samples4;

        public static Texture2D DrawLine(Vector2 from, Vector2 to, float w, Color col, Texture2D tex)
        {
            return DrawLine(from, to, w, col, tex, false, Color.black, 0);
        }

        public static Texture2D DrawLine(Vector2 from, Vector2 to, float w, Color col, Texture2D tex, bool stroke, Color strokeCol, float strokeWidth)
        {
            w = Mathf.Round(w);//It is important to round the numbers otherwise it will mess up with the texture width
            strokeWidth = Mathf.Round(strokeWidth);

            var extent = w + strokeWidth;
            var stY = Mathf.Clamp(Mathf.Min(from.y, to.y) - extent, 0, tex.height);//This is the topmost Y value
            var stX = Mathf.Clamp(Mathf.Min(from.x, to.x) - extent, 0, tex.width);
            var endY = Mathf.Clamp(Mathf.Max(from.y, to.y) + extent, 0, tex.height);
            var endX = Mathf.Clamp(Mathf.Max(from.x, to.x) + extent, 0, tex.width);//This is the rightmost Y value

            strokeWidth = strokeWidth / 2;
            var strokeInner = (w - strokeWidth) * (w - strokeWidth);
            var strokeOuter = (w + strokeWidth) * (w + strokeWidth);
            var strokeOuter2 = (w + strokeWidth + 1) * (w + strokeWidth + 1);
            var sqrW = w * w;//It is much faster to calculate with squared values

            var lengthX = endX - stX;
            var lengthY = endY - stY;
            var start = new Vector2(stX, stY);
            Color[] pixels = tex.GetPixels((int)stX, (int)stY, (int)lengthX, (int)lengthY, 0);//Get all pixels

            for (int y = 0; y < lengthY; y++)
            {
                for (int x = 0; x < lengthX; x++)
                {//Loop through the pixels
                    var p = new Vector2(x, y) + start;
                    var center = p + new Vector2(0.5f, 0.5f);
                    float dist = (center - Mathfx.NearestPointStrict(from, to, center)).sqrMagnitude;//The squared distance from the center of the pixels to the nearest point on the line
                    if (dist <= strokeOuter2)
                    {
                        var samples = Sample(p);
                        var c = Color.black;
                        var pc = pixels[y * (int)lengthX + x];
                        for (int i = 0; i < samples.Length; i++)
                        {//Loop through the samples
                            dist = (samples[i] - Mathfx.NearestPointStrict(from, to, samples[i])).sqrMagnitude;//The squared distance from the sample to the line
                            if (stroke)
                            {
                                if (dist <= strokeOuter && dist >= strokeInner)
                                {
                                    c += strokeCol;
                                }
                                else if (dist < sqrW)
                                {
                                    c += col;
                                }
                                else
                                {
                                    c += pc;
                                }
                            }
                            else
                            {
                                if (dist < sqrW)
                                {//Is the distance smaller than the width of the line
                                    c += col;
                                }
                                else
                                {
                                    c += pc;//No it wasn't, set it to be the original colour
                                }
                            }
                        }
                        c /= samples.Length;//Get the avarage colour
                        pixels[y * (int)lengthX + x] = c;
                    }
                }
            }
            tex.SetPixels((int)stX, (int)stY, (int)lengthX, (int)lengthY, pixels, 0);
            tex.Apply();
            return tex;
        }

        public static Texture2D Paint(Vector2 pos, float rad, Color col, float hardness, Texture2D tex)
        {
            var start = new Vector2(Mathf.Clamp(pos.x - rad, 0, tex.width), Mathf.Clamp(pos.y - rad, 0, tex.height));
            //var width = rad * 2;
            var end = new Vector2(Mathf.Clamp(pos.x + rad, 0, tex.width), Mathf.Clamp(pos.y + rad, 0, tex.height));
            var widthX = Mathf.Round(end.x - start.x);
            var widthY = Mathf.Round(end.y - start.y);
            //var sqrRad = rad * rad;
            var sqrRad2 = (rad + 1) * (rad + 1);
            Color[] pixels = tex.GetPixels((int)start.x, (int)start.y, (int)widthX, (int)widthY, 0);

            for (var y = 0; y < widthY; y++)
            {
                for (var x = 0; x < widthX; x++)
                {
                    var p = new Vector2(x, y) + start;
                    var center = p + new Vector2(0.5f, 0.5f);
                    float dist = (center - pos).sqrMagnitude;
                    if (dist > sqrRad2)
                    {
                        continue;
                    }
                    var samples = Sample(p);
                    var c = Color.black;
                    for (var i = 0; i < samples.Length; i++)
                    {
                        dist = Mathfx.GaussFalloff(Vector2.Distance(samples[i], pos), rad) * hardness;
                        if (dist > 0)
                        {
                            c += Color.Lerp(pixels[y * (int)widthX + x], col, dist);
                        }
                        else
                        {
                            c += pixels[y * (int)widthX + x];
                        }
                    }
                    c /= samples.Length;

                    pixels[y * (int)widthX + x] = c;
                }
            }

            tex.SetPixels((int)start.x, (int)start.y, (int)widthX, (int)widthY, pixels, 0);
            return tex;
        }

        public static void PaintLine(Vector2 from, Vector2 to, float rad, Color col, float strokeHardness, ref Texture2D tex)
        {
            //var width = rad * 2;
            PaintLine(new Stroke(){start = from, end = to, width = rad, color = col, hardness = strokeHardness}, ref tex);
            //var extent = rad;
            //var stY = Mathf.Clamp(Mathf.Min(from.y, to.y) - extent, 0, tex.height);
            //var stX = Mathf.Clamp(Mathf.Min(from.x, to.x) - extent, 0, tex.width);
            //var endY = Mathf.Clamp(Mathf.Max(from.y, to.y) + extent, 0, tex.height);
            //var endX = Mathf.Clamp(Mathf.Max(from.x, to.x) + extent, 0, tex.width);

            //var lengthX = endX - stX;
            //var lengthY = endY - stY;

            ////var sqrRad = rad * rad;
            //var sqrRad2 = (rad + 1) * (rad + 1);

            //var start = new Vector2(stX, stY);

            //Vector2 p = new Vector2();
            //Vector2 center = new Vector2();
            //float dist;
            //Color c;

            //for (int y = 0; y < (int)lengthY; y++)
            //{
            //    for (int x = 0; x < (int)lengthX; x++)
            //    {
            //        p.Set(x, y);
            //        p += start;

            //        center = p + Vector2.one * 0.5f;

            //        dist = (center - Mathfx.NearestPointStrict(from, to, center)).sqrMagnitude;
            //        if (dist > sqrRad2)
            //        {
            //            continue;
            //        }
            //        dist = Mathfx.GaussFalloff(Mathf.Sqrt(dist), rad) * hardness;
            //        if (dist > 0)
            //        {
            //            c = col;
            //            tex.SetPixel(x+(int)stX,y+(int)stY, col);
            //        }
            //    }
            //}
            //tex.Apply(false);
        }

        public static void PaintLine(Stroke stroke, ref Texture2D tex)
        {
            //var width = stroke.width * 2;

            var extent = stroke.width;
            var stY = Mathf.Clamp(Mathf.Min(stroke.start.y, stroke.end.y) - extent, 0, tex.height);
            var stX = Mathf.Clamp(Mathf.Min(stroke.start.x, stroke.end.x) - extent, 0, tex.width);
            var endY = Mathf.Clamp(Mathf.Max(stroke.start.y, stroke.end.y) + extent, 0, tex.height);
            var endX = Mathf.Clamp(Mathf.Max(stroke.start.x, stroke.end.x) + extent, 0, tex.width);

            var lengthX = endX - stX;
            var lengthY = endY - stY;

            //var sqrRad = stroke.width * stroke.width;
            var sqrRad2 = (stroke.width + 1) * (stroke.width + 1);

            var start = new Vector2(stX, stY);

            Vector2 p = new Vector2();
            Vector2 center = new Vector2();
            float dist;

            for (int y = 0; y < (int)lengthY; y++)
            {
                for (int x = 0; x < (int)lengthX; x++)
                {
                    p.Set(x, y);
                    p += start;

                    center = p + Vector2.one * 0.5f;

                    dist = (center - Mathfx.NearestPointStrict(stroke.start, stroke.end, center)).sqrMagnitude;
                    if (dist > sqrRad2)
                    {
                        continue;
                    }
                    dist = Mathfx.GaussFalloff(Mathf.Sqrt(dist), stroke.width) * stroke.hardness;
                    if (dist > 0)
                    {
                        tex.SetPixel(x + (int)stX, y + (int)stY, stroke.color);
                    }
                }
            }
            tex.Apply(false);
        }

        public static void MergeTextures(ref Texture2D baseTex, ref Texture2D drawingTex, int downScalingRatio)
        {
            Color c;
            for (int y = 0; y < baseTex.height; y++)
            {
                for (int x = 0; x < baseTex.width; x++)
                {
                    c = drawingTex.GetPixel((x) / (downScalingRatio), (y) / (downScalingRatio));
                    if (c == Color.clear) continue;
                    baseTex.SetPixel((x), (y), c);
                }
            }
            baseTex.Apply(false);
        }

        public class Stroke
        {
            public Vector2 start;
            public Vector2 end;
            public float width;
            public float hardness;
            public Color color;

            public void Set(Vector2 Start, Vector2 End, float Width, float Hardness, Color Color)
            {
                start = Start;
                end = End;
                width = Width;
                hardness = Hardness;
                color = Color;
            }
        }

        public class BezierPoint
        {
            internal Vector2 main;
            internal Vector2 control1;//Think of as left
            internal Vector2 control2;//Right
                                      //Rect rect;
            internal BezierCurve curve1;//Left
            internal BezierCurve curve2;//Right

            internal BezierPoint(Vector2 m, Vector2 l, Vector2 r)
            {
                main = m;
                control1 = l;
                control2 = r;
            }
        }

        public class BezierCurve
        {
            internal Vector2[] points;
            internal float aproxLength;
            internal Rect rect;
            internal Vector2 Get(float t)
            {
                int t2 = (int)Mathf.Round(t * (points.Length - 1));
                return points[t2];
            }

            void Init(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
            {

                Vector2 topleft = new Vector2(Mathf.Infinity, Mathf.Infinity);
                Vector2 bottomright = new Vector2(Mathf.NegativeInfinity, Mathf.NegativeInfinity);

                topleft.x = Mathf.Min(topleft.x, p0.x);
                topleft.x = Mathf.Min(topleft.x, p1.x);
                topleft.x = Mathf.Min(topleft.x, p2.x);
                topleft.x = Mathf.Min(topleft.x, p3.x);

                topleft.y = Mathf.Min(topleft.y, p0.y);
                topleft.y = Mathf.Min(topleft.y, p1.y);
                topleft.y = Mathf.Min(topleft.y, p2.y);
                topleft.y = Mathf.Min(topleft.y, p3.y);

                bottomright.x = Mathf.Max(bottomright.x, p0.x);
                bottomright.x = Mathf.Max(bottomright.x, p1.x);
                bottomright.x = Mathf.Max(bottomright.x, p2.x);
                bottomright.x = Mathf.Max(bottomright.x, p3.x);

                bottomright.y = Mathf.Max(bottomright.y, p0.y);
                bottomright.y = Mathf.Max(bottomright.y, p1.y);
                bottomright.y = Mathf.Max(bottomright.y, p2.y);
                bottomright.y = Mathf.Max(bottomright.y, p3.y);

                rect = new Rect(topleft.x, topleft.y, bottomright.x - topleft.x, bottomright.y - topleft.y);


                var ps = new List<Vector2>();

                var point1 = Mathfx.CubicBezier(0, p0, p1, p2, p3);
                var point2 = Mathfx.CubicBezier(0.05f, p0, p1, p2, p3);
                var point3 = Mathfx.CubicBezier(0.1f, p0, p1, p2, p3);
                var point4 = Mathfx.CubicBezier(0.15f, p0, p1, p2, p3);

                var point5 = Mathfx.CubicBezier(0.5f, p0, p1, p2, p3);
                var point6 = Mathfx.CubicBezier(0.55f, p0, p1, p2, p3);
                var point7 = Mathfx.CubicBezier(0.6f, p0, p1, p2, p3);

                aproxLength = Vector2.Distance(point1, point2) + Vector2.Distance(point2, point3) + Vector2.Distance(point3, point4) + Vector2.Distance(point5, point6) + Vector2.Distance(point6, point7);

                Debug.Log(Vector2.Distance(point1, point2) + "     " + Vector2.Distance(point3, point4) + "   " + Vector2.Distance(point6, point7));
                aproxLength *= 4;

                float a2 = 0.5f / aproxLength;//Double the amount of points since the approximation is quite bad
                for (float i = 0; i < 1; i += a2)
                {
                    ps.Add(Mathfx.CubicBezier(i, p0, p1, p2, p3));
                }

                points = ps.ToArray();
            }

            internal BezierCurve(Vector2 main, Vector2 control1, Vector2 control2, Vector2 end)
            {
                Init(main, control1, control2, end);
            }
        }

        public static void DrawBezier(BezierPoint[] points, float rad, Color col, Texture2D tex)
        {
            rad = Mathf.Round(rad);//It is important to round the numbers otherwise it will mess up with the texture width

            if (points.Length <= 1)
                return;

            Vector2 topleft = new Vector2(Mathf.Infinity, Mathf.Infinity);
            Vector2 bottomright = new Vector2(0, 0);

            for (int i = 0; i < points.Length - 1; i++)
            {
                Vector2 main = points[i].main;
                Vector2 control2 = points[i].control2;
                Vector2 control1 = points[i + 1].control1;
                Vector2 main2 = points[i + 1].main;
                BezierCurve curve = new BezierCurve(main, control2, control1, main2);
                points[i].curve2 = curve;
                points[i + 1].curve1 = curve;

                topleft.x = Mathf.Min(topleft.x, curve.rect.x);

                topleft.y = Mathf.Min(topleft.y, curve.rect.y);

                bottomright.x = Mathf.Max(bottomright.x, curve.rect.x + curve.rect.width);

                bottomright.y = Mathf.Max(bottomright.y, curve.rect.y + curve.rect.height);
            }

            topleft -= new Vector2(rad, rad);
            bottomright += new Vector2(rad, rad);

            var start = new Vector2(Mathf.Clamp(topleft.x, 0, tex.width), Mathf.Clamp(topleft.y, 0, tex.height));
            var width = new Vector2(Mathf.Clamp(bottomright.x - topleft.x, 0, tex.width - start.x), Mathf.Clamp(bottomright.y - topleft.y, 0, tex.height - start.y));

            Color[] pixels = tex.GetPixels((int)start.x, (int)start.y, (int)width.x, (int)width.y, 0);

            for (var y = 0; y < width.y; y++)
            {
                for (var x = 0; x < width.x; x++)
                {
                    var p = new Vector2(x + start.x, y + start.y);
                    if (!Mathfx.IsNearBeziers(p, points, rad + 2))
                    {
                        continue;
                    }

                    var samples = Sample(p);
                    var c = Color.black;
                    var pc = pixels[y * (int)width.x + x];//Previous pixel color
                    for (var i = 0; i < samples.Length; i++)
                    {
                        if (Mathfx.IsNearBeziers(samples[i], points, rad))
                        {
                            c += col;
                        }
                        else
                        {
                            c += pc;
                        }
                    }

                    c /= samples.Length;

                    pixels[y * (int)width.x + x] = c;
                }
            }

            tex.SetPixels((int)start.x, (int)start.y, (int)width.x, (int)width.y, pixels, 0);
            tex.Apply();
        }

        public static void AddP(List<Vector2> tmpList, Vector2 p, float ix, float iy)
        {
            var x = p.x + ix;
            var y = p.y + iy;
            tmpList.Add(new Vector2(x, y));
        }

        public static Vector2[] Sample(Vector2 p)
        {
            List<Vector2> tmpList = new List<Vector2>(32);

            switch (NumSamples)
            {
                case Samples.None:
                    AddP(tmpList, p, 0.5f, 0.5f);
                    break;

                case Samples.Samples2:
                    AddP(tmpList, p, 0.25f, 0.5f);
                    AddP(tmpList, p, 0.75f, 0.5f);
                    break;

                case Samples.Samples4:
                    AddP(tmpList, p, 0.25f, 0.5f);
                    AddP(tmpList, p, 0.75f, 0.5f);
                    AddP(tmpList, p, 0.5f, 0.25f);
                    AddP(tmpList, p, 0.5f, 0.75f);
                    break;

                case Samples.Samples8:
                    AddP(tmpList, p, 0.25f, 0.5f);
                    AddP(tmpList, p, 0.75f, 0.5f);
                    AddP(tmpList, p, 0.5f, 0.25f);
                    AddP(tmpList, p, 0.5f, 0.75f);

                    AddP(tmpList, p, 0.25f, 0.25f);
                    AddP(tmpList, p, 0.75f, 0.25f);
                    AddP(tmpList, p, 0.25f, 0.75f);
                    AddP(tmpList, p, 0.75f, 0.75f);
                    break;
                case Samples.Samples16:
                    AddP(tmpList, p, 0, 0);
                    AddP(tmpList, p, 0.3f, 0);
                    AddP(tmpList, p, 0.7f, 0);
                    AddP(tmpList, p, 1, 0);

                    AddP(tmpList, p, 0, 0.3f);
                    AddP(tmpList, p, 0.3f, 0.3f);
                    AddP(tmpList, p, 0.7f, 0.3f);
                    AddP(tmpList, p, 1, 0.3f);

                    AddP(tmpList, p, 0, 0.7f);
                    AddP(tmpList, p, 0.3f, 0.7f);
                    AddP(tmpList, p, 0.7f, 0.7f);
                    AddP(tmpList, p, 1, 0.7f);

                    AddP(tmpList, p, 0, 1);
                    AddP(tmpList, p, 0.3f, 1);
                    AddP(tmpList, p, 0.7f, 1);
                    AddP(tmpList, p, 1, 1);
                    break;

                case Samples.Samples32:
                    AddP(tmpList, p, 0, 0);
                    AddP(tmpList, p, 1, 0);
                    AddP(tmpList, p, 0, 1);
                    AddP(tmpList, p, 1, 1);

                    AddP(tmpList, p, 0.2f, 0.2f);
                    AddP(tmpList, p, 0.4f, 0.2f);
                    AddP(tmpList, p, 0.6f, 0.2f);
                    AddP(tmpList, p, 0.8f, 0.2f);

                    AddP(tmpList, p, 0.2f, 0.4f);
                    AddP(tmpList, p, 0.4f, 0.4f);
                    AddP(tmpList, p, 0.6f, 0.4f);
                    AddP(tmpList, p, 0.8f, 0.4f);

                    AddP(tmpList, p, 0.2f, 0.6f);
                    AddP(tmpList, p, 0.4f, 0.6f);
                    AddP(tmpList, p, 0.6f, 0.6f);
                    AddP(tmpList, p, 0.8f, 0.6f);

                    AddP(tmpList, p, 0.2f, 0.8f);
                    AddP(tmpList, p, 0.4f, 0.8f);
                    AddP(tmpList, p, 0.6f, 0.8f);
                    AddP(tmpList, p, 0.8f, 0.8f);

                    AddP(tmpList, p, 0.5f, 0);
                    AddP(tmpList, p, 0.5f, 1);
                    AddP(tmpList, p, 0, 0.5f);
                    AddP(tmpList, p, 1, 0.5f);

                    AddP(tmpList, p, 0.5f, 0.5f);
                    break;
                case Samples.RotatedDisc:
                    AddP(tmpList, p, 0, 0);
                    AddP(tmpList, p, 1, 0);
                    AddP(tmpList, p, 0, 1);
                    AddP(tmpList, p, 1, 1);

                    Vector2 pq = new Vector2(p.x + 0.5f, p.y + 0.5f);
                    AddP(tmpList, pq, 0.258f, 0.965f);//Sin (75°) && Cos (75°)
                    AddP(tmpList, pq, -0.965f, -0.258f);
                    AddP(tmpList, pq, 0.965f, 0.258f);
                    AddP(tmpList, pq, 0.258f, -0.965f);
                    break;
            }

            return tmpList.ToArray();
        }
    }
}