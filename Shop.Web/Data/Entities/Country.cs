
namespace Shop.Web.Data.Entities
{
    using Shop.Web.Data.Entity;
    public class Country:IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
