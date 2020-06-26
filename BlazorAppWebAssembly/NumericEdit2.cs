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
        [CascadingParameter]
        EditContext CascadedEditContext { get; set; }

        protected FieldIdentifier FieldIdentifier { get; set; }

        protected override void OnInitialized()
        {
            FieldIdentifier = FieldIdentifier.Create(ValueExpression);

            base.OnInitialized();
        }

        protected async override Task OnInternalValueChanged(TValue value)
        {
            await base.OnInternalValueChanged(value);

            if (CascadedEditContext != null)
            {
                CascadedEditContext.NotifyFieldChanged(FieldIdentifier);
            }
        }
    }
}
