using Its.Me.AleSeixas.Example.Domina.Entities;
using Its.Me.AleSeixas.Example.Repository.Context;
using Its.Me.AleSeixas.Example.Repository.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Its.Me.AleSeixas.Example.Repository.Test
{
    public class MappingsTeste
    {
        [Fact]
        public void PersonalMapping_ConfiguresPropertiesCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApiDbContext>() // Substitua YourDbContext pelo nome do seu DbContext
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;

            using (var context = new ApiDbContext(options)) // Substitua YourDbContext pelo nome do seu DbContext
            {
                var modelBuilder = new ModelBuilder(new ConventionSet());

                var sut = new PersonalMapping(); // Instância da classe de mapeamento

                // Act
                sut.Configure(modelBuilder.Entity<Personal>());

                // Assert
                var entityType = modelBuilder.Model.FindEntityType(typeof(Personal));
                var documentProperty = entityType.FindProperty(nameof(Personal.Document));
                var nameProperty = entityType.FindProperty(nameof(Personal.Name));

                // Verificar se a relação está configurada corretamente no contexto de mapeamento


                Assert.NotNull(documentProperty);
                Assert.Equal(100, documentProperty.GetMaxLength());

                Assert.NotNull(nameProperty);
                Assert.Equal(100, nameProperty.GetMaxLength());


            }
        }

        [Fact]
        public void CustomerMapping_ConfiguresPropertiesCorrectly()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApiDbContext>() // Substitua YourDbContext pelo nome do seu DbContext
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

            using (var context = new ApiDbContext(options)) // Substitua YourDbContext pelo nome do seu DbContext
            {
                var modelBuilder = new ModelBuilder(new ConventionSet());
                var sut = new CustomerMapping(); // Instância da classe de mapeamento

                // Act
                sut.Configure(modelBuilder.Entity<Customer>());

                // Assert
                var entityType = modelBuilder.Model.FindEntityType(typeof(Customer));
                var codeProperty = entityType.FindProperty(nameof(Customer.Code));
                var tokenProperty = entityType.FindProperty(nameof(Customer.Token));
                var personalNavigation = entityType.FindNavigation(nameof(Customer.Personal));

                Assert.NotNull(codeProperty);
                Assert.Equal(8, codeProperty.GetMaxLength());

                Assert.NotNull(tokenProperty);
                Assert.Equal(6, tokenProperty.GetMaxLength());

                Assert.NotNull(personalNavigation);
                Assert.Equal(nameof(Customer.Personal), personalNavigation.Name);

                // Verificar a chave estrangeira
                var foreignKey = entityType.FindForeignKeys(personalNavigation.ForeignKey.Properties);
                Assert.NotNull(foreignKey);

            }
        }


    }
}
