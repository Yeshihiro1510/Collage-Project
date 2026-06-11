using UnityEngine;

namespace Projects.StudyPractice.Root
{
    public class RootUIView : MonoBehaviour
    {
        [field: SerializeField] public ScreenFader ScreenFader { get; private set; }
        [SerializeField] private Transform _container;
        
        public void SetSceneUI(Transform sceneUI)
        {
            ClearSceneUI();
            sceneUI.SetParent(_container, true);
        }

        private void ClearSceneUI()
        {
            var childCount = _container.childCount;
            for (var i = 0; i < childCount; i++)
            {
                var child = _container.GetChild(i);
                Destroy(child.gameObject);
            }
        }
    }
}