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
            foreach (string fileName in DataManager.StoryFiles) {
                Button storyFileButton = Instantiate(ButtonPrefab, ScrollContent.transform).GetComponent<Button>();
                string localName = fileName;
                storyFileButton.onClick.AddListener(delegate {
                    PlayerPrefs.SetString(Properties.Prefs.LoadedStory, localName);
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                });
                storyFileButton.GetComponentInChildren<Text>().text = fileName;
            }
        }

        private void OnDisable() {
            foreach (Transform child in ScrollContent.transform) {
                Destroy(child.gameObject);
            }
        }
        
    }
}
