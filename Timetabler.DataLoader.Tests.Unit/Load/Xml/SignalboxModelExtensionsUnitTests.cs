﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Timetabler.DataLoader.Load.Xml;
using Timetabler.SerialData.Xml;

namespace Timetabler.DataLoader.Tests.Unit.Load.Xml
{
    [TestClass]
    public class SignalboxModelExtensionsUnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SignalboxModelExtensionsClass_ToSignalboxMethod_ThrowsArgumentNullException_IfParameterIsNull()
        {
            SignalboxModel testObject = null;

            _ = testObject.ToSignalbox();

            Assert.Fail();
        }

        [TestMethod]
        public void SignalboxModelExtensionsClass_ToSignalboxMethod_ThrowsArgumentNullExceptionWithCorrectParamNameProperty_IfParameterIsNull()
        {
            SignalboxModel testObject = null;

            try
            {
                _ = testObject.ToSignalbox();
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("model", ex.ParamName);
            }
        }
    }
}
