using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO; 

public class MainManager : MonoBehaviour
{
    private static MainManager instance; 
    public static MainManager Instance
    {
        get { return instance; }
    }

    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighScoreText; 
    public GameObject GameOverText;
    public string username; 
    
    private bool m_Started = false;
    private int m_Points;
    private int m_Highscore; 
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        Init();
        LoadHighScore(); 
    }
        
    

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
       
    }
    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_GameOver = false;
                m_Started = false;
                Init(); 
                SceneManager.LoadScene(1);
            }
        }
    }
    
    private void Init()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";

        if(m_Points > m_Highscore)
        {
            HighScoreText.text = $"Highscore - {username} : {m_Points}";
        }
    }

    public void GameOver()
    {
        if (m_Points > m_Highscore)
        {
            m_Highscore = m_Points;
            SaveScore(); 
        }
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    public void SaveScore()
    {
        SaveData data = new SaveData();

        data.name = username;
        data.score = m_Highscore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); 
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            m_Highscore = data.score; 

            HighScoreText.text = $"Highscore - {data.name} : {data.score}";
        }
    }

    [System.Serializable]
    class SaveData
    {
        public string name;
        public int score; 
    }

}
