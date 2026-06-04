namespace MikroTikSetupWizard.Desktop.Dialogs;

public interface ISaveFileDialogService
{
    string? GetSaveFilePath(string defaultFileName);
}
