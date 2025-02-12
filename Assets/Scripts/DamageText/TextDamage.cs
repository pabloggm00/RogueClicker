using TMPro;
using UnityEngine;

public class TextDamage : MonoBehaviour
{
    public TMP_Text text;

    [Header("Animation Settings")]
    public AnimationCurve opacity;
    public AnimationCurve scale;
    public AnimationCurve height;



    float time;
    Vector2 origin;

    private void Awake()
    {
        origin = transform.position;
    }

    private void Update()
    {
        text.color = new Color(1, 1, 1, opacity.Evaluate(time));

        transform.localScale = Vector3.one * scale.Evaluate(time);
        transform.position = origin + new Vector2(0, height.Evaluate(time));
        time += Time.deltaTime;
    }
}
