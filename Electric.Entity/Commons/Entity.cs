using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Electric.Entity.Commons
{
    public class EleEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Comment("创建者Id")]
        public long CreatorId { get; set; }


        [Comment("创建时间")]
        public DateTime CreationTime { get; set; } = DateTime.Now;

        [Comment("最后编辑时间")]
        public DateTime LastModificationTime { get; set; }
    }
}