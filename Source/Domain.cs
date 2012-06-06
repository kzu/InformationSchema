
using System.ComponentModel.DataAnnotations;
namespace System.Data.Entity.InformationSchema
{
    [ComplexType]
    public class Domain
    {
        /// <summary>
        /// Always returns NULL. (for T-SQL)
        /// </summary>
        [Column("DOMAIN_CATALOG")]
        public string Catalog { get; set; }

        /// <summary>
        /// Always returns NULL. (for T-SQL)
        /// </summary>
        [Column("DOMAIN_SCHEMA")]
        public string Schema { get; set; }

        /// <summary>
        /// Returns the unique name for the collation if the column is character data or text data type. Otherwise, NULL is returned.
        /// </summary>
        [Column("DOMAIN_NAME")]
        public string Name { get; set; }
    }
}
