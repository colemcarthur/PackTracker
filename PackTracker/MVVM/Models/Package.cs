using System;
using PackTracker.Abstractions;
using SQLiteNetExtensions.Attributes;

namespace PackTracker.MVVM.Models
{
	public class Package : TableData
	{
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.CascadeInsert |
                                       CascadeOperation.CascadeRead |
                                       CascadeOperation.CascadeDelete)]
        public List<Item> Items { get; set; }

        [ForeignKey(typeof(Item))]
        public int ItemId { get; set; }

        public Package()
		{

		}
	}
}

