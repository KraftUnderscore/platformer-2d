using UnityEngine;

public class Lever : MonoBehaviour, Interactable
{
    [SerializeField] private GameObject[] itemObjs;
    [SerializeField] private Sprite unactiveSprite;

    private InteractableItem[] items;
    private SpriteRenderer rend;
    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        rend = transform.GetChild(0).GetComponent<SpriteRenderer>();

        items = new InteractableItem[itemObjs.Length];
        for (int i = 0; i < itemObjs.Length; i++)
            items[i] = itemObjs[i].GetComponent<InteractableItem>();
    }

    public void Interact()
    {
        col.enabled = false;
        rend.sprite = unactiveSprite;

        foreach (InteractableItem item in items) 
            item.InteractAction();
    }

}
