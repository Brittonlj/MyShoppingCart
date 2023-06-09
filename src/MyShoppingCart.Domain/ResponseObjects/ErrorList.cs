﻿using System.Text;
using System.Text.Json;

namespace MyShoppingCart.Domain.ResponseObjects;

public sealed class ErrorList : List<Error>
{
    public ErrorList()
    {
    }

    public ErrorList(List<Error> errors)
    {
        AddRange(errors);
    }

    public ErrorList(Exception ex)
    {
        if (ex is null)
        {
            return;
        }

        Add(new Error(Error.EXCEPTION_CODE, ex.Message));

        while (ex.InnerException is not null)
        {
            ex = ex.InnerException;
            Add(new Error(Error.EXCEPTION_CODE, ex.Message));
        }
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach(var error in this)
        {
            sb.Append(error.ToString());
        }
        return sb.ToString();
    }
}
