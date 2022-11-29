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
            // Loaded Story
            if (PlayerPrefs.HasKey(Properties.Prefs.LoadedStory) && Check(PlayerPrefs.GetString(Properties.Prefs.LoadedStory))) {
                Load(PlayerPrefs.GetString(Properties.Prefs.LoadedStory));
                PlayerPrefs.DeleteKey(Properties.Prefs.LoadedStory);
                _thumbnailUI.UpdateThumbnailDropdown();
            }
            // Last Loaded Story
            else if (PlayerPrefs.HasKey(Properties.Prefs.LastStory) && Check(PlayerPrefs.GetString(Properties.Prefs.LastStory))) {
                Load(PlayerPrefs.GetString(Properties.Prefs.LastStory));
                PlayerPrefs.DeleteKey(Properties.Prefs.LastStory);
                _thumbnailUI.UpdateThumbnailDropdown();
            }
            // Force create new Story
            else {
                _newStoryCanvas.SetActive(true);
            }
        }

        /// <summary>
        /// Save the current story
        /// </summary>
        public void Save() {
            _thumbnailUI.Save();
            DataManager.SaveStory(CurrentStory);
            PlayerPrefs.SetString(Properties.Prefs.LastStory, CurrentStory.StoryName);
        }
        
        /// <summary>
        /// Load an existing story
        /// </summary>
        private void Load(string storyName) {
            CurrentStory = DataManager.LoadStory(storyName);
            if (CurrentStory == null) {
                _newStoryCanvas.SetActive(true);
            }
            else {
                _thumbnailUI.Load(0);
            }
        }

        private bool Check(string storyName) {
            return DataManager.CheckStory(storyName);
        }
        
        /// <summary>
        /// Quit the application
        /// </summary>
        public void Exit() {
            Application.Quit();
        }
        
    }
}
