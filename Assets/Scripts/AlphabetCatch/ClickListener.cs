using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickListener : MonoBehaviour, IPointerClickHandler
{
    private GameObject LetterSpawnObject;

    public void Start()
    {
        LetterSpawnObject = GameObject.Find("Spawner");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TextMeshProUGUI textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
        LetterSpawnObject.GetComponent<LetterSpawner>().HandleClick(textMeshPro.text.ToCharArray()[0], gameObject);
    }

}
