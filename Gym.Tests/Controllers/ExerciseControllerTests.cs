using FluentAssertions;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Gym.Tests.Controllers
{
    public class ExerciseControllerTests : ControllerTestsTemplate
    {
        [Fact]
        public async Task get_all_item_exercise_should_exist()
        {
            var response = await Client.GetAsync($"exercise");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task get_item_by_id_exercise_should_exist()
        {
            var response = await Client.GetAsync($"exercise/479dd50b-2382-4124-9736-534f5a74e7b2");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task get_item_by_id_exercise_should_not_exist()
        {
            var response = await Client.GetAsync($"exercise/{Guid.NewGuid()}");
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NotFound);
        }
    }
}
