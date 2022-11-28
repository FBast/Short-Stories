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
        [SerializeField] private List<ChoiceUI> ChoiceUiComponents = new List<ChoiceUI>();
        [SerializeField] private Transform ChoiceContent;
        [SerializeField] private GameObject ChoicePrefab;

        private Story CurrentStory => StoryUI.CurrentStory;

        private string _guid = Guid.NewGuid().ToString();

        public void New() {
            Save();
            _guid = Guid.NewGuid().ToString();
            TitleInputField.text = string.Empty;
            DescriptionInputField.text = string.Empty;
            ChoiceUiComponents.ForEach(component => Destroy(component.gameObject));
            ChoiceUiComponents = new List<ChoiceUI>();
            UpdateThumbnailDropdown();
        }
        
        public void Load(int index) {
            Save();
            Thumbnail thumbnail = CurrentStory.Thumbnails[index];
            _guid = thumbnail.Guid;
            TitleInputField.text = thumbnail.Title;
            DescriptionInputField.text = thumbnail.Description;
            foreach (Choice choice in thumbnail.Choices) {
                GameObject instantiate = Instantiate(ChoicePrefab, ChoiceContent);
                ChoiceUI choiceUI = instantiate.GetComponent<ChoiceUI>();
                choiceUI.Load(this, choice);
                ChoiceUiComponents.Add(choiceUI);
            }
        }
        
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
        
        public void Delete() {
            Thumbnail find = CurrentStory.Thumbnails.Find(existingThumbnail => existingThumbnail.Guid == _guid);
            if (find != null) CurrentStory.Thumbnails.Remove(find);
        }

        public Thumbnail ToThumbnail() {
            return new Thumbnail {
                Guid = _guid,
                Title = TitleInputField.text,
                Description = DescriptionInputField.text,
                Choices = ChoiceUiComponents.Select(ui => ui.ToChoice()).ToList()
            };
        }

        public void UpdateThumbnailDropdown() {
            ThumbnailSelectorDropdown.ClearOptions();
            CurrentStory.Thumbnails.ForEach(thumbnail => ThumbnailSelectorDropdown.options.Add(new TMP_Dropdown.OptionData(thumbnail.Title)));
            ChoiceUiComponents.ForEach(component => component.UpdateThumbnailDropdown());
            ThumbnailSelectorDropdown.interactable = CurrentStory.Thumbnails.Count != 0;
        }
        
        public void AddChoice() {
            GameObject instantiate = Instantiate(ChoicePrefab, ChoiceContent);
            ChoiceUI choiceUI = instantiate.GetComponent<ChoiceUI>();
            choiceUI.Build(this);
            ChoiceUiComponents.Add(choiceUI);
        }
        
        public void RemoveChoice(ChoiceUI choiceUI) {
            ChoiceUiComponents.Remove(choiceUI);
            Destroy(choiceUI.gameObject);
        }
        
    }
}