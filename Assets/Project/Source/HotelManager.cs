using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HotelManager : MonoBehaviour
{
    [SerializeField] private RoomViewModel _prefab;
    [SerializeField] private Transform _contentRoot;
    [SerializeField] private Button _addButton;
    [SerializeField] private Button _resetButton;
    [SerializeField] private Button _magicButton;
    private readonly List<RoomViewModel> _roomVMs = new();
    private readonly List<RoomModel> _roomModels = new();

    private void Awake()
    {
        _addButton.onClick.AddListener(Add);
        _resetButton.onClick.AddListener(ResetAll);
        _magicButton.onClick.AddListener(ThoseMethod);
    }

    private void Add()
    {
        var vm = Instantiate(_prefab, _contentRoot);
        var data = new RoomModel();
        vm.Init(data);
        _roomModels.Add(data);
        _roomVMs.Add(vm);
    }

    private void ResetAll() => _roomVMs.ForEach(v => v.Reset());

    private void ThoseMethod()
    {
        Debug.Log("With animals count: " + _roomModels.Count(d => d.haveAnimal));
        Debug.Log("Smiths" + _roomModels.Count(d => d.person == "Smith"));
        Debug.Log("Single numbers" + _roomModels.Count(d => d.number / 10 == 0));

        for (var i = 0; i < _roomModels.Count; i++)
        {
            var first = _roomModels[i];
            var last = _roomModels[_roomModels.Count - 1 - i];
            SwapPersons(first, last);
        }

        ResetAll();

        foreach (var vm in _roomVMs.Where(vm => vm.Model.number % 5 == 0))
        {
            Destroy(vm.gameObject);
        }

        _roomModels.RemoveAll(d => d.number % 5 == 0);
        _roomVMs.RemoveAll(vm => vm.Model.number % 5 == 0);

        var lastHaveAnimal = _roomModels.FindLast(d => d.haveAnimal);
        int lowestNumber = _roomModels.Min(m => m.number);
        var lowestRoom = _roomModels.Find(m => m.number == lowestNumber);
        SwapPersons(lastHaveAnimal, lowestRoom);
    }

    private void SwapPersons(RoomModel first, RoomModel second)
    {
        (first.person, second.person) = (second.person, first.person);
        (first.haveAnimal, second.haveAnimal) = (second.haveAnimal, first.haveAnimal);
    }
}

public class RoomModel
{
    public int number;
    public string person;
    public bool haveAnimal;
}