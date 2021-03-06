﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Taspinn.Models;
using Taspinn.Views;
using Taspinn.Services;
using System.Linq;

namespace Taspinn.ViewModels
{
    public class DisposedItemsViewModel : BaseViewModel
    {
        public IDisposalDataStore<Item> DataStore { get; set; }//=> DependencyService.Get<IShoppingDataStore<Item>>() ?? new MockShoppingDataStore();

        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public DisposedItemsViewModel()
        {
            Title = "Disposal List";
            DataStore = new MockDisposedDataStore();
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            //{
            //    var newItem = item as Item;
            //    Items.Add(newItem);
            //    await DataStore.AddItemAsync(newItem);
            //});
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync("nandre04", true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var result = await DataStore.DeleteItemAsync(id);

            if (result)
            {
                var item = Items.FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    Items.Remove(item);
                }
            }

            return result;
        }

        public async Task<bool> MoveItemToShoppingListAsync(int id)
        {
            //throw new NotImplementedException();
            var result = await DataStore.MoveItemToShoppingListAsync(id);

            if (result)
            {
                var item = Items.FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    Items.Remove(item);
                }
            }

            return result;
        }
    }
}
