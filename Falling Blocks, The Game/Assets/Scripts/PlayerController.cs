using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 7;
    public event System.Action OnPlayerDeath;

    float screenHalfWidthInWorldUnits;
    float halfPlayerWidth;
    // Start is called before the first frame update
    void Start() {
        halfPlayerWidth = transform.localScale.x / 2f;
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update() {
        float inputX = Input.GetAxisRaw ("Horizontal");
        float velocity = inputX * speed;
        float playerAndWorldHalfWidthUnits = screenHalfWidthInWorldUnits - halfPlayerWidth;
        transform.Translate (Vector2.right * velocity * Time.deltaTime);

        if (transform.position.x < -screenHalfWidthInWorldUnits) {
            transform.position = new Vector2 (playerAndWorldHalfWidthUnits, transform.position.y);
        }
        if (transform.position.x > screenHalfWidthInWorldUnits) {
            transform.position = new Vector2 (-playerAndWorldHalfWidthUnits, transform.position.y);
        }
       
    }

    void OnTriggerEnter2D(Collider2D triggerCollider) {
        if (triggerCollider.tag == "Block") {
            if (OnPlayerDeath != null) {
                OnPlayerDeath();
            }
            Destroy (gameObject);
        }
    }

}
