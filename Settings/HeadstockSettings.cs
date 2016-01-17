﻿using System;
using System.Collections.Generic;

namespace Settings
{
    /// <summary>
    /// Класс настроек головы грифа
    /// </summary>
    public class HeadstockSettings : ISettings
    {
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public HeadstockSettings()
        {
            _settingsDictionary = new Dictionary<SettingName, int>();
        }

        /// <summary>
        /// Словарь настроек
        /// </summary>
        private readonly Dictionary<SettingName, int> _settingsDictionary;

        /// <summary>
        /// Метода, задающий настройку
        /// </summary>
        /// <param name="settingName">Название настройки</param>
        /// <param name="settingValue">Значение</param>
        public void SetSetting(SettingName settingName, int settingValue)
        {
            if (settingName != SettingName.Material
                && settingName != SettingName.FingerboardMaterial
                && settingName != SettingName.ReverseHeadstock
                && settingName != SettingName.Inlay)
            {
                if (settingValue <= 0)
                {
                    throw new ArgumentException("Данное значение не может быть меньше либо равным нулю.");
                }
            }
            else
            {
                if (settingValue < 0)
                {
                    throw new ArgumentException("Не выбрано значение в комбо-боксе.");
                }
            }
            
            _settingsDictionary[settingName] = settingValue;
        }

        /// <summary>
        /// Метода, возвращающий значение настройки
        /// </summary>
        /// <param name="settingName">Название настройки</param>
        /// <returns>Значение настройки</returns>
        public double GetSetting(SettingName settingName)
        {
            if (!_settingsDictionary.ContainsKey(settingName))
            {
                throw new ArgumentException("Словарь не содержит такого ключа.");
            }

            //Делим на 10, так как инвентор воспринимает все размеры в см, кроме количества ладов
            if ((settingName == SettingName.ReverseHeadstock)
                || (settingName == SettingName.Material))
            {
                return _settingsDictionary[settingName];
            }

            return _settingsDictionary[settingName] / 10.0;
        }
    }
}