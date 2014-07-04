using System;
using SqlMapper.Framework.CustomAttributes;

namespace SqlMapperTest.EDs
{
    [Table("Categories")]
    public class Category
    {
        [Pk("CategoryID")]
        public int CategoryID { get; set; }
        public String CategoryName { get; set; }
        public String Description { get; set; }

    }
}
