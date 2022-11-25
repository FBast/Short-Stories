using System.Collections.Generic;
using System.Linq;
using Script.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI {
    public class ThumbnailUI : MonoBehaviour {

        [SerializeField] private InputField TitleInputField;
        [SerializeField] private InputField DescriptionInputField;
        [SerializeField] private Dropdown ThumbnailSelectorDropdown;
        [SerializeField] private List<ChoiceUI> ChoiceUiComponents = new List<ChoiceUI>();
        [SerializeField] private Transform ChoiceContent;
        [SerializeField] private GameObject ChoicePrefab;
        [SerializeField] private Story CurrentStory;

        public void Load(int index) {
            Thumbnail thumbnail = CurrentStory.Thumbnails[index];
            TitleInputField.text = thumbnail.Title;
            DescriptionInputField.text = thumbnail.Description;
            UpdateThumbnailDropdown();
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
            Thumbnail find = CurrentStory.Thumbnails.Find(existingThumbnail => existingThumbnail.Title == thumbnail.Title);
            if (find != null) {
                find.Title = thumbnail.Title;
                find.Description = thumbnail.Description;
                find.Choices = thumbnail.Choices;
            }
            else {
                CurrentStory.Thumbnails.Add(thumbnail);
            }
            Reset();
            UpdateThumbnailDropdown();
        }
        
        public void Delete() {
            Thumbnail find = CurrentStory.Thumbnails.Find(existingThumbnail => existingThumbnail.Title == TitleInputField.text);
            if (find != null)
                CurrentStory.Thumbnails.Remove(find);
        }

        public Thumbnail ToThumbnail() {
            return new Thumbnail
            {
                Title = TitleInputField.text,
                Description = DescriptionInputField.text,
                Choices = ChoiceUiComponents.Select(ui => ui.ToChoice()).ToList()
            };
        }
        
        public void Reset() {
            TitleInputField.text = string.Empty;
            DescriptionInputField.text = string.Empty;
            ChoiceUiComponents.ForEach(component => Destroy(component.gameObject));
            ChoiceUiComponents = new List<ChoiceUI>();
        }
        
        public void UpdateThumbnailDropdown() {
            ThumbnailSelectorDropdown.ClearOptions();
            CurrentStory.Thumbnails.ForEach(thumbnail => ThumbnailSelectorDropdown.options.Add(new Dropdown.OptionData(thumbnail.Title)));
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