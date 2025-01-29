using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private Plate _platePrefab;
    [SerializeField]
    private Node _nodePrefab;

    private PlateLoader _plateLoader;
    private NodeLoader _nodeLoader;
    private string _dataPath;
    protected override void Awake()
    {
        base.Awake();

        _plateLoader = new PlateLoader(_platePrefab);
        _nodeLoader = new NodeLoader(_nodePrefab);
        _dataPath = Application.persistentDataPath + '/';
    }

    public void LoadLevel(string levelName)
    {
        if (string.IsNullOrEmpty(levelName)) 
        { 
            return; 
        }

        string filePath = _dataPath + levelName;

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);

            SaveDataWrapper wrapper = JsonUtility.FromJson<SaveDataWrapper>(jsonData);
            LoadPlates(wrapper);
            LoadNode(wrapper);
        }


    }
    private void LoadPlates(SaveDataWrapper wrapper)
    {
        Vector2Int plateNums = wrapper.GetPlateNums();

        _plateLoader.CreatePlates(plateNums.x, plateNums.y);
    }

    private void LoadNode(SaveDataWrapper wrapper)
    {
        List<NodeData> dataList = wrapper.GetSaveDataList();

        foreach (NodeData data in dataList)
        {
            Vector3 nodePos = _plateLoader.GetPlateByIndex(data.NodeIndex).transform.position;
            nodePos.z = 0f;

            _nodeLoader.LoadNode(nodePos, data.NodeColor);
        }
    }
}
