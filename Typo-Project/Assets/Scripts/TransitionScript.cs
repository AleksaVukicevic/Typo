using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class TransitionScript : MonoBehaviour
{
    [SerializeField] private float time;
    void Start()
    {
        GetComponent<Image>().enabled = true;

        Destroy(this.gameObject, time);
    }
}
