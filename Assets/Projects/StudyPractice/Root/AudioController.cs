using DG.Tweening;
using Projects.StudyPractice.MainMenu;
using Projects.Utils;
using UnityEngine;

namespace Projects.StudyPractice.Root
{
    public class AudioController
    {
        public AudioController()
        {
            _musicSource = new GameObject("[Music Source]").AddComponent<AudioSource>();
            _musicSource.loop = true;

            _SFXSource = new GameObject("[SFX Source]").AddComponent<AudioSource>();

            if (JsonSavingUtil.TryGet<VolumeData>(Path, out var data))
            {
                _musicSource.volume = data.MusicVolume * data.GeneralVolume;
                _SFXSource.volume = data.SFXVolume * data.GeneralVolume;
            }

            Object.DontDestroyOnLoad(_musicSource.gameObject);
            Object.DontDestroyOnLoad(_SFXSource.gameObject);
        }

        public static string Path => Application.persistentDataPath + "/volume_data.json";

        private readonly AudioSource _musicSource;
        private readonly AudioSource _SFXSource;

        public void PlayMusic(AudioClip clip)
        {
            _musicSource.clip = clip;
            _musicSource.Play();
            _musicSource.DOFade(1f, 5f).From(0f).SetEase(Ease.InCirc);
        }

        public void StopMusic()
        {
            _musicSource.Stop();
        }

        public void Play(AudioClip clip)
        {
            _SFXSource.PlayOneShot(clip);
        }

        public void Apply(VolumeData data)
        {
            _musicSource.volume = data.MusicVolume * data.GeneralVolume;
            _SFXSource.volume = data.SFXVolume * data.GeneralVolume;
            JsonSavingUtil.Set(data, Path);
        }
    }
}