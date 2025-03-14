using NextHome.Domain.Entities;
using NextHome.Domain.Enums;

namespace NextHome.Application.DTOs;

public class PropertyPhotoDTO
{
    public int Id { get; set; }
    public string Url { get; set; } // URL da foto (写真のURL)
    public PhotoType Type { get; set; } // Tipo da foto (主な写真、サムネイル、ギャラリー)
}
