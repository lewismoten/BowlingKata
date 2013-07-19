using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using BowlingCada;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingCada_UnitTest
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class UnitTest1
	{
		public UnitTest1()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get { return testContextInstance; }
			set { testContextInstance = value; }
		}

		#region Additional test attributes

		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//

		#endregion

		[TestMethod]
		public void ValidatePropertiesPersistValuesStored()
		{
			Frame frame = new Frame();
			frame.Score1 = 4;
			frame.Score2 = 6;


			Assert.AreEqual(10, frame.Total);
			Assert.AreEqual(4, frame.Score1);
			Assert.AreEqual(6, frame.Score2);
		}

		[TestMethod]
		public void TheNextFrameIsRetrieved()
		{
			var nextFrame = new Frame();

			Frame frame = new Frame();
			frame.Next = nextFrame;

			Assert.AreEqual(nextFrame, frame.Next);
		}

		[TestMethod]
		public void CalculateScoreForPins()
		{
			var frame = new Frame();
			frame.Score1 = 10;
			frame.Score2 = null;

			Assert.AreEqual(10, frame.Total);

			var frame2 = new Frame();
			frame2.Score1 = 4;
			frame2.Score2 = 5;
			frame.Next = frame2;

			Assert.AreEqual(19, frame.Total);

			frame2 = new Frame();
			frame2.Score1 = 10;
			frame2.Score2 = null;
			frame.Next = frame2;

			Assert.AreEqual(20, frame.Total);

			var frame3 = new Frame();
			frame3.Score1 = 1;
			frame3.Score2 = 2;
			frame2.Next = frame3;

			Assert.AreEqual(21, frame.Total);
		}

		[TestMethod]
		public void GrandTotal()
		{
			var frame3 = new Frame
			                	{
			                		Score1 = 1,
			                		Score2 = 2
			        
								};
			var frame2 = new Frame
			                	{
			                		Score1 = 10,
									
								};
			var frame1 = new Frame
			             	{
			             		Score1 = 10,
			             	};

			frame2.Previous = frame1;
			frame3.Previous = frame2;

			frame1.Next = frame2;
			frame2.Next = frame3;

			Assert.AreEqual(37, frame3.GetGrandTotal());
		}

		[TestMethod]
		public void Blah()
		{
			var frame1 = new Frame {Score1 = 5, Score2 = 5, FrameNo = 1};
			var frame9 = frame1.Add(5, 5).Add(5, 5).Add(5, 5).Add(5, 5).Add(5, 5).Add(5, 5).Add(5, 5).Add(5, 5);
			var lastFrame = new LastFrame
			                	{
			                		Score1 = 5,
			                		Score2 = 5,
			                		Score3 = 5,
			                		Previous = frame9
			                	};
			frame9.Next = lastFrame;

			Assert.AreEqual(150, lastFrame.GetGrandTotal());
		}

		public HeadTail CreateGame(params Scores[] scores)
		{
			//    new HeadTail
			//        {
			//            Head = new Frame
			//                    {
			//                        Score1 = scores[0].Score1,
			//                        Score2 = scores[0].Score2
			//                    },
			//            Tail = new Frame
			//                    {
			//                        Score1 = scores[scores.Length - 1].Score1,
			//                        Score2 = scores[scores.Length - 1].Score2

			//                    }
			//        };


			//    for(int i = 0; i < scores.Length-1; i++)
			//    {
			//        var frame = new Frame();
			//    }
			//}
			return null;
		}




		[TestMethod]
		public void Blah2()
		{
			var frame1 = new Frame();
			frame1.Score1 = 10;
			
			var frame2 = new Frame();
			frame2.Score1 = 10;
			
			var frame3 = new Frame();
			frame3.Score1 = 4;
			frame3.Score2 = 6;
			var frame4 = new Frame();
			frame4.Score1 = 1;
			frame4.Score2 = 7;
			

			frame1.Next = frame2;
			frame2.Next = frame3;
			frame3.Next = frame4;
			

			frame2.Previous = frame1;
			frame3.Previous = frame2;
			frame4.Previous = frame3;
			

			Assert.AreEqual(63, frame4.GetGrandTotal());
		}

		[TestMethod]
		public void ValidateFrameAdditions()
		{
			Game g = new Game();
			g.AddFrame(10, null);
			g.AddFrame(10, null);
			g.AddFrame(4, 6);
			g.AddFrame(1, 7);
			

			Assert.AreEqual(4, g.Tail.FrameNo);
			Assert.AreEqual(63, g.Tail.GetGrandTotal());
		}
	}

	public class Scores
	{
		public int? Score1 { get; set; }
		public int? Score2 { get; set; }
	}

	public class HeadTail
	{
		public Frame Head { get; set; }
		public Frame Tail { get; set; }
	}
}