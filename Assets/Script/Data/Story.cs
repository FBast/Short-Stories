using System;
using System.Collections.Generic;
using Script.Component;
using UnityEngine;

namespace Script.Data {
    [Serializable]
    public class Story : ScriptableObject {
        
        public string StoryName;
        public List<Thumbnail> Thumbnails;

        public void Build(string storyName) 
        {
            StoryName = storyName;
            Thumbnails = new List<Thumbnail>();
        }

        public void AddThumbnail(ThumbnailUiComponent thumbnailUiComponent) {
            Thumbnail thumbnail = CreateInstance<Thumbnail>();
            thumbnail.Build(thumbnailUiComponent);
            Thumbnails.Add(thumbnail);
        }

    }
}
