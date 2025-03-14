namespace NextHome.Domain.Entities;

public class RealEstateAgency
{
    public int Id { get; set; }
    public string Name { get; set; } // Nome da imobiliária (不動産会社名)
    public string BusinessRegistrationNumber { get; set; } // Número de registro comercial (事業者登録番号)
    public string RealEstateLicense { get; set; } // Licença imobiliária (宅地建物取引業免許)
    public string Phone { get; set; } // Telefone (電話番号)
    public string Email { get; set; } // E-mail (メールアドレス)
    public string Address { get; set; } // Endereço (住所)
    public virtual ICollection<Property> Properties { get; set; } // Lista de propriedades da imobiliária (不動産会社の物件リスト)

}
