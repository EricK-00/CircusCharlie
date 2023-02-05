using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleManager : MonoBehaviour
{
    private static ObstacleManager instance;

    private List<GameObject[]> fireRingPool;
    private List<GameObject[]> shortFireRingPool;
    private List<GameObject> monkeyPool;
    private List<GameObject> blueMonkeyPool;
    private List<GameObject> ballPool;

    [SerializeField]
    private GameObject fireRing_BackPrefab;
    [SerializeField]
    private GameObject fireRing_FrontPrefab;
    [SerializeField]
    private GameObject shortFireRing_BackPrefab;
    [SerializeField]
    private GameObject shortFireRing_FrontPrefab;
    [SerializeField]
    private GameObject monkeyPrefab;
    [SerializeField]
    private GameObject blueMonkeyPrefab;
    [SerializeField]
    private GameObject ballPrefab;

    public static ObstacleManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjects().GetComponent<ObstacleManager>();
                Instance.Initialize();
                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    private static GameObject FindObjects()
    {
        GameObject[] rootObjs_ = SceneManager.GetActiveScene().GetRootGameObjects();

        GameObject gameManager = default;
        foreach (GameObject rootObj in rootObjs_)
        {
            if (rootObj.name.Equals("ObstacleGenerator"))
            {
                gameManager = rootObj;
                return gameManager;
            }
            else { continue; }
        }       // loop

        return gameManager;
    }

    private void Initialize()
    {
        fireRingPool = new List<GameObject[]>();
        shortFireRingPool = new List<GameObject[]>();
        monkeyPool = new List<GameObject>();
        blueMonkeyPool = new List<GameObject>();
        ballPool = new List<GameObject>();

        for (int i = 0; i < 10; i++)
        {
            fireRingPool.Add(new GameObject[2] { Instantiate(fireRing_BackPrefab), Instantiate(fireRing_FrontPrefab) });
            shortFireRingPool.Add(new GameObject[2] { Instantiate(shortFireRing_BackPrefab), Instantiate(shortFireRing_FrontPrefab) });
            monkeyPool.Add(Instantiate(monkeyPrefab));
            blueMonkeyPool.Add(Instantiate(blueMonkeyPrefab));
            ballPool.Add(Instantiate(ballPrefab));
        }
    }

    public void CreateBall()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
