using System;
using System.Text;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Random = UnityEngine.Random;

public class Cryptor : MonoBehaviour
{
    private const string CRYPTED_TEXT =
        "Cats are j*st h*ge kittens and n$othing else. These cr$azy animals ar$e preferred by a h*ge part of those who keep animals at home. People alw$ays thin$k that cats are lazy animals that lik$e t$o spend their ent$ire life sitting by the wind$ow, all day long.>We have all heard that ca$ts live an average of 8 years, b*t that is no$t the entire tr*th. This n*mber is only ave$rage, cats live 12 ye$ars and more on a$verage. Cats are s$ocial a$nimals, they live with other cats and th$ey als$o$ live with h*mans and other anim$als as well.>Cats live *p to 40 ho*rs a week, so they can not even be c$onsidered as lazy. They play with other animals and they spend tim$e sleeping or lo*nging aro*nd. They enjoy long walks a$nd we all$ know cats will.>It is easy to bring a kitten home, b*t it is not so $easy to raise and raise a worthy member of mo$dern society and make a decent cat o*t of a $baby. Before yo* bring a new cat home, it is im$portant to know how to take care of them and train them properly.>They req*ire the p$roper space, shelter and toys. Cats can be kept in a normal room with doors and wind$ows open, b*t don’t expo$se them $to the we$ather. If yo* want them to play o*tside and go on long walks, keep them inside the ho*se. The ho*se m*st be clean, a$nd food and water m*st alwa$ys be availa$ble. If yo* want yo*r cat to like yo*, yo* m*st be nice $to him or her.>A $cat is a *niq*e creat*re, she has different needs and a different disposition. If yo* choose$ the wrong one to k$eep, yo* mig$ht regret it. Cats are an im$portant part of o*r world, an$d we have to give them the$ right ed*ca$tion.zzz\n";

    public TMP_Text textField;
    public Button decryptButton;
    public Button correctButton;
    public Button peelButton;
    public Button encryptButton;
    public Button resetButton;

    private string CurrentText
    {
        get => _currentText;
        set
        {
            _currentText = value;
            if (textField != null) textField.text = _currentText;
        }
    }

    private string _currentText;

    private void OnEnable()
    {
        decryptButton?.onClick.AddListener(Decrypt);
        correctButton?.onClick.AddListener(Correct);
        peelButton?.onClick.AddListener(Peel);
        encryptButton?.onClick.AddListener(Encrypt);
        resetButton?.onClick.AddListener(ResetText);
    }

    private void OnDisable()
    {
        decryptButton?.onClick.RemoveListener(Decrypt);
        correctButton?.onClick.RemoveListener(Correct);
        peelButton?.onClick.RemoveListener(Peel);
        encryptButton?.onClick.RemoveListener(Encrypt);
        resetButton?.onClick.RemoveListener(ResetText);
    }

    private void Start()
    {
        ResetText();
    }

    private void ResetText() => CurrentText = CRYPTED_TEXT;
    private void Decrypt() => CurrentText = textField.text.Replace('*', 'u');
    private void Correct() => CurrentText = textField.text.Replace('>', '\n');

    private void Peel()
    {
        for (var i = 0; i < CurrentText.Length; i++)
            if (CurrentText[i] == '$')
                CurrentText = CurrentText.Remove(i, 1);

        var lastPointIndex = CurrentText.LastIndexOf('.');
        CurrentText = CurrentText.Remove(lastPointIndex + 1, CurrentText.Length - lastPointIndex - 1);
    }

    private void Encrypt()
    {
        var stringBuilder = new StringBuilder(CurrentText);

        for (var i = 0; i < stringBuilder.Length; i++)
            stringBuilder[i] = CurrentText[CurrentText.Length - i - 1];

        for (var i = 0; i < stringBuilder.Length; i++)
        {
            if (char.IsLower(stringBuilder[i]))
                stringBuilder[i] = char.ToUpper(stringBuilder[i]);
            else if (char.IsUpper(stringBuilder[i]))
                stringBuilder[i] = char.ToLower(stringBuilder[i]);
        }

        var random = Random.Range(1, 100);
        _ = random switch
        {
            < 25 => stringBuilder.Replace('I', '1'),
            > 30 and < 75 => stringBuilder.Replace('A', 'b'),
            > 75 => stringBuilder.Replace('B', 'c'),
            _ => throw new ArgumentOutOfRangeException()
        };

        textField.text = stringBuilder.ToString();
    }
}