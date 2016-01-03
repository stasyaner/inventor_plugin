using System;
using Inventor;
using InventorAPI;
using Settings;

namespace Parts
{
    /// <summary>
    /// Класс детали накладки грифа
    /// </summary>
    public class FingerboardPart : IPart
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
        public FingerboardPart(ISettings settings, InventorConnector inventorConnector)
        {
            _settings = settings;
            _invetorConnector = inventorConnector;
            _partDoc = (PartDocument)inventorConnector.InventorApplication.Documents.Add(
                DocumentTypeEnum.kPartDocumentObject, inventorConnector.InventorApplication.FileManager.GetTemplateFile(
                    DocumentTypeEnum.kPartDocumentObject, SystemOfMeasureEnum.kMetricSystemOfMeasure));
        }

        /// <summary>
        /// Метод построения накладки грифа
        /// </summary>
        public void Build()
        {
            #region fingerboardAtNutSketch

            //Создаем скетч на рабочей плоскости XY.
            PlanarSketch fingerboardAtNutSketch = _invetorConnector.MakeNewSketch(3, 0, _partDoc);

            // Создаем точки
            Point2d fingerboardAtNutPoint1 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                _settings.GetSetting(SettingName.AtNutWidth)/-2);
            Point2d fingerboardAtNutPoint2 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                _settings.GetSetting(SettingName.AtNutWidth)/2);
            Point2d fingerboardAtNutPoint3 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                _settings.GetSetting(SettingName.AtNutWidth)/2, 0.2);

            //Рисуем прямоугольник по трем точкам
            fingerboardAtNutSketch.SketchLines.AddAsThreePointRectangle(fingerboardAtNutPoint1,
                fingerboardAtNutPoint2, fingerboardAtNutPoint3);

            #endregion

            #region fingerboardAtTwelveFretSketch

            //Создаем скетч на расстоянии 12-го лада (половины длины грифа) от рабочей плоскости XY.
            PlanarSketch fingerboardAtTwelveFretSketch = _invetorConnector.MakeNewSketch(3,
                _settings.GetSetting(SettingName.Length)/2, _partDoc);

            // Создаем точки
            Point2d fingerboardAtTwelveFretPoint1 = _invetorConnector.InventorApplication.TransientGeometry
                .CreatePoint2d(
                    _settings.GetSetting(SettingName.AtNutWidth)/-2);
            Point2d fingerboardAtTwelveFretPoint2 = _invetorConnector.InventorApplication.TransientGeometry
                .CreatePoint2d(
                    _settings.GetSetting(SettingName.AtNutWidth)/2);
            Point2d fingerboardAtTwelveFretPoint3 = _invetorConnector.InventorApplication.TransientGeometry
                .CreatePoint2d(
                    _settings.GetSetting(SettingName.AtNutWidth)/2, 0.2);

            //Рисуем прямоугольник по трем точкам
            fingerboardAtTwelveFretSketch.SketchLines.AddAsThreePointRectangle(fingerboardAtTwelveFretPoint1,
                fingerboardAtTwelveFretPoint2, fingerboardAtTwelveFretPoint3);

            #endregion

            #region fingerboardAtLastFretFretSketch

            //Создаем скетч на расстоянии длины грифа от рабочей плоскости XY.
            PlanarSketch fingerboardAtLastFretFretSketch = _invetorConnector.MakeNewSketch(3,
                _settings.GetSetting(SettingName.Length), _partDoc);

            // Создаем точки
            Point2d fingerboardAtLastFretPoint1 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                _settings.GetSetting(SettingName.AtNutWidth)/-2);
            Point2d fingerboardAtLastFretPoint2 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                _settings.GetSetting(SettingName.AtNutWidth)/2);
            Point2d fingerboardLastFretPoint3 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                _settings.GetSetting(SettingName.AtNutWidth)/2, 0.2);

            //Рисуем прямоугольник по трем точкам
            fingerboardAtLastFretFretSketch.SketchLines.AddAsThreePointRectangle(fingerboardAtLastFretPoint1,
                fingerboardAtLastFretPoint2, fingerboardLastFretPoint3);

            #endregion

            #region loft

            //Задание описания лофта между тремя скетчами, заданнымми выше
            ObjectCollection loftObjectCollection =
                _invetorConnector.InventorApplication.TransientObjects.CreateObjectCollection();
            loftObjectCollection.Add(fingerboardAtNutSketch.Profiles.AddForSurface());
            loftObjectCollection.Add(fingerboardAtTwelveFretSketch.Profiles.AddForSurface());
            loftObjectCollection.Add(fingerboardAtLastFretFretSketch.Profiles.AddForSurface());
            LoftDefinition skethesLoftDefinition =
                _partDoc.ComponentDefinition.Features.LoftFeatures.CreateLoftDefinition(loftObjectCollection,
                    PartFeatureOperationEnum.kNewBodyOperation);

            //Приминение созданного выше лофта
            _partDoc.ComponentDefinition.Features.LoftFeatures.Add(skethesLoftDefinition);

            #endregion

            #region material

            //Меняем материал
            string materialName;
            switch (Convert.ToByte(_settings.GetSetting(SettingName.FingerboardMaterial)))
            {
                default:
                    materialName = "Maple";
                    break;
                case 0:
                    materialName = "Mahogany";
                    break;
                case 1:
                    materialName = "Maple";
                    break;
                case 2:
                    materialName = "Ash";
                    break;
            }
            _invetorConnector.ChangeMaterial(_partDoc, materialName);

            #endregion

            #region fretHoleSketch

            //Создаем скетч на рабочей плоскости ZX.
            PlanarSketch fretHoleSketch = _invetorConnector.MakeNewSketch(2, 0, _partDoc);

            // Создаем точки
            Point2d fretHolePoint1 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                _settings.GetSetting(SettingName.AtNutWidth) / -1.5, 1.2);
            Point2d fretHolePoint2 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                _settings.GetSetting(SettingName.AtNutWidth) / 1.5, 1.2);
            Point2d fretHolePoint3 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                _settings.GetSetting(SettingName.AtNutWidth) / 1.5, 1.25);

            //Рисуем прямоугольник по трем точкам
            fretHoleSketch.SketchLines.AddAsThreePointRectangle(fretHolePoint1,
                fretHolePoint2, fretHolePoint3);

            //Выдавливаем прямоугольник
            ExtrudeDefinition extrudeDef = _partDoc.ComponentDefinition.Features.ExtrudeFeatures
                .CreateExtrudeDefinition(fretHoleSketch.Profiles.AddForSolid(), PartFeatureOperationEnum.kCutOperation);
            extrudeDef.SetDistanceExtent(0.05, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
            _partDoc.ComponentDefinition.Features.ExtrudeFeatures.Add(extrudeDef);

            //Прямоугольный массив выдавленных прямоугольников
            ObjectCollection fretHoleObjectCollection = _invetorConnector.InventorApplication.TransientObjects.CreateObjectCollection();
            fretHoleObjectCollection.Add(_partDoc.ComponentDefinition.Features.ExtrudeFeatures[1]);
            _partDoc.ComponentDefinition.Features.RectangularPatternFeatures.Add(
                fretHoleObjectCollection, _partDoc.ComponentDefinition.WorkAxes[3],
                true,
                _settings.GetSetting(SettingName.FretNumber),
                _settings.GetSetting(SettingName.Length) / _settings.GetSetting(SettingName.FretNumber),
                ComputeType: PatternComputeTypeEnum.kIdenticalCompute);
            
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