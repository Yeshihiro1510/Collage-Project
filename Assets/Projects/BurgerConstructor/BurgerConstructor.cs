using UnityEngine;

public class BurgerConstructor : MonoBehaviour
{
    private void Start()
    {
        ThoseMethod(out var myBurger, "cutlet", "cutlet", "special sauce", "sesame bun");
        Debug.Log($"Your burger is {myBurger}");
    }

    private void ThoseMethod(out string burger, params string[] ingredients)
    {
        int cucumbers = 0;
        int cheese = 0;
        int ketchup = 0;
        int cutlets = 0;

        int specialSauce = 0;
        int sesameBun = 0;

        foreach (var i in ingredients)
        {
            if (i == "cucumber") cucumbers++;
            if (i == "cheese") cheese++;
            if (i == "ketchup") ketchup++;
            if (i == "cutlet") cutlets++;
            if (i == "special sauce") specialSauce++;
            if (i == "sesame bun") sesameBun++;
        }

        if (cucumbers == 1 && cheese == 1 && ketchup == 1 && cutlets == 1 && specialSauce == 0 && sesameBun == 0) burger = "cheeseburger";
        else if (specialSauce == 1 && sesameBun == 1 && cutlets == 2 && cucumbers == 0 && cheese == 0 && ketchup == 0 ) burger = "big mac";
        else burger = "random shi";
    }
}