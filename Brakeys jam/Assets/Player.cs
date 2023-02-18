using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

internal sealed class Player : MonoBehaviour
{
    [SerializeField]
    private float m_Speed, m_Jumpforce;
    public float countdown;
    public TextMeshProUGUI scord;

    [SerializeField]
    private Vector2 m_SpawnPoint;

    private Rigidbody2D _rigidbody2D = null;
    private float _movementInput;
    private bool _isGrounded = true;
    public int scenint;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
            Jump();
        scord.text = countdown.ToString();
        if(countdown == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, m_Jumpforce);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _movementInput = Input.GetAxis("Horizontal");
        if (_movementInput > 0 || _movementInput < 0)
        {
            _rigidbody2D.velocity = new Vector2(_movementInput * m_Speed, _rigidbody2D.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider == null)
            return;

        switch (collider.tag)
        {
            case "Ground":
                _isGrounded = true;
                break;
            case "Die Surface":
                Die();
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider == null)
            return;
       
        if (collider.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }

    private void Die()
    {
        GameObject diedPlayer = Instantiate(gameObject, transform.position, Quaternion.identity);
        diedPlayer.tag = "Ground";
        diedPlayer.GetComponent<SpriteRenderer>().color = Color.black;
        Destroy(diedPlayer.GetComponent<BoxCollider2D>());
        Destroy(diedPlayer.GetComponent<Player>());
        Destroy(diedPlayer.GetComponent<Rigidbody2D>());
        transform.position = m_SpawnPoint;
        countdown -= 1;
    }
     
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "end")
        {
            SceneManager.LoadScene(scenint);
            scenint += 1;
        }
    }
}
