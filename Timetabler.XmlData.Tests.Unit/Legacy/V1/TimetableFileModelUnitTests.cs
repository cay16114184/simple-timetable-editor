﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace Timetabler.XmlData.Tests.Unit.Legacy.V1
{
    [TestClass]
    public class TimetableFileModelUnitTests
    {
        [TestMethod]
        public void TimetableFileModelClassIsPublic()
        {
            Assert.IsTrue(typeof(XmlData.Legacy.V1.TimetableFileModel).IsPublic);
        }

        [TestMethod]
        public void TimetableFileModelClassIsDecoratedWithXmlRootAttribute()
        {
            Assert.IsNotNull(typeof(XmlData.Legacy.V1.TimetableFileModel).GetCustomAttributes<XmlRootAttribute>(false).First());
        }

        [TestMethod]
        public void TimetableFileModelClassXmlRootAttributeElementNamePropertyEqualsDocumentModel()
        {
            XmlRootAttribute attr = typeof(XmlData.Legacy.V1.TimetableFileModel).GetCustomAttributes<XmlRootAttribute>(false).First();
            Assert.AreEqual("DocumentModel", attr.ElementName);
        }

        [TestMethod]
        public void TimetableFileModelClassHasPublicParameterlessConstructor()
        {
            ConstructorInfo cInfo = typeof(XmlData.Legacy.V1.TimetableFileModel).GetConstructor(new Type[0]);
            Assert.IsNotNull(cInfo);
            Assert.IsTrue(cInfo.IsPublic);
        }

        [TestMethod]
        public void TimetableFileModelClassHasPublicVersionPropertyOfTypeInt()
        {
            PropertyInfo pInfo = typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("Version");
            Assert.IsNotNull(pInfo);
            Assert.IsTrue(pInfo.GetMethod.IsPublic);
            Assert.IsTrue(pInfo.SetMethod.IsPublic);
            Assert.AreEqual(typeof(int), pInfo.PropertyType);
        }

        [TestMethod]
        public void TimetableFileModelClassVersionPropertyIsDecoratedWithXmlAttributeAttribute()
        {
            Assert.IsNotNull(typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("Version").GetCustomAttributes<XmlAttributeAttribute>(false).First());
        }

        [TestMethod]
        public void TimetableFileModelClassVersionPropertyXmlAttributeAttributeAttributeNamePropertyEqualsVersion()
        {
            XmlAttributeAttribute attr = typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("Version").GetCustomAttributes<XmlAttributeAttribute>(false).First();
            Assert.AreEqual("version", attr.AttributeName);
        }

        [TestMethod]
        public void TimetableFileModelClassHasPublicOptionsPropertyOfTypeDocumentOptionsModel()
        {
            PropertyInfo pInfo = typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("Options");
            Assert.IsNotNull(pInfo);
            Assert.IsTrue(pInfo.GetMethod.IsPublic);
            Assert.IsTrue(pInfo.SetMethod.IsPublic);
            Assert.AreEqual(typeof(DocumentOptionsModel), pInfo.PropertyType);
        }

        [TestMethod]
        public void TimetableFileModelClassOptionsPropertyIsDecoratedWithXmlElementAttribute()
        {
            Assert.IsNotNull(typeof(TimetableFileModel).GetProperty("Options").GetCustomAttributes<XmlElementAttribute>(false).First());
        }

        [TestMethod]
        public void TimetableFileModelClassHasPublicTitlePropertyOfTypeString()
        {
            PropertyInfo pInfo = typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("Title");
            Assert.IsNotNull(pInfo);
            Assert.IsTrue(pInfo.GetMethod.IsPublic);
            Assert.IsTrue(pInfo.SetMethod.IsPublic);
            Assert.AreEqual(typeof(string), pInfo.PropertyType);
        }

        [TestMethod]
        public void TimetableFileModelClassTitlePropertyIsDecoratedWithXmlElementAttribute()
        {
            Assert.IsNotNull(typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("Title").GetCustomAttributes<XmlElementAttribute>(false).First());
        }

        [TestMethod]
        public void TimetableFileModelClassHasPublicSubtitlePropertyOfTypeString()
        {
            PropertyInfo pInfo = typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("Subtitle");
            Assert.IsNotNull(pInfo);
            Assert.IsTrue(pInfo.GetMethod.IsPublic);
            Assert.IsTrue(pInfo.SetMethod.IsPublic);
            Assert.AreEqual(typeof(string), pInfo.PropertyType);
        }

        [TestMethod]
        public void TimetableFileModelClassSubtitlePropertyIsDecoratedWithXmlElementAttribute()
        {
            Assert.IsNotNull(typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("Subtitle").GetCustomAttributes<XmlElementAttribute>(false).First());
        }

        [TestMethod]
        public void TimetableFileModelClassHasPublicDateDescriptionPropertyOfTypeString()
        {
            PropertyInfo pInfo = typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("DateDescription");
            Assert.IsNotNull(pInfo);
            Assert.IsTrue(pInfo.GetMethod.IsPublic);
            Assert.IsTrue(pInfo.SetMethod.IsPublic);
            Assert.AreEqual(typeof(string), pInfo.PropertyType);
        }

        [TestMethod]
        public void TimetableFileModelClassDateDescriptionPropertyIsDecoratedWithXmlElementAttribute()
        {
            Assert.IsNotNull(typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("DateDescription").GetCustomAttributes<XmlElementAttribute>(false).First());
        }

        [TestMethod]
        public void TimetableFileModelClassHasPublicLocationListPropertyOfTypeListOfLocationModel()
        {
            PropertyInfo pInfo = typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("LocationList");
            Assert.IsNotNull(pInfo);
            Assert.IsTrue(pInfo.GetMethod.IsPublic);
            Assert.IsTrue(pInfo.SetMethod.IsPublic);
            Assert.AreEqual(typeof(List<LocationModel>), pInfo.PropertyType);
        }

        [TestMethod]
        public void TimetableFileModelClassLocationListPropertyIsDecoratedWithXmlArrayAttribute()
        {
            Assert.IsNotNull(typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("LocationList").GetCustomAttributes<XmlArrayAttribute>(false).First());
        }

        [TestMethod]
        public void TimetableFileModelClassLocationListPropertyIsDecoratedWithXmlArrayItemAttribute()
        {
            Assert.IsNotNull(typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("LocationList").GetCustomAttributes<XmlArrayItemAttribute>(false).First());
        }

        [TestMethod]
        public void TimetableFileModelClassLocationListPropertyXmlArrayItemAttributeElementNamePropertyEqualsLocation()
        {
            XmlArrayItemAttribute attr = typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("LocationList").GetCustomAttributes<XmlArrayItemAttribute>(false).First();
            Assert.AreEqual("Location", attr.ElementName);
        }

        [TestMethod]
        public void TimetableFileModelClassHasPublicNoteDefinitionsPropertyOfTypeListOfNoteModel()
        {
            PropertyInfo pInfo = typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("NoteDefinitions");
            Assert.IsNotNull(pInfo);
            Assert.IsTrue(pInfo.GetMethod.IsPublic);
            Assert.IsTrue(pInfo.SetMethod.IsPublic);
            Assert.AreEqual(typeof(List<NoteModel>), pInfo.PropertyType);
        }

        [TestMethod]
        public void TimetableFileModelClassNoteDefinitionsPropertyIsDecoratedWithXmlArrayAttribute()
        {
            Assert.IsNotNull(typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("NoteDefinitions").GetCustomAttributes<XmlArrayAttribute>(false).First());
        }

        [TestMethod]
        public void TimetableFileModelClassNoteDefinitionsPropertyIsDecoratedWithXmlArrayItemAttribute()
        {
            Assert.IsNotNull(typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("NoteDefinitions").GetCustomAttributes<XmlArrayItemAttribute>(false).First());
        }

        [TestMethod]
        public void TimetableFileModelClassNoteDefinitionsPropertyXmlArrayItemAttributeElementNamePropertyEqualsNote()
        {
            XmlArrayItemAttribute attr = typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("NoteDefinitions").GetCustomAttributes<XmlArrayItemAttribute>(false).First();
            Assert.AreEqual("Note", attr.ElementName);
        }

        [TestMethod]
        public void TimetableFileModelClassHasPublicTrainClassListPropertyOfTypeListOfTrainClassModel()
        {
            PropertyInfo pInfo = typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("TrainClassList");
            Assert.IsNotNull(pInfo);
            Assert.IsTrue(pInfo.GetMethod.IsPublic);
            Assert.IsTrue(pInfo.SetMethod.IsPublic);
            Assert.AreEqual(typeof(List<TrainClassModel>), pInfo.PropertyType);
        }

        [TestMethod]
        public void TimetableFileModelClassTrainClassListPropertyIsDecoratedWithXmlArrayAttribute()
        {
            Assert.IsNotNull(typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("TrainClassList").GetCustomAttributes<XmlArrayAttribute>(false).First());
        }

        [TestMethod]
        public void TimetableFileModelClassTrainClassListPropertyIsDecoratedWithXmlArrayItemAttribute()
        {
            Assert.IsNotNull(typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("TrainClassList").GetCustomAttributes<XmlArrayItemAttribute>(false).First());
        }

        [TestMethod]
        public void TimetableFileModelClassTrainClassListPropertyXmlArrayItemAttributeElementNamePropertyEqualsTrainClass()
        {
            XmlArrayItemAttribute attr = typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("TrainClassList").GetCustomAttributes<XmlArrayItemAttribute>(false).First();
            Assert.AreEqual("TrainClass", attr.ElementName);
        }

        [TestMethod]
        public void TimetableFileModelClassHasPublicTrainListPropertyOfTypeListOfTrainModel()
        {
            PropertyInfo pInfo = typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("TrainList");
            Assert.IsNotNull(pInfo);
            Assert.IsTrue(pInfo.GetMethod.IsPublic);
            Assert.IsTrue(pInfo.SetMethod.IsPublic);
            Assert.AreEqual(typeof(List<XmlData.Legacy.V1.TrainModel>), pInfo.PropertyType);
        }

        [TestMethod]
        public void TimetableFileModelClassTrainListPropertyIsDecoratedWithXmlArrayAttribute()
        {
            Assert.IsNotNull(typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("TrainList").GetCustomAttributes<XmlArrayAttribute>(false).First());
        }

        [TestMethod]
        public void TimetableFileModelClassTrainListPropertyIsDecoratedWithXmlArrayItemAttribute()
        {
            Assert.IsNotNull(typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("TrainList").GetCustomAttributes<XmlArrayItemAttribute>(false).First());
        }

        [TestMethod]
        public void TimetableFileModelClassTrainListPropertyXmlArrayItemAttributeElementNamePropertyEqualsTrain()
        {
            XmlArrayItemAttribute attr = typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("TrainList").GetCustomAttributes<XmlArrayItemAttribute>(false).First();
            Assert.AreEqual("Train", attr.ElementName);
        }

        [TestMethod]
        public void TimetableFileModelClassHasPublicWrittenByPropertyOfTypeString()
        {
            PropertyInfo pInfo = typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("WrittenBy");
            Assert.IsNotNull(pInfo);
            Assert.IsTrue(pInfo.GetMethod.IsPublic);
            Assert.IsTrue(pInfo.SetMethod.IsPublic);
            Assert.AreEqual(typeof(string), pInfo.PropertyType);
        }

        [TestMethod]
        public void TimetableFileModelClassWrittenByPropertyIsDecoratedWithXmlElementAttribute()
        {
            Assert.IsNotNull(typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("WrittenBy").GetCustomAttributes<XmlElementAttribute>(false).First());
        }

        [TestMethod]
        public void TimetableFileModelClassHasPublicCheckedByPropertyOfTypeString()
        {
            PropertyInfo pInfo = typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("CheckedBy");
            Assert.IsNotNull(pInfo);
            Assert.IsTrue(pInfo.GetMethod.IsPublic);
            Assert.IsTrue(pInfo.SetMethod.IsPublic);
            Assert.AreEqual(typeof(string), pInfo.PropertyType);
        }

        [TestMethod]
        public void TimetableFileModelClassCheckedByPropertyIsDecoratedWithXmlElementAttribute()
        {
            Assert.IsNotNull(typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("CheckedBy").GetCustomAttributes<XmlElementAttribute>(false).First());
        }

        [TestMethod]
        public void TimetableFileModelClassHasPublicTimetableVersionPropertyOfTypeString()
        {
            PropertyInfo pInfo = typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("TimetableVersion");
            Assert.IsNotNull(pInfo);
            Assert.IsTrue(pInfo.GetMethod.IsPublic);
            Assert.IsTrue(pInfo.SetMethod.IsPublic);
            Assert.AreEqual(typeof(string), pInfo.PropertyType);
        }

        [TestMethod]
        public void TimetableFileModelClassTimetableVersionPropertyIsDecoratedWithXmlElementAttribute()
        {
            Assert.IsNotNull(typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("TimetableVersion").GetCustomAttributes<XmlElementAttribute>(false).First());
        }

        [TestMethod]
        public void TimetableFileModelClassHasPublicPublishedDatePropertyOfTypeString()
        {
            PropertyInfo pInfo = typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("PublishedDate");
            Assert.IsNotNull(pInfo);
            Assert.IsTrue(pInfo.GetMethod.IsPublic);
            Assert.IsTrue(pInfo.SetMethod.IsPublic);
            Assert.AreEqual(typeof(string), pInfo.PropertyType);
        }

        [TestMethod]
        public void TimetableFileModelClassPublishedDatePropertyIsDecoratedFromXmlElementAttribute()
        {
            Assert.IsNotNull(typeof(XmlData.Legacy.V1.TimetableFileModel).GetProperty("PublishedDate").GetCustomAttributes<XmlElementAttribute>(false).First());
        }
    }
}