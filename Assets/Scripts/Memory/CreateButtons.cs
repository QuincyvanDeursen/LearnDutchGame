using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateButtons : MonoBehaviour
{
    public int rows;
    public GameObject rowPrefab;
    public int columns;
    public GameObject buttonPrefab;
    // Start is called before the first frame update
    void Start()
    {
        print("CREATE BUTTONS");
        if (rows * columns % 2 != 0)
        {
            Debug.LogError("Rows * Columns must be even");
            return;
        }
        
        for (int i = 0; i < rows; i++)
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
