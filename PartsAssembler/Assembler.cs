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
                new NeckPart(settings.First(setting => setting.GetType() == typeof (NeckSettings)), _inventorConnector),
                new FingerboardPart(settings.First(setting => setting.GetType() == typeof (FingerboardSettings)), _inventorConnector),
                //new FretPart(settings.First(setting => setting.GetType() == typeof(FretSettings)), _inventorConnector),
                //new InlayPart(settings.First(setting => setting.GetType() == typeof(InlaySettings)), _inventorConnector),
                new HeadstockPart(settings.First(setting => setting.GetType() == typeof (HeadstockSettings)), _inventorConnector)
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

            object face; //Для метода CreateGeometryProxy

            #region neckOccurrence

            Inventor.Matrix neckMatrix = _inventorConnector.InventorApplication.TransientGeometry.CreateMatrix();
            ComponentOccurrence neckPartComponentOccurrence = _assemblyDocument.ComponentDefinition.Occurrences.AddByComponentDefinition(
                (ComponentDefinition)_parts.First(part => part.GetType() == typeof(NeckPart)).PartDocumentComponentDefinition, neckMatrix);

            #endregion

            #region fingerboardOccurence

            Inventor.Matrix fingerboardMatrix = _inventorConnector.InventorApplication.TransientGeometry.CreateMatrix();
            //fingerboardMatrix.Cell[1, 4] = 50; //[1, 4] - смещение по иксу, [2,4] - по игрику и тд
            ComponentOccurrence fingerboardPartComponentOccurrence = _assemblyDocument.ComponentDefinition.Occurrences
                .AddByComponentDefinition(
                    (ComponentDefinition)(_parts.First(part => part.GetType() == typeof(FingerboardPart)).PartDocumentComponentDefinition),
                    fingerboardMatrix);

            neckPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)neckPartComponentOccurrence.Definition).Features.LoftFeatures[1].Faces[6], out face);

            GeometryIntent neckPartIntent1 = _assemblyDocument.ComponentDefinition.CreateGeometryIntent(face as Face,
                PointIntentEnum.kPlanarFaceCenterPointIntent);

            fingerboardPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)fingerboardPartComponentOccurrence.Definition).Features.LoftFeatures[1].Faces[26], out face);

            GeometryIntent fingerboardPartIntent = _assemblyDocument.ComponentDefinition.CreateGeometryIntent(face as Face,
                PointIntentEnum.kPlanarFaceCenterPointIntent);

            AssemblyJointDefinition neckAndFingerboardJointDefinition =
                _assemblyDocument.ComponentDefinition.Joints.CreateAssemblyJointDefinition(
                    AssemblyJointTypeEnum.kRigidJointType, fingerboardPartIntent, neckPartIntent1);
            neckAndFingerboardJointDefinition.FlipOriginDirection = true;
            neckAndFingerboardJointDefinition.FlipAlignmentDirection = true;

            _assemblyDocument.ComponentDefinition.Joints.Add(neckAndFingerboardJointDefinition);

            #endregion

            #region headstockOccurence

            Inventor.Matrix headstockMatrix = _inventorConnector.InventorApplication.TransientGeometry.CreateMatrix();
            //headstockMatrix.Cell[1, 4] = 50; //[1, 4] - смещение по иксу, [2,4] - по игрику и тд
            ComponentOccurrence headstockPartComponentOccurrence = _assemblyDocument.ComponentDefinition.Occurrences
                .AddByComponentDefinition(
                    (ComponentDefinition)_parts.First(part => part.GetType() == typeof(HeadstockPart)).PartDocumentComponentDefinition,
                    headstockMatrix);

            neckPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)neckPartComponentOccurrence.Definition).Features.LoftFeatures[1].Faces[2], out face);

            GeometryIntent neckPartIntent2 = _assemblyDocument.ComponentDefinition.CreateGeometryIntent(face as Face,
                PointIntentEnum.kPlanarFaceCenterPointIntent);

            if (((HeadstockPart) _parts.First(part => part.GetType() == typeof (HeadstockPart))).Reversed)
            {
                headstockPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)headstockPartComponentOccurrence.Definition).Features.ExtrudeFeatures[1].SideFaces[1], out face);
            }
            else
            {
                headstockPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition) headstockPartComponentOccurrence.Definition).Features.ExtrudeFeatures[1].Faces[5], out face);
            }

            GeometryIntent headstockPartIntent = _assemblyDocument.ComponentDefinition.CreateGeometryIntent(face as Face,
                PointIntentEnum.kPlanarFaceCenterPointIntent);

            AssemblyJointDefinition neckAndHeadstockJointDefinition =
                _assemblyDocument.ComponentDefinition.Joints.CreateAssemblyJointDefinition(
                    AssemblyJointTypeEnum.kRigidJointType, headstockPartIntent, neckPartIntent2);

            neckAndHeadstockJointDefinition.FlipOriginDirection = true;

            if (((HeadstockPart)_parts.First(part => part.GetType() == typeof(HeadstockPart))).Reversed)
            {
                //neckAndHeadstockJointDefinition.FlipAlignmentDirection = true;
                headstockPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)headstockPartComponentOccurrence.Definition).Features.ExtrudeFeatures[1].StartFaces[1], out face);
            }
            else
            {
                headstockPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)headstockPartComponentOccurrence.Definition).Features.ExtrudeFeatures[1].EndFaces[1], out face);
            }

            
            neckAndHeadstockJointDefinition.AlignmentOne = face;

            neckPartComponentOccurrence.CreateGeometryProxy(
                    ((PartComponentDefinition)neckPartComponentOccurrence.Definition).Features.LoftFeatures[1].Faces[6], out face);
            neckAndHeadstockJointDefinition.AlignmentTwo = face;

            _assemblyDocument.ComponentDefinition.Joints.Add(neckAndHeadstockJointDefinition);

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
            _inventorConnector.InventorApplication.Quit();
        }
    }
}
