﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using Tests.Utility.Providers;

namespace Timetabler.CoreData.Tests.Unit
{
    [TestClass]
    public class TimeOfDayUnitTests
    {
        private static readonly Random _rnd = RandomProvider.Default;

        [TestMethod]
        public void TimeOfDayClass_ConstructorWithDoubleParameter_ReturnsTimeOfDayWithCorrectValue()
        {
            int testValue = _rnd.Next();
            double testParam = testValue + _rnd.NextDouble() - 0.5;

            TimeOfDay testOutput = new TimeOfDay(testParam);

            Assert.AreEqual(testValue, testOutput.AbsoluteSeconds);
        }

        [TestMethod]
        public void TimeOfDayClass_SubtractOperatorWithTimeOfDayAndTimeOfDayParameter_ReturnsTimeSpanWithCorrectValueIfFirstParameterIsBeforeSecondParameter()
        {
            int testSeconds0 = _rnd.Next(86399);
            int testSeconds1 = _rnd.Next(86400 - testSeconds0) + testSeconds0 + 1;
            TimeOfDay testValue0 = new TimeOfDay(testSeconds0);
            TimeOfDay testValue1 = new TimeOfDay(testSeconds1);

            TimeSpan testOutput = testValue1 - testValue0;

            Assert.AreEqual(testSeconds1 - testSeconds0, (int)testOutput.TotalSeconds);
        }

        [TestMethod]
        public void TimeOfDayClass_SubtractOperatorWithTimeOfDayAndTimeOfDayParameter_ReturnsTimeSpanWithCorrectValueIfFirstParameterIsAfterSecondParameter()
        {
            int testSeconds1 = _rnd.Next(86399);
            int testSeconds0 = _rnd.Next(86400 - testSeconds1) + testSeconds1 + 1;
            TimeOfDay testValue0 = new TimeOfDay(testSeconds0);
            TimeOfDay testValue1 = new TimeOfDay(testSeconds1);

            TimeSpan testOutput = testValue1 - testValue0;

            Assert.AreEqual(testSeconds1 - testSeconds0, (int)testOutput.TotalSeconds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TimeOfDayClass_SubtractOperatorWithTimeOfDayAndTimeOfDayOperands_ThrowsArgumentNullExceptionIfSecondOperandIsNull()
        {
            int testSeconds1 = _rnd.Next(86399);
            TimeOfDay testValue0 = null;
            TimeOfDay testValue1 = new TimeOfDay(testSeconds1);

            _ = testValue1 - testValue0;

            Assert.Fail();
        }

        [TestMethod]
        public void TimeOfDayClass_SubtractOperatorWithTimeOfDayAndTimeOfDayOperands_ThrowsArgumentNullExceptionWithParamNameEqualToT2IfSecondOperandIsNull()
        {
            int testSeconds1 = _rnd.Next(86399);
            TimeOfDay testValue0 = null;
            TimeOfDay testValue1 = new TimeOfDay(testSeconds1);

            try
            {
                _ = testValue1 - testValue0;
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("t2", ex.ParamName);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TimeOfDayClass_SubtractOperatorWithTimeOfDayAndTimeOfDayOperands_ThrowsArgumentNullExceptionIfFirstOperandIsNull()
        {
            int testSeconds1 = _rnd.Next(86399);
            int testSeconds0 = _rnd.Next(86400 - testSeconds1) + testSeconds1 + 1;
            TimeOfDay testValue0 = new TimeOfDay(testSeconds0);
            TimeOfDay testValue1 = null;

            _ = testValue1 - testValue0;

            Assert.Fail();
        }

        [TestMethod]
        public void TimeOfDayClass_SubtractOperatorWithTimeOfDayAndTimeOfDayOperands_ThrowsArgumentNullExceptionWithParamNameEqualToT1IfFirstOperandIsNull()
        {
            int testSeconds1 = _rnd.Next(86399);
            int testSeconds0 = _rnd.Next(86400 - testSeconds1) + testSeconds1 + 1;
            TimeOfDay testValue0 = new TimeOfDay(testSeconds0);
            TimeOfDay testValue1 = null;

            try
            {
                _ = testValue1 - testValue0;
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("t1", ex.ParamName);
            }
        }

        [TestMethod]
        public void TimeOfDayClass_SubtractMethodWithTimeOfDayAndTimeOfDayParameter_ReturnsTimeSpanWithCorrectValueIfFirstParameterIsBeforeSecondParameter()
        {
            int testSeconds0 = _rnd.Next(86399);
            int testSeconds1 = _rnd.Next(86400 - testSeconds0) + testSeconds0 + 1;
            TimeOfDay testValue0 = new TimeOfDay(testSeconds0);
            TimeOfDay testValue1 = new TimeOfDay(testSeconds1);

            TimeSpan testOutput = TimeOfDay.Subtract(testValue1, testValue0);

            Assert.AreEqual(testSeconds1 - testSeconds0, (int)testOutput.TotalSeconds);
        }

        [TestMethod]
        public void TimeOfDayClass_SubtractMethodWithTimeOfDayAndTimeOfDayParameters_ReturnsTimeSpanWithCorrectValueIfFirstParameterIsAfterSecondParameter()
        {
            int testSeconds1 = _rnd.Next(86399);
            int testSeconds0 = _rnd.Next(86400 - testSeconds1) + testSeconds1 + 1;
            TimeOfDay testValue0 = new TimeOfDay(testSeconds0);
            TimeOfDay testValue1 = new TimeOfDay(testSeconds1);

            TimeSpan testOutput = TimeOfDay.Subtract(testValue1, testValue0);

            Assert.AreEqual(testSeconds1 - testSeconds0, (int)testOutput.TotalSeconds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TimeOfDayClass_SubtractMethodWithTimeOfDayAndTimeOfDayParameters_ThrowsArgumentNullExceptionIfSecondOperandIsNull()
        {
            int testSeconds1 = _rnd.Next(86399);
            TimeOfDay testValue0 = null;
            TimeOfDay testValue1 = new TimeOfDay(testSeconds1);

            _ = TimeOfDay.Subtract(testValue1, testValue0);

            Assert.Fail();
        }

        [TestMethod]
        public void TimeOfDayClass_SubtractMethodWithTimeOfDayAndTimeOfDayParameters_ThrowsArgumentNullExceptionWithParamNameEqualToT2IfSecondOperandIsNull()
        {
            int testSeconds1 = _rnd.Next(86399);
            TimeOfDay testValue0 = null;
            TimeOfDay testValue1 = new TimeOfDay(testSeconds1);

            try
            {
                _ = TimeOfDay.Subtract(testValue1, testValue0);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("t2", ex.ParamName);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TimeOfDayClass_SubtractMethodWithTimeOfDayAndTimeOfDayOperands_ThrowsArgumentNullExceptionIfFirstOperandIsNull()
        {
            int testSeconds1 = _rnd.Next(86399);
            int testSeconds0 = _rnd.Next(86400 - testSeconds1) + testSeconds1 + 1;
            TimeOfDay testValue0 = new TimeOfDay(testSeconds0);
            TimeOfDay testValue1 = null;

            _ = TimeOfDay.Subtract(testValue1, testValue0);

            Assert.Fail();
        }

        [TestMethod]
        public void TimeOfDayClass_SubtractMethodWithTimeOfDayAndTimeOfDayOperands_ThrowsArgumentNullExceptionWithParamNameEqualToT1IfFirstOperandIsNull()
        {
            int testSeconds1 = _rnd.Next(86399);
            int testSeconds0 = _rnd.Next(86400 - testSeconds1) + testSeconds1 + 1;
            TimeOfDay testValue0 = new TimeOfDay(testSeconds0);
            TimeOfDay testValue1 = null;

            try
            {
                _ = TimeOfDay.Subtract(testValue1, testValue0);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("t1", ex.ParamName);
            }
        }

        [TestMethod]
        public void TimeOfDayClass_SubtractOperatorWithTimeOfDayAndTimeSpanOperands_ReturnsTimeSpanWithCorrectValueIfFirstParameterIsBeforeSecondParameter()
        {
            int testSeconds0 = _rnd.Next(86399);
            int testSeconds1 = _rnd.Next(86400 - testSeconds0) + testSeconds0 + 1;
            TimeSpan testValue0 = TimeSpan.FromSeconds(testSeconds0);
            TimeOfDay testValue1 = new TimeOfDay(testSeconds1);

            TimeSpan testOutput = testValue1 - testValue0;

            Assert.AreEqual(testSeconds1 - testSeconds0, (int)testOutput.TotalSeconds);
        }

        [TestMethod]
        public void TimeOfDayClass_SubtractOperatorWithTimeOfDayAndTimeSpanOperands_ReturnsTimeSpanWithCorrectValueIfFirstParameterIsAfterSecondParameter()
        {
            int testSeconds1 = _rnd.Next(86399);
            int testSeconds0 = _rnd.Next(86400 - testSeconds1) + testSeconds1 + 1;
            TimeSpan testValue0 = TimeSpan.FromSeconds(testSeconds0);
            TimeOfDay testValue1 = new TimeOfDay(testSeconds1);

            TimeSpan testOutput = testValue1 - testValue0;

            Assert.AreEqual(testSeconds1 - testSeconds0, (int)testOutput.TotalSeconds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TimeOfDayClass_SubtractOperatorWithTimeOfDayAndTimeSpanOperands_ThrowsArgumentNullExceptionIfFirstOperandIsNull()
        {
            int testSeconds0 = _rnd.Next(86399);
            TimeSpan testValue0 = TimeSpan.FromSeconds(testSeconds0);
            TimeOfDay testValue1 = null;

            _ = testValue1 - testValue0;

            Assert.Fail();
        }

        [TestMethod]
        public void TimeOfDayClass_SubtractOperatorWithTimeOfDayAndTimeSpanOperands_ThrowsArgumentNullExceptionWithParamNameEqualToT1IfFirstOperandIsNull()
        {
            int testSeconds0 = _rnd.Next(86399);
            TimeSpan testValue0 = TimeSpan.FromSeconds(testSeconds0);
            TimeOfDay testValue1 = null;

            try
            {
                _ = testValue1 - testValue0;
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("t1", ex.ParamName);
            }
        }

        [TestMethod]
        public void TimeOfDayClass_SubtractMethodWithTimeOfDayAndTimeSpanParameters_ReturnsTimeSpanWithCorrectValueIfFirstParameterIsBeforeSecondParameter()
        {
            int testSeconds0 = _rnd.Next(86399);
            int testSeconds1 = _rnd.Next(86400 - testSeconds0) + testSeconds0 + 1;
            TimeSpan testValue0 = TimeSpan.FromSeconds(testSeconds0);
            TimeOfDay testValue1 = new TimeOfDay(testSeconds1);

            TimeSpan testOutput = TimeOfDay.Subtract(testValue1, testValue0);

            Assert.AreEqual(testSeconds1 - testSeconds0, (int)testOutput.TotalSeconds);
        }

        [TestMethod]
        public void TimeOfDayClass_SubtractMethodWithTimeOfDayAndTimeSpanParameters_ReturnsTimeSpanWithCorrectValueIfFirstParameterIsAfterSecondParameter()
        {
            int testSeconds1 = _rnd.Next(86399);
            int testSeconds0 = _rnd.Next(86400 - testSeconds1) + testSeconds1 + 1;
            TimeSpan testValue0 = TimeSpan.FromSeconds(testSeconds0);
            TimeOfDay testValue1 = new TimeOfDay(testSeconds1);

            TimeSpan testOutput = TimeOfDay.Subtract(testValue1, testValue0);

            Assert.AreEqual(testSeconds1 - testSeconds0, (int)testOutput.TotalSeconds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TimeOfDayClass_SubtractMethodWithTimeOfDayAndTimeSpanParameters_ThrowsArgumentNullExceptionIfFirstOperandIsNull()
        {
            int testSeconds0 = _rnd.Next(86399);
            TimeSpan testValue0 = TimeSpan.FromSeconds(testSeconds0);
            TimeOfDay testValue1 = null;

            _ = TimeOfDay.Subtract(testValue1, testValue0);

            Assert.Fail();
        }

        [TestMethod]
        public void TimeOfDayClass_SubtractMethodWithTimeOfDayAndTimeSpanParameters_ThrowsArgumentNullExceptionWithParamNameEqualToT1IfFirstOperandIsNull()
        {
            int testSeconds0 = _rnd.Next(86399);
            TimeSpan testValue0 = TimeSpan.FromSeconds(testSeconds0);
            TimeOfDay testValue1 = null;

            try
            {
                _ = TimeOfDay.Subtract(testValue1, testValue0);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("t1", ex.ParamName);
            }
        }

        [TestMethod]
        public void TimeOfDayClass_SubtractOperatorWithTimeSpanAndTimeOfDayOperands_ReturnsTimeSpanWithCorrectValueIfFirstParameterIsBeforeSecondParameter()
        {
            int testSeconds0 = _rnd.Next(86399);
            int testSeconds1 = _rnd.Next(86400 - testSeconds0) + testSeconds0 + 1;
            TimeOfDay testValue0 = new TimeOfDay(testSeconds0);
            TimeSpan testValue1 = TimeSpan.FromSeconds(testSeconds1);

            TimeSpan testOutput = testValue1 - testValue0;

            Assert.AreEqual(testSeconds1 - testSeconds0, (int)testOutput.TotalSeconds);
        }

        [TestMethod]
        public void TimeOfDayClass_SubtractOperatorWithTimeSpanAndTimeOfDayOperands_ReturnsTimeSpanWithCorrectValueIfFirstParameterIsAfterSecondParameter()
        {
            int testSeconds1 = _rnd.Next(86399);
            int testSeconds0 = _rnd.Next(86400 - testSeconds1) + testSeconds1 + 1;
            TimeOfDay testValue0 = new TimeOfDay(testSeconds0);
            TimeSpan testValue1 = TimeSpan.FromSeconds(testSeconds1);

            TimeSpan testOutput = testValue1 - testValue0;

            Assert.AreEqual(testSeconds1 - testSeconds0, (int)testOutput.TotalSeconds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TimeOfDayClass_SubtractOperatorWithTimeSpanAndTimeOfDayOperands_ThrowsArgumentNullExceptionIfSecondOperandIsNull()
        {
            int testSeconds1 = _rnd.Next(86399);
            TimeOfDay testValue0 = null;
            TimeSpan testValue1 = TimeSpan.FromSeconds(testSeconds1);

            _ = testValue1 - testValue0;

            Assert.Fail();
        }

        [TestMethod]
        public void TimeOfDayClass_SubtractOperatorWithTimeSpanAndTimeOfDayOperands_ThrowsArgumentNullExceptionWithParamNameEqualToT2IfSecondOperandIsNull()
        {
            int testSeconds1 = _rnd.Next(86399);
            TimeOfDay testValue0 = null;
            TimeSpan testValue1 = TimeSpan.FromSeconds(testSeconds1);

            try
            {
                _ = testValue1 - testValue0;
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("t2", ex.ParamName);
            }
        }

        [TestMethod]
        public void TimeOfDayClass_SubtractMethodWithTimeSpanAndTimeOfDayParameters_ReturnsTimeSpanWithCorrectValueIfFirstParameterIsBeforeSecondParameter()
        {
            int testSeconds0 = _rnd.Next(86399);
            int testSeconds1 = _rnd.Next(86400 - testSeconds0) + testSeconds0 + 1;
            TimeOfDay testValue0 = new TimeOfDay(testSeconds0);
            TimeSpan testValue1 = TimeSpan.FromSeconds(testSeconds1);

            TimeSpan testOutput = TimeOfDay.Subtract(testValue1, testValue0);

            Assert.AreEqual(testSeconds1 - testSeconds0, (int)testOutput.TotalSeconds);
        }

        [TestMethod]
        public void TimeOfDayClass_SubtractMethodWithTimeSpanAndTimeOfDayParameters_ReturnsTimeSpanWithCorrectValueIfFirstParameterIsAfterSecondParameter()
        {
            int testSeconds1 = _rnd.Next(86399);
            int testSeconds0 = _rnd.Next(86400 - testSeconds1) + testSeconds1 + 1;
            TimeOfDay testValue0 = new TimeOfDay(testSeconds0);
            TimeSpan testValue1 = TimeSpan.FromSeconds(testSeconds1);

            TimeSpan testOutput = TimeOfDay.Subtract(testValue1, testValue0);

            Assert.AreEqual(testSeconds1 - testSeconds0, (int)testOutput.TotalSeconds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TimeOfDayClass_SubtractMethodWithTimeSpanAndTimeOfDayParameters_ThrowsArgumentNullExceptionIfSecondOperandIsNull()
        {
            int testSeconds1 = _rnd.Next(86399);
            TimeOfDay testValue0 = null;
            TimeSpan testValue1 = TimeSpan.FromSeconds(testSeconds1);

            _ = TimeOfDay.Subtract(testValue1, testValue0);

            Assert.Fail();
        }

        [TestMethod]
        public void TimeOfDayClass_SubtractMethodWithTimeSpanAndTimeOfDayParameters_ThrowsArgumentNullExceptionWithParamNameEqualToT2IfSecondOperandIsNull()
        {
            int testSeconds1 = _rnd.Next(86399);
            TimeOfDay testValue0 = null;
            TimeSpan testValue1 = TimeSpan.FromSeconds(testSeconds1);

            try
            {
                _ = TimeOfDay.Subtract(testValue1, testValue0);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("t2", ex.ParamName);
            }
        }

        [TestMethod]
        public void TimeOfDayClass_AddOperatorWithTimeOfDayAndTimeSpanOperands_ReturnsTimeOfDayWithCorrectValueIfResultDoesNotCrossMidnight()
        {
            int testSeconds0 = _rnd.Next(86398);
            int testSeconds1 = _rnd.Next(86399 - testSeconds0) + testSeconds0;
            TimeOfDay testValue0 = new TimeOfDay(testSeconds0);
            TimeSpan testValue1 = TimeSpan.FromSeconds(testSeconds1);

            TimeOfDay testOutput = testValue0 + testValue1;

            Assert.AreEqual(testSeconds0 + testSeconds1, testOutput.AbsoluteSeconds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TimeOfDayClass_AddOperatorWithTimeOfDayAndTimeSpanOperands_ThrowsArgumentNullExceptionIfFirstOperandIsNull()
        {
            int testSeconds0 = _rnd.Next(86398);
            int testSeconds1 = _rnd.Next(86399 - testSeconds0) + testSeconds0;
            TimeOfDay testValue0 = null;
            TimeSpan testValue1 = TimeSpan.FromSeconds(testSeconds1);

            _ = testValue0 + testValue1;

            Assert.Fail();
        }

        [TestMethod]
        public void TimeOfDayClass_AddOperatorWithTimeOfDayAndTimeSpanOperands_ThrowsArgumentNullExceptionWithCorrectParamNamePropertyIfFirstOperandIsNull()
        {
            int testSeconds0 = _rnd.Next(86398);
            int testSeconds1 = _rnd.Next(86399 - testSeconds0) + testSeconds0;
            TimeOfDay testValue0 = null;
            TimeSpan testValue1 = TimeSpan.FromSeconds(testSeconds1);

            try 
            {
                _ = testValue0 + testValue1;
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("t1", ex.ParamName);
            }
        }

        [TestMethod]
        public void TimeOfDayClass_AddMethodWithTimeOfDayAndTimeSpanParameters_ReturnsTimeOfDayWithCorrectValueIfResultDoesNotCrossMidnight()
        {
            int testSeconds0 = _rnd.Next(86398);
            int testSeconds1 = _rnd.Next(86399 - testSeconds0) + testSeconds0;
            TimeOfDay testValue0 = new TimeOfDay(testSeconds0);
            TimeSpan testValue1 = TimeSpan.FromSeconds(testSeconds1);

            TimeOfDay testOutput = TimeOfDay.Add(testValue0, testValue1);

            Assert.AreEqual(testSeconds0 + testSeconds1, testOutput.AbsoluteSeconds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TimeOfDayClass_AddMethodWithTimeOfDayAndTimeSpanParameters_ThrowsArgumentNullExceptionIfFirstParameterIsNull()
        {
            int testSeconds0 = _rnd.Next(86398);
            int testSeconds1 = _rnd.Next(86399 - testSeconds0) + testSeconds0;
            TimeOfDay testValue0 = null;
            TimeSpan testValue1 = TimeSpan.FromSeconds(testSeconds1);

            _ = TimeOfDay.Add(testValue0, testValue1);

            Assert.Fail();
        }

        [TestMethod]
        public void TimeOfDayClass_AddMethodWithTimeOfDayAndTimeSpanParameters_ThrowsArgumentNullExceptionWithCorrectParamNamePropertyIfFirstParameterIsNull()
        {
            int testSeconds0 = _rnd.Next(86398);
            int testSeconds1 = _rnd.Next(86399 - testSeconds0) + testSeconds0;
            TimeOfDay testValue0 = null;
            TimeSpan testValue1 = TimeSpan.FromSeconds(testSeconds1);

            try
            {
                _ = TimeOfDay.Add(testValue0, testValue1);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("t1", ex.ParamName);
            }
        }

        [TestMethod]
        public void TimeOfDayClass_AddOperatorWithTimeSpanAndTimeOfDayOperands_ReturnsTimeOfDayWithCorrectValueIfResultDoesNotCrossMidnight()
        {
            int testSeconds0 = _rnd.Next(86398);
            int testSeconds1 = _rnd.Next(86399 - testSeconds0) + testSeconds0;
            TimeSpan testValue0 = TimeSpan.FromSeconds(testSeconds0);
            TimeOfDay testValue1 = new TimeOfDay(testSeconds1);

            TimeOfDay testOutput = testValue0 + testValue1;

            Assert.AreEqual(testSeconds0 + testSeconds1, testOutput.AbsoluteSeconds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TimeOfDayClass_AddOperatorWithTimeSpanAndTimeOfDayOperands_ThrowsArgumentNullExceptionIfSecondOperandIsNull()
        {
            int testSeconds0 = _rnd.Next(86398);
            TimeSpan testValue0 = TimeSpan.FromSeconds(testSeconds0);
            TimeOfDay testValue1 = null;

            _ = testValue0 + testValue1;

            Assert.Fail();
        }

        [TestMethod]
        public void TimeOfDayClass_AddOperatorWithTimeSpanAndTimeOfDayOperands_ThrowsArgumentNullExceptionWithCorrectParamNamePropertyIfSecondOperandIsNull()
        {
            int testSeconds0 = _rnd.Next(86398);
            TimeSpan testValue0 = TimeSpan.FromSeconds(testSeconds0);
            TimeOfDay testValue1 = null;

            try
            {
                _ = testValue0 + testValue1;
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("t2", ex.ParamName);
            }
        }

        [TestMethod]
        public void TimeOfDayClass_AddMethodWithTimeSpanAndTimeOfDayParameters_ReturnsTimeOfDayWithCorrectValueIfResultDoesNotCrossMidnight()
        {
            int testSeconds0 = _rnd.Next(86398);
            int testSeconds1 = _rnd.Next(86399 - testSeconds0) + testSeconds0;
            TimeSpan testValue0 = TimeSpan.FromSeconds(testSeconds0);
            TimeOfDay testValue1 = new TimeOfDay(testSeconds1);

            TimeOfDay testOutput = TimeOfDay.Add(testValue0, testValue1);

            Assert.AreEqual(testSeconds0 + testSeconds1, testOutput.AbsoluteSeconds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TimeOfDayClass_AddMethodWithTimeSpanAndTimeOfDayParameters_ThrowsArgumentNullExceptionIfSecondParameterIsNull()
        {
            int testSeconds0 = _rnd.Next(86398);
            TimeSpan testValue0 = TimeSpan.FromSeconds(testSeconds0);
            TimeOfDay testValue1 = null;

            _ = TimeOfDay.Add(testValue0, testValue1);

            Assert.Fail();
        }

        [TestMethod]
        public void TimeOfDayClass_AddMethodWithTimeSpanAndTimeOfDayParameters_ThrowsArgumentNullExceptionWithCorrectParamNamePropertyIfSecondParameterIsNull()
        {
            int testSeconds0 = _rnd.Next(86398);
            TimeSpan testValue0 = TimeSpan.FromSeconds(testSeconds0);
            TimeOfDay testValue1 = null;

            try
            {
                _ = TimeOfDay.Add(testValue0, testValue1);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("t2", ex.ParamName);
            }
        }

        [TestMethod]
        public void TimeOfDayClass_FromTimeSpanMethod_ReturnsObjectWithCorrectAbsoluteSecondsProperty()
        {
            int testPeriod = _rnd.Next(86400);
            TimeSpan span = new TimeSpan(0, 0, testPeriod);

            TimeOfDay testOutput = TimeOfDay.FromTimeSpan(span);

            Assert.AreEqual(testPeriod, testOutput.AbsoluteSeconds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TimeOfDayClass_CopyAndReflectMethod_ThrowsArgumentNullExceptionIfParameterIsNull()
        {
            int origTime = _rnd.Next(86380) + 10;
            TimeOfDay testObject = new TimeOfDay(origTime);

            testObject.CopyAndReflect(null);

            Assert.Fail();
        }

        [TestMethod]
        public void TimeOfDayClass_CopyAndReflectMethod_ThrowsArgumentNullExceptionWithCorrectParamNamePropertyIfParameterIsNull()
        {
            int origTime = _rnd.Next(86380) + 10;
            TimeOfDay testObject = new TimeOfDay(origTime);

            try
            {
                testObject.CopyAndReflect(null);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("aroundTime", ex.ParamName);
            }
        }

        [TestMethod]
        public void TimeOfDayClass_CopyAndReflectMethod_ReturnsDifferentObject()
        {
            int origTime = _rnd.Next(86380) + 10;
            TimeOfDay testObject = new TimeOfDay(origTime);
            int reflectAroundSecs = _rnd.Next((86400 - origTime) / 2 + origTime / 2) + (origTime / 2);
            TimeOfDay testParam = new TimeOfDay(reflectAroundSecs);

            TimeOfDay testOutput = testObject.CopyAndReflect(testParam);

            Assert.AreNotSame(testObject, testOutput);
        }

        [TestMethod]
        public void TimeOfDayClass_CopyAndReflectMethod_DoesNotChangeOriginalObject()
        {
            int origTime = _rnd.Next(86380) + 10;
            TimeOfDay testObject = new TimeOfDay(origTime);
            int reflectAroundSecs = _rnd.Next((86400 - origTime) / 2 + origTime / 2) + (origTime / 2);
            TimeOfDay testParam = new TimeOfDay(reflectAroundSecs);

            _ = testObject.CopyAndReflect(testParam);

            Assert.AreEqual(origTime, testObject.AbsoluteSeconds);
        }

        [TestMethod]
        public void TimeOfDayClass_CopyAndReflectMethod_ReturnsObjectWithCorrectTimeIfReflectedInFutureObject()
        {
            int origTime = _rnd.Next(86380);
            TimeOfDay testObject = new TimeOfDay(origTime);
            int reflectAroundSecs = _rnd.Next((86400 - origTime) / 2) + origTime;
            TimeOfDay testParam = new TimeOfDay(reflectAroundSecs);

            TimeOfDay testOutput = testObject.CopyAndReflect(testParam);

            Assert.AreEqual(reflectAroundSecs * 2 - origTime, testOutput.AbsoluteSeconds);
        }

        [TestMethod]
        public void TimeOfDayClass_CopyAndReflectMethod_ReturnsObjectWithCorrectTimeIfReflectedInPriorObject()
        {
            int origTime = _rnd.Next(86380);
            TimeOfDay testObject = new TimeOfDay(origTime);
            int reflectAroundSecs = _rnd.Next(origTime / 2) + origTime / 2;
            TimeOfDay testParam = new TimeOfDay(reflectAroundSecs);

            TimeOfDay testOutput = testObject.CopyAndReflect(testParam);

            Assert.AreEqual(reflectAroundSecs * 2 - origTime, testOutput.AbsoluteSeconds);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TimeOfDayClass_ToStringMethodWithStringAndIFormatProviderParameters_ThrowsArgumentNullExceptionIfFirstParameterIsNull()
        {
            string testParam0 = null;
            int origTime = _rnd.Next(86380);
            TimeOfDay testObject = new TimeOfDay(origTime);

            testObject.ToString(testParam0, CultureInfo.CurrentCulture);

            Assert.Fail();
        }

        [TestMethod]
        public void TimeOfDayClass_ToStringMethodWithStringAndIFormatProviderParameters_ThrowsArgumentNullExceptionWithCorrectParamNamePropertyIfFirstParameterIsNull()
        {
            string testParam0 = null;
            int origTime = _rnd.Next(86380);
            TimeOfDay testObject = new TimeOfDay(origTime);

            try
            {
                testObject.ToString(testParam0, CultureInfo.CurrentCulture);
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("format", ex.ParamName);
            }
        }
    }
}
