using PCLStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace BI_FileSystem
{
    public partial class SaveLoadXaml : ContentPage
    {
        IFolder rootFolder = FileSystem.Current.LocalStorage;

        public SaveLoadXaml()
        {
            InitializeComponent();
        }

        private async void SaveClicked(object sender, EventArgs e)
        {
            IFile file = await rootFolder.CreateFileAsync("name.txt", CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(entry.Text);
        }

        private async void LoadClicked(object sender, EventArgs e)
        {
            ExistenceCheckResult res = await rootFolder.CheckExistsAsync("name.txt");
            if (res == ExistenceCheckResult.FileExists)
            {
                IFile file = await rootFolder.GetFileAsync("name.txt");
                string name = await file.ReadAllTextAsync();
                loadedLabel.Text = name;
            }
            else
            {
                await DisplayAlert("Error", "File is not found", "OK");
            }
        }

        private async void DeleteClicked(object sender, EventArgs e)
        {
            ExistenceCheckResult res = await rootFolder.CheckExistsAsync("name.txt");
            if (res == ExistenceCheckResult.FileExists)
            {
                IFile file = await rootFolder.GetFileAsync("name.txt");
                await file.DeleteAsync();
                loadedLabel.Text = "";
            }
        }

    }
}
