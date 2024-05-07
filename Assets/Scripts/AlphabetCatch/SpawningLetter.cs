using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Letter : MonoBehaviour, IPointerClickHandler
{
    public float speed;

    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed);
    }
}
