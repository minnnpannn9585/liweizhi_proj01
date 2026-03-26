using UnityEngine;
using UnityEngine.UI;

public class PropBtn : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private GameObject targetToActivate;
    [SerializeField] private Sprite img;

    private void Awake()
    {
        var btn = GetComponent<Button>();

            btn.onClick.AddListener(ActivateTarget);

    }

    private void OnDestroy()
    {
        var btn = GetComponent<Button>();

            btn.onClick.RemoveListener(ActivateTarget);
        
    }

    private void ActivateTarget()
    {

        targetToActivate.SetActive(true);
        targetToActivate.GetComponent<SpriteRenderer>().sprite = img;
    }
}