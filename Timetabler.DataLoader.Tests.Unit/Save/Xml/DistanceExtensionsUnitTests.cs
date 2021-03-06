﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Timetabler.Data;
using Timetabler.DataLoader.Save.Xml;

namespace Timetabler.DataLoader.Tests.Unit.Save.Xml
{
    [TestClass]
    public class DistanceExtensionsUnitTests
    {
#pragma warning disable CA1707 // Identifiers should not contain underscores

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DistanceExtensionsClass_ToDistanceModelMethod_ThrowsArgumentNullException_IfParameterIsNull()
        {
            Distance testObject = null;

            _ = testObject.ToDistanceModel();

            Assert.Fail();
        }

        [TestMethod]
        public void DistanceExtensionsClass_ToDistanceModelMethod_ThrowsArgumentNullExceptionWithCorrectParamNameProperty_IfParameterIsNull()
        {
            Distance testObject = null;

            try
            {
                _ = testObject.ToDistanceModel();
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("distance", ex.ParamName);
            }
        }

#pragma warning restore CA1707 // Identifiers should not contain underscores

    }
}
