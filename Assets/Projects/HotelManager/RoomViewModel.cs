using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomViewModel : MonoBehaviour
{
    [SerializeField] private TMP_InputField _roomNumInputField;
    [SerializeField] private TMP_InputField _personInputField;
    [SerializeField] private Toggle _hasAnimalToggle;
    
    private RoomModel _model;

    public RoomModel Model => _model;

    public void Init(RoomModel model)
    {
        _model = model;
        
        _roomNumInputField.onValueChanged.AddListener(v => _model.number = int.Parse(v));
        _personInputField.onValueChanged.AddListener(v => _model.person = v);
        _hasAnimalToggle.onValueChanged.AddListener(v => _model.haveAnimal = v);
    }

    public void Reset()
    {
        if (_model == null) return;
        
        _roomNumInputField.text = _model.number.ToString();
        _personInputField.text = _model.person;
        _hasAnimalToggle.isOn = _model.haveAnimal;
    }
}