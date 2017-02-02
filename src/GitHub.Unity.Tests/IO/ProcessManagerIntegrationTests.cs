﻿using FluentAssertions;
using NUnit.Framework;

namespace GitHub.Unity.Tests
{
    [TestFixture]
    class ProcessManagerIntegrationTests : BaseIntegrationTest
    {
        [Test]
        public void BranchListTest()
        {
            var fileSystem = new FileSystem();

            var environment = new DefaultEnvironment();
            var gitEnvironment = environment.IsWindows
                ? new WindowsGitEnvironment(fileSystem, environment)
                : (environment.IsLinux 
                    ? (IGitEnvironment)new LinuxBasedGitEnvironment(fileSystem, environment)
                    : new MacBasedGitEnvironment(fileSystem, environment));

            var processManager = new ProcessManager(environment, gitEnvironment, fileSystem);
            var gitBranches = processManager.GetGitBranches(TestGitRepoPath);

            gitBranches.Should().BeEquivalentTo(
                new GitBranch("master", string.Empty, true),
                new GitBranch("feature/document", string.Empty, false));
        }
    }
}