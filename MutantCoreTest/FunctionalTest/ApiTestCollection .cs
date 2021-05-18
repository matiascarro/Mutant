using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MutantCoreTest.FunctionalTest
{
    [CollectionDefinition(nameof(ApiTestCollection))]
    public class ApiTestCollection : ICollectionFixture<TestServerFixture>
    {

    }
}
