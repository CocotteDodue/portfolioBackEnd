using Microsoft.AspNetCore.Hosting;
using Moq;
using Xunit;

namespace Portfolio.Common.Extensions
{
    public class IHostingEnvironmentExtensionsTests
    {
        [Fact]
        public void HostingEnvironment_MustAnswerTrueToIsAnyProductionEnv_WhenEnvironmentNameContainsProd()
        {
            var fakeHostingEnvironment = new Mock<IHostingEnvironment>();
            
            fakeHostingEnvironment
                .Setup(env => env.EnvironmentName)
                .Returns("thisIsProd");

            Assert.True(fakeHostingEnvironment.Object.IsAnyProductionEnv());
        }

        [Fact]
        public void HostingEnvironment_MustAnswerFalseToIsAnyProductionEnv_WhenEnvironmentNameDoesNotContainsProd()
        {
            var fakeHostingEnvironment = new Mock<IHostingEnvironment>();

            fakeHostingEnvironment
                .Setup(env => env.EnvironmentName)
                .Returns("Dev");

            Assert.False(fakeHostingEnvironment.Object.IsAnyProductionEnv());
        }
    }
}
