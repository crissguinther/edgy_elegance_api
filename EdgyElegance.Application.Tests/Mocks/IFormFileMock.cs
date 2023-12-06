using Microsoft.AspNetCore.Http;

namespace EdgyElegance.Application.Tests.Mocks;

internal static class IFormFileMock {
    public static Mock<IFormFile> GetMock() {
        var mock = new Mock<IFormFile>();
        
        return mock;
    }
}
