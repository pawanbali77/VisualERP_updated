<%@ WebHandler Language="C#" Class="ResizerImage" %>

using System;
using System.Web;
using ImageResizer;

public class ResizerImage : IHttpHandler
{
    int widht, height;
    string imagePath = "";
    public void ProcessRequest(HttpContext context)
    {
        
// Specifying only one of width or height will behave the same as using maxwidth or maxheight. The difference is when you specify both.
// Specifying both width and height will force the image to those exact dimensions, unless the image is already smaller (see scale). 
// This is done by adding whitespace to the image. To center and crop instead, use &crop=auto. To lose aspect ratio and fill the specified rectangle, 
// use &stretch=fill.
// By default, images are not upscaled. If an image is already smaller than width/height/maxwidth/maxheight, it is not resized. To upscale images, use 
// ?scale=both. ?scale=downscaleonly is the default.

        //ResizeSettings settings = new ResizeSettings("?maxwidth=300&maxheight=300&format=png");


        if (context.Request["w"] != null)
            widht = Convert.ToInt32(context.Request["w"]);
        if (context.Request["h"] != null)
            height = Convert.ToInt32(context.Request["h"]);
        if (context.Request["path"] != null)
            imagePath = context.Request["path"];
        ResizeSettings settings = new ResizeSettings();
        settings.Height = height;
        settings.Width = widht;
        settings.Scale = ScaleMode.Both;
        settings.BackgroundColor = System.Drawing.Color.White;
       
      //  settings.BackgroundColor = System.Drawing.Color.Transparent;
        //settings.Scale = ScaleMode.Both;
        //settings.CropMode = CropMode.Auto;
        //settings.Stretch = StretchMode.Fill;
        
        //Set the mime-type
        context.Response.ContentType = ImageBuilder.Current.EncoderProvider.GetEncoder(settings, imagePath).MimeType;
        //Send result to output stream. 
        ImageBuilder.Current.Build(imagePath, context.Response.OutputStream, settings);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
