using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Header("Portal Settings")]
    [SerializeField] private string targetSceneName = "";
    [SerializeField] private bool requirePlayerTag = true;
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private bool requireKeyPress = false;

    [Space]
    [SerializeField] private ToolsList weaponsContainer;
    [SerializeField] private GameObject requireText;
    [SerializeField] private int requireToolLevel;

    private bool playerInTrigger = false;

    private void Awake()
    {
        if (requireText != null)
            requireText.SetActive(false);
    }

    private void Update()
    {
        if (playerInTrigger && requireKeyPress && Input.GetKeyDown(interactKey))
            TryLoadScene();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (requirePlayerTag && !other.CompareTag("Player"))
            return;
        
        playerInTrigger = true;
        TryLoadScene();
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (requirePlayerTag && !other.CompareTag("Player"))
            return;
            
        playerInTrigger = false;

        if (requireText != null)
            requireText.SetActive(false);
    }

    private void TryLoadScene()
    {
        if (string.IsNullOrEmpty(targetSceneName))
        {
            Debug.LogWarning("Portal: Не задано имя целевой сцены!");
            return;
        }

        if (requireToolLevel == 0)
        {
            StartCoroutine(Load());
            return;
        }

        int currentToolId = ToolSaver.LoadCurrentTool();
        ToolSkinItem currentTool = weaponsContainer.Weapons.ElementAt(currentToolId);
        int currentToolLevel = currentTool.Prefab.GetComponent<Tool>().ToolData.toolLevel;

        if (currentToolLevel >= requireToolLevel)
        {
            StartCoroutine(Load());
            return;
        }

        requireText.SetActive(true);
    }

    private IEnumerator Load()
    {
        SFXProvider.Play("TeleportSound");
        yield return SceneManager.LoadSceneAsync(targetSceneName);
    }
}