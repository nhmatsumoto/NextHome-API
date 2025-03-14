using NextHome.Domain.Enums;

namespace NextHome.Domain.Entities
{
    public class Property
    {
        public int Id { get; set; }
        public string Title { get; set; } // Título do imóvel (物件タイトル)
        public string Description { get; set; } // Descrição do imóvel (物件説明)
        public decimal Price { get; set; } // Preço do imóvel (価格)
        public decimal? ManagementFee { get; set; } // Taxa de administração (管理費)
        public decimal? DepositShikikin { get; set; } // Depósito (敷金)
        public decimal? KeyMoneyReikin { get; set; } // Luvas (礼金)
        public int Bedrooms { get; set; } // Número de quartos (寝室数)
        public int Bathrooms { get; set; } // Número de banheiros (バスルーム数)
        public int ParkingSpaces { get; set; } // Número de vagas de estacionamento (駐車スペース数)
        public double FloorArea { get; set; } // Área total (面積)
        public int YearBuilt { get; set; } // Ano de construção (築年数)
        public bool IsAvailable { get; set; } // Disponibilidade (利用可能かどうか)
        public PropertyType Type { get; set; } // Tipo de imóvel (物件の種類)
        public ListingCategory Category { get; set; } // Categoria do anúncio (掲載カテゴリ)
        public int AddressId { get; set; }
        public PropertyAddress Address { get; set; } // Endereço do imóvel (物件住所)
        public ICollection<PropertyPhoto> Photos { get; set; } // Fotos do imóvel (物件の写真)
        public PropertyFloorPlan FloorPlan { get; set; } // Planta do imóvel (間取り図)
        public ICollection<Inquiry> Inquiries { get; set; } // Contatos recebidos (問い合わせ)
        public int RealEstateAgencyId { get; set; } // ID da imobiliária (不動産会社ID)
        public RealEstateAgency RealEstateAgency { get; set; } // Imobiliária associada (不動産会社)
    }
}
