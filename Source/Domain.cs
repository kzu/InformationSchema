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

    /// <summary>
    /// Represents the domain information of a user-defined data type.
    /// </summary>
    ///	<nuget id="System.Data.Entity.InformationSchema" />
    [ComplexType]
    public class Domain
    {
        /// <summary>
        /// If the column is an alias data type, this column is the database name in which the user-defined data type was created. Otherwise, NULL is returned.
        /// </summary>
        [Column("DOMAIN_CATALOG")]
        public string Catalog { get; set; }

        /// <summary>
        /// If the column is a user-defined data type, this column returns the name of the schema of the user-defined data type. Otherwise, NULL is returned.
        /// </summary>
        [Column("DOMAIN_SCHEMA")]
        public string Schema { get; set; }

        /// <summary>
        /// If the column is a user-defined data type, this column is the name of the user-defined data type. Otherwise, NULL is returned.
        /// </summary>
        [Column("DOMAIN_NAME")]
        public string Name { get; set; }
    }
}
