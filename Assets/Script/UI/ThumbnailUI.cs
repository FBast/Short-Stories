using System;
using System.Collections.Generic;
using System.Linq;
using Script.Data;
using TMPro;
using UnityEngine;

namespace Script.UI {
    public class ThumbnailUI : MonoBehaviour {

        [SerializeField] private TMP_InputField TitleInputField;
        [SerializeField] private TMP_InputField DescriptionInputField;
        [SerializeField] private TMP_Dropdown ThumbnailSelectorDropdown;
        [SerializeField] private Transform ChoiceContent;
        [SerializeField] private GameObject ChoicePrefab;

        private string _guid = Guid.NewGuid().ToString();
        
        private List<ChoiceUI> ChoiceUIs => ChoiceContent.GetComponentsInChildren<ChoiceUI>().ToList();
        private Story CurrentStory => StoryUI.CurrentStory;

        /// <summary>
        /// Reset the thumbnailUI values
        /// </summary>
        public void Reset() {
            _guid = Guid.NewGuid().ToString();
            TitleInputField.text = string.Empty;
            DescriptionInputField.text = string.Empty;
            ChoiceUIs.ForEach(component => Destroy(component.gameObject));
        }

        /// <summary>
        /// Create a new thumbnail
        /// </summary>
        public void New() {
            Save();
            Reset();
            UpdateThumbnailDropdown();
        }
        
        /// <summary>
        /// Load an existing thumbnail using dropdown thumbnail
        /// </summary>
        /// <param name="index"></param>
        public void Load(int index) {
            Save();
            Reset();
            Thumbnail thumbnail = CurrentStory.Thumbnails[index];
            _guid = thumbnail.Guid;
            TitleInputField.text = thumbnail.Title;
            DescriptionInputField.text = thumbnail.Description;
            foreach (Choice choice in thumbnail.Choices) {
                GameObject instantiate = Instantiate(ChoicePrefab, ChoiceContent);
                ChoiceUI choiceUI = instantiate.GetComponent<ChoiceUI>();
                choiceUI.Load(choice);
            }
        }
        
        /// <summary>
        /// Save the current thumbnail using the same guid as id
        /// </summary>
        public void Save() {
            if (TitleInputField.text == string.Empty) return;
            Thumbnail thumbnail = ToThumbnail();
            Thumbnail find = CurrentStory.Thumbnails.Find(existingThumbnail => existingThumbnail.Guid == thumbnail.Guid);
            if (find == null) {
                CurrentStory.Thumbnails.Add(thumbnail);
            }
            else {
                find.Title = thumbnail.Title;
                find.Description = thumbnail.Description;
                find.Choices = thumbnail.Choices;
            }
        }
        
        /// <summary>
        /// Delete the current thumbnail
        /// </summary>
        public void Delete() {
            Thumbnail find = CurrentStory.Thumbnails.Find(existingThumbnail => existingThumbnail.Guid == _guid);
            if (find != null) CurrentStory.Thumbnails.Remove(find);
        }

        /// <summary>
        /// Convert the ThumbnailUI into Thumbnail
        /// </summary>
        /// <returns></returns>
        public Thumbnail ToThumbnail() {
            return new Thumbnail {
                Guid = _guid,
                Title = TitleInputField.text,
                Description = DescriptionInputField.text,
                Choices = ChoiceUIs.Select(ui => ui.ToChoice()).ToList()
            };
        }

        /// <summary>
        /// Update the content of the thumbnails dropdown
        /// </summary>
        public void UpdateThumbnailDropdown() {
            ThumbnailSelectorDropdown.ClearOptions();
            CurrentStory.Thumbnails.ForEach(thumbnail => ThumbnailSelectorDropdown.options.Add(new TMP_Dropdown.OptionData(thumbnail.Title)));
            ChoiceUIs.ForEach(component => component.UpdateLinkedThumbnailDropdown());
            ThumbnailSelectorDropdown.interactable = CurrentStory.Thumbnails.Count != 0;
        }
        
        /// <summary>
        /// Add a new choice on the current thumbnail
        /// </summary>
        public void AddChoice() {
            GameObject instantiate = Instantiate(ChoicePrefab, ChoiceContent);
            ChoiceUI choiceUI = instantiate.GetComponent<ChoiceUI>();
            choiceUI.UpdateLinkedThumbnailDropdown();
        }

    }
}