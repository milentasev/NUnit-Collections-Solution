using NUnit.Framework;
using System;
using System.Linq;

namespace Collection.Tests
{
	[TestFixture]
	public class CollectionTests
	{
		[OneTimeSetUp]
		public void One_Time_Setup()
		{
			TestContext.Progress.WriteLine("All tests started " + DateTime.Now);
		}

		[Test]
		public void Test_Collection_EmptyConstructor()
		{
			var actual = new Collection<int>();

			Assert.AreEqual(0, actual.Count);
			Assert.AreEqual(16, actual.Capacity); 
		}

		[Test]
		public void Test_Collection_ConstructorSingleItem()
		{
			var actual = new Collection<int>(5);

			Assert.AreEqual(1, actual.Count);
			Assert.AreEqual(5, actual[0]);
		}

		[Test]
		public void Test_Collection_ConstructorMultipleItems() 
		{
			var actual = new Collection<int>(5, 6, 7);

			Assert.That(actual.Count, Is.EqualTo (3));
			Assert.That(actual.ToString, Is.EqualTo("[5, 6, 7]"));
		}

		[Test]
		public void Test_Collection_Add()
		{
			var actual = new Collection<int>();
			actual.Add(5);
			actual.Add(6);
			
			Assert.AreEqual(5, actual[0]);
			Assert.AreEqual(6, actual[1]);
		}

		[Test]
		public void Test_Collection_AddWithGrow()
		{
			int[] nums = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

			var colection = new Collection<int>(nums);
			colection.Add(16);

			Assert.AreEqual(16, colection[15]);
			Assert.AreEqual(nums.Length + 1,colection.Count);
			Assert.AreEqual(nums.Length * 2, colection.Capacity);
		}

		[Test]
		public void Test_Collection_AddRange()
		{
			int[] numbers = new int[] { 1, 2, 3, 4, 5 };
			int[] addNumbers = new int[] { 6, 7, 8 };

			var colection = new Collection<int>(numbers);
			colection.AddRange(addNumbers);

			Assert.AreEqual(6, colection[5]);
			Assert.AreEqual(8, colection[7]);
			Assert.AreEqual(numbers.Length + addNumbers.Length, colection.Count);
		}

		[Test]
		public void Test_Collection_GetByIndex() 
		{
			var names = new Collection<string>("Ivan", "Maria");

			var firstItem = names[0];
			var secondItem = names[1];

			Assert.AreEqual("Ivan", firstItem);
			Assert.AreEqual("Maria", secondItem);
		}

		[Test]
		public void Test_Collection_GetByInvalidIndex()
		{
			var names = new Collection<string>("John", "Peter");

			Assert.That(() => { var name = names[-1]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
			Assert.That(() => { var name = names[2]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
		}
		
		[Test]
		public void Test_Collection_SetByIndex()
		{
			int[] nums = {2, 4, 5, 8 };
			int insertNum = 5;

			var collection = new Collection<int>(nums);

			collection.InsertAt(1, insertNum);

			Assert.AreEqual(insertNum, collection[1]);
		}

		[Test]
		public void Test_Colection_SetByInvalidIndex()
		{
			int[] nums = {2, 4, 5, 8 };
			int insertNum = 5;

			var collection = new Collection<int>(nums);

			Assert.That(() => collection.InsertAt(-1, insertNum), Throws.InstanceOf<ArgumentOutOfRangeException>());
			Assert.That(() => collection.InsertAt(17, insertNum), Throws.InstanceOf<ArgumentOutOfRangeException>());
		}

		[Test]
		public void Test_Collection_AddRangeWithGrow()
		{
			var nums = new Collection<int>();
			int oldCapacity = nums.Capacity;

			var newNums = Enumerable.Range(1000, 2000).ToArray();

			nums.AddRange(newNums);

			string expectedNums = "[" + string.Join(", ", newNums) + "]";

			Assert.AreEqual(expectedNums, nums.ToString());
			Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
			Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
		}

		[Test]
		public void Test_Collection_InsertAtStart() 
		{
			var numbers = new int[] {1, 2, 3, 4, 5 };
			int insertNumber = 0;

			var collection = new Collection<int>(numbers);
			collection.InsertAt(0, insertNumber);

			Assert.AreEqual(insertNumber, collection[0]);
		}

		[Test]
		public void Test_Collection_InsertAtEnd()
		{
			var numbers = new int[] { 1, 2, 3, 4, 5 };
			int insertNumber = 0;

			var collection = new Collection<int>(numbers);
			collection.InsertAt(numbers.Length, insertNumber);

			Assert.AreEqual(insertNumber, collection[numbers.Length]);
		}
		
		[Test]
		public void Test_Collection_InsertAtMiddle() 
		{
			var numbers = new int[] {1, 2, 3, 4, 5 };
			int insertNumber = 0;

			var collection = new Collection<int>(numbers);
			collection.InsertAt(numbers.Length / 2, insertNumber);

			Assert.AreEqual(insertNumber, collection[numbers.Length / 2 ]);
		}

		[Test]
		public void Test_Collection_InserAtWithGrow()
		{
			var numbers = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
			int insertNum = 16;

			var collection = new Collection<int>(numbers);
			collection.InsertAt(numbers.Length, insertNum);

			Assert.AreEqual(insertNum, collection[15]);
			Assert.AreEqual(numbers.Length * 2, collection.Capacity);
		}

		[Test]
		public void Test_Collection_InsertAtInvalidIndex()
		{
			var numbers = new int[] {1, 2, 3, 4, 5 };
			int insertNumber = 6;

			var collection = new Collection<int>(numbers);

			Assert.That(() => collection.InsertAt(-1, insertNumber), Throws.TypeOf<ArgumentOutOfRangeException>());
			Assert.That(() => collection.InsertAt(17, insertNumber), Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[Test]
		public void Test_Collection_ExchangeMiddle() 
		{
			var numbers = new int[] {1, 2, 3, 4, 5, 6 };

			var collection = new Collection<int>(numbers);

			collection.Exchange((numbers.Length / 2) - 1, numbers.Length / 2);

			Assert.AreEqual(numbers[numbers.Length / 2], collection[(numbers.Length / 2) - 1]);
			Assert.AreEqual(numbers[(numbers.Length / 2) - 1], collection[numbers.Length / 2]);
		}

		[Test]
		public void Test_Collection_ExchangeFirstLast() 
		{
			var numbers = new int[] { 1, 2, 3, 4, 5, 6 };

			var collection = new Collection<int>(numbers);
			collection.Exchange(0, numbers.Length - 1);

			Assert.AreEqual(numbers[0], collection[numbers.Length - 1]);
			Assert.AreEqual(numbers[numbers.Length - 1], collection[0]);
		}

		[Test]
		public void Test_Collection_ExchangeInvalidIndexes() 
		{
			var numbers = new int[] {1, 2, 3, 4, 5, 6 };
			var collection = new Collection<int>(numbers);

			Assert.That(() => collection.Exchange(-1, 6), Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[Test]
		public void Test_Collection_RemoveAtStart() 
		{
			var numbers = new int[] { 1, 2, 3, 4, 5, 6 };
			var collection = new Collection<int>(numbers);

			collection.RemoveAt(0);

			Assert.AreEqual(numbers[1], collection[0]);
		}

		[Test]
		public void Test_Collection_RemoveAtEnd() 
		{
			var numbers = new int[] {1, 2, 3, 4, 5, 6 };
			var collection = new Collection<int>(numbers);

			collection.RemoveAt(numbers.Length - 1);

			Assert.AreEqual(numbers.Length, collection.Count + 1);
		}

		[Test]
		public void Test_Collection_RemoveAtMiddle() 
		{
			var numbers = new int[] {1, 2 ,3 ,4 ,5, 6 }; 
			var collection = new Collection<int>(numbers);

			collection.RemoveAt(numbers.Length / 2 - 1);

			Assert.That(collection.ToString, Does.Not.Contain(3)); 
		}

		[Test]
		public void Test_Collection_RemoveAtInvalidIndex() 
		{
			var numbers = new int[] {1, 2, 3, 4, 5, 6 };
			var collection = new Collection<int>(numbers);

			Assert.That(()=> collection.RemoveAt(-1),Throws.TypeOf<ArgumentOutOfRangeException>());
			Assert.That(() => collection.RemoveAt(6), Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[Test]
		public void Test_Collection_RemoveAll() 
		{
			var numbers = new int[] {1, 2, 3, 4, 5, 6 };
			
			var collection = new Collection<int>(numbers);

			for (int i = collection.Count - 1; i >= 0; i--)
			{
				collection.RemoveAt(i);
			}

			Assert.AreEqual(0, collection.Count);
		}

		[Test]
		public void Test_Collection_Clear() 
		{
			var numbers = new int[] {1, 2, 3, 4, 5, 6 };
			var collection = new Collection<int>(numbers);

			collection.Clear();
			
			Assert.AreEqual(0, collection.Count);
		}

		[Test]
		public void Test_Collection_CountAndCapacity()
		{
			var numbers = new int[] { 1, 2, 3, 4, 5, 6 };
			var collection = new Collection<int>(numbers);
			var expectedCount = numbers.Length;
			var expectedCapacity = 16;

			Assert.AreEqual(expectedCount, collection.Count);
			Assert.AreEqual(expectedCapacity, collection.Capacity);
		}

		[Test]
		public void Test_Collection_ToStringEmpty() 
		{
			var collection = new Collection<int>();

			Assert.That(collection.ToString(),Is.EqualTo("[]"));
		}

		[Test]
		public void Test_Collection_ToStringSingle() 
		{
			var collection = new Collection<int>(1);
			
			Assert.That(collection.ToString(), Is.EqualTo("[1]"));
		}

		[Test]
		public void Test_Collection_ToStringMultiple() 
		{
			var collection = new Collection<int>(1, 2, 3);
			
			Assert.AreEqual(collection.ToString(), "[1, 2, 3]");
		}
		[Test]
		public void Test_Collection_ToStringNestedCollections() 
		{
			var names = new Collection<string>("Peter", "Maria");
			var numbers = new Collection<int>(1, 2, 3);
			var dates = new Collection<DateTime>();

			var collection = new Collection<object>(names, numbers, dates);

			Assert.That(collection.ToString(), Is.EqualTo("[[Peter, Maria], [1, 2, 3], []]"));
		}

		[Test]
		[Timeout(1000)]
		public void Test_Collection_OneMillionItems()
		{
			const int itemsCount = 1000000;
			var collection = new Collection<int>();
			
			collection.AddRange(Enumerable.Range(1, itemsCount).ToArray());

			Assert.That(collection.Count, Is.EqualTo(itemsCount));
			Assert.That(collection.Capacity, Is.GreaterThanOrEqualTo(collection.Count));

			for (int i = itemsCount - 1; i >= 0; i--)
			{
				collection.RemoveAt(i);
			}

			Assert.That(collection.ToString(), Is.EqualTo("[]"));
			Assert.That(collection.Capacity, Is.GreaterThanOrEqualTo(collection.Count));
		}

		[Test]
		public void Test_Collection_Set_WithIndex()
		{
			var collection = new Collection<int>(1, 2, 3);

			collection[2] = 4;

			Assert.AreEqual(4, collection[2]);
		}

		[TestCase("Peter", 0, "Peter")]
		[TestCase("Peter, Maria, George", 0, "Peter")]
		[TestCase("Peter, Maria, George", 1, "Maria")]
		[TestCase("Peter, Maria, George", 2, "George")]
		public void Test_Collection_GetByValidIndex(
			string data, int index, string expectedValue)
		{
			var nums = new Collection<string>(data.Split(", "));
			var actual = nums[index];

			Assert.AreEqual(expectedValue, actual);
		}

		[TestCase("", 0)]
		[TestCase("Peter", -1)]
		[TestCase("Peter", 1)]
		[TestCase("Peter, Maria, Steave", -1)]
		[TestCase("Peter, Maria, Steave", 3)]
		public void Test_Collection_GetByInvalidIndex(
			string data, int index)
		{
			var nums = new Collection<string>(data.Split(", ", StringSplitOptions.RemoveEmptyEntries));

			Assert.That(()=>nums[index], Throws.TypeOf<ArgumentOutOfRangeException>());
		}

		[OneTimeTearDown]
		public void One_Time_TearDown()
		{
			TestContext.Progress.WriteLine("All tests ended " + DateTime.Now);
		}
	}
}