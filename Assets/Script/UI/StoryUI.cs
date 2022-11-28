using Script.Data;
using Script.Managers;
using Script.Utils;
using UnityEngine;

namespace Script.UI {
    public class StoryUI : MonoBehaviour {
        
        public static Story CurrentStory;
        
        [SerializeField] private ThumbnailUI _thumbnailUI;
        [SerializeField] private GameObject _newStoryCanvas;

        private void Start() {
            // Load Story from Prefs
            if (PlayerPrefs.HasKey(Properties.Prefs.LoadedStory)) {
                LoadStory(PlayerPrefs.GetString(Properties.Prefs.LoadedStory));
                PlayerPrefs.DeleteKey(Properties.Prefs.LoadedStory);
                _thumbnailUI.UpdateThumbnailDropdown();
            }
            // Create new Story
            else {
                _newStoryCanvas.SetActive(true);
            }
        }

        public void SaveStory() {
            _thumbnailUI.Save();
            DataManager.SaveStory(CurrentStory);
        }
        
        private void LoadStory(string storyName) {
            CurrentStory = DataManager.LoadStory(storyName);
            if (CurrentStory == null) {
                _newStoryCanvas.SetActive(true);
            }
            else {
                _thumbnailUI.Load(0);
            }
        }
        
        public void Exit() {
            Application.Quit();
        }
        
    }
}
