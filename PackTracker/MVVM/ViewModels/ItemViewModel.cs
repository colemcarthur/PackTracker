using System;
using CommunityToolkit.Mvvm.ComponentModel;
using PackTracker.MVVM.Models;

namespace PackTracker.MVVM.ViewModels
{
	partial class ItemViewModel : ObservableObject
	{

		[ObservableProperty]
		Package package;

		public ItemViewModel(Package package)
		{
			Package = package;
		}
	}
}

