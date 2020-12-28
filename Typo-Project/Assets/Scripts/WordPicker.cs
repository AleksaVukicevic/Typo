using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class WordPicker : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI wordText;
    private string lastWord;
    public string Pick()
    {
        string word = GetWord();
        while (lastWord == word)
        {
            print("Duplicate");
            word = GetWord();
        }
        lastWord = word;
        wordText.text = word;
        return word;
    }
    private string GetWord()
    {
        return Words.words[Random.Range(0, Words.words.Length)];
    }
    private struct WordsLoadObject
    {
        public List<string> words;
    }
}

