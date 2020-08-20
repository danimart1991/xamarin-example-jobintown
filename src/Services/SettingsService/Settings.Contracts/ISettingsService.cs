namespace Settings.Contracts
{
    public interface ISettingsService
    {
        bool AddOrUpdateValue<T>(string key, T value);

        void ClearAll();

        bool ContainsKey(string key);

        T GetValue<T>(string key);

        T GetValue<T>(string key, T defaultValue);

        void RemoveKey(string key);
    }
}
