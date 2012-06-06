
using System.ComponentModel.DataAnnotations;
namespace System.Data.Entity.InformationSchema
{
    [ComplexType]
    public class Collation
    {
        /// <summary>
        /// Always returns NULL. (for T-SQL)
        /// </summary>
        [Column("COLLATION_CATALOG")]
        public string Catalog { get; set; }

        /// <summary>
        /// Always returns NULL. (for T-SQL)
        /// </summary>
        [Column("COLLATION_SCHEMA")]
        public string Schema { get; set; }

        /// <summary>
        /// Returns the unique name for the collation if the column is character data or text data type. Otherwise, NULL is returned.
        /// </summary>
        [Column("COLLATION_NAME")]
        public string Name { get; set; }
    }
}
