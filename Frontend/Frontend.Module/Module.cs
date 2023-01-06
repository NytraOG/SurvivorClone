using CoOrga.Survivors.Domain;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.AuditTrail;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Objects;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Validation;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl;
using Updater = CoOrga.Survivors.Frontend.Module.DatabaseUpdate.Updater;

namespace CoOrga.Survivors.Frontend.Module;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
public sealed class FrontendModule : ModuleBase
{
    public FrontendModule()
    {
        AdditionalExportedTypes.Add(typeof(ModelDifference));
        AdditionalExportedTypes.Add(typeof(ModelDifferenceAspect));
        AdditionalExportedTypes.Add(typeof(BaseObject));
        AdditionalExportedTypes.Add(typeof(AuditDataItemPersistent));
        AdditionalExportedTypes.Add(typeof(AuditedObjectWeakReference));
        RequiredModuleTypes.Add(typeof(SystemModule));
        RequiredModuleTypes.Add(typeof(SecurityModule));
        RequiredModuleTypes.Add(typeof(AuditTrailModule));
        RequiredModuleTypes.Add(typeof(BusinessClassLibraryCustomizationModule));
        RequiredModuleTypes.Add(typeof(ConditionalAppearanceModule));
        RequiredModuleTypes.Add(typeof(ValidationModule));
        RequiredModuleTypes.Add(typeof(DomainModule));
    }

    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
    {
        ModuleUpdater updater = new Updater(objectSpace, versionFromDB);

        return new[] { updater };
    }

    public override void Setup(XafApplication application) => base.Setup(application);

    // Manage various aspects of the application UI and behavior at the module level.
    public override void CustomizeTypesInfo(ITypesInfo typesInfo)
    {
        base.CustomizeTypesInfo(typesInfo);
        CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);
    }
}