namespace Entities.Abstruct
{
    public interface IEntity
    {
        public int Id { get; set; }
        public bool ActiveFlg { get; set; }
    }
}