using MutantWebApp.Resource;
using MutantCoreTest.FunctionalTest.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Data.Edm.Csdl;

namespace MutantCoreTest.FunctionalTest
{
    [Collection(nameof(ApiTestCollection))]
    public class FunctionalTest : BaseTest
    {
        private static double RATIO_EXPECTED = 0.5;
        private static int MUTANTEXPECTED = 1;
        private static int HUMANEXPECTED = 1;
        public FunctionalTest(TestServerFixture testServerFixture) : base(testServerFixture)
        {
        }

        [Fact]
        public async Task PostEmptyMutant_Should_Return_400_From_ServiceValidation()
        {
            MutantResourceForPost resource = new MutantResourceForPost();
            var response = await HttpClient.PostAsync(requestUri: MutantRoutes.Root, model: resource);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PostMutant_Should_Return_200_From_ServiceValidation()
        {
            MutantResourceForPost resource = new MutantResourceForPost
            {
                Dna = new string[]{ "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTA" }
            };
            var response = await HttpClient.PostAsync(requestUri: MutantRoutes.Root, model: resource);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task PostHuman_Should_Return_200_From_ServiceValidation()
        {
            MutantResourceForPost resource = new MutantResourceForPost
            {
                Dna = new string[] { "ATGCGA", "CAGTGC", "TTATTT", "AGACGG", "GCGTCA", "TCACTG" }
            };
            var response = await HttpClient.PostAsync(requestUri: MutantRoutes.Root, model: resource);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetStats_Should_Return_200_From_ServiceValidation()
        {
            var response = await HttpClient.GetAsync(requestUri: MutantRoutes.Stats);
            var mutantStatsResult = response.Content.GetAsync<MutantStatsResourceForGet>().Result;

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(RATIO_EXPECTED, mutantStatsResult.ratio);
            Assert.Equal(HUMANEXPECTED, mutantStatsResult.count_human_dna);
            Assert.Equal(MUTANTEXPECTED, mutantStatsResult.count_mutant_dna);
        }
    }
}
