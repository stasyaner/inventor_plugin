using System;
using Inventor;
using InventorAPI;
using Settings;

namespace Parts
{
    /// <summary>
    /// Класс головы грифа
    /// </summary>
    public class HeadstockPart : IPart
    {
        /// <summary>
        /// Поле ссылки на коннектор к инвентору
        /// </summary>
        private readonly InventorConnector _invetorConnector;

        /// <summary>
        /// Ссылка на документ детали.
        /// </summary>
        private readonly PartDocument _partDoc;

        /// <summary>
        /// Ссылка на настройки детали
        /// </summary>
        private readonly ISettings _settings;

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="inventorConnector"></param>
        public HeadstockPart(ISettings settings, InventorConnector inventorConnector)
        {
            _settings = settings;
            _invetorConnector = inventorConnector;
            _partDoc = (PartDocument)inventorConnector.InventorApplication.Documents.Add(
                DocumentTypeEnum.kPartDocumentObject, inventorConnector.InventorApplication.FileManager.GetTemplateFile(
                    DocumentTypeEnum.kPartDocumentObject, SystemOfMeasureEnum.kMetricSystemOfMeasure));
        }

        /// <summary>
        /// Метод построения головы грифа
        /// </summary>
        public void Build()
        {
            //Создаем скетч на рабочей плоскости XY.
            PlanarSketch headstockSketch = _invetorConnector.MakeNewSketch(3, 0, _partDoc);

            // Создаем точки
            Point2d headstockPoint0 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d();
            Point2d headstockPoint1 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                0, _settings.GetSetting(SettingName.AtNutWidth));
            Point2d headstockPoint2 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                1.37, _settings.GetSetting(SettingName.AtNutWidth) - 0.15);
            Point2d headstockPoint3 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                2.2, _settings.GetSetting(SettingName.AtNutWidth) + 1.6);
            Point2d headstockPoint4 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                13.5, -1.1);
            Point2d headstockPoint5 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                10, -2);
            Point2d headstockPoint6 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                5.2, -0.5);

            //Рисуем линию
            SketchLine headstockSketchLine1 = headstockSketch.SketchLines.AddByTwoPoints(headstockPoint0, headstockPoint1);

            //Сплайн по управляемым точкам
            ObjectCollection headstockSketchSplinePointsObjectCollection1 = _invetorConnector.InventorApplication
                .TransientObjects.CreateObjectCollection();
            headstockSketchSplinePointsObjectCollection1.Add(headstockSketchLine1.EndSketchPoint);
            headstockSketchSplinePointsObjectCollection1.Add(headstockPoint2);
            headstockSketchSplinePointsObjectCollection1.Add(headstockPoint3);
            SketchControlPointSpline headstockSketchControlPointSpline1 = headstockSketch.SketchControlPointSplines.Add(
                headstockSketchSplinePointsObjectCollection1);

            //Рисуем линии
            SketchLine headstockSketchLine2 =
                headstockSketch.SketchLines.AddByTwoPoints(headstockSketchControlPointSpline1.EndSketchPoint,
                    headstockPoint4);
            SketchLine headstockSketchLine3 =
                headstockSketch.SketchLines.AddByTwoPoints(headstockSketchLine2.EndSketchPoint,
                    headstockPoint5);

            //Сплайн по управляемым точкам
            ObjectCollection headstockSketchSplinePointsObjectCollection2 = _invetorConnector.InventorApplication
                .TransientObjects.CreateObjectCollection();
            headstockSketchSplinePointsObjectCollection2.Add(headstockSketchLine3.EndSketchPoint);
            headstockSketchSplinePointsObjectCollection2.Add(headstockPoint6);
            headstockSketchSplinePointsObjectCollection2.Add(headstockSketchLine1.StartSketchPoint);
            SketchControlPointSpline headstockSketchControlPointSpline2 = headstockSketch.SketchControlPointSplines.Add(
                headstockSketchSplinePointsObjectCollection2);

            //Выдавливаем
            ExtrudeDefinition headstockExtrudeDefinition = _partDoc.ComponentDefinition.Features.ExtrudeFeatures
                .CreateExtrudeDefinition(headstockSketch.Profiles.AddForSolid(), PartFeatureOperationEnum.kNewBodyOperation);
            headstockExtrudeDefinition.SetDistanceExtent(_settings.GetSetting(SettingName.AtNutHeight),
                PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            ExtrudeFeature headExtrudeFeature = _partDoc.ComponentDefinition.Features.ExtrudeFeatures.Add(headstockExtrudeDefinition);

            //Сопряжение1
            EdgeCollection headstockEdgeCollection1 = _invetorConnector.InventorApplication.TransientObjects.CreateEdgeCollection();
            headstockEdgeCollection1.Add(headExtrudeFeature.StartFaces[1].Edges[2]);
            FilletDefinition headstockFilletDefinition1 = _partDoc.ComponentDefinition.Features.FilletFeatures.CreateFilletDefinition();
            headstockFilletDefinition1.AddVariableRadiusEdgeSet(headstockEdgeCollection1, 0.8, 0.3);

            //Сопряжение2
            EdgeCollection headstockEdgeCollection2 = _invetorConnector.InventorApplication.TransientObjects.CreateEdgeCollection();
            headstockEdgeCollection2.Add(headExtrudeFeature.StartFaces[1].Edges[5]);
            headstockFilletDefinition1.AddVariableRadiusEdgeSet(headstockEdgeCollection2, 0.3, 0.8);

            //Примиение сопряжений
            _partDoc.ComponentDefinition.Features.FilletFeatures.Add(headstockFilletDefinition1);

            #region material

            //Меняем материал
            string materialName = @"Maple";
            switch (Convert.ToByte(_settings.GetSetting(SettingName.Material)))
            {
                case 0:
                    materialName = @"Mahogany";
                    break;
                case 1:
                    materialName = @"Maple";
                    break;
                case 2:
                    materialName = @"Ash";
                    break;
            }
            _invetorConnector.ChangeMaterial(_partDoc, materialName);

            #endregion
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