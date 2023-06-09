﻿using System;
using SQLiteNetExtensions.Attributes;
using PackTracker.Abstractions;
using SQLite;

namespace PackTracker.MVVM.Models
{
	public class Item : TableData
	{

		[ForeignKey(typeof(Package))]
		public int PackageID { get; set; }

		[MaxLength(100)]
		public string Description { get; set; }

		public byte[] Image { get; set; }

		public DateTime CreationDate { get; set; }

		public Double PurchasePrice { get; set; }

		[MaxLength(50)]
		public string PurchaseLocation { get; set; }

        public Item()
		{
		}
	}
}

