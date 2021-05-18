using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DTO;
using DTO;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Drawing;
using System.Net.Http;
using System.Text;
using Helpers;

namespace Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ImagesController : ControllerBase
  {
    private readonly EcommerContext _context;
    private readonly IWebHostEnvironment _environment;
    public ImagesController(EcommerContext context, IWebHostEnvironment environment)
    {
      _context = context;
      _environment = environment;
    }

    [HttpGet]
    public async Task<IEnumerable<imageDTO>> GetImage()
    {
      return await _context.images.Select(p => p.toimageDto()).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<imageDTO>> GetImage(int id)
    {
      var img = await _context.images.FindAsync(id);
      if (img == null)
      {
        return NotFound();
      }
      return img.toimageDto();
    }

    [HttpPost]
    public async Task<ActionResult<imageDTO>> CreateImage(imageDTO imgDTO)
    {
      var imgEntity = imgDTO.toimage();
      Base64Url base64Url = new Base64Url(_environment);
      Image physicalImg = base64Url.decode(imgEntity.url);
      imgEntity.url = base64Url.writeImage(physicalImg);
      _context.images.Add(imgEntity);
      await _context.SaveChangesAsync();
      return CreatedAtAction(nameof(GetImage), new { id = imgEntity.id }, imgEntity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateImage(int id, imageDTO img2)
    {
      var img1 = await _context.images.FindAsync(img2.id);
      if (img1 == null)
      {
        return NotFound();
      }
      img1.Mapto3(img2);
      _context.images.Update(img1);
      await _context.SaveChangesAsync();
      return Ok();
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> DeleteImage(int id)
    {
      var img1 = await _context.images.FindAsync(id);
      if (img1 == null)
      {
        return NotFound();
      }
      _context.images.Remove(img1);
      await _context.SaveChangesAsync();
      return Ok();
    }

    [HttpGet]
    [Route("bitmap/{img}")]
    public async Task<IActionResult> Get(string img)
    {
      var url = "/Images/" + img;
      var path = GetPhysicalPathFromRelativeUrl(url);
      if (System.IO.File.Exists(path))
      {
        return PhysicalFile(path, "image/png");
      }
      else
      {
        return NotFound();
      }
    }

    public string GetPhysicalPathFromRelativeUrl(string url)
    {
      var path = Path.Combine(_environment.ContentRootPath, url.TrimStart('/').Replace("/", "\\"));
      return path;
    }
    // [HttpGet]
    // [Route("encode/{img}")]

    // public String encode64base(String img)
    // {
    //   String url = "/Images/" + img;
    //   String url1 = GetPhysicalPathFromRelativeUrl(url);
    //   byte[] imageArray = System.IO.File.ReadAllBytes(url1);

    //   var base64 = Convert.ToBase64String(imageArray);
    //   return base64;
    // }

    // [HttpPost]
    // [Route("decode")]
    // public IActionResult decode64base([FromBody] images i)
    // {
    //   byte[] imageBytes = Convert.FromBase64String(i.url);
    //   MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
    //   ms.Write(imageBytes, 0, imageBytes.Length);
    //   System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
    //   String url1 = "/Images/bitis.jpg";
    //   String url2 = GetPhysicalPathFromRelativeUrl(url1);
    //   image.Save(url2);
    //   return PhysicalFile(url2, "image/png");
    // }
  }
}