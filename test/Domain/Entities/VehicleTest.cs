using MinimalApi.Domain.Entities;

namespace Test.Domain.Entities;

[TestClass]
public class VehicleTest
{
    [TestMethod]
    public void TestVehicleProps()
    {
        var vehicle = new Vehicle
        {
            Id = 1,
            Model = "Uno",
            Brand = "Fiat",
            Year = 2003
        };
        
        Assert.AreEqual("Uno", vehicle.Model);
        Assert.AreEqual("Fiat", vehicle.Brand);
        Assert.AreEqual(2003, vehicle.Year);
    }
}