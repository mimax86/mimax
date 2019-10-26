using System;
using Akka.Persistence;

namespace Mimax.Actor
{
    public class ShoppingCartActor : PersistentActor
    {
        public override string PersistenceId =>
            throw new
                NotImplementedException();

        protected override bool ReceiveRecover(object message)
        {
            throw new NotImplementedException();
        }

        protected override bool ReceiveCommand(object message)
        {
            if (message is AddItem msg)
            {
                var itemAddedEvent = new ItemAdded(msg.ItemId, msg.ItemCount);
                Persist(itemAddedEvent, HandleEvent);
                return true;
            }
            if (message is Shutdown)
            {
                Context.Stop(Self);
                return true;
            }
            return false;
        }

        private void HandleEvent(object @event)
        {
            if (@event is ItemAdded)
            {
                //var evt = (ItemAdded) @event;
                //if (_items.ContainsKey(evt.ItemId))
                //{
                //    var currentCount = _items[evt.ItemId];
                //    var newCount = currentCount + evt.ItemCount;
                //    _items[evt.ItemId] = newCount;
                //}
                //else
                //{
                //    _items[evt.ItemId] = evt.ItemCount;
                //}
            }
        }
    }

    class AddItem
    {
        public string ItemId { get; }
        public int ItemCount { get; }
        public AddItem(string itemId, int itemCount)
        {
            ItemId = itemId;
            ItemCount = itemCount;
        }
    }

    class ItemAdded
    {
        public string ItemId { get; }
        public int ItemCount { get; }
        public ItemAdded(string itemId, int itemCount)
        {
            ItemId = itemId;
            ItemCount = itemCount;
        }
    }

    class Shutdown { }
}