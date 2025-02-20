using UnityEngine;

namespace FlappyPlane_Project
{
    public class Player : MonoBehaviour
    {
        private GameManager gameManager;

        private Animator animator;
        private Rigidbody2D _rigidbody;

        public float flapForce = 0f;
        public float forwardSpeed = 3f;
        public bool isDead = false;

        private float deathCoolDown = 0f;

        private bool isFlap = false;

        public bool godMode = false;

        private void Start()
        {
            gameManager = GameManager.Instance;

            animator = GetComponentInChildren<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (isDead)
            {
                if (deathCoolDown > 0f)
                {
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                    {
                        gameManager.RestartGame();
                    }
                }
                else
                {
                    deathCoolDown -= Time.deltaTime;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    isFlap = true;
                }
            }
        }

        private void FixedUpdate()
        {
            if (isDead) return;

            Vector3 velocity = _rigidbody.velocity;
            velocity.x = forwardSpeed;

            if (isFlap)
            {
                velocity.y += flapForce;
                isFlap = false;
            }

            _rigidbody.velocity = velocity;

            float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);
            float lerpAngle = Mathf.Lerp(transform.rotation.eulerAngles.z, angle, Time.fixedDeltaTime * 5);

            transform.rotation = Quaternion.Euler(0, 0, lerpAngle);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (godMode) return;

            if (isDead) return;

            animator.SetInteger("isDie", 1);
            isDead = true;
            deathCoolDown = 1f;
            gameManager.GameOver();
        }
    }
}