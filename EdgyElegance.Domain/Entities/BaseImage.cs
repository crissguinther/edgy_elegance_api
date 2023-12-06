namespace EdgyElegance.Domain.Entities;

public abstract class BaseImage : BaseEntity {
    public string Path { get; set; } = string.Empty;
}
