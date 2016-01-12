﻿using System;
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
                _settings.GetSetting(SettingName.AtNutWidth) / -2.0);
            Point2d fingerboardAtNutPoint2 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                _settings.GetSetting(SettingName.AtNutWidth) / 2.0);
            Point2d fingerboardAtNutPoint3 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                _settings.GetSetting(SettingName.AtNutWidth) / 2.0, 0.2);
            Point2d fingerboardAtNutPoint4 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                _settings.GetSetting(SettingName.AtNutWidth) / -2.0, 0.2);
            Point2d fingerboardAtNutPoint0 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                0,
                Math.Sqrt(Math.Pow(_settings.GetSetting(SettingName.FingerboardRadius), 2) -
                          Math.Pow(_settings.GetSetting(SettingName.AtNutWidth) / -2.0, 2))//Теорема пифагора
                          * -1 + 0.2);

            //Рисуем 3 линии по форме прямоугольника
            SketchLine fingerboardAtNutLine1 = fingerboardAtNutSketch.SketchLines.AddByTwoPoints(fingerboardAtNutPoint1,
                fingerboardAtNutPoint2);
            SketchLine fingerboardAtNutLine2 = fingerboardAtNutSketch.SketchLines.AddByTwoPoints(
                fingerboardAtNutLine1.StartSketchPoint, fingerboardAtNutPoint4);
            SketchLine fingerboardAtNutLine3 = fingerboardAtNutSketch.SketchLines.AddByTwoPoints(
                fingerboardAtNutLine1.EndSketchPoint, fingerboardAtNutPoint3);

            //Дуга как 4-ая сторона "прямоугольника"
            fingerboardAtNutSketch.SketchArcs.AddByCenterStartEndPoint(fingerboardAtNutPoint0,
                fingerboardAtNutLine3.EndSketchPoint, fingerboardAtNutLine2.EndSketchPoint);

            #endregion

            #region fingerboardAtTwelveFretSketch

            //Создаем скетч на расстоянии 12-го лада (половины длины грифа) от рабочей плоскости XY.
            PlanarSketch fingerboardAtTwelveFretSketch = _invetorConnector.MakeNewSketch(3,
                _settings.GetSetting(SettingName.Length) / 2.0, _partDoc);

            // Создаем точки
            Point2d fingerboardAtTwelveFretPoint1 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                    _settings.GetSetting(SettingName.AtLastFretWidth) / -2.0);
            Point2d fingerboardAtTwelveFretPoint2 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                    _settings.GetSetting(SettingName.AtLastFretWidth) / 2.0);
            Point2d fingerboardAtTwelveFretPoint3 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                    _settings.GetSetting(SettingName.AtLastFretWidth) / 2.0, 0.2);
            Point2d fingerboardAtTwelveFretPoint4 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                _settings.GetSetting(SettingName.AtLastFretWidth) / -2.0, 0.2);
            Point2d fingerboardAtTwelveFretPoint0 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                0,
                Math.Sqrt(Math.Pow(_settings.GetSetting(SettingName.FingerboardRadius), 2) -
                          Math.Pow(_settings.GetSetting(SettingName.AtLastFretWidth) / -2.0, 2))//Теорема пифагора
                          * -1 + 0.2);

            //Рисуем 3 линии по форме прямоугольника
            SketchLine fingerboardAtTwelveFretLine1 = fingerboardAtTwelveFretSketch.SketchLines.AddByTwoPoints(fingerboardAtTwelveFretPoint1,
                fingerboardAtTwelveFretPoint2);
            SketchLine fingerboardAtTwelveFretLine2 = fingerboardAtTwelveFretSketch.SketchLines.AddByTwoPoints(
                fingerboardAtTwelveFretLine1.StartSketchPoint, fingerboardAtTwelveFretPoint4);
            SketchLine fingerboardAtTwelveFretLine3 = fingerboardAtTwelveFretSketch.SketchLines.AddByTwoPoints(
                fingerboardAtTwelveFretLine1.EndSketchPoint, fingerboardAtTwelveFretPoint3);

            //Дуга как 4-ая сторона "прямоугольника"
            fingerboardAtTwelveFretSketch.SketchArcs.AddByCenterStartEndPoint(fingerboardAtTwelveFretPoint0,
                fingerboardAtTwelveFretLine3.EndSketchPoint, fingerboardAtTwelveFretLine2.EndSketchPoint);

            #endregion

            #region fingerboardAtLastFretSketch

            //Создаем скетч на расстоянии длины грифа от рабочей плоскости XY.
            PlanarSketch fingerboardAtLastFretSketch = _invetorConnector.MakeNewSketch(3,
                _settings.GetSetting(SettingName.Length), _partDoc);

            // Создаем точки
            Point2d fingerboardAtLastFretPoint1 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                _settings.GetSetting(SettingName.AtLastFretWidth) / -2.0);
            Point2d fingerboardAtLastFretPoint2 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                _settings.GetSetting(SettingName.AtLastFretWidth) / 2.0);
            Point2d fingerboardLastFretPoint3 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                _settings.GetSetting(SettingName.AtLastFretWidth) / 2.0, 0.2);
            Point2d fingerboardAtLastFretPoint4 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                _settings.GetSetting(SettingName.AtLastFretWidth) / -2.0, 0.2);
            Point2d fingerboardAtLastFretPoint0 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                0,
                Math.Sqrt(Math.Pow(_settings.GetSetting(SettingName.FingerboardRadius), 2) -
                          Math.Pow(_settings.GetSetting(SettingName.AtLastFretWidth) / -2.0, 2))//Теорема пифагора
                          * -1 + 0.2);

            //Рисуем 3 линии по форме прямоугольника
            SketchLine fingerboardAtLastFretLine1 = fingerboardAtLastFretSketch.SketchLines.AddByTwoPoints(fingerboardAtLastFretPoint1,
                fingerboardAtLastFretPoint2);
            SketchLine fingerboardAtLastFretLine2 = fingerboardAtLastFretSketch.SketchLines.AddByTwoPoints(
                fingerboardAtLastFretLine1.StartSketchPoint, fingerboardAtLastFretPoint4);
            SketchLine fingerboardAtLastFretLine3 = fingerboardAtLastFretSketch.SketchLines.AddByTwoPoints(
                fingerboardAtLastFretLine1.EndSketchPoint, fingerboardLastFretPoint3);

            //Дуга как 4-ая сторона "прямоугольника"
            fingerboardAtLastFretSketch.SketchArcs.AddByCenterStartEndPoint(fingerboardAtLastFretPoint0,
                fingerboardAtLastFretLine3.EndSketchPoint, fingerboardAtLastFretLine2.EndSketchPoint);

            #endregion

            #region loft

            //Задание описания лофта между тремя скетчами, заданнымми выше
            ObjectCollection loftObjectCollection =
                _invetorConnector.InventorApplication.TransientObjects.CreateObjectCollection();
            loftObjectCollection.Add(fingerboardAtNutSketch.Profiles.AddForSurface());
            loftObjectCollection.Add(fingerboardAtTwelveFretSketch.Profiles.AddForSurface());
            loftObjectCollection.Add(fingerboardAtLastFretSketch.Profiles.AddForSurface());
            LoftDefinition skethesLoftDefinition =
                _partDoc.ComponentDefinition.Features.LoftFeatures.CreateLoftDefinition(loftObjectCollection,
                    PartFeatureOperationEnum.kNewBodyOperation);

            //Приминение созданного выше лофта
            _partDoc.ComponentDefinition.Features.LoftFeatures.Add(skethesLoftDefinition);

            #endregion

            #region fretHoleSketch

            //Создаем скетч на рабочей плоскости ZX.
            PlanarSketch fretHoleSketch = _invetorConnector.MakeNewSketch(
                2,
                Math.Sqrt(Math.Pow(_settings.GetSetting(SettingName.FingerboardRadius), 2) +
                          Math.Pow(_settings.GetSetting(SettingName.AtLastFretWidth)/-2.0, 2))
                          - _settings.GetSetting(SettingName.FingerboardRadius) + 0.2,
                _partDoc);

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
            ExtrudeDefinition fretHoleExtrudeDef = _partDoc.ComponentDefinition.Features.ExtrudeFeatures
                .CreateExtrudeDefinition(fretHoleSketch.Profiles.AddForSolid(), PartFeatureOperationEnum.kCutOperation);
            fretHoleExtrudeDef.SetDistanceExtent(0.2, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection);
            _partDoc.ComponentDefinition.Features.ExtrudeFeatures.Add(fretHoleExtrudeDef);

            //Прямоугольный массив выдавленных прямоугольников
            ObjectCollection fretHoleObjectCollection = _invetorConnector.InventorApplication.TransientObjects.CreateObjectCollection();
            fretHoleObjectCollection.Add(_partDoc.ComponentDefinition.Features.ExtrudeFeatures[1]);
            _partDoc.ComponentDefinition.Features.RectangularPatternFeatures.Add(
                fretHoleObjectCollection, _partDoc.ComponentDefinition.WorkAxes[3],
                true,
                _settings.GetSetting(SettingName.FretNumber),
                //Умножаем на 1.0, чтобы был double
                _settings.GetSetting(SettingName.Length) * 1.0 / _settings.GetSetting(SettingName.FretNumber),
                ComputeType: PatternComputeTypeEnum.kIdenticalCompute);

            #endregion

            #region inlaySketch

            if (_settings.GetSetting(SettingName.Inlay) != 0)
            {
                //Создаем скетч на рабочей плоскости ZX.
                PlanarSketch inlaySketch = _invetorConnector.MakeNewSketch(
                2,
                Math.Sqrt(Math.Pow(_settings.GetSetting(SettingName.FingerboardRadius), 2) +
                          Math.Pow(_settings.GetSetting(SettingName.AtLastFretWidth) / -2.0, 2))
                          - _settings.GetSetting(SettingName.FingerboardRadius) + 0.2,
                _partDoc);

                // Создаем точки
                Point2d inlayPoint1 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                    0, 0.6);
                Point2d inlayPoint2 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                    0.1, 0.7);

                //Рисуем прямоугольник по трем точкам
                inlaySketch.SketchLines.AddAsTwoPointCenteredRectangle(inlayPoint1, inlayPoint2);

                //Выдавливаем прямоугольник
                ExtrudeDefinition inlayExtrudeDef = _partDoc.ComponentDefinition.Features.ExtrudeFeatures
                    .CreateExtrudeDefinition(inlaySketch.Profiles.AddForSolid(), PartFeatureOperationEnum.kCutOperation);
                inlayExtrudeDef.SetDistanceExtent(0.2, PartFeatureExtentDirectionEnum.kSymmetricExtentDirection);
                _partDoc.ComponentDefinition.Features.ExtrudeFeatures.Add(inlayExtrudeDef);

                //Прямоугольный массив выдавленных прямоугольников
                ObjectCollection inlayObjectCollection =
                    _invetorConnector.InventorApplication.TransientObjects.CreateObjectCollection();
                inlayObjectCollection.Add(_partDoc.ComponentDefinition.Features.ExtrudeFeatures[2]);
                _partDoc.ComponentDefinition.Features.RectangularPatternFeatures.Add(
                    inlayObjectCollection, _partDoc.ComponentDefinition.WorkAxes[3],
                    true,
                    _settings.GetSetting(SettingName.FretNumber),
                    //Умножаем на 1.0, чтобы был double
                    _settings.GetSetting(SettingName.Length) * 1.0 / _settings.GetSetting(SettingName.FretNumber),
                    ComputeType: PatternComputeTypeEnum.kIdenticalCompute);
            }

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