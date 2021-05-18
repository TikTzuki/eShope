using System.Net.NetworkInformation;
using System.IO;
using System;
using System.Drawing;
using Microsoft.AspNetCore.Hosting;
using System.Drawing.Imaging;

namespace Helpers
{
  public class Base64Url
  {
    private readonly IWebHostEnvironment _environment;
    public static readonly string ImagePath = "/Images/";
    public static readonly string ImageRoute = "api/images/bitmap/";
    public Base64Url(IWebHostEnvironment environment)
    {
      _environment = environment;
    }
    public Image decode(string uri)
    {
      if(uri.IndexOf(",") != -1){
        uri = uri.Substring(uri.IndexOf(",")+1);
      }
      byte[] imageBytes = Convert.FromBase64String(uri);
      MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
      ms.Write(imageBytes, 0, imageBytes.Length);
      return Image.FromStream(ms, true);
    }

    public String writeImage(Image img)
    {
      ImageFormat format = ImageFormat.Png;
      string imageName = new Random().Next() + DateTime.Now.ToFileTime().ToString() + ".jpg";
      string imagePath = ImagePath + imageName;
      string relativePath = ImageRoute + imageName;
      string imagePhysicalPath = GetPhysicalPathFromRelativeUrl(imagePath);
      img.Save(imagePhysicalPath, format);
      return relativePath;
    }

    public string GetPhysicalPathFromRelativeUrl(string url)
    {
      var path = Path.Combine(_environment.ContentRootPath, url.TrimStart('/').Replace("/", "\\"));
      return path;
    }
  }
}