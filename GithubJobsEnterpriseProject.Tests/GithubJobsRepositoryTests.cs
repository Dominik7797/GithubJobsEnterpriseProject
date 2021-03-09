using GithubJobsEnterpriseProject.Models;
using NSubstitute;
using NUnit.Framework;

namespace GithubJobsEnterpriseProject.Tests
{
    class GithubJobsRepositoryTests
    {
        public IGithubJobsRepository githubJobsRepository = Substitute.For<IGithubJobsRepository>();

        [Test]
        public void TestIfWeGetJobById()
        {
            var githubJob = new GithubJob("5","testCompany","testJob");
            githubJobsRepository.GetJob("5").Returns(githubJob);
            Assert.AreEqual(githubJob, githubJobsRepository.GetJob("5"));
        }

        [Test]
        public void TestIfWeGetJobByWrongId()
        {
            var githubJob = new GithubJob("5", "testCompany", "testJob");
            githubJobsRepository.GetJob("6").Returns(new GithubJob("6", "testCompany2", "testJob2"));
            Assert.AreNotEqual(githubJob, githubJobsRepository.GetJob("6"));
        }

        [Test]
        public void TestIfWeGetJobCompany()
        {
            var githubJob = new GithubJob("5", "testCompany", "testJob");
            githubJobsRepository.GetJob("5").Returns(githubJob);
            Assert.AreEqual(githubJob.Company, githubJobsRepository.GetJob("5").Company);
        }

        [Test]
        public void TestIfJobIsDeleted()
        {
            var jobToDelete = new GithubJob("5", "testCompany", "testJob");
            githubJobsRepository.DeleteJob("5").Returns(jobToDelete);
            Assert.AreEqual(jobToDelete, githubJobsRepository.DeleteJob("5"));
        }
    }
}
