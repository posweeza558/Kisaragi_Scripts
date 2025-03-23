using UnityEngine;
using UnityEngine.SceneManagement;  // เพิ่มการใช้งาน SceneManager
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

    // รายชื่อของวัตถุที่ซ่อมแล้วและไอเท็มที่เลือกแล้ว
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
        public string currentScene;  // บันทึกชื่อซีนปัจจุบัน
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // ทำให้ GameObject นี้ไม่ถูกทำลายเมื่อโหลดซีนใหม่
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
        data.currentScene = SceneManager.GetActiveScene().name;  // บันทึกชื่อซีนปัจจุบัน

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

            // โหลดซีนที่ถูกบันทึกไว้
            string savedScene = data.currentScene;
            if (!string.IsNullOrEmpty(savedScene))
            {
                // เรียกใช้ Coroutine เพื่อโหลดซีนและตั้งค่า Player หลังจากซีนโหลดเสร็จ
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

    // Coroutine เพื่อโหลดซีนและตั้งค่า player หลังจากซีนโหลดเสร็จ
    private IEnumerator LoadSceneAndInitialize(string sceneName, SaveData data)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // หลังจากโหลดซีนแล้ว ค้นหา Player
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            // รีเซ็ตตำแหน่งและการตั้งค่าต่างๆ ของ player ตามที่บันทึกไว้
            player.position = new Vector3(data.playerX, data.playerY, data.playerZ);
            player.rotation = new Quaternion(data.rotX, data.rotY, data.rotZ, data.rotW);
        }

        // นำสถานะของวัตถุที่ซ่อมแล้วไปใช้กับทุก RepairableObject ในซีน
        RepairableObject[] repairables = FindObjectsOfType<RepairableObject>();
        foreach (var r in repairables)
        {
            if (repairedObjectIDs.Contains(r.objectID))
            {
                r.SetAsRepaired();
            }
        }

        // นำสถานะของไอเท็มที่เลือกแล้วไปใช้กับทุก SelectableItem ในซีน
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
