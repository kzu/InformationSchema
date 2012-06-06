﻿#region BSD License
/* 
Copyright (c) 2011, NETFx
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, 
are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list 
  of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice, this 
  list of conditions and the following disclaimer in the documentation and/or other 
  materials provided with the distribution.

* Neither the name of Clarius Consulting nor the names of its contributors may be 
  used to endorse or promote products derived from this software without specific 
  prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY 
EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES 
OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT 
SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, 
INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED 
TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR 
BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN 
ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH 
DAMAGE.
*/
#endregion

namespace System.Data.Entity.InformationSchema
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [Table("TABLES", Schema = "INFORMATION_SCHEMA")]
    public class Table
    {
        protected Table()
        {
            this.COLUMNS = new ObservableCollection<Column>();
            // Monitor columns added/removed to set the column parent 
            // table while avoiding a reference loop for EF.
            this.COLUMNS.CollectionChanged += OnColumnsChanged;
            this.KEYS = new Collection<KeyInfo>();
        }

        [Column("TABLE_CATALOG")]
        public string Catalog { get; private set; }
        [Column("TABLE_SCHEMA")]
        public string Schema { get; private set; }
        [Column("TABLE_NAME")]
        public string Name { get; private set; }

        [NotMapped]
        public IEnumerable<Column> Columns { get { return this.COLUMNS; } }

        internal string TABLE_TYPE { get; private set; }

        internal ObservableCollection<Column> COLUMNS { get; set; }

        // Exposing these at this level because joining with Column 
        // fails. See http://kzu.to/MEwThQ for the previous approach.
        internal ICollection<KeyInfo> KEYS { get; set; }

        private void OnColumnsChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add)
                args.NewItems.OfType<Column>().ToList().ForEach(c => c.Table = this);
            else if (args.Action == NotifyCollectionChangedAction.Remove)
                args.OldItems.OfType<Column>().ToList().ForEach(c => c.Table = null);
        }
    }
}
