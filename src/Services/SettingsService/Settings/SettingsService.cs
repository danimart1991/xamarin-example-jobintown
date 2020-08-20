using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Settings.Contracts;

namespace Settings
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettings _appSettings;

        public SettingsService()
        {
            _appSettings = CrossSettings.Current;
        }

        public bool AddOrUpdateValue<T>(string key, T value)
        {
            var json = JsonConvert.SerializeObject(value);
            return _appSettings.AddOrUpdateValue(key, json);
        }

        public T GetValue<T>(string key)
        {
            return GetValue(key, default(T));
        }

        public T GetValue<T>(string key, T defaultValue)
        {
            var json = _appSettings.GetValueOrDefault(key, string.Empty);
            return string.IsNullOrEmpty(json) ? defaultValue : JsonConvert.DeserializeObject<T>(json);
        }

        public void RemoveKey(string key)
        {
            _appSettings.Remove(key);
        }

        public void ClearAll()
        {
            _appSettings.Clear();
        }

        public bool ContainsKey(string key)
        {
            return _appSettings.Contains(key);
        }
    }
}
