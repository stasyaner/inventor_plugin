﻿using InventorAPI;
using Settings;

namespace Parts
{
    /// <summary>
    /// Итерфейс для деталей
    /// </summary>
    public interface IPart
    {
        /// <summary>
        /// Построение детали
        /// </summary>
        void Build();

        /// <summary>
        /// Метод, который закрывает текущий документ без сохранения.
        /// </summary>
        void Close();
    }
}
