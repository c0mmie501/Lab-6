using UnityEngine;
using UnityEngine.UIElements;
public class EnemyController : MonoBehaviour
{
    

    public float speed = 3f;
    public float chaseRange = 5f;
    public int health = 50;

    bool flag = false;
    Transform player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {



        if (!IsAlive())
        {

            Die();
        }

        if (chaseRange < Vector3.Distance(transform.position, player.position))
        {

            

            HandleMovement();
            flag = true;
        }
        else
        {
           



            HandleChase();
            flag = false;
        }
        
    }
    void HandleMovement()
    {
      
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

    void HandleChase()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < chaseRange)
        {
          
            transform.position = Vector3.MoveTowards(transform.position,player.position,speed * Time.deltaTime);
        }
    }


    public void OnCollisionEnter(Collision collision)
    {
         if (collision.gameObject.CompareTag("Kill"))
        {
            health -= 10;
        }
      
       
    }

    bool IsAlive()
    {
        return health > 0;
    }
    void Die()
    {
        Debug.Log("Enemy died.");
        FindObjectOfType<EnemySpawner>().RemoveEnemy(gameObject);
        Destroy(gameObject);
     
    }
}