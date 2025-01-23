namespace QLBH_Core.Commons
{
    public class BaseEntity
    {
        public long Id { get; set; }
    }
    public class BaseEntityIsDelete : BaseEntity
    {
        public bool IsDelete { get; set; }
    }
    public class BaseMasterData : BaseEntityIsDelete
    {
        public string Name { get; set; }
    }
}
