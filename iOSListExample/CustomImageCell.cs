using System;
using System.Drawing;
using CoreGraphics;
using Foundation;
using UIKit;

namespace iOSListExample
{
	public class CustomImageCell : UITableViewCell
	{
		CollectionViewSource source;
		UICollectionViewFlowLayout layout;
		UICollectionView collectionView;

		UILabel titleLabel;

		public CustomImageCell(IntPtr p) : base(p) // for the new cell reuse
		{
			SelectionStyle = UITableViewCellSelectionStyle.Blue;
			ContentView.BackgroundColor = UIColor.FromRGB(27, 16, 117);

			titleLabel = new UILabel()
			{
				Font = UIFont.FromName("Helvetica-Bold", 25f),
				TextColor = UIColor.White,
				BackgroundColor = UIColor.Clear
			};

			ContentView.AddSubviews(new UIView[] { titleLabel });
		}

		public CustomImageCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Blue;
			ContentView.BackgroundColor = UIColor.FromRGB(27, 16, 117);

			// Create the Title Label

			titleLabel = new UILabel()
			{
				Font = UIFont.FromName("Helvetica-Bold", 25f),
				TextColor = UIColor.White,
				BackgroundColor = UIColor.Clear
			};

			// Create the Collection View layout

			layout = new UICollectionViewFlowLayout
			{
				SectionInset = new UIEdgeInsets(5, 5, 5, 5),
				MinimumInteritemSpacing = 5,
				MinimumLineSpacing = 5,
			};

			// Create the Collection Ciew

			collectionView = new UICollectionView(ContentView.Bounds, layout);

            // Create the Collection View Source

            source = new CollectionViewSource();
			collectionView.Source = source;

            collectionView.Delegate = new CollectionViewFlowDelegate(source.Data);

			// Register the TextCell class for the Cells in the Collection View

            collectionView.RegisterClassForCell(typeof(ImageCell), ImageCell.CellId);

			// Add the Subviews to the TableViewCell

			ContentView.AddSubviews(new UIView[] {
				titleLabel,
				collectionView
			});

			// Set the frames and constraints for the Title Label and Collection View

			titleLabel.Frame = new RectangleF(5, 4, (float)ContentView.Bounds.Width - 63, 25);
			collectionView.Frame = new RectangleF(0, 30, 300, 200);

			ContentView.AddConstraint(
			  NSLayoutConstraint.Create(
				titleLabel, NSLayoutAttribute.Top,
				NSLayoutRelation.Equal,
				ContentView, NSLayoutAttribute.TopMargin,
				1, 3
			  )
			);

			ContentView.AddConstraint(
			  NSLayoutConstraint.Create(
				titleLabel, NSLayoutAttribute.Bottom,
				NSLayoutRelation.Equal,
				collectionView, NSLayoutAttribute.Top,
				1, 3
			  )
			);

			ContentView.AddConstraint(
			  NSLayoutConstraint.Create(
				collectionView, NSLayoutAttribute.Bottom,
				NSLayoutRelation.Equal,
				ContentView, NSLayoutAttribute.BottomMargin,
				1, 3
			  )
			);

		}

		public void UpdateCell(string title)
		{
			titleLabel.Text = title;
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();
		}

		// CollectionViewFlowDelegate - the Collection View's Flow Delegate

		class CollectionViewFlowDelegate : UICollectionViewDelegateFlowLayout
		{
			string[] data = null;

            public CollectionViewFlowDelegate(string[] inData)
            {
                data = inData;
            }

            public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
			{
				UIImage image = UIImage.FromBundle(data[indexPath.Row]);

				var imageWidth  = image.CurrentScale * image.Size.Width;
				var imageHeight = image.CurrentScale * image.Size.Height;

				return new CGSize(imageWidth, imageHeight);
			}
		}

		// CollectionViewSource - the Collection View's Source

		class CollectionViewSource : UICollectionViewSource
		{
			string[] data = { "Group_Image_AllElse", "Group_Image_StudentOrg" };

            public string[] Data
            {
                get
                {
                    return data;
                }
            }

			public override nint GetItemsCount(UICollectionView collectionView, nint section)
			{
				return data.Length;
			}

			public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
			{
				ImageCell imageCell = (ImageCell)collectionView.DequeueReusableCell(ImageCell.CellId, indexPath);

                UIImage image = UIImage.FromBundle(data[indexPath.Row]);

                var imageWidth  = image.CurrentScale * image.Size.Width;
                var imageHeight = image.CurrentScale * image.Size.Height;

				imageCell.imageView.Frame = new RectangleF(
                    (float)imageCell.imageView.Frame.Left, (float)imageCell.imageView.Frame.Top,
                    (float)imageWidth, (float)imageHeight
				);

                imageCell.imageView.Image = image;

				return imageCell;
			}

			public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
			{
				Console.WriteLine("Row {0} selected", indexPath.Row);
			}

			public override bool ShouldSelectItem(UICollectionView collectionView, NSIndexPath indexPath)
			{
				return true;
			}
		}

		// ImageCell - the Collection View Cells

		class ImageCell : UICollectionViewCell
		{
			public UIImageView imageView;

			public static readonly NSString CellId = new NSString("ImageCell");

			[Export("initWithFrame:")]
			ImageCell(RectangleF frame) : base(frame)
			{
                imageView = new UIImageView();
				ContentView.AddSubview(imageView);
			}
		}

	}
}
