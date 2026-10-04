using Soenneker.Threads.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Threads.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class ThreadsOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IThreadsOpenApiClientUtil _openapiclientutil;

    public ThreadsOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IThreadsOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
