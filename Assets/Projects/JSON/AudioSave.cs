using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Projects.JSON
{
    public class AudioSave : MonoBehaviour
    {
        public const string Path = "Assets/Resources/AudioSettings.json";

        [SerializeField] private AudioSource _audioSource;

        private void Awake()
        {
            if (File.Exists(Path))
            {
                using var sr = new StreamReader(Path);
                var jr = new JsonTextReader(sr);
                var s = JsonSerializer.CreateDefault();
                _audioSource.volume = s.Deserialize<float>(jr);
            }
        }

        private void OnDestroy()
        {
            using var sw = new StreamWriter(Path);
            var s = JsonSerializer.CreateDefault();
            s.Serialize(sw, _audioSource.volume);
        }
    }
}