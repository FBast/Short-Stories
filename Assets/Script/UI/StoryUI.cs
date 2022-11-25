using System.Collections.Generic;
using Script.Data;
using Script.Managers;
using Script.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Script.UI {
    public class StoryUI : MonoBehaviour {
        
        public static Story CurrentStory;
        
        [SerializeField] private ThumbnailUI _thumbnailUI;
        [SerializeField] private GameObject _newStoryPanel;
        [SerializeField] private CanvasGroup _overlayCanvasGroup;

        private void Start() {
            // Load Story from Prefs
            if (PlayerPrefs.HasKey(Properties.Prefs.LoadedStory)) {
                LoadStory(PlayerPrefs.GetString(Properties.Prefs.LoadedStory));
                PlayerPrefs.DeleteKey(Properties.Prefs.LoadedStory);
            }
            // Create new Story
            else {
                _overlayCanvasGroup.interactable = false;
                _newStoryPanel.SetActive(true);
            }
        }
        
        public void Build(InputField storyName) {
            CurrentStory = ToStory(storyName.text);
        }
        
        public void ReloadScene(string storyName) {
            PlayerPrefs.SetString(Properties.Prefs.LoadedStory, storyName);
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.UnloadSceneAsync(scene.name);
            SceneManager.LoadScene(scene.name);
        }
        
        public void SaveStory() {
            DataManager.SaveStory(CurrentStory);
        }
        
        public void LoadMainMenu() {
            SceneManager.LoadScene(Properties.Scene.Menu);
            SceneManager.UnloadSceneAsync(Properties.Scene.Editor);
        }

        public void Exit() {
            Application.Quit();
        }

        private Story ToStory(string storyName) {
            return new Story()
            {
                StoryName = storyName,
                Thumbnails = new List<Thumbnail>()
            };
        }
        
        private void LoadStory(string storyName) {
            CurrentStory = DataManager.LoadStory(storyName);
            if (CurrentStory == null) {
                _newStoryPanel.SetActive(true);
            }
            else {
                _overlayCanvasGroup.interactable = true;
                _thumbnailUI.Load(0);
            }
        }

    }
}
