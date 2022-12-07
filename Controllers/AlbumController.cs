using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class AlbumsController : ControllerBase
{

    [HttpGet("{album_id}")]
    public string Get(int album_id)
    {



        using (api.RecordStoreContext db = new RecordStoreContext())
        {
            Album album = db.Albums.Where(album => album.AlbumId == album_id).First();
            return JsonConvert.SerializeObject(new { data = album }); ;
        }

    }

    [HttpGet()]
    public string Get()
    {
        using (api.RecordStoreContext db = new RecordStoreContext())
        {
            IOrderedQueryable<Album> albums = db.Albums.OrderBy(album => album.AlbumId);
            return JsonConvert.SerializeObject(new { data = albums }); ;
        }
    }

    [HttpDelete("{album_id}")]
    public string Delete(int album_id)
    {
        using (api.RecordStoreContext db = new RecordStoreContext())
        {
            Album album = db.Albums.Where(album => album.AlbumId == album_id).First();
            db.Remove(album);
            db.SaveChanges();
            string json = JsonConvert.SerializeObject(new
            {
                message = $"album {album_id} successfully deleted",

            });
            return json;

        }
    }


    [HttpPost()]
    public string Post(Album album)

    {

        using (api.RecordStoreContext db = new RecordStoreContext())
        {
            db.Add(album);
            db.SaveChanges();
            string json = JsonConvert.SerializeObject(new
            {
                message = "album successfully created",
                data = album
            });
            return json;
        }

    }

    [HttpPatch("{album_id}")]
    public string Patch(int album_id, Album album)

    {
        using (api.RecordStoreContext db = new RecordStoreContext())
        {
            Album target = db.Albums.Where(album => album.AlbumId == album_id).First();
            target.Artist = album.Artist;
            target.Title = album.Title;
            target.Stock = album.Stock;
            target.ReleaseDate = album.ReleaseDate;
            target.Price = album.Price;
            db.SaveChanges();
            string json = JsonConvert.SerializeObject(new
            {
                message = $"album {album_id} successfully updated",
                data = target
            });
            return json;
        }

    }

}
