using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;

namespace CoOrga.Survivors.Frontend.Module.BusinessObjects;

[MapInheritance(MapInheritanceType.ParentTable)]
[DefaultProperty(nameof(UserName))]
public class ApplicationUser : PermissionPolicyUser, IObjectSpaceLink, ISecurityUserWithLoginInfo
{
    public ApplicationUser(Session session) : base(session) { }

    [Browsable(false)]
    [Aggregated]
    [Association("User-LoginInfo")]
    public XPCollection<ApplicationUserLoginInfo> LoginInfo => GetCollection<ApplicationUserLoginInfo>();

    IObjectSpace IObjectSpaceLink.ObjectSpace { get; set; }

    IEnumerable<ISecurityUserLoginInfo> IOAuthSecurityUser.UserLogins => LoginInfo.OfType<ISecurityUserLoginInfo>();

    ISecurityUserLoginInfo ISecurityUserWithLoginInfo.CreateUserLoginInfo(string loginProviderName, string providerUserKey)
    {
        var result = ((IObjectSpaceLink)this).ObjectSpace.CreateObject<ApplicationUserLoginInfo>();
        result.LoginProviderName = loginProviderName;
        result.ProviderUserKey   = providerUserKey;
        result.User              = this;

        return result;
    }
}