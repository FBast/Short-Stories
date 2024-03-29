﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FullSerializer;
using Script.Data;
using Script.Utils;
using UnityEngine;

namespace Script.Managers {
    public static class DataManager {
        
        private static readonly fsSerializer Serializer = new fsSerializer();
        private static readonly string StoryPath = Application.streamingAssetsPath;

        public static List<string> StoryFiles => Directory.GetFiles(StoryPath, "*" + Properties.File.StoryExt)
                .Select(Path.GetFileName).ToList();

        public static void SaveStory(Story story) {
            string path = StoryPath + Path.DirectorySeparatorChar + story.StoryName + Properties.File.StoryExt;
            if (!File.Exists(path)) {
                FileStream fileStream = File.Create(path);
                fileStream.Close();
            }
            File.WriteAllText(path, Serialize(typeof(Story), story));
            Debug.Log("Story saved at " + path);
        }
 
        public static Story LoadStory(string storyNameWithExtension) {
            string path = StoryPath + Path.DirectorySeparatorChar + storyNameWithExtension;
            if(File.Exists(path)) File.OpenRead(path);
            else Debug.LogError("File not found");
            string fileJson = File.ReadAllText(path);
            return Deserialize(typeof(Story), fileJson) as Story;
        }

        public static bool CheckStory(string storyNameWithExtension) {
            string path = StoryPath + Path.DirectorySeparatorChar + storyNameWithExtension;
            return File.Exists(path);
        }
        
        private static string Serialize(Type type, object value) {
            // serialize the data
            fsData data;
            Serializer.TrySerialize(type, value, out data).AssertSuccessWithoutWarnings();

            // emit the data via JSON
            return fsJsonPrinter.CompressedJson(data);
        }

        private static object Deserialize(Type type, string serializedState) {
            // step 1: parse the JSON data
            fsData data = fsJsonParser.Parse(serializedState);

            // step 2: deserialize the data
            object deserialized = null;
            Serializer.TryDeserialize(data, type, ref deserialized).AssertSuccessWithoutWarnings();

            return deserialized;
        }
        
    }
}