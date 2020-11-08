using System;
using System.Linq;
using Dosh.Middleware.DB;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test_Dosh.Middleware.DB.Middleware.Base
{
    [TestClass]
    public class Test_Record
    {
        [TestMethod]
        public void TestAdd()
        {
            // setup
            var record = new Record();
            var expected = "test_value";
            record.Add(expected);

            // assert
            record[0].Is(expected);
        }

        [TestMethod]
        public void TestAutoEnsureCapacity()
        {
            // setup
            var capacity = 10;
            var record = new Record(capacity);
            var testCases = createMockRecord(10);

            foreach (var tc in testCases)
            {
                record.Add(tc);
            }
        }

        [TestMethod]
        public void TestIndexerGetAndSet()
        {
            // setup
            var record = createMockRecord(1);

            // assert
            var updateValue = "update_value";
            record[0] = updateValue;
            record[0].Is(updateValue);
        }

        [TestMethod]
        public void TestOutOfRangeIndexerAccess()
        {
            // setup
            var record = createMockRecord(2);

            // assert
            AssertEx.Catch<ArgumentOutOfRangeException>(() => { var tmp = record[3]; });
        }

        [TestMethod]
        public void TestCheckCount()
        {
            // setup
            var record = createMockRecord(21);

            record.Count.Is(21);
        }

        [TestMethod]
        public void TestCapacityCheckAndUpdate()
        {
            // setup
            var record = createMockRecord();

            // assert default capacity
            record.Capacity.Is(0);

            // update capacity
            record.Capacity = 15;
            record.Capacity.Is(15);
        }

        [TestMethod]
        public void TestReadonlyCheck()
        {
            var record = createMockRecord();
            record.IsReadOnly.IsFalse();
        }

        [TestMethod]
        public void TestClearAndReference()
        {
            // setup
            var record = createMockRecord(20);
            record.Clear();

            // assert
            record.Count.Is(0);
            record.Any().IsFalse();
        }

        [TestMethod]
        public void TestContainsCheck()
        {
            // setup
            var record = createMockRecord(10);

            // assert exists
            var existsVal = "test9";
            record.Contains(existsVal).IsTrue();

            // assert not exists
            var notExistsVal = "test10";
            record.Contains(notExistsVal).IsFalse();
        }

        [TestMethod]
        public void TestCopyToArray()
        {
            // setup
            var record = createMockRecord(4);

            var copyArr = new string[4];
            record.CopyTo(copyArr, 0);

            for (var i = 0; i < copyArr.Count(); i++)
            {
                copyArr[i].Is($"test{i}");
            }
        }

        [TestMethod]
        public void TestIndexOfAndRemoveAt()
        {
            // setup
            var record = createMockRecord(10);

            var checkItem = "test4";
            var actualIndex = record.IndexOf(checkItem);
            actualIndex.Is(4);
            record.Contains(checkItem).IsTrue();

            record.RemoveAt(actualIndex);
            record.Contains(checkItem).IsFalse();
        }

        [TestMethod]
        public void TestRemoveAtOutOfRange()
        {
            // setup
            var record = createMockRecord(10);
            
            AssertEx.Catch<ArgumentOutOfRangeException>(() => { record.RemoveAt(10); }); 
        }

        [TestMethod]
        public void TestRemoveItem()
        {
            // setup
            var record = createMockRecord(10);

            var existItem = "test9";
            var notExistItem = "test10";

            record.Remove(existItem).IsTrue();
            record.Remove(notExistItem).IsFalse();
        }

        [TestMethod]
        public void TestGetEnumeratorAndAccessAll()
        {
            // setup
            var record = createMockRecord(10);
            var enumerator = record.GetEnumerator();

            var cnt = 0;
            while (enumerator.MoveNext())
            {
                enumerator.Current.Is($"test{cnt}");
                cnt++;
            }
        }

        [TestMethod]
        public void TestInsertCheck()
        {
            // setup
            var record = createMockRecord(10);

            var insertValue = "insert_value";
            var insertIndex = 5;

            record.Insert(insertIndex, insertValue);
            record[insertIndex].Is(insertValue);
            record.Count.Is(11);
        }

        /// <summary>
        /// Create a Mock instance for testing.
        /// </summary>
        /// <param name="recordCnt">The number of pre-set elements</param>
        /// <returns>Record instance.</returns>
        private Record createMockRecord(int recordCnt = 0)
        {
            if (recordCnt == 0)
            {
                return new Record();
            }

            var record = new Record();
            for (var i = 0; i < recordCnt; i++)
            {
                record.Add($"test{i}");
            }
            return record;
        }
    }
}
