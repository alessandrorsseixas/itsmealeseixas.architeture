using Its.Me.AleSeixas.Example.Domain.Tests.Helpers;
using Its.Me.AleSeixas.Example.Domina.Entities;
using Its.Me.AleSeixas.Example.Repository.Context;
using Its.Me.AleSeixas.Example.Repository.SeedWorks;
using itsmealeseixas.architeture.utilities.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Its.Me.AleSeixas.Example.Repository.Test
{
    public class RepositoryTests
    {
        private readonly DbContextOptions<ApiDbContext> _options;

        public RepositoryTests()
        {
            // Configure the DbContext to use an in-memory database for testing
            _options = new DbContextOptionsBuilder<ApiDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task Should_Pass_GetAllAsync_ReturnsPersonals()
        {
            var context = new ApiDbContext(_options);

            UnitOfWork _unitOfWork = new UnitOfWork(context);
            var repository = _unitOfWork.GetRepository<Personal>();

            // Add test data
            var personals = TestsUtils.GetValidMockListPersonal();

            await repository.AddRangeAsync(personals);
            await _unitOfWork.SaveChanges();

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task Should_Pass_Add_Personal()
        {
            // Arrange
            var context = new ApiDbContext(_options);

            UnitOfWork _unitOfWork = new UnitOfWork(context);
            var repository = _unitOfWork.GetRepository<Personal>();

            // 
            Personal personal = new Personal()
            {
                Identifier = Guid.Parse("f97ff725-8e15-4bc0-8e86-9a4219018eca"),
                Document = "64558658044",
                Name = "San Nathsa Wulfwen",
                DateOfBirth = DateTime.Parse("10/05/1984"),
                CreateAt = UtilsHelpers.GetDatetime(),
                CreateAtUtc = UtilsHelpers.GetDatetimeUtc(),
                CreateBy = "Teste",
            };

            await repository.AddAsync(personal);
            await _unitOfWork.SaveChanges();

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public async Task Should_Pass_GetByID_Returns_One_Personal()
        {
            // Arrange
            var context = new ApiDbContext(_options);
            UnitOfWork _unitOfWork = new UnitOfWork(context);
            var repository = _unitOfWork.GetRepository<Personal>();


            Personal personal = new Personal()
            {
                Identifier = Guid.Parse("f97ff725-8e15-4bc0-8e86-9a4219018eca"),
                Document = "64558658044",
                Name = "San Nathsa Wulfwen",
                DateOfBirth = DateTime.Parse("10/05/1984"),
                CreateAt = UtilsHelpers.GetDatetime(),
                CreateAtUtc = UtilsHelpers.GetDatetimeUtc(),
                CreateBy = "Teste",
            };

            await repository.AddAsync(personal);
            await _unitOfWork.SaveChanges();

            // Act
            var result = await repository.GetByIdAsync(personal.Identifier);

            // Assert
            Assert.Equal(personal.Identifier, result.Identifier);
        }


        [Fact]
        public async Task Should_Pass_Update_Persoanl()
        {

            // Arrange
            var context = new ApiDbContext(_options);
            UnitOfWork _unitOfWork = new UnitOfWork(context);

            var repository = _unitOfWork.GetRepository<Personal>();


            // Add test data
            Personal personal = new Personal()
            {
                Identifier = Guid.Parse("f97ff725-8e15-4bc0-8e86-9a4219018eca"),
                Document = "64558658044",
                Name = "San Nathsa Wulfwen",
                DateOfBirth = DateTime.Parse("10/05/1984"),
                CreateAt = UtilsHelpers.GetDatetime(),
                CreateAtUtc = UtilsHelpers.GetDatetimeUtc(),
                CreateBy = "Teste",
            };

            await repository.AddAsync(personal);
            await _unitOfWork.SaveChanges();

            // Add test data
            personal.UpdateAt = DateTime.Now;
            personal.UpdateBy = "Teste Unitário";
            personal.UpdateAtUtc = DateTime.UtcNow;
            personal.Document = "82512105000194";

            await repository.UpdateAsync(personal);
            await _unitOfWork.SaveChanges();

            // Act
            var result = await repository.GetByIdAsync(personal.Identifier);

            // Assert
            Assert.Equal(personal.Document, result.Document);
        }

        [Fact]
        public async Task Should_Pass_Delete_Personal()
        {

            // Arrange
            var context = new ApiDbContext(_options);

            UnitOfWork _unitOfWork = new UnitOfWork(context);
            var repository = _unitOfWork.GetRepository<Personal>();


            Personal personal = new Personal()
            {
                Identifier = Guid.Parse("f97ff725-8e15-4bc0-8e86-9a4219018eca"),
                Document = "64558658044",
                Name = "San Nathsa Wulfwen",
                DateOfBirth = DateTime.Parse("10/05/1984"),
                CreateAt = UtilsHelpers.GetDatetime(),
                CreateAtUtc = UtilsHelpers.GetDatetimeUtc(),
                CreateBy = "Teste",
            };

            await repository.AddAsync(personal);
            await _unitOfWork.SaveChanges();


            await repository.DeleteAsync(personal);
            await _unitOfWork.SaveChanges();

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.Empty(result);
        }
    }
}
