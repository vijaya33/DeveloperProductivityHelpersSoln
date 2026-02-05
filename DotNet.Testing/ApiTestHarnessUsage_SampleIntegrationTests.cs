//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;

//namespace DotNet.Testing
//{
//    internal class ApiTestHarnessUsage_SampleIntegrationTests
//    {
//    }
//}


// Below is sample tests code snippets  for using ApiTestHarness in an integration test project.
// It demonstrates how to set up a test class that uses the ApiFactory to create an HttpClient for making requests to the API being tested.

// This tests have been commented out to prevent any compilatioin errors. Uncomment them and customize them to your own API testing needs.  

/* 
using ApiTestHarness;
using Xunit;

public class ApiTests : IClassFixture<ApiFactory<Program>>
{
    private readonly HttpClient _client;

    public ApiTests(ApiFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Health_Returns_Ok()
    {
        var resp = await _client.GetAsync("/health");
        resp.EnsureSuccessStatusCode();
    }
}
*/ 
