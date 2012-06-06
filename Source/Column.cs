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

namespace System.Data.Entity.InformationSchema
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [Table("COLUMNS", Schema = "INFORMATION_SCHEMA")]
    public class Column
    {
        public Column()
        {
            this.CharacterSet = new CharacterSet();
            this.Collation = new Collation();
            this.Domain = new Domain();
        }

        internal string TABLE_CATALOG { get; private set; }
        internal string TABLE_SCHEMA { get; private set; }
        internal string TABLE_NAME { get; private set; }
        internal string IS_NULLABLE { get; private set; }

        /// <summary>
        /// Column name.
        /// </summary>
        [Column("COLUMN_NAME")]
        public string Name { get; private set; }

        /// <summary>
        /// Column identification number.
        /// </summary>
        [Column("ORDINAL_POSITION")]
        public int Position { get; private set; }

        /// <summary>
        /// Default value of the column.
        /// </summary>
        [Column("COLUMN_DEFAULT")]
        public string DefaultValue { get; private set; }

        /// <summary>
        /// System-supplied data type.
        /// </summary>
        [Column("DATA_TYPE")]
        public string DataType { get; private set; }

        /// <summary>
        /// Nullability of the column.
        /// </summary>
        [NotMapped]
        public bool IsNullable { get { return this.IS_NULLABLE == "YES"; } }

        /// <summary>
        /// Maximum length, in characters, for binary data, character data, or text and image data.
        /// -1 for xml and large-value type data. Otherwise, NULL is returned. 
        /// </summary>
        /// <remarks>
        /// For more information, see Data Types (Transact-SQL) (http://msdn.microsoft.com/en-us/library/ms187752).
        /// </remarks>
        [Column("CHARACTER_MAXIMUM_LENGTH")]
        public int? MaxLength { get; private set; }

        /// <summary>
        /// Maximum length, in bytes, for binary data, character data, or text and image data.
        /// -1 for xml and large-value type data. Otherwise, NULL is returned. 
        /// </summary>
        /// <remarks>
        /// For more information, see Data Types (Transact-SQL) (http://msdn.microsoft.com/en-us/library/ms187752).
        /// </remarks>
        [Column("CHARACTER_OCTET_LENGTH")]
        public int? OctetLength { get; private set; }

        /// <summary>
        /// Precision of approximate numeric data, exact numeric data, integer data, or monetary data. Otherwise, NULL is returned.
        /// </summary>
        [Column("NUMERIC_PRECISION")]
        public int? NumericPrecision { get; private set; }

        /// <summary>
        /// Precision radix of approximate numeric data, exact numeric data, integer data, or monetary data. Otherwise, NULL is returned.
        /// </summary>
        [Column("NUMERIC_PRECISION_RADIX")]
        public int? NumericPrecisionRadix { get; private set; }

        /// <summary>
        /// Scale of approximate numeric data, exact numeric data, integer data, or monetary data. Otherwise, NULL is returned.
        /// </summary>
        [Column("NUMERIC_SCALE")]
        public int? NumericScale { get; private set; }

        /// <summary>
        /// Subtype code for datetime and ISO interval data types. For other data types, NULL is returned.
        /// </summary>
        [Column("DATETIME_PRECISION")]
        public short? DateTimePrecision { get; private set; }

        public CharacterSet CharacterSet { get; private set; }

        public Collation Collation { get; private set; }

        public Domain Domain { get; private set; }

        public bool IsKey { get { return this.Table.KEYS.Any(k => k.COLUMN_NAME == this.Name); } }

        [NotMapped]
        internal Table Table { get; set; }
    }
}
