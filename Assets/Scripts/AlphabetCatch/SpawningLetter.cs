using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawningLetter : MonoBehaviour
{
    public float speed;
    public float lifetime = 15.0f; 

    void Start()
    {
        Destroy(gameObject, lifetime);  
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.fixedDeltaTime);
    }

    void UpdateColliderPosition() {
        // Stel dat je collider een aparte component is die je handmatig moet bijwerken
        var collider = GetComponent<Collider>();
        collider.transform.position = transform.position;
    }
}
