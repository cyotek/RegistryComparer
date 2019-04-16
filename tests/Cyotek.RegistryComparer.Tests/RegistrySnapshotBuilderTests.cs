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
    public void TakeSnapshot_should_return_snapshot_of_single_key()
    {
      // arrange
      RegistrySnapshotBuilder target;
      RegistryKeySnapshot snapshot;
      RegistryKeySnapshot compare;
      RegistrySnapshot lhs;
      RegistrySnapshot rhs;
      ChangeResult[] expected;
      ChangeResult[] actual;

      target = new RegistrySnapshotBuilder();
      compare = this.LoadBaseSnapshot().Keys[0];
      compare.Name = "Tests";
      lhs = new RegistrySnapshot();
      lhs.Keys.Add(compare);

      expected = new ChangeResult[0];

      this.CreateBase();

      // act
      snapshot = target.TakeSnapshot(this.FullRootKeyName);

      // assert
      rhs = new RegistrySnapshot();
      rhs.Keys.Add(snapshot);
      actual = new RegistrySnapshotComparer(lhs, rhs).Compare();
      CollectionAssert.AreEqual(expected, actual);
    }

    #endregion
  }
}
