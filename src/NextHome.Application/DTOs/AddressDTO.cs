namespace NextHome.Application.DTOs;

public class AddressDTO
{
    public int Id { get; set; }
    public string Street { get; set; } // Rua (通り)
    public string City { get; set; } // Cidade (市町村)
    public string Prefecture { get; set; } // Província (都道府県)
    public string PostalCode { get; set; } // Código postal (郵便番号)
    public string NearestStation { get; set; } // Estação mais próxima (最寄り駅)
    public int MinutesToStation { get; set; } // Minutos até a estação (駅までの分数)
}
