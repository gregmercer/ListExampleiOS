using System;
using System.Collections.Generic;

using Foundation;
using UIKit;

namespace iOSListExample
{
	public class ListSourceFromCustomImageCell : UITableViewSource
	{

		protected List<ListItem> listItems;
		protected string CellId = "TableImageCell";

		public ListSourceFromCustomImageCell(List<ListItem> items)
		{
			listItems = items;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			new UIAlertView("Row Selected", listItems[indexPath.Row].Title, null, "OK", null).Show();
			tableView.DeselectRow(indexPath, true);
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return listItems.Count;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(CellId) as CustomImageCell;

			if (cell == null)
			{
				cell = new CustomImageCell((NSString)CellId);
			}

			cell.UpdateCell(
				listItems[indexPath.Row].Title
			);

			return cell;
		}

	}

}
