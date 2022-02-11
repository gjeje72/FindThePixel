using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyMovement : MonoBehaviour
{
    private Vector3 initPos;
    public List<GameObject> checkpoints;
    private GameObject target;
    public float speed;
    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        initPos = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.target = checkpoints[i];
        Vector3 dir = this.target.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
            i++;

        if (i == checkpoints.Count)
            i = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("laser"))
            this.gameObject.transform.position = initPos;
    }
}
