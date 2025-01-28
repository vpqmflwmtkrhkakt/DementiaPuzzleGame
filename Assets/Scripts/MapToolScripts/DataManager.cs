using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class NodeData
{
    public NodeData(Vector2Int nodeIndex, Vector2Int siblingNodeIndex, Color nodeColor)
    {
        NodeIndex = nodeIndex;
        SiblingNodeIndex = siblingNodeIndex;
        NodeColor = nodeColor;
    }

    public Vector2Int NodeIndex;
    public Vector2Int SiblingNodeIndex;
    public Color NodeColor;
}

[System.Serializable]
public class SaveDataWrapper
{
    [SerializeField] 
    private List<NodeData> _nodeDataList = new List<NodeData>();
    [SerializeField]
    private int _row;
    [SerializeField]
    private int _col;

    // 노드 인덱스, 노드 컬러
    public void AddSaveData(Vector2Int index, Vector2Int siblingIndex, Color nodeColor)
    {
        _nodeDataList.Add(new NodeData(index, siblingIndex, nodeColor));
    }
    public List<NodeData> GetSaveDataList()
    {
        return _nodeDataList;
    }

    public void SetPlateNums(Vector2Int nums)
    {
        _row = nums.x;
        _col = nums.y;
    }
    public Vector2Int GetPlateNums()
    {
        return new Vector2Int(_row, _col);
    }
}

public class DataManager : Singleton<DataManager>
{
    private PlateCreator _plateCreator;
    private string _saveLoadPath;

    private void Start()
    {
        _saveLoadPath = Application.persistentDataPath + "/";
    }
    public void SaveCurrentMap(string saveFileName)
    {
        string saveFilePath = _saveLoadPath + saveFileName;

        // 필요한것
        // 1. 템플릿 가로세로 갯수(row, col)
        // 2. 깔려있는 노드의 인덱스(노드들은 저장할때 배열로 저장/로드)
        // 3. 깔려있는 노드의 컬러값

        SaveDataWrapper wrapper = new SaveDataWrapper();

        if(_plateCreator == null)
        {
            _plateCreator = GameObject.Find("PlateCreator").GetComponent<PlateCreator>();
            Debugger.CheckInstanceIsNullAndQuit(_plateCreator);
        }

        wrapper.SetPlateNums(_plateCreator.PlatesNum);

        List<List<ToolPlate>> plateList = _plateCreator.GetPlateList();

        foreach (List<ToolPlate> list in plateList)
        {
            foreach (ToolPlate plate in list)
            {
                ToolNode node = plate.GetPlacedNode();

                if (node != null)
                {
                    Vector2Int nodeIndex = _plateCreator.GetPlateIndex(plate.transform.position);
                    Vector2Int siblingIndex = _plateCreator.GetPlateIndex(node.SiblingNode.transform.position);
                    Color nodeColor = node.GetColor();
                    wrapper.AddSaveData(nodeIndex, siblingIndex, nodeColor);
                }
            }

            string jsonData = JsonUtility.ToJson(wrapper);
            File.WriteAllText(saveFilePath, jsonData);
        }
    }

    public void LoadMap(string saveFileName)
    {
        string loadFilePath = _saveLoadPath + saveFileName;

        if (File.Exists(loadFilePath))
        {
            string jsonData = File.ReadAllText(loadFilePath);

            SaveDataWrapper wrapper = JsonUtility.FromJson<SaveDataWrapper>(jsonData);
            LoadPlates(wrapper);
            LoadNode(wrapper);
        }
    }

    private static void LoadNode(SaveDataWrapper wrapper)
    {
        NodeCreator nodeCreator = GameObject.Find("NodeCreator").GetComponent<NodeCreator>();
        Debugger.CheckInstanceIsNullAndQuit(nodeCreator);

        List<NodeData> dataList = wrapper.GetSaveDataList();

        foreach (NodeData data in dataList)
        {
            nodeCreator.LoadNode(data.NodeIndex, data.SiblingNodeIndex, data.NodeColor);
        }
    }

    private void LoadPlates(SaveDataWrapper wrapper)
    {
        if (_plateCreator == null)
        {
            _plateCreator = GameObject.Find("PlateCreator").GetComponent<PlateCreator>();
            Debugger.CheckInstanceIsNullAndQuit(_plateCreator);
        }

        Vector2Int plateNums = wrapper.GetPlateNums();
        _plateCreator.CreatePlates(plateNums.x, plateNums.y);
    }
}
