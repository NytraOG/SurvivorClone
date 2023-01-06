using System.Diagnostics;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor;

namespace CoOrga.Survivors.Frontend.Blazor.Server;

public class The_GUIBlazorApplication : BlazorApplication
{
    public The_GUIBlazorApplication()
    {
        ApplicationName         =  "The GUI";
        CheckCompatibilityType  =  CheckCompatibilityType.DatabaseSchema;
        DatabaseVersionMismatch += The_GUIBlazorApplication_DatabaseVersionMismatch;
    }

    protected override void OnSetupStarted()
    {
        base.OnSetupStarted();
#if DEBUG
        if (Debugger.IsAttached && CheckCompatibilityType == CheckCompatibilityType.DatabaseSchema)
            DatabaseUpdateMode = DatabaseUpdateMode.UpdateDatabaseAlways;
#endif
    }

    private void The_GUIBlazorApplication_DatabaseVersionMismatch(object sender, DatabaseVersionMismatchEventArgs e)
    {
#if EASYTEST
        e.Updater.Update();
        e.Handled = true;
#else
        if (Debugger.IsAttached)
        {
            e.Updater.Update();
            e.Handled = true;
        }
        else
        {
            var message = "The application cannot connect to the specified database, " +
                          "because the database doesn't exist, its version is older " +
                          "than that of the application or its schema does not match " +
                          "the ORM data model structure. To avoid this error, use one " +
                          "of the solutions from the https://www.devexpress.com/kb=T367835 KB Article.";

            if (e.CompatibilityError != null && e.CompatibilityError.Exception != null)
                message += "\r\n\r\nInner exception: " + e.CompatibilityError.Exception.Message;

            throw new InvalidOperationException(message);
        }
#endif
    }
}