using System.Collections;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D m_RbProjectile;
    public float force;
    private float timeRemaining = 10000;
    
    // Start is called before the first frame update
    void Start()
    {
    
    StartCoroutine(StartIgnoreLayerCollision());
    
    //Fetching the main camera object attached to the MainCamera game object
    mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    
    // Attaching the Rigidbody2D component of the present game object to m_RbProjectile
    m_RbProjectile = GetComponent<Rigidbody2D>();
    
    // Converting the mouse position from screen space to world space
    mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

    // Subtracting object position from mouse position to get direction vector
    Vector3 direction = mousePos - transform.position;
    
    // Subtracting mouse position from object position to get rotation vector
    Vector3 rotation = transform.position - mousePos;

    // Normalizing the direction vector and multiplying it by force to set velocity
    m_RbProjectile.velocity = new Vector2(direction.x, direction.y).normalized * force;

    // Calculating rotation of the object to face the mouse position
    float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
    
    // Setting object rotation to face the mouse position
    transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    
    // Destroying the game object after 3 seconds
    Destroy(gameObject, 3f);
    
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                enemy.GoToSleep();
            }
            
        }
        Destroy(gameObject);
        Physics2D.IgnoreLayerCollision(0, 0, false);
    }
     
    IEnumerator StartIgnoreLayerCollision()
    {
        yield return new WaitForSeconds(1); // specify the wait time here
        // unignoring the collision between the objects of same layer
        Physics2D.IgnoreLayerCollision(0, 0, false);
    }
}

