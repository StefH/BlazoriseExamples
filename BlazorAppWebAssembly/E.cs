﻿using System;
using System.Linq;
using System.Linq.Expressions;
using Blazorise;
using Blazorise.Bootstrap;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorAppWebAssembly
{
    public static class E
    {
        public static string BlazoriseFieldCssClass(this EditContext editContext, in Expression<Func<object>> accessor)
        {
            //editContext.OnValidationStateChanged += EditContext_OnValidationStateChanged;

            var fi = FieldIdentifier.Create(accessor);
            bool modified = editContext.IsModified(accessor);
            Console.WriteLine($"{fi.FieldName} modified = {modified}");

            Console.WriteLine($"{fi.FieldName} messages = {string.Join(',',editContext.GetValidationMessages(accessor))}");

            bool isValid = !editContext.GetValidationMessages(accessor).Any();
            Console.WriteLine($"{fi.FieldName} valid = {isValid}");

            var c = new BootstrapClassProvider();

            if (!modified)
            {
               return c.ToValidationStatus(ValidationStatus.None);
            }

            return c.ToValidationStatus(isValid ? ValidationStatus.Success : ValidationStatus.Error);

            //if (modified)
            //{
            //    return isValid ? c.ToValidationStatus(ValidationStatus.Success) : c.ToValidationStatus(ValidationStatus.Error);
            //}
            //else
            //{
            //    return isValid ? c.ToValidationStatus(ValidationStatus.None) : c.ToValidationStatus(ValidationStatus.Error);
            //}
        }

        //private static void EditContext_OnValidationStateChanged(object sender, ValidationStateChangedEventArgs e)
        //{
        //    var editContext = (EditContext) sender;
        //    bool isValid = !editContext.GetValidationMessages(accessor).Any();
        //    Console.WriteLine($"{fi.FieldName} valid = {isValid}");

        //    var c = new BootstrapClassProvider();

        //    if (!modified)
        //    {
        //        return c.ToValidationStatus(ValidationStatus.None);
        //    }

        //    return c.ToValidationStatus(isValid ? ValidationStatus.Success : ValidationStatus.Error);
        //}
    }
}
