using System;
using System.Runtime.InteropServices;
using Inventor;
using Application = Inventor.Application;

namespace InventorAPI
{
    public class InventorConnector
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public InventorConnector()
        {
            //Инициализируем переменную.
            InventorApplication = null;
            try
            {
                //Пытаемся перехватить контроль над приложением инвентора, и сделать его видимым.
                InventorApplication = (Inventor.Application)Marshal.GetActiveObject("Inventor.Application");
                InventorApplication.Visible = true;
            }
            catch (Exception)
            {
                try
                {
                    //Если не получилось перехватить приложение - выкинется исключение на то,
                    //что такого приложения нет. Попробуем создать приложение вручную.
                    Type invAppType = Type.GetTypeFromProgID("Inventor.Application");
                    InventorApplication = (Application)Activator.CreateInstance(invAppType);
                    InventorApplication.Visible = true;
                }
                catch (Exception)
                {
                    //Если ничего не получилось - выкинем месседжбокс о том, что инвентор не установлен,
                    //либо по каким-то причинам не получилось до него добраться.
                    ConnectionError = @"Не получилось запустить инвентор.
                        Проверьте установен ли Autodesk Inventor 2016";
                }
            }
        }

        /// <summary>
        /// InventorApplication свойство.
        /// </summary>
        /// <value>
        /// Содержит указатель на приложение.
        /// </value>
        public Inventor.Application InventorApplication { get; }

        /// <summary>
        /// ConnectionError свойтво.
        /// </summary>
        /// <value>
        /// Содержит сообщение об ошибки при инициализации приложения.
        /// </value>
        public string ConnectionError;

        /// <summary>
        /// Метод для построения скетча с помощью рабочих плоскостей.
        /// На вход подается номер рабочей плоскости от которой будет сдвиг и сам сдвиг.
        /// //[1 - ZY ; 2 - ZX ; 3 - XY] - номера плоскостей
        /// </summary>
        /// <param name="n">Номер плоскости</param>
        /// <param name="offset">Сдвиг</param>
        /// <param name="partDocument">Ссылка на документ детали</param>
        /// <returns>Полученный скетч</returns>
        public PlanarSketch MakeNewSketch(int n, double offset, PartDocument partDocument)
        {
            //Получаем ссылку на рабочую плоскость.
            WorkPlane mainPlane = partDocument.ComponentDefinition.WorkPlanes[n];

            //Делаем сдвинутую плоскость.
            WorkPlane offsetPlane = partDocument.ComponentDefinition.WorkPlanes.AddByPlaneAndOffset(mainPlane, offset);

            //Создаем на плоскости скетч.
            PlanarSketch sketch = partDocument.ComponentDefinition.Sketches.Add(offsetPlane);

            //прячем плоскость от пользователя.
            offsetPlane.Visible = false;

            //возвращаем скетч вызвавшему методу
            return sketch;
        }

        public void DrawCircle(double diameter, PlanarSketch sketch, Object centerPoint)
        {
            //рисуем кружочек в текущем скетче
            sketch.SketchCircles.AddByCenterRadius(centerPoint, diameter / 2);
        }
    }
}