﻿using System.Collections.Generic;

namespace Settings
{
    public class FingerboardSettings : ISettings
    {
        public FingerboardSettings()
        {
            _settingsDictionary = new Dictionary<SettingName, int>();
        }

        private Dictionary<SettingName, int> _settingsDictionary;
        public void SetSetting(SettingName settingName, int settingValue)
        {
            _settingsDictionary[settingName] = settingValue;
        }

        public int GetSetting(SettingName settingName)
        {
            return _settingsDictionary[settingName];
        }
    }
}