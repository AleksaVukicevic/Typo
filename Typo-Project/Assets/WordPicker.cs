using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordPicker : MonoBehaviour
{
    private string wordsFile = "words.json";
    private List<string> words;

    void Start()
    {
        words = LoadWords();
        print(words.Count);//For testing
    }
    public string PickWord()
    {
        return words[Random.Range(0, words.Count)]; // Returns a random word from the array
    }
    private List<string> LoadWords()
    {
        string filePath = Application.dataPath + $"/{wordsFile}";   // Generate the file path
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);     // Read the json file and put it in a string
            WordsLoadObject wordsObject = JsonUtility.FromJson<WordsLoadObject>(jsonString);    // Parse from json into an object
            return wordsObject.words;   // Return the list of words
        }
        else
        {
            Debug.LogError("No load file!");    // No file found
            return null;
        }
    }
    private struct WordsLoadObject
    {
        public List<string> words;
    }
}

