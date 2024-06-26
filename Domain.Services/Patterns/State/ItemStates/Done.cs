﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Backlog;

namespace Domain.Services.Patterns.State.ItemStates
{
    public class Done : IStateItem
    {
        public Item Item { get; set; }

        public Done(Item item)
        {
            Item = item;
        }

        public void NextState()
        {
            var x = new ToDo();
            Item.State = x;
            x.Item = Item;
        }

        public IStateItem GetState()
        {
            return this;
        }
    }
}
