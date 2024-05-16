using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDifficultyManager : MonoBehaviour
{
    private LevelDifficulty selectedDifficulty;
    public static LevelDifficultyManager instance;

    public void Awake()
    {
        instance = this;
    }

    public LevelDifficulty GetSelectedDifficulty()
    {
        return selectedDifficulty;
    }

    public void SetSelectedDifficulty(int difficulty)
    {
        selectedDifficulty = (LevelDifficulty)difficulty;
    }
}
