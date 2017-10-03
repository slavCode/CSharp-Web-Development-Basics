namespace ManyToManyRelation
{
    public class StartUp
    {
        public static void Main()
        {
            var context = new MyDbContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
