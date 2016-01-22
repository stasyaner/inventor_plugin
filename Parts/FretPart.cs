using System;
using Inventor;
using InventorAPI;
using Settings;

namespace Parts
{
    /// <summary>
    /// Класс детали лада
    /// </summary>
    public class FretPart : IPart
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
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="inventorConnector"></param>
        public FretPart(ISettings settings, InventorConnector inventorConnector)
        {
            _settings = settings;
            _inventorConnector = inventorConnector;
            _partDoc = (PartDocument)inventorConnector.InventorApplication.Documents.Add(
                DocumentTypeEnum.kPartDocumentObject, inventorConnector.InventorApplication.FileManager.GetTemplateFile(
                    DocumentTypeEnum.kPartDocumentObject, SystemOfMeasureEnum.kMetricSystemOfMeasure));
        }

        /// <summary>
        /// Метод построения лада
        /// </summary>
        public void Build()
        {
            #region directrixSketch

            PlanarSketch directrixSketch = _inventorConnector.MakeNewSketch(2, 0, _partDoc);

            Point2d directrixPoint1 = _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                _settings.GetSetting(SettingName.AtNutWidth) / -2.0 - 0.05);
            Point2d directrixPoint2 = _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                _settings.GetSetting(SettingName.AtNutWidth) / 2.0 + 0.05);
            Point2d directrixPoint0 = _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                0, Math.Sqrt(Math.Pow(_settings.GetSetting(SettingName.FingerboardRadius), 2) -
                          Math.Pow(_settings.GetSetting(SettingName.AtNutWidth) / 2.0 + 0.1, 2)) * -1);

            SketchArc fretDirectrixArc = directrixSketch.SketchArcs.AddByCenterStartEndPoint(directrixPoint0, directrixPoint2, directrixPoint1);

            #endregion

            #region fretTopSketch

            PlanarSketch fretTopSketch = _inventorConnector.MakeNewSketch(1, 0, _partDoc);
            
            Point2d fretTopPoint1 = _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
               1.025 - _settings.GetSetting(SettingName.FretWidth) / 2, 0.05);
            Point2d fretTopPoint2 = _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
               1.025 + _settings.GetSetting(SettingName.FretWidth) / 2, 0.05);
            Point2d fretTopPoint3 = _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
               1.025, 0.05 + _settings.GetSetting(SettingName.FretHeight));
            
            SketchLine fretLine1 = fretTopSketch.SketchLines.AddByTwoPoints(fretTopPoint1, fretTopPoint2);

            fretTopSketch.SketchArcs.AddByThreePoints(fretLine1.EndSketchPoint, fretTopPoint3, fretLine1.StartSketchPoint);

            SweepDefinition fretSweepDefinition = PartDocumentComponentDefinition.Features.SweepFeatures.CreateSweepDefinition(
                SweepTypeEnum.kPathSweepType, fretTopSketch.Profiles.AddForSolid(),
                PartDocumentComponentDefinition.Features.SweepFeatures.CreatePath(fretDirectrixArc),
                PartFeatureOperationEnum.kNewBodyOperation);

            SweepFeature fretSweepFeature = PartDocumentComponentDefinition.Features.SweepFeatures.Add(fretSweepDefinition);

            #endregion

            #region fretBottomSketch

            WorkPoint fretTopBottomLineCenterPoint = PartDocumentComponentDefinition.WorkPoints.AddAtCentroid(fretSweepFeature.StartFaces[1].Edges[2]);
            WorkPlane fretBottomPlane = PartDocumentComponentDefinition.WorkPlanes.AddByPlaneAndPoint(
                PartDocumentComponentDefinition.WorkPlanes[2], fretTopBottomLineCenterPoint);
            fretBottomPlane.Visible = false;
            fretTopBottomLineCenterPoint.Visible = false;

            PlanarSketch fretBottomSketch = PartDocumentComponentDefinition.Sketches.Add(fretBottomPlane);

            Point2d fretBottomPoint1 = _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d();
            Point2d fretBottomPoint2 = _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d(0, -0.05);
            Point2d fretBottomPoint3 = _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                _settings.GetSetting(SettingName.AtNutWidth) * -1 - 0.1);
            Point2d fretBottomPoint4 = _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                _settings.GetSetting(SettingName.AtNutWidth) * -1 - 0.1, -0.05);
            Point2d fretBottomPoint5 = _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                _settings.GetSetting(SettingName.AtNutWidth) / -2 - 0.05,
                Math.Sqrt(Math.Pow(_settings.GetSetting(SettingName.FingerboardRadius), 2) -
                          Math.Pow(_settings.GetSetting(SettingName.AtNutWidth) / 2.0 + 0.1, 2)) * -1);

            SketchLine fretBottomSketchLine1 = fretBottomSketch.SketchLines.AddByTwoPoints(fretBottomPoint1, fretBottomPoint2);
            SketchLine fretBottomSketchLine2 = fretBottomSketch.SketchLines.AddByTwoPoints(fretBottomSketchLine1.EndSketchPoint, fretBottomPoint4);
            SketchLine fretBottomSketchLine3 = fretBottomSketch.SketchLines.AddByTwoPoints(fretBottomSketchLine2.EndSketchPoint, fretBottomPoint3);
            SketchLine fretBottomSketchLine4 = fretBottomSketch.SketchLines.AddByTwoPoints(fretBottomPoint1, fretBottomPoint4);
            fretBottomSketch.SketchArcs.AddByCenterStartEndPoint(fretBottomPoint5, fretBottomSketchLine1.StartSketchPoint,
                fretBottomSketchLine3.EndSketchPoint);

            ExtrudeDefinition fretBottomExtrudeDefinition = PartDocumentComponentDefinition.Features.ExtrudeFeatures.CreateExtrudeDefinition(
                fretBottomSketch.Profiles.AddForSolid(), PartFeatureOperationEnum.kNewBodyOperation);
            fretBottomExtrudeDefinition.SetDistanceExtent(0.05, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection);
            PartDocumentComponentDefinition.Features.ExtrudeFeatures.Add(fretBottomExtrudeDefinition);

            #endregion
            
            _inventorConnector.ChangeMaterial(_partDoc, @"Aluminum 6061");
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