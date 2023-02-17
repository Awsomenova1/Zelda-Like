using UnityEngine;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private int _maxHealth;
    [SerializeField]
    [Range(0, 10)]
    private int _health;
    private UIDocument _doc;

    private static readonly string emptyHeartClassName = "empty-heart";
    private static readonly string halfHeartClassName = "half-heart";
    private static readonly string fullHeartClassName = "full-heart";

    void OnEnable()
    {
        _doc = GetComponent<UIDocument>();
        CalculateHearts();
    }

    void Update() {
        CalculateHearts(); 
    } 

    void CalculateHearts() {
        var root = _doc.rootVisualElement;
        var heartContainer = root.Q<VisualElement>("heart-container");
        heartContainer.Clear();

        var halfHearts = _health % 2;
        var fullHearts = (_health - halfHearts) / 2;
        var emptyHearts = (_maxHealth / 2) - (halfHearts + fullHearts);

        for (int i = 0; i < fullHearts; i++) {
           AddHeart(heartContainer, fullHeartClassName); 
        } 

        if (halfHearts > 0) {
           AddHeart(heartContainer, halfHeartClassName); 
        }

        for (int i = 0; i < emptyHearts; i++) {
            AddHeart(heartContainer, emptyHeartClassName);
        }
 
    }
     
    void AddHeart(VisualElement heartContainer, string type)
    {
        var heart = new VisualElement();
        heart.AddToClassList(type);
        heartContainer.Add(heart);
    }

}
