using MinimalApi.Domain.Entities;

namespace Test.Domain.Entities;

[TestClass]
public class AdminTest
{
    [TestMethod]
    public void TestPropsAdmin()
    {
        var password = BCrypt.Net.BCrypt.HashPassword("password");
        
        var admin = new Admin
        {
            Id = 1,
            Email = "email@email.com",
            Password = password
        };
        
        Assert.AreEqual(admin.Id, 1);
        Assert.AreEqual("email@email.com", admin.Email);
        Assert.IsTrue(BCrypt.Net.BCrypt.Verify("password", admin.Password));
    }
}