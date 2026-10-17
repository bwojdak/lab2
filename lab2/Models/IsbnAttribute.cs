using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace lab2.Models;

public class IsbnAttribute : ValidationAttribute
{
    private bool _requireControlSum;

    public IsbnAttribute(bool requireControlSum)
    {
        _requireControlSum = requireControlSum;
    }

    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            ErrorMessage = "Isbn nie może być pusty";
            return false;
        }
        if (value is not string isbn)
        {
            ErrorMessage = "Isbn musi być podany jako łańcuch";
            return false;
        }

        if (isbn.Length != 10 || isbn.Length != 13)
        {
            ErrorMessage = "Isbn musi mieć długość 10 lub 13 cyfr";
            return false;
        }

        return true;
    }
}