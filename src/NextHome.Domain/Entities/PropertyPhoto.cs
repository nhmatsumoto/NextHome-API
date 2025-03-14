using NextHome.Domain.Enums;

namespace NextHome.Domain.Entities;

public class PropertyPhoto
{
    public int Id { get; set; }
    public string Url { get; set; } // URL da foto (写真のURL)
    public int PropertyId { get; set; }
    public PhotoType Type { get; set; } // Tipo da foto (主な写真、サムネイル、ギャラリー)
    public  Property Property { get; set; }
}
