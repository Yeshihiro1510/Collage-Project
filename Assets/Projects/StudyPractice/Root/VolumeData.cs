using System;

namespace Projects.StudyPractice.Root
{
    [Serializable]
    public struct VolumeData
    {
        public VolumeData(float generalVolume, float musicVolume, float sfxVolume)
        {
            GeneralVolume = generalVolume;
            MusicVolume = musicVolume;
            SFXVolume = sfxVolume;
        }

        public float GeneralVolume;
        public float MusicVolume;
        public float SFXVolume;
    }
}