namespace Integrations.Cdek.Entities.RegisterOrderEntities;

public class Package
{
    public string Number {  get; set; }

    public int Weight {  get; set; }

    public int Length {  get; set; }
    public int Width {  get; set; }
    public int Height { get; set; }

    public Item[] Items { get; set; }
}