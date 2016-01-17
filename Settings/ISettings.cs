namespace Settings
{
    /// <summary>
    /// Интерфейса для классов настроек деталей
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        /// Сигнатура метода, задающего настройку
        /// </summary>
        /// <param name="settingName">Название настройки</param>
        /// <param name="settingValue">Значение</param>
        void SetSetting(SettingName settingName, int settingValue);

        /// <summary>
        /// Сигнатура метода, возвращающего значение настройки
        /// </summary>
        /// <param name="settingName">Название настройки</param>
        /// <returns>Значение настройки</returns>
        double GetSetting(SettingName settingName);
    }
}
