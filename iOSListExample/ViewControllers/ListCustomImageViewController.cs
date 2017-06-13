using System;
using System.Collections.Generic;

using Foundation;
using UIKit;
namespace iOSListExample
{
	public partial class ListCustomImageViewController : UIViewController
	{
		public ListCustomImageViewController()
		{
		}

		List<ListItem> listItems;

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			UITableView table = new UITableView(View.Bounds);

			listItems = new List<ListItem> {
				new ListItem {Title = "First", Description="1st item"},
				new ListItem {Title = "Second", Description="2nd item"},
				new ListItem {Title = "Third", Description="3rd item"}
			};

			table.RowHeight = UITableView.AutomaticDimension;
			table.EstimatedRowHeight = new nfloat(15.0);

			table.Source = new ListSourceFromCustomImageCell(listItems);

			Add(table);
		}


	}
}

