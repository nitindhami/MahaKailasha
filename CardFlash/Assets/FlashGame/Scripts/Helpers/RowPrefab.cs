
using UnityEngine;

public class RowPrefab : MonoBehaviour
{
    void Start()
    {
        GameController.s_Instance.onGameCompleted += selfDestruct;
    }

    private void OnDestroy()
    {
        GameController.s_Instance.onGameCompleted -= selfDestruct;
    }
    // To Improve and make it better.
    void selfDestruct()
    {
        Destroy(gameObject);

    }

}
