using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Editors.Adapters;
using DevExpress.ExpressApp.Editors;
using Microsoft.AspNetCore.Components;

namespace CoOrga.Survivors.Frontend.Blazor.Server.Pages;

public class CoOrgaSurvivorsGameAdapter : IComponentAdapter, IComplexControl
{
    private XafApplication application;
    private RenderFragment component;

    public void Refresh() { }

    public void Setup(IObjectSpace objectSpace, XafApplication application) => this.application = application;

    public RenderFragment ComponentContent
    {
        get
        {
            if (component == null)
            {
                component = builder =>
                {
                    builder.OpenComponent<CoOrgaSurvivorsGame>(0);
                    builder.AddAttribute(1, nameof(CoOrgaSurvivorsGame.Title), application.Title);
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