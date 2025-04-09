using UnityEngine;

public class enemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float stoppingDistance = 1f; // Distancia mínima antes de detenerse
    [SerializeField] private float visionRange = 5f; // Rango de visión del enemigo
    [SerializeField] private float patrolChangeTime = 5f; // Tiempo antes de cambiar de dirección al patrullar
    [SerializeField] private float patrolAnimSpeed = 0.2f; // Velocidad de animación cuando patrulla
    [SerializeField] private float followAnimSpeed = 1f; // Velocidad de animación cuando sigue al jugador

    private Rigidbody2D rb;
    private Transform player;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 patrolDirection;
    private float patrolTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Encontrar al jugador por su etiqueta
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        patrolTimer = patrolChangeTime; // Inicializar el temporizador de patrullaje
        ChooseNewPatrolDirection(); // Escoger una dirección inicial de patrullaje
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer < visionRange)
            {
                FollowPlayer(distanceToPlayer);
            }
            else
            {
                Patrol();
            }

            AdjustFacingDirection();
        }
    }

    private void FollowPlayer(float distanceToPlayer)
    {
        if (distanceToPlayer > stoppingDistance)
        {
            // Movimiento hacia el jugador
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity  = direction * moveSpeed;

            // Cambiar velocidad de animación a la velocidad de seguimiento
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
            animator.SetFloat("AnimationSpeed", followAnimSpeed);
        }
        else
        {
            // Detenerse completamente cuando está cerca del jugador
            rb.velocity = Vector2.zero;
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", 0);
            animator.SetFloat("AnimationSpeed", followAnimSpeed);
        }
    }

    private void Patrol()
    {
        patrolTimer -= Time.fixedDeltaTime;

        if (patrolTimer <= 0)
        {
            ChooseNewPatrolDirection();
            patrolTimer = patrolChangeTime;
        }

        // Moverse en la dirección elegida
        rb.velocity = patrolDirection * (moveSpeed * 0.5f); // La velocidad en patrullaje es menor

        // Cambiar velocidad de animación a la velocidad de patrullaje
        animator.SetFloat("MoveX", patrolDirection.x);
        animator.SetFloat("MoveY", patrolDirection.y);
        animator.SetFloat("AnimationSpeed", patrolAnimSpeed);
    }

    private void ChooseNewPatrolDirection()
    {
        // Escoger una dirección aleatoria
        patrolDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void AdjustFacingDirection()
    {
        if (rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}