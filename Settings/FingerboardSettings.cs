using System;
using System.Collections.Generic;

namespace Settings
{
    public class FingerboardSettings : ISettings
    {
        public FingerboardSettings()
        {
            _settingsDictionary = new Dictionary<SettingName, double>();
        }

        private readonly Dictionary<SettingName, double> _settingsDictionary;
        public void SetSetting(SettingName settingName, double settingValue)
        {
            if (settingName != SettingName.Material
                && settingName != SettingName.FingerboardMaterial
                && settingName != SettingName.HeadstickType
                && settingName != SettingName.FingerboardInlayType)
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

            //Делим на 10, так как инвентор воспринимает все размеры в см, кроме количества ладов
            if (settingName != SettingName.FretNumber)
            {
                _settingsDictionary[settingName] = settingValue / 10;
            }
            else
            {
                _settingsDictionary[settingName] = settingValue;
            }
        }

        public double GetSetting(SettingName settingName)
        {
            if (!_settingsDictionary.ContainsKey(settingName))
            {
                throw new ArgumentException("Словарь не содержит такого ключа.");
            }
            return _settingsDictionary[settingName];
        }
    }
}