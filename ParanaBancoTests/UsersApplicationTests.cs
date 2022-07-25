using Microsoft.EntityFrameworkCore;
using ParanaBancoWeb.Application;
using ParanaBancoWeb.Models;

namespace ParanaBancoTests
{
    [TestClass]
    public class UsersApplicationTests
    {
        [TestMethod]
        public async Task Should_Return_Created_User()
        {
            var context = CreateContext();

            var app = new UsersApplication(context);

            var user = await app.CreateUser(new ParanaBancoWeb.ViewModels.UserSignUp() { Email = "diegol.aquino@gmail.com", Nome = "Diego Aquino" });

            Assert.IsNotNull(user);
            Assert.IsTrue(user.Email == "diegol.aquino@gmail.com");

        }

        [TestMethod]
        public void Should_Return_Completed_Task_When_Deleted()
        {
            var context = CreateContext();

            context.Users.Add(new User() { Email = "diegol.aquino@gmail.com", Nome = "Diego Aquino" });
            context.SaveChanges();

            var app = new UsersApplication(context);

             var task = app.DeleteUser("diegol.aquino@gmail.com");

            Assert.IsNotNull(task);
            Assert.IsTrue(task == Task.CompletedTask);

        }

        [TestMethod]
        public async Task Should_Return_Updated_User()
        {
            var context = CreateContext();

            var id = Guid.NewGuid();
            var userAdd = new User() { Id = id, Email = "diegol.aquino@gmail.com", Nome = "Diego Aquino" };
            context.Users.Add(userAdd);
            context.SaveChanges();

            context.Entry(userAdd).State = EntityState.Detached;

            var app = new UsersApplication(context);

            var user = await app.UpdateUser(new ParanaBancoWeb.ViewModels.UserViewModel() { Id = id, Email = "diego@gmail.com", Nome = "Aquino"  });

            Assert.IsNotNull(user);
            Assert.IsTrue(user.Nome == "Aquino");
            Assert.IsTrue(user.Email == "diego@gmail.com");

        }

        private ApplicationContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            var databaseContext = new ApplicationContext(options);
            databaseContext.Database.EnsureCreated();

            return databaseContext;
        } 
    }
}