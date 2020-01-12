using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public GameObject Explosion;

    public float waitBeforeStart = 3f;
    public Animator anim;
    public float wiggleAmplitude = 2f;
    public float wiggleFrequency = 5f;


    public float minAttackTime = 2f;
    public float maxAttackTime = 4f;
    public float tongueAttackChance = 0.3f;

    public GameObject emptyBoxPrefab;
    public GameObject mysteryBoxPrefab;
    public Transform[] boxSpawnPoints;

    public Rigidbody2D bellRB;
    public AudioSource bellAudio;

    private GameObject[] spawnedBoxes = new GameObject[0];
    [SerializeField]
    private string newLevel;

    bool works = false;
    float wiggleBlend = 0f;
    Wiggle wiggle;
    Transform spriteTransform;
    public static bool isDead = false;

    public static Boss instance;

    // TONGUE ATTACK
    public bool isContactingPlayer;
    public int damage = 10;
    

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(StartRoutine());
        spawnedBoxes = new GameObject[boxSpawnPoints.Length];
        spriteTransform = transform.GetChild(0);
        wiggle = new Wiggle(wiggleAmplitude, wiggleFrequency, 0f, Vector2.zero);
    }

    private void Update()
    {
        if (works && !isDead)
        {
            wiggleBlend = Mathf.MoveTowards(wiggleBlend, 1f, Time.deltaTime);
            Vector2 pos2d = wiggle.GetPosition() * wiggleBlend;
            spriteTransform.localPosition = pos2d;
        }
        
    }

    IEnumerator StartRoutine()
    {
        yield return new WaitForSeconds(waitBeforeStart);
        anim.SetBool("Out", true);
        works = true;
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(Random.Range(minAttackTime, maxAttackTime));
        if (Random.Range(0f, 1f) <= tongueAttackChance)
        {
            bellAudio.Play();
            yield return new WaitForSeconds(0.1f);
            DoTongueAttack();
            // anim.SetBool("Lick", false);
        }
        else DoBoxAttack();
        StartCoroutine(AttackRoutine());
    }

    // Disable Tongue Collider after 1.5 sec
    IEnumerator ActivationRoutine()
    {
        yield return new WaitForSeconds(1.2f);
        GameObject.Find("TongueCollider").GetComponent<Collider2D>().enabled = false;
    }

    void DoTongueAttack()
    {
        ////////////////////////////////////////////////////////////////////////
        GameObject.Find("TongueCollider").GetComponent<Collider2D>().enabled = true;
        anim.SetTrigger("lick");
        StartCoroutine(ActivationRoutine());
        
    }

    void DoBoxAttack()
    {
        if (BoxesExist()) return;
        int mysteryIndex = Random.Range(0, boxSpawnPoints.Length);
        for (int i = 0; i < boxSpawnPoints.Length; i++)
        {
            if (i == mysteryIndex) spawnedBoxes[i] = SpawnBox(boxSpawnPoints[i], mysteryBoxPrefab);
            else spawnedBoxes[i] = SpawnBox(boxSpawnPoints[i], emptyBoxPrefab);
        }
    }

    bool BoxesExist()
    {
        for (int i = 0; i < spawnedBoxes.Length; i++)
        {
            if (spawnedBoxes[i] != null) return true;
        }
        return false;
    }

    GameObject SpawnBox(Transform spawnPoint, GameObject prefab)
    {
        GameObject go = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        float rightForce = Random.Range(4f, 9f);
        float upForce = Random.Range(2f, 6f);
        rb.AddForce(Vector2.right * rightForce + Vector2.up * upForce, ForceMode2D.Impulse);
        return go;
    }

    //To kill the boss, call Boss.instance.Die();
    public void Die()
    {
        Instantiate(Explosion, transform.localPosition, transform.rotation);
        if (isDead) return;
        bellRB.isKinematic = false;
        bellRB.transform.parent = null;
        Vector3 pos = bellRB.position;
        Collider2D bellCollider = bellRB.GetComponent<Collider2D>();
        bellCollider.isTrigger = false;
        bellAudio.Play();
        isDead = true;
        float playbackTime = anim.playbackTime;
        anim.Rebind();
        anim.playbackTime = playbackTime;
        bellRB.gameObject.SetActive(true);
        bellRB.position = pos;
        bellRB.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
        Invoke("ChangeScene", 3f);
        StopAllCoroutines();
    }

    void ChangeScene()
    {
        Debug.Log("Scene Changing");
        
        SceneManager.LoadScene(newLevel);
    }
    
   
}