using GithubJobsEnterpriseProject.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GithubJobsEnterpriseProject.Tests
{
    class UserRepositoryTests
    {
        [Test]
        public void TestIfWeGetUserById()
        {
            var usersInMemoryDatabase = new List<User>();
            var initUser = new User(34, "username", "email", "password");
            usersInMemoryDatabase.Add(initUser);

            var repository = new Mock<IUserRepository>();
            repository.Setup(x => x.GetUser(It.IsAny<int>()))
            .Returns((int i) => usersInMemoryDatabase.Single(n => n.Id == i));

            var userToFind = repository.Object.GetUser(34);

            Assert.AreEqual(initUser, userToFind);
            Assert.AreEqual(initUser.Username, userToFind.Username);
            try
            {
                repository.Object.GetUser(2);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.Pass();
            }
        }

    }
}
