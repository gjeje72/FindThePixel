using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;
    public GameObject target;
    public GameObject[] checkpoints;
    public GameObject[] laserGroup1;
    public GameObject[] laserGroup2;
    public GameObject[] laserGroup3;
    public GameObject walls;
    public GameObject player;
    public Material visibleWall;
    public Material visiblePlayer;
    public Material invisible;

    private int activeScene => SceneManager.GetActiveScene().buildIndex;

    /// <inheritdoc/>
    private void Awake()
    {
        Instance = this;

        if (this.activeScene == 4 || this.activeScene == 10)
        {
            StartCoroutine("disappearance");
            InvokeRepeating("flashing", 4, 4);
        }

        if (this.activeScene == 6)
        {
            InvokeRepeating("lasershow", 3f, 6f);
        }

        if(this.activeScene == 8)
        {
            InvokeRepeating("lasershow2", 3f, 9f);
        }
    }

    /// <summary>
    ///     Method used to randomly instantiate the target.
    /// </summary>
    private void Start()
    {
        var maxRange = checkpoints.Length;
        var random = Random.Range(0, maxRange);
        Instantiate(target, checkpoints[random].transform.position, checkpoints[random].transform.rotation);
    }

    /// <summary>
    ///     Coroutine used to desappearance walls and player.
    /// </summary>
    IEnumerator disappearance()
    {
        yield return new WaitForSeconds(1);
        foreach (Renderer r in walls.GetComponentsInChildren<Renderer>())
            r.material.DOColor(invisible.color, 2);

        player.GetComponent<Renderer>().material.DOColor(invisible.color, 2);
    }

    /// <summary>
    ///     Method used to see walls and player for 1 seconde.
    /// </summary>
    private void flashing()
    {
        foreach (Renderer r in walls.GetComponentsInChildren<Renderer>())
        {
            r.material.color = visibleWall.color;
            r.material.DOColor(invisible.color, 2);
        }
        player.GetComponent<Renderer>().material.color = visiblePlayer.color;
        player.GetComponent<Renderer>().material.DOColor(invisible.color, 2);
    }

    /// <summary>
    ///     Method used to start laser coroutine.
    /// </summary>
    private void lasershow()
    {
        StartCoroutine(this.laser( this.laserGroup1, 0f));
        
        StartCoroutine(this.laser(this.laserGroup2, 3f));
    }

    /// <summary>
    ///     Method used to start laser coroutine 2.
    /// </summary>
    private void lasershow2()
    {
        StartCoroutine(this.laser(this.laserGroup1, 0f));
        StartCoroutine(this.laser(this.laserGroup2, 3f));
        StartCoroutine(this.laser(this.laserGroup3, 6f));
    }

    /// <summary>
    ///     Coroutine used to switch lasers Off , wait for 3s and switch lasers On.
    /// </summary>
    /// <param name="lasers">Lasers to manage.</param>
    /// <param name="waitingTime">Delay to apply when coroutine start.</param>
    IEnumerator laser(GameObject[] lasers, float waitingTime)
    {
        yield return new WaitForSeconds(waitingTime);
        var rays = new List<GameObject>();
        foreach (var laser in lasers)
        {
            var ray = laser.transform.Find("ray").gameObject;
            rays.Add(ray);
            ray.SetActive(false);
        }
        yield return new WaitForSeconds(3);
        rays.ForEach(x => x.SetActive(true));
    }
}
