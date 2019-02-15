using Script.Component;
using Script.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Script
{
    public class EditorManager : MonoBehaviour {

        public Story CurrentStory;
        public ThumbnailUiComponent ThumbnailUiComponent;
        public GameObject NewStoryPanel;
        public CanvasGroup OverlayCanvasGroup;

        private void Start()
        {
            // Load Story from Prefs
            if (PlayerPrefs.HasKey(Properties.Prefs.LoadedStory))
            {
                LoadStory(PlayerPrefs.GetString(Properties.Prefs.LoadedStory));
                PlayerPrefs.DeleteKey(Properties.Prefs.LoadedStory);
            }
            // Create new Story
            else
            {
                OverlayCanvasGroup.interactable = false;
                NewStoryPanel.SetActive(true);
            }
        }

        public void ReloadScene(string storyName)
        {
            PlayerPrefs.SetString(Properties.Prefs.LoadedStory, storyName);
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.UnloadSceneAsync(scene.name);
            SceneManager.LoadScene(scene.name);
        }
        
        public void Build(InputField storyName)
        {            
            CurrentStory = ScriptableObject.CreateInstance<Story>();
            CurrentStory.Build(storyName.text);
            ThumbnailUiComponent.Build(CurrentStory);
        }

        public void LoadStory(string storyName)
        {
            CurrentStory = DataManager.LoadStory(storyName);
            if (CurrentStory == null)
            {
                NewStoryPanel.SetActive(true);
            }
            else
            {
                OverlayCanvasGroup.interactable = true;
                ThumbnailUiComponent.Load(CurrentStory, CurrentStory.Thumbnails[0]);
            }
        }

        public void SaveStory()
        {
            CurrentStory.AddThumbnail(ThumbnailUiComponent);
            DataManager.SaveStory(CurrentStory);
        }

        public void LoadThumbnail(int index)
        {
            Thumbnail thumbnail = CurrentStory.Thumbnails[index];
            ThumbnailUiComponent.Load(CurrentStory, thumbnail);
        }
        
        public void SaveThumbnail(ThumbnailUiComponent thumbnailUiComponent)
        {
            if (thumbnailUiComponent.TitleInputField.text == string.Empty) return;
            Thumbnail thumbnail = ScriptableObject.CreateInstance<Thumbnail>();
            thumbnail.Build(thumbnailUiComponent);
            Thumbnail find = CurrentStory.Thumbnails.Find(existingThumbnail => existingThumbnail.Title == thumbnail.Title);
            if (find != null)
            {
                find.Title = thumbnail.Title;
                find.Description = thumbnail.Description;
                find.Choices = thumbnail.Choices;
            }
            else
            {
                CurrentStory.Thumbnails.Add(thumbnail);
            }
            thumbnailUiComponent.Reset();
            thumbnailUiComponent.UpdateThumbnailDropdown();
        }

        public void DeleteThumbnail(ThumbnailUiComponent thumbnailUiComponent)
        {
            Thumbnail thumbnail = ScriptableObject.CreateInstance<Thumbnail>();
            thumbnail.Build(thumbnailUiComponent);
            Thumbnail find = CurrentStory.Thumbnails.Find(existingThumbnail => existingThumbnail.Title == thumbnail.Title);
            if (find != null)
                CurrentStory.Thumbnails.Remove(find);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(Properties.Scene.Menu);
            SceneManager.UnloadSceneAsync(Properties.Scene.Editor);
        }

        public void Exit()
        {
            Application.Quit();
        }
        
    }
}
