using UnityEngine;
using UnityEngine.SceneManagement;  // ���������ҹ SceneManager
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Collections;

public class SaveLoadSystem : MonoBehaviour
{
    public static SaveLoadSystem Instance;

    public Transform player;
    public int currentLevel;
    public int score;

    // ��ª��ͧ͢�ѵ�ط�����������������������͡����
    public List<string> repairedObjectIDs = new List<string>();
    public List<string> collectedItemIDs = new List<string>();

    [System.Serializable]
    public class SaveData
    {
        public float playerX, playerY, playerZ;
        public float rotX, rotY, rotZ, rotW;
        public int currentLevel;
        public int score;
        public List<string> repairedObjectIDs;
        public List<string> collectedItemIDs;
        public string currentScene;  // �ѹ�֡���ͫչ�Ѩ�غѹ
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // ����� GameObject ������١������������Ŵ�չ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.playerX = player.position.x;
        data.playerY = player.position.y;
        data.playerZ = player.position.z;
        data.rotX = player.rotation.x;
        data.rotY = player.rotation.y;
        data.rotZ = player.rotation.z;
        data.rotW = player.rotation.w;
        data.currentLevel = currentLevel;
        data.score = score;
        data.repairedObjectIDs = repairedObjectIDs;
        data.collectedItemIDs = collectedItemIDs;
        data.currentScene = SceneManager.GetActiveScene().name;  // �ѹ�֡���ͫչ�Ѩ�غѹ

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savegame.dat";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, data);
        stream.Close();

        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savegame.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            // ��Ŵ�չ���١�ѹ�֡���
            string savedScene = data.currentScene;
            if (!string.IsNullOrEmpty(savedScene))
            {
                // ���¡�� Coroutine ������Ŵ�չ��е�駤�� Player ��ѧ�ҡ�չ��Ŵ����
                StartCoroutine(LoadSceneAndInitialize(savedScene, data));
            }
            else
            {
                Debug.Log("No scene found to load.");
            }
        }
        else
        {
            Debug.Log("No saved data found.");
        }
    }

    // Coroutine ������Ŵ�չ��е�駤�� player ��ѧ�ҡ�չ��Ŵ����
    private IEnumerator LoadSceneAndInitialize(string sceneName, SaveData data)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // ��ѧ�ҡ��Ŵ�չ���� ���� Player
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            // ���絵��˹���С�õ�駤�ҵ�ҧ� �ͧ player ������ѹ�֡���
            player.position = new Vector3(data.playerX, data.playerY, data.playerZ);
            player.rotation = new Quaternion(data.rotX, data.rotY, data.rotZ, data.rotW);
        }

        // ��ʶҹТͧ�ѵ�ط������������Ѻ�ء RepairableObject 㹫չ
        RepairableObject[] repairables = FindObjectsOfType<RepairableObject>();
        foreach (var r in repairables)
        {
            if (repairedObjectIDs.Contains(r.objectID))
            {
                r.SetAsRepaired();
            }
        }

        // ��ʶҹТͧ�����������͡�������Ѻ�ء SelectableItem 㹫չ
        SelectableItem[] items = FindObjectsOfType<SelectableItem>();
        foreach (var item in items)
        {
            if (collectedItemIDs.Contains(item.objectID))
            {
                item.SetAsCollected();
            }
        }

        Debug.Log("Game Loaded");
    }
}
