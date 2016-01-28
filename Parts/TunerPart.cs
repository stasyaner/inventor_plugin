using System;
using Inventor;
using InventorAPI;
using Settings;

namespace Parts
{
    public class TunerPart : IPart
    {
        /// <summary>
        /// Поле ссылки на коннектор к инвентору
        /// </summary>
        private readonly InventorConnector _inventorConnector;

        /// <summary>
        /// Ссылка на документ детали.
        /// </summary>
        private readonly PartDocument _partDoc;

        /// <summary>
        /// Ссылка на описание компонентов документа детали
        /// </summary>
        public PartComponentDefinition PartDocumentComponentDefinition => _partDoc.ComponentDefinition;

        /// <summary>
        /// Ссылка на настройки детали
        /// </summary>
        private readonly ISettings _settings;

        /// <summary>
        /// Угол поворота колков
        /// </summary>
        public double TunerAngle => _settings.GetSetting(SettingName.TunerAngle);

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="inventorConnector"></param>
        public TunerPart(ISettings settings, InventorConnector inventorConnector)
        {
            _settings = settings;
            _inventorConnector = inventorConnector;
            _partDoc = (PartDocument)inventorConnector.InventorApplication.Documents.Add(
                DocumentTypeEnum.kPartDocumentObject, inventorConnector.InventorApplication.FileManager.GetTemplateFile(
                    DocumentTypeEnum.kPartDocumentObject, SystemOfMeasureEnum.kMetricSystemOfMeasure));
        }

        /// <summary>
        /// Метод построения колка
        /// </summary>
        public void Build()
        {
            #region tunerSketch1

            PlanarSketch tunerSketch1 = _inventorConnector.MakeNewSketch(3, 0, _partDoc);

            Point2d tunerCenterPoint = _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d();

            tunerSketch1.SketchCircles.AddByCenterRadius(tunerCenterPoint, 0.5);

            ExtrudeDefinition tunerExtrudeDefinition1 = PartDocumentComponentDefinition.Features.ExtrudeFeatures
                .CreateExtrudeDefinition(tunerSketch1.Profiles.AddForSolid(), PartFeatureOperationEnum.kNewBodyOperation);
            tunerExtrudeDefinition1.SetDistanceExtent(0.5, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            ExtrudeFeature tunerExtrudeFeature1 = PartDocumentComponentDefinition.Features.ExtrudeFeatures.Add(tunerExtrudeDefinition1);

            #endregion

            #region tunerSketch2

            WorkPlane tunerSketchWorkPlane2 =
                PartDocumentComponentDefinition.WorkPlanes.AddByPlaneAndOffset(tunerExtrudeFeature1.EndFaces[1], 0);
            tunerSketchWorkPlane2.Visible = false;

            PlanarSketch tunerSketch2 = PartDocumentComponentDefinition.Sketches.Add(tunerSketchWorkPlane2);

            tunerSketch2.SketchCircles.AddByCenterRadius(tunerCenterPoint, 0.35);

            ExtrudeDefinition tunerExtrudeDefinition2 = PartDocumentComponentDefinition.Features.ExtrudeFeatures
                .CreateExtrudeDefinition(tunerSketch2.Profiles.AddForSolid(), PartFeatureOperationEnum.kNewBodyOperation);
            tunerExtrudeDefinition2.SetDistanceExtent(0.1, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            ExtrudeFeature tunerExtrudeFeature2 = PartDocumentComponentDefinition.Features.ExtrudeFeatures.Add(tunerExtrudeDefinition2);

            #endregion

            #region tunerSketch3

            WorkPlane tunerSketchWorkPlane3 = 
                PartDocumentComponentDefinition.WorkPlanes.AddByPlaneAndOffset(tunerExtrudeFeature2.EndFaces[1], 0);
            tunerSketchWorkPlane3.Visible = false;

            PlanarSketch tunerSketch3 = PartDocumentComponentDefinition.Sketches.Add(tunerSketchWorkPlane3);
            
            tunerSketch3.SketchCircles.AddByCenterRadius(tunerCenterPoint, 0.1);

            ExtrudeDefinition tunerExtrudeDefinition3 = PartDocumentComponentDefinition.Features.ExtrudeFeatures
                .CreateExtrudeDefinition(tunerSketch3.Profiles.AddForSolid(), PartFeatureOperationEnum.kNewBodyOperation);
            tunerExtrudeDefinition3.SetDistanceExtent(
                _settings.GetSetting(SettingName.AtNutHeight) + 0.5, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            ExtrudeFeature tunerExtrudeFeature3 = PartDocumentComponentDefinition.Features.ExtrudeFeatures.Add(tunerExtrudeDefinition3);

            #endregion

            #region tunerSketch4

            WorkPlane tunerSketchWorkPlane4 = PartDocumentComponentDefinition.WorkPlanes.AddByPlaneAndOffset(tunerExtrudeFeature1.EndFaces[1],
                _settings.GetSetting(SettingName.AtNutHeight));
            tunerSketchWorkPlane4.Visible = false;

            PlanarSketch tunerSketch4 = PartDocumentComponentDefinition.Sketches.Add(tunerSketchWorkPlane4);
            
            tunerSketch4.SketchCircles.AddByCenterRadius(tunerCenterPoint, 0.4);

            ExtrudeDefinition tunerExtrudeDefinition4 = PartDocumentComponentDefinition.Features.ExtrudeFeatures
                .CreateExtrudeDefinition(tunerSketch4.Profiles.AddForSolid(), PartFeatureOperationEnum.kNewBodyOperation);
            tunerExtrudeDefinition4.SetDistanceExtent(0.01, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            ExtrudeFeature tunerExtrudeFeature4 = PartDocumentComponentDefinition.Features.ExtrudeFeatures.Add(tunerExtrudeDefinition4);

            #endregion

            #region tunerSketch5

            WorkPlane tunerSketchWorkPlane5 = PartDocumentComponentDefinition.WorkPlanes.AddByPlaneAndOffset(tunerExtrudeFeature1.EndFaces[1],
                _settings.GetSetting(SettingName.AtNutHeight));
            tunerSketchWorkPlane5.Visible = false;

            PlanarSketch tunerSketch5 = PartDocumentComponentDefinition.Sketches.Add(tunerSketchWorkPlane5);

            tunerSketch5.SketchLines.AddAsPolygon(6, tunerCenterPoint, 
                _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d(0, 0.3), true);

            ExtrudeDefinition tunerExtrudeDefinition5 = PartDocumentComponentDefinition.Features.ExtrudeFeatures
                .CreateExtrudeDefinition(tunerSketch5.Profiles.AddForSolid(), PartFeatureOperationEnum.kNewBodyOperation);
            tunerExtrudeDefinition5.SetDistanceExtent(0.08, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            ExtrudeFeature tunerExtrudeFeature5 = PartDocumentComponentDefinition.Features.ExtrudeFeatures.Add(tunerExtrudeDefinition5);

            #endregion

            #region tunerSketch6

            WorkPlane tunerSketchTempWorkPlane = PartDocumentComponentDefinition.WorkPlanes.AddByPlaneAndTangent(
                PartDocumentComponentDefinition.WorkPlanes[2], tunerExtrudeFeature1.SideFaces[1], 
                _inventorConnector.InventorApplication.TransientGeometry.CreatePoint(0, 0, 5));
            tunerSketchTempWorkPlane.Visible = false;

            WorkPlane tunerSketchWorkPlane6 = PartDocumentComponentDefinition.WorkPlanes.AddByPlaneAndOffset(
                tunerSketchTempWorkPlane, 0.3);
            tunerSketchWorkPlane6.Visible = false;

            PlanarSketch tunerSketch6 = PartDocumentComponentDefinition.Sketches.Add(tunerSketchWorkPlane6);

            tunerSketch6.SketchEllipses.Add(
                _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d(0, 0.35),
                _inventorConnector.InventorApplication.TransientGeometry.CreateUnitVector2d(0, 0.5),
                0.06,
                0.2);
            
            ObjectCollection loftObjectCollection1 =
                _inventorConnector.InventorApplication.TransientObjects.CreateObjectCollection();
            loftObjectCollection1.Add(tunerExtrudeFeature1.SideFaces[1].EdgeLoops[1]);
            loftObjectCollection1.Add(tunerSketch6.Profiles.AddForSurface());
            PartDocumentComponentDefinition.Features.LoftFeatures.Add(
                PartDocumentComponentDefinition.Features.LoftFeatures.CreateLoftDefinition(loftObjectCollection1,
                    PartFeatureOperationEnum.kNewBodyOperation));

            #endregion

            #region tunerSketch7

            WorkPlane tunerSketchWorkPlane7 = PartDocumentComponentDefinition.WorkPlanes.AddByPlaneAndOffset(
                tunerSketchWorkPlane6, 0.1);
            tunerSketchWorkPlane7.Visible = false;

            PlanarSketch tunerSketch7 = PartDocumentComponentDefinition.Sketches.Add(tunerSketchWorkPlane7);

            tunerSketch7.SketchCircles.AddByCenterRadius(
                _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d(0, 0.35), 0.05);

            ObjectCollection loftObjectCollection2 =
                _inventorConnector.InventorApplication.TransientObjects.CreateObjectCollection();
            loftObjectCollection2.Add(PartDocumentComponentDefinition.Features.LoftFeatures[1].EndFace.EdgeLoops[1]);
            loftObjectCollection2.Add(tunerSketch7.Profiles.AddForSurface());
            PartDocumentComponentDefinition.Features.LoftFeatures.Add(
                PartDocumentComponentDefinition.Features.LoftFeatures.CreateLoftDefinition(loftObjectCollection2,
                    PartFeatureOperationEnum.kNewBodyOperation));

            #endregion

            #region tunerSketch8

            PlanarSketch tunerTempSketch =
                PartDocumentComponentDefinition.Sketches.Add(PartDocumentComponentDefinition.Features.ExtrudeFeatures[3].EndFaces[1]);
            SketchPoint tunerSketch8SketchPoint = tunerTempSketch.SketchPoints.Add(
                _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d());
            tunerTempSketch.Visible = false;

            WorkPlane tunerSketchTempWorkPlane2 = PartDocumentComponentDefinition.WorkPlanes.AddByPlaneAndPoint(
                PartDocumentComponentDefinition.WorkPlanes[1], tunerSketch8SketchPoint);
            tunerSketchTempWorkPlane2.Visible = false;

            WorkPlane tunerSketchWorkPlane8 = PartDocumentComponentDefinition.WorkPlanes.AddByPlaneAndOffset(
                tunerSketchTempWorkPlane2, 0.1);
            tunerSketchWorkPlane8.Visible = false;

            PlanarSketch tunerSketch8 = PartDocumentComponentDefinition.Sketches.Add(tunerSketchWorkPlane8);

            tunerSketch8.SketchCircles.AddByCenterRadius(
                _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d(0, -0.2), 0.03);

            ExtrudeDefinition tunerExtrudeDefinition8 = PartDocumentComponentDefinition.Features.ExtrudeFeatures
                .CreateExtrudeDefinition(tunerSketch8.Profiles.AddForSolid(), PartFeatureOperationEnum.kCutOperation);
            ObjectCollection lala = _inventorConnector.InventorApplication.TransientObjects.CreateObjectCollection();
            lala.Add(PartDocumentComponentDefinition.Features.ExtrudeFeatures[3].SurfaceBody);
            tunerExtrudeDefinition8.AffectedBodies = lala;
            tunerExtrudeDefinition8.SetDistanceExtent(0.2, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);
            ExtrudeFeature tunerExtrudeFeature8 = PartDocumentComponentDefinition.Features.ExtrudeFeatures.Add(tunerExtrudeDefinition8);

            #endregion

            #region fillets

            //Сопряжение1
            EdgeCollection tunerEdgeCollection1 = _inventorConnector.InventorApplication.TransientObjects.CreateEdgeCollection();
            tunerEdgeCollection1.Add(tunerExtrudeFeature1.SideFaces[1].Edges[2]);
            tunerEdgeCollection1.Add(tunerExtrudeFeature3.EndFaces[1].Edges[1]);

            FilletDefinition tunerFilletDefinition1 = PartDocumentComponentDefinition.Features.FilletFeatures.CreateFilletDefinition();
            tunerFilletDefinition1.AddConstantRadiusEdgeSet(tunerEdgeCollection1, 0.05);

            //Примиение сопряжений
            PartDocumentComponentDefinition.Features.FilletFeatures.Add(tunerFilletDefinition1);

            #endregion

            #region chamfers

            EdgeCollection tunerEdgeCollection2 = _inventorConnector.InventorApplication.TransientObjects.CreateEdgeCollection();
            foreach (Edge edge in tunerExtrudeFeature5.EndFaces[1].Edges)
            {
                tunerEdgeCollection2.Add(edge);
            }

            PartDocumentComponentDefinition.Features.ChamferFeatures.AddUsingDistance(tunerEdgeCollection2, 0.02, true, true, false);

            #endregion

            _inventorConnector.ChangeMaterial(_partDoc, "Silver");
        }

        /// <summary>
        /// Метод, который закрывает текущий документ без сохранения.
        /// </summary>
        public void Close()
        {
            _partDoc.Close(true);
        }
    }
}