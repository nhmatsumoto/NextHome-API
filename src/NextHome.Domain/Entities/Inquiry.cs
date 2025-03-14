namespace NextHome.Domain.Entities;

public class Inquiry
{
    public int Id { get; set; }
    public string Name { get; set; } // Nome do interessado (問い合わせ者の名前)
    public string Email { get; set; } // E-mail do interessado (問い合わせ者のメール)
    public string Phone { get; set; } // Telefone do interessado (問い合わせ者の電話番号)
    public DateTime InquiryDate { get; set; } // Data do contato (問い合わせ日)
    public int PropertyId { get; set; }
    public virtual Property Property { get; set; }

    public Inquiry()
    {
        
    }
}