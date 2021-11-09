using Microsoft.EntityFrameworkCore;

namespace EfLogic {
    public static class Test {
        public static void DoStuff() {
            var options = new DbContextOptionsBuilder().UseInMemoryDatabase("test").Options;
            using var context = new TestContext(options);
            var x = new TestModel() {
                Id = 1,
                Name = "Test"
            };
            context.Add(x); //Breaks in .NET 6 but works in .NET 5
            context.SaveChanges();
            var y = context.TestModels.FirstOrDefault(); //Breaks in .NET 6 but works in .NET 5
            Console.WriteLine(y?.Id);
        }
    }
    
    public class TestContext : DbContext {
        public TestContext(DbContextOptions options) : base(options) { }
    
        public DbSet<TestModel> TestModels { get; set; }
    }
    
    public class TestModel {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}