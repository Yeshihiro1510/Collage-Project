using System;

namespace Projects.StudyPractice.Gameplay
{
    [Serializable]
    public struct NotificationsData
    {
        public NotificationsData(bool hide)
        {
            this.hide = hide;
        }
        public bool hide;

    }
}