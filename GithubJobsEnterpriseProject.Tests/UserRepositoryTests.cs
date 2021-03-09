using GithubJobsEnterpriseProject.Models;
using NSubstitute;
using NUnit.Framework;

namespace GithubJobsEnterpriseProject.Tests
{
    class UserRepositoryTests
    {
        public IUserRepository userRepository = Substitute.For<IUserRepository>();

        [Test]
        public void TestIfWeGetUserById()
        {
            var user = new User("username", "email", "password");
            userRepository.GetUser(5).Returns(user);
            Assert.AreEqual(user, userRepository.GetUser(5));
        }

        [Test]
        public void TestIfWeGetUserByWrongId()
        {
            var user = new User("username", "email", "password");
            userRepository.GetUser(6).Returns(new User("username11", "email2", "password2"));
            Assert.AreNotEqual(user, userRepository.GetUser(6));
        }

        [Test]
        public void TestIfWeGetUsernameAndEmail()
        {
            var user = new User("username", "email", "password");
            userRepository.GetUser(5).Returns(user);
            Assert.AreEqual(user.Username, userRepository.GetUser(5).Username);
            Assert.AreEqual(user.Email, userRepository.GetUser(5).Email);
        }

        [Test]
        public void TestIfJobIsDeleted()
        {
            var userToDelete = new User("username", "email", "password");
            userRepository.DeleteUser(5).Returns(userToDelete);
            Assert.AreEqual(userToDelete, userRepository.DeleteUser(5));
        }
    }
}
