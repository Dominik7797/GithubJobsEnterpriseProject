using GithubJobsEnterpriseProject.Models;
using GithubJobsEnterpriseProject.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace GithubJobsEnterpriseProject.Tests
{
    class LoginServiceTest
    {
        private static UserContext InitContext()
        {
            var options = new DbContextOptionsBuilder<UserContext>()
            .UseInMemoryDatabase(databaseName: "UserDatabase")
            .Options;
            var context = new UserContext(options);
            return context;
        }

        [Test]
        public void TestIfLoginReturnsTrue()
        {
            var context = InitContext();
            var username = "username";
            var password = "password";
            var email = "email";
            var hashService = new HashService();

            var hashedPassword = hashService.Hash(password);

            context.Users.Add(new User(username, email, hashedPassword));
            context.SaveChanges();

            var loginService = new LoginService(context);

            Assert.IsTrue(loginService.Login(username, password));
            Assert.IsFalse(loginService.Login(username, "wrongpass"));
        }
    }
}
