using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Bootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorAppWebAssembly
{
    public class NumericEdit2<TValue> : NumericEdit<TValue>
    {
        /// <summary>
        /// Gets the associated <see cref="Forms.EditContext"/>.
        /// </summary>
        protected EditContext EditContext { get; set; }

        [CascadingParameter]
        EditContext CascadedEditContext { get; set; }

        /// <summary>
        /// Gets the <see cref="FieldIdentifier"/> for the bound value.
        /// </summary>
        protected FieldIdentifier FieldIdentifier { get; set; }

        private Type _nullableUnderlyingType;

        /// <inheritdoc />
        public override Task SetParametersAsync(ParameterView parameters)
        {
            parameters.SetParameterProperties(this);

            if (EditContext == null)
            {
                // This is the first run
                // Could put this logic in OnInit, but its nice to avoid forcing people who override OnInit to call base.OnInit()

                if (CascadedEditContext == null)
                {
                    throw new InvalidOperationException($"{GetType()} requires a cascading parameter " +
                        $"of type {nameof(EditContext)}. For example, you can use {GetType().FullName} inside " +
                        $"an {nameof(EditForm)}.");
                }

                if (ValueExpression == null)
                {
                    throw new InvalidOperationException($"{GetType()} requires a value for the 'ValueExpression' " +
                        $"parameter. Normally this is provided automatically when using 'bind-Value'.");
                }

                EditContext = CascadedEditContext;
                FieldIdentifier = FieldIdentifier.Create(ValueExpression);
                _nullableUnderlyingType = Nullable.GetUnderlyingType(typeof(TValue));

                EditContext.OnFieldChanged += EditContext_OnValidationStateChanged;
                //EventCallback<TValue> x = new EventCallback<TValue>();
                //this.ValueChanged = x;
            }
            else if (CascadedEditContext != EditContext)
            {
                // Not the first run

                // We don't support changing EditContext because it's messy to be clearing up state and event
                // handlers for the previous one, and there's no strong use case. If a strong use case
                // emerges, we can consider changing this.
                throw new InvalidOperationException($"{GetType()} does not support changing the " +
                    $"{nameof(Microsoft.AspNetCore.Components.Forms.EditContext)} dynamically.");
            }

            //SetAdditionalAttributesIfValidationFailed();

            // For derived components, retain the usual lifecycle with OnInit/OnParametersSet/etc.
            return base.SetParametersAsync(ParameterView.Empty);
        }

        private void EditContext_OnValidationStateChanged(object sender, FieldChangedEventArgs e)
        {
            SetAdditionalAttributesIfValidationFailed();

            StateHasChanged();
        }

        private void SetAdditionalAttributesIfValidationFailed()
        {
            bool modified = EditContext.IsModified(FieldIdentifier);
            Console.WriteLine($"{FieldIdentifier.FieldName} modified = {modified}");

            bool isValid = !EditContext.GetValidationMessages(FieldIdentifier).Any();
            Console.WriteLine($"{FieldIdentifier.FieldName} valid = {isValid}");

            if (EditContext.GetValidationMessages(FieldIdentifier).Any())
            {
                var c = new BootstrapClassProvider();

                string cl = "";
                if (!modified)
                {
                    cl =c.ToValidationStatus(ValidationStatus.None);
                }

                cl = c.ToValidationStatus(isValid ? ValidationStatus.Success : ValidationStatus.Error);

                // To make the `Input` components accessible by default
                // we will automatically render the `aria-invalid` attribute when the validation fails
                //additionalAttributes["class"] = true;

                Class = cl;
            }
        }

        /// <summary>
        /// Gets or sets a collection of additional attributes that will be applied to the created element.
        /// </summary>
       // [Parameter(CaptureUnmatchedValues = true)] public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }
    }
}
