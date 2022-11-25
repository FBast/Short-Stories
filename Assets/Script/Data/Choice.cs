using System;

namespace Script.Data {
    [Serializable]
    public class Choice {

        public string Description;
        public Thumbnail Link;

        public Choice(string description, Thumbnail link) {
            Description = description;
            Link = link;
        }

    }
}