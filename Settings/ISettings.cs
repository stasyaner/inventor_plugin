namespace Settings
{
    public interface ISettings
    {
        void SetSetting(SettingName settingName, int settingValue);
        double GetSetting(SettingName settingName);
    }
}
