using Microsoft.Win32;

namespace MikroTikSetupWizard.Desktop.Dialogs;

public sealed class SaveFileDialogService : ISaveFileDialogService
{
    public string? GetSaveFilePath(string defaultFileName)
    {
        var dialog = new SaveFileDialog
        {
            AddExtension = true,
            DefaultExt = ".rsc",
            FileName = defaultFileName,
            Filter = "RouterOS script (*.rsc)|*.rsc|Все файлы (*.*)|*.*",
            OverwritePrompt = true,
            Title = "Сохранить .rsc файл"
        };

        return dialog.ShowDialog() == true ? dialog.FileName : null;
    }
}
