using Inventor;
using InventorAPI;
using Settings;

namespace Parts
{
    /// <summary>
    /// Класс детали инкрустации грифа
    /// </summary>
    public class InlayPart : IPart
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
        public InlayPart(ISettings settings, InventorConnector inventorConnector)
        {
            if (settings.GetSetting(SettingName.Inlay) == 1)
            {
                _settings = settings;
                _invetorConnector = inventorConnector;
                _partDoc = (PartDocument) inventorConnector.InventorApplication.Documents.Add(
                    DocumentTypeEnum.kPartDocumentObject,
                    inventorConnector.InventorApplication.FileManager.GetTemplateFile(
                        DocumentTypeEnum.kPartDocumentObject, SystemOfMeasureEnum.kMetricSystemOfMeasure));
            }
        }

        /// <summary>
        /// Метод построения инкрустации грифа
        /// </summary>
        public void Build()
        {
            if ((_settings != null) && (_settings.GetSetting(SettingName.Inlay) == 1))
            {
                //Создаем скетч на рабочей плоскости ZX.
                PlanarSketch inlaySketch = _invetorConnector.MakeNewSketch(2, 0, _partDoc);

                // Создаем точки
                Point2d inlayPoint1 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d();
                Point2d inlayPoint2 = _invetorConnector.InventorApplication.TransientGeometry.CreatePoint2d(
                    0.1, 0.1);

                //Рисуем прямоугольник по трем точкам
                inlaySketch.SketchLines.AddAsTwoPointCenteredRectangle(inlayPoint1, inlayPoint2);

                //Выдавливаем прямоугольник
                ExtrudeDefinition inlayExtrudeDef = _partDoc.ComponentDefinition.Features.ExtrudeFeatures
                    .CreateExtrudeDefinition(inlaySketch.Profiles.AddForSolid(), PartFeatureOperationEnum.kNewBodyOperation);
                inlayExtrudeDef.SetDistanceExtent(0.1, PartFeatureExtentDirectionEnum.kPositiveExtentDirection);
                _partDoc.ComponentDefinition.Features.ExtrudeFeatures.Add(inlayExtrudeDef);

                //Меняем материал
                _invetorConnector.ChangeMaterial(_partDoc, "Polystyrene");
            }
        }

        /// <summary>
        /// Метод, который закрывает текущий документ без сохранения.
        /// </summary>
        public void Close()
        {
            if ((_settings != null) && (_settings.GetSetting(SettingName.Inlay) == 1))
            {
                _partDoc.Close(true);
            }
        }
    }
}