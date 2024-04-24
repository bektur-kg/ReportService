namespace FonTech.Domain.Interfaces;

public interface IAuditable
{
    public long CreatedBy { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long? UpdatedBy { get; set; }
}
