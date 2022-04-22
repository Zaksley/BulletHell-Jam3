using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    float rotation; 
    [SerializeField] float speed; 
    [SerializeField] float rotateSpeed; 

    private Rigidbody2D r; 
    public Camera cam; 

    private Vector2 movement; 
    private Vector2 mousePos; 

    void Start()
    {
        r = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); 
        movement.y = Input.GetAxisRaw("Vertical");  

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
    }

    void FixedUpdate() 
    {
        float magnitude = Mathf.Clamp01(movement.magnitude); 
        movement.Normalize(); 

        transform.Translate(movement * speed * magnitude * Time.deltaTime, Space.World); 

        Vector2 lookDir = mousePos - r.position; 
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f; 
        r.rotation = angle; 
    }
}
