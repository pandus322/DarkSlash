using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private TMP_Text _desription;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private Image _image;
    [SerializeField] private Button _sellButton;
    private Goods _good;

    public event UnityAction<Goods, ItemContainer> SellButtonClick;

    private void OnEnable()
    {
        _sellButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _sellButton.onClick.RemoveListener(OnButtonClick);
    }

    public void Render(Goods good)
    {
        _label.text = good.Label;
        _desription.text = good.Description;
        _price.text = good.Price.ToString();
        _level.text = $"LV: {good.Level}";
        _image.sprite = good.Image;
        _good = good;
    }

    private void OnButtonClick()
    {
        SellButtonClick?.Invoke(_good, this);
    }
}
