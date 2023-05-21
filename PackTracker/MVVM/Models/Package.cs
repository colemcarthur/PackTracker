using Microsoft.Maui.Controls;
using System;
using PackTracker.Abstractions;
using SQLiteNetExtensions.Attributes;
using System.Windows.Input;
using SQLite;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace PackTracker.MVVM.Models
{
	public class Package : TableData
	{
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Item> Items { get; set; }

        [ForeignKey(typeof(Item))]
        public int ItemId { get; set; }

        [Ignore]
        public ImageSource QRImage
        {
            get
            {
               
                Stream sr = App.BarcodeService.ConvertImageStream(this.Id.ToString() + " - " + this.Name, this.Name, 25, 25, false);
                return ImageSource.FromStream(() => sr);
       
            }
        }

        [Ignore]
        public Int32 ItemCount
        {
            get
            {
                return App.PackagesRepo.ItemCount(this.Id);
            }
        }
        public Package()
		{

		}
    }
}

