using UnityEngine;
[CreateAssetMenu(fileName = "DataSettings", menuName = "ScriptableObjects/SpawnDataSettings", order = 1)]

public class DataSettingsScriptableObject : ScriptableObject
{
    public TextAsset textAsset;
    public int coordinatesStartAt;
    public int coordinatesEndAt;
}
