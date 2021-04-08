using GithubJobsEnterpriseProject.Models;
using Moq;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GithubJobsEnterpriseProject.Tests
{
    class GithubJobsRepositoryTests
    {
        [Test]
        public void TestIfWeGetUserById()
        {
            var jobsInMemoryDatabase = new List<GithubJob>();
            var initjob = new GithubJob("10","test","test2");
            jobsInMemoryDatabase.Add(initjob);

            var repository = new Mock<IGithubJobsRepository>();
            repository.Setup(x => x.GetJob(It.IsAny<string>()))
            .Returns((string i) => jobsInMemoryDatabase.Single(n => n.Id == i));

            var userToFind = repository.Object.GetJob("10");

            Assert.AreEqual(initjob, userToFind);
            Assert.AreEqual(initjob.Company, userToFind.Company);
            try
            {
                repository.Object.GetJob("100");
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.Pass();
            }
        }
    }
}
