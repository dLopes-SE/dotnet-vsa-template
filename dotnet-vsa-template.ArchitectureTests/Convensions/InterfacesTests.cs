using System.Reflection;

namespace finance_app.ArchitectureTests.Convensions
{
  public class InterfacesTests
  {
    [Fact]
    public void Should_start_with_I()
    {
      // Arrange
      var interfaceTypes = Assembly.Load("finance-app")
        .GetTypes()
        .Where(t => t.IsInterface);

      // Act
      var allStartWithI = interfaceTypes.All(t => t.Name.StartsWith('I'));

      // Assert
      Assert.True(allStartWithI);
    }
  }
}
