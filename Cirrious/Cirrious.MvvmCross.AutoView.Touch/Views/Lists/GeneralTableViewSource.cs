using System.Collections.Generic;
using Cirrious.MvvmCross.AutoView.Touch.Interfaces.Lists;
using Cirrious.MvvmCross.Binding.Touch.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Cirrious.MvvmCross.AutoView.Touch.Views.Lists
{
    public class GeneralTableViewSource : MvxBindableTableViewSource
    {
        private readonly IMvxLayoutListItemViewFactory _defaultFactory;
        private readonly Dictionary<string, IMvxLayoutListItemViewFactory> _factories;

        public GeneralTableViewSource(UITableView tableView, IMvxLayoutListItemViewFactory defaultFactory, Dictionary<string, IMvxLayoutListItemViewFactory> factories)
            : base(tableView)
        {
            _defaultFactory = defaultFactory;
            _factories = factories;
        }

        private const string BaseCellIdentifier = @"GeneralTabelViewSource_";

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            var cellId = GetCellIdentifier(item);
            var reuse = tableView.DequeueReusableCell(cellId);

            if (reuse != null)
                return reuse;

            IMvxLayoutListItemViewFactory factory;
            if (_factories != null || !_factories.TryGetValue(item.GetType().Name, out factory))
            {
                factory = _defaultFactory;
            }

            var cell = factory.BuildView(indexPath, item, cellId);
            return cell;
        }

        private static string GetCellIdentifier(object item)
        {
            return BaseCellIdentifier + item.ToString();
        }
    }
}