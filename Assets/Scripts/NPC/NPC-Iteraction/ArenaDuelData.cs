using UnityEngine;

public class ArenaDuelData : MonoBehaviour
{
    public static ArenaDuelData Instance;

    public int npcLevel;
    public string npcNickname;
    public int reward;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Не уничтожать при смене сцены
        }
        else
        {
            Destroy(gameObject);
        }
    }
}