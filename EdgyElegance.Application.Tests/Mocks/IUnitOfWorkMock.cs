using EdgyElegance.Application.Contracts.Persistence;
using EdgyElegance.Application.Interfaces;

namespace EdgyElegance.Application.Tests.Mocks;

public static class IUnitOfWorkMock {
    public static Mock<IUnitOfWork> GetMock() {
        Mock<IUnitOfWork> mock = new();

        // Repositories
        Mock<IGenderRepository> genderRepositoryMock = new();

        // Setup
        mock.Setup(m => m.GenderRepository)
            .Returns(genderRepositoryMock.Object);

        return mock;
    }
}
