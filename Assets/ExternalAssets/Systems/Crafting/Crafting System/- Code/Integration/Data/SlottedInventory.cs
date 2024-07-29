﻿using System;
using System.Collections.Generic;
using System.Linq;
using Polyperfect.Crafting.Framework;

namespace Polyperfect.Crafting.Integration
{
    public interface ISlottedInventory<T> : IInsert<IEnumerable<ItemStack>>, IInsert<ItemStack>,IExtract<IEnumerable<ItemStack>>,IChangeable where T : ISlot<Quantity, ItemStack>
    {
        IEnumerable<T> Slots { get; }
    }

    public class SlottedInventory<SLOT> : ISlottedInventory<SLOT> where SLOT : ISlot<Quantity, ItemStack>
    {
        public IEnumerable<SLOT> Slots => SlotList;
        public IReadOnlyList<SLOT> SlotList =>slotList;
        readonly List<SLOT> slotList = new List<SLOT>();
        public event Action<ItemStack> OnItemInsertedViaInventory;
        public event PolyChangeEvent Changed;
        public IEnumerable<ItemStack> RemainderIfInserted(IEnumerable<ItemStack> toInsert)
        {
            return InventoryOps.RemainderAfterInsertCollection(toInsert, SlotList);
        }

        public IEnumerable<ItemStack> InsertPossible(IEnumerable<ItemStack> toInsert)
        {
            return InventoryOps.InsertPossible(toInsert, Slots);
        }


        public IEnumerator<ItemStack> GetEnumerator()
        {
            return Slots.Select(s => s.Peek()).GetEnumerator();
        }

        public void Add(ItemStack item)
        {
            Slots.First(s => s.CanInsertCompletely(item)).InsertCompletely(item);
        }

        public void Insert(int index, ItemStack item)
        {
            while (true)
            {
                if (index >= SlotList.Count)
                    throw new ArgumentOutOfRangeException();
                if (SlotList[index].Peek().IsDefault())
                {
                    SlotList[index].InsertCompletely(item);
                    return;
                }

                var extracted = SlotList[index].ExtractAll();
                SlotList[index].InsertCompletely(item);
                index += 1;
                item = extracted;
            }
        }

        public void RemoveAt(int index)
        {
            SlotList[index].ExtractAll();
        }

        public ItemStack this[int index]
        {
            get => SlotList[index].Peek();
            set
            {
                SlotList[index].ExtractAll();
                SlotList[index].InsertCompletely(value);
            }
        }

        public ItemStack RemainderIfInserted(ItemStack toInsert)
        {
            return InventoryOps.RemainderAfterInsertCollection(toInsert.MakeEnumerable(), SlotList).FirstOrDefault();
        }

        public ItemStack InsertPossible(ItemStack toInsert)
        {
            var ret = InventoryOps.InsertPossible(toInsert, Slots);
            if (ret!=toInsert)
                OnItemInsertedViaInventory?.Invoke(new ItemStack(toInsert.ID,toInsert.Value-ret.Value));
            return ret;
        }

        public IEnumerable<ItemStack> Peek()
        {
            return Slots.Select(s => s.Peek());
        }

        public IEnumerable<ItemStack> ExtractAll() => Slots.Select(s => s.ExtractAll()).ToList();

        public bool CanExtract()
        {
            return true;
        }

        public void AddSlot(SLOT slot)
        {
            slotList.Add(slot);
            slot.Changed += FireChanged;
            Changed?.Invoke();
        }

        void FireChanged()//I lost like 3 hours to this stupidity
        {
            Changed?.Invoke();
        }
        public void RemoveSlot(SLOT slot)
        {
            slotList.Remove(slot);
            slot.Changed -= FireChanged;
            FireChanged();
        }

        public void BulkAddSlots(IEnumerable<SLOT> slots)
        {
            foreach (var item in slots)
            {
                slotList.Add(item);
                item.Changed += FireChanged;
            }
            FireChanged();
        }
        public void BulkRemoveSlots(IEnumerable<SLOT> slots)
        {
            foreach (var item in slots)
            {
                slotList.Remove(item);
                item.Changed -= FireChanged;
            }
            FireChanged();
        }

        public void ClearSlots()
        {
            foreach (var item in slotList) 
                item.Changed -= FireChanged;
            
            slotList.Clear();
            FireChanged();
        }
    }
}