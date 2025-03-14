namespace NextHome.Domain.Entities;

public class PropertyFloorPlan
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } // URL da planta (間取り図のURL)
    public int PropertyId { get; set; }
    public virtual Property Property { get; set; }

    public PropertyFloorPlan()
    {
        
    }
}
