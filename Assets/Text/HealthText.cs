using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public float duration = 0.5f;
    public float raiseSpeed = 100;
    public Vector3 floatDirection = new Vector3(0,1,0);
    public TextMeshProUGUI tmp;
    float timeElapsed = 0.0f;
    RectTransform rTrf;
    public Color startingColor;

    void Start()
    {
        rTrf = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        rTrf.position += floatDirection * raiseSpeed * Time.deltaTime;
        tmp.color = new Color(startingColor.r, startingColor.g, startingColor.b, 1 - (timeElapsed/duration));
        if (timeElapsed >= duration) {
            Destroy(gameObject);
        }
    }
}
