using System;
using System.Collections.Generic;

namespace api;

public partial class Album
{
    public int AlbumId { get; set; }

    public string? Title { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public int? Stock { get; set; }

    public string? Artist { get; set; }

    public float? Price { get; set; }
}
