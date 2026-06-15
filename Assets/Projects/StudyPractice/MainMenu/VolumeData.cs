using System;

namespace Projects.StudyPractice.MainMenu
{
    [Serializable]
    public readonly struct VolumeData
    {
        public VolumeData(float generalVolume, float musicVolume, float sfxVolume)
        {
            GeneralVolume = generalVolume;
            MusicVolume = musicVolume;
            SFXVolume = sfxVolume;
        }

        public readonly float GeneralVolume;
        public readonly float MusicVolume;
        public readonly float SFXVolume;
    }
}