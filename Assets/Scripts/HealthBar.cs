using UnityEngine;
using UnityEngine.UIElements;

public class HealthBar : MonoBehaviour
{
    public PlayerStatistics stats;
    [SerializeField]
    [Tooltip("The maximum number of full hearts the player can have.")]
    private int _maxHearts;
    private int _maxHealth;
    [SerializeField]
    [Tooltip("One unit of health is half a heart.")]
    [Range(0, 20)]
    private int _health;
    private UIDocument _doc;

    private static readonly string emptyHeartClassName = "empty-heart";
    private static readonly string halfHeartClassName = "half-heart";
    private static readonly string fullHeartClassName = "full-heart";

    void OnEnable()
    {
        _doc = GetComponent<UIDocument>();
        _health = stats.getCurrentHealth();
        _maxHealth = stats.getMaxHealth();
        CalculateHearts();
        stats.HealthChanged.AddListener(OnHealthChange);
        stats.MaxHealthChanged.AddListener(OnMaxHealthChange);
    }

    void OnDisable()
    {
        stats.HealthChanged.RemoveListener(OnHealthChange);
        stats.MaxHealthChanged.RemoveListener(OnMaxHealthChange);
    }

    void Update()
    {
        CalculateHearts();
    }

    void OnHealthChange(int health)
    {
        _health = health;
    }

    void OnMaxHealthChange(int maxHealth)
    {
        _maxHearts = maxHealth / 2;
    }

    void CalculateHearts()
    {
        var root = _doc.rootVisualElement;
        var heartContainer = root.Q<VisualElement>("heart-container");
        heartContainer.Clear();

        _maxHealth = _maxHearts * 2;
        var halfHearts = _health % 2;
        var fullHearts = (_health - halfHearts) / 2;
        var emptyHearts = (_maxHealth / 2) - (halfHearts + fullHearts);

        for (int i = 0; i < fullHearts; i++)
        {
            AddHeart(heartContainer, fullHeartClassName);
        }

        if (halfHearts > 0)
        {
            AddHeart(heartContainer, halfHeartClassName);
        }

        for (int i = 0; i < emptyHearts; i++)
        {
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
