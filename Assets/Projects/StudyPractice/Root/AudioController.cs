using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Projects.StudyPractice.MainMenu;
using Projects.Utils;
using UnityEngine;
using UnityEngine.Audio;

namespace Projects.StudyPractice.Root
{
    public class AudioController
    {
        public AudioController()
        {
            _musicSource = new GameObject("[Music Source]").AddComponent<AudioSource>();
            _musicSource.loop = true;
            _SFXSource = new GameObject("[SFX Source]").AddComponent<AudioSource>();
            Object.DontDestroyOnLoad(_musicSource.gameObject);
            Object.DontDestroyOnLoad(_SFXSource.gameObject);

            if (JsonSavingUtil.TryGet<VolumeData>(Path, out var data)) Apply(data);
            else SetDefault();

            _audioClips = Resources.LoadAll<AudioResource>("").ToDictionary(p => p.name, p => p);
        }

        private static string Path => Application.persistentDataPath + "/volume_data.json";

        private readonly AudioSource _musicSource;
        private readonly AudioSource _SFXSource;

        private readonly Dictionary<string, AudioResource> _audioClips;
        public VolumeData VolumeData { get; private set; }

        public void PlayMusic(AudioClip clip)
        {
            _musicSource.clip = clip;
            _musicSource.Play();
            _musicSource.DOFade(VolumeData.MusicVolume, 5f).From(0f).SetEase(Ease.InCirc);
        }

        public void StopMusic()
        {
            _musicSource.Stop();
        }

        public void Play(string name)
        {
            _SFXSource.resource = _audioClips[name];
            _SFXSource.Play();
        }

        public void Apply(VolumeData data)
        {
            VolumeData = data;
            _musicSource.volume = data.MusicVolume * data.GeneralVolume;
            _SFXSource.volume = data.SFXVolume * data.GeneralVolume;
            Save();
        }

        public void Save()
        {
            JsonSavingUtil.Set(VolumeData, Path);
        }

        public void SetDefault()
        {
            var defaultData = new VolumeData(1f,1f,1f);
            Apply(defaultData);
        }
    }
}