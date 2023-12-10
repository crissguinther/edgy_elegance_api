using EdgyElegance.Application.Contracts.Persistence;
using EdgyElegance.Application.Interfaces;

namespace EdgyElegance.Application.Tests.Mocks;

public static class IUnitOfWorkMock {
    public static Mock<IUnitOfWork> GetMock() {
        Mock<IUnitOfWork> mock = new();

        // Repositories
        Mock<IGenderRepository> genderRepositoryMock = new();
        Mock<IProductRepository> productRepositoryMock = new();
        Mock<ICategoryRepository> categoryRepositoryMock = new();

        // Setup
        mock.Setup(m => m.CategoryRepository)
            .Returns(categoryRepositoryMock.Object);

        mock.Setup(m => m.GenderRepository)
            .Returns(genderRepositoryMock.Object);

        mock.Setup(m => m.ProductRepository)
            .Returns(productRepositoryMock.Object);

        return mock;
    }
}
