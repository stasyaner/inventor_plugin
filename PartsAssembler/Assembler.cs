﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Inventor;
using InventorAPI;
using Parts;
using Settings;

namespace PartsAssembler
{
    /// <summary>
    /// Класс сборщика деталей
    /// </summary>
    public class Assembler
    {
        /// <summary>
        /// Ссылка на коннектор
        /// </summary>
        private readonly InventorConnector _inventorConnector;

        /// <summary>
        /// Список деталей
        /// </summary>
        private readonly List<IPart> _parts;

        /// <summary>
        /// Ссылка на документ сборки
        /// </summary>
        private readonly AssemblyDocument _assemblyDocument;

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="settings">Список настроек</param>
        /// <param name="inventorConnector">Ссылка на коннектор</param>
        public Assembler(List<ISettings> settings, InventorConnector inventorConnector)
        {
            _inventorConnector = inventorConnector;
            _parts = new List<IPart>()
            {
                new TunerPart(settings.First(setting => setting.GetType() == typeof(TunerSettings)), _inventorConnector),
                new FretPart(settings.First(setting => setting.GetType() == typeof(FretSettings)), _inventorConnector),
                new InlayPart(settings.First(setting => setting.GetType() == typeof(InlaySettings)), _inventorConnector),
                new HeadstockPart(settings.First(setting => setting.GetType() == typeof(HeadstockSettings)), _inventorConnector),
                new FingerboardPart(settings.First(setting => setting.GetType() == typeof (FingerboardSettings)), _inventorConnector),
                new NeckPart(settings.First(setting => setting.GetType() == typeof(NeckSettings)), _inventorConnector),
            };
            _assemblyDocument = (AssemblyDocument)inventorConnector.InventorApplication.Documents.Add(
                DocumentTypeEnum.kAssemblyDocumentObject, inventorConnector.InventorApplication.FileManager.GetTemplateFile(
                    DocumentTypeEnum.kAssemblyDocumentObject, SystemOfMeasureEnum.kMetricSystemOfMeasure));
        }

        /// <summary>
        /// Метод сборки деталей
        /// </summary>
        public void Assembly()
        {
            BuildParts();

            object faceProxy; //Для метода CreateGeometryProxy

            #region neckOccurrence

            ComponentOccurrence neckPartComponentOccurrence = _assemblyDocument.ComponentDefinition.Occurrences.AddByComponentDefinition(
                (ComponentDefinition)_parts.First(part => part.GetType() == typeof(NeckPart)).PartDocumentComponentDefinition,
                _inventorConnector.InventorApplication.TransientGeometry.CreateMatrix());

            #endregion

            #region fingerboardOccurence

            ComponentOccurrence fingerboardPartComponentOccurrence = _assemblyDocument.ComponentDefinition.Occurrences
                .AddByComponentDefinition(
                    (ComponentDefinition)(_parts.First(part => part.GetType() == typeof(FingerboardPart)).PartDocumentComponentDefinition),
                    _inventorConnector.InventorApplication.TransientGeometry.CreateMatrix());

            neckPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)neckPartComponentOccurrence.Definition).Features.LoftFeatures[1].Faces[6], out faceProxy);

            GeometryIntent neckPartIntent1 = _assemblyDocument.ComponentDefinition.CreateGeometryIntent(faceProxy as Face,
                PointIntentEnum.kPlanarFaceCenterPointIntent);

            fingerboardPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)fingerboardPartComponentOccurrence.Definition).Features.LoftFeatures[1].Faces[
                        ((FingerboardPart)_parts.First(part => part.GetType() == typeof(FingerboardPart))).FretNumber + 4], out faceProxy);

            GeometryIntent fingerboardPartIntent1 = _assemblyDocument.ComponentDefinition.CreateGeometryIntent(faceProxy as Face,
                PointIntentEnum.kPlanarFaceCenterPointIntent);

            AssemblyJointDefinition neckAndFingerboardJointDefinition =
                _assemblyDocument.ComponentDefinition.Joints.CreateAssemblyJointDefinition(
                    AssemblyJointTypeEnum.kRigidJointType, fingerboardPartIntent1, neckPartIntent1);
            neckAndFingerboardJointDefinition.FlipOriginDirection = true;
            neckAndFingerboardJointDefinition.FlipAlignmentDirection = true;

            _assemblyDocument.ComponentDefinition.Joints.Add(neckAndFingerboardJointDefinition);

            #endregion

            #region headstockOccurence

            ComponentOccurrence headstockPartComponentOccurrence = _assemblyDocument.ComponentDefinition.Occurrences
                .AddByComponentDefinition(
                    (ComponentDefinition)_parts.First(part => part.GetType() == typeof(HeadstockPart)).PartDocumentComponentDefinition,
                    _inventorConnector.InventorApplication.TransientGeometry.CreateMatrix());

            neckPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)neckPartComponentOccurrence.Definition).Features.LoftFeatures[1].Faces[2], out faceProxy);

            GeometryIntent neckPartIntent2 = _assemblyDocument.ComponentDefinition.CreateGeometryIntent(faceProxy as Face,
                PointIntentEnum.kPlanarFaceCenterPointIntent);

            if (((HeadstockPart)_parts.First(part => part.GetType() == typeof(HeadstockPart))).IsReversed)
            {
                headstockPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)headstockPartComponentOccurrence.Definition).Features.ExtrudeFeatures[1].SideFaces[1],
                    out faceProxy);
            }
            else
            {
                headstockPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)headstockPartComponentOccurrence.Definition).Features.ExtrudeFeatures[1].Faces[5],
                    out faceProxy);
            }

            GeometryIntent headstockPartIntent = _assemblyDocument.ComponentDefinition.CreateGeometryIntent(faceProxy as Face,
                PointIntentEnum.kPlanarFaceCenterPointIntent);

            AssemblyJointDefinition neckAndHeadstockJointDefinition =
                _assemblyDocument.ComponentDefinition.Joints.CreateAssemblyJointDefinition(
                    AssemblyJointTypeEnum.kRigidJointType, headstockPartIntent, neckPartIntent2);

            neckAndHeadstockJointDefinition.FlipOriginDirection = true;

            if (((HeadstockPart)_parts.First(part => part.GetType() == typeof(HeadstockPart))).IsReversed)
            {
                headstockPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)headstockPartComponentOccurrence.Definition).Features.ExtrudeFeatures[1].StartFaces[1],
                    out faceProxy);
            }
            else
            {
                headstockPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)headstockPartComponentOccurrence.Definition).Features.ExtrudeFeatures[1].EndFaces[1],
                    out faceProxy);
            }


            neckAndHeadstockJointDefinition.AlignmentOne = faceProxy;

            neckPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)neckPartComponentOccurrence.Definition).Features.LoftFeatures[1].Faces[6], out faceProxy);
            neckAndHeadstockJointDefinition.AlignmentTwo = faceProxy;

            _assemblyDocument.ComponentDefinition.Joints.Add(neckAndHeadstockJointDefinition);

            #endregion

            #region inlayOccurence

            if (((InlayPart)_parts.First(part => part.GetType() == typeof(InlayPart))).IsActive)
            {
                for (int i = 1; i <= ((FingerboardPart)_parts.First(part => part.GetType() == typeof(FingerboardPart))).FretNumber * 2; i++)
                {
                    if (i % 2 != 0) continue;

                    ComponentOccurrence inlayPartComponentOccurrence = _assemblyDocument.ComponentDefinition.Occurrences
                        .AddByComponentDefinition(
                            (ComponentDefinition)_parts.First(part => part.GetType() == typeof(InlayPart)).PartDocumentComponentDefinition,
                            _inventorConnector.InventorApplication.TransientGeometry.CreateMatrix());

                    inlayPartComponentOccurrence.CreateGeometryProxy(
                        ((PartComponentDefinition)inlayPartComponentOccurrence.Definition).Features.ExtrudeFeatures[1].StartFaces[1],
                        out faceProxy);

                    GeometryIntent inlayPartIntent = _assemblyDocument.ComponentDefinition.CreateGeometryIntent(faceProxy as Face,
                        PointIntentEnum.kPlanarFaceCenterPointIntent);

                    fingerboardPartComponentOccurrence.CreateGeometryProxy(
                        ((PartComponentDefinition)fingerboardPartComponentOccurrence.Definition).Features.ExtrudeFeatures[i].StartFaces[1],
                        out faceProxy);

                    GeometryIntent fingerboardPartIntent2 = _assemblyDocument.ComponentDefinition.CreateGeometryIntent(faceProxy as Face,
                        PointIntentEnum.kPlanarFaceCenterPointIntent);

                    AssemblyJointDefinition fingerboardAndInlayJointDefinition =
                        _assemblyDocument.ComponentDefinition.Joints.CreateAssemblyJointDefinition(
                            AssemblyJointTypeEnum.kRigidJointType, fingerboardPartIntent2, inlayPartIntent);
                    fingerboardAndInlayJointDefinition.FlipOriginDirection = true;

                    _assemblyDocument.ComponentDefinition.Joints.Add(fingerboardAndInlayJointDefinition);
                }
            }

            #endregion

            #region fretOccurence

            for (int i = 1; i <= ((FingerboardPart)_parts.First(part => part.GetType() == typeof(FingerboardPart))).FretNumber * 2; i++)
            {
                if (i % 2 == 0) continue;

                ComponentOccurrence fretPartComponentOccurrence = _assemblyDocument.ComponentDefinition.Occurrences.AddByComponentDefinition(
                    (ComponentDefinition)_parts.First(part => part.GetType() == typeof(FretPart)).PartDocumentComponentDefinition,
                    _inventorConnector.InventorApplication.TransientGeometry.CreateMatrix());

                fretPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)fretPartComponentOccurrence.Definition).Features.ExtrudeFeatures[1].SideFaces[1],
                    out faceProxy);

                GeometryIntent fretPartIntent = _assemblyDocument.ComponentDefinition.CreateGeometryIntent(faceProxy as Face,
                    PointIntentEnum.kPlanarFaceCenterPointIntent);

                fingerboardPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)fingerboardPartComponentOccurrence.Definition).Features.ExtrudeFeatures[i].StartFaces[1],
                    out faceProxy);

                GeometryIntent fretPartIntent2 = _assemblyDocument.ComponentDefinition.CreateGeometryIntent(faceProxy as Face,
                    PointIntentEnum.kPlanarFaceCenterPointIntent);

                AssemblyJointDefinition fingerboardAndFretJointDefinition =
                    _assemblyDocument.ComponentDefinition.Joints.CreateAssemblyJointDefinition(
                        AssemblyJointTypeEnum.kRigidJointType, fretPartIntent2, fretPartIntent);
                fingerboardAndFretJointDefinition.FlipOriginDirection = true;

                _assemblyDocument.ComponentDefinition.Joints.Add(fingerboardAndFretJointDefinition);
            }

            #endregion

            #region tunerOccurence

            ComponentOccurrence tunerPartComponentOccurrence = _assemblyDocument.ComponentDefinition.Occurrences.AddByComponentDefinition(
                (ComponentDefinition)_parts.First(part => part.GetType() == typeof(TunerPart)).PartDocumentComponentDefinition,
                _inventorConnector.InventorApplication.TransientGeometry.CreateMatrix());

            ComponentOccurrence tunerTopPartComponentOccurrence = _assemblyDocument.ComponentDefinition.Occurrences.Add(
                @"C:\Users\Станислав\Documents\Visual Studio 2015\Projects\GuitarNeckBuilder\GuitarNeckBuilder\bin\Debug\tuner_top.ipt",
                _inventorConnector.InventorApplication.TransientGeometry.CreateMatrix());

            tunerPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)tunerPartComponentOccurrence.Definition).Features.LoftFeatures[2].EndFace,
                    out faceProxy);

            GeometryIntent tunerPartIntent1 = _assemblyDocument.ComponentDefinition.CreateGeometryIntent(faceProxy as Face,
                PointIntentEnum.kPlanarFaceCenterPointIntent);

            tunerTopPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)tunerTopPartComponentOccurrence.Definition).Features.ExtrudeFeatures[1].SideFaces[6],
                    out faceProxy);

            GeometryIntent tunerTopPartIntent = _assemblyDocument.ComponentDefinition.CreateGeometryIntent(faceProxy as Face,
                PointIntentEnum.kPlanarFaceCenterPointIntent);

            AssemblyJointDefinition tunerAndTunerTopJointDefinition =
                    _assemblyDocument.ComponentDefinition.Joints.CreateAssemblyJointDefinition(
                        AssemblyJointTypeEnum.kRotationalJointType, tunerTopPartIntent, tunerPartIntent1);
            tunerAndTunerTopJointDefinition.FlipOriginDirection = true;
            tunerAndTunerTopJointDefinition.AngularPosition = Math.PI *
                ((TunerPart)_parts.First(part => part.GetType() == typeof(TunerPart))).TunerAngle / 180;

            _assemblyDocument.ComponentDefinition.Joints.Add(tunerAndTunerTopJointDefinition);

            headstockPartComponentOccurrence.CreateGeometryProxy(
                ((HeadstockPart)_parts.First(part => part.GetType() == typeof(HeadstockPart))).IsReversed ?
                    ((PartComponentDefinition)headstockPartComponentOccurrence.Definition).Features.ExtrudeFeatures[2].Faces[1].Edges[2]
                    : ((PartComponentDefinition)headstockPartComponentOccurrence.Definition).Features.ExtrudeFeatures[2].Faces[1].Edges[1],
                    out faceProxy);

            GeometryIntent headstockPartIntent2 = _assemblyDocument.ComponentDefinition.CreateGeometryIntent(faceProxy as Edge);

            tunerPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)tunerPartComponentOccurrence.Definition).Features.ExtrudeFeatures[2].StartFaces[1],
                    out faceProxy);

            GeometryIntent tunerPartIntent2 = _assemblyDocument.ComponentDefinition.CreateGeometryIntent(faceProxy as Face,
                PointIntentEnum.kPlanarFaceCenterPointIntent);

            AssemblyJointDefinition tunerAndHeadstockJointDefinition =
                    _assemblyDocument.ComponentDefinition.Joints.CreateAssemblyJointDefinition(
                        AssemblyJointTypeEnum.kRotationalJointType, tunerPartIntent2, headstockPartIntent2);
            tunerAndHeadstockJointDefinition.FlipOriginDirection = true;

            tunerPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)tunerPartComponentOccurrence.Definition).Features.LoftFeatures[2].EndFace,
                    out faceProxy);

            tunerAndHeadstockJointDefinition.AlignmentOne = faceProxy;

            headstockPartComponentOccurrence.CreateGeometryProxy(
                ((HeadstockPart)_parts.First(part => part.GetType() == typeof(HeadstockPart))).IsReversed ?
                    ((PartComponentDefinition)headstockPartComponentOccurrence.Definition).Features.ExtrudeFeatures[1].SideFaces[4]
                    : ((PartComponentDefinition)headstockPartComponentOccurrence.Definition).Features.ExtrudeFeatures[1].SideFaces[3],
                    out faceProxy);

            tunerAndHeadstockJointDefinition.AlignmentTwo = faceProxy;

            _assemblyDocument.ComponentDefinition.Joints.Add(tunerAndHeadstockJointDefinition);

            ObjectCollection tunerPartParentComponentsObjectCollection =
                _inventorConnector.InventorApplication.TransientObjects.CreateObjectCollection();
            tunerPartParentComponentsObjectCollection.Add(tunerPartComponentOccurrence);
            tunerPartParentComponentsObjectCollection.Add(tunerTopPartComponentOccurrence);

            object tunersHolesRectangularPatternFeatureProxy;
            headstockPartComponentOccurrence.CreateGeometryProxy(
                ((PartComponentDefinition)headstockPartComponentOccurrence.Definition).Features.RectangularPatternFeatures[1],
                out tunersHolesRectangularPatternFeatureProxy);

            _assemblyDocument.ComponentDefinition.OccurrencePatterns.AddFeatureBasedPattern(tunerPartParentComponentsObjectCollection,
                tunersHolesRectangularPatternFeatureProxy as PartFeature);

            #endregion
        }

        /// <summary>
        /// Метод постройки всех деталей
        /// </summary>
        private void BuildParts()
        {
            foreach (var part in _parts)
            {
                part.Build();
            }
        }

        /// <summary>
        /// Метод закрытия документа сборки, документов деталей и инвентора
        /// </summary>
        public void Close()
        {
            _assemblyDocument?.Close(true);
            foreach (var part in _parts)
            {
                part?.Close();
            }
            //_inventorConnector.InventorApplication.Quit();
        }
    }
}
