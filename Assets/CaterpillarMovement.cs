using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaterpillarMovement : MonoBehaviour
{
    public float speed;
    Vector3 previousMousePosition;

    bool shouldMove = true;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float collisionDistance;
    [SerializeField] private Body bodyPiecePrefab;


    [SerializeField] private bool debug;

    [SerializeField] private List<Body> bodyPieces = new List<Body>();

    [SerializeField] private GameObject waterDrops;

    [SerializeField] private UIManager uIManager;

    [SerializeField] private AudioSource chomp;
    [SerializeField] private AudioSource death;

    public int score;


    void Update()
    {
        RotationManager();
        MovementManager();

        speed += Time.deltaTime * 0.05f;
    }

    void RotationManager()
    {
        Vector3 mousePos = GetMousePosition();
        Vector3 dir = mousePos - transform.position;

        dir.y = 0;

        if(Vector3.Distance(mousePos, transform.position) < transform.localScale.x / 2)
        {
            shouldMove = false;
            return;
        }
        shouldMove = true;

        
        Quaternion endRot = Quaternion.FromToRotation(transform.forward, dir.normalized) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, endRot, Time.deltaTime * rotationSpeed);

    }

    void MovementManager()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime * (shouldMove ? 1 : 0));
    }

    void FixedUpdate()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, collisionDistance))
        {
            if(hit.transform.TryGetComponent<Body>(out Body b))
            {
                Kill();
            }
            else if(hit.transform.CompareTag("DEATH"))
            {
                Kill();
            }
            else if(hit.transform.TryGetComponent<Grass>(out Grass g))
            {
                g.Rotate();
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.transform.TryGetComponent<Fruit>(out Fruit fruit))
        {
            if(fruit.isBug)
                Kill();

            if(fruit.isAlive)
            {
                chomp.Play();
                fruit.Despawn();
                AddBodyPiece();
                Instantiate(waterDrops, fruit.transform.position + Vector3.up * 0.1f, waterDrops.transform.rotation);
                score += 25;
                uIManager.UpdateScore(score);
            }
        }
    }

    void Kill()
    {
        death.Play();
        Debug.Log("KILLED");
        uIManager.EndGame();
    }

    void AddBodyPiece()
    {
        Body b = Instantiate(bodyPiecePrefab, transform.position - transform.forward * 10, transform.rotation, transform.parent);
        b.Init(bodyPieces[bodyPieces.Count - 1].transform);
        bodyPieces.Add(b);
    }
    
    Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("GROUND")))
        {
            return hit.point;
        }
        return transform.position;
    }

    void OnDrawGizmos()
    {
        if(!debug) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position + Vector3.up * 0.1f, transform.forward * collisionDistance);
    }

}
