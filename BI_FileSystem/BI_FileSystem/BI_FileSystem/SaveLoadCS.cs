using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using PCLStorage;
using Xamarin.Forms;

namespace BI_FileSystem
{
    public class SaveLoadCS : ContentPage
    {
        IFolder rootFolder = FileSystem.Current.LocalStorage;
        Entry entry;
        Label loadedLabel;

        public SaveLoadCS()
        {
            entry = new Entry
            {
                Placeholder = "Please input your name"
            };
            var saveButton = new Button
            {
                Text = "Save",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            saveButton.Clicked += SaveButton_Clicked;
            var loadButton = new Button
            {
                Text = "Load",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            loadButton.Clicked += LoadButton_Clicked;
            var deleteButton = new Button
            {
                Text = "Delete",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            deleteButton.Clicked += DeleteButton_Clicked;
            loadedLabel = new Label
            {
                Text = "Saved text will be displayed here when loaded."
            };

            Content = new StackLayout
            {
                Children = {
                    entry,
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            saveButton,
                            loadButton,
                            deleteButton
                        }
                    },
                    loadedLabel
                }
            };
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            IFile file = await rootFolder.CreateFileAsync("name.txt", CreationCollisionOption.ReplaceExisting);
            await file.WriteAllTextAsync(entry.Text);
        }

        private async void LoadButton_Clicked(object sender, EventArgs e)
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

        private async void DeleteButton_Clicked(object sender, EventArgs e)
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
