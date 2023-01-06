using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp.Xpo;
using Updater = CoOrga.Survivors.Domain.DatabaseUpdate.Updater;

namespace CoOrga.Survivors.Domain;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.ModuleBase.
public sealed class DomainModule : ModuleBase
{
    public DomainModule() =>
            RequiredModuleTypes.Add(typeof(SystemModule));

    public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB)
    {
        ModuleUpdater updater = new Updater(objectSpace, versionFromDB);

        return new[] { updater };
    }

    // Manage various aspects of the application UI and behavior at the module level.
    public override void CustomizeTypesInfo(ITypesInfo typesInfo)
    {
        base.CustomizeTypesInfo(typesInfo);
        CalculatedPersistentAliasHelper.CustomizeTypesInfo(typesInfo);
    }
}