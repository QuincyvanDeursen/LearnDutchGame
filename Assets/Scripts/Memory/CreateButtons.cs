using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateButtons : MonoBehaviour
{
    public GameObject rowPrefab;
    public int columns = 4;
    public GameObject buttonPrefab;

    public LevelDifficulty levelDifficulty
    {
        get
        {
            return LevelDifficultyManager.instance?.GetSelectedDifficulty() ?? LevelDifficulty.EASY;
        }
    }
    private int Rows
    {
        get
        {
            return levelDifficulty switch
            {
                LevelDifficulty.EASY => 2,
                LevelDifficulty.MODERATE => 4,
                LevelDifficulty.HARD => 6,
                _ => 7,// Default is easy
            };
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Rows * columns % 2 != 0)
        {
            Debug.LogError("Rows * Columns must be even");
            return;
        }
        
        for (int i = 0; i < Rows; i++)
        {
            GameObject row = Instantiate(rowPrefab, gameObject.transform);
            for (int j = 0; j < columns; j++)
            {
                GameObject button = Instantiate(buttonPrefab, row.transform);
                button.tag = "MemoryButton";
            }
        }
    }
}
