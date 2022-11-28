using System;
using Script.Managers;
using Script.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Script.UI {
    public class StoryBrowserUI : MonoBehaviour {
        public GameObject ButtonPrefab;
        public GameObject ScrollContent;

        private void OnEnable() {
            foreach (string fileName in DataManager.ListStories()) {
                Button storyFileButton = Instantiate(ButtonPrefab, ScrollContent.transform).GetComponent<Button>();
                string localName = fileName;
                storyFileButton.onClick.AddListener(delegate {
                    ReloadScene(localName);
                });
                storyFileButton.GetComponentInChildren<Text>().text = fileName;
            }
        }

        private void ReloadScene(string storyName) {
            PlayerPrefs.SetString(Properties.Prefs.LoadedStory, storyName);
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

    }
}
