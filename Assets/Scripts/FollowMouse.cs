using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    public float speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Chain chain = GetComponent<Chain>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - rigidBody.position;
        rigidBody.velocity = direction * speed;
    }
}
