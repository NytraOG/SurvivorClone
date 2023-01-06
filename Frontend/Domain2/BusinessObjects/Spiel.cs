using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace CoOrga.Survivors.Domain.BusinessObjects;

public class Spiel : BaseObject
{
    private Charakter ausgewählterChar;

    public Spiel(Session session)
            : base(session) { }

    public Charakter AusgewählterChar
    {
        get => ausgewählterChar;
        set => SetPropertyValue(nameof(AusgewählterChar), ref ausgewählterChar, value);
    }
}