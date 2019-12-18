﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tests.Utility.Providers;
using Timetabler.Data;
using Unicorn.Impl.PdfSharp;
using Unicorn.Interfaces;
using Unicorn.Writer;

namespace Timetabler.PdfExport.Tests.Unit
{
    [TestClass]
    public class DocumentDescriptorFactoryUnitTests
    {
        private static readonly Random _rnd = RandomProvider.Default;

        [TestMethod]
        public void DocumentDescriptorFactoryClass_GetDocumentDescriptorMethod_ReturnsPdfDocumentClassInstance_IfConstructorParameterWasEqualToUnicorn()
        {
            DocumentDescriptorFactory testObject = new DocumentDescriptorFactory(PdfExportEngine.Unicorn);
            double testParam0 = _rnd.NextDouble();
            double testParam1 = _rnd.NextDouble();

            IDocumentDescriptor testOutput = testObject.GetDocumentDescriptor(testParam0, testParam1);

            Assert.AreEqual(testOutput.GetType(), typeof(PdfDocument));
        }

        [TestMethod]
        public void DocumentDescriptorFactoryClass_GetDocumentDescriptorMethod_ReturnsPdfDocumentClassInstanceWithDefaultPageSizeEqualToA4_IfConstructorParameterWasEqualToUnicorn()
        {
            DocumentDescriptorFactory testObject = new DocumentDescriptorFactory(PdfExportEngine.Unicorn);
            double testParam0 = _rnd.NextDouble();
            double testParam1 = _rnd.NextDouble();

            IDocumentDescriptor testOutput = testObject.GetDocumentDescriptor(testParam0, testParam1);

            Assert.AreEqual(PhysicalPageSize.A4, testOutput.DefaultPhysicalPageSize);
        }

        [TestMethod]
        public void DocumentDescriptorFactoryClass_GetDocumentDescriptorMethod_ReturnsPdfDocumentClassInstanceWithDefaultPageOrientationEqualToLandscape_IfConstructorParameterWasEqualToUnicorn()
        {
            DocumentDescriptorFactory testObject = new DocumentDescriptorFactory(PdfExportEngine.Unicorn);
            double testParam0 = _rnd.NextDouble();
            double testParam1 = _rnd.NextDouble();

            IDocumentDescriptor testOutput = testObject.GetDocumentDescriptor(testParam0, testParam1);

            Assert.AreEqual(PageOrientation.Landscape, testOutput.DefaultPageOrientation);
        }

        [TestMethod]
        public void DocumentDescriptorFactoryClass_GetDocumentDescriptorMethod_ReturnsPdfDocumentClassInstanceWithDefaultHorizontalMarginProportionEqualToFirstParameter_IfConstructorParameterWasEqualToUnicorn()
        {
            DocumentDescriptorFactory testObject = new DocumentDescriptorFactory(PdfExportEngine.Unicorn);
            double testParam0 = _rnd.NextDouble();
            double testParam1 = _rnd.NextDouble();

            IDocumentDescriptor testOutput = testObject.GetDocumentDescriptor(testParam0, testParam1);

            Assert.AreEqual(testParam0, testOutput.DefaultHorizontalMarginProportion);
        }

        [TestMethod]
        public void DocumentDescriptorFactoryClass_GetDocumentDescriptorMethod_ReturnsPdfDocumentClassInstanceWithDefaultVerticalMarginProportionEqualToSecondParameter_IfConstructorParameterWasEqualToUnicorn()
        {
            DocumentDescriptorFactory testObject = new DocumentDescriptorFactory(PdfExportEngine.Unicorn);
            double testParam0 = _rnd.NextDouble();
            double testParam1 = _rnd.NextDouble();

            IDocumentDescriptor testOutput = testObject.GetDocumentDescriptor(testParam0, testParam1);

            Assert.AreEqual(testParam1, testOutput.DefaultVerticalMarginProportion);
        }

        [TestMethod]
        public void DocumentDescriptorFactoryClass_GetDocumentDescriptorMethod_ReturnsDocumentDescriptorClassInstance_IfConstructorParameterWasEqualToExternal()
        {
            DocumentDescriptorFactory testObject = new DocumentDescriptorFactory(PdfExportEngine.External);
            double testParam0 = _rnd.NextDouble();
            double testParam1 = _rnd.NextDouble();

            IDocumentDescriptor testOutput = testObject.GetDocumentDescriptor(testParam0, testParam1);

            Assert.AreEqual(testOutput.GetType(), typeof(DocumentDescriptor));
        }

        [TestMethod]
        public void DocumentDescriptorFactoryClass_GetDocumentDescriptorMethod_ReturnsPdfDocumentClassInstanceWithDefaultPageSizeEqualToA4_IfConstructorParameterWasEqualToExternal()
        {
            DocumentDescriptorFactory testObject = new DocumentDescriptorFactory(PdfExportEngine.External);
            double testParam0 = _rnd.NextDouble();
            double testParam1 = _rnd.NextDouble();

            IDocumentDescriptor testOutput = testObject.GetDocumentDescriptor(testParam0, testParam1);

            Assert.AreEqual(PhysicalPageSize.A4, testOutput.DefaultPhysicalPageSize);
        }

        [TestMethod]
        public void DocumentDescriptorFactoryClass_GetDocumentDescriptorMethod_ReturnsPdfDocumentClassInstanceWithDefaultPageOrientationEqualToLandscape_IfConstructorParameterWasEqualToExternal()
        {
            DocumentDescriptorFactory testObject = new DocumentDescriptorFactory(PdfExportEngine.External);
            double testParam0 = _rnd.NextDouble();
            double testParam1 = _rnd.NextDouble();

            IDocumentDescriptor testOutput = testObject.GetDocumentDescriptor(testParam0, testParam1);

            Assert.AreEqual(PageOrientation.Landscape, testOutput.DefaultPageOrientation);
        }

        [TestMethod]
        public void DocumentDescriptorFactoryClass_GetDocumentDescriptorMethod_ReturnsPdfDocumentClassInstanceWithDefaultHorizontalMarginProportionEqualToFirstParameter_IfConstructorParameterWasEqualToExternal()
        {
            DocumentDescriptorFactory testObject = new DocumentDescriptorFactory(PdfExportEngine.External);
            double testParam0 = _rnd.NextDouble();
            double testParam1 = _rnd.NextDouble();

            IDocumentDescriptor testOutput = testObject.GetDocumentDescriptor(testParam0, testParam1);

            Assert.AreEqual(testParam0, testOutput.DefaultHorizontalMarginProportion);
        }

        [TestMethod]
        public void DocumentDescriptorFactoryClass_GetDocumentDescriptorMethod_ReturnsPdfDocumentClassInstanceWithDefaultVerticalMarginProportionEqualToSecondParameter_IfConstructorParameterWasEqualToExternal()
        {
            DocumentDescriptorFactory testObject = new DocumentDescriptorFactory(PdfExportEngine.External);
            double testParam0 = _rnd.NextDouble();
            double testParam1 = _rnd.NextDouble();

            IDocumentDescriptor testOutput = testObject.GetDocumentDescriptor(testParam0, testParam1);

            Assert.AreEqual(testParam1, testOutput.DefaultVerticalMarginProportion);
        }
    }
}
