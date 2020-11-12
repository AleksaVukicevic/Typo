using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using System.Linq;

public class WordPicker : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI wordText;
    //private List<string> words;

    void Awake()
    {
        //words = LoadWords();
    }

    public string Pick()
    {
        //string word = words[Random.Range(0, words.Count)];
        string word = Words.words[Random.Range(0, Words.words.Length)];
        wordText.text = word;
        return word;
    }
    //private List<string> LoadWords()
    //{
    //    string filePath = Application.dataPath + $"/{wordsFile}";   // Generate the file path
    //    if (File.Exists(filePath))
    //    {
    //        string jsonString = File.ReadAllText(filePath);     // Read the json file and put it in a string
    //        WordsLoadObject wordsObject = JsonUtility.FromJson<WordsLoadObject>(jsonString);    // Parse from json into an object
    //        return wordsObject.words;   // Return the list of words
    //    }
    //    else
    //    {
    //        Debug.LogError("No load file!");    // No file found
    //        return null;
    //    }
    //}
    private struct WordsLoadObject
    {
        public List<string> words;
    }
}

