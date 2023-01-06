using System.Reflection;
using CoOrga.Survivors.Domain.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.BaseImpl;

namespace CoOrga.Survivors.Domain.DatabaseUpdate;

// For more typical usage scenarios, be sure to check out https://docs.devexpress.com/eXpressAppFramework/DevExpress.ExpressApp.Updating.ModuleUpdater
public class Updater : ModuleUpdater
{
    public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) { }

    public override void UpdateDatabaseAfterUpdateSchema()
    {
        SeedCharaktere();
        ObjectSpace.CommitChanges();
        base.UpdateDatabaseAfterUpdateSchema();
    }

    private void SeedCharaktere()
    {
        SeedCharakter("Development", "Alex", 1);
        SeedCharakter("Development", "Tobias", 2);
        SeedCharakter("Development", "Christian", 3);
        SeedCharakter("Cheffs", "Björn", 1);
        SeedCharakter("Kabelfuzzis", "Jens", 1);
    }

    private void SeedCharakter(string abteilung, string name, int sortIndex)
    {
        var charakter = ObjectSpace.GetObjectsQuery<Charakter>()
                                   .FirstOrDefault(c => c.Name == name)
                        ?? ObjectSpace.CreateObject<Charakter>();

        if (!ObjectSpace.IsNewObject(charakter))
            return;

        charakter.Name      = name;
        charakter.Abteilung = abteilung;
        charakter.SortIndex = sortIndex;
        charakter.Bild      = new FileData(charakter.Session);
        LoadCharakterBildInto(charakter.Bild, name.ToLower());
    }

    private void LoadCharakterBildInto(FileData target, string bildName)
    {
        var resourceName = Assembly.GetExecutingAssembly()
                                   .GetManifestResourceNames()
                                   .First(n => n.Contains(bildName));
        var       imgName  = $"{bildName}{Path.GetExtension(resourceName)}";
        using var schdream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        target.LoadFromStream(imgName, schdream);
    }

    //DomainObject1 theObject = ObjectSpace.FirstOrDefault<DomainObject1>(u => u.Name == name);
    //if(theObject == null) {
    //    theObject = ObjectSpace.CreateObject<DomainObject1>();
    //    theObject.Name = name;
    //}
    //ObjectSpace.CommitChanges(); //Uncomment this line to persist created object(s).
    public override void UpdateDatabaseBeforeUpdateSchema() => base.UpdateDatabaseBeforeUpdateSchema();
    //if(CurrentDBVersion < new Version("1.1.0.0") && CurrentDBVersion > new Version("0.0.0.0")) {
    //    RenameColumn("DomainObject1Table", "OldColumnName", "NewColumnName");
    //}
}