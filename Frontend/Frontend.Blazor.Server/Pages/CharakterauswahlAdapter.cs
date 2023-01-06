using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using Microsoft.AspNetCore.Components;

namespace CoOrga.Survivors.Frontend.Blazor.Server.Pages;

public class CharakterauswahlAdapter : IComponentAdapter, IComplexControl
{
    private XafApplication application;
    private RenderFragment component;
    private IObjectSpace space;

    public void Refresh() { }

    public void Setup(IObjectSpace objectSpace, XafApplication application)
    {
        this.application = application;
        this.space  = objectSpace;
    }

    public RenderFragment ComponentContent
    {
        get
        {
            if (component == null)
            {
                component = builder =>
                {
                    builder.OpenComponent<Charakterauswahl>(0);
                    builder.AddAttribute(1, nameof(Charakterauswahl.ObjectSpace), space);
                    builder.CloseComponent();
                };
            }

            return component;
        }
    }

    public object GetValue() => null;

    public void SetValue(object value) { }

    public event EventHandler ValueChanged;
}