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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    public class KeyInfo
    {
        private string TABLE_CATALOG { get; set; }
        private string TABLE_SCHEMA { get; set; }
        private string TABLE_NAME { get; set; }
        private string COLUMN_NAME { get; set; }

        [Column("ORDINAL_POSITION")]
        public int Position { get; private set; }

        internal static Expression GetKey()
        {
            return New(x => new { x.TABLE_CATALOG, x.TABLE_SCHEMA, x.TABLE_NAME, x.COLUMN_NAME });
        }

        internal static IEnumerable<Expression> GetHiddenProperties()
        {
            yield return Property(x => x.TABLE_CATALOG);
            yield return Property(x => x.TABLE_SCHEMA);
            yield return Property(x => x.TABLE_NAME);
            yield return Property(x => x.COLUMN_NAME);
        }

        private static Expression<Func<KeyInfo, T>> Property<T>(Expression<Func<KeyInfo, T>> property)
        {
            return property;
        }

        private static Expression<Func<KeyInfo, T>> New<T>(Expression<Func<KeyInfo, T>> key)
        {
            return key;
        }
    }
}
