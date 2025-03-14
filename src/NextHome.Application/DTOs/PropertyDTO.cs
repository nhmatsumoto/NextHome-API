namespace NextHome.Application.DTOs;

public class PropertyDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal? ManagementFee { get; set; }
    public decimal? DepositShikikin { get; set; }
    public decimal? KeyMoneyReikin { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public int ParkingSpaces { get; set; }
    public double FloorArea { get; set; }
    public int YearBuilt { get; set; }
    public bool IsAvailable { get; set; }
    public string Type { get; set; }
    public string Category { get; set; }
    public string FullAddress { get; set; } // Endereço formatado
    public List<string> PhotoUrls { get; set; } // Lista de URLs das fotos do imóvel
    public string FloorPlanUrl { get; set; } // URL da planta do imóvel
    public string RealEstateAgencyName { get; set; } // Nome da imobiliária
}
