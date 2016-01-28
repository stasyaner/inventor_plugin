﻿using System;
using Inventor;
using InventorAPI;
using Settings;

namespace Parts
{
    /// <summary>
    /// Класс детали грифа
    /// </summary>
    public class NeckPart : IPart
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
        public PartComponentDefinition PartDocumentComponentDefinition
        {
            get { return _partDoc.ComponentDefinition; }
        }

        /// <summary>
        /// Ссылка на настройки детали
        /// </summary>
        private readonly ISettings _settings;

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="inventorConnector"></param>
        public NeckPart(ISettings settings, InventorConnector inventorConnector)
        {
            _settings = settings;
            _inventorConnector = inventorConnector;
            _partDoc = (PartDocument)inventorConnector.InventorApplication.Documents.Add(
                DocumentTypeEnum.kPartDocumentObject, inventorConnector.InventorApplication.FileManager.GetTemplateFile(
                    DocumentTypeEnum.kPartDocumentObject, SystemOfMeasureEnum.kMetricSystemOfMeasure));
        }

        /// <summary>
        /// Метод построения грифа
        /// </summary>
        public void Build()
        {
            #region nutSketch

            //Создаем скетч на рабочей плоскости XY.
            PlanarSketch nutSketch = _inventorConnector.MakeNewSketch(3, 0, _partDoc);

            // Создаем точки
            Point2d nutPoint3 = _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                0, _settings.GetSetting(SettingName.AtNutHeight));
            Point2d nutPoint0 = _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d();

            //Рисуем дуги
            SketchArc nutSketchArc1 = nutSketch.SketchArcs.AddByCenterStartSweepAngle(
                nutPoint0, _settings.GetSetting(SettingName.AtNutWidth) / 2.0, 0, Math.PI * 0.25);
            SketchArc nutSketchArc2 = nutSketch.SketchArcs.AddByCenterStartSweepAngle(
                nutPoint0, _settings.GetSetting(SettingName.AtNutWidth) / 2.0, Math.PI * 0.75, Math.PI * 0.25);

            //Рисуем линию
            nutSketch.SketchLines.AddByTwoPoints(nutSketchArc1.StartSketchPoint, nutSketchArc2.EndSketchPoint);

            //Сплайн между дугами
            ObjectCollection nutSketchSplinePointsObjectCollection = _inventorConnector.InventorApplication.TransientObjects.CreateObjectCollection();
            nutSketchSplinePointsObjectCollection.Add(nutSketchArc1.EndSketchPoint);
            nutSketchSplinePointsObjectCollection.Add(nutPoint3);
            nutSketchSplinePointsObjectCollection.Add(nutSketchArc2.StartSketchPoint);
            nutSketch.SketchSplines.Add(nutSketchSplinePointsObjectCollection);

            #endregion

            #region atTwelveFretSketch

            //Создаем скетч на расстоянии 12-го лада (половины длины грифа) от рабочей плоскости XY.
            PlanarSketch atTwelveFretSketch = _inventorConnector.MakeNewSketch(3, _settings.GetSetting(SettingName.Length) / 2.0, _partDoc);

            // Создаем точки
            Point2d atTwelveFretPoint3 = _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                0, _settings.GetSetting(SettingName.AtTwelveFretHeight));
            Point2d atTwelveFretPoint0 = _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d();

            //Рисуем дуги
            SketchArc atTwelveFretSketchArc1 = atTwelveFretSketch.SketchArcs.AddByCenterStartSweepAngle(
                atTwelveFretPoint0, _settings.GetSetting(SettingName.AtLastFretWidth) / 2.0, 0, Math.PI * 0.25);
            SketchArc atTwelveFretSketchArc2 = atTwelveFretSketch.SketchArcs.AddByCenterStartSweepAngle(
                atTwelveFretPoint0, _settings.GetSetting(SettingName.AtLastFretWidth) / 2.0, Math.PI * 0.75, Math.PI * 0.25);

            //Рисуем линию
            atTwelveFretSketch.SketchLines.AddByTwoPoints(atTwelveFretSketchArc1.StartSketchPoint, atTwelveFretSketchArc2.EndSketchPoint);

            //Задание точек сплайна между дугами
            ObjectCollection atTwelveFretSketchSplinePointsObjectCollection = _inventorConnector.InventorApplication.TransientObjects.CreateObjectCollection();
            atTwelveFretSketchSplinePointsObjectCollection.Add(atTwelveFretSketchArc1.EndSketchPoint);
            atTwelveFretSketchSplinePointsObjectCollection.Add(atTwelveFretPoint3);
            atTwelveFretSketchSplinePointsObjectCollection.Add(atTwelveFretSketchArc2.StartSketchPoint);

            //Рисуем сплайн, заданный выше
            atTwelveFretSketch.SketchSplines.Add(atTwelveFretSketchSplinePointsObjectCollection);

            #endregion

            #region atLastFretSketch

            //Создаем скетч на расстоянии длины грифа от рабочей плоскости XY.
            PlanarSketch atLastFretSketch = _inventorConnector.MakeNewSketch(3, _settings.GetSetting(SettingName.Length), _partDoc);

            // Создаем точки
            Point2d atLastFretPoint3 = _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                0, _settings.GetSetting(SettingName.AtTwelveFretHeight));
            Point2d atLastFretPoint0 = _inventorConnector.InventorApplication.TransientGeometry.CreatePoint2d();

            //Рисуем дуги
            SketchArc atLastFretSketchArc1 = atLastFretSketch.SketchArcs.AddByCenterStartSweepAngle(
                atLastFretPoint0, _settings.GetSetting(SettingName.AtLastFretWidth) / 2, 0, Math.PI * 0.25);
            SketchArc atLastFretSketchArc2 = atLastFretSketch.SketchArcs.AddByCenterStartSweepAngle(
                atLastFretPoint0, _settings.GetSetting(SettingName.AtLastFretWidth) / 2, Math.PI * 0.75, Math.PI * 0.25);

            //Рисуем линию
            atLastFretSketch.SketchLines.AddByTwoPoints(atLastFretSketchArc1.StartSketchPoint, atLastFretSketchArc2.EndSketchPoint);

            //Задание точек сплайна между дугами
            ObjectCollection atLastFretSketchSplinePointsObjectCollection = _inventorConnector.InventorApplication.TransientObjects.CreateObjectCollection();
            atLastFretSketchSplinePointsObjectCollection.Add(atLastFretSketchArc1.EndSketchPoint);
            atLastFretSketchSplinePointsObjectCollection.Add(atLastFretPoint3);
            atLastFretSketchSplinePointsObjectCollection.Add(atLastFretSketchArc2.StartSketchPoint);

            //Рисуем сплайн, заданный выше
            atLastFretSketch.SketchSplines.Add(atLastFretSketchSplinePointsObjectCollection);

            #endregion

            #region loft

            //Задание описания лофта между тремя скетчами, заданнымми выше
            ObjectCollection loftObjectCollection = _inventorConnector.InventorApplication.TransientObjects.CreateObjectCollection();
            loftObjectCollection.Add(nutSketch.Profiles.AddForSurface());
            loftObjectCollection.Add(atTwelveFretSketch.Profiles.AddForSurface());
            loftObjectCollection.Add(atLastFretSketch.Profiles.AddForSurface());
            LoftDefinition skethesLoftDefinition = PartDocumentComponentDefinition.Features.LoftFeatures.CreateLoftDefinition(loftObjectCollection,
                PartFeatureOperationEnum.kNewBodyOperation);

            //Приминение созданного выше лофта
            PartDocumentComponentDefinition.Features.LoftFeatures.Add(skethesLoftDefinition);

            #endregion

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
            _inventorConnector.ChangeMaterial(_partDoc, materialName);

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