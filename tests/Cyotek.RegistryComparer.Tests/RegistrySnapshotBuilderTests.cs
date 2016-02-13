using NUnit.Framework;

namespace Cyotek.RegistryComparer.Tests
{
  [TestFixture]
  public class RegistrySnapshotBuilderTests : TestBase
  {
    #region Setup/Teardown

    [TearDown]
    public void TearDown()
    {
      this.DeleteBase();
    }

    #endregion

    #region  Tests

    [Test]
    [Ignore]
    public void TakeSnapshot_should_return_snapshot_of_single_key()
    {
      // arrange
      RegistrySnapshotBuilder target;
      RegistryKeySnapshot actual;
      RegistryKeySnapshot expected;

      target = new RegistrySnapshotBuilder();
      expected = this.LoadBaseSnapshot().
                      Keys[0];
      expected.Name = "Tests";

      this.CreateBase();

      // act
      actual = target.TakeSnapshot(this.FullRootKeyName);

      // assert
    }

    #endregion
  }
}
