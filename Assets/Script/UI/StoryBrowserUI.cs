using System;
using Script.Managers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Script.UI {
    [Serializable]
    public class StringEvent : UnityEvent<string> {}
    
    public class StoryBrowserUI : MonoBehaviour {
        public GameObject ButtonPrefab;
        public GameObject ScrollContent;
        public StringEvent OnChooseStoryButton;

        public void Build() {
            foreach (string fileName in DataManager.ListStories()) {
                Button storyFileButton = Instantiate(ButtonPrefab, ScrollContent.transform).GetComponent<Button>();
                string localName = fileName;
                storyFileButton.onClick.AddListener(delegate {
                    OnChooseStoryButton.Invoke(localName);
                });
                storyFileButton.GetComponentInChildren<Text>().text = fileName;
            }
        }
        
    }
}
