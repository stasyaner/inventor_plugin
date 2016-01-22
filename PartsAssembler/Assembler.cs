using System.Collections.Generic;
using System.Linq;
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
                new NeckPart(settings.First(setting => setting.GetType() == typeof(NeckSettings)), _inventorConnector),
                new FingerboardPart(settings.First(setting => setting.GetType() == typeof (FingerboardSettings)), _inventorConnector),
                new FretPart(settings.First(setting => setting.GetType() == typeof(FretSettings)), _inventorConnector),
                new InlayPart(settings.First(setting => setting.GetType() == typeof(InlaySettings)), _inventorConnector),
                new HeadstockPart(settings.First(setting => setting.GetType() == typeof(HeadstockSettings)), _inventorConnector)
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

            if (((HeadstockPart)_parts.First(part => part.GetType() == typeof(HeadstockPart))).Reversed)
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

            if (((HeadstockPart)_parts.First(part => part.GetType() == typeof(HeadstockPart))).Reversed)
            {
                //neckAndHeadstockJointDefinition.FlipAlignmentDirection = true;
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

            if (((InlayPart)_parts.First(part => part.GetType() == typeof(InlayPart))).Active)
            {
                ComponentOccurrence inlayPartComponentOccurrence = _assemblyDocument.ComponentDefinition.Occurrences
                    .AddByComponentDefinition(
                        (ComponentDefinition)_parts.First(part => part.GetType() == typeof(InlayPart)).PartDocumentComponentDefinition,
                        _inventorConnector.InventorApplication.TransientGeometry.CreateMatrix());

                inlayPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)inlayPartComponentOccurrence.Definition).Features.ExtrudeFeatures[1].StartFaces[1], out faceProxy);

                GeometryIntent inlayPartIntent = _assemblyDocument.ComponentDefinition.CreateGeometryIntent(faceProxy as Face,
                    PointIntentEnum.kPlanarFaceCenterPointIntent);

                fingerboardPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)fingerboardPartComponentOccurrence.Definition).Features.ExtrudeFeatures[2].StartFaces[1],
                    out faceProxy);

                GeometryIntent fingerboardPartIntent2 = _assemblyDocument.ComponentDefinition.CreateGeometryIntent(faceProxy as Face,
                    PointIntentEnum.kPlanarFaceCenterPointIntent);

                AssemblyJointDefinition fingerboardAndInlayJointDefinition =
                    _assemblyDocument.ComponentDefinition.Joints.CreateAssemblyJointDefinition(
                        AssemblyJointTypeEnum.kRigidJointType, fingerboardPartIntent2, inlayPartIntent);
                fingerboardAndInlayJointDefinition.FlipOriginDirection = true;

                _assemblyDocument.ComponentDefinition.Joints.Add(fingerboardAndInlayJointDefinition);

                //Массив инкрустации
                ObjectCollection inlayPartParentComponentsObjectCollection = 
                    _inventorConnector.InventorApplication.TransientObjects.CreateObjectCollection();
                inlayPartParentComponentsObjectCollection.Add(inlayPartComponentOccurrence);

                object inlayRectangularPatternFeatureProxy;
                fingerboardPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)fingerboardPartComponentOccurrence.Definition).Features.RectangularPatternFeatures[2],
                    out inlayRectangularPatternFeatureProxy);

                _assemblyDocument.ComponentDefinition.OccurrencePatterns.AddFeatureBasedPattern(inlayPartParentComponentsObjectCollection,
                    inlayRectangularPatternFeatureProxy as PartFeature);
            }

            #endregion

            #region fretOccurence

            ComponentOccurrence fretPartComponentOccurrence = _assemblyDocument.ComponentDefinition.Occurrences.AddByComponentDefinition(
                (ComponentDefinition)_parts.First(part => part.GetType() == typeof(FretPart)).PartDocumentComponentDefinition,
                _inventorConnector.InventorApplication.TransientGeometry.CreateMatrix());

            fretPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)fretPartComponentOccurrence.Definition).Features.ExtrudeFeatures[1].SideFaces[1],
                    out faceProxy);

            GeometryIntent fretPartIntent = _assemblyDocument.ComponentDefinition.CreateGeometryIntent(faceProxy as Face,
                PointIntentEnum.kPlanarFaceCenterPointIntent);

            fingerboardPartComponentOccurrence.CreateGeometryProxy(
                ((PartComponentDefinition)fingerboardPartComponentOccurrence.Definition).Features.ExtrudeFeatures[1].StartFaces[1],
                out faceProxy);

            GeometryIntent fretPartIntent2 = _assemblyDocument.ComponentDefinition.CreateGeometryIntent(faceProxy as Face,
                PointIntentEnum.kPlanarFaceCenterPointIntent);

            AssemblyJointDefinition fingerboardAndFretJointDefinition =
                _assemblyDocument.ComponentDefinition.Joints.CreateAssemblyJointDefinition(
                    AssemblyJointTypeEnum.kRigidJointType, fretPartIntent2, fretPartIntent);
            fingerboardAndFretJointDefinition.FlipOriginDirection = true;

            _assemblyDocument.ComponentDefinition.Joints.Add(fingerboardAndFretJointDefinition);

            //Массив инкрустации
            ObjectCollection fretPartParentComponentsObjectCollection = 
                _inventorConnector.InventorApplication.TransientObjects.CreateObjectCollection();
            fretPartParentComponentsObjectCollection.Add(fretPartComponentOccurrence);

            object fretRectangularPatternFeatureProxy;
            fingerboardPartComponentOccurrence.CreateGeometryProxy(
                ((PartComponentDefinition)fingerboardPartComponentOccurrence.Definition).Features.RectangularPatternFeatures[1],
                out fretRectangularPatternFeatureProxy);

            _assemblyDocument.ComponentDefinition.OccurrencePatterns.AddFeatureBasedPattern(fretPartParentComponentsObjectCollection,
                fretRectangularPatternFeatureProxy as PartFeature);

            #endregion
        }

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
            _assemblyDocument.Close(true);
            foreach (var part in _parts)
            {
                part.Close();
            }
            //_inventorConnector.InventorApplication.Quit();
        }
    }
}
