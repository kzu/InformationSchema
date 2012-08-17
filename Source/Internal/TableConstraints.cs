#region BSD License
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

namespace System.Data.Entity.InformationSchema.Internal
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

    [Table("TABLE_CONSTRAINTS", Schema = "INFORMATION_SCHEMA")]
    internal class TableConstraints
    {
        internal enum Kind
        {
            PrimaryKey,
            Unique,
            ForeignKey,
            Check,
        }

        public TableConstraints()
        {
            this.COLUMNS = new Collection<ConstraintColumnUsage>();
        }

        // PK
        public string CONSTRAINT_CATALOG { get; set; }
        public string CONSTRAINT_SCHEMA { get; set; }
        public string CONSTRAINT_NAME { get; set; }

        // FK
        public string TABLE_CATALOG { get; private set; }
        public string TABLE_SCHEMA { get; private set; }
        public string TABLE_NAME { get; private set; }

        private string type;

        public string CONSTRAINT_TYPE
        {
            get { return this.type; }
            set { this.type = value; this.Type = (Kind)Enum.Parse(typeof(Kind), value.Replace(" ", ""), true); }
        }

        public string IS_DEFERRABLE { get; private set; }
        public string INITIALLY_DEFERRED { get; private set; }

        [NotMapped]
        public Kind Type { get; private set; }

        [NotMapped]
        public bool Deferrable { get { return this.IS_DEFERRABLE != "NO"; } }

        [NotMapped]
        public bool InitiallyDeferred { get { return this.INITIALLY_DEFERRED != "NO"; } }

        public ICollection<ConstraintColumnUsage> COLUMNS { get; private set; }
    }
}
