using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace CoOrga.Survivors.Domain.BusinessObjects;

public class Charakter : BaseObject
{
    private string   abteilung;
    private FileData bild;
    private string   name;

    private int sortIndex;

    public Charakter(Session session)
            : base(session) { }

    public string Name
    {
        get => name;
        set => SetPropertyValue(nameof(Name), ref name, value);
    }

    public FileData Bild
    {
        get => bild;
        set => SetPropertyValue(nameof(Bild), ref bild, value);
    }

    public int SortIndex
    {
        get => sortIndex;
        set => SetPropertyValue(nameof(SortIndex), ref sortIndex, value);
    }

    public string Abteilung
    {
        get => abteilung;
        set => SetPropertyValue(nameof(Abteilung), ref abteilung, value);
    }

    [NonPersistent]
    public string ImgUrl => $"data:image/{Path.GetExtension(Bild.FileName).Substring(1)};base64,{Convert.ToBase64String(Bild.Content)}";
}