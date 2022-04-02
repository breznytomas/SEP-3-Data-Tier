namespace Entity.ModelData.Behaviors
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}