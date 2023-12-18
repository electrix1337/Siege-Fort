using UnityEngine;

public class RessouceExtractorComponent : MonoBehaviour
{
    [SerializeField] float timeBetweenExtraction;
    [SerializeField] string ressourceName;
    [SerializeField] int amountToExtract;

    RessourceManagerComponent ressourceManager;
    float time = 0;

    private void Start()
    {
        ressourceManager = GameObject.Find(GameObjectPath.GetPath("RessourceManager")).GetComponent<RessourceManagerComponent>();
    }



    private void Update()
    {
        time += Time.deltaTime;
        if (time > timeBetweenExtraction)
        {
            time -= timeBetweenExtraction;
            ressourceManager.AddRessource(ressourceName, amountToExtract);
        }
    }
}
