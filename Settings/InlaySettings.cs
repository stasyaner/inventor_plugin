using System;
using System.Collections.Generic;

namespace Settings
{
    public class InlaySettings : ISettings
    {
        public InlaySettings()
        {
            _settingsDictionary = new Dictionary<SettingName, int>();
        }

        private readonly Dictionary<SettingName, int> _settingsDictionary;
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

        public int GetSetting(SettingName settingName)
        {
            if (!_settingsDictionary.ContainsKey(settingName))
            {
                throw new ArgumentException("Словарь не содержит такого ключа.");
            }

            return _settingsDictionary[settingName];
        }
    }
}