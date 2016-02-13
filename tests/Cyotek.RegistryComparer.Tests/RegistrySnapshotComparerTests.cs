using Microsoft.Win32;
using NUnit.Framework;

namespace Cyotek.RegistryComparer.Tests
{
  [TestFixture]
  public class RegistrySnapshotComparerTests : TestBase
  {
    #region  Tests

    [Test]
    public void Compare_should_detect_deleted_value()
    {
      // arrange
      RegistrySnapshotComparer target;
      ChangeResult[] expected;
      ChangeResult[] actual;
      RegistrySnapshot lhs;
      RegistrySnapshot rhs;

      expected = new[]
                 {
                   new ChangeResult(ChangeType.Deletion, this.FullRootKeyName + "\\Alpha", "multi_string",
                                    RegistryValueKind.MultiString, "beta\ngamma", null)
                 };

      rhs = this.LoadBaseSnapshot();
      lhs = this.LoadDeletedValueSnapshot();

      target = new RegistrySnapshotComparer(lhs, rhs);

      // act
      actual = target.Compare();

      // assert
      CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void Compare_should_detect_missing_subkey()
    {
      // arrange
      RegistrySnapshotComparer target;
      ChangeResult[] expected;
      ChangeResult[] actual;
      RegistrySnapshot lhs;
      RegistrySnapshot rhs;

      expected = new[]
                 {
                   new ChangeResult(ChangeType.Deletion, this.FullRootKeyName + "\\Beta")
                 };

      rhs = this.LoadBaseSnapshot();
      lhs = this.LoadDeletedKeySnapshot();

      target = new RegistrySnapshotComparer(lhs, rhs);

      // act
      actual = target.Compare();

      // assert
      CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void Compare_should_detect_modified_value()
    {
      // arrange
      RegistrySnapshotComparer target;
      ChangeResult[] expected;
      ChangeResult[] actual;
      RegistrySnapshot lhs;
      RegistrySnapshot rhs;

      expected = new[]
                 {
                   new ChangeResult(ChangeType.Modification, this.FullRootKeyName + "\\Alpha", "string",
                                    RegistryValueKind.String, "alpha", "eta"),
                   new ChangeResult(ChangeType.Modification, this.FullRootKeyName + "\\Alpha", "expanded_string",
                                    RegistryValueKind.ExpandString, "%systemroot%\\delta", "%systemroot%\\theta")
                 };

      rhs = this.LoadBaseSnapshot();
      lhs = this.LoadChangedValueSnapshot();

      target = new RegistrySnapshotComparer(lhs, rhs);

      // act
      actual = target.Compare();

      // assert
      CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void Compare_should_detect_new_subkey()
    {
      // arrange
      RegistrySnapshotComparer target;
      ChangeResult[] expected;
      ChangeResult[] actual;
      RegistrySnapshot lhs;
      RegistrySnapshot rhs;

      expected = new[]
                 {
                   new ChangeResult(ChangeType.Insertion, this.FullRootKeyName + "\\Gamma")
                 };

      rhs = this.LoadBaseSnapshot();
      lhs = this.LoadInsertedKeySnapshot();

      target = new RegistrySnapshotComparer(lhs, rhs);

      // act
      actual = target.Compare();

      // assert
      CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void Compare_should_detect_new_value()
    {
      // arrange
      RegistrySnapshotComparer target;
      ChangeResult[] expected;
      ChangeResult[] actual;
      RegistrySnapshot lhs;
      RegistrySnapshot rhs;

      expected = new[]
                 {
                   new ChangeResult(ChangeType.Insertion, this.FullRootKeyName + "\\Alpha", "iota",
                                    RegistryValueKind.String, "kappa", null)
                 };

      rhs = this.LoadBaseSnapshot();
      lhs = this.LoadInsertedValueSnapshot();

      target = new RegistrySnapshotComparer(lhs, rhs);

      // act
      actual = target.Compare();

      // assert
      CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void Compare_should_return_no_results_when_snapshots_are_equal()
    {
      // arrange
      RegistrySnapshotComparer target;
      ChangeResult[] expected;
      ChangeResult[] actual;
      RegistrySnapshot lhs;
      RegistrySnapshot rhs;

      expected = new ChangeResult[0];

      rhs = this.LoadBaseSnapshot();
      lhs = this.LoadBaseSnapshot();

      target = new RegistrySnapshotComparer(lhs, rhs);

      // act
      actual = target.Compare();

      // assert
      CollectionAssert.AreEqual(expected, actual);
    }

    #endregion
  }
}
