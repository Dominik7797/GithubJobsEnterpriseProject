using GithubJobsEnterpriseProject.Controllers;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;
using System;

namespace GithubJobsEnterpriseProject.Tests
{
    class JobApiServiceTests
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return config;
        }

        public static IConfiguration InitConfigurationBadData()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("badpath.json")
                .Build();
            return config;
        }

        [Test]
        public void GetLinkReturnValueCorrectTest()
        {
            var config = InitConfiguration();
            JobApiService jobApiService = new JobApiService(config);
            Assert.That(jobApiService.GetLink().Url, Is.EqualTo("https://jobs.github.com/positions.json"));
            Assert.That(jobApiService.GetLink().BaseUrl, Is.EqualTo("https://jobs.github.com/"));
        }

        [Test]
        public void GetGithubJobsFromUrlWrongUrlreturnsNullTest()
        {
            var config = Substitute.For<IConfiguration>();
            config.GetValue<string>("GithubJobs:Url").Returns("https://badtest.com/positions.json");
            JobApiService jobApiService = new JobApiService(config);
            Assert.Throws<UriFormatException>(() => jobApiService.GetGithubJobsFromUrl());
        }

        [Test]
        public void GetGithubJobsFromUrlWithParametersWrongUrlReturnValueTest()
        {
            var config = InitConfiguration();
            string location = "Budapest";
            string description = "java";
            JobApiService jobApiService = new JobApiService(config);
            Assert.IsNotNull(jobApiService.GetGithubJobsByParameters(description, location));
        }
    }
}
