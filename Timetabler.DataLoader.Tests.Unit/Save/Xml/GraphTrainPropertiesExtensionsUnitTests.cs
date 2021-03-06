﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Timetabler.Data;
using Timetabler.DataLoader.Save.Xml;

namespace Timetabler.DataLoader.Tests.Unit.Save.Xml
{
    [TestClass]
    public class GraphTrainPropertiesExtensionsUnitTests
    {
#pragma warning disable CA1707 // Identifiers should not contain underscores

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GraphTrainPropertiesExtensionsClass_ToGraphTrainPropertiesModelMethod_ThrowsArgumentNullException_IfParameterIsNull()
        {
            GraphTrainProperties testObject = null;

            _ = testObject.ToGraphTrainPropertiesModel();

            Assert.Fail();
        }

        [TestMethod]
        public void GraphTrainPropertiesExtensionsClass_ToGraphTrainPropertiesModelMethod_ThrowsArgumentNullExceptionWithCorrectParamNameProperty_IfParameterIsNull()
        {
            GraphTrainProperties testObject = null;

            try
            {
                _ = testObject.ToGraphTrainPropertiesModel();
                Assert.Fail();
            }
            catch (ArgumentNullException ex)
            {
                Assert.AreEqual("gtp", ex.ParamName);
            }
        }

#pragma warning restore CA1707 // Identifiers should not contain underscores

    }
}
