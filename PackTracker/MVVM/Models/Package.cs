using Microsoft.Maui.Controls;
using System;
using PackTracker.Abstractions;
using SQLiteNetExtensions.Attributes;
using System.Windows.Input;

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

        public Package()
		{

		}
    }
}

