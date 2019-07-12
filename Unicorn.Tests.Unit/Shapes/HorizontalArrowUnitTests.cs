﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Utility.Extensions;
using Tests.Utility.Providers;
using Unicorn.Interfaces;
using Unicorn.Shapes;

namespace Unicorn.Tests.Unit.Shapes
{
    [TestClass]
    public class HorizontalArrowUnitTests
    {
        private static Random _rnd = RandomProvider.Default;

        private HorizontalArrow GetArrow(HorizontalDirection? dir = null)
        {
            if (!dir.HasValue)
            {
                dir = _rnd.NextBoolean() ? HorizontalDirection.ToLeft : HorizontalDirection.ToRight;
            }
            return new HorizontalArrow(dir.Value, _rnd.NextDouble() * 100, _rnd.NextDouble() * 100, _rnd.NextDouble() * 100, _rnd.NextDouble() * 100, _rnd.NextDouble() * 100);
        }

        [TestMethod]
        public void HorizontalArrowClassConstructorFirstParameterSetsDirectionProperty()
        {
            HorizontalDirection testParam0 = _rnd.NextBoolean() ? HorizontalDirection.ToLeft : HorizontalDirection.ToRight;
            double testParam1 = _rnd.NextDouble() * 100;
            double testParam2 = _rnd.NextDouble() * 100;
            double testParam3 = _rnd.NextDouble() * 100;
            double testParam4 = _rnd.NextDouble() * 100;
            double testParam5 = _rnd.NextDouble() * 100;

            HorizontalArrow testObject = new HorizontalArrow(testParam0, testParam1, testParam2, testParam3, testParam4, testParam5);

            Assert.AreEqual(testParam0, testObject.Direction);
        }

        [TestMethod]
        public void HorizontalArrowClassConstructorSecondParameterSetsLengthProperty()
        {
            HorizontalDirection testParam0 = _rnd.NextBoolean() ? HorizontalDirection.ToLeft : HorizontalDirection.ToRight;
            double testParam1 = _rnd.NextDouble() * 100;
            double testParam2 = _rnd.NextDouble() * 100;
            double testParam3 = _rnd.NextDouble() * 100;
            double testParam4 = _rnd.NextDouble() * 100;
            double testParam5 = _rnd.NextDouble() * 100;

            HorizontalArrow testObject = new HorizontalArrow(testParam0, testParam1, testParam2, testParam3, testParam4, testParam5);

            Assert.AreEqual(testParam1, testObject.Length);
        }

        [TestMethod]
        public void HorizontalArrowClassConstructorSecondParameterSetsWidthProperty()
        {
            HorizontalDirection testParam0 = _rnd.NextBoolean() ? HorizontalDirection.ToLeft : HorizontalDirection.ToRight;
            double testParam1 = _rnd.NextDouble() * 100;
            double testParam2 = _rnd.NextDouble() * 100;
            double testParam3 = _rnd.NextDouble() * 100;
            double testParam4 = _rnd.NextDouble() * 100;
            double testParam5 = _rnd.NextDouble() * 100;

            HorizontalArrow testObject = new HorizontalArrow(testParam0, testParam1, testParam2, testParam3, testParam4, testParam5);

            Assert.AreEqual(testParam1, testObject.Width);
        }

        [TestMethod]
        public void HorizontalArrowClassConstructorThirdParameterSetsStemThicknessProperty()
        {
            HorizontalDirection testParam0 = _rnd.NextBoolean() ? HorizontalDirection.ToLeft : HorizontalDirection.ToRight;
            double testParam1 = _rnd.NextDouble() * 100;
            double testParam2 = _rnd.NextDouble() * 100;
            double testParam3 = _rnd.NextDouble() * 100;
            double testParam4 = _rnd.NextDouble() * 100;
            double testParam5 = _rnd.NextDouble() * 100;

            HorizontalArrow testObject = new HorizontalArrow(testParam0, testParam1, testParam2, testParam3, testParam4, testParam5);

            Assert.AreEqual(testParam2, testObject.StemThickness);
        }

        [TestMethod]
        public void HorizontalArrowClassConstructorFourthParameterSetsHeadBreadthProperty()
        {
            HorizontalDirection testParam0 = _rnd.NextBoolean() ? HorizontalDirection.ToLeft : HorizontalDirection.ToRight;
            double testParam1 = _rnd.NextDouble() * 100;
            double testParam2 = _rnd.NextDouble() * 100;
            double testParam3 = _rnd.NextDouble() * 100;
            double testParam4 = _rnd.NextDouble() * 100;
            double testParam5 = _rnd.NextDouble() * 100;

            HorizontalArrow testObject = new HorizontalArrow(testParam0, testParam1, testParam2, testParam3, testParam4, testParam5);

            Assert.AreEqual(testParam3, testObject.HeadBreadth);
        }

        [TestMethod]
        public void HorizontalArrowClassConstructorFourthParameterSetsHeightProperty()
        {
            HorizontalDirection testParam0 = _rnd.NextBoolean() ? HorizontalDirection.ToLeft : HorizontalDirection.ToRight;
            double testParam1 = _rnd.NextDouble() * 100;
            double testParam2 = _rnd.NextDouble() * 100;
            double testParam3 = _rnd.NextDouble() * 100;
            double testParam4 = _rnd.NextDouble() * 100;
            double testParam5 = _rnd.NextDouble() * 100;

            HorizontalArrow testObject = new HorizontalArrow(testParam0, testParam1, testParam2, testParam3, testParam4, testParam5);

            Assert.AreEqual(testParam3, testObject.Height);
        }

        [TestMethod]
        public void HorizontalArrowClassConstructorFifthParameterSetsHeadLengthProperty()
        {
            HorizontalDirection testParam0 = _rnd.NextBoolean() ? HorizontalDirection.ToLeft : HorizontalDirection.ToRight;
            double testParam1 = _rnd.NextDouble() * 100;
            double testParam2 = _rnd.NextDouble() * 100;
            double testParam3 = _rnd.NextDouble() * 100;
            double testParam4 = _rnd.NextDouble() * 100;
            double testParam5 = _rnd.NextDouble() * 100;

            HorizontalArrow testObject = new HorizontalArrow(testParam0, testParam1, testParam2, testParam3, testParam4, testParam5);

            Assert.AreEqual(testParam4, testObject.HeadLength);
        }

        [TestMethod]
        public void HorizontalArrowClassConstructorSixthParameterSetsHeadRakeProperty()
        {
            HorizontalDirection testParam0 = _rnd.NextBoolean() ? HorizontalDirection.ToLeft : HorizontalDirection.ToRight;
            double testParam1 = _rnd.NextDouble() * 100;
            double testParam2 = _rnd.NextDouble() * 100;
            double testParam3 = _rnd.NextDouble() * 100;
            double testParam4 = _rnd.NextDouble() * 100;
            double testParam5 = _rnd.NextDouble() * 100;

            HorizontalArrow testObject = new HorizontalArrow(testParam0, testParam1, testParam2, testParam3, testParam4, testParam5);

            Assert.AreEqual(testParam5, testObject.HeadRake);
        }

        [TestMethod]
        public void HorizontalArrowClassFlipMethodReturnsObjectWithDirectionPropertyToRightIfDirectionPropertyEqualsToLeft()
        {
            HorizontalArrow testObject = GetArrow(HorizontalDirection.ToLeft);

            HorizontalArrow testOutput = testObject.Flip();

            Assert.AreEqual(HorizontalDirection.ToRight, testOutput.Direction);
        }

        [TestMethod]
        public void HorizontalArrowClassFlipMethodReturnsObjectWithDirectionPropertyToLeftIfDirectionPropertyEqualsToRight()
        {
            HorizontalArrow testObject = GetArrow(HorizontalDirection.ToRight);

            HorizontalArrow testOutput = testObject.Flip();

            Assert.AreEqual(HorizontalDirection.ToLeft, testOutput.Direction);
        }

        [TestMethod]
        public void HorizontalArrowClassFlipMethodReturnsDifferentObject()
        {
            HorizontalArrow testObject = GetArrow();

            HorizontalArrow testOutput = testObject.Flip();

            Assert.AreNotSame(testOutput, testObject);
        }

        [TestMethod]
        public void HorizontalArrowClassFlipMethodReturnsObjectWithSameLength()
        {
            HorizontalArrow testObject = GetArrow();

            HorizontalArrow testOutput = testObject.Flip();

            Assert.AreEqual(testOutput.Length, testObject.Length);
        }

        [TestMethod]
        public void HorizontalArrowClassFlipMethodReturnsObjectWithSameStemThickness()
        {
            HorizontalArrow testObject = GetArrow();

            HorizontalArrow testOutput = testObject.Flip();

            Assert.AreEqual(testOutput.StemThickness, testObject.StemThickness);
        }

        [TestMethod]
        public void HorizontalArrowClassFlipMethodReturnsObjectWithSameHeadBreadth()
        {
            HorizontalArrow testObject = GetArrow();

            HorizontalArrow testOutput = testObject.Flip();

            Assert.AreEqual(testOutput.HeadBreadth, testObject.HeadBreadth);
        }

        [TestMethod]
        public void HorizontalArrowClassFlipMethodReturnsObjectWithSameHeadLength()
        {
            HorizontalArrow testObject = GetArrow();

            HorizontalArrow testOutput = testObject.Flip();

            Assert.AreEqual(testOutput.HeadLength, testObject.HeadLength);
        }

        [TestMethod]
        public void HorizontalArrowClassFlipMethodReturnsObjectWithSameHeadRake()
        {
            HorizontalArrow testObject = GetArrow();

            HorizontalArrow testOutput = testObject.Flip();

            Assert.AreEqual(testOutput.HeadRake, testObject.HeadRake);
        }

        [TestMethod]
        public void HorizontalArrowClassFlipMethodReturnsObjectWithSameWidth()
        {
            HorizontalArrow testObject = GetArrow();

            HorizontalArrow testOutput = testObject.Flip();

            Assert.AreEqual(testOutput.Width, testObject.Width);
        }

        [TestMethod]
        public void HorizontalArrowClassFlipMethodReturnsObjectWithSameHeight()
        {
            HorizontalArrow testObject = GetArrow();

            HorizontalArrow testOutput = testObject.Flip();

            Assert.AreEqual(testOutput.Height, testObject.Height);
        }

        [TestMethod]
        public void HorizontalArrowClassDrawAtMethodCallsDrawFilledPolygonMethodOfFirstParameter()
        {
            Mock<IGraphicsContext> mockGraphicsContext = new Mock<IGraphicsContext>();
            double testParam1 = _rnd.NextDouble() * 100;
            double testParam2 = _rnd.NextDouble() * 100;
            HorizontalArrow testObject = GetArrow();

            testObject.DrawAt(mockGraphicsContext.Object, testParam1, testParam2);

            mockGraphicsContext.Verify(c => c.DrawFilledPolygon(It.IsAny<IEnumerable<UniPoint>>()));
        }

        [TestMethod]
        public void HorizontalArrowClassDrawAtMethodCallsDrawFilledPolygonMethodOfFirstParameterWithNonNullParameter()
        {
            List<UniPoint> capturedPointList = null;
            Mock<IGraphicsContext> mockGraphicsContext = new Mock<IGraphicsContext>();
            mockGraphicsContext.Setup(c => c.DrawFilledPolygon(It.IsAny<IEnumerable<UniPoint>>())).Callback<IEnumerable<UniPoint>>(e => { capturedPointList = e.ToList(); });
            double testParam1 = _rnd.NextDouble() * 100;
            double testParam2 = _rnd.NextDouble() * 100;
            HorizontalArrow testObject = GetArrow();

            testObject.DrawAt(mockGraphicsContext.Object, testParam1, testParam2);

            Assert.IsNotNull(capturedPointList);
        }

        [TestMethod]
        public void HorizontalArrowClassDrawAtMethodCallsDrawFilledPolygonMethodOfFirstParameterWithParameterContainingPoints()
        {
            List<UniPoint> capturedPointList = null;
            Mock<IGraphicsContext> mockGraphicsContext = new Mock<IGraphicsContext>();
            mockGraphicsContext.Setup(c => c.DrawFilledPolygon(It.IsAny<IEnumerable<UniPoint>>())).Callback<IEnumerable<UniPoint>>(e => { capturedPointList = e.ToList(); });
            double testParam1 = _rnd.NextDouble() * 100;
            double testParam2 = _rnd.NextDouble() * 100;
            HorizontalArrow testObject = GetArrow();

            testObject.DrawAt(mockGraphicsContext.Object, testParam1, testParam2);

            Assert.IsTrue(capturedPointList.Count(p => p != null) != 0);
        }

        // We probably should not constrain the implementation too much by specifying the exact drawing style of an arrow by test, but it is worth providing
        // tests to confirm the arrow does not draw outside of its own self-declared bounding box
        //
        // These methods allow for the drawn arrow to project outside its bounding box _very slightly_ due to rounding errors.  When I say "very slightly": if the 
        // output was not enlarged, the amount of overdraw allowed would be on roughly the atomic scale.
        [TestMethod]
        public void HorizontalArrowClassDrawAtMethodCallsDrawFilledPolygonMethodWithNoPointsWithALowerXCoordinateThanTheSecondParameterToTheOriginalMethod()
        {
            List<UniPoint> capturedPointList = null;
            Mock<IGraphicsContext> mockGraphicsContext = new Mock<IGraphicsContext>();
            mockGraphicsContext.Setup(c => c.DrawFilledPolygon(It.IsAny<IEnumerable<UniPoint>>())).Callback<IEnumerable<UniPoint>>(e => { capturedPointList = e.ToList(); });
            double testParam1 = _rnd.NextDouble() * 100;
            double testParam2 = _rnd.NextDouble() * 100;
            HorizontalArrow testObject = GetArrow();

            testObject.DrawAt(mockGraphicsContext.Object, testParam1, testParam2);

            foreach (UniPoint point in capturedPointList)
            {
                Assert.IsTrue(point.X - testParam1 > -0.0000000001);
            }
        }

        [TestMethod]
        public void HorizontalArrowClassDrawAtMethodCallsDrawFilledPolygonMethodWithNoPointsWithALowerYCoordinateThanTheThirdParameterToTheOriginalMethod()
        {
            List<UniPoint> capturedPointList = null;
            Mock<IGraphicsContext> mockGraphicsContext = new Mock<IGraphicsContext>();
            mockGraphicsContext.Setup(c => c.DrawFilledPolygon(It.IsAny<IEnumerable<UniPoint>>())).Callback<IEnumerable<UniPoint>>(e => { capturedPointList = e.ToList(); });
            double testParam1 = _rnd.NextDouble() * 100;
            double testParam2 = _rnd.NextDouble() * 100;
            HorizontalArrow testObject = GetArrow();

            testObject.DrawAt(mockGraphicsContext.Object, testParam1, testParam2);

            foreach (UniPoint point in capturedPointList)
            {
                Assert.IsTrue(point.Y - testParam2 > -0.0000000001);
            }
        }

        [TestMethod]
        public void HorizontalArrowClassDrawAtMethodCallsDrawFilledPolygonMethodWithNoPointsWithAGreaterXCoordinateThanTheSecondParameterToTheOriginalMethodPlusTheArrowWidthProperty()
        {
            List<UniPoint> capturedPointList = null;
            Mock<IGraphicsContext> mockGraphicsContext = new Mock<IGraphicsContext>();
            mockGraphicsContext.Setup(c => c.DrawFilledPolygon(It.IsAny<IEnumerable<UniPoint>>())).Callback<IEnumerable<UniPoint>>(e => { capturedPointList = e.ToList(); });
            double testParam1 = _rnd.NextDouble() * 100;
            double testParam2 = _rnd.NextDouble() * 100;
            HorizontalArrow testObject = GetArrow();

            testObject.DrawAt(mockGraphicsContext.Object, testParam1, testParam2);

            foreach (UniPoint point in capturedPointList)
            {
                Assert.IsTrue(point.X - (testParam1 + testObject.Width) < 0.0000000001);
            }
        }

        [TestMethod]
        public void HorizontalArrowClassDrawAtMethodCallsDrawFilledPolygonMethodWithNoPointsWithAGreaterYCoordinateThanTheThirdParameterToTheOriginalMethodPlusTheArrowHeightProperty()
        {
            List<UniPoint> capturedPointList = null;
            Mock<IGraphicsContext> mockGraphicsContext = new Mock<IGraphicsContext>();
            mockGraphicsContext.Setup(c => c.DrawFilledPolygon(It.IsAny<IEnumerable<UniPoint>>())).Callback<IEnumerable<UniPoint>>(e => { capturedPointList = e.ToList(); });
            double testParam1 = _rnd.NextDouble() * 100;
            double testParam2 = _rnd.NextDouble() * 100;
            HorizontalArrow testObject = GetArrow();

            testObject.DrawAt(mockGraphicsContext.Object, testParam1, testParam2);

            foreach (UniPoint point in capturedPointList)
            {
                Assert.IsTrue(point.Y - (testParam2 + testObject.Height) < 0.0000000001);
            }
        }
    }
}
