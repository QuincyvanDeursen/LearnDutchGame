using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVCharacterLoader : MonoBehaviour
{
    public List<TextAsset> csvFiles;
    private List<char> characterList = new List<char>();
    public GameManager gameManager;

    void Start()
    {
        if (csvFiles != null)
        {
            ReadCSV(csvFiles[gameManager.getPlayerLevel()]);
        }
        else
        {
            Debug.LogError("CSV file is not assigned. Please assign a CSV file in the inspector.");
        }
    }

    void ReadCSV(TextAsset file)
    {
        string[] data = file.text.Split('\n'); // Split de data op elke nieuwe regel

        foreach (string line in data)
        {
            if (!string.IsNullOrEmpty(line))
            {
                string[] row = line.Split(','); // Split elke regel op komma's
                foreach (string entry in row)
                {
                    if (!string.IsNullOrEmpty(entry))
                    {
                        char character = entry[0]; // Neem het eerste karakter van elke entry
                        characterList.Add(character);
                    }
                }
            }
        }
    }

    public List<char> GetCharacterList()
    {
        return characterList;
    }
}
