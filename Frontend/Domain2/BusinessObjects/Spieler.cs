using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace CoOrga.Survivors.Domain.BusinessObjects;

public class Charakter : BaseObject
{
    private string name;

    public Charakter(Session session)
            : base(session) { }

    public string Name
    {
        get => name;
        set => SetPropertyValue(nameof(Name), ref name, value);
    }
}